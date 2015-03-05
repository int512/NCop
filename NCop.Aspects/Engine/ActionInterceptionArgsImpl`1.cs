﻿using NCop.Aspects.Framework;
using System.Reflection;

namespace NCop.Aspects.Engine
{
    public class ActionInterceptionArgsImpl<TInstance, TArg1> : ActionInterceptionArgs<TArg1>, IActionArgs<TArg1>
    {
        private TInstance instance = default(TInstance);
        private readonly IActionBinding<TInstance, TArg1> actionBinding = null;

        public ActionInterceptionArgsImpl(TInstance instance, MethodInfo method, IActionBinding<TInstance, TArg1> actionBinding, TArg1 arg1) {
            Arg1 = arg1;
            this.Method = method;
            this.actionBinding = actionBinding;
            Instance = this.instance = instance;
        }

        public override void Proceed() {
            actionBinding.Proceed(ref instance, this);
        }

        public override void Invoke() {
            actionBinding.Invoke(ref instance, this);
        }
    }
}
