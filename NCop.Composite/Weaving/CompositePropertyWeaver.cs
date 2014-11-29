﻿using System;
using System.Reflection;
using NCop.Aspects.Aspects;
using NCop.Aspects.Weaving;
using NCop.Weaving;
using NCop.Aspects.Extensions;

namespace NCop.Composite.Weaving
{
    public class CompositePropertyWeaver : AbstractPropertyWeaver
    {
        private readonly IAspectDefinitionCollection aspectDefinitions = null;
        private readonly IAspectPropertyWeavingSettings aspectWeavingSettings = null;

        public CompositePropertyWeaver(IAspectDefinitionCollection aspectDefinitions, IAspectPropertyWeavingSettings aspectWeavingSettings)
            : base(aspectWeavingSettings.WeavingSettings) {
            this.aspectDefinitions = aspectDefinitions;
            this.aspectWeavingSettings = aspectWeavingSettings;
        }

        public override IMethodWeaver GetGetMethod() {
            if (CanRead) {
                return new GetPropertyAspectWeaver(aspectDefinitions, aspectWeavingSettings.ToGetPropertyAspectWeavingSettings());
            }

            return null;
        }

        public override IMethodWeaver GetSetMethod() {
            if (CanWrite) {
                return new SetPropertyAspectWeaver(aspectDefinitions, aspectWeavingSettings.ToSetPropertyAspectWeavingSettings());
            }

            return null;
        }
    }
}
