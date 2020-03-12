using AnyCompany.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AnyCompany.Dto.CustomerOrders;

namespace AnyCompany.Repository
{
    public class DatabaseHelper 
    {
        private  SqlConnection connection;
        private  SqlCommand command;
        private StringBuilder SqlString;

        public string ConnectionString {get; set;}
        public DatabaseHelper()
        {
            ConnectionString = Properties.Settings.Default.ConnStr;
            if (!string.IsNullOrEmpty(ConnectionString))
            {
                connection = new SqlConnection(ConnectionString);
                if (OpenConnection())
                { 
                    command = connection.CreateCommand();
                    command.CommandType = System.Data.CommandType.Text;
                    CreateCustomerTable();
                    CreateOrderTable();
                }
            }
        }
        private bool OpenConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
                return true;
            }
            return false;
        }

        private bool CloseConnection()
        {
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
                return true;
            }
            return false;
        }

        private void CreateCustomerTable()
        {
            SqlString = new StringBuilder();
            SqlString.Append("CREATE TABLE Customer");
            SqlString.Append("(");
            SqlString.Append("CustomerID int identity not null primary key,");
            SqlString.Append("Country varchar(100) not null unique,");
            SqlString.Append("DateOfBirth datetime default(getdate()),");
            SqlString.Append("[Name] varchar(200) not null");
            SqlString.Append(")");
            command.CommandText = SqlString.ToString();
            try
            {
                OpenConnection();
                command.ExecuteNonQuery();
            }
            catch (Exception)
            { }
            finally
            {
                CloseConnection();
            }
        }

        private void CreateOrderTable()
        {
            SqlString = new StringBuilder();
            SqlString.Append("CREATE TABLE Orders");
            SqlString.Append("(");
            SqlString.Append("OrderId  int identity not null primary key,");
            SqlString.Append("CustomerID  int not null foreign key references Customer(CustomerID),");
            SqlString.Append("Amount decimal (19,0) not null,");
            SqlString.Append("VAT decimal (19,0) not null");
            SqlString.Append(")");
            command.CommandText = SqlString.ToString();
            try
            {
                OpenConnection();
                command.ExecuteNonQuery();
            }
            catch (Exception)
            { }
            finally
            {
                CloseConnection();
            }
        }
    }
}
