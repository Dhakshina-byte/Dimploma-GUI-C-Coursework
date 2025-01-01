using C_Coursework.Controller;
using C_Coursework.Model;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Coursework.View
{
    public partial class Manege_Flights : MaterialForm
    {
        private readonly FlightController _controller;
        private Flights _flights;

        public Manege_Flights()
        {
            InitializeComponent();
            InitializeMaterialSkin();
            _controller = new FlightController();
            _flights = new Flights();
            
        }
        private void InitializeMaterialSkin()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }
        private void refe() 
        {
            var dt = _controller.GetAllFlights();
            bunifuDataGridView1.DataSource = dt;
        }
        private void showData() 
        {
            var dt = _controller.GetAllFlights();
            bunifuDataGridView1.DataSource = dt;
            SetupDataGridView();

        }
        private void SetupDataGridView() 
        {
            bunifuDataGridView1.AllowUserToAddRows = false;
            bunifuDataGridView1.Columns[8].ReadOnly = true;
            bunifuDataGridView1.Columns[0].ReadOnly = true;
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
            bunifuDataGridView1.Columns.Insert(9, deleteButton);

            // Add Edit button column
            var editButton = new DataGridViewButtonColumn
            {
                Name = "EditButton",
                HeaderText = "Edit",
                Width = 100,
                Text = "Edit",
                UseColumnTextForButtonValue = true
            };
            bunifuDataGridView1.Columns.Insert(10, editButton);
        }
        private void Manege_Flights_Load(object sender, EventArgs e)
        {
            showData();
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9)
            {
                DataGridViewRow rows = bunifuDataGridView1.Rows[e.RowIndex]; // Get the clicked row
                if (MessageBox.Show(string.Format("Are you sure you want to delete this row?", rows.Cells["Flight_Number"].Value), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes) // Show confirmation message box
                {
                    _flights.FlightNumber = Convert.ToString(rows.Cells["Flight_Number"].Value); // Get the ID of the clicked row
                    _controller.DeleteFlight(_flights.FlightNumber); // Delete the row

                    var dt = _controller.GetAllFlights();
                    bunifuDataGridView1.DataSource = dt;

                    // Ensure the button columns are still present
                    if (bunifuDataGridView1.Columns["DeleteButton"] == null ||
                    bunifuDataGridView1.Columns["EditButton"] == null)
                    {
                        SetupDataGridView();

                    }
                    //new Manege_Flights().Show();
                    //this.Close();

                }
            }
            if (e.ColumnIndex == 10)
            {
                DataGridViewRow rows1 = bunifuDataGridView1.Rows[e.RowIndex]; // Get the clicked row
                if (MessageBox.Show(string.Format("Are you sure you want to Edit this row?", rows1.Cells["Flight_Number"].Value), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes) // Show confirmation message box
                {
                    _flights.FlightNumber = Convert.ToString(rows1.Cells["Flight_Number"].Value);
                    _flights.AirLinename = Convert.ToString(rows1.Cells["Airline_name"].Value);
                    _flights.Airplanetype = Convert.ToString(rows1.Cells["Plane_Type"].Value);
                    _flights.Eseat = Convert.ToInt32(rows1.Cells["Economy_Seats"].Value);
                    _flights.EPseat = Convert.ToInt32(rows1.Cells["Premium_Economy_Seats"].Value);
                    _flights.Bseat = Convert.ToInt32(rows1.Cells["Business_Seats"].Value);
                    _flights.Fseat = Convert.ToInt32(rows1.Cells["First_Class_Seats"].Value);
                    _flights.totalseat = _controller.CalculateTotalSeats(rows1.Cells["Economy_Seats"].Value.ToString(), rows1.Cells["First_Class_Seats"].Value.ToString(), rows1.Cells["Business_Seats"].Value.ToString(), rows1.Cells["Premium_Economy_Seats"].Value.ToString());
                    _controller.UpdateFlight(_flights);

                    var dt = _controller.GetAllFlights();
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
                string search = searchflightstxtbox.Text;
                var searchResults = _controller.SearchFlight(search);
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
    }
}
