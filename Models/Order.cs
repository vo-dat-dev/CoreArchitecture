using MassTransit;

namespace CoreArchitecture.Models;

public class Order: SagaStateMachineInstance
{
   public int Id { get; set; } 
   public int OrderName { get; set; }
   public DateTime OrderDate { get; set; }
   public Guid CorrelationId { get; set; }
   public required string CurrentState { get; set; }
}