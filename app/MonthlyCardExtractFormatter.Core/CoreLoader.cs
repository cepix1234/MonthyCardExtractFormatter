using MonthlyCardExtractFormatter.Core.Logging;
using MonthlyCardExtractFormatter.Core.Logging.Models;
using Microsoft.Extensions.DependencyInjection;

namespace MonthlyCardExtractFormatter.Core;

public static class CoreLoader
{
    public static void Load(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IConsoleLogger, ConsoleLogger>();
    }
}