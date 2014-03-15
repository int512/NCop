﻿using NCop.IoC.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCop.IoC
{
    public interface IContainerRegistry : IArgumentsFluentRegistry, INCopRegistry, IEnumerable<IRegistration>, IRegisterEntry, IRegistrationResolver
    {
    }
}
