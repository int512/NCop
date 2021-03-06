﻿using NCop.Aspects.Advices;
using NCop.Aspects.Aspects;
using NCop.Aspects.Weaving.Expressions;
using NCop.Weaving;
using NCop.Weaving.Extensions;
using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace NCop.Aspects.Weaving
{
    internal class TopGetPropertyInterceptionAspectWeaver : AbstractInterceptionAspectWeaver
    {
        protected readonly IArgumentsWeaver argumentsWeaver = null;

        internal TopGetPropertyInterceptionAspectWeaver(IPropertyAspectDefinition aspectDefinition, IAspectWeavingSettings aspectWeavingSettings, FieldInfo weavedType)
            : base(aspectDefinition, aspectWeavingSettings, weavedType) {
            argumentsWeavingSettings.BindingsDependency = weavedType;
            argumentsWeavingSettings.Parameters = new[] { aspectDefinition.Member.PropertyType };
            argumentsWeaver = new TopGetPropertyInterceptionArgumentsWeaver(aspectDefinition.Member, argumentsWeavingSettings, aspectWeavingSettings);
            weaver = new MethodScopeWeaversQueue(methodScopeWeavers);
        }

        protected override IAdviceExpression ResolveInterceptionAdviceExpression() {
            IAdviceDefinition selectedAdviceDefinition = null;
            var onGetPropertyAdvice = adviceDiscoveryVistor.OnGetPropertyAdvice;
            Func<IAdviceDefinition, IAdviceExpression> adviceExpressionFactory = null;

            adviceExpressionFactory = adviceVisitor.Visit(adviceDiscoveryVistor.OnGetPropertyAdvice);
            selectedAdviceDefinition = advices.First(advice => advice.Advice.Equals(onGetPropertyAdvice));

            return adviceExpressionFactory(selectedAdviceDefinition);
        }

        public override void Weave(ILGenerator ilGenerator) {
            LocalBuilder aspectArgLocalBuilder = null;
            var argumentType = argumentsWeavingSettings.ArgumentType;
            var valueProperty = argumentType.GetProperty("Value");

            argumentsWeaver.Weave(ilGenerator);
            weaver.Weave(ilGenerator);
            aspectArgLocalBuilder = localBuilderRepository.Get(argumentType);
            ilGenerator.EmitLoadLocal(aspectArgLocalBuilder);
            ilGenerator.Emit(OpCodes.Callvirt, valueProperty.GetGetMethod());
        }
    }
}
