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
    /// Interaction logic for AddTrainee.xaml
    /// </summary>
    public partial class AddTrainee : Window
    {
        IBl _bl;
        private BE.Trainee trainee;
        
        public AddTrainee()
        {

            InitializeComponent();
            trainee = new BE.Trainee { Name = new FullName(), Address = new Address(), TeacherName = new FullName() };
            _bl = FactorySingletonBl.GetBl();
            init();

            
            //CityCBox.SelectedIndex = 0;
            //GenderCBox.SelectedIndex = 0;
            //VehicleCBox.SelectedIndex = 0;
            //GearCBox.SelectedIndex = 0;

        }
        public AddTrainee(BE.Trainee traineeToUp)
        {
            InitializeComponent();
            trainee = traineeToUp;
            _bl = FactorySingletonBl.GetBl();
            init();
            this.AddTraineeButton.Content = "Update";

           
           
            //CityCBox.SelectedItem = trainee.Address.Town;
            //GenderCBox.SelectedItem = trainee.Gender;
            //VehicleCBox.SelectedItem = trainee.MyVehicle;
            //GearCBox.SelectedItem = trainee.MyGear;
            //StreetTBox.Text = trainee.Address.Street;
            //NumberTBox.Text = trainee.Address.Building.ToString();
            //ExpCBox.SelectedItem = trainee.Experience;
            //MaxTestsCBox.SelectedItem = trainee.MaxTests;
            //MaxDistanceCBox.SelectedItem = trainee.MaxDistance;
            //FNameTBox.Text = trainee.Name.FirstName;
            //LNameTBox.Text = trainee.Name.LastName;
            
            //TeacherFNameTBox.Text = trainee.TeacherName.FirstName;
            //TeacherLNameTBox.Text = trainee.TeacherName.LastName;

            //LessonsTBox.Text = trainee.NumOfLessons.ToString();
            //TestsTBox.Text = trainee.NUmOfTests.ToString();

            //DOBDPicker.SelectedDate = trainee.DateOfBirth;
           // DOBDPicker.Text = trainee.DateOfBirth.ToShortDateString();
        }
        private void init()
        {
            this.InnerGrid.DataContext = trainee;

            GearCBox.ItemsSource = Enum.GetValues(typeof(Gear));

            CityCBox.ItemsSource = Enum.GetValues(typeof(Cities));

            GenderCBox.ItemsSource = Enum.GetValues(typeof(Gender));

            this.VehicleCBox.ItemsSource = Enum.GetValues(typeof(Vehicle));
        }

        private void AddTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //trainee.Name.FirstName = FNameTBox.Text;
                //trainee.Name.LastName = LNameTBox.Text;
                //trainee.Phone = PhoneTBox.Text;
                //trainee.School = SchoolTBox.Text;
                //trainee.TeacherName.FirstName = TeacherFNameTBox.Text;
                //trainee.TeacherName.LastName = TeacherLNameTBox.Text;
                //trainee.DateOfBirth = DOBDPicker.SelectedDate.Value;
                //Object gen = GenderCBox.SelectedItem;
                //trainee.Gender = Configuration.ToEnum<Gender>(gen.ToString());
                //object city = CityCBox.SelectedItem;
                //// trainee.Address.Town = (city.ToString());
                //Object V = VehicleCBox.SelectedItem;
                //trainee.MyVehicle = Configuration.ToEnum<Vehicle>(V.ToString());
                //Object g = GearCBox.SelectedItem;
                //trainee.MyGear = Configuration.ToEnum<Gear>(g.ToString());
                //trainee.Address.Street = StreetTBox.Text;
                //trainee.Address.Building = int.Parse(NumberTBox.Text);


                if (this.AddTraineeButton.Content.ToString() != "Update")
                {
                    IBl _bl = FactorySingletonBl.GetBl();
                    if (_bl.AddTrainee(trainee))
                    {
                        MessageBox.Show(trainee.ToString() + "added successfully");
                        this.Close();
                    }
                }
                else
                {
                    IBl _bl = FactorySingletonBl.GetBl();

                    if (_bl.UpdateTrainee(trainee))
                    {
                        MessageBox.Show(trainee.ToString() + "updated successfully");
                        this.Close();
                    }
                }

            }
            catch (Exception m)
            {

                MessageBox.Show(m.Message);
            }

        }
    }
}
