using System;
using System.Reactive.Linq;

namespace RuleEnginePlatform
{
    partial class Repository<TFact>
    {
        private class FactAddedEventArgs : EventArgs
        {
            public TFact Fact { get; }

            public FactAddedEventArgs(TFact fact) => Fact = fact;
        }

        private class FactUpdatedEventArgs : EventArgs
        {
            public TFact Fact { get; }

            public string Property { get; }

            public FactUpdatedEventArgs(TFact fact, string property) => (Fact, Property) = (fact, property);
        }

        private event EventHandler<FactAddedEventArgs> FactAdded;

        private event EventHandler<FactUpdatedEventArgs> FactUpdated;

        public IObservable<TFact> FactsAdded { get; }

        public IObservable<(TFact Fact, string Property)> FactsUpdated { get; }

        public Repository()
        {
            var factAddedObservable =
                Observable.FromEvent<EventHandler<FactAddedEventArgs>, FactAddedEventArgs>(
                    handler => (sender, args) => handler(args),
                    handler => FactAdded += handler,
                    handler => FactAdded -= handler);

            FactsAdded = factAddedObservable
                .Select(args => args.Fact)
                .Publish()
                .RefCount();

            var factUpdatedObservable =
                Observable.FromEvent<EventHandler<FactUpdatedEventArgs>, FactUpdatedEventArgs>(
                    handler => (sender, args) => handler(args),
                    handler => FactUpdated += handler,
                    handler => FactUpdated -= handler);

            FactsUpdated = factUpdatedObservable
                .Select(args => (args.Fact, args.Property))
                .Publish()
                .RefCount();
        }
    }
}