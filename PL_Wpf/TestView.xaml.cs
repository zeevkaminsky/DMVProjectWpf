using BL;
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
    /// Interaction logic for TestView.xaml
    /// </summary>
    public partial class TestView : Window
    {
        IBl _bl;
        public TestView()
        {
            InitializeComponent();
            _bl = FactorySingletonBl.GetBl();
            this.LVUsers.ItemsSource = _bl.GetTests();//show all tests
        }
    }
}
