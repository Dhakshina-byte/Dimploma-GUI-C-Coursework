using C_Coursework.Controller;
using C_Coursework.Model;
using C_Coursework.View;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Windows.Forms;

namespace C_Coursework
{
    public partial class Mange_Customers_form : MaterialForm
    {
        private CustomerController _controller;
        private Customer _customer;

        public Mange_Customers_form() 
        {
            InitializeComponent(); 
            InitializeMaterialSkin(); 
            _controller = new CustomerController();
            _customer = new Customer();
        }
        private void InitializeMaterialSkin()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        // Method to display data in the DataGridView
        private void DisplayData()
        {
            var dt = _controller.GetAllCustomers();
            bunifuDataGridView1.DataSource = dt;
            SetupDataGridView();
        }
        private void SetupDataGridView()
        {
            bunifuDataGridView1.AllowUserToAddRows = false;
            bunifuDataGridView1.Columns[0].ReadOnly = true;
            bunifuDataGridView1.Columns[7].ReadOnly = true;
            AddButtonColumns();
        }
        private void AddButtonColumns()
        {
            // Add Delete button column
            var deleteButton = new DataGridViewButtonColumn
            {
                Name = "DeleteButton",
                HeaderText = "Delete",
                Width = 100,
                Text = "Delete",
                UseColumnTextForButtonValue = true
            };
            bunifuDataGridView1.Columns.Insert(11, deleteButton);

            // Add Edit button column
            var editButton = new DataGridViewButtonColumn
            {
                Name = "EditButton",
                HeaderText = "Edit",
                Width = 100,
                Text = "Edit",
                UseColumnTextForButtonValue = true
            };
            bunifuDataGridView1.Columns.Insert(12, editButton);
        }

        private void Mange_Customers_form_Load(object sender, EventArgs e)
        {
            DisplayData();
            searchtxtbox.Text = "Search";
        }

        // Event handler for cell content click in DataGridView
        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 11) // Check if Delete button column is clicked
            {
                DataGridViewRow rows = bunifuDataGridView1.Rows[e.RowIndex]; // Get the clicked row
                if (MessageBox.Show(string.Format("Are you sure you want to delete this row?", rows.Cells["ID"].Value), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes) // Show confirmation message box
                {
                    _customer.customerId = Convert.ToInt32(rows.Cells["ID"].Value); // Get the ID of the clicked row
                    _controller.DeleteCustomer(_customer.customerId);
                    new Mange_Customers_form().Show();
                    this.Close();

                    var dt = _controller.GetAllCustomers();
                    bunifuDataGridView1.DataSource = dt;

                    // Ensure the button columns are still present
                    if (bunifuDataGridView1.Columns["DeleteButton"] == null ||
                   bunifuDataGridView1.Columns["EditButton"] == null)
                    {
                        SetupDataGridView();

                    }
                }
            }
            if (e.ColumnIndex == 12) // Check if Edit button column is clicked
            {
                DataGridViewRow rows1 = bunifuDataGridView1.Rows[e.RowIndex]; // Get the clicked row
                if (MessageBox.Show(string.Format("Are you sure you want to Edit this row?", rows1.Cells["ID"].Value), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _customer.customerId = Convert.ToInt32(rows1.Cells["ID"].Value);
                    _customer.FirstName = Convert.ToString(rows1.Cells["First_Name"].Value);
                    _customer.LastName = Convert.ToString(rows1.Cells["Last_Name"].Value);
                    _customer.Email = Convert.ToString(rows1.Cells["Email"].Value);
                    _customer.Phone = Convert.ToInt32(rows1.Cells["Phone_No"].Value);
                    _customer.City = Convert.ToString(rows1.Cells["City"].Value);
                    _customer.Title = Convert.ToString(rows1.Cells["Title"].Value);
                    _customer.Address = Convert.ToString(rows1.Cells["cAddress"].Value);
                    _customer.Postal = Convert.ToInt32(rows1.Cells["Postal_code"].Value);
                    _customer.Country = Convert.ToString(rows1.Cells["Country"].Value);
                    _controller.UpdateCustomer(_customer);

                    var dt = _controller.GetAllCustomers();
                    bunifuDataGridView1.DataSource = dt;

                    // Ensure the button columns are still present
                    if (bunifuDataGridView1.Columns["DeleteButton"] == null ||
                   bunifuDataGridView1.Columns["EditButton"] == null)
                    {
                        SetupDataGridView();

                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string search = searchtxtbox.Text;
                var searchResults = _controller.SearchCustomer(search);
                bunifuDataGridView1.DataSource = searchResults;

                // Ensure the button columns are still present
                if (bunifuDataGridView1.Columns["DeleteButton"] == null ||
                    bunifuDataGridView1.Columns["EditButton"] == null)
                {
                    SetupDataGridView();
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error performing search: {ex.Message}", "Search Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnExitProgram_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void searchtxtbox_Click(object sender, EventArgs e)
        {
            searchtxtbox.Text = "";
        }
    }
}
