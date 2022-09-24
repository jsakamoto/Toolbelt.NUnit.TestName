using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace Toolbelt.NUnit.TestName;

//
// Summary:
//     TestCaseSourceAttribute indicates the source to be used to provide test cases
//     for a test method.
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class TestCaseSourceAttribute : NUnitAttribute, ITestBuilder, IImplyFixture
{
    private readonly global::NUnit.Framework.TestCaseSourceAttribute _TestCaseSourceAttribute;

    /// <summary>
    /// A set of parameters passed to the method, works only if the Source Name is a
    /// method. If the source name is a field or property has no effect.
    /// </summary>
    public object[] MethodParams => this._TestCaseSourceAttribute.MethodParams;

    /// <summary>
    /// The name of a the method, property or fiend to be used as a source
    /// </summary>
    public string SourceName => this._TestCaseSourceAttribute.SourceName;

    /// <summary>
    /// A Type to be used as a source
    /// </summary>
    public Type SourceType => this._TestCaseSourceAttribute.SourceType;

    /// <summary>
    /// Gets or sets the category associated with every fixture created from this attribute.<br/>
    /// May be a single category or a comma-separated list.
    /// </summary>
    public string Category
    {
        get => this._TestCaseSourceAttribute.Category;
        set => this._TestCaseSourceAttribute.Category = value;
    }

    /// <summary>
    /// Gets or sets the name of the test.
    /// </summary>
    /// <value>The name of the test.</value>
    public string? TestName { get; set; }

    /// <summary>
    /// Construct with the name of the method, property or field that will provide data
    /// </summary>
    /// <param name="sourceName">The name of a static method, property or field that will provide data.</param>
    public TestCaseSourceAttribute(string sourceName)
    {
        this._TestCaseSourceAttribute = new(sourceName);
    }

    /// <summary>
    /// Construct with a Type and name
    /// </summary>
    /// <param name="sourceType">The Type that will provide data</param>
    /// <param name="sourceName">The name of a static method, property or field that will provide data.</param>
    /// <param name="methodParams">A set of parameters passed to the method, works only if the Source Name is a method. If the source name is a field or property has no effect.</param>
    public TestCaseSourceAttribute(Type sourceType, string sourceName, object[] methodParams)
    {
        this._TestCaseSourceAttribute = new(sourceType, sourceName, methodParams);
    }

    /// <summary>
    /// Construct with a Type and name
    /// </summary>
    /// <param name="sourceType">The Type that will provide data</param>
    /// <param name="sourceName">The name of a static method, property or field that will provide data.</param>
    public TestCaseSourceAttribute(Type sourceType, string sourceName)
    {
        this._TestCaseSourceAttribute = new(sourceType, sourceName);
    }

    /// <summary>
    /// Construct with a name
    /// </summary>
    /// <param name="sourceName">The name of a static method, property or field that will provide data.</param>
    /// <param name="methodParams">A set of parameters passed to the method, works only if the Source Name is a method. If the source name is a field or property has no effect.</param>
    public TestCaseSourceAttribute(string sourceName, object[] methodParams)
    {
        this._TestCaseSourceAttribute = new(sourceName, methodParams);
    }

    /// <summary>
    /// Construct with a Type
    /// </summary>
    /// <param name="sourceType">The type that will provide data</param>
    public TestCaseSourceAttribute(Type sourceType)
    {
        this._TestCaseSourceAttribute = new(sourceType);
    }

    /// <summary>
    /// Builds any number of tests from the specified method and context.
    /// </summary>
    /// <param name="method">The IMethod for which tests are to be constructed.</param>
    /// <param name="suite">The suite to which the tests will be added.</param>
    IEnumerable<TestMethod> ITestBuilder.BuildFrom(IMethodInfo method, Test? suite)
    {
        foreach (var testMethod in this._TestCaseSourceAttribute.BuildFrom(method, suite))
        {
            if (!string.IsNullOrEmpty(this.TestName))
            {
                var formattedTestName = string.Format(this.TestName, testMethod.Arguments);
                if (formattedTestName != this.TestName)
                {
                    testMethod.Name = formattedTestName;
                }
                else
                {
                    var argsString = string.Join(", ", testMethod.Arguments.Select(arg => arg?.ToString() ?? "null"));
                    testMethod.Name = $"{this.TestName} ({argsString})";
                }
            }
            yield return testMethod;
        }
    }
}
