using System;
using System.Collections.Generic;
using System.Globalization;
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
using BE;
using BL;
using System.ComponentModel;
using System.Threading;

namespace UI
{
    /// <summary>
    /// Interaction logic for ContractAddWindow.xaml
    /// </summary>
    public partial class ContractAddWindow : Window
    {
        Contract newContract;
        IBL bl;
        DataGrid dataGridToRefresh;

        public ContractAddWindow(DataGrid dataGridInput)
        {
            InitializeComponent();
            dataGridToRefresh = dataGridInput;
            newContract = new Contract();
            bl = BL.FactoryBL.GetBL();

            //give the values for child and nanny comboboxes
            All_Childs_ComboBox.ItemsSource = bl.GetAllChilds().ToList();
            All_Nannies_ComboBox.ItemsSource = bl.GetAllNannies().ToList();
        }



        private void All_Childs_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Nannies_Fit_Mom_DataGrid.ItemsSource = null;
            if (Nannies_warning.Visibility == Visibility.Visible)
            {
                Nannies_warning.Visibility = Visibility.Collapsed;
                Order_ComboBox.IsEnabled = true;
            }
            if (Order_ComboBox.IsEnabled == true)
                Order_ComboBox_SelectionChanged(null, null);

            //if a child and nanny chosen, calculate nanny salary immediately
            if (All_Nannies_ComboBox.SelectedItem != null)
                CalculateSalary();
        }


        private void All_Nannies_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if a child and nanny chosen, calculate nanny salary immediately
            if (All_Childs_ComboBox.SelectedItem != null)
                CalculateSalary();
            //All_Childs_ComboBox.IsLoaded
        }

        private void Select_Button_Click(object sender, RoutedEventArgs e)
        {
            //will show the nanny inthe nanny combobox when nanny chosen 
            Nanny nanny;
            if (Nannies_Fit_Mom_DataGrid.SelectedItem is Nanny)
            {
                nanny = BlTools.GetNanny((Nannies_Fit_Mom_DataGrid.SelectedItem as Nanny).NannyId);
            }   
            else
                nanny = BlTools.GetNanny((Nannies_Fit_Mom_DataGrid.SelectedItem as NannyWithDis).NannyId);

            if (nanny == null)
            {
                throw new Exception("Error, The nanny was deleted");
            }

            All_Nannies_ComboBox.SelectedItem = nanny;
        }


        private void Show_Nanny_Card_Button_Click(object sender, RoutedEventArgs e)
        {
            //Show_Nanny_Card
            Nanny nanny;
            if (Nannies_Fit_Mom_DataGrid.SelectedItem is Nanny)
            {
                nanny = Nannies_Fit_Mom_DataGrid.SelectedItem as Nanny;
            }
            else
                nanny = BlTools.GetNanny((Nannies_Fit_Mom_DataGrid.SelectedItem as NannyWithDis).NannyId);
            NannyCard nannyCard = new NannyCard(nanny, dataGridToRefresh);
            nannyCard.Show();
        }

        private void Order_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //reset
            Nannies_Fit_Mom_DataGrid.ItemsSource = null;
            Column_NannyMonthlySalary.Visibility = Visibility.Collapsed;
            Column_NannyHourlySalary.Visibility = Visibility.Collapsed;
            Column_NannyYearsOfExperience.Visibility = Visibility.Collapsed;
            Column_Distance.Visibility = Visibility.Collapsed;
            if (Nannies_warning.Visibility == Visibility.Visible)
                Nannies_warning.Visibility = Visibility.Collapsed;


            int distance;
            if (int.TryParse(Distance_TextBox.Text, out distance))
            {
                if (All_Childs_ComboBox.SelectedItem != null)
                {
                    Child child = All_Childs_ComboBox.SelectedItem as Child;
                    wait_image.Visibility = Visibility.Visible;

                    switch (Order_ComboBox.SelectedIndex)
                    {
                        case 0:
                            ThreadAndFillDataGrid(BlTools.MostAppropriateNanniesByDistance(child, distance));
                            break;
                        case 1:
                            ThreadAndFillDataGrid(BlTools.MostAppropriateNanniesByMonthlyPrice(child, distance));
                            Column_NannyMonthlySalary.Visibility = Visibility.Visible;
                            break;
                        case 2:
                            ThreadAndFillDataGrid(BlTools.MostAppropriateNanniesByHourlyPrice(child, distance));
                            Column_NannyHourlySalary.Visibility = Visibility.Visible;
                            break;
                        case 3:
                            ThreadAndFillDataGrid(BlTools.MostAppropriateNanniesByYearsOfExperience(child, distance));
                            Column_NannyYearsOfExperience.Visibility = Visibility.Visible;
                            break;
                    }
                }
            }
        }

        private void Distance_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Order_ComboBox.IsEnabled = false;
            if (Distance_TextBox.Text == "")
                return;
            if (!UiTools.Id_Error_Alerter(Distance_TextBox))
            {
                int distance;
                if (int.TryParse(Distance_TextBox.Text, out distance) && All_Childs_ComboBox.SelectedItem != null)
                    Order_ComboBox.IsEnabled = true;
            }
        }

        private void Distance_TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Distance_TextBox.Foreground == Brushes.Red)
                UiTools.Restore_Text_Box(Distance_TextBox);
        }

        //threading
        private void ThreadAndFillDataGrid(IEnumerable<Nanny> IEnanny)
        {
            try
            {
                int momId = BlTools.GetMomIdOfChildId((All_Childs_ComboBox.SelectedItem as Child).ChildID);
                new Thread(() =>
                {
                    List<Nanny> l = IEnanny.ToList();
                    List<NannyWithDis> fiveAppropriateNanniesList = null;

                    //if there is no fully fit nanny
                    if (l.Count == 0)
                    {
                        Mother mother;
                        Dispatcher.BeginInvoke(new Action(() =>
                        {
                            try
                            {
                                Nannies_warning.Visibility = Visibility.Visible;
                                Column_Distance.Visibility = Visibility.Visible;
                                Order_ComboBox.IsEnabled = false;
                            }
                            catch (Exception c)
                            {

                               MessageBox.Show(c.Message);
                            }
                        }));
                        mother = BlTools.GetMother(momId);
                        fiveAppropriateNanniesList = BlTools.FiveAppropriateNannies(mother);                   
                    }

                    Dispatcher.Invoke(new Action(() =>
                    {
                        try
                        {
                            wait_image.Visibility = Visibility.Collapsed;
                            if (fiveAppropriateNanniesList == null)
                                Nannies_Fit_Mom_DataGrid.ItemsSource = l;
                            else Nannies_Fit_Mom_DataGrid.ItemsSource = fiveAppropriateNanniesList;
                        }
                        catch (Exception n)
                        {
                            MessageBox.Show(n.Message);
                        }
                    }));
                }).Start();
            }
            catch (Exception n)
            {
                MessageBox.Show(n.Message);
            }
        }

        private void CalculateSalary()
        {

            Payment_Method_ComboBox.IsEnabled = true;
            Child child = All_Childs_ComboBox.SelectedItem as Child;
            Nanny nanny = All_Nannies_ComboBox.SelectedItem as Nanny;
            double salary;
            if (!nanny.NannyIsHourlySalary)
            {
                Payment_Method_ComboBox.IsEnabled = false;
                Hourly_ComboBoxItem.Visibility = Visibility.Collapsed;
                Payment_Method_ComboBox.SelectedItem = Monthly_ComboBoxItem;
                salary = BlTools.CalculateMonthlySalary(nanny, child);
                Calculated_Wage_TextBox.Text = Convert.ToString(salary) + " NIS";
                newContract.MonthlySalary = salary;
            }
            else
            {
                Hourly_ComboBoxItem.Visibility = Visibility.Visible;
                if (Payment_Method_ComboBox.SelectedItem != null)
                {
                    if (Payment_Method_ComboBox.SelectedIndex == 1)
                    {
                        salary = BlTools.CalculateMonthlySalary(nanny, child);
                        Calculated_Wage_TextBox.Text = Convert.ToString(salary) + " NIS";
                        newContract.MonthlySalary = salary;
                    }
                    if (Payment_Method_ComboBox.SelectedIndex == 0)
                    {                      
                        salary = BlTools.CalculateHourlySalary(nanny, child);
                        Calculated_Wage_TextBox.Text = Convert.ToString(salary) + " NIS";
                        newContract.HourlySalary = salary;
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                newContract.ChildID = (All_Childs_ComboBox.SelectedItem as Child).ChildID;
                newContract.NannyID = (All_Nannies_ComboBox.SelectedItem as Nanny).NannyId;
                newContract.IsIntroductoryMeeting = (bool)IntroductoryMeeting_CheckBox.IsChecked;
                newContract.SpeicalDetailsOfMeeting = SpecialDetailsOfMeeting_TextBox.Text;
            
                if (Contract_Signed_CheckBox.IsChecked == true)
                {
                    if (Start_DatePicker.Text != "" && End_DatePicker.Text != "")
                    {
                        newContract.StartDate = new DateTime(Start_DatePicker.SelectedDate.Value.Year, Start_DatePicker.SelectedDate.Value.Month, Start_DatePicker.SelectedDate.Value.Day);
                        newContract.EndDate = new DateTime(End_DatePicker.SelectedDate.Value.Year, End_DatePicker.SelectedDate.Value.Month, End_DatePicker.SelectedDate.Value.Day); 
                    }
                    else throw new Exception("contract must have start and end valid dates");
                    if (newContract.StartDate > newContract.EndDate)
                    {
                        throw new Exception("contract start must be before end");
                    }
                }
                
                
                bl.AddContract(newContract);
                dataGridToRefresh.ItemsSource = null;
                dataGridToRefresh.ItemsSource = bl.GetAllContracts();
                //shlomi
                MessageBox.Show("Succesfuly Added", "Succesfuly Add",MessageBoxButton.OK,MessageBoxImage.Information);
                this.IsEnabled = false;
                newContract = new Contract();
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }
        }

        private void Payment_Method_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculateSalary();
        }
    }


}
