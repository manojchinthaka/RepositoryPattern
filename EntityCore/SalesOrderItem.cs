using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityCore
{
    public class SalesOrderItem
    {
        public Guid Id { get; set; }
        public SalesOrder OrderId { get; set; }
        public ItemMaster Id { get; set; }
        public decimal Price { get; set; }
        




    }
}
