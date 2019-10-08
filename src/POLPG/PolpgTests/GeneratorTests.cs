using System;
using System.Collections.Generic;
using System.IO;
using PolpgUI;
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
            input = new Dictionary<string, string>();
            input.Add("SimplePage", result);

            sut = new PageObjectGenerator(input);

            var generatedCode = sut.Generate();

            Assert.Equal(result, generatedCode);
        }

        [Fact]
        public void GeneratorSanitaze_Results()
        {
            var result = "done";
            input = new Dictionary<string, string>();
            input.Add("SimplePage", $"${result}$$$$$$");

            sut = new PageObjectGenerator(input);

            var generatedCode = sut.Generate();

            Assert.Equal(result, generatedCode);
        }

        [Fact]
        public void Generator_DefaultPageName_IsLoginPAge()
        {
            var result = "LoginPage";
            input = new Dictionary<string, string>();
            input.Add("SimplePage", "$className$");

            sut = new PageObjectGenerator(input);

            var generatedCode = sut.Generate();

            Assert.Equal(result, generatedCode);
        }

        [Fact]
        public void Generator_SetBamePageName_IsLoginPAge()
        {
            var result = "DarkPage";
            input = new Dictionary<string, string>();
            input.Add("SimplePage", "$className$");

            sut = new PageObjectGenerator(input);

            var generatedCode = sut.SetName(result).Generate();

            Assert.Equal(result, generatedCode);
        }

        [Fact]
        public void Generator_NoInheritence_IsEmpty()
        {
            input = new Dictionary<string, string>();
            input.Add("SimplePage", "$inheritanceName$");

            sut = new PageObjectGenerator(input);

            var generatedCode = sut.Generate();

            Assert.Equal(string.Empty, generatedCode);
        }

        // inhertience without activeting it

        // inheritence with active

        // no inheritence no base
        // inheritence base

        // inheritence remove driver line
        // no iheritence leave it

        // driver changes driver name

        // inheritence and driver possible collison

        // nosimple page error
    }
}
