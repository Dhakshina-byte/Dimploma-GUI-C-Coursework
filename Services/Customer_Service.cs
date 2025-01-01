using C_Coursework.Controller;
using C_Coursework.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Coursework.Services
{
    public class customer_Service
    {
        private readonly SqlConnection connection;

        public customer_Service()
        {
            connection = DatabaseConnection.GetConnection();
        }
        public void InsertCustomer(Customer customer)
        {
            try
            {
                connection.Close();
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Customers(First_Name,Last_Name,Phone_No,Title,Email) VALUES (@FirstName,@LastName,@Phone,@Title,@Email)", connection))
                {
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@Title", customer.Title);
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand("INSERT INTO CustomerAddress(Country,City,Postal_code,cAddress) VALUES (@Country,@City,@Postal,@Address)", connection))
                {
                    cmd.Parameters.AddWithValue("@Country", customer.Country);
                    cmd.Parameters.AddWithValue("@City", customer.City);
                    cmd.Parameters.AddWithValue("@Postal", customer.Postal);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
        public DataTable GetAllCustomers()
        {
            {
                string query = "SELECT * FROM Customers INNER JOIN CustomerAddress ON Customers.ID = CustomerAddress.Customer_ID;";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public void DeleteCustomer(int customerId)
        {
            {
                connection.Close();
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Customers WHERE ID = @ID", connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@ID", customerId);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("DELETE FROM CustomerAddress WHERE Customer_ID = @ID", connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@ID", customerId);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            {
                connection.Close();
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string customerQuery = "Update Customers set First_Name=@Fname,Last_Name=@Lname,Phone_No=@PNo,Title=@title,Email=@Email where ID = @ID;";
                        using (SqlCommand cmd = new SqlCommand(customerQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@ID", customer.customerId);
                            cmd.Parameters.AddWithValue("@Fname", customer.FirstName);
                            cmd.Parameters.AddWithValue("@Lname", customer.LastName);
                            cmd.Parameters.AddWithValue("@Email", customer.Email);
                            cmd.Parameters.AddWithValue("@PNo", customer.Phone);
                            cmd.Parameters.AddWithValue("@title", customer.Title);
                            cmd.ExecuteNonQuery();
                        }

                        string addressQuery = "Update CustomerAddress set Country=@country,City=@city,Postal_code=@postal,cAddress=@cadd where Customer_ID = @ID;";
                        using (SqlCommand cmd = new SqlCommand(addressQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@ID", customer.customerId);
                            cmd.Parameters.AddWithValue("@Country", customer.Country);
                            cmd.Parameters.AddWithValue("@City", customer.City);
                            cmd.Parameters.AddWithValue("@Postal", customer.Postal);
                            cmd.Parameters.AddWithValue("@cadd", customer.Address);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public DataTable SearchCustomer(string search)
        {
            
           string query = "SELECT * FROM Customers INNER JOIN CustomerAddress ON Customers.ID = CustomerAddress.Customer_ID WHERE First_Name LIKE @search OR Last_Name LIKE @search OR Phone_No LIKE @search OR Title LIKE @search OR Email LIKE @search OR Country LIKE @search OR City LIKE @search OR Postal_code LIKE @search OR cAddress LIKE @search;";
           using (SqlCommand cmd = new SqlCommand(query, connection))
           {
                    cmd.Parameters.AddWithValue("@search", "%" + search+ "%");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
           } 
        }
        public DataTable LoadserchCustomer()
        {
            string query = "SELECT ID ,First_Name FROM Customers;";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public DataTable AutoSearchCustomers(string Search)
        {
            string query = "SELECT ID, First_Name FROM Customers WHERE ID LIKE @ID OR First_Name LIKE @First_Name;";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ID", "%" + Search + "%");
                cmd.Parameters.AddWithValue("@First_Name", "%" + Search + "%");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
