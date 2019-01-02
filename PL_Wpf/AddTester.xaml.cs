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
    /// Interaction logic for AddTester.xaml
    /// </summary>
    public partial class AddTester : Window
    {
        public AddTester()
        {
            InitializeComponent();

            var vehicles = Enum.GetValues(typeof(Vehicle));
            foreach (var v in vehicles)
            {
                SpecialityCBox.Items.Add(v);
            }

            var cities = Enum.GetValues(typeof(Cities));
            foreach (var c in cities)
            {
                CityCBox.Items.Add(c);
            }

            foreach (var item in SchedGrid.Children)
            {
                if (item is ListBox)
                {
                    ListBox LB = item as ListBox;
                    for (int i = 9; i < 16; ++i)
                    {
                        ListBoxItem newItem = new ListBoxItem();
                        newItem.Content = i + ":00";
                        LB.Items.Add(newItem);
                    }
                }
            }
            

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
        /// <summary>
        /// convert to enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumString"></param>
        /// <returns></returns>
        public static T ToEnum<T>(string enumString)
        {
            return (T)Enum.Parse(typeof(T), enumString);
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {


            BE.Tester tester = new BE.Tester();
            tester.ID = IDTBox.Text;
            tester.Name.FirstName = FirstNameTBox.Text;
            tester.Name.LastName = LastNameTBox.Text;
            ComboBoxItem typeItem = new ComboBoxItem();
            typeItem = (ComboBoxItem)GenderCBox.SelectedItem;
            tester.Gender = ToEnum<Gender>(typeItem.Content.ToString());
            //tester.MaxDistance = (double)MaxDistanceTBox.Text;

            try
            {
                IBl _bl = FactorySingletonBl.GetBl();
                if(_bl.AddTester(tester))
                {
                    MessageBox.Show(tester.ToString() + "added successfully");
                }
            }
            catch (Exception m)
            {

                throw m;
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

