﻿using NCop.Aspects.Framework;
using NCop.Core.Extensions;
using System;
using System.Collections.Generic;

namespace NCop.Aspects.Engine
{
    public abstract class AbstractFunctionEventBroker<TInstance, TArg1, TResult> : IEventBroker<Func<TArg1, TResult>>
    {
        protected readonly TInstance instance = default(TInstance);
        private readonly LinkedList<Func<TArg1, TResult>> linkedHandlers = null;
        private readonly IEventFunctionBinding<TInstance, TArg1, TResult> binding = null;

        protected AbstractFunctionEventBroker(TInstance instance, IEventFunctionBinding<TInstance, TArg1, TResult> binding) {
            this.binding = binding;
            this.instance = instance;
            linkedHandlers = new LinkedList<Func<TArg1, TResult>>();
        }

        public void AddHandler(Func<TArg1, TResult> handler) {
            var isFirst = linkedHandlers.First.IsNull();

            if (isFirst) {
                SubscribeImpl();
            }

            linkedHandlers.AddLast(handler);
        }

        protected TResult OnEventFired(TArg1 arg1) {
            var args = new EventFunctionInterceptionArgsImpl<TInstance, TArg1, TResult>();

            for (var i = linkedHandlers.First; i != null; i = i.Next) {
                args.Arg1 = arg1;
                args.Handler = i.Value;
                OnInvokeHandler(args);
            }

            return args.ReturnValue;
        }

        public void InvokeHandler(Func<TArg1, TResult> handler) {

        }

        public void RemoveHandler(Func<TArg1, TResult> handler) {
            linkedHandlers.Remove(handler);

            if (linkedHandlers.First.IsNull()) {
                UnsubscribeImpl();
            }
        }

        protected abstract void SubscribeImpl();

        protected abstract void UnsubscribeImpl();

        protected abstract void OnInvokeHandler(EventFunctionInterceptionArgsImpl<TInstance, TArg1, TResult> args);
    }
}
