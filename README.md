# Test Name for NUnit [![NuGet Package](https://img.shields.io/nuget/v/Toolbelt.NUnit.TestName.svg)](https://www.nuget.org/packages/Toolbelt.NUnit.TestName/) [![tests](https://github.com/jsakamoto/Toolbelt.NUnit.TestName/actions/workflows/unit-tests.yml/badge.svg?branch=main&event=push)](https://github.com/jsakamoto/Toolbelt.NUnit.TestName/actions/workflows/unit-tests.yml)

## Summary

This package provides test attributes that alternate NUnit's `[Test]` and `[TestCaseSource]` attribute to add `TestName` property.

**Before:**  
![](https://raw.githubusercontent.com/jsakamoto/Toolbelt.NUnit.TestName/main/.assets/before.png)


**After:**  
![](https://raw.githubusercontent.com/jsakamoto/Toolbelt.NUnit.TestName/main/.assets/after.png)

## How to use

1. Add package to your project like this.

```shell
dotnet add package Toolbelt.NUnit.TestName
```

2. Add two `global using ...` statements in the `Usings.cs` C# source code file in the NUnit test project.

```csharp
/* 📜 "Usings.cs" */
global using NUnit.Framework;

// 👇 Add these tow lines.
global using Test = Toolbelt.NUnit.TestName.TestAttribute;
global using TestCaseSource = Toolbelt.NUnit.TestName.TestCaseSourceAttribute;
```

3. After doing the above step, You will see the compile errors like below if you have already implemented some test methods. 

- `error CS1614: 'Test' is ambiguous between 'TestAttribute' and 'TestAttribute'. Either use '@Test' or explicitly include the 'Attribute' suffix.` 
- `error CS1614: 'TestCaseSource' is ambiguous between 'TestCaseSourceAttribute' and 'TestCaseSourceAttribute'. Either use '@TestCaseSource' or explicitly include the 'Attribute' suffix.`

4. If so, please replace existing `[Test]` to `[@Test]` and replace existing `[TestCaseSource]` to `[@TestCaseSource]`. And later, please use `[@Test]` instead of `[Test]`, and use `[@TestCaseSource]` instead of `[TestCaseSource]`. In short, please make those test attribute names start with `@`. 

```csharp
[@Test] // 👈 Use [@Test] instead of [Test]
public void MyTestMethod() {
  ...

[@TestCaseSource("...")] // 👈 Use [@TestCaseSource] instead of [TestCaseSource]
public void MyTestCasesMethod(...) {
  ...
```

5. Finally, you can use the `TestName` attribute in `[@Test]` and `[@TestCaseSource]` attributes to show more readable test names in a display such as Visual Studio Test Explorer or the `dotnet test` command.

```csharp
// 👇 You can use the "TestName" property.
[@Test(TestName = "...")] 
public void MyTestMethod() {
  ...
// 👇 You can use the "TestName" property.
[@TestCaseSource("...", TestName = "...")]
public void MyTestCasesMethod(...) {
  ...
```

### Aside: Why doesn't this package provide the `TestCase` attribute?

Because the `TestCase` attribute in NUnit already has the `TestName` property.
Honestly, I don't know why NUnit doesn't provide the `TestName` property on the `Test` and `TestCaseSource` attributes.
## License

[Mozilla Public License Version 2.0](https://github.com/jsakamoto/Toolbelt.NUnit.TestName/blob/main/LICENSE)
