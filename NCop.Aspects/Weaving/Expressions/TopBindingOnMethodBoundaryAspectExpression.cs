﻿using NCop.Aspects.Aspects;
using NCop.Aspects.Extensions;

namespace NCop.Aspects.Weaving.Expressions
{
    internal class TopBindingOnMethodBoundaryAspectExpression : AbstractAspectExpression
    {
        internal TopBindingOnMethodBoundaryAspectExpression(IAspectExpression aspectExpression, IAspectDefinition aspectDefinition)
            : base(aspectExpression, aspectDefinition) {
        }

        public override IAspectWeaver Reduce(IAspectWeavingSettings aspectWeavingSettings) {
            var clonedSettings = aspectWeavingSettings.CloneWith(settings => {
                settings.ByRefArgumentsStoreWeaver = NullObjectByRefArgumentsStoreWeaver.Empty;
            });

            var nestedWeaver = aspectExpression.Reduce(clonedSettings);

            return new TopBindingOnMethodBoundaryAspectWeaver(nestedWeaver, aspectDefinition, clonedSettings);
        }
    }
}
