﻿using System.Reflection;

namespace NCop.Aspects.Aspects
{
    public class AspectMap
    {
        public static AspectMap Empty = new AspectMap();

        private AspectMap() {
        }

        public AspectMap(MemberInfo member, IAspectDefinitionCollection aspects) {
            Member = member;
            Aspects = aspects;
        }

        public MemberInfo Member { get; private set; }
        public IAspectDefinitionCollection Aspects { get; private set; }
    }
}
