using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ObservableDemo
{
    public static class ChainedHandlersCheck
    {
        public static void Run()
        {
            //ChainedObservable();
            //DetouredObservable();
            //IfObservable1();
            //IfObservable2();
            //IfObservable3();
            //SwitchObservable();
            MapConditionalObservable();
        }

        private static void MapConditionalObservable()
        {
            var numberSequence = Observable.Range(1, 10);

            (bool Handled, int Result) evenFunc(int x) => (x % 2 == 0) ? (true, x * 10) : (false, x);
            (bool Handled, int Result) threeMultipleFunc(int x) => (x % 3 == 0) ? (true, x * 100) : (false, x);

            var mapped = numberSequence.MapConditional(
                new Func<int, (bool Handled, int Result)>[]
                {
                    evenFunc,
                    threeMultipleFunc
                },
                x => x);

            mapped.Subscribe(Console.WriteLine);
        }

        private static void SwitchObservable()
        {
            var numberSequence = Observable.Range(1, 10);

            IObservable<int> evenFunc(IObservable<int> source) => Observable.Create<int>(observer
                => source.Subscribe(x =>
                {
                    if (x % 2 == 0)
                        observer.OnNext(x * 10);
                },
                observer.OnError,
                observer.OnCompleted));

            IObservable<int> oddFunc(IObservable<int> source) => Observable.Create<int>(observer
                => source.Subscribe(x =>
                {
                    if (x % 3 == 0) 
                        observer.OnNext(x * 100);
                },
                observer.OnError,
                observer.OnCompleted));

            var switched = numberSequence.Switch(
                new Func<IObservable<int>, IObservable<int>>[] { evenFunc, oddFunc });

            var disposable = switched.Subscribe(Console.WriteLine);
        }

        private static void IfObservable1()
        {
            var numberSequence = Observable.Range(1, 10);

            var ifNumberSequence = numberSequence.If(
                number => number % 2 == 0,
                number => Observable.Return(number * 100));

            var disposable = ifNumberSequence
                .Subscribe(number => Console.WriteLine($"Number: {number}"));
        }

        private static void IfObservable2()
        {
            var numberSequence = Observable.Range(1, 10);

            var evenNumberSequence = new Subject<int>();

            var disposable1 = evenNumberSequence.Subscribe(number =>
            {
                Console.WriteLine($"Even-number: {number}");
            });

            var ifNumberSequence = numberSequence.If(
                number => number % 2 == 0,
                number => evenNumberSequence);

            var disposable2 = ifNumberSequence
                .Subscribe(number => Console.WriteLine($"Number: {number}"));

            disposable1.Dispose();
            disposable2.Dispose();
        }

        private static void IfObservable3()
        {
            var numberSequence = Observable.Range(1, 10);

            var ifNumberSequence = numberSequence.If(
                number => number % 2 == 0,
                number => Observable.Return(number * 100),
                number => Observable.Return(number));

            var disposable = ifNumberSequence
                .Subscribe(number => Console.WriteLine($"Number: {number}"));
        }

        private static void DetouredObservable()
        {
            //var numberSequence = Observable.Range(0, 10);

            //var detouredSequence = numberSequence
            //    .DetourOn(number => number % 2 == 0)
            //    .Where(detouredGroup => detouredGroup.Key)
            //    .Select(detouredGroup => );
        }

        private static void ChainedObservable()
        {
            var numberSequence = Observable.Range(0, 10);

            var chainedNumberSequence = numberSequence.Chain(new List<Func<int, bool>>
            {
                number =>
                {
                    var divisibleByTwo = number % 2 == 0;
                    Console.WriteLine ($"Number: {number}, Divisible by 2: <Handled: {divisibleByTwo}>");
                    return divisibleByTwo;
                },
                number =>
                {
                    var divisibleByThree = number % 3 == 0;
                    Console.WriteLine ($"Number: {number}, Divisible by 3: <Handled: {divisibleByThree}>");
                    return divisibleByThree;
                }
            });

            var disposable = chainedNumberSequence
                .Subscribe(number => Console.WriteLine($"Number: {number}, Divisible neither by 2 nor by 3"));
        }
    }
}