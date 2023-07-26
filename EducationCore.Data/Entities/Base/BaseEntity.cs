using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationCore.Data.Entities.Base
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public string? Creator { get; set; }

        public DateTime CreationTime { get; set; }

        public string? LastModifier { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public string? ModifyDescription { get; set; }
        public string? DeletionDescription { get; set; }

        public bool IsDeleted { get; set; }
    }
}