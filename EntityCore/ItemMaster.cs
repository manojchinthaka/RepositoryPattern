using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace EntityCore
{
    public class ItemMaster
    {
 
        public Guid Id { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string imagePath { get; set; }     
        public Guid ItemCategoryId { get; set; }

       [ForeignKey("ItemCategoryId")]
        public ItemCategory ItemCategory { get; set; }
    }
}
