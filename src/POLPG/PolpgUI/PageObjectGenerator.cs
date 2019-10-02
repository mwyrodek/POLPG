using System;

namespace PolpgUI
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Resources;

    public class PageObjectGenerator
    {
        private readonly List<string> pageTemplates = new List<string>();
        private readonly string separator = "<end>";
        private string pageName = "LoginPage";

        public PageObjectGenerator(StreamResourceInfo streamResourceInfo)
        {
            string readToEnd = string.Empty;
            using (var reader = new StreamReader(streamResourceInfo.Stream))
            {
                readToEnd = reader.ReadToEnd();
            }

            this.pageTemplates = readToEnd.Split(this.separator).ToList();
        }

        public PageObjectGenerator SetName(string name)
        {
            this.pageName = name;
            return this;
        }

        public string Generate()
        {
            var generatedPage = this.pageTemplates.FirstOrDefault().Replace("$className$", this.pageName);
            return generatedPage;
        }
    }
}