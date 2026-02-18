using MonthlyCardExtractFormatter.Core.Logging.Models;
using Spectre.Console.Cli;
using MonthlyCardExtractFormatter.Infrastructure.Commands.FormatCommand.Settings;
using MonthlyCardExtractFormatter.Core.Formatting;
using MonthlyCardExtractFormatter.Core.Formatting.Models;

namespace MonthlyCardExtractFormatter.Infrastructure.Commands.FormatCommand;

public class FormatCommand : Command<FormatCommandSettings>
{
    private readonly IConsoleLogger _consoleLogger;
    private readonly IFormatter _formatter;

    public FormatCommand(IConsoleLogger consoleLogger)
    {
        _consoleLogger = consoleLogger;
        _formatter = new Formatter();
    }

    public override int Execute(CommandContext context, FormatCommandSettings settings)
    {
        string[] files = Directory.GetFiles(settings.Dir);
        foreach (string file in files)
        {
            this.FormatFile(file);
        }
        return 0;
    }

    private void FormatFile(string filePath)
    {
        string[] fileLines = File.ReadAllLines(filePath);
        string[] formatedFileLines = this.FormatLines(fileLines);
        File.WriteAllLines(filePath, formatedFileLines);
    }

    private string[] FormatLines(string[] fileLies)
    {
        string[] result = [];
        foreach (string line in fileLies)
        {
            string? formattedLine = this._formatter.Format(line);
            if (formattedLine != null)
            {
                result = [.. result, formattedLine];
            }
        }
        return result;

    }
}
