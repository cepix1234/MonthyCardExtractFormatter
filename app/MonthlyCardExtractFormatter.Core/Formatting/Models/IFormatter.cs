namespace MonthlyCardExtractFormatter.Core.Formatting.Models;

public interface IFormatter
{
    /// <summary>
    /// Format the line given by performing multiple steps.
    /// If the lie is not a data line null is returned;
    /// </summary>
    /// <param name="line">Sting line to format.</param>
    string? Format(string line);

    /// <summary>
    /// Validate if line is data line with regex \d\d.\d\d.\d\d\.
    /// If not a data line skip it.
    /// </summary>
    /// <param name="line">String line to check if line is a data line</param>
    bool IsDataLine(string line);

    /// <summary>
    /// Split Date and establishment name with excel delimiter.
    /// </summary>
    /// <param name="line">Line to split</param>
    string SplitDateAsstablishmentName(string line);

    /// <summary>
    /// Split establishment name and cost with excel delimiter.
    /// </summary>
    /// <param name="line">Line to split</param>
    string SplitNameCost(string line);

    /// <summary>
    /// Split cost and status with excel delimiter.
    /// </summary>
    /// <param name="line">Line to split</param>
    string SplitStatusCost(string line);

    /// <summary>
    /// Fix positive post to not be prepend with +.
    /// </summary>
    /// <param name="line">Line to fix</param>
    string FixPositiveCost(string line);

    /// <summary>
    ///  Fix number formatting for decimal numbers from 0,00 to 0.00
    /// </summary>
    /// <param name="line">Line to fix</param>
    string FixDecimalDelimiter(string line);

    /// <summary>
    ///  Fix number formatting for thousands from 1.000 to 1,000
    /// </summary>
    /// <param name="line">Line to fix</param>
    string FixThausendDelimiter(string line);
}
