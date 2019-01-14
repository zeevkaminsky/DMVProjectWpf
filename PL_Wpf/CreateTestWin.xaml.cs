using BE;
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
    /// Interaction logic for CreateTestWin.xaml
    /// </summary>
    public partial class CreateTestWin : Window
    {
        IBl _bl;
        private BE.Test test;
        public CreateTestWin()
        {
            InitializeComponent();
            _bl = FactorySingletonBl.GetBl();
            test = new BE.Test { Gear = new Gear() , Vehicle = new Vehicle(), TestDay = new DateTime(), Criteria = new Dictionary<string, bool?>() , ExitPoint = new Address()};
            this.DataContext = test;
            this.gearComboBox.ItemsSource = Enum.GetValues(typeof(Gear));
            this.vehicleComboBox.ItemsSource = Enum.GetValues(typeof(Vehicle));
            this.exitPointCityComboBox.ItemsSource = Enum.GetValues(typeof(Cities));
            this.testerIDComboBox.ItemsSource = _bl.GetTesters();
            this.traineeIDComboBox.ItemsSource = _bl.GetTrainees();
        }

        private void FindTesterButton_Click(object sender, RoutedEventArgs e)
        {
            List<BE.Tester> matchingTesters =  _bl.FindTesterToTest(test);
        }
    }
}
