using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Patronage2020.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private const string FileDir = "Data";
        private const string FileName = "PatronageFile";
        private const int FileMaxSize = 256;
        private const int LineMaxLength = 50;

        private readonly string _filePath = Path.Combine(FileDir, FileName);

        [HttpGet]
        public string Get()
        {
            var builder = new StringBuilder();

            if (!Directory.Exists(FileDir))
            {
                Directory.CreateDirectory(FileDir);
            }

            // Concatenate all files into single string
            var dirInfo = new DirectoryInfo(FileDir);
            foreach (var fileInfo in dirInfo.GetFiles())
            {
                using (var file = new StreamReader(fileInfo.FullName))
                {
                    builder.Append(file.ReadToEnd());
                }
            }

            return builder.ToString();
        }

        [HttpPost]
        public bool Post([FromBody] string content)
        {
            ValidateContent(content);

            if (!Directory.Exists(FileDir))
            {
                Directory.CreateDirectory(FileDir);
            }

            // Delete all existing data
            var dirInfo = new DirectoryInfo(FileDir);            
            foreach (var file in dirInfo.GetFiles())
            {
                file.Delete();
            }

            using (var file = new StreamWriter(_filePath + 1))
            {
                file.Write(content);
            }

            return true;
        }

        [HttpPut]
        public bool Put([FromBody] string content)
        {
            ValidateContent(content);

            if (!Directory.Exists(FileDir))
            {
                Directory.CreateDirectory(FileDir);
            }

            content = Environment.NewLine + content;
            var fileNumber = 1;
            var lineWasInserted = false;

            while (!lineWasInserted)
            {
                if (!System.IO.File.Exists(_filePath + fileNumber))
                {
                    using (var file = new StreamWriter(_filePath + fileNumber, true))
                    {
                        file.Write(content);
                    }

                    lineWasInserted = true;
                    break;
                }                

                var fileInfo = new FileInfo(_filePath + fileNumber);
                var fileSize = (int)fileInfo.Length;

                if (fileSize >= FileMaxSize)
                {
                    fileNumber++;
                    continue;
                }
                else
                {
                    var totalLength = content.Length + fileSize;

                    // If it's too large, we take as many characters as we can and put the rest in the next file
                    if (totalLength > FileMaxSize)
                    {
                        // Measure how many characters will fit in
                        var length = totalLength % FileMaxSize;
                        length = content.Length - length;

                        // Get the part that still fits in
                        var firstPart = content.Substring(0, length);

                        // Put it in the first file
                        using (var f = new StreamWriter(_filePath + fileNumber, true))
                        {
                            f.Write(firstPart);
                        }

                        fileNumber++;

                        // Save the characters we haven't put in yet
                        content = content.Substring(length);
                    }

                    using (var outputFile = new StreamWriter(_filePath + fileNumber, true))
                    {
                        outputFile.Write(content);
                    }

                    lineWasInserted = true;
                }
            }            

            return true;
        }

        private static void ValidateContent(string content)
        {
            if (content.Length > LineMaxLength)
            {
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}