using AnyCompany.Repository;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnyCompany
{
    public class OrderRepository
    {
        public List<Order> GetOrdersByCustomer(int CustomerID)
        {
            List<Order> Orders = new List<Order>();

            DatabaseHelper ConnStr = new DatabaseHelper();
            SqlConnection connection = new SqlConnection(ConnStr.ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Orders WHERE CustomerID = @CustomerID", connection);
            command.Parameters.AddWithValue("@CustomerID", CustomerID);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Orders.Add(new Order
                {
                    Amount = double.Parse(reader["Amount"].ToString()),
                    VAT = double.Parse(reader["VAT"].ToString()),
                    CustomerID = int.Parse(reader["CustomerID"].ToString()),
                    OrderId = int.Parse(reader["OrderId"].ToString())
                });
            }

            connection.Close();
            return Orders;
        }

        public void Save(Order order)
        {
            DatabaseHelper ConnStr = new DatabaseHelper();
            SqlConnection connection = new SqlConnection(ConnStr.ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Orders VALUES (@CustomerID, @Amount, @VAT)", connection);
            command.Parameters.AddWithValue("@CustomerID", order.CustomerID);
            command.Parameters.AddWithValue("@Amount", order.Amount);
            command.Parameters.AddWithValue("@VAT", order.VAT);

            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
