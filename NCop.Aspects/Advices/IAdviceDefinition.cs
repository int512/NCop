﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NCop.Aspects.Engine;
using NCop.Aspects.Weaving.Expressions;
using NCop.Aspects.Weaving;

namespace NCop.Aspects.Advices
{
	public interface IAdviceDefinition : IAcceptsVisitor<AdviceVisitor, IAdviceExpression>, IAcceptsVisitor<AdviceDiscoveryVisitor>
	{
		IAdvice Advice { get; }
		MethodInfo AdviceMethod { get; }
	}
}
