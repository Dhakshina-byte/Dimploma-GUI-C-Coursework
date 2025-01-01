using C_Coursework.Model;
using C_Coursework.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Coursework.Controller
{
    public class CustomerController
    {
        private readonly customer_Service _dbContext;
        public CustomerController()
        {
            _dbContext = new customer_Service();
        }
        public void SaveCustomer(Customer customer)
        {
            try
            {
                _dbContext.InsertCustomer(customer);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving customer: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DataTable GetAllCustomers()
        {
            return _dbContext.GetAllCustomers();
        }

        public void DeleteCustomer(int customerId)
        {
            _dbContext.DeleteCustomer(customerId);
        }

        public void UpdateCustomer(Customer customer)
        {
            _dbContext.UpdateCustomer(customer);
        }

        public DataTable SearchCustomer(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return GetAllCustomers();
            }
            return _dbContext.SearchCustomer(search);
        }

        public DataTable LoadserchCustomer()
        {
            return _dbContext.LoadserchCustomer();
        }

        public DataTable AutoserchCustomer(string Search)
        {
            return _dbContext.AutoSearchCustomers(Search);
        }

    }
}
