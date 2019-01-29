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
                
                IBl _bl = FactorySingletonBl.GetBl();
                //combo box show all testers id
                foreach (var item in _bl.GetTesters())
                {
                    testers.Add(item.ID);
                }
                IDCBox.ItemsSource = testers; 


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
                    IBl _bl = FactorySingletonBl.GetBl();

                    //get all tests of thid tester
                    var testersInTest = from t in _bl.GetTests()
                                  where t.TesterID == IDCBox.SelectedItem.ToString()
                                  select t;
                    if (testersInTest.Any())//need to remove the test first
                    {
                        throw new Exception("tester can not be remove. need to remove all tests that he was the tester");
                    }
                    MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Tester", System.Windows.MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        
                        string IDToRemove = IDCBox.SelectedItem as string;
                        if (_bl.RemoveTester(IDToRemove))
                        {
                            testers.Remove(IDToRemove);
                            MessageBox.Show(IDToRemove + " removed successfully");
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
