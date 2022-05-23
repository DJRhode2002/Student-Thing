using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentDing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Student_Click(object sender, RoutedEventArgs e)
        {
            Students st = new Students();
            st.Show();
            this.Close();
        }

        private void Teachs_Click(object sender, RoutedEventArgs e)
        {
            Teachers t = new Teachers();
            t.Show();
            this.Close();
        }

        private void Subjects_Click(object sender, RoutedEventArgs e)
        {
            Subjects subjects = new Subjects();
            subjects.Show();
            this.Close();
        }

        private void mark_Click(object sender, RoutedEventArgs e)
        {
            Marks mn = new Marks();
            mn.Show();
            this.Close();
        }

        private void group_Click(object sender, RoutedEventArgs e)
        {
            Groups n = new Groups();
            n.Show();
            this.Close();
        }

        private void ST_Click(object sender, RoutedEventArgs e)
        {
            SubTeach t = new SubTeach();
            t.Show();
            this.Close();
        }
    }
}
