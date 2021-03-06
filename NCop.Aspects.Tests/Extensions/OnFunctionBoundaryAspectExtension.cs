﻿using NCop.Aspects.Framework;
using NCop.Core.Extensions;
using System.Collections.Generic;

namespace NCop.Aspects.Tests.Extensions
{   
    public static class OnFunctionBoundaryAspectExtension
    {
        private static readonly string format = "{0}:{1}";

        internal static string Stringify(this IEnumerable<AspectJoinPoints> source) {
            return string.Join(":", source);
        }

        internal static void AddToReturnValue(this FunctionExecutionArgs<string> args, AspectJoinPoints joinPoint) {
            if (args.ReturnValue.IsNullOrEmpty()) {
                args.ReturnValue = joinPoint.ToString();
                return;
            }

            args.ReturnValue = format.Fmt(args.ReturnValue, joinPoint.ToString());
        }

        internal static void AddToReturnValue(this FunctionInterceptionArgs<string> args, AspectJoinPoints joinPoint) {
            if (args.ReturnValue.IsNullOrEmpty()) {
                args.ReturnValue = joinPoint.ToString();
                return;
            }

            args.ReturnValue = format.Fmt(args.ReturnValue, joinPoint.ToString());
        }
    }
}
