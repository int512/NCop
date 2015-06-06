﻿using System;
using System.Reflection.Emit;

namespace NCop.Weaving
{
    public abstract class AbstractFieldBuilderDefinition : IFieldBuilderDefinition
    {
        protected AbstractFieldBuilderDefinition(Type type, TypeBuilder typeBuilder, string fieldName = null) {
            Type = type;
            FieldBuilder = new FieldWeaver(typeBuilder, type, fieldName).Weave();
        }

        public Type Type { get; private set; }

        public FieldBuilder FieldBuilder { get; private set; }
    }
}
