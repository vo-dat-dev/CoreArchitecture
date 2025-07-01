using Stateless;
using System;

#pragma warning disable CA1050
public enum OrderState
{
    Submitted,
    Paid,
    Shipped
}

public enum OrderTrigger
{
    Pay,
    Ship
}

#pragma warning restore CA1050

namespace CoreArchitecture.State
{
    public class Order
    {
        private readonly StateMachine<OrderState, OrderTrigger> _machine;

        public Order()
        {
            _machine = new StateMachine<OrderState, OrderTrigger>(OrderState.Submitted);

            _machine.Configure(OrderState.Submitted)
                .Permit(OrderTrigger.Pay, OrderState.Paid);

            _machine.Configure(OrderState.Paid)
                .Permit(OrderTrigger.Ship, OrderState.Shipped);
        }

        public void Process()
        {
            Console.WriteLine("Current State: " + _machine.State);
            _machine.Fire(OrderTrigger.Pay);
            Console.WriteLine("After Pay: " + _machine.State);
            _machine.Fire(OrderTrigger.Ship);
            Console.WriteLine("After Ship: " + _machine.State);
        }
    }
};