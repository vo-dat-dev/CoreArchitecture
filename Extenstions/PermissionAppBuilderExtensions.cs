namespace CoreArchitecture.Extenstions;

public static class PermissionAppBuilderExtensions
{
    private const string PermissionsMiddlewareSetKey = "_PermissionsMiddlewareSet";

    public static IApplicationBuilder UsePermissions(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.Properties[PermissionsMiddlewareSetKey] = true;
        
        return app.UseMiddleware<PermissionMiddleware>();
    }
}