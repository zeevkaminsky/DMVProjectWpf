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
            tester = new BE.Tester();
            
            init(tester);//initialize all comboboxes with apropriate values

            
        }
        public AddTester(Tester testerToUp)//update window sending an existing tester
        {


            InitializeComponent();
            tester = testerToUp;
            this.EnterButton.Content = "update";
            this.IDTBox.IsReadOnly = true;
            init(testerToUp);

            foreach (var item in SchedGrid.Children.OfType<CheckBox>())
            {
                int row = Grid.GetRow(item);
                int column = Grid.GetColumn(item);
                item.IsChecked = tester.WeeklySchedule.weeklySchedule[column - 1][row - 1];

            }

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
        
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                foreach (var item in SchedGrid.Children.OfType<CheckBox>())
                {
                    int row = Grid.GetRow(item);
                    int column = Grid.GetColumn(item);
                    tester.WeeklySchedule.weeklySchedule[column - 1][row - 1] = item.IsChecked == true?true:false;
                    
                }
                

                

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

        //private void MenuButton_Click(object sender, RoutedEventArgs e)
        //{
        //    MainWindow mainWindow = new MainWindow();
        //    this.Close();
        //    mainWindow.Show();
        //}

        
    }
}


