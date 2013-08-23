﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCop.Aspects.Framework
{
	public class MethodExecutionArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult> : MethodExecutionArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult> 
	{
		public TArg7 Arg7 { get; private set; }
	}
}