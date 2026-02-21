using MonthlyCardExtractFormatter.Core.Formatting;

namespace MonthlyCardExtractFormatter.Core.test.formatting;

public class FormatterTests
{
    private static Formatter formatter;

    [SetUp]
    public void Setup()
    {
        formatter = new Formatter();
    }

    [Test]
    public void Test_Data_line_is_Validated_As_Correct()
    {
        const string line = "03.12.14 some asstablashemnt SI56 234 234234 432523 -1,42 1.972,06";
        bool result = formatter.IsDataLine(line);
        Assert.That(result, Is.EqualTo(true));
    }

    [Test]
    public void Test_Data_line_is_Validated_As_Not_Correct()
    {
        const string line = "some random line that is not a data line";
        bool result = formatter.IsDataLine(line);
        Assert.That(result, Is.EqualTo(false));
    }

    [Test]
    public void Test_Split_Date_Asstablishment_Correctly_Splits()
    {
        string line = "03.12.14 some asstablashemnt SI56 234 234234 432523 -1,42 1.972,06";
        string result = formatter.SplitDateAsstablishmentName(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523 -1,42 1.972,06"));

        line = "03.12.14 234234 some asstablashemnt SI56 234 234234 432523 -1,42 1.972,06";
        result = formatter.SplitDateAsstablishmentName(line);
        Assert.That(result, Is.EqualTo("03.12.14;234234 some asstablashemnt SI56 234 234234 432523 -1,42 1.972,06"));
    }

    [Test]
    public void Test_Split_Date_Asstablishment_Does_Not_Alter_Line()
    {
        const string line = "03.12.14;some asstablashemnt SI56 234 234234 432523 -1,42 1.972,06";
        string result = formatter.SplitDateAsstablishmentName(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523 -1,42 1.972,06"));
    }

    [Test]
    public void Test_Split_Name_Cost_Correctly_Splits()
    {
        const string line = "03.12.14;some asstablashemnt SI56 234 234234 432523 -1,42 1.972,06";
        string result = formatter.SplitNameCost(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;-1,42 1.972,06"));
    }

    [Test]
    public void Test_Split_Name_Cost_Does_Not_alter_Line()
    {
        const string line = "03.12.14;some asstablashemnt SI56 234 234234 432523;-1,42 1.972,06";
        string result = formatter.SplitNameCost(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;-1,42 1.972,06"));
    }

    [Test]
    public void Test_Split_Status_Cost_Correctly_Splits()
    {
        string line = "03.12.14;some asstablashemnt SI56 234 234234 432523;-1,42 1.972,06";
        string result = formatter.SplitStatusCost(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;-1,42;1.972,06"));

        line = "03.12.14;some asstablashemnt SI56 234 234234 432523;-1,42 972,06";
        result = formatter.SplitStatusCost(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;-1,42;972,06"));
    }

    [Test]
    public void Test_Split_Status_Cost_Does_No_Alter_line()
    {
        const string line = "03.12.14;some asstablashemnt SI56 234 234234 432523;-1,42;1.972,06";
        string result = formatter.SplitStatusCost(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;-1,42;1.972,06"));
    }

    [Test]
    public void Test_Fix_Positive_Cost_Correclty_Fixes_Line()
    {
        const string line = "03.12.14;some asstablashemnt SI56 234 234234 432523;+1,42;1.972,06";
        string result = formatter.FixPositiveCost(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;1,42;1.972,06"));
    }

    [Test]
    public void Test_Fix_Positive_Cost_Correclty_Does_Not_Alter_Line()
    {
        const string line = "03.12.14;some asstablashemnt SI56 234 234234 432523;1,42;1.972,06";
        string result = formatter.FixPositiveCost(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;1,42;1.972,06"));
    }

    [Test]
    public void Test_Fix_Positive_Cost_Correclty_Does_Not_Alter_Negative_Cost_Line()
    {
        const string line = "03.12.14;some asstablashemnt SI56 234 234234 432523;-1,42;1.972,06";
        string result = formatter.FixPositiveCost(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;-1,42;1.972,06"));
    }

    [Test]
    public void Test_Fix_Decimal_Delimiter_Correctly()
    {
        string line = "03.12.14;some asstablashemnt SI56 234 234234 432523;-1,42;1.972,06";
        string result = formatter.FixDecimalDelimiter(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;-1.42;1.972.06"));

        line = "03.12.14;some asstablashemnt SI56 234 234234 432523;-132,42;1.972,06";
        result = formatter.FixDecimalDelimiter(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;-132.42;1.972.06"));

        line = "03.12.14;some asstablashemnt SI56 234 234234 432523;-2.132,42;1.972,06";
        result = formatter.FixDecimalDelimiter(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;-2.132.42;1.972.06"));
    }

    [Test]
    public void Test_Fix_Decimal_Delimiter_Does_Not_Alter_Line()
    {
        string line = "03.12.14;some asstablashemnt SI56 234 234234 432523;-1.42;1.972.06";
        string result = formatter.FixDecimalDelimiter(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;-1.42;1.972.06"));

        line = "03.12.14;some asstablashemnt SI56 234 234234 432523;-132.42;1.972.06";
        result = formatter.FixDecimalDelimiter(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;-132.42;1.972.06"));

        line = "03.12.14;some asstablashemnt SI56 234 234234 432523;-2.132.42;1.972.06";
        result = formatter.FixDecimalDelimiter(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;-2.132.42;1.972.06"));
    }

    [Test]
    public void Test_Fix_Thausend_Delimiter_Correctly()
    {
        string line = "03.12.14;some asstablashemnt SI56 234 234234 432523;-1.42;1.972.06";
        string result = formatter.FixThausendDelimiter(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;-1.42;1,972.06"));

        line = "03.12.14;some asstablashemnt SI56 234 234234 432523;-132.42;1.972.06";
        result = formatter.FixThausendDelimiter(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;-132.42;1,972.06"));

        line = "03.12.14;some asstablashemnt SI56 234 234234 432523;2.132.42;1.972.06";
        result = formatter.FixThausendDelimiter(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;2,132.42;1,972.06"));

        line = "03.12.14;some asstablashemnt SI56 234 234234 432523;-2.132.42;1.972.06";
        result = formatter.FixThausendDelimiter(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;-2,132.42;1,972.06"));
    }

    [Test]
    public void Test_Fix_Thausend_Delimiter_Does_Not_Alter_Line()
    {
        string line = "03.12.14;some asstablashemnt SI56 234 234234 432523;-1.42;1,972.06";
        string result = formatter.FixThausendDelimiter(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;-1.42;1,972.06"));

        line = "03.12.14;some asstablashemnt SI56 234 234234 432523;-132.42;1,972.06";
        result = formatter.FixThausendDelimiter(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;-132.42;1,972.06"));

        line = "03.12.14;some asstablashemnt SI56 234 234234 432523;-2,132.42;1,972.06";
        result = formatter.FixThausendDelimiter(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;-2,132.42;1,972.06"));
    }

    [Test]
    public void Test_Format_Correctly_Formates_Line()
    {
        string line = "03.12.14 some asstablashemnt SI56 234 234234 432523 -1,42 1.972,06";
        string? result = formatter.Format(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;-1.42;1,972.06"));

        line = "03.12.14 some asstablashemnt SI56 234 234234 432523 +1,42 1.972,06";
        result = formatter.Format(line);
        Assert.That(result, Is.EqualTo("03.12.14;some asstablashemnt SI56 234 234234 432523;1.42;1,972.06"));

        line = "some non data line";
        result = formatter.Format(line);
        Assert.That(result, Is.EqualTo(null));
    }
}
