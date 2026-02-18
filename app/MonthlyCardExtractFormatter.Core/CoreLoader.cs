using MonthlyCardExtractFormatter.Core.Logging;
using MonthlyCardExtractFormatter.Core.Logging.Models;
using Microsoft.Extensions.DependencyInjection;
using MonthlyCardExtractFormatter.Core.Formatting.Models;
using MonthlyCardExtractFormatter.Core.Formatting;

namespace MonthlyCardExtractFormatter.Core;

public static class CoreLoader
{
    public static void Load(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IConsoleLogger, ConsoleLogger>();
        serviceCollection.AddSingleton<IFormatter, Formatter>();
    }
}
