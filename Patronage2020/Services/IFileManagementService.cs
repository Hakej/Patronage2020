using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patronage2020
{
    public interface IFileManagementService
    {
        /// <summary>
        /// Reads all content contained in data
        /// </summary>
        /// <returns>Content of all data</returns>
        public string ReadContent();

        /// <summary>
        /// Overwrites all content with a new one
        /// </summary>
        /// <param name="content">Content to overwrite with</param>
        /// <returns>True if everything went correctly</returns>
        /// <exception cref="System.Web.Http.HttpResponseException">Thrown when data doesn't pass the validation</exception>
        public bool OverwriteContent(string content);

        /// <summary>
        /// Adds content as a new line at the end of the file
        /// </summary>
        /// <param name="content">Content we want to add</param>
        /// <returns>True if everything went correctly</returns>
        /// <exception cref="System.Web.Http.HttpResponseException">Thrown when data doesn't pass the validation</exception>
        public bool AddContent(string content);

        /// <summary>
        /// Checks if data is valid to save in the files
        /// </summary>
        /// <param name="content">Data to validate</param>
        /// <exception cref="System.Web.Http.HttpResponseException">Thrown when data is invalid</exception>
        public void ValidateContent(string content);
    }
}
