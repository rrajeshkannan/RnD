using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace ObservableDemo
{
    public class Repository<TItem>
    {
        private class ItemAddedEventArgs<T> : EventArgs
        {
            public T Item { get; }

            public ItemAddedEventArgs(T number) => Item = number;
        }

        private event EventHandler<ItemAddedEventArgs<TItem>> ItemAdded;

        public IObservable<TItem> ItemsAdded { get; }

        public Repository()
        {
            var itemsAddedObservable = Observable.FromEventPattern<ItemAddedEventArgs<TItem>>(
                handler => ItemAdded += handler,
                handler => ItemAdded -= handler);

            ItemsAdded = itemsAddedObservable
                .Select(args => args.EventArgs.Item)
                .Publish()
                .RefCount();
        }

        private IList<TItem> _underlyingCollection = new List<TItem>();

        public void Add(TItem item)
        {
            _underlyingCollection.Add(item);
            ItemAdded?.Invoke(this, new ItemAddedEventArgs<TItem>(item));
        }
    }
}