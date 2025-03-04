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
    /// Interaction logic for NannyEditWindow.xaml
    /// </summary>
    public partial class NannyEditWindow : Window
    {
        IBL bl;
        DataGrid dataGridToRefresh;

        public NannyEditWindow(Nanny nannyToUpdate, DataGrid dataGridInput)
        {
            InitializeComponent();

            bl = BL.FactoryBL.GetBL();
            dataGridToRefresh = dataGridInput;

            ID_TextBox.Text = nannyToUpdate.NannyId.ToString();
            ID_TextBox.IsEnabled = false;
            FirstName_TextBox.Text = nannyToUpdate.NannyPrivateName;
            LastName_TextBox.Text = nannyToUpdate.NannyFamilyName;
            birth_DatePicker.Text = nannyToUpdate.NannyDateOfBirth.Day + "/" + nannyToUpdate.NannyDateOfBirth.Month + "/" + nannyToUpdate.NannyDateOfBirth.Year;
            PhoneNumber_TextBox.Text = "0" + nannyToUpdate.NannyPhoneNum.ToString();
            int IndexOfStreet = nannyToUpdate.NannyAdress.IndexOf(',');
            int IndexOfCity = nannyToUpdate.NannyAdress.LastIndexOf(',');
            Street_TextBox.Text = nannyToUpdate.NannyAdress.Substring(0, IndexOfStreet);
            City_TextBox.Text = nannyToUpdate.NannyAdress.Substring(IndexOfStreet + 2, IndexOfCity - IndexOfStreet - 2);
            Floor_TextBox.Text = nannyToUpdate.NannyFloor.ToString();
            Elevator_CheckBox.IsChecked = nannyToUpdate.NannyIsElevator;


            Experience_TextBox.Text = nannyToUpdate.NannyYearsOfExperience.ToString();
            MaxChildrens_TextBox.Text = nannyToUpdate.NannyMaxInfants.ToString();
            MinAge_TextBox.Text = nannyToUpdate.NannyMinInfantAge.ToString();
            MaxAge_TextBox.Text = nannyToUpdate.NannyMaxInfantAge.ToString();
            if (nannyToUpdate.NannyIsMOE)
                Vacation_ComboBox.SelectedIndex = 0;
            else Vacation_ComboBox.SelectedIndex = 1;


            MonthlySalary_TextBox.Text = nannyToUpdate.NannyMonthlySalary.ToString();
            HourlySalary_CheckBox.IsChecked = nannyToUpdate.NannyIsHourlySalary;
            if (nannyToUpdate.NannyIsHourlySalary)
            {
                HourlySalary_TextBox.Visibility = Visibility.Visible;
                HourlySalary_TextBox.Text = nannyToUpdate.NannyHourlySalary.ToString();
            }

            Recommendations_TextBox.Text = nannyToUpdate.NannyRecommendations;

            for (int i = 0; i < 6; i++)
            {
                if (nannyToUpdate.NannyWorkingDays[i])
                {
                    switch (i)
                    {
                        case 0:
                            Schedule.SundayCheckBox.IsChecked = true;
                            Schedule.SundayStartTime.HoursValue = nannyToUpdate.NannyWorkingHours[i].StartTime.Hours;
                            Schedule.SundayStartTime.MinutesValue = nannyToUpdate.NannyWorkingHours[i].StartTime.Minutes;
                            Schedule.SundayEndTime.HoursValue = nannyToUpdate.NannyWorkingHours[i].EndTime.Hours;
                            Schedule.SundayEndTime.MinutesValue = nannyToUpdate.NannyWorkingHours[i].EndTime.Minutes;
                            break;
                        case 1:
                            Schedule.MondayCheckBox.IsChecked = true;
                            Schedule.MondayStartTime.HoursValue = nannyToUpdate.NannyWorkingHours[i].StartTime.Hours;
                            Schedule.MondayStartTime.MinutesValue = nannyToUpdate.NannyWorkingHours[i].StartTime.Minutes;
                            Schedule.MondayEndTime.HoursValue = nannyToUpdate.NannyWorkingHours[i].EndTime.Hours;
                            Schedule.MondayEndTime.MinutesValue = nannyToUpdate.NannyWorkingHours[i].EndTime.Minutes;
                            break;
                        case 2:
                            Schedule.TuesdayCheckBox.IsChecked = true;
                            Schedule.TuesdayStartTime.HoursValue = nannyToUpdate.NannyWorkingHours[i].StartTime.Hours;
                            Schedule.TuesdayStartTime.MinutesValue = nannyToUpdate.NannyWorkingHours[i].StartTime.Minutes;
                            Schedule.TuesdayEndTime.HoursValue = nannyToUpdate.NannyWorkingHours[i].EndTime.Hours;
                            Schedule.TuesdayEndTime.MinutesValue = nannyToUpdate.NannyWorkingHours[i].EndTime.Minutes;
                            break;
                        case 3:
                            Schedule.WednesdayCheckBox.IsChecked = true;
                            Schedule.WednesdayStartTime.HoursValue = nannyToUpdate.NannyWorkingHours[i].StartTime.Hours;
                            Schedule.WednesdayStartTime.MinutesValue = nannyToUpdate.NannyWorkingHours[i].StartTime.Minutes;
                            Schedule.WednesdayEndTime.HoursValue = nannyToUpdate.NannyWorkingHours[i].EndTime.Hours;
                            Schedule.WednesdayEndTime.MinutesValue = nannyToUpdate.NannyWorkingHours[i].EndTime.Minutes;
                            break;
                        case 4:
                            Schedule.ThursdayCheckBox.IsChecked = true;
                            Schedule.ThursdayStartTime.HoursValue = nannyToUpdate.NannyWorkingHours[i].StartTime.Hours;
                            Schedule.ThursdayStartTime.MinutesValue = nannyToUpdate.NannyWorkingHours[i].StartTime.Minutes;
                            Schedule.ThursdayEndTime.HoursValue = nannyToUpdate.NannyWorkingHours[i].EndTime.Hours;
                            Schedule.ThursdayEndTime.MinutesValue = nannyToUpdate.NannyWorkingHours[i].EndTime.Minutes;
                            break;
                        case 5:
                            Schedule.FridayCheckBox.IsChecked = true;
                            Schedule.FridayStartTime.HoursValue = nannyToUpdate.NannyWorkingHours[i].StartTime.Hours;
                            Schedule.FridayStartTime.MinutesValue = nannyToUpdate.NannyWorkingHours[i].StartTime.Minutes;
                            Schedule.FridayEndTime.HoursValue = nannyToUpdate.NannyWorkingHours[i].EndTime.Hours;
                            Schedule.FridayEndTime.MinutesValue = nannyToUpdate.NannyWorkingHours[i].EndTime.Minutes;
                            break;

                        default:
                            break;




                    }
                }
            }

            nannyToUpdate = new Nanny();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Nanny updatedNanny = new Nanny();

            try
            {
                updatedNanny.NannyId = int.Parse(ID_TextBox.Text);
                updatedNanny.NannyPrivateName = FirstName_TextBox.Text;
                updatedNanny.NannyFamilyName = LastName_TextBox.Text;
                updatedNanny.NannyPhoneNum = int.Parse(PhoneNumber_TextBox.Text);
                updatedNanny.NannyAdress = Street_TextBox.Text + ", " + City_TextBox.Text + ", israel";
                updatedNanny.NannyFloor = int.Parse(Floor_TextBox.Text);
                updatedNanny.NannyIsElevator = (bool)Elevator_CheckBox.IsChecked;

                updatedNanny.NannyYearsOfExperience = int.Parse(Experience_TextBox.Text);
                updatedNanny.NannyMaxInfants = int.Parse(MaxChildrens_TextBox.Text);
                updatedNanny.NannyMinInfantAge = int.Parse(MinAge_TextBox.Text);
                updatedNanny.NannyMaxInfantAge = int.Parse(MaxAge_TextBox.Text);
                updatedNanny.NannyMonthlySalary = double.Parse(MonthlySalary_TextBox.Text);
                updatedNanny.NannyIsHourlySalary = (bool)HourlySalary_CheckBox.IsChecked;
                if (updatedNanny.NannyIsHourlySalary)
                    updatedNanny.NannyHourlySalary = double.Parse(HourlySalary_TextBox.Text);

                updatedNanny.NannyDateOfBirth = new DateTime(birth_DatePicker.SelectedDate.Value.Year, birth_DatePicker.SelectedDate.Value.Month, birth_DatePicker.SelectedDate.Value.Day);
                updatedNanny.NannyIsMOE = (Vacation_ComboBox.SelectedItem == MOE_ComboBoxItem) ? true : false;
                updatedNanny.NannyRecommendations = Recommendations_TextBox.Text;

                #region Time input
                updatedNanny.NannyWorkingDays[0] = (bool)Schedule.SundayCheckBox.IsChecked;
                updatedNanny.NannyWorkingDays[1] = (bool)Schedule.MondayCheckBox.IsChecked;
                updatedNanny.NannyWorkingDays[2] = (bool)Schedule.TuesdayCheckBox.IsChecked;
                updatedNanny.NannyWorkingDays[3] = (bool)Schedule.WednesdayCheckBox.IsChecked;
                updatedNanny.NannyWorkingDays[4] = (bool)Schedule.ThursdayCheckBox.IsChecked;
                updatedNanny.NannyWorkingDays[5] = (bool)Schedule.FridayCheckBox.IsChecked;

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

                updatedNanny.NannyWorkingHours = hoursArray;
                #endregion

                bl.UpdateNannyDetails(updatedNanny);
                updatedNanny = new Nanny();

                this.IsEnabled = false;

                if (dataGridToRefresh != null)
                {
                    dataGridToRefresh.ItemsSource = null;
                    dataGridToRefresh.ItemsSource = bl.GetAllNannies();
                }

                MessageBox.Show("Update succeeded");
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
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
