using EducationCore.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationCore.Data.Entities
{
    public class Course : BaseEntity
    {
        public string? Name { get; set; }
        public double? Price { get; set; }
    }
}