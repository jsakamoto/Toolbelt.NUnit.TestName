using Toolbelt.Diagnostics;

namespace Toolbelt.TestTools.NUnit.Test;

public class TestNameTest
{
    [Test]
    public async Task DotNetCLI_Test()
    {
        // Given
        var solutionDir = FileIO.FindContainerDirToAncestor("*.sln");
        using var workDir = WorkDirectory.CreateCopyFrom(Path.Combine(solutionDir, "SampleTest"), args => args.Name is not "obj" and not "bin");

        // When
        var dotnetCLI = await XProcess.Start("dotnet", "test -v:q --nologo --list-tests", workDir).WaitForExitAsync();

        // Then
        Console.WriteLine(dotnetCLI.Output);
        dotnetCLI.ExitCode.Is(0);

        var tests = dotnetCLI.Output.Split('\n')
            .Select(x => x.Trim())
            .SkipWhile(x => x != "The following Tests are available:")
            .Skip(1);
        tests.Is(
            "Test for TestAttribute with TestName",
            "TestAttribute_without_TestName_Test",
            "Test for TestCaseSource (1, Two, 3.4)",
            "Test for TestCaseSource (5, Six, 7.8)",
            "Test for TestCaseSource with place holder - 1, \"Two\", 3.4",
            "Test for TestCaseSource with place holder - 5, \"Six\", 7.8",
            "TestCaseSource_without_TestName_Test(1,\"Two\",3.4d)",
            "TestCaseSource_without_TestName_Test(5,\"Six\",7.8d)");
    }
}