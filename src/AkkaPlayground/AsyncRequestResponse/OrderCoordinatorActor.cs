using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Core.Messages.Common;

namespace AsyncRequestResponse
{
    public class OrderCoordinatorActor : ReceiveActor
    {
        private IActorRef _storageActor;
        private IActorRef _kitchen;

        public OrderCoordinatorActor()
        {
            Receive<AskPizza>(m =>
            {
                _storageActor.Tell(new RequestFlow<HaveIngredients>(new HaveIngredients(m.Flavour), Context.Sender));
            });


            Receive<ResponseFlow<HaveIngredients, MissingIngredients>>(flow =>
            {
                flow.IntendedDestination.Tell("sorry");
            });

            Receive<ResponseFlow<HaveIngredients, WeHaveAllIngrendients>>(flow =>
            {
                _kitchen.Tell(new AskPizza(flow.Request.Flavour), flow.IntendedDestination);
            });


        }



        protected override void PreStart()
        {
            base.PreStart();

            _storageActor = Context.ActorOf(Props.Create<StorageActor>(), "storageActor");
            _kitchen = Context.ActorOf(Props.Create<KitchenActor>(), "kitchenActor");

        }
    }
}
