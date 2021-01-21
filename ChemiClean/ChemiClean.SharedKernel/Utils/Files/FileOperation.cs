using Microsoft.Extensions.Configuration;
using MSCC.SharedKernel;
using System;
using System.IO;

namespace ChemiClean.SharedKernel

{
    public class FileOperation
    {
        private readonly IConfiguration _configuration;

        public FileOperation(IConfiguration config)
        {
            _configuration = config;
        }

        public string SaveFile(string filePath)
        {
            string chemiCleanFolderPath = _configuration.GetSection("FilesPaths").GetSection("BaseUrl").Value;
            FileInfo file = new FileInfo(filePath);
            string fileName = $"{file.Name}";
            if (!fileName.Contains("."))  
                fileName = $"{file.Name}.txt";
            string fileFullPath = $"{chemiCleanFolderPath}{fileName}";
            byte[] imgByteArray = Convert.FromBase64String(file.FullName);
            File.WriteAllBytes(fileFullPath, imgByteArray);

            return fileName;
        }

        public bool DeleteFile(string fileName)
        {
            string chemiCleanFolderPath = _configuration.GetSection("FilesPaths").GetSection("BaseUrl").Value;
            string fileFullPath = $"{chemiCleanFolderPath}{fileName}";
            try
            {
                File.Delete(fileFullPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string ReadFile(string fileName)
        {
            string chemiCleanFolderPath = _configuration.GetSection("FilesPaths").GetSection("BaseUrl").Value;
            string fileFullPath = $"{chemiCleanFolderPath}{fileName}";
            try
            {
                Byte[] bytes = File.ReadAllBytes(fileFullPath);
                String fileContents = Convert.ToBase64String(bytes);
                return fileContents;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}