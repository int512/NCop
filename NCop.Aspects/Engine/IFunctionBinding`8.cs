﻿
namespace NCop.Aspects.Engine
{
	public interface IFunctionBinding<TInstance, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>
	{
		TResult Invoke(ref TInstance instance, IFunctionArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult> args);
		TResult Proceed(ref TInstance instance, IFunctionArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult> args);
	}
}
