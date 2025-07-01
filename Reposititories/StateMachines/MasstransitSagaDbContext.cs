using CoreArchitecture.Models;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreArchitecture.Reposititories.StateMachines;

public class MasstransitSagaDbContext(DbContextOptions options) : SagaDbContext(options)
{
    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get { yield return new OrderStateMap(); }
    }
    
    // public DbSet<Order> Orders { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Order>().ToTable("Orders");
        modelBuilder.Entity<Order>().Property(o => o.CorrelationId).IsRequired();
        modelBuilder.Entity<Order>().Property(o => o.CurrentState).IsRequired();
    }
}

public class OrderStateMap : SagaClassMap<Order>
{
    protected override void Configure(EntityTypeBuilder<Order> entity, ModelBuilder model)
    {
        entity.Property(x => x.CorrelationId).IsRequired(); 
        entity.Property(x => x.OrderName).IsRequired(); 
        entity.Property(x => x.CurrentState);
    }
}