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
    /// Interaction logic for ContractCard.xaml
    /// </summary>
    public partial class ContractCard : Window
    {
        IBL bl;
        Contract contractOfCard;
        DataGrid dataGridToRefresh;

        public ContractCard(Contract contractToShow, DataGrid dataGridInput)
        {
            InitializeComponent();
            dataGridToRefresh = dataGridInput;
            bl = FactoryBL.GetBL();
            contractOfCard = contractToShow.GetCopy();

            ID_TextBlock.Text = contractToShow.ContID.ToString();
            Child_ID_TextBlock.Text = contractToShow.ChildID.ToString();
            Nanny_ID_TextBlock.Text = contractToShow.NannyID.ToString();
            Introductory_Meeting_CheckBox.IsChecked = contractToShow.IsIntroductoryMeeting;
            SpeicalDetailsOfMeeting.Text = contractToShow.SpeicalDetailsOfMeeting;
            signed_CheckBox.IsChecked = contractToShow.IsContractSigned;
            PaymentMethod_TextBlock.Text = contractToShow.PaymentMethod.ToString();
            if (PaymentMethod_TextBlock.Text == "Hourly")
            {
                Calculated_wage_TextBlock.Text = contractToShow.HourlySalary.ToString();
            }
            else Calculated_wage_TextBlock.Text = contractToShow.MonthlySalary.ToString();
            Start_Date_TextBlock.Text = $"{contractToShow.StartDate.Day}/{contractToShow.StartDate.Month}/{contractToShow.StartDate.Year}";
            End_Date_TextBlock.Text = $"{contractToShow.EndDate.Day}/{contractToShow.EndDate.Month}/{contractToShow.EndDate.Year}";


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((Button)sender == Update_Btn)
                {
                    ContractEditWindow contractEditWindow = new ContractEditWindow(contractOfCard, dataGridToRefresh);
                    contractEditWindow.Show();
                    this.Close();
                }
                if ((Button)sender == Delete_Btn)
                {
                    bl.DeleteContract(contractOfCard.ContID);                  
                    MessageBox.Show("Successfully deleted");
                    contractOfCard = null;
                    dataGridToRefresh.ItemsSource = null;
                    dataGridToRefresh.ItemsSource = bl.GetAllContracts();
                    this.Close();
                }
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }
        }

    }
}
