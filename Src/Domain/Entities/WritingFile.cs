using System;
using System.Collections.Generic;
using System.Text;
using Patronage2020.Domain.Common;

namespace Patronage2020.Domain.Entities
{
    public class WritingFile : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
