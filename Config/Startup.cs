using System.Reflection;
using CoreArchitecture.Data;
using CoreArchitecture.Models;
using MassTransit.MultiBus;
using Microsoft.IdentityModel.Tokens;
using CoreArchitecture.Reposititories.StateMachines;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CoreArchitecture.Config;

public class Startup
{
    private readonly ILogger<Startup> _logger;
    private readonly IConfiguration _configuration;

    // Constructor-based dependency injection
    public Startup(ILogger<Startup> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    // Configure Authentication
    public void ConfigurationAuthentication(IServiceCollection services)
    {
        _logger.LogInformation("Calling Configuring authentication...");

        services.AddAuthentication()
            .AddJwtBearer("some-scheme", jwtOptions =>
            {
                jwtOptions.MetadataAddress = _configuration["Api:MetadataAddress"];
                jwtOptions.Authority = _configuration["Api:Authority"];
                jwtOptions.Audience = _configuration["Api:Audience"];
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudiences = _configuration.GetSection("Api:ValidAudiences").Get<string[]>(),
                    ValidIssuers = _configuration.GetSection("Api:ValidIssuers").Get<string[]>()
                };

                jwtOptions.MapInboundClaims = false;
            });
    }

    public void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            _logger.LogInformation("Please configurate MassTransit...");
            return;
        }
        
        services.AddDbContext<MasstransitSagaDbContext>(options =>
            options.UseNpgsql(connectionString)); // or Npgsql for PostgreSQL

        _logger.LogInformation("Configuring MassTransit...");
        services.AddMassTransit(cfg =>
        {
            cfg.UsingInMemory((context, config) =>
            {
                config.ConfigureEndpoints(context);
            });
            cfg.AddSagaStateMachine<OrderStateMachine, Order>()
                .EntityFrameworkRepository(r =>
                {
                    r.ConcurrencyMode = ConcurrencyMode.Optimistic; // or use Pessimistic, which does not require RowVersion
                    r.ExistingDbContext<ApplicationDbContext>();
                    r.AddDbContext<DbContext, MasstransitSagaDbContext>((provider,builder) =>
                    {
                        builder.UseNpgsql(connectionString, m =>
                        {
                            m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                            m.MigrationsHistoryTable($"__{nameof(MasstransitSagaDbContext)}");
                        });
                    });

                    //This line is added to enable PostgreSQL features
                    r.UsePostgres();
                });
        });
    }
}