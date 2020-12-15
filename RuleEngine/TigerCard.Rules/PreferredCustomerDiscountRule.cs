using RuleEnginePlatform;
using TigerCard.DomainModels;
using System.Reactive.Linq;
using System;
using System.Linq;
using System.Reactive.Disposables;

namespace TigerCard.Rules
{
    public class PreferredCustomerDiscountRule : Rule, IDisposable
    {
        public override string Name => nameof(PreferredCustomerDiscountRule);

        private IDisposable _disposable = Disposable.Empty;

        public override void Define(IRuleDefinitionContext ruleContext)
        {
            //var newOrders = ruleContext.OnNew<Order>();
            //var updatedOrders = ruleContext.OnChange<Order>().Select(orderFact => (orderFact.Context, orderFact.Fact));

            _disposable = ruleContext.OnNew<Order>()
                .Where(orderFact => orderFact.Fact.IsOpen)
                .Where(orderFact => !orderFact.Fact.IsDiscounted)
                .Subscribe(orderFact => 
                {
                    var context = orderFact.Context;
                    var order = orderFact.Fact;

                    var customer = context.Get<Customer>(order.Customer.Id);

                    if (customer.IsPreferred)
                    {
                        order.ApplyDiscount(10.0);
                        context.Update(order, nameof(Order.PercentDiscount));
                    }
                });
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}