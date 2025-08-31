namespace CoreArchitecture.Routing;
public class Public : Attribute
{
    private readonly string _resource;
    private readonly string _actions;

    public Public(string resource, string actions)
    {
        _resource = resource;
        _actions = actions;
    }

    // Getter methods to access the private fields
    public string Resource => _resource;
    public string Actions => _actions;
}