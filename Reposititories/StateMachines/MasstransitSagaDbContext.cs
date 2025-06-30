using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreArchitecture.Reposititories.StateMachines;

public class MasstransitSagaDbContext: SagaDbContext
{
    public MasstransitSagaDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get { yield return new OrderStateMap(); }
    }
    
}

public class OrderStateMap : 
    SagaClassMap<OrderState>
{
    protected override void Configure(EntityTypeBuilder<OrderState> entity, ModelBuilder model)
    {
        entity.Property(x => x.Status);
    }
}