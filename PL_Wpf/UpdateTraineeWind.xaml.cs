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
    /// Interaction logic for UpdateTraineeWind.xaml
    /// </summary>
    public partial class UpdateTraineeWind : Window
    {
        private ObservableCollection<string> trainees = new ObservableCollection<string>();
        public UpdateTraineeWind()
        {
            InitializeComponent();

            IBl _bl = FactorySingletonBl.GetBl();
            foreach (var item in _bl.GetTrainees())
            {
                trainees.Add(item.ID);
            }


            IDCBox.ItemsSource = trainees;

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            IBl _bl = FactorySingletonBl.GetBl();
            BE.Trainee trainee = _bl.FindTraineeByID(IDCBox.SelectedItem as string);
            this.Close();
            new AddTrainee(trainee).Show();
        }
    }
}
