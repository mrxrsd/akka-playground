using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace AtLeastOnce
{
    public class WriterActor : ReceiveActor
    {
        private readonly DatabaseService _dbContext;

        public WriterActor(DatabaseService dbContext)
        {
            _dbContext = dbContext;

            ReceiveAsync<ReliableDeliveryEnvelope<Insert>>(InsertHandler);
        }

        private async Task InsertHandler(ReliableDeliveryEnvelope<Insert> f)
        {
            await _dbContext.Write(f.Message.Id, f.Message.Msg);
            Context.Sender.Tell(new ReliableDeliveryAck(f.MessageId));
            Console.WriteLine($"Inserted {f.Message.Id}");
        }
    }
}
