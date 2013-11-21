﻿using System;
using System.Reflection;
using NCop.Aspects.Advices;
using NCop.Aspects.Engine;
using NCop.Aspects.Framework;
using NCop.Aspects.Weaving.Expressions;
using NCop.Core.Extensions;

namespace NCop.Aspects.Aspects
{
	internal class MethodInterceptionAspectDefinition : AbstractAspectDefinition
	{
		private readonly MethodInterceptionAspectAttribute aspect = null;

		internal MethodInterceptionAspectDefinition(MethodInterceptionAspectAttribute aspect)
			: base(aspect) {
			this.aspect = aspect;
		}

		public override AspectType AspectType {
			get {
				return AspectType.MethodInterceptionAspect;
			}
		}

		protected override void BulidAdvices() {
			Aspect.AspectType
				 .GetOverridenMethods()
				 .ForEach(method => {
					 TryBulidAdvice<OnMethodInvokeAdviceAttribute>(method, (advice, mi) => {
						 return new OnMethodInvokeAdviceDefinition(advice, mi);
					 });
				 });
		}

		public override IHasAspectExpression Accept(AspectVisitor visitor) {
			return visitor.Visit(aspect).Invoke(this);
		}
	}
}
