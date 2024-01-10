namespace HotChocolateSingletonBug;

[ExtendObjectType(OperationTypeNames.Query)]
public class DemoQuery
{
    private readonly IDictionary<string, Guid> _demoCache;

    public DemoQuery(IDictionary<string, Guid> demoCache)
    {
        _demoCache = demoCache;
    }

    //[Authorize]
    public Task<IEnumerable<Guid>> TestAsync(
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_demoCache.Values.AsEnumerable());
    }
}