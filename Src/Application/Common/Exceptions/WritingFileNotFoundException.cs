using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Patronage2020.Application.Common.Exceptions
{
    public class WritingFileNotFoundException : Exception
    {
        public WritingFileNotFoundException(int id, Exception ex)
            : base($"File with id: \"{id}\" was not found.", ex)
        {
        }
    }
}
