using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace Core.Messages.Common
{

    public static class Flow
    {
        public static ResponseFlow<TRequest, TResponse> Respond<TRequest, TResponse>(TResponse response, RequestFlow<TRequest> request)
        {
            return new ResponseFlow<TRequest, TResponse>(response, request.IntendedDestination, request.ConversationId, request.Request);
        }
    }

    public class RequestFlow<T>
    {
        public T Request { get;  }
        public IActorRef IntendedDestination { get; }
        public Guid ConversationId { get; }

        public RequestFlow(T request, IActorRef intendedDestination, Guid? conversationId = null)
        {
            ConversationId = conversationId ?? Guid.NewGuid();
            IntendedDestination = intendedDestination;
            Request = request;
        }
    }

    public class ResponseFlow<TRequest, TResponse>
    {
        public TResponse Response { get; }
        public IActorRef IntendedDestination { get; }
        public Guid ConversationId { get; }
        public TRequest Request { get; }

        public ResponseFlow(TResponse response, IActorRef intendedDestination, Guid conversationId, TRequest request = default)
        {
            Response = response;
            IntendedDestination = intendedDestination;
            ConversationId = conversationId;
            Request = request;
        }
    }
}
