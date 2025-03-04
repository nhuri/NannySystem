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
using System.Collections.ObjectModel;
using System.Globalization;

namespace UI
{
    /// <summary>
    /// Interaction logic for ChildWindow.xaml
    /// </summary>
    public partial class ChildWindow : Window
    {
        IBL bl;
        public ChildWindow()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();
            Childs_DataGrid.ItemsSource = bl.GetAllChilds();

            List<Mother> mothersToShowInComboBox = new List<Mother>(bl.GetAllMoms());
            mothersToShowInComboBox.Insert(0, new Mother { MomFirstName = "All", MomFamilyName = "options" });
            MotherFilter_ComboBox.ItemsSource = mothersToShowInComboBox;


            Mother mother = MotherFilter_ComboBox.SelectedItem as Mother;



            MotherFilter_ComboBox.SelectedIndex = 0;
            isHaveNannyFilter_ComboBox.SelectedIndex = 0;
            SpecialNeedsFilter_ComboBox.SelectedIndex = 0;
        }

        private void Childs_DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Child ChildToShow = Childs_DataGrid.SelectedItem as Child;
            ChildCard childCard = new ChildCard(ChildToShow, Childs_DataGrid);
            childCard.ShowDialog();


        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if ((Slider)sender == MinAgeFilter_Slider)
                MinAgeFilter_TextBlock.Text = "Min Age (in months): " + (int)MinAgeFilter_Slider.Value;
            if ((Slider)sender == MaxAgeFilter_Slider)
                MaxAgeFilter_TextBlock.Text = "Max Age (in months): " + (int)MaxAgeFilter_Slider.Value;


        }

        private void Filter_Btn_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGridOfChilds();
            SimpleFilter_ComboBox.SelectedIndex = -1;
        }

        private void RefreshDataGridOfChilds()
        {
            if (MinAgeFilter_Slider.Value > MaxAgeFilter_Slider.Value)
                throw new Exception("Error, The minimum age must be less than the maximum age");


            bool? isHaveNanny;
            switch (isHaveNannyFilter_ComboBox.SelectedIndex)
            {
                case (0):
                    isHaveNanny = null;
                    break;
                case (1):
                    isHaveNanny = true;
                    break;
                case (2):
                    isHaveNanny = false;
                    break;
                default:
                    isHaveNanny = null;
                    break;
            }

            bool? isHaveSpecialNeeds;
            switch (SpecialNeedsFilter_ComboBox.SelectedIndex)
            {
                case (0):
                    isHaveSpecialNeeds = null;
                    break;
                case (1):
                    isHaveSpecialNeeds = true;
                    break;
                case (2):
                    isHaveSpecialNeeds = false;
                    break;
                default:
                    isHaveSpecialNeeds = null;
                    break;
            }
            Mother mother = null;
            if (MotherFilter_ComboBox.SelectedIndex != 0)
                mother = MotherFilter_ComboBox.SelectedItem as Mother;

            Childs_DataGrid.ItemsSource = BlTools.FilterChilds(isHaveNanny, isHaveSpecialNeeds, (int)MaxAgeFilter_Slider.Value, (int)MinAgeFilter_Slider.Value, mother);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddChild newAddChildWindow = new AddChild(Childs_DataGrid);
            newAddChildWindow.ShowDialog();
        }

        private void Filter_ComBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (SimpleFilter_ComboBox.SelectedIndex)
            {
                case (0):
                    isHaveNannyFilter_ComboBox.SelectedIndex = 0;
                    SpecialNeedsFilter_ComboBox.SelectedIndex = 0;
                    MotherFilter_ComboBox.SelectedIndex = 0;
                    MaxAgeFilter_Slider.Value = 156;
                    MinAgeFilter_Slider.Value = 3;
                    RefreshDataGridOfChilds();
                    break;
                case (1):
                    isHaveNannyFilter_ComboBox.SelectedIndex = 0;
                    SpecialNeedsFilter_ComboBox.SelectedIndex = 1;
                    MotherFilter_ComboBox.SelectedIndex = 0;
                    MaxAgeFilter_Slider.Value = 156;
                    MinAgeFilter_Slider.Value = 3;
                    RefreshDataGridOfChilds();
                    break;
                case (2):
                    isHaveNannyFilter_ComboBox.SelectedIndex = 1;
                    SpecialNeedsFilter_ComboBox.SelectedIndex = 0;
                    MotherFilter_ComboBox.SelectedIndex = 0;
                    MaxAgeFilter_Slider.Value = 156;
                    MinAgeFilter_Slider.Value = 3;
                    RefreshDataGridOfChilds();
                    break;
                default:
                  
                    break;
            } 
            
        }
    }
    public class ConvertMomIdToMomName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int Momid = (int)value;
            Mother mother = BlTools.GetMother(Momid);
            return mother.MomFirstName + " " + mother.MomFamilyName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConvertBoolToCheakBox : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new CheckBox{IsChecked = (bool)value, IsEnabled = false};       
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
