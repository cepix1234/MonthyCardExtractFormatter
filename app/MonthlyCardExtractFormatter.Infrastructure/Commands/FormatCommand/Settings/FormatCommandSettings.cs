using System.ComponentModel;
using MonthlyCardExtractFormatter.Infrastructure.Commands.FormatCommand.Validation;
using Spectre.Console.Cli;

namespace MonthlyCardExtractFormatter.Infrastructure.Commands.FormatCommand.Settings;

public class FormatCommandSettings : CommandSettings
{
    private string _Dir = string.Empty;

    [CommandArgument(0, "<Dir>")]
    [Description("Directory with .txt or .md files to format for excel importing.")]
    [FormatCommandValidationDir]
    public string Dir
    {
        get => _Dir;
        set
        {
            _Dir = Path.GetFullPath(value);
        }
    }
}
