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

namespace UI
{
    /// <summary>
    /// Interaction logic for AddChild.xaml
    /// </summary>
    public partial class AddChild : Window
    {
        Child newChild;
        BL.IBL bl;
        DateTime date = DateTime.Now;
        DataGrid dataGridToRefresh;

        public AddChild(DataGrid dataGridInput)
        {
            InitializeComponent();
            newChild = new Child();
            dataGridToRefresh = dataGridInput;
            bl = BL.FactoryBL.GetBL();
            //to bind the end date to now so the user will can't be choose a date after today 
            birth_DatePicker.DataContext = date;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                newChild.ChildID = int.Parse(ID_TextBox.Text);
                newChild.ChildName = Name_TextBox.Text;
                newChild.ChildMomID = int.Parse(MotherId_TextBox.Text);
                newChild.ChildAge = new DateTime(birth_DatePicker.SelectedDate.Value.Year, birth_DatePicker.SelectedDate.Value.Month, birth_DatePicker.SelectedDate.Value.Day);
                newChild.ChildIsSpecialNeeds = (bool)SpecialNeeds_CheckBox.IsChecked;
                if (newChild.ChildIsSpecialNeeds)
                    newChild.ChildTypesOfSpecialNeeds = SpecialNeeds_TextBox.Text;

                bl.AddChild(newChild);
                dataGridToRefresh.ItemsSource = bl.GetAllChilds();
                MessageBox.Show("addition successful");
                this.IsEnabled = false;
                newChild = new Child();
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void Name_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //if user inserted somthing else than letters - he will be alerted
            UiTools.Name_Error_Alerter(Name_TextBox);
        }

        private void Name_TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //to restore the text box to his to previous state (by designing)
            if (Name_TextBox.Foreground == Brushes.Red)
            {
                Name_TextBox.FontSize = 18;
                UiTools.Restore_Text_Box(Name_TextBox);
             
            }
        }

        private void MotherId_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //if user inserted somthing else than numbers - he will be alerted
            UiTools.Id_Error_Alerter(MotherId_TextBox);
        }

        private void MotherId_TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //to restore the text box to his to previous state (by designing)
            if (MotherId_TextBox.Foreground == Brushes.Red)
            {
                UiTools.Restore_Text_Box(MotherId_TextBox); 
            }
        }
    }

    public class ConvertIsCheckedToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return Visibility.Visible;
            return Visibility.Hidden;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
      

