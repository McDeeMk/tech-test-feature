using AnyCompany.Repository;

namespace AnyCompany.services
{
    public class OrderService 
    {
     
        private readonly OrderRepository orderRepository = new OrderRepository();

        public bool PlaceOrder(Order order, int customerId)
        {
            Customer customer = CustomerRepository.GetCustomerByID(customerId);

            if (order.Amount == 0)
                return false;

            if (customer.Country == "UK")
                order.VAT = 0.2d;
            else
                order.VAT = 0;

            orderRepository.Save(order);

            return true;
        }
    }
}
