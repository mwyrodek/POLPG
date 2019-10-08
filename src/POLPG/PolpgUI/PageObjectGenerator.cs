using PolpgUI.Annotations;

namespace PolpgUI
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Resources;

    /// <summary>
    /// Page Object Generator using fluent interfaces.
    /// </summary>
    public class PageObjectGenerator
    {
        private readonly Dictionary<string, string> pageTemplates = new Dictionary<string, string>();
        private readonly string separator = "<end>";
        private string pageName = "LoginPage";
        private string inheritance = string.Empty;
        private string driver = "driver";
        private bool IsInheritance = false;
        private string currentTemplate = "SimplePage";

        /// <summary>
        /// Initializes a new instance of the <see cref="PageObjectGenerator"/> class.
        /// </summary>
        /// <param name="streamResourceInfo">stream from WPF content.</param>
        public PageObjectGenerator(StreamResourceInfo streamResourceInfo)
        {
            string readToEnd = string.Empty;
            using (var reader = new StreamReader(streamResourceInfo.Stream))
            {
                readToEnd = reader.ReadToEnd();
            }

            var splitFile = readToEnd.Split(this.separator);
            currentTemplate = splitFile[0];
            this.pageTemplates.Add(splitFile[0], splitFile[1]);
        }

        /// Initializes a new instance of the <see cref="PageObjectGenerator"/> class.
        /// </summary>
        /// <param name="streamResourceInfo">stream from WPF content.</param>
        public PageObjectGenerator(Dictionary<string, string> templates)
        {
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

            //sanitize other values that werent sanitized
            return generatedPage.Replace("$", string.Empty);
        }

        private string ApplyInheritance(string generatedPage)
        {
            generatedPage = generatedPage.Replace("$inheritanceName$", this.IsInheritance ? this.inheritance : string.Empty);
            if (this.IsInheritance)
            {
                generatedPage = generatedPage.Replace("$private IWebDriver driver;$", string.Empty)
                    .Replace("(IWebDriver driver)", "(IWebDriver driver) : base(driver)")
                    .Replace("driver", driver);
            }

            return generatedPage;
        }

        public PageObjectGenerator EnableInheritance(bool isInheritanceIsChecked)
        {
            this.IsInheritance = isInheritanceIsChecked;
            return this;
        }
    }
}