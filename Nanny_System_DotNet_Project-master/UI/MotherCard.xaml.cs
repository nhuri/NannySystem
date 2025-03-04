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
    /// Interaction logic for MotherCard.xaml
    /// </summary>
    public partial class MotherCard : Window
    {
        IBL bl;
        Mother motherOfCard;
        DataGrid dataGridToRefresh;

        public MotherCard(Mother MotherToShow, DataGrid dataGridInput)
        {
            InitializeComponent();

            try
            {
                bl = FactoryBL.GetBL();
                dataGridToRefresh = dataGridInput;
                motherOfCard = MotherToShow.GetCopy();

                ID_TextBlock.Text = MotherToShow.MomID.ToString();
                FirstName_TextBlock.Text = MotherToShow.MomFirstName;
                LastName_TextBlock.Text = MotherToShow.MomFamilyName;
                PhoneNumber_TextBlock.Text = "0" + MotherToShow.MomPhoneNum.ToString();
                Adress_TextBlock.Text = MotherToShow.MomAdress;
                NannyAdress_TextBlock.Text = MotherToShow.MomSearchAdress;
                Comments_TextBlock.Text = MotherToShow.MomComment;


                for (int i = 0; i < 6; i++)
                {
                    if (MotherToShow.MomDaysNannyNeeds[i])
                    {
                        switch (i)
                        {
                            case 0:
                                Sunday_Day_TextBlock.Visibility = Visibility.Visible;
                                Sunday_Time_TextBlock.Visibility = Visibility.Visible;
                                Sunday_Day_TextBlock.Text = UiTools.NumToDay(i);
                                Sunday_Time_TextBlock.Text = MotherToShow.MomHoursNannyNeeds[i].ToString();
                                break;
                            case 1:
                                Monday_Day_TextBlock.Visibility = Visibility.Visible;
                                Monday_Time_TextBlock.Visibility = Visibility.Visible;
                                Monday_Day_TextBlock.Text = UiTools.NumToDay(i);
                                Monday_Time_TextBlock.Text = MotherToShow.MomHoursNannyNeeds[i].ToString();
                                break;
                            case 2:
                                Tuesday_Day_TextBlock.Visibility = Visibility.Visible;
                                Tuesday_Time_TextBlock.Visibility = Visibility.Visible;
                                Tuesday_Day_TextBlock.Text = UiTools.NumToDay(i);
                                Tuesday_Time_TextBlock.Text = MotherToShow.MomHoursNannyNeeds[i].ToString();
                                break;
                            case 3:
                                Wednesday_Day_TextBlock.Visibility = Visibility.Visible;
                                Wednesday_Time_TextBlock.Visibility = Visibility.Visible;
                                Wednesday_Day_TextBlock.Text = UiTools.NumToDay(i);
                                Wednesday_Time_TextBlock.Text = MotherToShow.MomHoursNannyNeeds[i].ToString();
                                break;
                            case 4:
                                Thursday_Day_TextBlock.Visibility = Visibility.Visible;
                                Thursday_Time_TextBlock.Visibility = Visibility.Visible;
                                Thursday_Day_TextBlock.Text = UiTools.NumToDay(i);
                                Thursday_Time_TextBlock.Text = MotherToShow.MomHoursNannyNeeds[i].ToString();
                                break;
                            case 5:
                                Friday_Day_TextBlock.Visibility = Visibility.Visible;
                                Friday_Time_TextBlock.Visibility = Visibility.Visible;
                                Friday_Day_TextBlock.Text = UiTools.NumToDay(i);
                                Friday_Time_TextBlock.Text = MotherToShow.MomHoursNannyNeeds[i].ToString();
                                break;

                            default:
                                break;
                        }

                    }
                }
                MotherToShow = new Mother();
            }
            catch (Exception c)
            {

                MessageBox.Show(c.Message);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((Button)sender == Update_Btn)
                {
                    MotherEditWindow motherEditWindow = new MotherEditWindow(motherOfCard, dataGridToRefresh);
                    motherEditWindow.ShowDialog();
                    this.Close();
                }
                if ((Button)sender == Delete_Btn)
                {
                    bl.DeleteMom(motherOfCard.MomID);
                    motherOfCard = null;
                    dataGridToRefresh.ItemsSource = null;
                    dataGridToRefresh.ItemsSource = bl.GetAllMoms();
                    MessageBox.Show("Successfully deleted");
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
