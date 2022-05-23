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
    /// Interaction logic for Subjects.xaml
    /// </summary>
    public partial class Subjects : Window
    {
        string name;
        public Subjects()
        {
            InitializeComponent();
            var TeachDataTable = new DataTable();
            TeachDataTable.Columns.Add("Subject_Id");
            TeachDataTable.Columns.Add("Title");
            var TFromdb = new List<Subject>();
            string co = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(co))
            {
                string sql = "Select * from dbo.Subjects";
                var SqlCommand = new SqlCommand(sql, conn);
                conn.Open();
                using SqlDataReader rd = SqlCommand.ExecuteReader();
                {
                    while (rd.Read())
                    {
                        var TItem = new Subject();
                        TItem.Subject_Id = rd["Subject_id"].ToString();
                        TItem.Title = rd["Title"].ToString();
                        TFromdb.Add(TItem);
                    }
                    conn.Close();
                }
            }
            foreach (var teacher in TFromdb)
            {
                TeachDataTable.Rows.Add(teacher.Subject_Id, teacher.Title);
            }
            SUBJ.ItemsSource = TeachDataTable.DefaultView;
        }
        public class Subject
        {
            public string? Subject_Id { get; set; }
            public string? Title { get; set; }
        }

        private void HM_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void SUBJ_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView? row = gd.SelectedItem as DataRowView;
            if (row != null)
            {
                SL.Text = row["Subject_Id"].ToString();
                TT.Text = row["Title"].ToString();
            }
        }

        private void Ad_Click(object sender, RoutedEventArgs e)
        {
            if (TL.Text == "Admin111")
            {
                //string name, surn;
                name = TT.Text;
                string connectionString = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
                string query = @"INSERT INTO dbo.Subjects(Title)" +
                "VALUES('" + name + "')";
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

        private void ED_Click(object sender, RoutedEventArgs e)
        {
            if (SL.Text != String.Empty)
            {
                string constring = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
                SqlConnection conn = new SqlConnection(constring);
                conn.Open();
                String Query = "Update dbo.Subjects set Title=@prop Subject_Id = " + this.SL.Text;
                SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.AddWithValue("@prop", TT.Text);


                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Replaced");

            }
        }

        private void DL_Click(object sender, RoutedEventArgs e)
        {
            if (SL.Text != String.Empty)
            {
                string constring = "Data Source=DESKTOP-KUENU0V;Initial Catalog=StudentStuff;Integrated Security=True";
                SqlConnection conn = new SqlConnection(constring);
                string Query = "DELETE FROM dbo.Subjects where Subject_Id= '" + this.SL.Text + "'";
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
