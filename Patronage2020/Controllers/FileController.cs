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
    ///  It's responsible for responding to requests, passing input and output data through FileManagementService
    /// </summary>  
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {

        private readonly IFileManagementService fileManager;

        /// <summary>
        /// Initializes a new instance of the FileController class
        /// </summary>
        /// <param name="fileManager">Service managing operations on files and data validation</param>
        public FileController(IFileManagementService fileManager)
        {
            this.fileManager = fileManager;
        }

        /// <summary>
        /// Handles a GET request - responds with files' content
        /// </summary>
        /// <returns>Content of all data files</returns>
        [HttpGet]
        public string Get()
        {
            return fileManager.ReadContent();
        }

        /// <summary>
        /// Handles a POST request - overwrites all content
        /// </summary>
        /// <param name="content">Content to overwrite with</param>
        /// <returns>True if everything went correctly</returns>
        /// <exception cref="System.Web.Http.HttpResponseException">Thrown when data doesn't pass the validation</exception>
        [HttpPost]
        public bool Post([FromBody] string content)
        {
            return fileManager.OverwriteContent(content);
        }

        /// <summary>
        /// Handles a POST request - puts content in the end of the file as a new line
        /// </summary>
        /// <param name="content">Content we want to put</param>
        /// <returns>True if everything went correctly</returns>
        /// <exception cref="System.Web.Http.HttpResponseException">Thrown when data doesn't pass the validation</exception>
        [HttpPut]
        public bool Put([FromBody] string content)
        {
            return fileManager.AddContent(content);
        }
    }
}