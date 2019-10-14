using System;
using PolpgUI.Exceptions;

namespace PolpgUI
{
    using System.Collections.Generic;

    /// <summary>
    /// Page Object Generator using fluent interfaces.
    /// </summary>
    public class PageObjectGenerator
    {
        private readonly Dictionary<string, string> pageTemplates = new Dictionary<string, string>();
        private string pageName = "LoginPage";
        private string inheritance = string.Empty;
        private string driver = "driver";
        private bool isInheritance = false;
        private string currentTemplate = "SimplePage";

        /// <summary>
        /// Initializes a new instance of the <see cref="PageObjectGenerator"/>.
        /// </summary>
        /// <param name="templates">dictonary with list of templetes should include SimplePage</param>
        public PageObjectGenerator(Dictionary<string, string> templates)
        {
            try
            {
                var template = templates["SimplePage"];
            }
            catch (KeyNotFoundException)
            {
                throw new TemplateNotFoundException("Simple Page Template is required.");
            }

            this.pageTemplates = templates;
        }

        /// <summary>
        /// Sets class name.
        /// </summary>
        /// <param name="name">name.</param>
        /// <returns>Page</returns>
        public PageObjectGenerator SetName(string name)
        {
            this.pageName = name;
            return this;
        }

        public PageObjectGenerator SetInheritanceValue(string inheritanceText)
        {
            inheritance = $" : {inheritanceText}";
            return this;
        }

        /// <summary>
        /// Generates String with formatted page object model class for login page.
        /// </summary>
        /// <returns>string with POM class.</returns>
        public string Generate()
        {
            var generatedPage = this.pageTemplates[this.currentTemplate]
                .Replace("$className$", this.pageName);
            generatedPage = this.ApplyInheritance(generatedPage);

            // sanitize other values that weren''t sanitized
            return generatedPage.Replace("driver",driver).Replace("$", string.Empty);
        }

        public PageObjectGenerator EnableInheritance(bool isInheritanceIsChecked)
        {
            this.isInheritance = isInheritanceIsChecked;
            return this;
        }

        public PageObjectGenerator SetDriverName(string newName)
        {
            driver = newName;
            return this;
        }

        private string ApplyInheritance(string generatedPage)
        {
            generatedPage = generatedPage.Replace("$inheritanceName$", this.isInheritance ? this.inheritance : string.Empty);
            if (this.isInheritance)
            {
                generatedPage = generatedPage.Replace("$private IWebDriver driver;$", string.Empty)
                    .Replace("(IWebDriver driver)", "(IWebDriver driver) : base(driver)")
                    .Replace("driver", driver);
            }

            return generatedPage;
        }
    }
}