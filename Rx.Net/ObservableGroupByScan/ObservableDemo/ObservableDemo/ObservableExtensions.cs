using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ObservableDemo
{
    public static class ObservableExtensions
    {
        public static IObservable<TSource> Chain<TSource>(
            this IObservable<TSource> source,
            List<Func<TSource, bool>> handlerList)
        {
            return source.SelectMany(x =>
            {
                var passedHandler = handlerList.FirstOrDefault(handler => handler(x) == true);
                return passedHandler == null ? Observable.Return(x) : Observable.Empty(x);
            });
        }

        //public static IObservable<TSource> DivertOn<TSource>(
        //    this IObservable<TSource> source,
        //    Func<TSource, bool> predicate, 
        //    out IObservable<TSource> diverted)
        //{
        //    var subject = new Subject<TSource>();
        //    diverted = subject;

        //    return source.SelectMany(x =>
        //    {
        //        var passed = predicate(x);

        //        if (passed)
        //        {
        //            subject.OnNext(x);
        //            return Observable.Empty(x);
        //        }

        //        return Observable.Return(x);
        //    });
        //}

        public static IObservable<TSource> Divert<TSource>(
            this IObservable<TSource> source,
            Func<TSource, bool> predicate,
            out IObservable<TSource> diverted)
        {
            var diverting = new Subject<TSource>();
            diverted = diverting.Publish().RefCount();

            return Observable.Create<TSource>(observer => source.Subscribe(
                x => (predicate(x) ? diverting : observer).OnNext(x),
                ex => { diverting.OnError(ex); observer.OnError(ex); },
                () => { diverting.OnCompleted(); observer.OnCompleted(); })
            ).Publish().RefCount();
        }

        public static IObservable<TSource> If<TSource>(
            this IObservable<TSource> source,
            Func<TSource, bool> predicate,
            Func<TSource, IObservable<TSource>> thenSource)
        {
            return source.SelectMany(value
                => predicate(value)
                ? thenSource(value)
                : Observable.Return(value));
        }

        public static IObservable<TResult> If<TSource, TResult>(
            this IObservable<TSource> source,
            Func<TSource, bool> predicate,
            Func<TSource, IObservable<TResult>> thenSource,
            Func<TSource, IObservable<TResult>> elseSource)
        {
            return source.SelectMany(value 
                => predicate(value) 
                ? thenSource(value) 
                : elseSource(value));
        }

        public static IObservable<IGroupedObservable<bool, TSource>> DetourOn<TSource>(
            this IObservable<TSource> source,
            Func<TSource, bool> predicate)
        {
            return source
                .GroupBy(predicate)
                .Publish()
                .RefCount();
        }

        public static IObservable<IGroupedObservable<TRoute, TElement>> Route<TSource, TRoute, TElement>(
            this IObservable<TSource> source, Func<TSource, TRoute> routeSelector, Func<TSource, TElement> elementSelector)
        {
            return source.GroupBy(routeSelector, elementSelector);
        }

        public static IObservable<TResult> MapConditional<TResult, TSource>(
            this IObservable<TSource> source, 
            IEnumerable<Func<TSource, (bool Handled, TResult Result)>> resultSelectors,
            Func<TSource, TResult> defaultResultSelector)
        {
            return Observable.Create<TResult>(o 
                => source.Subscribe(x =>
                {
                    foreach (var resultSelector in resultSelectors)
                    {
                        var result = resultSelector(x);

                        if (result.Handled)
                        {
                            o.OnNext(result.Result);
                            return;
                        }
                    }

                    o.OnNext(defaultResultSelector(x));
                },
                o.OnError,
                o.OnCompleted));
        }

        public static IObservable<TResult> Switch<TResult, TSource>(
            this IObservable<TSource> source,
            IEnumerable<Func<IObservable<TSource>, IObservable<TResult>>> resultSelectors)
        {
            var sequences = resultSelectors.ToObservable()
                .Select(resultSelector => resultSelector(source));

            return sequences.Switch();
        }


    }
}