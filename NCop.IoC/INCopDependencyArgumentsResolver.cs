﻿
namespace NCop.IoC
{
    public interface INCopDependencyArgumentsResolver : INCopDependencyResolver
    {
        TService Resolve<TArg1, TService>(TArg1 arg1, string name = null);
        TService TryResolve<TArg1, TService>(TArg1 arg1, string name = null);
        TService Resolve<TArg1, TArg2, TService>(TArg1 arg1, TArg2 arg2, string name = null);
        TService TryResolve<TArg1, TArg2, TService>(TArg1 arg1, TArg2 arg2, string name = null);
        TService Resolve<TArg1, TArg2, TArg3, TService>(TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null);
        TService TryResolve<TArg1, TArg2, TArg3, TService>(TArg1 arg1, TArg2 arg2, TArg3 arg3, string name = null);
        TService Resolve<TArg1, TArg2, TArg3, TArg4, TService>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null);
        TService TryResolve<TArg1, TArg2, TArg3, TArg4, TService>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, string name = null);
        TService Resolve<TArg1, TArg2, TArg3, TArg4, TArg5, TService>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, string name = null);
        TService TryResolve<TArg1, TArg2, TArg3, TArg4, TArg5, TService>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, string name = null);
        TService Resolve<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TService>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, string name = null);
        TService TryResolve<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TService>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, string name = null);
        TService Resolve<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TService>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, string name = null);
        TService TryResolve<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TService>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, string name = null);
        TService Resolve<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TService>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, string name = null);
        TService TryResolve<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TService>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, string name = null);
    }
}
