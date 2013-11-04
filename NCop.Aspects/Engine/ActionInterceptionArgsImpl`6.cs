﻿using NCop.Aspects.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCop.Aspects.Engine
{
    public class ActionInterceptionArgsImpl<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> : ActionExecutionArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>
    {
        private readonly IActionBinding<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> actionBinding = null;

        public ActionInterceptionArgsImpl(object instance, IActionBinding<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> actionBinding, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6) {
            Arg1 = arg1;
            Arg2 = arg2;
            Arg3 = arg3;
            Arg4 = arg4;
            Arg5 = arg5;
            Instance = instance;
            this.actionBinding = actionBinding;
        }

        public override void Proceed() {
            var instance = Instance;

            actionBinding.Invoke(ref instance, Arg1, Arg2, Arg3, Arg4, Arg5, Arg6);
        }
    }
}
