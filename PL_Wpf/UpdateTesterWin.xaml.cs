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
    /// Interaction logic for UpdateTesterWin.xaml
    /// </summary>
    public partial class UpdateTesterWin : Window
    {
        private ObservableCollection<string> testers = new ObservableCollection<string>();
        public UpdateTesterWin()
        {
            InitializeComponent();

            IBl _bl = FactorySingletonBl.GetBl();
            //combo box with all testers ID
            foreach (var item in _bl.GetTesters())
            {
                testers.Add(item.ID);
            }
            IDCBox.ItemsSource = testers;

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            IBl _bl = FactorySingletonBl.GetBl();
            BE.Tester tester = _bl.FindTesterByID(IDCBox.SelectedItem as string);
            this.Close();
            new AddTester(tester).Show();//send to add window
        }
    }
}
