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
    /// Interaction logic for NannyWindow.xaml
    /// </summary>
    public partial class NannyWindow : Window
    {

        IBL bl;
        public NannyWindow()
        {
            InitializeComponent();
            bl = FactoryBL.GetBL();

            Nannys_DataGrid.ItemsSource = bl.GetAllNannies();
            MaxNumberOfChildren_ComboBox.SelectedIndex = 7;

        }

        private void dou_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Nanny NannyToShow = Nannys_DataGrid.SelectedItem as Nanny;
            NannyCard nannyCard = new NannyCard(NannyToShow, Nannys_DataGrid);
            nannyCard.ShowDialog();
        }

        private void Filter_Btn_Click(object sender, RoutedEventArgs e)
        {
            bool[] filterWorkDayArr = new bool[6];
            if (Sunday_ListBoxItem.IsSelected)
                filterWorkDayArr[0] = true;
            if (Monday_ListBoxItem.IsSelected)
                filterWorkDayArr[1] = true;
            if (Tuesday_ListBoxItem.IsSelected)
                filterWorkDayArr[2] = true;
            if (Wednesday_ListBoxItem.IsSelected)
                filterWorkDayArr[3] = true;
            if (Thursday_ListBoxItem.IsSelected)
                filterWorkDayArr[4] = true;
            if (Friday_ListBoxItem.IsSelected)
                filterWorkDayArr[5] = true;

            int MaxNumberOfChilds = 1000;
            if (MaxNumberOfChildren_ComboBox.SelectedIndex < 7)
                MaxNumberOfChilds = 2 + MaxNumberOfChildren_ComboBox.SelectedIndex * 2;

            Nannys_DataGrid.ItemsSource = BlTools.FilterNannys((int)MinAgeFilter_Slider.Value,
                                                      (int)MinExperienceFilter_Slider.Value,
                                                      (int)MinAgeOfChilds_Slider.Value,
                                                      (int)MaxAgeOfChilds_Slider.Value,
                                                      MaxNumberOfChilds,
                                                      filterWorkDayArr);
            SimpleFilter_ComboBox.SelectedIndex = -1;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           if ((Slider)sender == MinAgeFilter_Slider)
            {
                MinAgeFilter_TextBlock.Text = "Min Age(in years): " + (int)MinAgeFilter_Slider.Value;
            }
            if ((Slider)sender == MinAgeOfChilds_Slider)
            {
                MinAgeOfChildsFilter_TextBlock.Text = "Min Age of Childs(in months): " + (int)MinAgeOfChilds_Slider.Value;
            }
            if ((Slider)sender == MaxAgeOfChilds_Slider)
            {
                MaxAgeOfChilds_TextBlock.Text = "Max Age of Childs(in months): " + (int)MaxAgeOfChilds_Slider.Value;
            }
            if ((Slider)sender == MinExperienceFilter_Slider)
            {
                MinExperienceFilter_TextBlock.Text = "Min years of Experience: " + (int)MinExperienceFilter_Slider.Value;
            }
        }

        private void SimpleFilter_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MaxNumberOfChildren_ComboBox.SelectedIndex = 7;
            MinAgeFilter_Slider.Value = 18;
            MinAgeOfChilds_Slider.Value = 3;
            MaxAgeOfChilds_Slider.Value = 156;
            MinExperienceFilter_Slider.Value = 0;

            Sunday_ListBoxItem.IsSelected = false;
            Monday_ListBoxItem.IsSelected = false;
            Tuesday_ListBoxItem.IsSelected = false;
            Wednesday_ListBoxItem.IsSelected = false;
            Thursday_ListBoxItem.IsSelected = false;
            Friday_ListBoxItem.IsSelected = false;

            if (SimpleFilter_ComboBox.SelectedIndex == 0)
                Nannys_DataGrid.ItemsSource = bl.GetAllNannies();
            if (SimpleFilter_ComboBox.SelectedIndex == 1)
                Nannys_DataGrid.ItemsSource = BlTools.AllNanniesAllowsHourlySalary();
        }

        private void AddNanny_Btn_Click(object sender, RoutedEventArgs e)
        {
            NannyAddWindow nannyAddWindow = new NannyAddWindow(Nannys_DataGrid);
            nannyAddWindow.ShowDialog();
        }
    }
}
