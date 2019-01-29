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
    /// Interaction logic for UpdateTestWin.xaml
    /// </summary>
    public partial class UpdateTestWin : Window
    {
        IBl _bl;
        BE.Test test;
        private ObservableCollection<int> tests = new ObservableCollection<int>();
        public UpdateTestWin()
        {
            //this.DataContext = test.Criteria;
            InitializeComponent();
            this.ResultGrid.Visibility = Visibility.Hidden;
            _bl = FactorySingletonBl.GetBl();
            //combo box with all serial numbers
            foreach (var item in _bl.GetTests())
            {
                tests.Add(item.SerialNumber);
            }
            SNCBox.ItemsSource = tests;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            test = _bl.FindTestBySerialNumber(int.Parse(SNCBox.SelectedItem.ToString()));
            this.innerGrid.Visibility = Visibility.Hidden;
            this.ResultGrid.Visibility = Visibility.Visible;
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                test.Criteria["isSignaling"] = this.signalingCheckBox.IsChecked == true;
                test.Criteria["TwoHandsOnWheel"] = this.wheelCheckBox.IsChecked == true;
                test.Criteria["Mirors"] = this.mirorsCheckBox.IsChecked == true;
                if (_bl.IsLisense(test))//check if trainee past 80% precent of requirments
                {
                    MessageBox.Show("trainee past the test");
                    test.TestResult = true;

                }
                else
                {
                    MessageBox.Show("trainee  failed the test");
                    test.TestResult = false;
                }
                if(_bl.UpdateDrivingTest(test))
                {
                    MessageBox.Show("update succeded");
                }
                Close();
            }
            catch (Exception)
            {

                throw;
            }
            
            
        }
    }
}
