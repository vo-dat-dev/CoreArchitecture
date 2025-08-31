using System.Reflection;
using CoreArchitecture.Data;
using CoreArchitecture.Implement;
using CoreArchitecture.Interface;
using CoreArchitecture.Reposititories;
using Microsoft.EntityFrameworkCore;
using CoreArchitecture.Config;
using CoreArchitecture.Extenstions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IAuthentication, AuthenticationImpl>();
builder.Services.AddTransient<IUserRepository, UserRepositoryImpl>();
builder.Services.AddLogging();
builder.Services.AddSingleton<Startup>();

var startup = builder.Services.BuildServiceProvider().GetRequiredService<Startup>();
startup.ConfigurationAuthentication(builder.Services);
startup.ConfigureServices(builder.Services, builder.Configuration);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Authentication API",
        Version = "v1",
        Description = "Authentication API for managing user authentication and authorization"
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication API");
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UsePermissions();
app.Run();