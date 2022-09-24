using NUnit.Framework;
using Test = Toolbelt.NUnit.TestName.TestAttribute;

public class TestAttributeTest
{
    [@Test(TestName = "Test for TestAttribute with TestName")]
    public void TestAttribute_with_TestName_Test()
    {
        Console.WriteLine("This is " + nameof(TestAttribute_with_TestName_Test) + ".");
        Assert.Pass();
    }

    [@Test]
    public void TestAttribute_without_TestName_Test()
    {
        Console.WriteLine("This is " + nameof(TestAttribute_without_TestName_Test) + ".");
        Assert.Pass();
    }
}
