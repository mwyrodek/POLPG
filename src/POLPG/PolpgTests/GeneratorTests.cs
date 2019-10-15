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
            var expectedMessage = "SimplePage Template not found.";

            var exception = Assert.Throws<TemplateNotFoundException>(
                () => new PageObjectGenerator(input));

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

        [Fact]
        public void Generator_ChainTrue_ChainedTemplateUsed()
        {
            input = new Dictionary<string, string> { { "SimplePage", "$private IWebDriver driver;$ (IWebDriver driver)" }, { "SimplePageChained", "$private IWebDriver driver;$ (IWebDriver driver)" } };
            sut = new PageObjectGenerator(input);
            sut.EnableMethodChaining(true).Generate();

            Assert.Equal("SimplePageChained", sut.CurrentTemplate);
        }

        [Fact]
        public void Generator_ChainTrue_NoTemplate_ExceptioIsThrown()
        {
            input = new Dictionary<string, string> { { "SimplePage", "$private IWebDriver driver;$ (IWebDriver driver)" }};
            sut = new PageObjectGenerator(input);

            var exception = Assert.Throws<TemplateNotFoundException>(
                () => sut.EnableMethodChaining(true));

        }

        [Fact]
        public void Generator_ChainFalseTrue_PlainTemplateUsed()
        {
            input = new Dictionary<string, string> { { "SimplePage", "$private IWebDriver driver;$ (IWebDriver driver)" }, { "SimplePageChained", "$private IWebDriver driver;$ (IWebDriver driver)" } };
            sut = new PageObjectGenerator(input);
            sut.EnableMethodChaining(false).Generate();

            Assert.Equal("SimplePage", sut.CurrentTemplate);
        }

        [Fact]
        public void Generator_TemplateChanged_NewTempleateIsUsed()
        {
            var expected = "NewTemplate";
            input = new Dictionary<string, string> { { "SimplePage", "$private IWebDriver driver;$ (IWebDriver driver)" }, { expected, "$private IWebDriver driver;$ (IWebDriver driver)" } };
            sut = new PageObjectGenerator(input);
            sut.SelectTemplate(expected).Generate();

            Assert.Equal(expected, sut.CurrentTemplate);
        }


        [Fact]
        public void Generator_TemplateChanged_NoTemplate_ErrorIsThrown()
        {
            input = new Dictionary<string, string> { { "SimplePage", "$private IWebDriver driver;$ (IWebDriver driver)" } };
            sut = new PageObjectGenerator(input);

            var exception = Assert.Throws<TemplateNotFoundException>(
                () => sut.SelectTemplate("notemplateexists"));
        }

        [Fact]
        public void Generator_TemplateChangedWhenChainActiveWillKeepChain_NoTemplate_ErrorIsThrown()
        {
            input = new Dictionary<string, string> { { "SimplePage", "" }, { "SimplePageChained", "" }, { "NewPage", "" }, { "NewPageChained", "" } };
            sut = new PageObjectGenerator(input);
            sut.EnableMethodChaining(true).SelectTemplate("NewPage");

            Assert.Equal("NewPageChained", sut.CurrentTemplate);
        }
    }
}
