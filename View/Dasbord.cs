using C_Coursework.Controller;
using C_Coursework.Model;
using C_Coursework.Services;
using C_Coursework.View;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace C_Coursework
{
    public partial class Dasbord : MaterialForm
    {
        private readonly CustomerController _controller;
        private readonly FlightController _flightController;
        private readonly Flight_Schedule_Controller _scheduleController;
        private readonly PassengerPriceCalculator passengerPriceCalculator;
        private readonly PriceCalculation _priceCalculation;
        private readonly Booking_Controller _bookingController;
        private readonly FlightBookingBilling_Controller flightBookingBilling_Controller;
        private Customer _customer;
        private Flights _flights;
        private Flight_Schedule _schedule;
        private Booking _Booking;
        private FlightBookingBilling bookingBilling;


        public Dasbord()
        {
            InitializeComponent();
            _controller = new CustomerController();
            _flightController = new FlightController();
            _scheduleController = new Flight_Schedule_Controller();
            _bookingController = new Booking_Controller();
            flightBookingBilling_Controller = new FlightBookingBilling_Controller();
            _Booking = new Booking();
            _customer = new Customer();
            _flights = new Flights();
            _schedule = new Flight_Schedule();
            bookingBilling = new FlightBookingBilling();

            InitializeMaterialSkin();
        }
        private void InitializeMaterialSkin()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800,
                Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200,
                TextShade.WHITE);
        }
        private void loadCustomers()
        {

            var dt = _controller.LoadserchCustomer();
            bunifuDataGridView2.DataSource = dt;

        }
        private void paymenthistory()
        {
            var dt = flightBookingBilling_Controller.GetFlightBookingBilling();
            kryptonDataGridView1.DataSource = dt;
        }

        private void showSchedule()
        {
            var dt = _scheduleController.GetAllSchedule();
            bunifuDataGridView1.DataSource = dt;
            bunifuDataGridView1.AllowUserToAddRows = false;
            bunifuDataGridView1.Columns[0].ReadOnly = true;
            bunifuDataGridView1.Columns[1].ReadOnly = true;
            bunifuDataGridView1.Columns[2].ReadOnly = true;
            bunifuDataGridView1.Columns[3].ReadOnly = true;
            bunifuDataGridView1.Columns[4].ReadOnly = true;
            bunifuDataGridView1.Columns[5].ReadOnly = true;
            ADDbutton();
        }
        private void ADDbutton() 
        {
            var selectButton = new DataGridViewButtonColumn
            {
                Name = "selectButton",
                HeaderText = "select",
                Width = 100,
                Text = "select",
                UseColumnTextForButtonValue = true
            };
            bunifuDataGridView1.Columns.Insert(6, selectButton);
        }

        private void hideflightlabels()
        {
            AirplaneLabel.Visible = false;
            Ariplanenolabel.Visible = false;
            Tolocationlable.Visible = false;
            totimelabel.Visible = false;
          //  FlightDurationlabel.Visible = false;
            Fromlocationlabel.Visible = false;
            fromtimelable.Visible = false;
            pricelabel.Visible = false;
            materialLabel6.Visible = false;
        }
        public void hidecustomerlabels() 
        {
            CustIDlable.Visible = false;
            CustNamelable.Visible = false;
        }
        public void showcustomerlabels()
        {
            CustIDlable.Visible = true;
            CustNamelable.Visible = true;
        }
        private void showflightlabels()
        {
            AirplaneLabel.Visible = true;
            Ariplanenolabel.Visible = true;
            Tolocationlable.Visible = true;
            totimelabel.Visible = true;
           // FlightDurationlabel.Visible = true;
            Fromlocationlabel.Visible = true;
            fromtimelable.Visible = true;
            pricelabel.Visible = true;
        }

        private void Dasbord_Load(object sender, EventArgs e)
        {
            Retundatebox.Enabled = materialCheckbox2.Checked;
            materialCheckbox1.Checked = !materialCheckbox2.Checked;
            Departdatebox.MinDate = DateTime.Now;
            Retundatebox.MinDate = DateTime.Now;
            CHILDRENtxtbox.Text = "0";
            ADULTStxtbox.Text = "1";
            INFANTStxtbox.Text = "0";
            Ecoseatstxtbox.Text = "0";
            hideflightlabels();
            showSchedule();
            loadCustomers();
            hidecustomerlabels();
            usersearchtxtbox.Text = "Search";
            paymenthistory();

        }
        private void materialCheckbox2_CheckedChanged(object sender, EventArgs e)
        {
            // Enable or disable the return date box based on the checkbox state
            Retundatebox.Enabled = materialCheckbox2.Checked;
            materialCheckbox1.Checked = !materialCheckbox2.Checked;
        }
        private void materialCheckbox1_CheckedChanged(object sender, EventArgs e)
        {
            // Ensure only one of the checkboxes is checked at a time
            materialCheckbox2.Checked = !materialCheckbox1.Checked;
        }
        private void tabPage9_Click(object sender, EventArgs e)
        {
            // Close the current form and show the login form
            this.Close();
            Login login = new Login();
            login.Show();
        }
        private void materialButton4_Click(object sender, EventArgs e)
        {
            // Event handler for button4 click event
            if (int.TryParse(ADULTStxtbox.Text, out int value))
            {
                if (value < 9)
                {
                    ADULTStxtbox.Text = (value + 1).ToString();
                }
            }
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            // Event handler for button3 click event
            if (int.TryParse(ADULTStxtbox.Text, out int value))
            {
                if (value > 1)
                {
                    ADULTStxtbox.Text = (value - 1).ToString();

                }
            }
        }

        private void materialTextBox1_TextChanged(object sender, EventArgs e)
        {
            // Validate that the input in the ADULTStxtbox is numeric
            if (!System.Text.RegularExpressions.Regex.IsMatch(ADULTStxtbox.Text, "^[0-9]$"))
            {
                ADULTStxtbox.Text = string.Empty;
            }
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            // Switch to the fourth tab in the tab control
            materialTabControl1.SelectedIndex = 1;
            SHtocoombobox.SelectedItem = tocombobox.SelectedItem;
            SHfromcoombobox.SelectedItem = fromcoombobox.SelectedItem;
            SHdepartdatebox.Value = Departdatebox.Value;
            SHreturndatebox.Value = Retundatebox.Value;
            SHreturndatebox.Enabled = materialCheckbox2.Checked;

        }

        private void CHILDRENtxtbox_TextChanged(object sender, EventArgs e)
        {
            // Validate that the input in the CHILDRENtxtbox is a single digit number
            if (!System.Text.RegularExpressions.Regex.IsMatch(CHILDRENtxtbox.Text, "^[0-9]$"))
            {
                CHILDRENtxtbox.Text = string.Empty;
            }
        }

        private void INFANTStxtbox_TextChanged(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(INFANTStxtbox.Text, "^[0-9]$"))
            {
                INFANTStxtbox.Text = string.Empty;
            }
        }

        private void infantmaxbtn_Click(object sender, EventArgs e)
        {
            // Console.WriteLine(INFANTStxtbox);
            if (int.TryParse(INFANTStxtbox.Text, out int value))
            {

                if (value < 9)
                {
                    INFANTStxtbox.Text = (value + 1).ToString();

                }
            }

        }

        private void chidrenminbtn_Click(object sender, EventArgs e)
        {
            if (int.TryParse(CHILDRENtxtbox.Text, out int value))
            {
                if (value > 0)
                {
                    CHILDRENtxtbox.Text = (value - 1).ToString();

                }
            }
        }

        private void childrenmaxbtn_Click(object sender, EventArgs e)
        {
            if (int.TryParse(CHILDRENtxtbox.Text, out int value))
            {

                if (value < 9)
                {
                    CHILDRENtxtbox.Text = (value + 1).ToString();


                }
            }
        }

        private void infantminbtn_Click(object sender, EventArgs e)
        {
            if (int.TryParse(INFANTStxtbox.Text, out int value))
            {
                if (value > 0)
                {
                    INFANTStxtbox.Text = (value - 1).ToString();

                }
            }
        }
        
        private void Cancelbtn_Click(object sender, EventArgs e)
        {
            Retundatebox.Enabled = materialCheckbox2.Checked;
            materialCheckbox1.Checked = !materialCheckbox2.Checked;
            Departdatebox.Value = DateTime.Now;
            Retundatebox.Value = DateTime.Now;
            CHILDRENtxtbox.Text = "0";
            ADULTStxtbox.Text = "1";
            INFANTStxtbox.Text = "0";
            hideflightlabels();
        }

        private void paybtn_Click(object sender, EventArgs e)
        {
            materialTabControl1.SelectedIndex = 5;

            _Booking.Arrival =Tolocationlable.Text;
            _Booking.Departure = Fromlocationlabel.Text;
            _Booking.DepartureDate = totimelabel.Text;
            _Booking.ArrivalDate = fromtimelable.Text;
            _Booking.classtype = TRAVELCLASSbox.Text;
            _Booking.adultct = int.Parse(ADULTStxtbox.Text);
            _Booking.childct = int.Parse(CHILDRENtxtbox.Text);
            _Booking.infantct = int.Parse(INFANTStxtbox.Text);
           _Booking.FlightNumber = AirplaneLabel.Text;
            _Booking.totalprice = float.Parse(totalpricetxtbox.Text, System.Globalization.NumberStyles.Currency);
            _Booking.userID = int.Parse(CustIDlable.Text);

            _bookingController.SaveBooking(_Booking);

            CustomerID.Text = _Booking.userID.ToString();
            TotalTextBox.Text = _Booking.totalprice.ToString("C");
            FlightNumber.Text = _Booking.FlightNumber;

        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            Mange_Customers_form form1 = new Mange_Customers_form();
            form1.Show();
        }
        private void Ecoseatstxtbox_TextChanged(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(Ecoseatstxtbox.Text, "^[0-9]*$"))
            {
                Ecoseatstxtbox.Text = string.Empty;
            }
        }

        private void Submit1btn_Click(object sender, EventArgs e)
        {
            try
            {
                _flights.AirLinename = Airlinecombobox.Text;
                _flights.FlightNumber = Flnumtxtbox.Text;
                _flights.Airplanetype = PlanetypecomboBox.Text;
                _flights.Eseat = int.Parse(Ecoseatstxtbox.Text);
                _flights.EPseat = int.Parse(PEcoSeatstextbox.Text);
                _flights.Bseat = int.Parse(Busseatstxtbox.Text);
                _flights.Fseat = int.Parse(FClassSeatstxtbox.Text);
                _flights.totalseat = _flightController.CalculateTotalSeats(Ecoseatstxtbox.Text, FClassSeatstxtbox.Text, Busseatstxtbox.Text, PEcoSeatstextbox.Text);
                
                _flightController.SaveFlight(_flights);
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid input format. Please check your entries.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Checkbtn_Click(object sender, EventArgs e)
        {
           // _flightController.ValidateFlight(_flights);
            _flights.totalseat = _flightController.CalculateTotalSeats(Ecoseatstxtbox.Text, FClassSeatstxtbox.Text, Busseatstxtbox.Text, PEcoSeatstextbox.Text);
            Total_label.Text = _flights.totalseat.ToString();
            
        }

        private void Submitbtn_Click(object sender, EventArgs e)
        {

            try
            {
                _customer.FirstName = firstnametxtbox.Text;
                _customer.LastName = lastnametxtbox.Text;
                _customer.Email = emailtxtbox.Text;
                _customer.Phone = int.Parse(phonetxtbox.Text);
                _customer.City = citytxtbox.Text;
                _customer.Title = titletxtbox.Text;
                _customer.Address = addresstxtbox.Text;
                _customer.Postal =  int.Parse(postaltxtbox.Text);
                _customer.Country = countrycombobox.Text;



                _controller.SaveCustomer(_customer);
                materialTabControl1.SelectedIndex = 2;
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid input format. Please check your entries.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Manegeflightsbtn_Click(object sender, EventArgs e)
        {
            Manege_Flights manege_Flights = new Manege_Flights();
            manege_Flights.Show();

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void SHresetbtn_Click(object sender, EventArgs e)
        {
            var dt = _scheduleController.GetAllSchedule();
            bunifuDataGridView1.DataSource = dt;

            if (bunifuDataGridView1.Columns["selectButton"] == null)
            {
                showSchedule();

            }
        }

        private void ScheduleFlightsbtn_Click(object sender, EventArgs e)
        {
            Manege_Flight_Shedule manege_Flight_Shedule = new Manege_Flight_Shedule();
            manege_Flight_Shedule.Show();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void usersearchtxtbox_TextChanged(object sender, EventArgs e)
        {
            if (usersearchtxtbox.TextLength >= 2)
            {
                _customer.Search = usersearchtxtbox.Text;
                var searchResults = _controller.AutoserchCustomer(_customer.Search);

                if (searchResults != null && searchResults.Rows.Count > 0)
                {
                    bunifuDataGridView2.ColumnHeadersVisible = false;
                    bunifuDataGridView2.Columns[0].ReadOnly = true;
                    bunifuDataGridView2.Columns[1].ReadOnly = true;
                    bunifuDataGridView2.DataSource = searchResults;
                    bunifuDataGridView2.Height = searchResults.Rows.Count * 35;
                }
                else
                {
                    bunifuDataGridView2.Height = 0;
                }
            }
            else if (usersearchtxtbox.TextLength == 0)
            {
                bunifuDataGridView2.Height = 0;
            }
        }

        private void bunifuDataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = bunifuDataGridView2.Rows[e.RowIndex];
            showcustomerlabels();
            CustIDlable.Text = row.Cells[0].Value.ToString();
            CustNamelable.Text = row.Cells[1].Value.ToString();
     
        }

        private void usersearchtxtbox_Click(object sender, EventArgs e)
        {
            usersearchtxtbox.Text = string.Empty;
        }

        private void ADDcustbtn_Click(object sender, EventArgs e)
        {
            materialTabControl1.SelectedIndex = 4;
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            // Check if the clicked cell is the button column
            if (e.ColumnIndex == 0)
            {
                // Get the clicked row
                DataGridViewRow row1 = bunifuDataGridView1.Rows[e.RowIndex];

                // Show flight labels
                showflightlabels();
                materialTabControl1.SelectedIndex = 2;
                // Check for null values before accessing cell values
                AirplaneLabel.Text = row1.Cells[1].Value?.ToString() ?? string.Empty;
                Tolocationlable.Text = row1.Cells[2].Value?.ToString() ?? string.Empty;
                totimelabel.Text = row1.Cells[3].Value?.ToString() ?? string.Empty;
                Fromlocationlabel.Text = row1.Cells[4].Value?.ToString() ?? string.Empty;
                fromtimelable.Text = row1.Cells[5].Value?.ToString() ?? string.Empty;
                _schedule.Arrival = row1.Cells[2].Value?.ToString() ?? string.Empty;
                _schedule.Departure = row1.Cells[4].Value?.ToString() ?? string.Empty;
                Console.WriteLine(_schedule.Arrival);
                Console.WriteLine(_schedule.Departure);
                var _priceCalculation = new PriceCalculation(_schedule.Arrival, _schedule.Departure);
                pricelabel.Text = _priceCalculation.GetPrice().ToString("C");
            }
        }

        private void materialButton3_Click_1(object sender, EventArgs e)
        {
            _schedule.Arrival = SHfromcoombobox.Text;
            _schedule.Departure = SHtocoombobox.Text;
            _schedule.DepartureDate = SHdepartdatebox.Text;
            _schedule.ArrivalDate = SHreturndatebox.Text;

            var searchResults = _scheduleController.SearchSchedule(_schedule.Departure, _schedule.Arrival, _schedule.DepartureDate, _schedule.ArrivalDate);
            bunifuDataGridView1.DataSource = searchResults;
        }

        private void checkbtn1_Click(object sender, EventArgs e)
        {
            _Booking.classtype= TRAVELCLASSbox.Text;
            _Booking.adultct = int.Parse(ADULTStxtbox.Text);
            _Booking.childct = int.Parse(CHILDRENtxtbox.Text);
            _Booking.flightprice = float.Parse(pricelabel.Text, System.Globalization.NumberStyles.Currency);
             var _passengerPriceCalculator = new PassengerPriceCalculator(_Booking.classtype,_Booking.adultct, _Booking.childct, _Booking.flightprice);
            totalpricetxtbox .Text= _passengerPriceCalculator.GetTotalPrice().ToString("C");
        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            bookingBilling.BookingID = 16;
            bookingBilling.CustomerID = int.Parse(CustomerID.Text); 
            bookingBilling.PaidAmount = float.Parse(Paidamttxtbox.Text, System.Globalization.NumberStyles.Currency); 
            bookingBilling.DueAmount = float.Parse(label33.Text, System.Globalization.NumberStyles.Currency);
            bookingBilling.TotalAmount = float.Parse(TotalTextBox.Text, System.Globalization.NumberStyles.Currency); 
            bookingBilling.PaymentMethod = Paymenmtd.Text;
            bookingBilling.DiscountType = distyp.Text;
            bookingBilling.Discount = float.Parse(label33.Text, System.Globalization.NumberStyles.Currency); 
            bookingBilling.TransactionID = "None";
            bookingBilling.Currency = "USD";
    
            bookingBilling.PaymentStatus = "Paid";
            flightBookingBilling_Controller.SaveFlightBookingBilling(bookingBilling);
        }

        private void materialButton4_Click_1(object sender, EventArgs e)
        {
            bookingBilling.DiscountType = distyp.Text;
            bookingBilling.TotalAmount = double.Parse(totalpricetxtbox.Text, System.Globalization.NumberStyles.Currency);
            label33.Text = flightBookingBilling_Controller.CalculateDiscount(bookingBilling.DiscountType, bookingBilling.TotalAmount).ToString("C");
            bookingBilling.TotalAmount = double.Parse(label33.Text, System.Globalization.NumberStyles.Currency) - double.Parse(totalpricetxtbox.Text, System.Globalization.NumberStyles.Currency);
            TotalTextBox.Text = "";
            TotalTextBox.Text = bookingBilling.TotalAmount.ToString("C");
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bookingBilling.PaymentMethod = Paymenmtd.Text;
            bookingBilling.TotalAmount = double.Parse(TotalTextBox.Text, System.Globalization.NumberStyles.Currency);
            bookingBilling.PaidAmount = double.Parse(Paidamttxtbox.Text, System.Globalization.NumberStyles.Currency);
            bookingBilling.DueAmount = flightBookingBilling_Controller.CalculateDueAmount(bookingBilling.TotalAmount, bookingBilling.PaymentMethod, bookingBilling.PaidAmount);
            DueAmtlabl.Text = bookingBilling.DueAmount.ToString("C");
        }
    }
}
