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
using System.Windows.Shapes;

namespace PL_Wpf
{
    /// <summary>
    /// Interaction logic for Tester.xaml
    /// </summary>
    public partial class TesterWind : Window
    {
        public TesterWind()
        {
            InitializeComponent();
        }

        private void AddTester_Click(object sender, RoutedEventArgs e)
        {
            new AddTester().ShowDialog();
           
        }

        private void RemoveTester_Click(object sender, RoutedEventArgs e)
        {
            new RemoveTester().ShowDialog();
           
        }

        private void UpdateTester_Click(object sender, RoutedEventArgs e)
        {
            new UpdateTesterWin().ShowDialog();
        }

        private void DataBaseTester_Click(object sender, RoutedEventArgs e)
        {
            new TesterView().ShowDialog();
        }
    }
}
