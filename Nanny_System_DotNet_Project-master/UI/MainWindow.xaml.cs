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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BL.BlTools.LoadData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((Button)sender == MothersBtn)
            {
                MotherWindow motherWindow = new MotherWindow();
                motherWindow.Show();
            }
            if ((Button)sender == ChildsBtn)
            {
                ChildWindow childWindow = new ChildWindow();
                childWindow.Show();
            }

            if ((Button)sender == NannysBtn)
            {
                NannyWindow nannyWindow = new NannyWindow();
                nannyWindow.Show();
            }

            if ((Button)sender == ContractsBtn)
            {
                ContractWindow contractWindow = new ContractWindow();
                contractWindow.Show();
            }
            if ((Button)sender == GroupingBtn)
            {
                GroupingWindow groupingWindow = new GroupingWindow();
                groupingWindow.Show();
            }

        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if ((Button)sender == MothersBtn)
            {
                MotherTitle.FontSize *= 1.75;
            }
            if ((Button)sender == ChildsBtn)
            {
                ChildrenTitle.FontSize *= 1.75;
            }

            if ((Button)sender == NannysBtn)
            {
                NannysTitle.FontSize *= 1.75;
            }

            if ((Button)sender == ContractsBtn)
            {
                ContractsTitle.FontSize *= 1.75;
            }
            if ((Button)sender == GroupingBtn)
            {
                GroupingTitle.FontSize *= 1.75;
            }
        }

        private void Button_MyMouseLeave(object sender, MouseEventArgs e)
        {
            if ((Button)sender == MothersBtn)
            {
                MotherTitle.FontSize /= 1.75;
            }
            if ((Button)sender == ChildsBtn)
            {
                ChildrenTitle.FontSize /= 1.75;
            }

            if ((Button)sender == NannysBtn)
            {
                NannysTitle.FontSize /= 1.75;
            }

            if ((Button)sender == ContractsBtn)
            {
                ContractsTitle.FontSize /= 1.75;
            }
            if ((Button)sender == GroupingBtn)
            {
                GroupingTitle.FontSize /= 1.75;
            }

        }
    }
}
