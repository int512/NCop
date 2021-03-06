﻿using NCop.Aspects.Aspects;
using NCop.Aspects.Extensions;
using NCop.Weaving;
using System.Reflection.Emit;

namespace NCop.Aspects.Weaving
{
    internal class BindingMethodAspectDecoratorWeaver : AbstractMethodScopeWeaver, IAspectWeaver
    {
        private readonly IBindingWeaver weaver = null;
        private readonly IMethodScopeWeaver methodDecoratorScopeWeaver = null;

        internal BindingMethodAspectDecoratorWeaver(IMethodAspectDefinition aspectDefinition, IAspectWeavingSettings aspectWeavingSettings, IArgumentsWeavingSettings argumentsWeavingSettings)
            : base(aspectDefinition.Member, aspectWeavingSettings.WeavingSettings) {
            var bindingSettings = aspectDefinition.ToBindingSettings();

            methodDecoratorScopeWeaver = new MethodDecoratorScopeWeaver(aspectDefinition.Member, aspectWeavingSettings);
            weaver = new MethodDecoratorBindingWeaver(aspectDefinition.Member, bindingSettings, aspectWeavingSettings, this);
        }

        public override void Weave(ILGenerator ilGenerator) {
            methodDecoratorScopeWeaver.Weave(ilGenerator);
        }
    }
}
