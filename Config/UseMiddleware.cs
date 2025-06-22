namespace CoreArchitecture.Config;

public static class UseMiddleware
{
    public static IApplicationBuilder ConfigureMiddleware(this IApplicationBuilder app)
    {
        Console.WriteLine("Configuring middleware...");
        app.Use(async (context, next) =>
        {
            // Do work that can write to the Response.
            await next.Invoke();
            // Do logging or other work that doesn't write to the Response.
        });

        // app.Run(async context => { await context.Response.WriteAsync("Hello from 2nd delegate."); });


        return app;
    }
}