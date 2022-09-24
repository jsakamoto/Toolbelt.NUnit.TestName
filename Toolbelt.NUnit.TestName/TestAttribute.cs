using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace Toolbelt.NUnit.TestName;

/// <summary>
/// Adding this attribute to a method within a NUnit.Framework.TestFixtureAttribute
/// class makes the method callable from the NUnit test runner. There is a property
/// called Description which is optional which you can provide a more detailed test
/// description. This class cannot be inherited.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class TestAttribute : NUnitAttribute, ISimpleTestBuilder, IApplyToTest, IImplyFixture
{
    private readonly global::NUnit.Framework.TestAttribute _TestAttribute;

    /// <summary>
    /// Descriptive text for this test
    /// </summary>
    public string Description
    {
        get => this._TestAttribute.Description;
        set => this._TestAttribute.Description = value;
    }

    /// <summary>
    /// The author of this test
    /// </summary>
    public string Author
    {
        get => this._TestAttribute.Author;
        set => this._TestAttribute.Author = value;
    }

    /// <summary>
    /// The type that this test is testing
    /// </summary>
    public Type TestOf
    {
        get => this._TestAttribute.TestOf;
        set => this._TestAttribute.TestOf = value;
    }


    /// <summary>
    /// Gets or sets the expected result.
    /// </summary>
    public object ExpectedResult
    {
        get => this._TestAttribute.ExpectedResult;
        set => this._TestAttribute.ExpectedResult = value;
    }

    /// <summary>
    /// Returns true if an expected result has been set
    /// </summary>
    public bool HasExpectedResult => this._TestAttribute.HasExpectedResult;

    /// <summary>
    /// Gets or sets the name of the test.
    /// </summary>
    /// <value>The name of the test.</value>
    public string? TestName { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TestAttribute"/> class.
    /// </summary>
    public TestAttribute()
    {
        this._TestAttribute = new();
    }

    void IApplyToTest.ApplyToTest(Test test)
    {
        this._TestAttribute.ApplyToTest(test);
    }

    TestMethod ISimpleTestBuilder.BuildFrom(IMethodInfo method, Test suite)
    {
        var testMethod = this._TestAttribute.BuildFrom(method, suite);
        if (!string.IsNullOrEmpty(this.TestName))
        {
            testMethod.Name = this.TestName;
        }
        return testMethod;
    }
}
