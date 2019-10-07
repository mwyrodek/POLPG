using System.Windows.Resources;

namespace PolpgUI.Configuration
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Windows;
    using PolpgUI.Models;

    public static class DataManager
    {
        private static readonly string directory = "Data";
        private static readonly string templates = "PageTemplates.txt";
        private static readonly string settings = "setting.json";
        private static readonly string separator = "<sep>";

        public static GeneratorSettingsModel ReadDefaultValues()
        {
            var readFile = ReadFileFacade(settings);
            throw new NotImplementedException();
        }

        public static Dictionary<string, string> ReadTemplate()
        {
            var readFile = ReadFileFacade(templates);
            var dictionary = new Dictionary<string, string>();
            var splitedFile = readFile.Split(separator);
            
            for (int i = 0; i < splitedFile.Length-1; i += 2)
            {
                var key = splitedFile[i].Replace("\r\n", string.Empty);
                var value = splitedFile[i + 1];
                dictionary.Add(key, value);
            }

            return dictionary;
        }

        private static string GetDirectory(string fileName)
        {
            return $@"{Directory.GetCurrentDirectory()}\{directory}\{fileName}";
        }

        /// <summary>
        /// A facaded that read setting file and in case it doesn't exist reconstructs it from resources.
        /// </summary>
        /// <param name="path">path to file includes directory.</param>
        /// <returns>string with data.</returns>
        private static string ReadFileFacade(string file)
        {
            string path = GetDirectory(templates);
            var readFile = string.Empty;
            if (!File.Exists(path))
            {
                readFile = ReadDataFromResource(file);
                SaveFile(path, readFile);
                return readFile;
            }

            readFile = ReadFile(path);
            return readFile;
        }

        private static string ReadDataFromResource(string file)
        {
            Uri uri = new Uri($"{directory}/{file}", UriKind.Relative);
            var streamResourceInfo = Application.GetContentStream(uri);
            string fileContent = string.Empty;
            using (var reader = new StreamReader(streamResourceInfo.Stream))
            {
                fileContent = reader.ReadToEnd();
            }

            return fileContent;
        }

        private static string ReadFile(string filePath)
        {
            string fileContent =String.Empty;
            using (var reader = new StreamReader(filePath))
            {
                fileContent = reader.ReadToEnd();
            }

            return fileContent;
        }

        private static void SaveFile(string fileName, string content)
        {
            using (var writer = new StreamWriter(fileName))
            {
                writer.Write(content);
            }
        }
    }
}