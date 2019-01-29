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
    /// Interaction logic for TesterView.xaml
    /// </summary>
    public partial class TesterView : Window
    {
        IBl _bl;
        public TesterView()
        {
            _bl = FactorySingletonBl.GetBl();
            InitializeComponent();
            this.LVUsers.ItemsSource = _bl.GetTesters();//shows all testers in the system

            this.cityCB.ItemsSource = Enum.GetValues(typeof(Cities));
            this.VehicleCB.ItemsSource = Enum.GetValues(typeof(Vehicle));
        }
        // show only testers with chosen type of vehicle
        private void VehicleCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(VehicleCB.SelectedItem != null)
            {
                this.LVUsers.ItemsSource = (from item1 in _bl.TestersByVehicle()
                                            from item2 in item1
                                            where item1.Key.ToString() == VehicleCB.SelectedItem.ToString()
                                            select item2).ToList();
            }
            
        }
        //show only testers with chosen city
        private void CityCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cityCB.SelectedItem != null)
            {
                this.LVUsers.ItemsSource = (from item1 in _bl.TestersByCity()
                                            from item2 in item1
                                            where item1.Key.ToString() == cityCB.SelectedItem.ToString()
                                            select item2).ToList();
            }

        }
        //show all
        private void AllBtn_Click(object sender, RoutedEventArgs e)
        {
            this.LVUsers.ItemsSource = _bl.GetTesters();
        }
    }
}
