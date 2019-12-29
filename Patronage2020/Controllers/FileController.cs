using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Patronage2020.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private const string FILE_DIR = "Data/";
        private const string FILE_NAME = "PatronageFile";

        private readonly string _filePath = FILE_DIR + FILE_NAME;
        private readonly ILogger<FileController> _logger;

        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            var builder = new StringBuilder();

            // If file directory doesn't exist, create one
            if (!Directory.Exists(FILE_DIR))
            {
                Directory.CreateDirectory(FILE_DIR);
            }

            // Concatenate all files into single string
            var dirInfo = new DirectoryInfo(FILE_DIR);
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
        public string Post([FromBody] string content)
        {
            // If content is invalid, throw an error
            ValidateContent(content);

            // If file directory doesn't exist, create one
            if (!Directory.Exists(FILE_DIR))
            {
                Directory.CreateDirectory(FILE_DIR);
            }

            // Delete all existing data
            var dirInfo = new DirectoryInfo(FILE_DIR);            
            foreach (var file in dirInfo.GetFiles())
            {
                file.Delete();
            }

            // Create new file and insert content
            using (var file = new StreamWriter(_filePath + 1))
            {
                file.Write(content);
            }

            return "Content was replaced successfully.";
        }

        [HttpPut]
        public string Put([FromBody] string content)
        {
            // If content is invalid, throw an error
            ValidateContent(content);

            // If file directory doesn't exist, create one
            if (!Directory.Exists(FILE_DIR))
            {
                Directory.CreateDirectory(FILE_DIR);
            }

            // Start content with new line
            content = Environment.NewLine + content;

            // Number identifying file
            var fileNumber = 1;

            // Flag if the line was inserted
            var lineWasInserted = false;

            while (!lineWasInserted)
            {
                try
                {
                    // Check if file is not larger than 256 bytes
                    var fileInfo = new FileInfo(_filePath + fileNumber);
                    var fileSize = (int)fileInfo.Length;

                    // If the file is too large, move to the next one
                    if (fileSize >= 256)
                    {
                        fileNumber++;
                        continue;
                    }
                    else
                    {
                        // Check the size of the input
                        var inputLength = content.Length + fileSize;

                        // If it's too large, we take as many characters as we can and put the rest in the next one
                        if (inputLength > 256)
                        {
                            // Measure how many characters will fit in
                            var length = inputLength % 256;
                            length = content.Length - length;

                            // Get the part that still fits in
                            var firstPart = content.Substring(0, length);

                            // Put in the part of the content that still fits in
                            using (var f = new StreamWriter(_filePath + fileNumber, true))
                            {
                                f.Write(firstPart);
                            }

                            fileNumber++;

                            // Save the characters we haven't put in yet
                            content = content.Substring(length);
                        }

                        // Save the WHOLE content inside EXISTING file 
                        // or the REST of the content inside a NEW file
                        using (var outputFile = new StreamWriter(_filePath + fileNumber, true))
                        {
                            outputFile.Write(content);
                        }

                        lineWasInserted = true;
                    }
                }
                catch (FileNotFoundException e)
                {
                    // File was not found, so create new file and add content to it
                    using (var file = new StreamWriter(_filePath + fileNumber, true))
                    {
                        file.Write(content);
                    }

                    lineWasInserted = true;
                }
            }

            return "Content was insterted successfully.";
        }

        private static void ValidateContent(string content)
        {
            if (content.Length > 50)
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.BadRequest);
        }
    }
}