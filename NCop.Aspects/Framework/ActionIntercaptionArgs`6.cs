﻿
namespace NCop.Aspects.Framework
{
    public abstract class ActionInterceptionArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> : ActionInterceptionArgs<TArg1, TArg2, TArg3, TArg4, TArg5>
    {
        public TArg6 Arg6 { get; set; }
	}
}
