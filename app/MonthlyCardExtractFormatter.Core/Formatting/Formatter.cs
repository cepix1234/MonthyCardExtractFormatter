using System.Text.RegularExpressions;
using MonthlyCardExtractFormatter.Core.Formatting.Models;

namespace MonthlyCardExtractFormatter.Core.Formatting;

public class Formatter : IFormatter
{
    private readonly Regex _isDataLineRegex = new Regex(@"^\d\d\.\d\d\.\d\d ");

    private readonly Regex _splitDateNameRegex = new Regex(@"^(\d\d\.\d\d\.\d\d) (\D)");
    private readonly string _splitDateNameReplacement = "$1;$2";

    private readonly Regex _splitNameCostRegex = new Regex(@" ([+-]\d)");
    private readonly string _splitNameCostReplacement = ";$1";

    private readonly Regex _splitStatusCostRegex = new Regex(@" (\d\.)");
    private readonly string _splitStatusCostReplacement = ";$1";

    private readonly Regex _fixPositiveCostRegex = new Regex(@";\+(\d)");
    private readonly string _fixPositiveCostReplacement = ";$1";

    private readonly Regex _fixDecimalDelimiterRegex = new Regex(@"(\d),(\d\d)");
    private readonly string _fixDecimalDelimiterReplacement = "$1.$2";

    private readonly Regex _fixThausendDelimiterRegex = new Regex(@";(-?\d).(\d\d\d)");
    private readonly string _fixThausendDelimiterReplacement = ";$1,$2";

    public bool IsDataLine(string line)
    {
        return this._isDataLineRegex.IsMatch(line);
    }

    public string SplitDateAsstablishmentName(string line)
    {
        return this._splitDateNameRegex.Replace(line, this._splitDateNameReplacement);
    }

    public string SplitNameCost(string line)
    {
        return this._splitNameCostRegex.Replace(line, this._splitNameCostReplacement);
    }

    public string SplitStatusCost(string line)
    {
        return this._splitStatusCostRegex.Replace(line, this._splitStatusCostReplacement);
    }

    public string FixPositiveCost(string line)
    {
        return this._fixPositiveCostRegex.Replace(line, this._fixPositiveCostReplacement);
    }

    public string FixDecimalDelimiter(string line)
    {
        return this._fixDecimalDelimiterRegex.Replace(line, this._fixDecimalDelimiterReplacement);
    }

    public string FixThausendDelimiter(string line)
    {
        return this._fixThausendDelimiterRegex.Replace(line, this._fixThausendDelimiterReplacement);
    }

    public string? Format(string line)
    {
        if (this.IsDataLine(line))
        {
            string result = this.SplitDateAsstablishmentName(line);
            result = this.SplitNameCost(result);
            result = this.SplitStatusCost(result);
            result = this.FixPositiveCost(result);
            result = this.FixDecimalDelimiter(result);
            result = this.FixThausendDelimiter(result);
            return result;
        }
        return null;
    }
}
