using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            test = new BE.Test { Gear = new Gear() , Vehicle = new Vehicle(), TestDay = new DateTime() , ExitPoint = new Address()};
            this.DataContext = test;
            this.gearComboBox.ItemsSource = Enum.GetValues(typeof(Gear));
            this.vehicleComboBox.ItemsSource = Enum.GetValues(typeof(Vehicle));
            this.exitPointCityComboBox.ItemsSource = Enum.GetValues(typeof(Cities));
           
            this.traineeIDComboBox.ItemsSource = _bl.GetTrainees();
            this.traineeIDComboBox.DisplayMemberPath = "ID";

            findTesterButton.Visibility = Visibility.Visible;
            availabilityDataGrid.Visibility = Visibility.Hidden;
            testerDetailsLabel.Visibility = Visibility.Hidden;
        }

        private void FindTesterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                test.TestHour = TimeSpan.Parse(testHourTextBox.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            var testers = _bl.FindTesterToTest(test);
            if (testers != null)
            {
                BE.Tester tester = testers.First();
                this.availabilityDataGrid.ItemsSource = new ObservableCollection<Person>(testers);
                test.TesterID = tester.ID;
            }
            else
            {

            }
            
            findTesterButton.Visibility = Visibility.Hidden;
            availabilityDataGrid.Visibility = Visibility.Visible;
            testerDetailsLabel.Visibility = Visibility.Visible;

        }

        private void TestHourTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            findTesterButton.Visibility = Visibility.Visible;
            availabilityDataGrid.Visibility = Visibility.Hidden;
            testerDetailsLabel.Visibility = Visibility.Hidden;
        }

        private void AddTestButton_Click(object sender, RoutedEventArgs e)
        {
            

            try
            {
                if (_bl.AddDrivingTest(test))
                {
                    MessageBox.Show(test + "added successfully");
                }
            }
            catch (Exception m)
            {

                MessageBox.Show(m.Message);
            }
            
        }
    }
}
