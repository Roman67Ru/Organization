using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        [ForeignKey("OfficeId")]
        public virtual Office Office { get; set; }
        public int? OfficeId { get; set; }
    }
}
