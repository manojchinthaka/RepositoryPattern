using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace EntityCore
{
    public class ItemCategory
    {
        
        public Guid Id { get; set; }
        public String Name { get; set; }
        public IEnumerable<ItemMaster> ItemMaster { get; set; }
    }
}
