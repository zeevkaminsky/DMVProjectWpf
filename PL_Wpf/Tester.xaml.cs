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
    public partial class Tester : Window
    {
        public Tester()
        {
            InitializeComponent();
        }

        private void AddTester_Click(object sender, RoutedEventArgs e)
        {
            AddTester addTesterWin = new AddTester();
           
            addTesterWin.Show();
            this.Close();
        }

        private void RemoveTester_Click(object sender, RoutedEventArgs e)
        {
            RemoveTester win = new RemoveTester();
            this.Close();
            win.Show();
        }
    }
}
