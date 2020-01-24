using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Patronage2020.Application.Common.Exceptions
{
    public class WritingFileNotFoundException : Exception
    {
        private int id;

        public WritingFileNotFoundException(int id)
            : base($"Requested file with id: \"{id}\" was not found.")
        {
        }

        public WritingFileNotFoundException(int id, Exception ex)
            : base($"Requested file with id: \"{id}\" was not found.", ex)
        {
        }
    }
}
