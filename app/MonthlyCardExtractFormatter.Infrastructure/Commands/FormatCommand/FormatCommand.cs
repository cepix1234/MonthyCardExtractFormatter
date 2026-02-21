using MonthlyCardExtractFormatter.Core.Logging.Models;
using Spectre.Console.Cli;
using MonthlyCardExtractFormatter.Infrastructure.Commands.FormatCommand.Settings;
using MonthlyCardExtractFormatter.Core.Formatting.Models;

namespace MonthlyCardExtractFormatter.Infrastructure.Commands.FormatCommand;

public class FormatCommand : Command<FormatCommandSettings>
{
    private readonly IConsoleLogger _consoleLogger;
    private readonly IFormatter _formatter;

    public FormatCommand(IConsoleLogger consoleLogger, IFormatter formatter)
    {
        _consoleLogger = consoleLogger;
        _formatter = formatter;
    }

    public override int Execute(CommandContext context, FormatCommandSettings settings)
    {
        this._consoleLogger.Log(string.Format("Getting files from directory: {0}", settings.Dir));
        string[] files = Directory.GetFiles(settings.Dir);
        foreach (string file in files)
        {
            if (new FileInfo(file).Extension == ".md" || new FileInfo(file).Extension == ".txt")
            {
                this.FormatFile(file);
            }
            else
            {
                this._consoleLogger.Log(string.Format("File extension is not correct for file {0}", file));
            }
        }
        return 0;
    }

    private void FormatFile(string filePath)
    {
        this._consoleLogger.Log(string.Format("Formatting file content {0}", filePath));
        string[] fileLines = File.ReadAllLines(filePath);
        string[] formatedFileLines = this.FormatLines(fileLines);
        this._consoleLogger.Log(string.Format("Saving formatted content to {0}", filePath));
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
