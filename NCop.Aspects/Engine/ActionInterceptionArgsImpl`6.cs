﻿using NCop.Aspects.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NCop.Aspects.Engine
{
    public class ActionInterceptionArgsImpl<TInstance, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> : ActionInterceptionArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>, IActionArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>
    {
        private TInstance instance = default(TInstance);
        private readonly IActionBinding<TInstance, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> actionBinding = null;

        public ActionInterceptionArgsImpl(TInstance instance, MethodInfo method, IActionBinding<TInstance, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> actionBinding, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6) {
            Arg1 = arg1;
            Arg2 = arg2;
            Arg3 = arg3;
            Arg4 = arg4;
            Arg5 = arg5;
            Arg6 = arg6;
            Method = method;
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
