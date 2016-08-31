using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityCore
{
    public class Cart
    {
        [Key]
        public Guid RecordId { get; set; }
        public string CartId { get; set; }
        public Guid ItemId { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }

        [ForeignKey("ItemId")]
        public virtual ItemMaster ItemMaster { get; set; }
    }
}
