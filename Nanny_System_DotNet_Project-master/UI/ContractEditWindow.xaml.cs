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
using BE;
using BL;

namespace UI
{
    /// <summary>
    /// Interaction logic for ContractEditWindow.xaml
    /// </summary>
    public partial class ContractEditWindow : Window
    {
        IBL bl;
        DataGrid dataGridToRefresh;
        double finalSalary;
        public ContractEditWindow(Contract contractToUpdate, DataGrid dataGridInput)
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();
            dataGridToRefresh = dataGridInput;
            Update_ID_TextBlock.Text = contractToUpdate.ContID.ToString();
            Update_Child_ID_TextBlock.Text = contractToUpdate.ChildID.ToString();
            Update_Nanny_ID_TextBlock.Text = contractToUpdate.NannyID.ToString();
            Update_SpeicalDetailsOfMeeting.Text = contractToUpdate.SpeicalDetailsOfMeeting;
            if (contractToUpdate.PaymentMethod == Payment_method.hourly)
            {
                Payment_Method_ComboBox.SelectedItem = Hourly_ComboBoxItem;
                Calculated_Wage_TextBlock.Text = contractToUpdate.HourlySalary.ToString();
            }
            else
            {
                Payment_Method_ComboBox.SelectedItem = Monthly_ComboBoxItem;
                Calculated_Wage_TextBlock.Text = contractToUpdate.MonthlySalary.ToString();
            }
            Update_Start_Date_TextBlock.Text = contractToUpdate.StartDate.Day + "/" + contractToUpdate.StartDate.Month + "/" + contractToUpdate.StartDate.Year;
            Update_End_Date_TextBlock.Text = contractToUpdate.EndDate.Day + "/" + contractToUpdate.EndDate.Month + "/" + contractToUpdate.EndDate.Year;
            Update_signed_CheckBox.IsChecked = contractToUpdate.IsContractSigned;
            Update_Introductory_Meeting_CheckBox.IsChecked = contractToUpdate.IsIntroductoryMeeting;

            contractToUpdate = new Contract();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Contract updateContract = new Contract();

                updateContract.ContID = int.Parse(Update_ID_TextBlock.Text);
                updateContract.ChildID = int.Parse(Update_Child_ID_TextBlock.Text);
                updateContract.NannyID = int.Parse(Update_Nanny_ID_TextBlock.Text);
                updateContract.SpeicalDetailsOfMeeting = Update_SpeicalDetailsOfMeeting.Text;
                updateContract.IsIntroductoryMeeting = (bool)Update_Introductory_Meeting_CheckBox.IsChecked;
                if (Payment_Method_ComboBox.SelectedItem == Hourly_ComboBoxItem)
                {
                    updateContract.HourlySalary = finalSalary;
                }
                else updateContract.MonthlySalary = finalSalary;
                updateContract.StartDate = new DateTime(Update_Start_Date_TextBlock.SelectedDate.Value.Year, Update_Start_Date_TextBlock.SelectedDate.Value.Month, Update_Start_Date_TextBlock.SelectedDate.Value.Day);
                updateContract.EndDate = new DateTime(Update_End_Date_TextBlock.SelectedDate.Value.Year, Update_End_Date_TextBlock.SelectedDate.Value.Month, Update_End_Date_TextBlock.SelectedDate.Value.Day);
                if (updateContract.EndDate < updateContract.StartDate)
                {
                    throw new Exception("cannot end before start");
                }
                updateContract.IsContractSigned = (bool)Update_signed_CheckBox.IsChecked;

                bl.UpdateContractDetails(updateContract);
                updateContract = new Contract();
                this.IsEnabled = false;
                if (dataGridToRefresh != null)
                {
                    dataGridToRefresh.ItemsSource = null;
                    dataGridToRefresh.ItemsSource = bl.GetAllContracts();
                }
                MessageBox.Show("Update succeeded");
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message);              
            }
        }


        private void CalculateSalary()
        {

            Child child = BlTools.GetChild(int.Parse(Update_Child_ID_TextBlock.Text));
            Nanny nanny = BlTools.GetNanny(int.Parse(Update_Nanny_ID_TextBlock.Text));
            double salary;
            if (!nanny.NannyIsHourlySalary)
            {
                Payment_Method_ComboBox.IsEnabled = false;
                Hourly_ComboBoxItem.Visibility = Visibility.Collapsed;
                Payment_Method_ComboBox.SelectedItem = Monthly_ComboBoxItem;
                salary = BlTools.CalculateMonthlySalary(nanny, child);
                finalSalary = salary;
                Calculated_Wage_TextBlock.Text = Convert.ToString(salary) + " NIS";
       
            }
            else
            {
                Hourly_ComboBoxItem.Visibility = Visibility.Visible;
                if (Payment_Method_ComboBox.SelectedItem != null)
                {
                    if (Payment_Method_ComboBox.SelectedIndex == 1)
                    {
                        salary = BlTools.CalculateMonthlySalary(nanny, child);
                        Calculated_Wage_TextBlock.Text = Convert.ToString(salary) + " NIS";
                        finalSalary = salary;
                    }
                    if (Payment_Method_ComboBox.SelectedIndex == 0)
                    {
                        salary = BlTools.CalculateHourlySalary(nanny, child);
                        Calculated_Wage_TextBlock.Text = Convert.ToString(salary) + " NIS";
                        finalSalary = salary;
                    }
                }
            }
        }

        private void Payment_Method_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculateSalary();
        }
    }
}
