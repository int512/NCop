﻿using NCop.Aspects.Advices;
using NCop.Aspects.Engine;

namespace NCop.Aspects.Framework
{
    public abstract class OnActionBoundaryAspect<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> : IOnMethodBoundaryAspect
    {
        [OnMethodEntryAdvice]
        public virtual void OnEntry(ActionExecutionArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> args) { }

        [FinallyAdvice]
        public virtual void OnExit(ActionExecutionArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> args) { }

        [OnMethodSuccessAdvice]
        public virtual void OnSuccess(ActionExecutionArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> args) { }

        [OnMethodExceptionAdvice]
        public virtual void OnException(ActionExecutionArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> args) { }
    }
}
