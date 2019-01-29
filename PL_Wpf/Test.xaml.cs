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
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        public Test()
        {
            InitializeComponent();
        }

        private void CreateTest_Click(object sender, RoutedEventArgs e)
        {
            new CreateTestWin().ShowDialog();
        }

        private void UpdateTest_Click(object sender, RoutedEventArgs e)
        {
            new UpdateTestWin().ShowDialog();
        }

        private void RemoveTest_Click(object sender, RoutedEventArgs e)
        {
            new RemoveTestWin().ShowDialog();
        }

        private void DataBaseTest_Click(object sender, RoutedEventArgs e)
        {
            new TestView().ShowDialog();
        }
    }
}
