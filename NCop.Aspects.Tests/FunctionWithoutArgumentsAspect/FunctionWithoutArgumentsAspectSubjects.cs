﻿using NCop.Aspects.Framework;
using NCop.Aspects.Tests.Extensions;
using NCop.Composite.Framework;
using NCop.Core.Extensions;
using NCop.Mixins.Framework;
using System;

namespace NCop.Aspects.Tests.FunctionWithoutArgumentAspect.Subjects
{
    public static class JoinPointsContainer
    {
        public static AspectOrderedJoinPoints JoinPoints = new AspectOrderedJoinPoints();
    }

    public interface IFunctionWithoutArgumentsBoundaryAspect
    {
        string InterceptionAspect();
        string OnMethodBoundaryAspect();
        string MultipleInterceptionAspects();
        string InterceptionAspectUsingInvoke();
        string MultipleOnMethodBoundaryAspects();
        string AllAspectsStartingWithInterception();
        string AllAspectsStartingWithOnMethodBoundary();
        string AlternatelAspectsStartingWithInterception();
        string OnMethodBoundaryAspectWithOnlyOnEntryAdvide();
        string AlternateAspectsStartingWithOnMethodBoundary();
        string OnMethodBoundaryAspectThatRaiseAnExceptionInMethodImpl();
        string TryfinallyOnMethodBoundaryAspectThatRaiseAnExceptionInMethodImpl();
        string OnMethodBoundaryAspectThatRaiseAnExceptionInMethodImplWithoutTryFinally();
        string OnMethodBoundaryAspectThatRaiseAnExceptionInMethodImplDecoratedWithContinueFlowBehaviourAspect();
    }

    public class Mixin : IFunctionWithoutArgumentsBoundaryAspect
    {
        private string AddInMethodJoinPoint() {
            JoinPointsContainer.JoinPoints.Add(AspectJoinPoints.InMethod);

            return AspectJoinPoints.InMethod.ToString();
        }

        public string InterceptionAspect() {
            return AddInMethodJoinPoint();
        }

        public string OnMethodBoundaryAspect() {
            return AddInMethodJoinPoint();
        }

        public string MultipleInterceptionAspects() {
            return AddInMethodJoinPoint();
        }

        public string InterceptionAspectUsingInvoke() {
            return AddInMethodJoinPoint();
        }

        public string MultipleOnMethodBoundaryAspects() {
            return AddInMethodJoinPoint();
        }

        public string AllAspectsStartingWithInterception() {
            return AddInMethodJoinPoint();
        }

        public string AllAspectsStartingWithOnMethodBoundary() {
            return AddInMethodJoinPoint();
        }

        public string AlternatelAspectsStartingWithInterception() {
            return AddInMethodJoinPoint();
        }

        public string OnMethodBoundaryAspectWithOnlyOnEntryAdvide() {
            return AddInMethodJoinPoint();
        }

        public string AlternateAspectsStartingWithOnMethodBoundary() {
            return AddInMethodJoinPoint();
        }

        public string OnMethodBoundaryAspectThatRaiseAnExceptionInMethodImpl() {
            AddInMethodJoinPoint();
            throw new Exception("InMethodException");
        }

        public string TryfinallyOnMethodBoundaryAspectThatRaiseAnExceptionInMethodImpl() {
            return OnMethodBoundaryAspectThatRaiseAnExceptionInMethodImpl();
        }

        public string OnMethodBoundaryAspectThatRaiseAnExceptionInMethodImplWithoutTryFinally() {
            return OnMethodBoundaryAspectThatRaiseAnExceptionInMethodImpl();
        }

        public string OnMethodBoundaryAspectThatRaiseAnExceptionInMethodImplDecoratedWithContinueFlowBehaviourAspect() {
            return OnMethodBoundaryAspectThatRaiseAnExceptionInMethodImpl();
        }
    }

    [TransientComposite]
    [Mixins(typeof(Mixin))]
    public interface IFunctionWithoutArgumentsComposite : IFunctionWithoutArgumentsBoundaryAspect
    {
        [MethodInterceptionAspect(typeof(FunctionWithoutArgumentsInterceptionAspect))]
        new string InterceptionAspect();

        [OnMethodBoundaryAspect(typeof(FunctionWithoutArgumentsOnMethodBoundaryAspect))]
        new string OnMethodBoundaryAspect();

        [OnMethodBoundaryAspect(typeof(FunctionWithoutArgumentsOnMethodBoundaryAspect))]
        [OnMethodBoundaryAspect(typeof(FunctionWithoutArgumentsOnMethodBoundaryAspect))]
        [OnMethodBoundaryAspect(typeof(FunctionWithoutArgumentsOnMethodBoundaryAspect))]
        new string MultipleOnMethodBoundaryAspects();

        [MethodInterceptionAspect(typeof(FunctionWithoutArgumentsInterceptionAspect))]
        [MethodInterceptionAspect(typeof(FunctionWithoutArgumentsInterceptionAspect))]
        [MethodInterceptionAspect(typeof(FunctionWithoutArgumentsInterceptionAspect))]
        new string MultipleInterceptionAspects();

        [MethodInterceptionAspect(typeof(FunctionWithoutArgumentsInterceptionUsinInvokeAspect), AspectPriority = 1)]
        [OnMethodBoundaryAspect(typeof(FunctionWithoutArgumentsOnMethodBoundaryAspect), AspectPriority = 2)]
        new string InterceptionAspectUsingInvoke();

        [MethodInterceptionAspect(typeof(FunctionWithoutArgumentsInterceptionAspect), AspectPriority = 1)]
        [OnMethodBoundaryAspect(typeof(FunctionWithoutArgumentsOnMethodBoundaryAspect), AspectPriority = 2)]
        new string AllAspectsStartingWithInterception();

        [OnMethodBoundaryAspect(typeof(FunctionWithoutArgumentsOnMethodBoundaryAspect), AspectPriority = 1)]
        [MethodInterceptionAspect(typeof(FunctionWithoutArgumentsInterceptionAspect), AspectPriority = 2)]
        new string AllAspectsStartingWithOnMethodBoundary();

        [MethodInterceptionAspect(typeof(FunctionWithoutArgumentsInterceptionAspect), AspectPriority = 1)]
        [OnMethodBoundaryAspect(typeof(FunctionWithoutArgumentsOnMethodBoundaryAspect), AspectPriority = 2)]
        [MethodInterceptionAspect(typeof(FunctionWithoutArgumentsInterceptionAspect), AspectPriority = 3)]
        [OnMethodBoundaryAspect(typeof(FunctionWithoutArgumentsOnMethodBoundaryAspect), AspectPriority = 4)]
        [MethodInterceptionAspect(typeof(FunctionWithoutArgumentsInterceptionAspect), AspectPriority = 5)]
        [OnMethodBoundaryAspect(typeof(FunctionWithoutArgumentsOnMethodBoundaryAspect), AspectPriority = 6)]
        new string AlternatelAspectsStartingWithInterception();

        [OnMethodBoundaryAspect(typeof(OnEntry_FunctionWithoutArgumentsBoundaryAspect))]
        new string OnMethodBoundaryAspectWithOnlyOnEntryAdvide();

        [OnMethodBoundaryAspect(typeof(FunctionWithoutArgumentsOnMethodBoundaryAspect), AspectPriority = 1)]
        [MethodInterceptionAspect(typeof(FunctionWithoutArgumentsInterceptionAspect), AspectPriority = 2)]
        [OnMethodBoundaryAspect(typeof(FunctionWithoutArgumentsOnMethodBoundaryAspect), AspectPriority = 3)]
        [MethodInterceptionAspect(typeof(FunctionWithoutArgumentsInterceptionAspect), AspectPriority = 4)]
        [OnMethodBoundaryAspect(typeof(FunctionWithoutArgumentsOnMethodBoundaryAspect), AspectPriority = 5)]
        [MethodInterceptionAspect(typeof(FunctionWithoutArgumentsInterceptionAspect), AspectPriority = 6)]
        new string AlternateAspectsStartingWithOnMethodBoundary();

        [OnMethodBoundaryAspect(typeof(FunctionWithoutArgumentsOnMethodBoundaryAspect))]
        new string OnMethodBoundaryAspectThatRaiseAnExceptionInMethodImpl();

        [OnMethodBoundaryAspect(typeof(OnEntry_OnSuccess_OnExit_FunctionWithoutArgumentsBoundaryAspect))]
        new string TryfinallyOnMethodBoundaryAspectThatRaiseAnExceptionInMethodImpl();

        [OnMethodBoundaryAspect(typeof(OnEntry_OnSuccess_FunctionWithoutArgumentsBoundaryAspect))]
        new string OnMethodBoundaryAspectThatRaiseAnExceptionInMethodImplWithoutTryFinally();

        [OnMethodBoundaryAspect(typeof(WithContinueFlowBehvoiurFunctionWithoutArgumentsBoundaryAspect))]
        new string OnMethodBoundaryAspectThatRaiseAnExceptionInMethodImplDecoratedWithContinueFlowBehaviourAspect();
    }

    public class FunctionWithoutArgumentsOnMethodBoundaryAspect : OnFunctionBoundaryAspect<string>
    {
        public override void OnEntry(FunctionExecutionArgs<string> args) {
            JoinPointsContainer.JoinPoints.Add(AspectJoinPoints.OnEntry);
            args.AddToReturnValue(AspectJoinPoints.OnEntry);
            base.OnEntry(args);
        }

        public override void OnSuccess(FunctionExecutionArgs<string> args) {
            JoinPointsContainer.JoinPoints.Add(AspectJoinPoints.OnSuccess);
            args.AddToReturnValue(AspectJoinPoints.OnSuccess);
            base.OnSuccess(args);
        }

        public override void OnException(FunctionExecutionArgs<string> args) {
            var ex = args.Exception;

            if (ex.IsNotNull() && ex.GetType() == typeof(Exception) && ex.Message.Equals("InMethodException")) {
                JoinPointsContainer.JoinPoints.Add(AspectJoinPoints.OnException);
                args.AddToReturnValue(AspectJoinPoints.OnException);
            }

            base.OnException(args);
        }

        public override void OnExit(FunctionExecutionArgs<string> args) {
            JoinPointsContainer.JoinPoints.Add(AspectJoinPoints.OnExit);
            args.AddToReturnValue(AspectJoinPoints.OnExit);
            base.OnExit(args);
        }
    }

    public class OnEntry_FunctionWithoutArgumentsBoundaryAspect : OnFunctionBoundaryAspect<string>
    {
        public override void OnEntry(FunctionExecutionArgs<string> args) {
            args.FlowBehavior = FlowBehavior.Continue;
            base.OnEntry(args);
        }
    }

    public class OnEntry_OnSuccess_FunctionWithoutArgumentsBoundaryAspect : OnFunctionBoundaryAspect<string>
    {
        public override void OnEntry(FunctionExecutionArgs<string> args) {
            args.FlowBehavior = FlowBehavior.Continue;
            base.OnEntry(args);
        }

        public override void OnSuccess(FunctionExecutionArgs<string> args) {
            base.OnSuccess(args);
        }
    }

    public class OnEntry_OnSuccess_OnExit_FunctionWithoutArgumentsBoundaryAspect : OnFunctionBoundaryAspect<string>
    {
        public override void OnEntry(FunctionExecutionArgs<string> args) {
            args.FlowBehavior = FlowBehavior.Continue;
            base.OnEntry(args);
        }

        public override void OnSuccess(FunctionExecutionArgs<string> args) {
            base.OnSuccess(args);
        }

        public override void OnExit(FunctionExecutionArgs<string> args) {
            base.OnExit(args);
        }
    }

    public class WithContinueFlowBehvoiurFunctionWithoutArgumentsBoundaryAspect : OnFunctionBoundaryAspect<string>
    {
        public override void OnEntry(FunctionExecutionArgs<string> args) {
            args.FlowBehavior = FlowBehavior.Continue;
            JoinPointsContainer.JoinPoints.Add(AspectJoinPoints.OnEntry);
            args.AddToReturnValue(AspectJoinPoints.OnEntry);
            base.OnEntry(args);
        }

        public override void OnSuccess(FunctionExecutionArgs<string> args) {
            JoinPointsContainer.JoinPoints.Add(AspectJoinPoints.OnSuccess);
            args.AddToReturnValue(AspectJoinPoints.OnSuccess);
            base.OnSuccess(args);
        }

        public override void OnException(FunctionExecutionArgs<string> args) {
            var ex = args.Exception;

            if (ex.IsNotNull() && ex.GetType() == typeof(Exception) && ex.Message.Equals("InMethodException")) {
                JoinPointsContainer.JoinPoints.Add(AspectJoinPoints.OnException);
                args.AddToReturnValue(AspectJoinPoints.InMethod);
                args.AddToReturnValue(AspectJoinPoints.OnException);
            }

            base.OnException(args);
        }

        public override void OnExit(FunctionExecutionArgs<string> args) {
            JoinPointsContainer.JoinPoints.Add(AspectJoinPoints.OnExit);
            args.AddToReturnValue(AspectJoinPoints.OnExit);
            base.OnExit(args);
        }
    }

    public class FunctionWithoutArgumentsInterceptionAspect : FunctionInterceptionAspect<string>
    {
        public override void OnInvoke(FunctionInterceptionArgs<string> args) {
            JoinPointsContainer.JoinPoints.Add(AspectJoinPoints.OnInvoke);
            args.AddToReturnValue(AspectJoinPoints.OnInvoke);
            base.OnInvoke(args);
        }
    }

    public class FunctionWithoutArgumentsInterceptionUsinInvokeAspect : FunctionInterceptionAspect<string>
    {
        public override void OnInvoke(FunctionInterceptionArgs<string> args) {
            args.Invoke();
        }
    }
}
