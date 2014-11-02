﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace NCop.Weaving
{
	public class SetPropertySignatureWeaver : AbstractMemberSignatureWeaver
	{
		public SetPropertySignatureWeaver(ITypeDefinition typeDefinition)
			: base(typeDefinition) {
		}

		public override MethodBuilder Weave(MethodInfo methodInfo) {
			return typeDefinition.TypeBuilder.DefineMethod(methodInfo);
		}
	}
}