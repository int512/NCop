﻿using NCop.Aspects.Advices;
using NCop.Aspects.Aspects;
using NCop.Aspects.Weaving.Expressions;
using NCop.Composite.Weaving;
using NCop.Core.Extensions;
using NCop.Weaving;
using NCop.Weaving.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace NCop.Aspects.Weaving
{
    internal abstract class AbstractOnMethodBoundaryAspectWeaver : AbstractMethodAspectWeaver
    {
        protected List<IMethodScopeWeaver> tryWeavers = null;
        protected IMethodScopeWeaver returnValueWeaver = null;
        protected readonly ILocalBuilderRepository localBuilderRepository = null;
        protected readonly IByRefArgumentsStoreWeaver byRefArgumentsStoreWeaver = null;

        internal AbstractOnMethodBoundaryAspectWeaver(IAspectWeaver nestedAspect, IAspectDefinition aspectDefinition, IAspectWeavingSettings aspectWeavingSettings)
            : base(aspectDefinition, aspectWeavingSettings) {
            IMethodScopeWeaver entryWeaver = null;
            IMethodScopeWeaver catchWeaver = null;
            Action<ILGenerator> storeArgsAction = null;
            IAdviceExpression selectedExpression = null;
            IMethodScopeWeaver storeArgsArgsWeaver = null;
            var finallyWeavers = new List<IMethodScopeWeaver>();
            var adviceWeavingSettings = new AdviceWeavingSettings(aspectWeavingSettings, argumentsWeavingSetings);

            ArgumentType = argumentsWeavingSetings.ArgumentType;
            byRefArgumentsStoreWeaver = aspectWeavingSettings.ByRefArgumentsStoreWeaver;
            storeArgsAction = byRefArgumentsStoreWeaver.StoreArgsIfNeeded;
            storeArgsArgsWeaver = storeArgsAction.ToMethodScopeWeaver();
            tryWeavers = new List<IMethodScopeWeaver>() { nestedAspect };
            localBuilderRepository = aspectWeavingSettings.LocalBuilderRepository;

            if (adviceDiscoveryVistor.HasOnMethodEntryAdvice) {
                selectedExpression = ResolveOnMethodEntryAdvice();
                entryWeaver = selectedExpression.Reduce(adviceWeavingSettings);
            }

            if (adviceDiscoveryVistor.HasOnMethodSuccessAdvice) {
                selectedExpression = ResolveOnMethodSuccessAdvice();
                tryWeavers.Add(selectedExpression.Reduce(adviceWeavingSettings));
            }

            if (argumentsWeavingSetings.IsFunction) {
                OnFunctionWeavingDetected();
            }

            if (adviceDiscoveryVistor.HasFinallyAdvice) {
                selectedExpression = ResolveFinallyAdvice();
                finallyWeavers.Add(selectedExpression.Reduce(adviceWeavingSettings));
                finallyWeavers.Add(storeArgsArgsWeaver);

                if (adviceDiscoveryVistor.HasOnMethodExceptionAdvice) {
                    var aspectMember = aspectRepository.GetAspectFieldByType(aspectDefinition.Aspect.AspectType);
                    var settings = new TryCatchFinallySettings(ArgumentType, aspectMember, localBuilderRepository);

                    selectedExpression = ResolveOnMethodExceptionAdvice();
                    catchWeaver = selectedExpression.Reduce(adviceWeavingSettings);
                    weaver = new TryCatchFinallyAspectWeaver(settings, entryWeaver, tryWeavers, catchWeaver, finallyWeavers, returnValueWeaver);
                }
                else {
                    weaver = new OnMethodBoundaryTryFinallyAspectWeaver(entryWeaver, tryWeavers, finallyWeavers, returnValueWeaver);
                }
            }
            else {
                var weavers = new List<IMethodScopeWeaver> { entryWeaver };

                if (!byRefArgumentsStoreWeaver.ContainsByRefParams) {
                    weavers.AddRange(tryWeavers);

                    if (returnValueWeaver.IsNotNull()) {
                        weavers.Add(returnValueWeaver);
                    }

                    weaver = new MethodScopeWeaversQueue(weavers);
                }
                else {
                    finallyWeavers.Add(storeArgsArgsWeaver);
                    weaver = new OnMethodBoundaryTryFinallyAspectWeaver(entryWeaver, weavers, finallyWeavers, returnValueWeaver);
                }
            }
        }

        protected virtual void OnFunctionWeavingDetected() { }

        private IAdviceExpression ResolveOnMethodEntryAdvice() {
            IAdviceDefinition selectedAdviceDefinition = null;
            Func<IAdviceDefinition, IAdviceExpression> adviceExpressionFactory = null;
            var onMethodEntryAdvice = adviceDiscoveryVistor.OnMethodEntryAdvice;

            adviceExpressionFactory = adviceVisitor.Visit(adviceDiscoveryVistor.OnMethodEntryAdvice);
            selectedAdviceDefinition = advices.First(advice => advice.Advice.Equals(onMethodEntryAdvice));

            return adviceExpressionFactory(selectedAdviceDefinition);
        }

        private IAdviceExpression ResolveOnMethodSuccessAdvice() {
            IAdviceDefinition selectedAdviceDefinition = null;
            Func<IAdviceDefinition, IAdviceExpression> adviceExpressionFactory = null;
            var onMethodSuccessAdvice = adviceDiscoveryVistor.OnMethodSuccessAdvice;

            adviceExpressionFactory = adviceVisitor.Visit(adviceDiscoveryVistor.OnMethodSuccessAdvice);
            selectedAdviceDefinition = advices.First(advice => advice.Advice.Equals(onMethodSuccessAdvice));

            return adviceExpressionFactory(selectedAdviceDefinition);
        }

        private IAdviceExpression ResolveOnMethodExceptionAdvice() {
            IAdviceDefinition selectedAdviceDefinition = null;
            Func<IAdviceDefinition, IAdviceExpression> adviceExpressionFactory = null;
            var onMethodExceptionAdvice = adviceDiscoveryVistor.OnMethodExceptionAdvice;

            adviceExpressionFactory = adviceVisitor.Visit(adviceDiscoveryVistor.OnMethodExceptionAdvice);
            selectedAdviceDefinition = advices.First(advice => advice.Advice.Equals(onMethodExceptionAdvice));

            return adviceExpressionFactory(selectedAdviceDefinition);
        }

        private IAdviceExpression ResolveFinallyAdvice() {
            IAdviceDefinition selectedAdviceDefinition = null;
            Func<IAdviceDefinition, IAdviceExpression> adviceExpressionFactory = null;
            var finallyAdvice = adviceDiscoveryVistor.FinallyAdvice;

            adviceExpressionFactory = adviceVisitor.Visit(adviceDiscoveryVistor.FinallyAdvice);
            selectedAdviceDefinition = advices.First(advice => advice.Advice.Equals(finallyAdvice));

            return adviceExpressionFactory(selectedAdviceDefinition);
        }

    }
}
