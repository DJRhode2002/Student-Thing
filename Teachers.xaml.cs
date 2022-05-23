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
    /// Interaction logic for Teachers.xaml
    /// </summary>
    public partial class Teachers : Window
    {
        string name, surn;
        public Teachers()
        {
           
            InitializeComponent();
            var TeachDataTable = new DataTable();
            TeachDataTable.Columns.Add("Teacher_Id");
            TeachDataTable.Columns.Add("Name");
            TeachDataTable.Columns.Add("Surname");
            var TFromdb = new List<Teacher>();
            string co = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(co))
            {
                string sql = "Select * from dbo.Teachers";
                var SqlCommand = new SqlCommand(sql, conn);
                conn.Open();
                using SqlDataReader rd = SqlCommand.ExecuteReader();
                {
                    while (rd.Read())
                    {
                        var TItem = new Teacher();
                        TItem.Teacher_Id = rd["Teacher_id"].ToString();
                        TItem.First_Name = rd["First_Name"].ToString();
                        TItem.Last_Name = rd["Last_Name"].ToString();
                        TFromdb.Add(TItem);
                    }
                    conn.Close();
                }
            }
            foreach (var teacher in TFromdb)
            {
                TeachDataTable.Rows.Add(teacher.Teacher_Id, teacher.First_Name, teacher.Last_Name);
            }
            Teach.ItemsSource = TeachDataTable.DefaultView;

        }
        public class Teacher
        {
            public string? Teacher_Id { get; set; }
            public string? First_Name { get; set; }
            public string? Last_Name { get; set; }
        }
        private void Teach_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView? row = gd.SelectedItem as DataRowView;
            if (row != null)
            {
                SL.Text = row["Teacher_Id"].ToString();
                NN.Text = row["Name"].ToString();
                SN.Text = row["Surname"].ToString();
            }
        }

        private void H_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mn = new MainWindow();
            mn.Show();
            this.Close();
        }

        private void ED_Click(object sender, RoutedEventArgs e)
        {
            if (SL.Text != String.Empty)
            {   
                string constring = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
                SqlConnection conn = new SqlConnection(constring);
                conn.Open();
                String Query = "Update dbo.Teachers set First_Name=@prop, Last_Name=@nam where Teacher_Id = " + this.SL.Text;
                SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.AddWithValue("@prop", NN.Text);
                cmd.Parameters.AddWithValue("@nam", SN.Text);

                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Replaced");

            }
        }

        private void AD_Click(object sender, RoutedEventArgs e)
        {
            if (TL.Text == "Admin111")
            {
                //string name, surn;
                name = NN.Text;
                surn = SN.Text;
                string connectionString = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
                string query = @"INSERT INTO dbo.Teachers(First_Name, Last_Name)" +
                "VALUES('" + name + "','" + surn + "')";
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

        private void DE_Click(object sender, RoutedEventArgs e)
        {
            if (SL.Text != String.Empty)
            {
                string constring = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
                SqlConnection conn = new SqlConnection(constring);
                string Query = "DELETE FROM dbo.Teachers where Teacher_Id= '" + this.SL.Text + "'";
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
    }
}
