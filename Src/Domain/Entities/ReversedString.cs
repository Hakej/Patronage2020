using System;
using System.Collections.Generic;
using System.Text;
using Patronage2020.Domain.Common;

namespace Patronage2020.Domain.Entities
{
    public class ReversedString : AuditableEntity
    {
        public string Content { get; set; }
    }
}
