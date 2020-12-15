using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace RuleEnginePlatform
{
    public class MyInference : IInference
    {

    }

    public class Rule : IRule
    {
        public virtual string Name => GetType().FullName;

        public Func<>

        public void Compile(Context context)
        {
            var matchedSequence = context.InferencesRepository.GetInferenceSequence<MyInference>()
                .Select(inference => )
        }

        public virtual ILeftHandSideExpression When()
        {
            var observable = new Subject<IInference>();

            var sequence = Observable.Create<IInference>(observer =>
            {
                return Disposable.Empty;
            });

            return null;
        }

        public virtual IRightHandSideExpression Then()
        {
            throw new NotImplementedException();
        }
    }
}