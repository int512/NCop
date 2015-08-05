﻿using System.Linq;
using NCop.Aspects.Aspects;
using NCop.Aspects.Extensions;
using NCop.Weaving.Extensions;
using System.Reflection.Emit;
using NCop.Core.Extensions;

namespace NCop.Aspects.Weaving
{
    internal class TopRaiseEventInterceptionArgumentsWeaver : AbstractEventAspectArgumentsWeaver
    {
        internal TopRaiseEventInterceptionArgumentsWeaver(IEventAspectDefinition aspectDefinition, IArgumentsWeavingSettings argumentWeavingSettings, IAspectWeavingSettings aspectWeavingSettings, BindingSettings bindingSettings)
            : base(aspectDefinition, argumentWeavingSettings, aspectWeavingSettings, bindingSettings) {
        }

        public override LocalBuilder BuildArguments(ILGenerator ilGenerator) {
            var aspectArgLocalBuilder = ilGenerator.DeclareLocal(ArgumentType);
            var contractFieldBuilder = WeavingSettings.TypeDefinition.GetFieldBuilder(WeavingSettings.ContractType);
            var eventArgumentContract = Member.ToEventArgumentContract();
            var eventBrokerProperty = eventArgumentContract.GetProperty("EventBroker");
            var eventBrokerType = eventBrokerProperty.PropertyType;
            var handlerType = eventBrokerType.GetGenericArguments().First();
            var getEventMethod = eventArgumentContract.GetProperty("Event").GetGetMethod();
            var getHandlerMethod = eventArgumentContract.GetProperty("Handler").GetGetMethod();
            var ctorInterceptionArgs = ArgumentType.GetConstructors().Single(ctor => ctor.GetParameters().Length != 0);
            var nullableArgs = handlerType.GetInvokeMethod().GetParameters();

            ilGenerator.EmitLoadArg(0);
            ilGenerator.Emit(OpCodes.Ldfld, contractFieldBuilder);
            ilGenerator.EmitLoadArg(1);
            ilGenerator.Emit(OpCodes.Callvirt, getEventMethod);
            ilGenerator.EmitLoadArg(1);
            ilGenerator.Emit(OpCodes.Callvirt, getHandlerMethod);
            ilGenerator.Emit(OpCodes.Ldsfld, BindingsDependency);
            ilGenerator.EmitLoadArg(1);
            ilGenerator.Emit(OpCodes.Callvirt, eventBrokerProperty.GetGetMethod());
            nullableArgs.ForEach(arg => ilGenerator.Emit(OpCodes.Ldnull));
            ilGenerator.Emit(OpCodes.Newobj, ctorInterceptionArgs);
            ilGenerator.EmitStoreLocal(aspectArgLocalBuilder);

            return aspectArgLocalBuilder;
        }
    }
}