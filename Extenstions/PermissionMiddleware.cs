using CoreArchitecture.Routing;

namespace CoreArchitecture.Extenstions;

public class PermissionMiddleware
{
    private readonly RequestDelegate _next;

    public PermissionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine("PermissionMiddleware invoked" + context.Request.Path);
        var metadata = context.GetEndpoint()?.Metadata?.GetMetadata<Public>();
        bool isPublic = metadata != null;
        if (isPublic)
        {
            Console.WriteLine("PermissionMiddleware is public", metadata.Resource, metadata.Actions);
        }

        Console.WriteLine(isPublic);
        await _next(context);
    }
}