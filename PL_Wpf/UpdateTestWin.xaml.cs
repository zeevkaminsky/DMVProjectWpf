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

        private void PastTestButton_Click(object sender, RoutedEventArgs e)
        {
            
            test.Criteria["isSignaling"] = this.SignalCheckBox.IsChecked;
            test.Criteria["TwoHandsOnWheel"] = this.WheelCheckBox.IsChecked;
            test.Criteria["Mirors"] = this.MirorsCheckBox.IsChecked;
            if (_bl.IsLisense(test.TraineeID))
            {
                this.resultTextBlock.Text = "trainee past the test";
                test.TestResult = true;
            }
            else
            {
                this.resultTextBlock.Text = "trainee did not past the test";
                test.TestResult = false;
            }
        }
    }
}
