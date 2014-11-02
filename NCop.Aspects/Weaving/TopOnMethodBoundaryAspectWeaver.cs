﻿using NCop.Aspects.Aspects;
using NCop.Core.Extensions;
using NCop.Weaving;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace NCop.Aspects.Weaving
{
    internal class TopOnMethodBoundaryAspectWeaver : AbstractTopOnMethodBoundaryAspectWeaver
    {
        protected IArgumentsWeaver argumentsWeaver = null;

        internal TopOnMethodBoundaryAspectWeaver(IAspectWeaver nestedWeaver, IAspectDefinition aspectDefinition, IAspectMethodWeavingSettings aspectWeavingSettings)
            : base(nestedWeaver, aspectDefinition, aspectWeavingSettings) {
            var @params = weavingSettings.MethodInfoImpl.GetParameters();

            methodScopeWeavers = new List<IMethodScopeWeaver>();
            argumentsWeavingSetings.Parameters = @params.ToArray(@param => @param.ParameterType).ToArray();
            argumentsWeaver = new TopOnMethodBoundaryArgumentsWeaver(argumentsWeavingSetings, aspectWeavingSettings);
        }

        public override ILGenerator Weave(ILGenerator ilGenerator) {
            argumentsWeaver.Weave(ilGenerator);
            
            return weaver.Weave(ilGenerator);
        }
    }
}
