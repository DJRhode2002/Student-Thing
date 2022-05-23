using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StudentDing
{
    /// <summary>
    /// Interaction logic for SubTeach.xaml
    /// </summary>
    public partial class SubTeach : Window
    {
        string stud, sub, dates;
        public SubTeach()
        {
            InitializeComponent();
            InitializeComponent();
            fill_Combo();
            cobo();
            Comb();
            //STUD show in table
            var StudTable = new DataTable();
            StudTable.Columns.Add("Subject_id");
            StudTable.Columns.Add("Teacher_Id");
            StudTable.Columns.Add("Group_Id");

            var Studfromdb = new List<STS>();
            string c = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(c))
            {
                string sql = "SELECT * FROM dbo.SubTeach";
                var SqlCommand = new SqlCommand(sql, con);
                con.Open();
                using SqlDataReader re = SqlCommand.ExecuteReader();
                {
                    while (re.Read())
                    {
                        var SItem = new STS();
                        SItem.Subject_Id = re["Subject_Id "].ToString();
                        SItem.Teacher_Id = re["Teacher_Id"].ToString();
                        SItem.Group_Id = re["Group_Id"].ToString();
                        Studfromdb.Add(SItem);
                    }
                    con.Close();
                }
            }
            foreach (var Stud in Studfromdb)
            {
                StudTable.Rows.Add(Stud.Subject_Id, Stud.Teacher_Id, Stud.Group_Id);
            }
            ST.ItemsSource = StudTable.DefaultView;
        }


        public class STS
        {
            public string? Subject_Id { get; set; }
            public string? Teacher_Id { get; set; }
            public string? Group_Id { get; set; }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mn = new MainWindow();
            mn.Show();
            this.Close();
        }
        void fill_Combo()
        {

            string co = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
            string qe = "SELECT * FROM dbo.Subjects ;";
            SqlConnection cn = new SqlConnection(co);
            SqlCommand g = new SqlCommand(qe, cn);
            SqlDataReader rd;

            try
            {
                cn.Open();
                rd = g.ExecuteReader();
                while (rd.Read())
                {
                    int n = rd.GetInt32("Subject_Id");
                    SI?.Items.Add(n);
                }

            }
            catch
            {
                MessageBox.Show("Eh");
            }
        }
        void cobo()
        {
            string co = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
            string qe = "SELECT * FROM dbo.Teachers ;";
            SqlConnection cn = new SqlConnection(co);
            SqlCommand g = new SqlCommand(qe, cn);
            SqlDataReader rd;

            try
            {
                cn.Open();
                rd = g.ExecuteReader();
                while (rd.Read())
                {
                    int n = rd.GetInt32("Teacher_Id");
                    TI?.Items.Add(n);
                }

            }
            catch
            {
                MessageBox.Show("Eh");
            }

        }
        void Comb()
        {
            string co = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
            string qe = "SELECT * FROM dbo.Groups ;";
            SqlConnection cn = new SqlConnection(co);
            SqlCommand g = new SqlCommand(qe, cn);
            SqlDataReader rd;

            try
            {
                cn.Open();
                rd = g.ExecuteReader();
                while (rd.Read())
                {
                    int n = rd.GetInt32("Group_Id");
                    GI?.Items.Add(n);
                }

            }
            catch
            {
                MessageBox.Show("Eh");
            }
        }

        private void ST_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView? row = gd.SelectedItem as DataRowView;
            if (row != null)
            {
                SI.Text = row["Subject_Id_Id"].ToString();
                TI.Text = row["Teacher_Id"].ToString();
                GI.Text = row["Group_Id"].ToString();
                SL.Text = row["Subject_Id_Id"].ToString();

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SL.Text != String.Empty)
            {
                string constring = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
                SqlConnection conn = new SqlConnection(constring);
                string Query = "DELETE FROM dbo.SubTeach where Subject_Id= '" + this.SL.Text + "'";
                SqlCommand cmd = new SqlCommand(Query, conn);
                SqlDataReader myreader;
                try
                {
                    conn.Open();
                    myreader = cmd.ExecuteReader();
                    MessageBox.Show("Deleted");
                    while (myreader.Read())
                    {

                    }
                    conn.Close();
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void ED_Click(object sender, RoutedEventArgs e)
        {

            if (SL.Text != String.Empty)
            {
                string constring = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
                SqlConnection conn = new SqlConnection(constring);
                conn.Open();
                String Query = "Update dbo.SubTeach set Subject_Id=@prop, Teacher_Id=@nam, Group_Id=@dat where Subject_Id = " + this.SL.Text;
                SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.AddWithValue("@prop", SI.Text);
                cmd.Parameters.AddWithValue("@nam", TI.Text);
                cmd.Parameters.AddWithValue("@dat", GI.Text);

                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Replaced");

            }
        }

        private void AD_Click(object sender, RoutedEventArgs e)
        {

            if (TL.Text == "Admin111")
            {
                //string stud, sub, dates;
                stud = SI.Text;
                sub = TI.Text;
                dates = GI.Text;


                string connectionString = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
                string query = @"INSERT INTO dbo.Subjects(Subject_Id, Teacher_Id, Group_Id)" +
                "VALUES('" + stud + "','" + sub + "','" + dates + "')";
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();



                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Saved! ");



                }
                connection.Close();
            }
            else
            {
                MessageBox.Show("Enter password");
            }
        }
    }
}
