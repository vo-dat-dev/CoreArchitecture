using Microsoft.IdentityModel.Tokens; 

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
}