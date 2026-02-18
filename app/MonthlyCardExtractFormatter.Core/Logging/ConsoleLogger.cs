using MonthlyCardExtractFormatter.Core.Logging.Models;

namespace MonthlyCardExtractFormatter.Core.Logging;

public class ConsoleLogger: IConsoleLogger
{
    public void Log(String str)
    {
        Console.WriteLine(str);
    }
}