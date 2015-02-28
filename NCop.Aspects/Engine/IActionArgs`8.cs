﻿using System.Reflection;

namespace NCop.Aspects.Engine
{
    public interface IActionArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>
    {
        TArg1 Arg1 { get; set; }
        TArg2 Arg2 { get; set; }
        TArg3 Arg3 { get; set; }
        TArg4 Arg4 { get; set; }
        TArg5 Arg5 { get; set; }
        TArg6 Arg6 { get; set; }
        TArg7 Arg7 { get; set; }
        TArg8 Arg8 { get; set; }
        MethodInfo Method { get; set; }
    }
}
