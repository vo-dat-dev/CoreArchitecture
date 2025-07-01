using CoreArchitecture.Models;
using MassTransit;

namespace CoreArchitecture.Reposititories.StateMachines;

public record OrderPlaced(Guid OrderId, string CustomerNumber);

public record OrderShipped(Guid OrderId, string CustomerNumber);

public class OrderStateMachine : MassTransitStateMachine<Order>
{
    public MassTransit.State Pending { get; private set; }
    public MassTransit.State Processing { get; private set; }
    public MassTransit.State Completed { get; private set; }

    private Event<OrderPlaced> OrderPlaced { get; set; } = null!;
    public Event<OrderShipped> OrderShipped { get; private set; } = null!;

    public OrderStateMachine()
    {
        // 👇 Initialize events here
        Event(() => OrderPlaced, x => x.CorrelateById(context => context.Message.OrderId));
        Event(() => OrderShipped, x => x.CorrelateById(context => context.Message.OrderId));

        Initially(
            When(OrderPlaced)
                .Then(context => context.Saga.CurrentState = "Pending")
                .TransitionTo(Pending)
        );

        During(Pending,
            When(OrderShipped)
                .Then(context => context.Saga.CurrentState = "Processing")
                .TransitionTo(Processing));

        During(Processing,
            When(OrderShipped)
                .Then(context => context.Saga.CurrentState = "Completed")
                .TransitionTo(Completed));
    }
}