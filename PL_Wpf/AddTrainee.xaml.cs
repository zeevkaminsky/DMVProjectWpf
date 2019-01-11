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
            this.InnerGrid.DataContext = trainee;

            CityCBox.ItemsSource = Enum.GetValues(typeof(Cities));
            CityCBox.SelectedIndex = 0;

            GenderCBox.ItemsSource = Enum.GetValues(typeof(Gender));
            GenderCBox.SelectedIndex = 0;


            this.VehicleCBox.ItemsSource = Enum.GetValues(typeof(Vehicle));
            VehicleCBox.SelectedIndex = 0;

            GearCBox.ItemsSource = Enum.GetValues(typeof(Gear));
            GearCBox.SelectedIndex = 0;
            
        }

        private void AddTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                trainee.Name.FirstName = FNameTBox.Text;
                trainee.Name.LastName = LNameTBox.Text;
                trainee.Phone = PhoneTBox.Text;
                trainee.School = SchoolTBox.Text;
                trainee.TeacherName.FirstName = TeacherFNameTBox.Text;
                trainee.TeacherName.LastName = TeacherLNameTBox.Text;
                trainee.DateOfBirth = DOBDPicker.SelectedDate.Value;
                Object gen = GenderCBox.SelectedItem;
                trainee.Gender = Configuration.ToEnum<Gender>(gen.ToString());
                object city = CityCBox.SelectedItem;
                trainee.Address.Town = (city.ToString());
                Object V = VehicleCBox.SelectedItem;
                trainee.MyVehicle = Configuration.ToEnum<Vehicle>(V.ToString());
                Object g = GearCBox.SelectedItem;
                trainee.MyGear = Configuration.ToEnum<Gear>(g.ToString());
                trainee.Address.Street = StreetTBox.Text;
                trainee.Address.Building = int.Parse(NumberTBox.Text);


                if ( _bl.AddTrainee(trainee))
                {
                    MessageBox.Show(trainee.ToString() + "added successfully");
                    this.Close();
                }
                
            }
            catch (Exception m )
            {

                MessageBox.Show(m.Message) ;
            }
            
        }
    }
}
