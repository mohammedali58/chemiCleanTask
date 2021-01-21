using System.IO;
using System.Reflection;

namespace ChemiClean.SharedKernel

{
    public class HtmlFileReader : IHtmlFileReader
    {
        #region Constructor

        public HtmlFileReader()
        { }

        #endregion Constructor

        #region Get File Content

        private string GetFileContent(string _culture, string fileName)
        {
            string fileContent = string.Empty;
            string rootDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string path = $@"{rootDir}\Utils\HtmlUtils\HtmlFiles\{_culture}\{fileName}.html";
            if (File.Exists(path))
                fileContent = File.ReadAllText(path);

            return fileContent;
        }

        #endregion Get File Content

        public string ForgetPasswordHtml(string _culture, string fileName)
        {
            return GetFileContent(_culture, fileName);
        }

        public string NewRegistrationHtml(string _culture, string fileName)
        {
            return GetFileContent(_culture, fileName);
        }
    }
}