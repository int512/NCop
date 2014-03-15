﻿using NCop.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCop.Composite.Extensions;
using NCop.Core;

namespace NCop.Composite.Engine
{
    internal class CompositeRegistryDecorator : INCopRegistry
    {
        private readonly INCopRegistry regisrty = null;

        public CompositeRegistryDecorator(INCopRegistry regisrty) {
            this.regisrty = regisrty;
        }

        public void Register(Type concreteType, Type serviceType, IEnumerable<TypeMap> dependencies, string name = null) {
            name = serviceType.GetNameFromAttribute();

            regisrty.Register(concreteType, serviceType, dependencies, name);
        }
    }
}
