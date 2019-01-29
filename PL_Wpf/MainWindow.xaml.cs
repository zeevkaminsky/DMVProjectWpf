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

namespace PL_Wpf
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

        private void Tester_Click(object sender, RoutedEventArgs e)
        {
            new TesterWind().ShowDialog();
           
          
        }

        private void Trainee_Click(object sender, RoutedEventArgs e)
        {
            new Trainee().ShowDialog();
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            new Test().ShowDialog();
        }

        

        private void Schedule_Click(object sender, RoutedEventArgs e)
        {
            new Schedule().ShowDialog();
        }
    }
}
