﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCop.Aspects.Advices
{
	public enum AdviceType
	{
		FinallyAdvice,
		OnMethodEntryAdvice,
		OnMethodInvokeAdvice,
		OnMethodSuccessAdvice,
		OnMethodExceptionAdvice,
	}
}
