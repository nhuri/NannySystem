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
    /// Interaction logic for MotherEditWindow.xaml
    /// </summary>
    public partial class MotherEditWindow : Window
    {
        IBL bl;
        DataGrid dataGridToRefresh;

        public MotherEditWindow(Mother motherToEdit, DataGrid dataGridInput)
        {

            InitializeComponent();
            bl = BL.FactoryBL.GetBL();
            dataGridToRefresh = dataGridInput;

            ID_TextBox.Text = motherToEdit.MomID.ToString();
            ID_TextBox.IsEnabled = false;
            FirstName_TextBox.Text = motherToEdit.MomFirstName.ToString();
            LastName_TextBox.Text = motherToEdit.MomFamilyName.ToString();
            PhoneNumber_TextBox.Text = "0" + motherToEdit.MomPhoneNum.ToString();

            int IndexOfStreet = motherToEdit.MomAdress.IndexOf(',');
            int IndexOfCity = motherToEdit.MomAdress.LastIndexOf(',');
            Street_TextBox.Text = motherToEdit.MomAdress.Substring(0, IndexOfStreet);
            City_TextBox.Text = motherToEdit.MomAdress.Substring(IndexOfStreet + 2, IndexOfCity - IndexOfStreet - 2);

            IndexOfStreet = motherToEdit.MomSearchAdress.IndexOf(',');
            IndexOfCity = motherToEdit.MomSearchAdress.LastIndexOf(',');
            NannyStreet_TextBox.Text = motherToEdit.MomSearchAdress.Substring(0, IndexOfStreet);
            NannyCity_TextBox.Text = motherToEdit.MomSearchAdress.Substring(IndexOfStreet + 2, IndexOfCity - IndexOfStreet - 2);
            Comments_TextBox.Text = motherToEdit.MomComment;

            for (int i = 0; i < 6; i++)
            {
                if (motherToEdit.MomDaysNannyNeeds[i])
                {
                    switch (i)
                    {
                        case 0:
                            Schedule.SundayCheckBox.IsChecked = true;
                            Schedule.SundayStartTime.HoursValue = motherToEdit.MomHoursNannyNeeds[i].StartTime.Hours;
                            Schedule.SundayStartTime.MinutesValue = motherToEdit.MomHoursNannyNeeds[i].StartTime.Minutes;
                            Schedule.SundayEndTime.HoursValue = motherToEdit.MomHoursNannyNeeds[i].EndTime.Hours;
                            Schedule.SundayEndTime.MinutesValue = motherToEdit.MomHoursNannyNeeds[i].EndTime.Minutes;
                            break;
                        case 1:
                            Schedule.MondayCheckBox.IsChecked = true;
                            Schedule.MondayStartTime.HoursValue = motherToEdit.MomHoursNannyNeeds[i].StartTime.Hours;
                            Schedule.MondayStartTime.MinutesValue = motherToEdit.MomHoursNannyNeeds[i].StartTime.Minutes;
                            Schedule.MondayEndTime.HoursValue = motherToEdit.MomHoursNannyNeeds[i].EndTime.Hours;
                            Schedule.MondayEndTime.MinutesValue = motherToEdit.MomHoursNannyNeeds[i].EndTime.Minutes;
                            break;
                        case 2:
                            Schedule.TuesdayCheckBox.IsChecked = true;
                            Schedule.TuesdayStartTime.HoursValue = motherToEdit.MomHoursNannyNeeds[i].StartTime.Hours;
                            Schedule.TuesdayStartTime.MinutesValue = motherToEdit.MomHoursNannyNeeds[i].StartTime.Minutes;
                            Schedule.TuesdayEndTime.HoursValue = motherToEdit.MomHoursNannyNeeds[i].EndTime.Hours;
                            Schedule.TuesdayEndTime.MinutesValue = motherToEdit.MomHoursNannyNeeds[i].EndTime.Minutes;
                            break;
                        case 3:
                            Schedule.WednesdayCheckBox.IsChecked = true;
                            Schedule.WednesdayStartTime.HoursValue = motherToEdit.MomHoursNannyNeeds[i].StartTime.Hours;
                            Schedule.WednesdayStartTime.MinutesValue = motherToEdit.MomHoursNannyNeeds[i].StartTime.Minutes;
                            Schedule.WednesdayEndTime.HoursValue = motherToEdit.MomHoursNannyNeeds[i].EndTime.Hours;
                            Schedule.WednesdayEndTime.MinutesValue = motherToEdit.MomHoursNannyNeeds[i].EndTime.Minutes;
                            break;
                        case 4:
                            Schedule.ThursdayCheckBox.IsChecked = true;
                            Schedule.ThursdayStartTime.HoursValue = motherToEdit.MomHoursNannyNeeds[i].StartTime.Hours;
                            Schedule.ThursdayStartTime.MinutesValue = motherToEdit.MomHoursNannyNeeds[i].StartTime.Minutes;
                            Schedule.ThursdayEndTime.HoursValue = motherToEdit.MomHoursNannyNeeds[i].EndTime.Hours;
                            Schedule.ThursdayEndTime.MinutesValue = motherToEdit.MomHoursNannyNeeds[i].EndTime.Minutes;
                            break;
                        case 5:
                            Schedule.FridayCheckBox.IsChecked = true;
                            Schedule.FridayStartTime.HoursValue = motherToEdit.MomHoursNannyNeeds[i].StartTime.Hours;
                            Schedule.FridayStartTime.MinutesValue = motherToEdit.MomHoursNannyNeeds[i].StartTime.Minutes;
                            Schedule.FridayEndTime.HoursValue = motherToEdit.MomHoursNannyNeeds[i].EndTime.Hours;
                            Schedule.FridayEndTime.MinutesValue = motherToEdit.MomHoursNannyNeeds[i].EndTime.Minutes;
                            break;

                        default:
                            break;
                    }
                }
            }


            motherToEdit = new Mother();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Mother EditedMother = new Mother();

            try
            {
                EditedMother.MomID = int.Parse(ID_TextBox.Text);
                EditedMother.MomFirstName = FirstName_TextBox.Text;
                EditedMother.MomFamilyName = LastName_TextBox.Text;
                EditedMother.MomPhoneNum = int.Parse(PhoneNumber_TextBox.Text);
                EditedMother.MomAdress = Street_TextBox.Text + ", " + City_TextBox.Text + ", israel";
                EditedMother.MomSearchAdress = NannyStreet_TextBox.Text + ", " + NannyCity_TextBox.Text + ", israel";
                EditedMother.MomComment = Comments_TextBox.Text;

                #region Time input
                EditedMother.MomDaysNannyNeeds[0] = (bool)Schedule.SundayCheckBox.IsChecked;
                EditedMother.MomDaysNannyNeeds[1] = (bool)Schedule.MondayCheckBox.IsChecked;
                EditedMother.MomDaysNannyNeeds[2] = (bool)Schedule.TuesdayCheckBox.IsChecked;
                EditedMother.MomDaysNannyNeeds[3] = (bool)Schedule.WednesdayCheckBox.IsChecked;
                EditedMother.MomDaysNannyNeeds[4] = (bool)Schedule.ThursdayCheckBox.IsChecked;
                EditedMother.MomDaysNannyNeeds[5] = (bool)Schedule.FridayCheckBox.IsChecked;

                List<WeeklyWorkSchedule> hoursArray = new List<WeeklyWorkSchedule>(6);
                for (int i = 0; i < 6; i++)
                    hoursArray.Insert(i, new WeeklyWorkSchedule());




                hoursArray[0].StartTime = new TimeSpan((int)Schedule.SundayStartTime.HoursValue, (int)Schedule.SundayStartTime.MinutesValue, 0);
                hoursArray[0].EndTime = new TimeSpan((int)Schedule.SundayEndTime.HoursValue, (int)Schedule.SundayEndTime.MinutesValue, 0);

                hoursArray[1].StartTime = new TimeSpan((int)Schedule.MondayStartTime.HoursValue, (int)Schedule.MondayStartTime.MinutesValue, 0);
                hoursArray[1].EndTime = new TimeSpan((int)Schedule.MondayEndTime.HoursValue, (int)Schedule.MondayEndTime.MinutesValue, 0);

                hoursArray[2].StartTime = new TimeSpan((int)Schedule.TuesdayStartTime.HoursValue, (int)Schedule.TuesdayStartTime.MinutesValue, 0);
                hoursArray[2].EndTime = new TimeSpan((int)Schedule.TuesdayEndTime.HoursValue, (int)Schedule.TuesdayEndTime.MinutesValue, 0);

                hoursArray[3].StartTime = new TimeSpan((int)Schedule.WednesdayStartTime.HoursValue, (int)Schedule.WednesdayStartTime.MinutesValue, 0);
                hoursArray[3].EndTime = new TimeSpan((int)Schedule.WednesdayEndTime.HoursValue, (int)Schedule.WednesdayEndTime.MinutesValue, 0);

                hoursArray[4].StartTime = new TimeSpan((int)Schedule.ThursdayStartTime.HoursValue, (int)Schedule.ThursdayStartTime.MinutesValue, 0);
                hoursArray[4].EndTime = new TimeSpan((int)Schedule.ThursdayEndTime.HoursValue, (int)Schedule.ThursdayEndTime.MinutesValue, 0);

                hoursArray[5].StartTime = new TimeSpan((int)Schedule.FridayStartTime.HoursValue, (int)Schedule.FridayStartTime.MinutesValue, 0);
                hoursArray[5].EndTime = new TimeSpan((int)Schedule.FridayEndTime.HoursValue, (int)Schedule.FridayEndTime.MinutesValue, 0);

                EditedMother.MomHoursNannyNeeds = hoursArray;
                bl.UpdateMomDetails(EditedMother);
                EditedMother = new Mother();
                this.IsEnabled = false;

                MessageBox.Show("Update succeeded");
                dataGridToRefresh.ItemsSource = null;
                dataGridToRefresh.ItemsSource = bl.GetAllMoms();

                #endregion

            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }

        }

        private void FirstName_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //if user inserted somthing else than numbers - he will be alerted
            UiTools.Name_Error_Alerter(FirstName_TextBox);
        }
        private void FirstName_TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //to restore the text box to his to previous state (by designing)
            if (FirstName_TextBox.Foreground == Brushes.Red)
            {
                UiTools.Restore_Text_Box(FirstName_TextBox);
            }
        }

        private void LastName_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //if user inserted somthing else than numbers - he will be alerted
            UiTools.Name_Error_Alerter(LastName_TextBox);
        }

        private void LastName_TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //to restore the text box to his to previous state (by designing)
            if (LastName_TextBox.Foreground == Brushes.Red)
            {
                UiTools.Restore_Text_Box(LastName_TextBox);
            }
        }    
    }
}
