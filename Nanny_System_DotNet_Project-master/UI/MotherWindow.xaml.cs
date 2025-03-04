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
    /// Interaction logic for MotherWindow.xaml
    /// </summary>
    public partial class MotherWindow : Window
    {
        IBL bl;

        public MotherWindow()
        {
            InitializeComponent();
            bl = FactoryBL.GetBL();
            Mothers_DataGrid.ItemsSource = bl.GetAllMoms();
        }

        private void dou_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Mother MotherToSow = Mothers_DataGrid.SelectedItem as Mother;
            MotherCard motherCard = new MotherCard(MotherToSow, Mothers_DataGrid);
            motherCard.ShowDialog();
        }

        private void Filter_Btn_Click(object sender, RoutedEventArgs e)
        {
            bool[] filterDaysNeedNannyArr = new bool[6];
            if (Sunday_ListBoxItem.IsSelected)
                filterDaysNeedNannyArr[0] = true;
            if (Monday_ListBoxItem.IsSelected)
                filterDaysNeedNannyArr[1] = true;
            if (Tuesday_ListBoxItem.IsSelected)
                filterDaysNeedNannyArr[2] = true;
            if (Wednesday_ListBoxItem.IsSelected)
                filterDaysNeedNannyArr[3] = true;
            if (Thursday_ListBoxItem.IsSelected)
                filterDaysNeedNannyArr[4] = true;
            if (Friday_ListBoxItem.IsSelected)
                filterDaysNeedNannyArr[5] = true;

            Mothers_DataGrid.ItemsSource = BlTools.FilterMothers(filterDaysNeedNannyArr);


        }

        private void ShowAll_Btn_Click(object sender, RoutedEventArgs e)
        {
            Sunday_ListBoxItem.IsSelected = false;
            Monday_ListBoxItem.IsSelected = false;
            Tuesday_ListBoxItem.IsSelected = false;
            Wednesday_ListBoxItem.IsSelected = false;
            Thursday_ListBoxItem.IsSelected = false;
            Friday_ListBoxItem.IsSelected = false;

            Mothers_DataGrid.ItemsSource = bl.GetAllMoms();
        }

        private void AddNanny_Btn_Click(object sender, RoutedEventArgs e)
        {
            MotherAddWindow motherAddWindow = new MotherAddWindow(Mothers_DataGrid);
            motherAddWindow.ShowDialog();
        }
    }
}
