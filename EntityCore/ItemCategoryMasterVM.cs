using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityCore
{
   public class ItemCategoryMasterVM
    {
        public Guid Id { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public String CategoryName { get; set; }
        public Guid CategoryId { get; set; }

    }
}
