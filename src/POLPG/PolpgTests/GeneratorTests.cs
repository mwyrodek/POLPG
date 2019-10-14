using System;
using System.Collections.Generic;
using System.IO;
using PolpgUI;
using PolpgUI.Exceptions;
using Xunit;

namespace PolpgTests
{
    public class GeneratorTests
    {
        private PageObjectGenerator sut;
        private Dictionary<string, string> input;

        [Fact]
        public void SimplePage_IsDefaultTemplate()
        {
            var result = "done";
            input = new Dictionary<string, string> {{"SimplePage", result}};

            sut = new PageObjectGenerator(input);

            var generatedCode = sut.Generate();

            Assert.Equal(result, generatedCode);
        }

        [Fact]
        public void GeneratorSanitaze_Results()
        {
            var result = "done";
            input = new Dictionary<string, string> {{"SimplePage", $"${result}$$$$$$"}};

            sut = new PageObjectGenerator(input);

            var generatedCode = sut.Generate();

            Assert.Equal(result, generatedCode);
        }

        [Fact]
        public void Generator_DefaultPageName_IsLoginPAge()
        {
            var result = "LoginPage";
            input = new Dictionary<string, string> {{"SimplePage", "$className$"}};

            sut = new PageObjectGenerator(input);

            var generatedCode = sut.Generate();

            Assert.Equal(result, generatedCode);
        }

        [Fact]
        public void Generator_SetBamePageName_IsLoginPAge()
        {
            var result = "DarkPage";
            input = new Dictionary<string, string> {{"SimplePage", "$className$"}};

            sut = new PageObjectGenerator(input);

            var generatedCode = sut.SetName(result).Generate();

            Assert.Equal(result, generatedCode);
        }

        [Fact]
        public void Generator_NoInheritence_IsEmpty()
        {
            input = new Dictionary<string, string> {{"SimplePage", "$inheritanceName$"}};

            sut = new PageObjectGenerator(input);

            var generatedCode = sut.Generate();

            Assert.Equal(string.Empty, generatedCode);
        }

        [Fact]
        public void Generator_InheritenceActive_SetValue()
        {
            var entryValue = "BasePage";
            var expectedValue = " : BasePage";
            
            input = new Dictionary<string, string> {{"SimplePage", "$inheritanceName$"}};

            sut = new PageObjectGenerator(input);

            var generatedCode = sut.EnableInheritance(true)
                .SetInheritanceValue(entryValue)
                .Generate();

            Assert.Equal(expectedValue, generatedCode);
        }

        [Fact]
        public void Generator_NoInheritence_DriverLineNotRemoved()
        {
            input = new Dictionary<string, string> {{"SimplePage", "$private IWebDriver driver;$"}};

            sut = new PageObjectGenerator(input);

            var generatedCode = sut.Generate();

            Assert.Equal("private IWebDriver driver;", generatedCode);
        }

        [Fact]
        public void Generator_InheritenceActive_DriverLineRemoved()
        {
            input = new Dictionary<string, string> {{"SimplePage", "$private IWebDriver driver;$"}};

            sut = new PageObjectGenerator(input);

            var generatedCode = sut.EnableInheritance(true)
                .Generate();

            Assert.Equal(string.Empty, generatedCode);
        }

        [Fact]
        public void Generator_NoInheritence_NoBase()
        {
            input = new Dictionary<string, string> {{"SimplePage", "(IWebDriver driver)"}};

            sut = new PageObjectGenerator(input);

            var generatedCode = sut.Generate();

            Assert.Equal("(IWebDriver driver)", generatedCode);
        }

        [Fact]
        public void Generator_Iheritence_AddedBase()
        {
            input = new Dictionary<string, string> { { "SimplePage", "(IWebDriver driver)" } };

            sut = new PageObjectGenerator(input);

            var generatedCode = sut.EnableInheritance(true).Generate();

            Assert.Equal("(IWebDriver driver) : base(driver)", generatedCode);
        }

        [Fact]
        public void Generator_RequiresSimpleTemplate()
        {
            input = new Dictionary<string, string> { { "SimplePageX", "$private IWebDriver driver;$" } };
            var expectedMessage = "Simple Page Template is required.";

            var exception = Assert.Throws<TemplateNotFoundException>(
                () => sut = new PageObjectGenerator(input));

            Assert.Equal(expectedMessage, exception.Message);
        }

        // driver changes driver name
        [Fact]
        public void Generator_ChangeDriverName()
        {
            input = new Dictionary<string, string> { { "SimplePage", "(IWebDriver driver)" } };
            var newName = "fake";
            var expected = $"(IWebDriver {newName})";
            sut = new PageObjectGenerator(input);

            var generatedCode = sut.SetDriverName(newName).Generate();

            Assert.Equal(expected, generatedCode);
        }

        [Fact]
        public void Generator_IheritenceAndDriverCollsion_WorksWell()
        {
            input = new Dictionary<string, string> { { "SimplePage", "$private IWebDriver driver;$ (IWebDriver driver)" } };
            var newName = "fake";
            var expected = $" (IWebDriver {newName}) : base({newName})";
            sut = new PageObjectGenerator(input);

            var generatedCode = sut.SetDriverName(newName).EnableInheritance(true).Generate();

            Assert.Equal(expected, generatedCode);
        }
    }
}
