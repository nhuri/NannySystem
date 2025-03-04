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
using BL;
using BE;

namespace UI
{
    /// <summary>
    /// Interaction logic for MotherAddWindow.xaml
    /// </summary>
    public partial class MotherAddWindow : Window
    {
        IBL bl;
        Mother newMother;
        DataGrid dataGridToRefresh;

        public MotherAddWindow(DataGrid dataGridInput)
        {
            InitializeComponent();
            dataGridToRefresh = dataGridInput;
            newMother = new Mother();
            bl = BL.FactoryBL.GetBL();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                newMother.MomID = int.Parse(ID_TextBox.Text);
                newMother.MomFirstName = FirstName_TextBox.Text;
                newMother.MomFamilyName = LastName_TextBox.Text;
                newMother.MomPhoneNum = int.Parse(PhoneNumber_TextBox.Text);
                newMother.MomAdress = Street_TextBox.Text + ", " + City_TextBox.Text + ", israel";
                newMother.MomSearchAdress = NannyStreet_TextBox.Text + ", " + NannyCity_TextBox.Text + ", israel";
                newMother.MomComment = Comments_TextBox.Text;

                #region Time input
                newMother.MomDaysNannyNeeds[0] = (bool)MotherSchedule.SundayCheckBox.IsChecked;
                newMother.MomDaysNannyNeeds[1] = (bool)MotherSchedule.MondayCheckBox.IsChecked;
                newMother.MomDaysNannyNeeds[2] = (bool)MotherSchedule.TuesdayCheckBox.IsChecked;
                newMother.MomDaysNannyNeeds[3] = (bool)MotherSchedule.WednesdayCheckBox.IsChecked;
                newMother.MomDaysNannyNeeds[4] = (bool)MotherSchedule.ThursdayCheckBox.IsChecked;
                newMother.MomDaysNannyNeeds[5] = (bool)MotherSchedule.FridayCheckBox.IsChecked;

                List<WeeklyWorkSchedule> hoursArray = new List<WeeklyWorkSchedule>(6);
                for (int i = 0; i < 6; i++)
                    hoursArray.Insert(i, new WeeklyWorkSchedule());
                

              

                hoursArray[0].StartTime = new TimeSpan((int)MotherSchedule.SundayStartTime.HoursValue,(int)MotherSchedule.SundayStartTime.MinutesValue,0);
                hoursArray[0].EndTime = new TimeSpan((int)MotherSchedule.SundayEndTime.HoursValue, (int)MotherSchedule.SundayEndTime.MinutesValue, 0);

                hoursArray[1].StartTime = new TimeSpan((int)MotherSchedule.MondayStartTime.HoursValue, (int)MotherSchedule.MondayStartTime.MinutesValue, 0);
                hoursArray[1].EndTime = new TimeSpan((int)MotherSchedule.MondayEndTime.HoursValue, (int)MotherSchedule.MondayEndTime.MinutesValue, 0);

                hoursArray[2].StartTime = new TimeSpan((int)MotherSchedule.TuesdayStartTime.HoursValue, (int)MotherSchedule.TuesdayStartTime.MinutesValue, 0);
                hoursArray[2].EndTime = new TimeSpan((int)MotherSchedule.TuesdayEndTime.HoursValue, (int)MotherSchedule.TuesdayEndTime.MinutesValue, 0);

                hoursArray[3].StartTime = new TimeSpan((int)MotherSchedule.WednesdayStartTime.HoursValue, (int)MotherSchedule.WednesdayStartTime.MinutesValue, 0);
                hoursArray[3].EndTime = new TimeSpan((int)MotherSchedule.WednesdayEndTime.HoursValue, (int)MotherSchedule.WednesdayEndTime.MinutesValue, 0);

                hoursArray[4].StartTime = new TimeSpan((int)MotherSchedule.ThursdayStartTime.HoursValue, (int)MotherSchedule.ThursdayStartTime.MinutesValue, 0);
                hoursArray[4].EndTime = new TimeSpan((int)MotherSchedule.ThursdayEndTime.HoursValue, (int)MotherSchedule.ThursdayEndTime.MinutesValue, 0);

                hoursArray[5].StartTime = new TimeSpan((int)MotherSchedule.FridayStartTime.HoursValue, (int)MotherSchedule.FridayStartTime.MinutesValue, 0);
                hoursArray[5].EndTime = new TimeSpan((int)MotherSchedule.FridayEndTime.HoursValue, (int)MotherSchedule.FridayEndTime.MinutesValue, 0);

                newMother.MomHoursNannyNeeds = hoursArray;
#endregion


                bl.AddMom(newMother);
                dataGridToRefresh.ItemsSource = null;
                dataGridToRefresh.ItemsSource = bl.GetAllMoms();
                MessageBox.Show("addition successful");
                this.IsEnabled = false;
                newMother = new Mother();
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }

        }

        private void ID_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //if user inserted somthing else than numbers - he will be alerted
            UiTools.Id_Error_Alerter(ID_TextBox);
        }
        private void ID_TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //to restore the text box to his to previous state (by designing)
            if (ID_TextBox.Foreground == Brushes.Red)
            {
                UiTools.Restore_Text_Box(ID_TextBox);
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
