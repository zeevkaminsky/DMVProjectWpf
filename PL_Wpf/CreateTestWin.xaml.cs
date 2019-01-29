using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
            test = new BE.Test();
            this.DataContext = test;


            this.exitPointCityComboBox.ItemsSource = Enum.GetValues(typeof(Cities));

            this.traineeIDComboBox.ItemsSource = _bl.GetTrainees();
            this.traineeIDComboBox.DisplayMemberPath = "ID";
            //test hour
            for (int i = 9; i < 15; i++)
            {
                string str = i + ":00";
                this.testHourComboBox.Items.Add(str);
            }



            findTesterButton.Visibility = Visibility.Visible;
            availabilityDataGrid.Visibility = Visibility.Hidden;
            testerDetailsLabel.Visibility = Visibility.Hidden;
            this.AddTestButton.Visibility = Visibility.Hidden;


            //set the time picker to next 1000 days; friday & saturday will be unavailable;
            testDayDatePicker.DisplayDateStart = DateTime.Now;
            testDayDatePicker.DisplayDateEnd = DateTime.Now + TimeSpan.FromDays(1000);
            var minDate = testDayDatePicker.DisplayDateStart ?? DateTime.MinValue;
            var maxDate = testDayDatePicker.DisplayDateEnd ?? DateTime.MaxValue;
            for (var d = minDate; d <= maxDate && DateTime.MaxValue > d; d = d.AddDays(1))
            {
                if (d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Friday)
                {
                    testDayDatePicker.BlackoutDates.Add(new CalendarDateRange(d));
                }
            }
        }

        private void FindTesterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                test.TestHour = TimeSpan.Parse(this.testHourComboBox.SelectedItem.ToString());
                var testers = _bl.FindTesterToTest(test);//find  a matcing tester; day,hour,vehicle,distance;
                if (testers.Any())
                {

                    this.availabilityDataGrid.ItemsSource = new ObservableCollection<Person>(testers);
                    findTesterButton.Visibility = Visibility.Hidden;
                    availabilityDataGrid.Visibility = Visibility.Visible;
                    testerDetailsLabel.Visibility = Visibility.Visible;
                    this.AddTestButton.Visibility = Visibility.Visible;


                }
                else
                {
                    MessageBox.Show("No tester match. please pick a different day.");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("please choose an hour for the test");
            }




        }



        private void AddTestButton_Click(object sender, RoutedEventArgs e)
        {
            BE.Tester tester;
            try
            {
                if (availabilityDataGrid.SelectedItem != null && availabilityDataGrid.SelectedItem as BE.Tester != null)
                {
                    tester = availabilityDataGrid.SelectedItem as BE.Tester;
                }
                else
                {
                    throw new Exception("please choose one tester from the list");
                }
                test.Vehicle = _bl.FindTraineeByID(test.TraineeID).MyVehicle;
                test.Gear = _bl.FindTraineeByID(test.TraineeID).MyGear;
                test.TesterID = tester.ID;
                
                new Thread(() =>
                {
                    try
                    {
                        
                        if (_bl.AddDrivingTest(test))
                        {
                            MessageBox.Show("the test set to " + test.TestHour.ToString());
                            MessageBox.Show(test + "added successfully");
                        }
                    }
                    catch (Exception m)
                    {
                        MessageBox.Show(m.Message);
                    }
                    Dispatcher.Invoke(new Action(() =>
                    {
                        try
                        {
                            Close();
                        }
                        catch (Exception n)
                        {
                            MessageBox.Show(n.Message);
                        }
                    }));

                }).Start();
            }
            catch (Exception p)
            {

                MessageBox.Show(p.Message);
            }
        }
        
        private void TestHourComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            findTesterButton.Visibility = Visibility.Visible;
            availabilityDataGrid.Visibility = Visibility.Hidden;
            testerDetailsLabel.Visibility = Visibility.Hidden;
            this.AddTestButton.Visibility = Visibility.Hidden;
        }
        //input validation
        private void ExitPointNumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Extra.NumbersValidate(sender, e);
        }
    }
}
