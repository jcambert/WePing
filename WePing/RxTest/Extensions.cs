﻿using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace RxTest
{
    public static class Extensions
    {
        public static IDisposable SubscribeAsync<T>(this IObservable<T> source,
                         Func<Task> asyncAction, Action<Exception> handler = null)
        {
            Func<T, Task<Unit>> wrapped = async t =>
            {
                await asyncAction();
                return Unit.Default;
            };
            if (handler == null)
                return source.SelectMany(wrapped).Subscribe(_ => { });
            else
                return source.SelectMany(wrapped).Subscribe(_ => { }, handler);
        }

        public static IDisposable SubscribeAsync<T>(this IObservable<T> source,
                                 Func<T, Task> asyncAction, Action<Exception> handler = null)
        {
            Func<T, Task<Unit>> wrapped = async t =>
            {
                await asyncAction(t);
                return Unit.Default;
            };
            if (handler == null)
                return source.SelectMany(wrapped).Subscribe(_ => { });
            else
                return source.SelectMany(wrapped).Subscribe(_ => { }, handler);
        }
    }
}
