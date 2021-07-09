using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Akka.IO;
using Akka.Persistence;

namespace AtLeastOnce
{
    public class StoreActor : AtLeastOnceDeliveryReceiveActor
    {
        // Going to use our name for persistence purposes
        public override string PersistenceId => Context.Self.Path.Name;
        private ICancelable _recurringSnapshotCleanup;
        private readonly IActorRef _targetActor;


        public StoreActor(IActorRef targetActor) : base(settings =>
        {
            settings.WithRedeliverInterval(TimeSpan.FromMinutes(2));

            return settings;
        })
        {
            _targetActor = targetActor;


            Command<Insert>(write =>
            {
                Deliver(_targetActor.Path, messageId => new ReliableDeliveryEnvelope<Insert>(write, messageId));
                SaveSnapshot(GetDeliverySnapshot());
            });

            #region protocol 
            // recover the most recent at least once delivery state
            Recover<SnapshotOffer>(offer => offer.Snapshot is Akka.Persistence.AtLeastOnceDeliverySnapshot, offer =>
            {
                var snapshot = offer.Snapshot as Akka.Persistence.AtLeastOnceDeliverySnapshot;
                SetDeliverySnapshot(snapshot);
            });


            Command<ReliableDeliveryAck>(ack =>
            {
                ConfirmDelivery(ack.MessageId);
            });

            Command<CleanSnapshots>(clean =>
            {
                // save the current state (grabs confirmations)
                SaveSnapshot(GetDeliverySnapshot());
            });

            Command<SaveSnapshotSuccess>(saved =>
            {
                var seqNo = saved.Metadata.SequenceNr;
                DeleteSnapshots(new SnapshotSelectionCriteria(seqNo, saved.Metadata.Timestamp.AddMilliseconds(-1))); // delete all but the most current snapshot
            });

            Command<SaveSnapshotFailure>(failure =>
            {
                // log or do something else
            });

            Command<DeleteSnapshotsSuccess>(f => { });

            #endregion
        }

        protected override void PreStart()
        {

            _recurringSnapshotCleanup =
                Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(10), Self, new CleanSnapshots(), ActorRefs.NoSender);

            base.PreStart();
        }

        protected override void PostStop()
        {
            _recurringSnapshotCleanup?.Cancel();
            base.PostStop();
        }
    }
}
