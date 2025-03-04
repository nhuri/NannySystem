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
    /// Interaction logic for ContractWindow.xaml
    /// </summary>
    public partial class ContractWindow : Window
    {
        IBL bl;
        public ContractWindow()
        {
            InitializeComponent();

            bl = new BLֹֹ_imp();
            All_Contracts_DataGrid.ItemsSource = bl.GetAllContracts();
        }

        private void All_Contracts_DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Contract contractToShow = All_Contracts_DataGrid.SelectedItem as Contract;
            ContractCard contractCard = new ContractCard(contractToShow, All_Contracts_DataGrid);
            contractCard.ShowDialog();
        }

        private void AddContract_Btn_Click(object sender, RoutedEventArgs e)
        {
            ContractAddWindow contractAddWindow = new ContractAddWindow(All_Contracts_DataGrid);
            contractAddWindow.Show();
        }
    }
}
