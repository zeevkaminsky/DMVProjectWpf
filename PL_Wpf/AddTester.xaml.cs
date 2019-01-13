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
    /// Interaction logic for AddTester.xaml
    /// </summary>
    public partial class AddTester : Window
    {

        private BE.Tester tester;

        public AddTester()
        {
            InitializeComponent();
            tester = new BE.Tester { Name = new FullName(), Address = new Address(), WeeklySchedule = new Schedule() };
            init(tester);//initialize all comboboxes with apropriate values

            ////dafault combo box values
            //GenderCBox.SelectedIndex = 0;
            //SpecialityCBox.SelectedIndex = 0;
            //CityCBox.SelectedIndex = 0;
            //ExpCBox.SelectedIndex = 0;
            //MaxTestsCBox.SelectedIndex = 0;
            //MaxDistanceCBox.SelectedIndex = 0;

           // DateOfBirthDatePicker.SelectedDate = DateTime.Now.AddYears(-50);
        }
        public AddTester(Tester testerToUp)//update window sending an existing tester
        {


            InitializeComponent();
            tester = testerToUp;
            this.EnterButton.Content = "update";
            init(testerToUp);
            //GenderCBox.SelectedItem = testerToUp.Gender;
            //SpecialityCBox.SelectedItem = testerToUp.MyVehicle;
            //CityCBox.SelectedItem = testerToUp.Address.Town;
            //StreetTBox.Text = testerToUp.Address.Street;
            //NumberTBox.Text = testerToUp.Address.Building.ToString();
            //ExpCBox.SelectedItem = testerToUp.Experience;
            //MaxTestsCBox.SelectedItem = testerToUp.MaxTests;
            //MaxDistanceCBox.SelectedItem = testerToUp.MaxDistance;
            //FirstNameTBox.Text = testerToUp.Name.FirstName;
            //LastNameTBox.Text = testerToUp.Name.LastName;

            //DateOfBirthDatePicker.SelectedDate = testerToUp.DateOfBirth;
            //DateOfBirthDatePicker.Text = testerToUp.DateOfBirth.ToShortDateString();
        }

        private void init(BE.Tester tester)
        {

            
            this.TextBoxGrid.DataContext = tester;

            GenderCBox.ItemsSource = Enum.GetValues(typeof(Gender));
            SpecialityCBox.ItemsSource = Enum.GetValues(typeof(Vehicle));
            CityCBox.ItemsSource = Enum.GetValues(typeof(Cities));

            for (int i = 0; i < Configuration.MaxTesterAge - Configuration.minTesterAge; i++)
            {
                ExpCBox.Items.Add(i);
            }

            for (int i = 0; i <= 30; i++)
            {
                MaxTestsCBox.Items.Add(i);
            }

            for (int i = 0; i < 150; i++)
            {
                MaxDistanceCBox.Items.Add(i);
            }

        }


        #region disableButoon


        #endregion



       


        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            Schedule schedule = new Schedule { weeklySchedule = new WorkAvailability[5][] };
            for (int i = 0; i < 5; i++)
            {
                schedule.weeklySchedule[i] = new WorkAvailability[6];
            }
            try
            {
                foreach (var item in SchedGrid.Children.OfType<CheckBox>())
                {
                    int row = Grid.GetRow(item);
                    int column = Grid.GetColumn(item);
                    if (item.IsChecked == true)
                    {
                        schedule.weeklySchedule[column - 1][row - 1] = WorkAvailability.work;
                    }
                    else
                    {
                        schedule.weeklySchedule[column - 1][row - 1] = WorkAvailability.not_work;
                    }
                }
                tester.WeeklySchedule.weeklySchedule = schedule.weeklySchedule;

                //tester.Name.FirstName = FirstNameTBox.Text;
                //tester.Name.LastName = LastNameTBox.Text;
                //tester.DateOfBirth = DateOfBirthDatePicker.SelectedDate.Value;
                //Object gen = GenderCBox.SelectedItem;
                //tester.Gender = Configuration.ToEnum<Gender>(gen.ToString());
                //tester.Phone = PhoneTBox.Text;
                //tester.MaxTests = int.Parse(MaxTestsCBox.SelectedItem.ToString());
                //tester.Experience = int.Parse(ExpCBox.SelectedItem.ToString());
                //tester.MaxDistance = int.Parse(MaxDistanceCBox.SelectedItem.ToString());
                //Object V = SpecialityCBox.SelectedItem;
                //tester.MyVehicle = Configuration.ToEnum<Vehicle>(V.ToString());
                //object city = CityCBox.SelectedItem;
                //tester.Address.Town = (city.ToString());
                //tester.Address.Street = StreetTBox.Text;
                //tester.Address.Building = int.Parse(NumberTBox.Text);

                if (EnterButton.Content.ToString() != "update")
                {
                    IBl _bl = FactorySingletonBl.GetBl();
                    if (_bl.AddTester(tester))
                    {
                        MessageBox.Show(tester.ToString() + "added successfully");
                        this.Close();
                    }
                }
                else
                {
                    IBl _bl = FactorySingletonBl.GetBl();
                    
                    if (_bl.UpdateTester(tester))
                    {
                        MessageBox.Show(tester.ToString() + "updated successfully");
                        this.Close();
                    }
                }
               
            }
            catch (Exception m)
            {

                MessageBox.Show(m.Message);
            }







        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        
    }
}


