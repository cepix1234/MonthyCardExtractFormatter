using Spectre.Console;
using Spectre.Console.Cli;

namespace MonthlyCardExtractFormatter.Infrastructure.Commands.FormatCommand.Validation;

public class FormatCommandValidationDir : ParameterValidationAttribute
{

    public FormatCommandValidationDir() : base("Provided directory should exist.") { }

    public override ValidationResult Validate(CommandParameterContext context)
    {
        if (context.Value is string)
        {
            if (!Directory.Exists(context.Value.ToString()))
            {
                return ValidationResult.Error(
                    $"{context.Parameter.PropertyName} ({context.Value}) directory does not exist.");
            }
            return ValidationResult.Success();
        }
        return ValidationResult.Error($"{context.Parameter.PropertyName} ({context.Value}) needs to be a string.");
    }
}
