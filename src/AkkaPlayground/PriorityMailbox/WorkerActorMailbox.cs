using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Akka.Configuration;
using Akka.Dispatch;

namespace PriorityMailbox
{
    public class WorkerActorMailbox : UnboundedPriorityMailbox
    {
        public WorkerActorMailbox(Settings settings, Config config) : base(settings, config)
        {
        }

        protected override int PriorityGenerator(object message)
        {
            switch (message)
            {
                case PanicMessage t:
                    return 0;
                default:
                    return 1;
            }
        }
    }
}
