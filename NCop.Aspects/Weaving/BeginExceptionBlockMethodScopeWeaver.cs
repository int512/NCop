﻿using NCop.Composite.Weaving;
using NCop.Core.Extensions;
using NCop.Weaving;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace NCop.Aspects.Weaving
{
	internal class BeginExceptionBlockMethodScopeWeaver : IMethodScopeWeaver
	{
		internal BeginExceptionBlockMethodScopeWeaver() {
		}

		public void Weave(ILGenerator ilGenerator) {
			ilGenerator.BeginExceptionBlock();
		}
	}
}
