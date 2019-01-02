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
    /// Interaction logic for RemoveTester.xaml
    /// </summary>
    public partial class RemoveTester : Window
    {
        private ObservableCollection<string> testers = new ObservableCollection<string>();
        public RemoveTester()
        {
            
            InitializeComponent();
            try
            {
                DataContext = testers;
                IBl _bl = FactorySingletonBl.GetBl();
                
               testers = (ObservableCollection<string>)from tester in _bl.GetTesters()
                         select tester.ID;

                IDCBox.ItemsSource = testers; 


            }
            catch (Exception)
            {
                MessageBox.Show("ergfhfgjh");
            }
           

            
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IBl _bl = FactorySingletonBl.GetBl();
                _bl.RemoveTester(IDCBox.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
