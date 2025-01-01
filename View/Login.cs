using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace C_Coursework
{
    public partial class Login : MaterialForm

    {
        public Login()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        
        }


        public SqlConnection con;
        public SqlCommand cmd;
        public SqlDataReader dr;
        public static string LN = "";
        public string UN;
        public string PWD;


        private void Form1_Load(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = "Data Source =OM3GA;Initial Catalog= Flight_Booking_System;Integrated Security =True";
        }

        private void kryptonLabel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonLabel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Login where username=@UN and Password=@PWD", con);
            UN = username.Text;
            PWD = password.Text;
            cmd.Parameters.AddWithValue("UN", UN);
            cmd.Parameters.AddWithValue("PWD", PWD);

            dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                
                   Dasbord dasbord = new Dasbord();

                   dasbord.Show();
                   this.Hide();
                }
                dr.Close();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dr.Close();
                con.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
