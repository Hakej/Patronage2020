using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private static string _errMsg = "Content cannot be larger than 50 characters.";
        private static string _fileDir = "Data/";
        private static string _fileName = "PatronageFile";
        private static string _filePath = _fileDir + _fileName;

        private readonly ILogger<FileController> _logger;

        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            var content = "";

            // If file directory doesn't exist, create one
            if (!Directory.Exists(_fileDir))
            {
                Directory.CreateDirectory(_fileDir);
            }

            // Concatenate all files into single string
            var di = new DirectoryInfo(_fileDir);
            foreach (FileInfo fi in di.GetFiles())
            {                        
                using StreamReader file = new StreamReader(fi.FullName);
                content += file.ReadToEnd();
            }

            return content;
        }

        [HttpPost]
        public string Post([FromBody] string content)
        {
            // If content is not valid, return an error
            if (!IsContentValid(content))
            {
                _logger.LogError(_errMsg);
                return _errMsg;
            }

            // If file directory doesn't exist, create one
            if (!Directory.Exists(_fileDir))
            {
                Directory.CreateDirectory(_fileDir);
            }

            // Delete all existing data
            var di = new DirectoryInfo(_fileDir);            
            foreach (FileInfo fi in di.GetFiles())
            {
                fi.Delete();
            }

            // Create new file and insert content
            using StreamWriter file = new StreamWriter(_filePath + 1);
            file.Write(content);
            file.Close();

            return "Content was replaced successfully.";
        }

        [HttpPut]
        public string Put([FromBody] string content)
        {
            // If content is not valid, return an error
            if (!IsContentValid(content))
            {
                _logger.LogError(_errMsg);
                return _errMsg;
            }

            // If file directory doesn't exist, create one
            if (!Directory.Exists(_fileDir))
            {
                Directory.CreateDirectory(_fileDir);
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
                    FileInfo fi = new FileInfo(_filePath + fileNumber);
                    var fileSize = (int)fi.Length;

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
                            using StreamWriter f = new StreamWriter(_filePath + fileNumber, true);
                            f.Write(firstPart);
                            f.Close();
                            fileNumber++;

                            // Save the characters we haven't put in yet
                            content = content.Substring(length);
                        }

                        // Save the WHOLE content inside EXISTING file 
                        // or the REST of the content inside a NEW file
                        using StreamWriter file = new StreamWriter(_filePath + fileNumber, true);
                        file.Write(content);
                        file.Close();
                        lineWasInserted = true;
                    }
                }
                catch (FileNotFoundException e)
                {
                    // File was not found, so create new file and add content to it
                    using StreamWriter file = new StreamWriter(_filePath + fileNumber, true);
                    file.Write(content);
                    file.Close();
                    lineWasInserted = true;
                }
            }

            return "Content was insterted successfully";
        }

        private static bool IsContentValid(string content)
        {
            return (content.Length <= 50);
        }
    }
}