using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Dto
{
   public class CustomerOrders
    {
        public struct OrdersByCustomer
        {
            public int CustomerID { get; set; }
            public string Country { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Name { get; set; }
            public List<Order> Orders { get; set; }
        }
        
    }
}
