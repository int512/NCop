﻿
namespace NCop.Aspects.Framework
{
    public abstract class ActionInterceptionArgs<TArg1, TArg2, TArg3, TArg4> : ActionInterceptionArgs<TArg1, TArg2, TArg3>
    {
        public TArg4 Arg4 { get; set; }
	}
}
