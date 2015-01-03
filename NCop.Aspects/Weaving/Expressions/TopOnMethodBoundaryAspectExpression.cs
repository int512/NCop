﻿using NCop.Aspects.Aspects;
using NCop.Aspects.Extensions;

namespace NCop.Aspects.Weaving.Expressions
{
	internal class TopOnMethodBoundaryAspectExpression : AbstractAspectMethodExpression
    {
        internal TopOnMethodBoundaryAspectExpression(IAspectExpression aspectExpression, IAspectDefinition aspectDefinition)
            : base(aspectExpression, aspectDefinition) {
        }

        public override IAspectWeaver Reduce(IAspectWeavingSettings aspectWeavingSettings) {
            var clonedAspectWeavingSettings = aspectWeavingSettings.CloneWith(settings => {
                var localBuilderRepository = new LocalBuilderRepository();
                var aspectArgumentImplType = aspectDefinition.ToAspectArgumentImpl();
                    
                settings.LocalBuilderRepository = localBuilderRepository;
                settings.ByRefArgumentsStoreWeaver = new TopAspectByRefArgumentsStoreWeaver(aspectArgumentImplType, aspectDefinition.Member, localBuilderRepository);
            });

            var nestedWeaver = aspectExpression.Reduce(clonedAspectWeavingSettings);

            return new TopOnMethodBoundaryAspectWeaver(nestedWeaver, aspectDefinition, clonedAspectWeavingSettings);
        }
    }
}
