﻿using System;
using NCop.Aspects.Aspects;
using NCop.Aspects.Extensions;
using NCop.Composite.Weaving;
using NCop.Weaving.Extensions;
using System.Reflection;
using System.Reflection.Emit;

namespace NCop.Aspects.Weaving
{
    internal class BindingMethodInterceptionAspectWeaver : AbstractMethodInterceptionAspectWeaver
    {
        protected readonly Type topAspectInScopeArgType = null;
        protected readonly IArgumentsWeaver argumentsWeaver = null;

        internal BindingMethodInterceptionAspectWeaver(Type topAspectInScopeArgType, IAspectDefinition aspectDefinition, IAspectMethodWeavingSettings aspectWeavingSettings, FieldInfo weavedType)
            : base(aspectDefinition, aspectWeavingSettings, weavedType) {
            this.topAspectInScopeArgType = topAspectInScopeArgType;
            ArgumentType = argumentsWeavingSettings.ArgumentType;
            argumentsWeavingSettings.BindingsDependency = weavedType;
            argumentsWeaver = new BindingMethodInterceptionArgumentsWeaver(topAspectInScopeArgType, argumentsWeavingSettings, aspectWeavingSettings);
            methodScopeWeavers.Add(new NestedAspectArgsMappingWeaver(topAspectInScopeArgType, aspectWeavingSettings, argumentsWeavingSettings));
            weaver = new MethodScopeWeaversQueue(methodScopeWeavers);
        }

        public override ILGenerator Weave(ILGenerator ilGenerator) {
            argumentsWeaver.Weave(ilGenerator);
           
            return weaver.Weave(ilGenerator);
        }
    }
}
