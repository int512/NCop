﻿using System;
using NCop.Aspects.Advices;
using NCop.Aspects.Engine;
using NCop.Aspects.LifetimeStrategies;
using System.Diagnostics;
using NCop.Aspects.Aspects;
using NCop.Aspects.Framework;

namespace NCop.Aspects.Framework
{
    public abstract class OnActionBoundaryAspectImpl<TArg1, TArg2, TArg3, TArg4, TArg5>
    {	
		[OnMethodEntryAdvice]
		public virtual void OnEntry(ActionExecutionArgs<TArg1, TArg2, TArg3, TArg4, TArg5> args) { }

		[FinallyAdvice]
		public virtual void OnExit(ActionExecutionArgs<TArg1, TArg2, TArg3, TArg4, TArg5> args) { }

		[OnMethodSuccessAdvice]
		public virtual void OnSuccess(ActionExecutionArgs<TArg1, TArg2, TArg3, TArg4, TArg5> args) { }

		[OnMethodExceptionAdvice]
		public virtual void OnException(ActionExecutionArgs<TArg1, TArg2, TArg3, TArg4, TArg5> args) { }
	}
}