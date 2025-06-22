using System.Reflection;
using AuthenticationApi.Data;
using AuthenticationApi.Implement;
using AuthenticationApi.Interface;
using AuthenticationApi.Reposititories;
using Microsoft.EntityFrameworkCore;
using AuthenticationApi.Config;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IAuthentication, AuthenticationImpl>();
builder.Services.AddTransient<IUserRepository, UserRepositoryImpl>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Authentication API",
        Version = "v1",
        Description = "Authentication API for managing user authentication and authorization",
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication API"); });


// Configure the HTTP request pipeline.
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

app.ConfigureMiddleware();
app.Run();