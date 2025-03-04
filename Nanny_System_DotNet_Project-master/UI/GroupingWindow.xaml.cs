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
using System.Threading;

namespace UI
{
    /// <summary>
    /// Interaction logic for GroupingWindow.xaml
    /// </summary>
    public partial class GroupingWindow : Window
    {

        public GroupingWindow()
        {
            InitializeComponent();
        }

       

        private void Nannies_Button_Click(object sender, RoutedEventArgs e)
        {
            Contracts_Grouping_ListView.Visibility = Visibility.Collapsed;
            All_Contracts_TextBlock.Visibility = Visibility.Collapsed;
            Range_Between_Groups_TextBlock.Visibility = Visibility.Collapsed;
            DistanceGrouper_Slider.Visibility = Visibility.Collapsed;
            Sort_Contracts_CheckBox.Visibility = Visibility.Collapsed;

            Nannies_Grouping_ListView.Visibility = Visibility.Visible;
            Sort_Nannies_TextBlock.Visibility = Visibility.Visible;
            Sort_Nannies_CheckBox.Visibility = Visibility.Visible;
            All_Nannies_TextBlock.Visibility = Visibility.Visible;
            Max_Nannies_TextBlock.Visibility = Visibility.Visible;
            Max_Nannies_CheckBox.Visibility = Visibility.Visible; 
            NannyFillListView(false, false);
        }

           
        private void Sort_Nannies_CheckBox_Click(object sender, RoutedEventArgs e)
        {
            NannyFillListView((bool)Max_Nannies_CheckBox.IsChecked, (bool)Sort_Nannies_CheckBox.IsChecked);
        }

        private void Max_Nannies_CheckBox_Click(object sender, RoutedEventArgs e)
        {
            NannyFillListView((bool)Max_Nannies_CheckBox.IsChecked, (bool)Sort_Nannies_CheckBox.IsChecked);
        }

        private void Sort_Contracts_CheckBox_Click(object sender, RoutedEventArgs e)
        {
            ContractFillListView((bool)Sort_Contracts_CheckBox.IsChecked, (int)DistanceGrouper_Slider.Value);
        }

        private void Contracts_Button_Click(object sender, RoutedEventArgs e)
        {
           
            All_Contracts_TextBlock.Visibility = Visibility.Visible;
            Range_Between_Groups_TextBlock.Visibility = Visibility.Visible;
            DistanceGrouper_Slider.Visibility = Visibility.Visible;
            Sort_Contracts_CheckBox.Visibility = Visibility.Visible;
            Sort_Nannies_TextBlock.Visibility = Visibility.Visible;

            Nannies_Grouping_ListView.Visibility = Visibility.Collapsed;
            Sort_Nannies_CheckBox.Visibility = Visibility.Collapsed;
            All_Nannies_TextBlock.Visibility = Visibility.Collapsed;
            Max_Nannies_TextBlock.Visibility = Visibility.Collapsed;
            Max_Nannies_CheckBox.Visibility = Visibility.Collapsed;

            ContractFillListView((bool)Sort_Contracts_CheckBox.IsChecked, (int)DistanceGrouper_Slider.Value);
        }


        private void NannyFillListView(bool max, bool sort)
        {
            Nannies_Grouping_ListView.Items.Clear();
            IEnumerable<IGrouping<int, Nanny>> v = BlTools.GroupNanniesByChildMonths(max, sort);
            foreach (var item in v)
            {
                Nannies_Grouping_ListView.Items.Add(item);
            }
        }

        private void DistanceGrouper_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
            Range_Between_Groups_TextBlock.Text = $"Range between groups: {(int)DistanceGrouper_Slider.Value}";
            if (All_Contracts_TextBlock.Visibility == Visibility.Collapsed)
            {
                return;
            }
        }

        private void ContractFillListView(bool sort, int range)
        {

            if (Contracts_Grouping_ListView != null)
            {
                Contracts_Grouping_ListView.Items.Clear();
                wait_image.Visibility = Visibility.Visible;
            }
            int sliderValue = (int)DistanceGrouper_Slider.Value;
            new Thread(() =>
            {
                List<IGrouping<int, Contract>> v = BlTools.GroupContractByDistance(sort, sliderValue).ToList();
                Dispatcher.Invoke(new Action(() =>                
                {
                    try
                    {
                        wait_image.Visibility = Visibility.Collapsed;
                        Contracts_Grouping_ListView.Visibility = Visibility.Visible;
                        foreach (var item in v)
                        {
                     
                            Contracts_Grouping_ListView.Items.Add(item);
                        }
                    }
                    catch (Exception n)
                    {
                        MessageBox.Show(n.Message);
                    }
                }));
            }).Start();

        }

       
    }


    public class ConvertIntToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int topValue = (int)value;
            if (topValue == 3)
                return "3 Months";
            return $"{topValue - 2} - {topValue} Months";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConvertInt1ToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int topValue = (int)value;
            Slider DistanceGrouper_Slider = parameter as Slider;
            double sliderValue = DistanceGrouper_Slider.Value;
            return $"{topValue - sliderValue} - {topValue} Meters";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
