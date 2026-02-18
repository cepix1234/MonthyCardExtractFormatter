using Microsoft.Extensions.DependencyInjection;

namespace MonthlyCardExtractFormatter.Infrastructure;

public static class InfrastructureLoader
{
    public static void Load(IServiceCollection serviceCollection)
    {
        // When Infrastructure creates classes that need to be registered as singletons is should be done here.
    }
}