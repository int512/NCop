﻿using System;
using System.Reflection;

namespace NCop.Aspects.Weaving
{
    public class ArgumentsWeavingSettings : IArgumentsWeavingSettings
    {
        public bool IsProperty { get; set; }
        public Type ReturnType { get; set; }
        public Type AspectType { get; set; }
        public bool IsFunction { get; set; }
        public Type ArgumentType { get; set; }
        public Type[] Parameters { get; set; }
        public FieldInfo BindingsDependency { get; set; }
    }
}
