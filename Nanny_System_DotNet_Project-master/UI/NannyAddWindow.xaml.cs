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
    /// Interaction logic for NannyAddWindow.xaml
    /// </summary>
    public partial class NannyAddWindow : Window
    {
        Nanny newNanny;
        BL.IBL bl;
        DataGrid dataGridToRefresh;

        public NannyAddWindow(DataGrid dataGridInput)
        {
            InitializeComponent();
            newNanny = new Nanny();
            dataGridToRefresh = dataGridInput;
            bl = BL.FactoryBL.GetBL();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                newNanny.NannyId = int.Parse(ID_TextBox.Text);
                newNanny.NannyPrivateName = FirstName_TextBox.Text;
                newNanny.NannyFamilyName = LastName_TextBox.Text;
                newNanny.NannyPhoneNum = int.Parse(PhoneNumber_TextBox.Text);
                newNanny.NannyAdress = Street_TextBox.Text + ", " + City_TextBox.Text + ", israel";
                newNanny.NannyFloor = int.Parse(Floor_TextBox.Text);
                newNanny.NannyIsElevator = (bool)Elevator_CheckBox.IsChecked;

                newNanny.NannyYearsOfExperience = int.Parse(Experience_TextBox.Text);
                newNanny.NannyMaxInfants = int.Parse(MaxChildrens_TextBox.Text);
                newNanny.NannyMinInfantAge = int.Parse(MinAge_TextBox.Text);
                newNanny.NannyMaxInfantAge = int.Parse(MaxAge_TextBox.Text);
                newNanny.NannyMonthlySalary = int.Parse(MonthlySalary_TextBox.Text);
                newNanny.NannyIsHourlySalary = (bool)HourlySalary_CheckBox.IsChecked;
                if (newNanny.NannyIsHourlySalary)
                    newNanny.NannyHourlySalary = int.Parse(HourlySalary_TextBox.Text);

                newNanny.NannyDateOfBirth = new DateTime(birth_DatePicker.SelectedDate.Value.Year, birth_DatePicker.SelectedDate.Value.Month, birth_DatePicker.SelectedDate.Value.Day);
                newNanny.NannyIsMOE = (Vacation_ComboBox.SelectedItem == MOE_ComboBoxItem) ? true : false;
                newNanny.NannyRecommendations = Recommendations_TextBox.Text;

                #region Time input
                newNanny.NannyWorkingDays[0] = (bool)Schedule.SundayCheckBox.IsChecked;
                newNanny.NannyWorkingDays[1] = (bool)Schedule.MondayCheckBox.IsChecked;
                newNanny.NannyWorkingDays[2] = (bool)Schedule.TuesdayCheckBox.IsChecked;
                newNanny.NannyWorkingDays[3] = (bool)Schedule.WednesdayCheckBox.IsChecked;
                newNanny.NannyWorkingDays[4] = (bool)Schedule.ThursdayCheckBox.IsChecked;
                newNanny.NannyWorkingDays[5] = (bool)Schedule.FridayCheckBox.IsChecked;

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

                newNanny.NannyWorkingHours = hoursArray;
                #endregion

                bl.AddNanny(newNanny);
                dataGridToRefresh.ItemsSource = null;
                dataGridToRefresh.ItemsSource = bl.GetAllNannies();

                MessageBox.Show("addition successful");
                this.IsEnabled = false;
                newNanny = new Nanny();
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }



        }

        private void ID_TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //to restore the text box to his to previous state (by designing)
            if (ID_TextBox.Foreground == Brushes.Red)
            {
                UiTools.Restore_Text_Box(ID_TextBox);
            }
        }

        private void FirstName_TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //to restore the text box to his to previous state (by designing)
            if (FirstName_TextBox.Foreground == Brushes.Red)
            {
                UiTools.Restore_Text_Box(FirstName_TextBox);
            }
        }

        private void LastName_TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //to restore the text box to his to previous state (by designing)
            if (LastName_TextBox.Foreground == Brushes.Red)
            {
                UiTools.Restore_Text_Box(LastName_TextBox);
            }
        }

        private void ID_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //if user inserted somthing else than numbers - he will be alerted
            UiTools.Id_Error_Alerter(ID_TextBox);
        }

        private void FirstName_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //if user inserted somthing else than numbers - he will be alerted
            UiTools.Name_Error_Alerter(FirstName_TextBox);
        }

        private void LastName_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //if user inserted somthing else than numbers - he will be alerted
            UiTools.Name_Error_Alerter(LastName_TextBox);
        }
    }
}
