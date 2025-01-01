using C_Coursework.Controller;
using C_Coursework.Model;
using ComponentFactory.Krypton.Toolkit;
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
    public partial class Manege_Flight_Shedule : MaterialForm
    {
        private readonly Flight_Schedule_Controller _controller;
        private Flight_Schedule flight_Schedule;
        public Manege_Flight_Shedule()
        {
            InitializeComponent();
            InitializeMaterialSkin();
            _controller = new Flight_Schedule_Controller();
            flight_Schedule = new Flight_Schedule();

        }
        private void InitializeMaterialSkin()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new MaterialSkin.ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }
        private void showData()
        {
            var dt = _controller.GetAllSchedule();
            bunifuDataGridView1.DataSource = dt;
        }
        private void LoadFlightNumbers()
        {
            var flightSchedules = _controller.Getflightnumber();
            foreach (DataRow row in flightSchedules.Rows)
            {
                Flight_numbercombobox.Items.Add(row["Flight_Number"].ToString());
            }
        }

        private void Manege_Flight_Shedule_Load(object sender, EventArgs e)
        {
            MSHdepartdatebox.MinDate = DateTime.Now;
            MSHreturndatebox.MinDate = DateTime.Now;
            showData();
            LoadFlightNumbers();
            bunifuDataGridView1.Columns[0].ReadOnly = true;
            bunifuDataGridView1.Columns[1].ReadOnly = true;
            bunifuDataGridView1.Columns[2].ReadOnly = true;
            bunifuDataGridView1.Columns[3].ReadOnly = true;
            bunifuDataGridView1.Columns[4].ReadOnly = true;
            bunifuDataGridView1.Columns[5].ReadOnly = true;
        }

        private void ADDbtn_Click(object sender, EventArgs e)
        {
            flight_Schedule.FlightNumber = Flight_numbercombobox.Text;
            flight_Schedule.DepartureDate = MSHdepartdatebox.Text;
            flight_Schedule.Departure = MSHfromcoombobox.Text;
            flight_Schedule.Arrival = MSHtocoombobox.Text;
            flight_Schedule.ArrivalDate = MSHreturndatebox.Text;
            flight_Schedule.Status = Statuscombobox.Text;

            _controller.SaveSchedule(flight_Schedule);
            showData();
        }

        private void bunifuDataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
                //DataGridViewRow row = bunifuDataGridView1.Rows[e.RowIndex];
                //flight_Schedule.FlightNumber = Convert.ToString(row.Cells["Flight_Number"].Value);

        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            flight_Schedule.FlightNumber = Flight_numbercombobox.Text;
            flight_Schedule.DepartureDate = MSHdepartdatebox.Text;
            flight_Schedule.ArrivalDate = MSHreturndatebox.Text;
            flight_Schedule.Status = Statuscombobox.Text;

            _controller.UpdateSchedule(flight_Schedule);
            showData();
        }

    }
}
