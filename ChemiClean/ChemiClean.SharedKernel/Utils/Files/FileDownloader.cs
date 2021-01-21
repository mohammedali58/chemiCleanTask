using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;

namespace ChemiClean.SharedKernel
{
    public class FileDownloader
    {
        private readonly string _url;
        private readonly string _fullPathWhereToSave;
        private bool _result = false;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(0);
        private readonly IConfiguration _configuration;


        public FileDownloader(string url, IConfiguration config)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException("url");

            _configuration = config;
            _url = url;
            _fullPathWhereToSave = $"{_configuration.GetSection("FilesPaths").GetSection("BaseUrl").Value}";
        }

        public byte[] StartDownload(int timeout)
        {
            try
            {

                Directory.CreateDirectory(Path.GetDirectoryName(_fullPathWhereToSave));

                if (File.Exists(_fullPathWhereToSave))
                {
                    File.Delete(_fullPathWhereToSave);
                }
                using WebClient client = new WebClient();
                var ur = new Uri(_url);

                byte[] response = new System.Net.WebClient().DownloadData(ur);

                return response;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        private void WebClientDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) { }

        private void WebClientDownloadCompleted(object sender, AsyncCompletedEventArgs args)
        {
            _result = !args.Cancelled;
            _semaphore.Release();
        }

        public static byte[] DownloadFile(string url, IConfiguration config, int timeoutInMilliSec)
        {
            return new FileDownloader(url, config).StartDownload(timeoutInMilliSec);
        }

        public static byte[] GetBinaryFile(string filename)
        {
            //byte[] bytes = null;
            byte[] bytes = System.IO.File.ReadAllBytes(filename);
            System.IO.File.Delete(filename);
            return bytes;
            //using (FileStream fileStream = File.OpenRead(filename))
            //{
            //    MemoryStream memStream = new MemoryStream();
            //    memStream.SetLength(fileStream.Length);
            //    fileStream.Read(memStream.GetBuffer(), 0, (int)fileStream.Length);
            //    return memStream.ToArray();
            //}

        }
    }
}
