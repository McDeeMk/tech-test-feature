using AnyCompany.Repository;
using System.Collections.Generic;
using static AnyCompany.Dto.CustomerOrders;

namespace AnyCompany.services
{
    public class CustomerService
    { 
        public bool SaveCustomer(Customer customer,out int CustomerID)
        {
             CustomerRepository.Save(customer,out CustomerID);
             return true;
        }

        public List<OrdersByCustomer> GetOrdersByCustomer()
        {
           return CustomerRepository.GetOrdersByCustomer();
        }
    }
}
