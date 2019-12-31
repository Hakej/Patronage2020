using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Patronage2020
{
    public class FileManagementService : IFileManagementService
    {

        private readonly IOptions<FileConfig> config;

        /// <summary>
        /// Initializes a new instance of the FileManagementService class
        /// </summary>
        /// <param name="config">Project's configuration file</param>
        public FileManagementService(IOptions<FileConfig> config)
        {
            this.config = config;
        }

        public bool AddContent(string content)
        {
            ValidateContent(content);

            var fileDir = config.Value.FileDir;
            var filePath = Path.Combine(config.Value.FileDir, config.Value.FileName);

            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }

            content = Environment.NewLine + content;
            var fileNumber = 1;
            var lineWasInserted = false;

            while (!lineWasInserted)
            {
                if (!System.IO.File.Exists(filePath + fileNumber))
                {
                    using (var file = new StreamWriter(filePath + fileNumber, true))
                    {
                        file.Write(content);
                    }

                    lineWasInserted = true;
                    break;
                }

                var fileInfo = new FileInfo(filePath + fileNumber);
                var fileSize = (int)fileInfo.Length;
                var fileMaxSize = config.Value.FileMaxSize;

                if (fileSize >= fileMaxSize)
                {
                    fileNumber++;
                    continue;
                }
                else
                {
                    var totalLength = content.Length + fileSize;

                    // If it's too large, we take as many characters as we can and put the rest in the next file
                    if (totalLength > fileMaxSize)
                    {
                        // Measure how many characters will fit in
                        var length = totalLength % fileMaxSize;
                        length = content.Length - length;

                        // Get the part that still fits in
                        var firstPart = content.Substring(0, length);

                        // Put it in the first file
                        using (var f = new StreamWriter(filePath + fileNumber, true))
                        {
                            f.Write(firstPart);
                        }

                        fileNumber++;

                        // Save the characters we haven't put in yet
                        content = content.Substring(length);
                    }

                    using (var outputFile = new StreamWriter(filePath + fileNumber, true))
                    {
                        outputFile.Write(content);
                    }

                    lineWasInserted = true;
                }
            }

            return true;
        }

        public bool OverwriteContent(string content)
        {
            ValidateContent(content);

            var fileDir = config.Value.FileDir;
            var filePath = Path.Combine(config.Value.FileDir, config.Value.FileName);

            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }

            // Delete all existing data
            var dirInfo = new DirectoryInfo(fileDir);
            foreach (var file in dirInfo.GetFiles())
            {
                file.Delete();
            }

            using (var file = new StreamWriter(filePath + 1))
            {
                file.Write(content);
            }

            return true;
        }

        public string ReadContent()
        {
            var fileDir = config.Value.FileDir;

            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }

            var builder = new StringBuilder();
            var dirInfo = new DirectoryInfo(fileDir);

            foreach (var fileInfo in dirInfo.GetFiles())
            {
                using (var file = new StreamReader(fileInfo.FullName))
                {
                    // Concatenate all files into single string
                    builder.Append(file.ReadToEnd());
                }
            }

            return builder.ToString();
        }

        public void ValidateContent(string content)
        {
            var lineMaxLength = config.Value.LineMaxLength;
            if (content.Length > lineMaxLength)
            {
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.BadRequest);
            }
        }               
    }
}
