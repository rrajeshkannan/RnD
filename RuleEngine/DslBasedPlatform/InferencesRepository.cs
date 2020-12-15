using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace RuleEnginePlatform
{
    public class InferencesRepository
    {
        private Dictionary<Type, IObservable<IInference>> _repository = new Dictionary<Type, IObservable<IInference>>();

        public IObservable<IInference> GetInferenceSequence<T>() where T : IInference
        {
            if (_repository.TryGetValue(typeof(T), out var sequence))
            {
                return sequence;
            }

            //sequence = Observable.Create<IInference>(observer =>
            //{
            //    return Disposable.Empty;
            //});

            sequence = new Subject<T>() as IObservable<IInference>;

            _repository.Add(typeof(T), sequence);

            return sequence;
        }
    }
}