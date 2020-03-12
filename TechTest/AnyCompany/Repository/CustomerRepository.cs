using AnyCompany.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using static AnyCompany.Dto.CustomerOrders;

namespace AnyCompany
{
    public static class CustomerRepository 
    {   
        public static Customer GetCustomerByID(int CustomerID)
        {
            Customer customer = new Customer();
            DatabaseHelper ConnStr = new DatabaseHelper();
            SqlConnection connection = new SqlConnection(ConnStr.ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE CustomerID = @CustomerID", connection);
            command.Parameters.AddWithValue("@CustomerID", CustomerID);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                customer.Country = reader["Country"].ToString();
                customer.CustomerID = CustomerID;
                customer.DateOfBirth = DateTime.Parse( reader["DateOfBirth"].ToString());
                customer.Name = reader["Name"].ToString();
            }

            connection.Close();
            return customer;
        }

        public static List<OrdersByCustomer> GetOrdersByCustomer()
        {
            List<OrdersByCustomer> ordersByCustomer = new List<OrdersByCustomer>();
            DatabaseHelper ConnStr = new DatabaseHelper();
            SqlConnection connection = new SqlConnection(ConnStr.ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Customer", connection);
            var reader = command.ExecuteReader();
            OrderRepository order = new OrderRepository();
            while (reader.Read())
            {
                ordersByCustomer.Add(new OrdersByCustomer
                {
                    Country = reader["Country"].ToString(),
                    CustomerID = int.Parse(reader["CustomerID"].ToString()),
                    DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                    Name = reader["Name"].ToString(),
                    Orders = order.GetOrdersByCustomer(int.Parse(reader["CustomerID"].ToString()))

                });
            }

            connection.Close();
            return ordersByCustomer;
        }

        public static void Save(Customer Customer, out int CustomerID)
        {
            DatabaseHelper ConnStr = new DatabaseHelper();
            SqlConnection connection = new SqlConnection(ConnStr.ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Customer VALUES (@Name, @DateOfBirth, @Country); select SCOPE_IDENTITY()", connection);
            command.Parameters.AddWithValue("@Name", Customer.Name);
            command.Parameters.AddWithValue("@DateOfBirth", Customer.DateOfBirth);
            command.Parameters.AddWithValue("@Country", Customer.Country);

            CustomerID = int.Parse(command.ExecuteScalar().ToString());

            connection.Close();
        }
    }
}
