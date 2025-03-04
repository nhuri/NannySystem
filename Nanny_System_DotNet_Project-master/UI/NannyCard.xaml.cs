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

    public partial class NannyCard : Window
    {
        IBL bl;
        Nanny nannyOfCard;
        DataGrid dataGridToRefresh;

        public NannyCard(Nanny NannyToShow, DataGrid dataGridInput)
        {
            InitializeComponent();
            dataGridToRefresh = dataGridInput;
            bl = FactoryBL.GetBL();
            nannyOfCard = NannyToShow.GetCopy();
            ID_TextBox.Text = NannyToShow.NannyId.ToString();
            FirstName_TextBox.Text = NannyToShow.NannyPrivateName;
            LastName_TextBox.Text = NannyToShow.NannyFamilyName;
            PhoneNumber_TextBox.Text = "0" + NannyToShow.NannyPhoneNum.ToString();
            birth_TextBox.Text = NannyToShow.NannyDateOfBirth.Day + "/" + NannyToShow.NannyDateOfBirth.Month + "/" + NannyToShow.NannyDateOfBirth.Year;
            Adress_TextBox.Text = NannyToShow.NannyAdress;
            Floor_TextBox.Text = NannyToShow.NannyFloor.ToString();
            Elevator_CheckBox.IsChecked = NannyToShow.NannyIsElevator;
            Experience_TextBox.Text = NannyToShow.NannyYearsOfExperience.ToString();
            Maxchildrens_TextBox.Text = NannyToShow.NannyMaxInfants.ToString();
            AgeRange_TextBox.Text = NannyToShow.NannyMinInfantAge.ToString() + "-" + NannyToShow.NannyMaxInfantAge.ToString();
            Vacation_TextBox.Text = NannyToShow.NannyIsElevator ? "Ministry of Education" : "Ministry of Industry and Trade";
            MonthlySalary_TextBox.Text = NannyToShow.NannyMonthlySalary.ToString() + " NIS";
            HourlySalary_TextBox.Text = NannyToShow.NannyIsHourlySalary ? NannyToShow.NannyHourlySalary.ToString() + " NIS" : "Does not allow hourly salary";
            Recommendations_TextBox.Text = NannyToShow.NannyRecommendations;
            

            for (int i = 0; i < 6; i++)
            {
                if (NannyToShow.NannyWorkingDays[i])
                {
                    switch (i)
                    {
                        case 0:
                            Sunday_Day_TextBlock.Visibility = Visibility.Visible;
                            Sunday_Time_TextBlock.Visibility = Visibility.Visible;
                            Sunday_Day_TextBlock.Text = UiTools.NumToDay(i);
                            Sunday_Time_TextBlock.Text = NannyToShow.NannyWorkingHours[i].ToString();
                            break;
                        case 1:
                            Monday_Day_TextBlock.Visibility = Visibility.Visible;
                            Monday_Time_TextBlock.Visibility = Visibility.Visible;
                            Monday_Day_TextBlock.Text = UiTools.NumToDay(i);
                            Monday_Time_TextBlock.Text = NannyToShow.NannyWorkingHours[i].ToString();
                            break;
                        case 2:
                            Tuesday_Day_TextBlock.Visibility = Visibility.Visible;
                            Tuesday_Time_TextBlock.Visibility = Visibility.Visible;
                            Tuesday_Day_TextBlock.Text = UiTools.NumToDay(i);
                            Tuesday_Time_TextBlock.Text = NannyToShow.NannyWorkingHours[i].ToString();
                            break;
                        case 3:
                            Wednesday_Day_TextBlock.Visibility = Visibility.Visible;
                            Wednesday_Time_TextBlock.Visibility = Visibility.Visible;
                            Wednesday_Day_TextBlock.Text = UiTools.NumToDay(i);
                            Wednesday_Time_TextBlock.Text = NannyToShow.NannyWorkingHours[i].ToString();
                            break;
                        case 4:
                            Thursday_Day_TextBlock.Visibility = Visibility.Visible;
                            Thursday_Time_TextBlock.Visibility = Visibility.Visible;
                            Thursday_Day_TextBlock.Text = UiTools.NumToDay(i);
                            Thursday_Time_TextBlock.Text = NannyToShow.NannyWorkingHours[i].ToString();
                            break;
                        case 5:
                            Friday_Day_TextBlock.Visibility = Visibility.Visible;
                            Friday_Time_TextBlock.Visibility = Visibility.Visible;
                            Friday_Day_TextBlock.Text = UiTools.NumToDay(i);
                            Friday_Time_TextBlock.Text = NannyToShow.NannyWorkingHours[i].ToString();
                            break;

                        default:
                            break;
                    }

                }
            }


            NannyToShow = new Nanny();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if ((Button)sender == Update_Btn)
                {
                    NannyEditWindow nannyEditWindow = new NannyEditWindow(nannyOfCard, dataGridToRefresh);
                    nannyEditWindow.ShowDialog();
                    this.Close();
                }
                    if ((Button)sender == Delete_Btn)
                {
                    bl.DeleteNanny(nannyOfCard.NannyId);
                    nannyOfCard = null;
                    if (dataGridToRefresh !=null)
                    {
                        dataGridToRefresh.ItemsSource = null;
                        dataGridToRefresh.ItemsSource = bl.GetAllNannies();
                        MessageBox.Show("Successfully deleted");
                        this.Close();
                    }
                    
                }

            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }
           
        }
    }
}
