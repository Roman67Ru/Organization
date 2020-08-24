using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Data
{
    public class Office
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("HouseId")]
        public virtual House House { get; set; }
        public int? HouseId { get; set; }
    }
}
