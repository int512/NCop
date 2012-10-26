﻿using NCop.Aspects.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NCop.Aspects.Aspects.Interception
{
    public class SetPropertyInterception : IJoinPoint, IPropertySetExecuter, IPreventable
    {
        public void ProceedSetValue() {
        }

        public object Instance { get; private set; }

        public bool IsPrevented { get; private set; }

        public object Argument { get; private set; }

        public MemberInfo TargetMember { get; private set; }
    }
}
