// Partial Copyright (c) LogoUI Software Solutions LTD
// Autor: Vladislav Spivak
// This source file is the part of LogoFX Framework http://logofx.codeplex.com
// See accompanying licences and credits.

#if IRESULT_RECIEPE
using System;
using Caliburn.Micro;
using LogoFX.Misc;
using ICalResult = Caliburn.Micro.IResult;

namespace Caliburn.Micro
{
    /// <summary>
    /// Static service for creating <see cref="IResult"/> from <see cref="ResultCallback{T}"/>
    /// </summary>
    public static class Result
    {
        public static ICalResult FromResultCallbackPattern<T1, T2, TCallback>(T1 t1, T2 t2, Action<T1, T2, ResultCallback<TCallback>> action, ResultCallback<TCallback> callback)
        {
            return new ResultCallback2<T1, T2, TCallback>(t1, t2, action, callback);
        }
        public static ICalResult FromResultCallbackPattern<T1, T2, T3, TCallback>(T1 t1, T2 t2, T3 t3, Action<T1, T2, T3, ResultCallback<TCallback>> action, ResultCallback<TCallback> callback)
        {
            return new ResultCallback3<T1, T2, T3, TCallback>(t1, t2, t3, action, callback);
        }
        public static ICalResult FromResultCallbackPattern<TCallback>(Action<ResultCallback<TCallback>> action, ResultCallback<TCallback> callback)
        {
            return new ResultCallback0<TCallback>(action, callback);
        }
        public static ICalResult FromResultCallbackPattern<T1,TCallback>(T1 t,Action<T1,ResultCallback<TCallback>> action, ResultCallback<TCallback> callback)
        {
            return new ResultCallback1<T1,TCallback>(t, action, callback);
        }
        /// <summary>
        /// Base <see cref="IResult"/> implementation 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        abstract class ResultCallbackResult<T> : ICalResult
        {
            private readonly ResultCallback<T> _callback;

            public ResultCallbackResult(ResultCallback<T> callback)
            {
                _callback = callback;
            }

            public abstract void ExecuteOverride(ResultCallback<T> callback);
            public void Execute(ActionExecutionContext context)
            {
                ExecuteOverride((a) =>
                {
                    _callback(a);
                    InvokeCompleted(new ResultCompletionEventArgs() { Error = a.Error != null ? a.Error.Exception : null, WasCancelled = false });
                });
            }

            public event EventHandler<ResultCompletionEventArgs> Completed;

            public void InvokeCompleted(ResultCompletionEventArgs e)
            {
                EventHandler<ResultCompletionEventArgs> handler = Completed;
                if (handler != null) handler(this, e);
            }
        }

        class ResultCallback1<T1, T3> : ResultCallbackResult<T3>
        {
            private readonly T1 _t1;
            private readonly Action<T1, ResultCallback<T3>> _action;

            public ResultCallback1(T1 t1, Action<T1, ResultCallback<T3>> action, ResultCallback<T3> callback)
                : base(callback)
            {
                _t1 = t1;
                _action = action;
            }

            public override void ExecuteOverride(ResultCallback<T3> callback)
            {
                _action(_t1, callback);
            }
        }
        class ResultCallback0<T3> : ResultCallbackResult<T3>
        {
            private readonly Action<ResultCallback<T3>> _action;

            public ResultCallback0(Action<ResultCallback<T3>> action, ResultCallback<T3> callback)
                : base(callback)
            {
                _action = action;
            }

            public override void ExecuteOverride(ResultCallback<T3> callback)
            {
                _action(callback);
            }
        }
        class ResultCallback3<T1, T2, T3, TResult> : ResultCallbackResult<TResult>
        {
            private readonly T1 _t1;
            private readonly T2 _t2;
            private readonly T3 _t3;
            private readonly Action<T1, T2, T3, ResultCallback<TResult>> _action;

            public ResultCallback3(T1 t1, T2 t2, T3 t3, Action<T1, T2, T3, ResultCallback<TResult>> action, ResultCallback<TResult> callback)
                : base(callback)
            {
                _t1 = t1;
                _t2 = t2;
                _t3 = t3;
                _action = action;
            }

            public override void ExecuteOverride(ResultCallback<TResult> callback)
            {
                _action(_t1, _t2, _t3, callback);
            }
        }
        class ResultCallback2<T1, T2, T3> : ResultCallbackResult<T3>
        {
            private readonly T1 _t1;
            private readonly T2 _t2;
            private readonly Action<T1, T2, ResultCallback<T3>> _action;

            public ResultCallback2(T1 t1, T2 t2, Action<T1, T2, ResultCallback<T3>> action, ResultCallback<T3> callback)
                : base(callback)
            {
                _t1 = t1;
                _t2 = t2;
                _action = action;
            }

            public override void ExecuteOverride(ResultCallback<T3> callback)
            {
                _action(_t1, _t2, callback);
            }
        }
    }


}

#endif
