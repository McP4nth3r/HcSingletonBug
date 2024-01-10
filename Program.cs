using HotChocolateSingletonBug;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    private static readonly GraphQlService GraphQlService = new();

    public static async Task Main(string[] args)
    {
        await InitServicesAsync();
        StartGraphQlService();
        Console.ReadKey();
        await Task.Run(() =>
        {
            var demoCache = GraphQlService.GetServiceProvider().GetRequiredService<IDictionary<string, Guid>>();
            Console.WriteLine(demoCache.Count);
            Console.WriteLine(demoCache["test1"]);
            Console.WriteLine(demoCache["test2"]);
            Console.WriteLine(demoCache["test3"]);
            Console.WriteLine(demoCache["test4"]);
        });
        Console.ReadKey();
    }

    private static async Task InitServicesAsync(CancellationToken cancellationToken = default)
    {
        await GraphQlService.InitServicesAsync(cancellationToken);
        await Task.Delay(TimeSpan.FromMilliseconds(10), cancellationToken);
        var demoCache = GraphQlService.GetServiceProvider().GetRequiredService<IDictionary<string, Guid>>();
        demoCache.Add("test1", new Guid("2c016eae-1dcb-4604-ad9e-4ae59386d471"));
        demoCache.Add("test2", new Guid("2c016eae-1dcb-4604-ad9e-4ae59386d472"));
        demoCache.Add("test3", new Guid("2c016eae-1dcb-4604-ad9e-4ae59386d473"));
        demoCache.Add("test4", new Guid("2c016eae-1dcb-4604-ad9e-4ae59386d474"));
    }

    private static void StartGraphQlService()
    {
        Task.Run(() => GraphQlService.StartAsync());
    }
}