﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NCop.Aspects.Weaving;
using NCop.Aspects.Weaving.Expressions;

namespace NCop.Aspects.Advices
{
    internal class OnSetPropertyAdviceDefinition : AbstractAdviceDefinition
    {
        private readonly OnSetPropertyAdviceAttribute advice = null;

        internal OnSetPropertyAdviceDefinition(OnSetPropertyAdviceAttribute advice, MethodInfo adviceMethod)
            : base(advice, adviceMethod) {
            this.advice = advice;
        }

        public override IAdviceExpression Accept(AdviceVisitor visitor) {
            return visitor.Visit(advice).Invoke(this);
        }

        public override void Accept(AdviceDiscoveryVisitor visitor) {
            visitor.Visit(advice);
        }
    }
}
