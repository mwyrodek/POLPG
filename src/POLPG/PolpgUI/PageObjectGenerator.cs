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
        private readonly List<string> pageTemplates = new List<string>();
        private readonly string separator = "<end>";
        private string pageName = "LoginPage";
        private string inheritance = string.Empty;
        private string driver = "driver";
        private bool IsInheritance = false;

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

            this.pageTemplates = readToEnd.Split(this.separator).ToList();
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
            var generatedPage = this.pageTemplates
                .FirstOrDefault()
                .Replace("$className$", this.pageName);
            generatedPage = this.ApplyInheritance(generatedPage);

            return generatedPage;
        }

        private string ApplyInheritance(string generatedPage)
        {
            return generatedPage = generatedPage.Replace("$inheritanceName$", this.IsInheritance ? this.inheritance : string.Empty);
        }

        public PageObjectGenerator EnableInheritance(bool isInheritanceIsChecked)
        {
            this.IsInheritance = isInheritanceIsChecked;
            return this;
        }
    }
}