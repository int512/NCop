﻿using NCop.Weaving;
using NCop.Weaving.Extensions;
using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace NCop.Aspects.Weaving
{
    internal abstract class AbstractAspectArgsMappingWeaver : IWeaver, IMethodScopeWeaver
    {
        protected readonly Type[] parameters = null;
        protected readonly Type aspectArgumentType = null;
        protected readonly Type[] mappingParameters = null;
        protected readonly IMethodWeavingSettings weavingSettings = null;
        protected readonly IArgumentsSettings argumentsSettings = null;
        protected readonly IAspectMethodWeavingSettings aspectWeavingSettings = null;
        protected readonly ILocalBuilderRepository localBuilderRepository = null;

        internal AbstractAspectArgsMappingWeaver(IAspectMethodWeavingSettings aspectWeavingSettings, IArgumentsSettings argumentsSettings) {
            Type[] @params = null;

            this.argumentsSettings = argumentsSettings;
            this.aspectWeavingSettings = aspectWeavingSettings;
            aspectArgumentType = argumentsSettings.ArgumentType;
            weavingSettings = aspectWeavingSettings.WeavingSettings;
            localBuilderRepository = aspectWeavingSettings.LocalBuilderRepository;
            @params = argumentsSettings.ArgumentType.GetGenericArguments();
            parameters = argumentsSettings.Parameters;
            mappingParameters = @params.Skip(1).ToArray();
        }

        public virtual ILGenerator Weave(ILGenerator ilGenerator) {
            MethodInfo mapGenericMethod = null;
            Func<int, MethodInfo> getMappingArgsMethod = null;
            var argsImplLocalBuilder = localBuilderRepository.Get(aspectArgumentType);

            if (mappingParameters.Length > 0) {
                getMappingArgsMethod = argumentsSettings.IsFunction ?
                                            aspectWeavingSettings.AspectArgsMapper.GetMappingArgsFunction :
                                            (Func<int, MethodInfo>)aspectWeavingSettings.AspectArgsMapper.GetMappingArgsAction;

                mapGenericMethod = getMappingArgsMethod(mappingParameters.Length);
                mapGenericMethod = mapGenericMethod.MakeGenericMethod(mappingParameters);

                ilGenerator.EmitLoadLocal(argsImplLocalBuilder);
                WeaveAspectArg(ilGenerator);
                ilGenerator.Emit(OpCodes.Call, mapGenericMethod);
            }

            return ilGenerator;
        }

        protected abstract void WeaveAspectArg(ILGenerator ilGenerator);
    }
}
