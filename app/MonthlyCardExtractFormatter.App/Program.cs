using MonthlyCardExtractFormatter.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Spectre.Console.Cli;
using Spectre.Console.Cli.Extensions.DependencyInjection;
using MonthlyCardExtractFormatter.Core.Models.Application;
using MonthlyCardExtractFormatter.Infrastructure;
using MonthlyCardExtractFormatter.Infrastructure.Commands.FormatCommand;

var serviceCollection = new ServiceCollection()
    .AddLogging(configure =>
        configure
            .AddSimpleConsole(opts => { opts.TimestampFormat = "yyyy-MM-dd HH:mm:ss "; })
    );

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();

serviceCollection.Configure<AppSettings>(configuration.GetSection("Settings"));
RegisterServices(serviceCollection);

using var registrar = new DependencyInjectionRegistrar(serviceCollection);
var app = new CommandApp(registrar);
app.SetDefaultCommand<FormatCommand>();
app.Configure(
    config =>
    {
        config.AddCommand<FormatCommand>("format")
        .WithAlias("f")
        .WithDescription("Format given files of card extract to excel import friendly format.")
        .WithExample("format", "<Dir>");
    });

return await app.RunAsync(args);

void RegisterServices(IServiceCollection services)
{
    CoreLoader.Load(services);
    InfrastructureLoader.Load(services);
}
