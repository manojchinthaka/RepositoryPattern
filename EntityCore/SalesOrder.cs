using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityCore
{
    public class SalesOrder
    {
        public Guid Id { get; set; }
        public string OrderNo { get; set; }
        public Customer CustomerId { get; set; }
        public DateTime OrderDate { get; set; }


    }
}
