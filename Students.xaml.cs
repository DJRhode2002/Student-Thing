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
    /// Interaction logic for Students.xaml
    /// </summary>
    public partial class Students : Window
    {
        public string name, surname, group;
        public Students()
        {

            InitializeComponent();
            fill_Combo();
            //STUD show in table
            var StudTable = new DataTable();
            StudTable.Columns.Add("Student_Id");
            StudTable.Columns.Add("First_Name");
            StudTable.Columns.Add("Last_Name");
            StudTable.Columns.Add("Group_Id");
            var Studfromdb = new List<Stud>();
            string c = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(c))
            {
                string sql = "SELECT * FROM dbo.Students";
                var SqlCommand = new SqlCommand(sql, con);
                con.Open();
                using SqlDataReader re = SqlCommand.ExecuteReader();
                {
                    while (re.Read())
                    {
                        var SItem = new Stud();
                        SItem.Student_Id = re["Student_Id"].ToString();
                        SItem.First_Name = re["First_Name"].ToString();
                        SItem.Last_Name = re["Last_Name"].ToString();
                        SItem.Group_Id = re["Group_Id"].ToString();
                        Studfromdb.Add(SItem);
                    }
                    con.Close();
                }
            }
            foreach (var Stud in Studfromdb)
            {
                StudTable.Rows.Add(Stud.Student_Id, Stud.First_Name, Stud.Last_Name, Stud.Group_Id);
            }
            STUD.ItemsSource = StudTable.DefaultView;
        }

        public class Stud
        {
            public string Student_Id { get; set; }
            public string First_Name { get; set; }
            public string Last_Name { get; set; }
            public string Group_Id { get; set; }
        }


        private void HB_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mn = new MainWindow();
            mn.Show();
            this.Close();
        }

        private void GI_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void STUD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView? row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null)
            //SN, SS, GI
            {
                SL.Text = row_selected["Student_Id"].ToString();
                SN.Text = row_selected["First_Name"].ToString();
                SS.Text = row_selected["Last_Name"].ToString();
                GI.Text = row_selected["Group_Id"].ToString();
            }
        }

        private void DB_Click(object sender, RoutedEventArgs e)
        {
            if (SL.Text != String.Empty)
            {
                string c = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
                SqlConnection co = new SqlConnection(c);
                string q = "DELETE FROM dbo.Students where Students_Id = '" + this.SL.Text + "'";
                SqlCommand nt = new SqlCommand(q, co);
                SqlDataReader rd;
                try
                {
                    co.Open();
                    rd = nt.ExecuteReader();
                    MessageBox.Show("Deleted");
                }
                catch
                {
                    MessageBox.Show("Uh.........." +
                        "Akward");
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
                String Query = "Update dbo.Students set First_Name @sell, Last_Name=@num, Group_Id=@Bar where Student_Id = " + this.SL.Text;

                try
                {
                    SqlCommand cmd = new SqlCommand(Query, conn);
                    cmd.Parameters.AddWithValue("@sell", SN.Text);
                    cmd.Parameters.AddWithValue("@num", SS.Text);
                    cmd.Parameters.AddWithValue("@Bar", GI.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Replaced");
                }
                catch
                {
                    MessageBox.Show("Uh.........." +
                           "Akward");
                }
            }
        }

        private void AD_Click(object sender, RoutedEventArgs e)
        {
            //string  name, surname, group;

            name = SN.Text;
            surname = SS.Text;
            group = GI.Text;
            string connectionString = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
            string query = @"INSERT INTO dbo.Students(First_Name, Last_Name, Group_Id)" +
            "VALUES('" + name + "','" + surname + "','" + group + "')";
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
        void fill_Combo()
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
    }
}
