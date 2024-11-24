using System;

namespace Bears.Core.MessengerInternal
{
    internal interface IMessageEvent
    {
        int Count { get; }
        
        void RemoveListener(object action);
    }
    
    internal interface IMessageEvent<in TAction> : IMessageEvent where TAction : Delegate
    {
        void AddListener(TAction action);
        void RemoveListener(TAction action);
    }
}