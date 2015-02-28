﻿using NCop.Aspects.Advices;
using NCop.Aspects.Engine;

namespace NCop.Aspects.Framework
{
    public abstract class FunctionInterceptionAspect<TArg1, TArg2, TArg3, TResult> : IMethodInterceptionAspect
    {
        [OnMethodInvokeAdvice]
        public virtual void OnInvoke(FunctionInterceptionArgs<TArg1, TArg2, TArg3, TResult> args) {
            args.Proceed();
        }
    }
}
