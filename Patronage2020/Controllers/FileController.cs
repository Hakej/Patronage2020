using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Patronage2020.Controllers
{
    /// <summary>  
    ///  The main controller class.
    ///  It's responsible for responding to requests, such as reading and modifying files content
    /// </summary>  
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {

        private readonly IOptions<FileConfig> config;

        /// <summary>
        /// Initializes a new instance of the FileController class
        /// </summary>
        /// <param name="config">Project's configuration file</param>
        public FileController(IOptions<FileConfig> config)
        {
            this.config = config;
        }

        /// <summary>
        /// Handles a GET request - responds with data from files
        /// </summary>
        /// <returns>Content of all data files</returns>
        [HttpGet]
        public string Get()
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

        /// <summary>
        /// Handles a POST request - overwrites all data
        /// </summary>
        /// <param name="content">Data we want to overwrite with</param>
        /// <returns>True if everything went correctly</returns>
        /// <exception cref="System.Web.Http.HttpResponseException">Thrown when data doesn't pass the validation</exception>
        [HttpPost]
        public bool Post([FromBody] string content)
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

        /// <summary>
        /// Handles a POST request - puts data in the end of the file as a new line
        /// </summary>
        /// <param name="content">Data we want to put</param>
        /// <returns>True if everything went correctly</returns>
        /// <exception cref="System.Web.Http.HttpResponseException">Thrown when data doesn't pass the validation</exception>
        [HttpPut]
        public bool Put([FromBody] string content)
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

        /// <summary>
        /// Validates data if it's valid to save in the files
        /// </summary>
        /// <param name="content">Data to validate</param>
        /// <exception cref="System.Web.Http.HttpResponseException">Thrown when data is invalid</exception>
        private void ValidateContent(string content)
        {
            var lineMaxLength = config.Value.LineMaxLength;
            if (content.Length > lineMaxLength)
            {
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}