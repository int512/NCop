﻿using NCop.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using NCop.Weaving.Extensions;

namespace NCop.Aspects.Weaving
{
    internal abstract class AbstractBindingByRefArgumentsWeaver : AbstractByRefArgumentsStoreWeaver
    {
        protected readonly Type aspectArgumentType = null;
        protected readonly IDictionary<int, LocalBuilder> byRefParamslocalBuilderMap = null;

        internal AbstractBindingByRefArgumentsWeaver(Type aspectArgumentType, MethodInfo methodInfoImpl, ILocalBuilderRepository localBuilderRepository)
            : base(methodInfoImpl, localBuilderRepository) {
            this.aspectArgumentType = aspectArgumentType;
            byRefParamslocalBuilderMap = new Dictionary<int, LocalBuilder>();
        }

        public override bool Contains(int argPosition) {
            return byRefParamslocalBuilderMap.ContainsKey(argPosition);
        }

        public override void StoreArgsIfNeeded(ILGenerator ilGenerator) {
            parameters.ForEach(param => {
                LocalBuilder localBuilder;
                int argPosition = param.Position + 1;
                Type parameterElementType = param.ParameterType.GetElementType();
                var property = aspectArgumentType.GetProperty("Arg{0}".Fmt(argPosition));

                if (!byRefParamslocalBuilderMap.TryGetValue(argPosition, out localBuilder)) {
                    localBuilder = ilGenerator.DeclareLocal(parameterElementType);
                    byRefParamslocalBuilderMap.Add(argPosition, localBuilder);
                }

                WeaveAspectArg(ilGenerator);
                ilGenerator.Emit(OpCodes.Callvirt, property.GetGetMethod());
                ilGenerator.EmitStoreLocal(localBuilder);
            });
        }

        public override void RestoreArgsIfNeeded(ILGenerator ilGenerator) {
            byRefParamslocalBuilderMap.ForEach(keyValue => {
                var argPosition = keyValue.Key;
                var localBuilder = keyValue.Value;
                var property = aspectArgumentType.GetProperty("Arg{0}".Fmt(argPosition));

                WeaveAspectArg(ilGenerator);
                ilGenerator.EmitLoadLocal(localBuilder);
                ilGenerator.Emit(OpCodes.Callvirt, property.GetSetMethod());
            });
        }

        public override void EmitLoadLocalAddress(ILGenerator ilGenerator, int argPosition) {
            LocalBuilder localBuilder;

            if (byRefParamslocalBuilderMap.TryGetValue(argPosition, out localBuilder)) {
                ilGenerator.EmitLoadLocalAddress(localBuilder);
            }
        }

        protected abstract void WeaveAspectArg(ILGenerator ilGenerator);
    }
}

