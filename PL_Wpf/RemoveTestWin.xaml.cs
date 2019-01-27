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
    /// Interaction logic for RemoveTestWin.xaml
    /// </summary>
    public partial class RemoveTestWin : Window
    {
       
        private ObservableCollection<int> tests = new ObservableCollection<int>();
        public RemoveTestWin()
        {

            InitializeComponent();
            try
            {

                IBl _bl = FactorySingletonBl.GetBl();
                foreach (var item in _bl.GetTests())
                {
                    tests.Add(item.SerialNumber);
                }


                IDCBox.ItemsSource = tests;


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }



        }
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IDCBox.SelectedItem != null)
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Tester", System.Windows.MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        IBl _bl = FactorySingletonBl.GetBl();
                        int TestToRemove =int.Parse( IDCBox.SelectedItem.ToString());
                        if (_bl.RemovedrivingTest(TestToRemove))
                        {
                            tests.Remove(TestToRemove);
                            MessageBox.Show(TestToRemove + " removed successfully");
                            this.Close();
                        }
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
