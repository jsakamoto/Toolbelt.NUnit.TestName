using NUnit.Framework;
using TestCaseSource = Toolbelt.NUnit.TestName.TestCaseSourceAttribute;

public class TestCaseSourceAttributeTest
{
    public static readonly IEnumerable<object[]> TestCases1 = new[] {
        new object[]{ 1, "Two", 3.4 },
        new object[]{ 5, "Six", 7.8 },
    };

    [@TestCaseSource(nameof(TestCases1))]
    public void TestCaseSource_without_TestName_Test(int i, string j, double k)
    {
        Console.WriteLine("This is " + nameof(TestCaseSource_without_TestName_Test) + ".");
        Console.WriteLine($"i:{i}, j:{j}, k:{k}");
        Assert.Pass();
    }

    [@TestCaseSource(nameof(TestCases1), TestName = "Test for TestCaseSource")]
    public void TestCaseSource_Test(int i, string j, double k)
    {
        Console.WriteLine("This is " + nameof(TestCaseSource_Test) + ".");
        Console.WriteLine($"i:{i}, j:{j}, k:{k}");
        Assert.Pass();
    }

    [@TestCaseSource(nameof(TestCases1), TestName = "Test for TestCaseSource with place holder - {0}, \"{1}\", {2}")]
    public void TestCaseSource_with_PlaceHolder_Test(int i, string j, double k)
    {
        Console.WriteLine("This is " + nameof(TestCaseSource_with_PlaceHolder_Test) + ".");
        Console.WriteLine($"i:{i}, j:{j}, k:{k}");
        Assert.Pass();
    }
}