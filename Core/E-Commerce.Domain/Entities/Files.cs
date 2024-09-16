using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Entities.Common;

namespace E_Commerce.Domain.Entities
{
    public class Files : BaseEntitiy
    {

        public string FileName { get; set; }
        public string Path { get; set; }
        public string? Storage { get; set; }

        [NotMapped] // Bu isaretleyiciyle UpdaDate cloumunu migrate etmesini engeledik.
        public override DateTime UpdateDate { get => base.UpdateDate; set => base.UpdateDate = value; }
    }
}
