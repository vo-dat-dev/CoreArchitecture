using MassTransit;

namespace CoreArchitecture.Reposititories.StateMachines;

public class OrderState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string Status { get; set; }
}

public record OrderPlaced(Guid OrderId, string CustomerNumber);

public record OrderShipped(Guid OrderId, string CustomerNumber);

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    private MassTransit.State Pending { get; set; } = null!;
    private MassTransit.State Processing { get; set; } = null!;
    private MassTransit.State Completed { get; set; } = null!;

    private Event<OrderPlaced> OrderPlaced { get; set; } = null!;
    public Event<OrderShipped> OrderShipped { get; private set; } = null!;

    public OrderStateMachine()
    {
        // 👇 Initialize events here
        Event(() => OrderPlaced, x => x.CorrelateById(context => context.Message.OrderId));
        Event(() => OrderShipped, x => x.CorrelateById(context => context.Message.OrderId));

        Initially(
            When(OrderPlaced)
                .Then(context => context.Saga.Status = "Pending")
                .TransitionTo(Pending)
        );

        During(Pending,
            When(OrderShipped)
                .Then(context => context.Saga.Status = "Processing")
                .TransitionTo(Processing));

        During(Processing,
            When(OrderShipped)
                .Then(context => context.Saga.Status = "Completed")
                .TransitionTo(Completed));
    }
}