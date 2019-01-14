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

            
        }
        public AddTester(Tester testerToUp)//update window sending an existing tester
        {


            InitializeComponent();
            tester = testerToUp;
            this.EnterButton.Content = "update";
            init(testerToUp);
            
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


