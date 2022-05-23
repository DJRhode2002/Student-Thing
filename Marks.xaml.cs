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
    /// Interaction logic for Marks.xaml
    /// </summary>
    public partial class Marks : Window
    {
        string stud, sub, dates, marks;
        public Marks()
        {
            InitializeComponent();
            fill_Combo();
            cobo();
            //STUD show in table
            var StudTable = new DataTable();
            StudTable.Columns.Add("Mark_id");
            StudTable.Columns.Add("Student_Id");
            StudTable.Columns.Add("Subject_id");
            StudTable.Columns.Add("Dates");
            StudTable.Columns.Add("Marks");
            var Studfromdb = new List<Mark>();
            string c = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(c))
            {
                string sql = "SELECT * FROM dbo.Marks";
                var SqlCommand = new SqlCommand(sql, con);
                con.Open();
                using SqlDataReader re = SqlCommand.ExecuteReader();
                {
                    while (re.Read())
                    {
                        var SItem = new Mark();
                        SItem.Mark_Id = re["Mark_Id"].ToString();
                        SItem.Student_Id = re["Student_Id"].ToString();
                        SItem.Subject_Id = re["Subject_Id"].ToString();
                        SItem.Date = re["Dates"].ToString();
                        SItem.Marks = re["Marks"].ToString();
                        Studfromdb.Add(SItem);
                    }
                    con.Close();
                }
            }
            foreach (var Stud in Studfromdb)
            {
                StudTable.Rows.Add(Stud.Mark_Id, Stud.Student_Id, Stud.Subject_Id, Stud.Date, Stud.Marks);
            }
            STUD.ItemsSource = StudTable.DefaultView;
        }
        public class Mark
        {
            public string? Mark_Id { get; set; }
            public string? Student_Id { get; set; }
            public string? Subject_Id { get; set; }
            public string? Date { get; set; }
            public string? Marks { get; set; }
        }

        private void DB_Click(object sender, RoutedEventArgs e)
        {
            if (SL.Text != String.Empty)
            {
                string constring = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
                SqlConnection conn = new SqlConnection(constring);
                string Query = "DELETE FROM dbo.Marks where Mark_Id= '" + this.SL.Text + "'";
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
                String Query = "Update dbo.Marks set Student_Id=@prop, Student_Id=@nam, Dates=@dat,Marks=@mk where Mark_Id = " + this.SL.Text;
                SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.AddWithValue("@prop", SI.Text);
                cmd.Parameters.AddWithValue("@nam", SD.Text);
                cmd.Parameters.AddWithValue("@dat", SD.Text);
                cmd.Parameters.AddWithValue("@mk", SD.Text);

                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Replaced");

            }
        }

        private void STUD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView? row = gd.SelectedItem as DataRowView;
            if (row != null)
            {
                SL.Text = row["Mark_Id"].ToString();
                SI.Text = row["Student_Id"].ToString();
                SD.Text = row["Subject_Id"].ToString();
                DP.Text = row["Dates"].ToString();
                MRK.Text = row["Marks"].ToString();
            }
        }

        private void HB_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void AD_Click(object sender, RoutedEventArgs e)
        {
            if (TL.Text == "Admin111")
            {
                //string stud, sub, dates, marks;
                stud = SI.Text;
                sub = SD.Text;
                dates = DP.Text;
                marks = MRK.Text;

                string connectionString = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
                string query = @"INSERT INTO dbo.Marks(Student_Id, Subject_Id, Dates, Marks)" +
                "VALUES('" + stud + "','" + sub + "','" + dates + "','" + marks + "')";
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
        void fill_Combo()
        {
            string co = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
            string qe = "SELECT * FROM dbo.Students ;";
            SqlConnection cn = new SqlConnection(co);
            SqlCommand g = new SqlCommand(qe, cn);
            SqlDataReader rd;

            try
            {
                cn.Open();
                rd = g.ExecuteReader();
                while (rd.Read())
                {
                    int n = rd.GetInt32("Student_Id");
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
                    SD?.Items.Add(n);
                }

            }
            catch
            {
                MessageBox.Show("Eh");
            }

        }

    }
}
