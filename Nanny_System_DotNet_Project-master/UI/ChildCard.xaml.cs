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
    /// Interaction logic for ChildCard.xaml
    /// </summary>
    public partial class ChildCard : Window
    {
        Child ChildOfCard;
        IBL bl;
        DataGrid DataGridToRefresh;
        public ChildCard(Child ChildToShow, DataGrid DataGridInput)
        {
            InitializeComponent();
            bl = FactoryBL.GetBL();
            DataGridToRefresh = DataGridInput;
            ChildOfCard = ChildToShow.GetCopy();

            ID_TextBox.Text = ChildToShow.ChildID.ToString();
            MotherID_TextBox.Text = ChildToShow.ChildMomID.ToString();
            Name_TextBox.Text = ChildToShow.ChildName.ToString();
            Birth_TextBox.Text = ChildToShow.ChildAge.Day + "/" + ChildToShow.ChildAge.Month + "/" + ChildToShow.ChildAge.Year;
            if (ChildToShow.ChildIsSpecialNeeds)
            {
                SpecialNeeds_TextBox.Text = ChildToShow.ChildTypesOfSpecialNeeds;
            }
            else
            {
                SpecialNeeds_TextBox.Text = "There are no special needs";
            }

            ChildToShow = new Child();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((Button)sender == Update_Btn)
                {
                    ChildEditWindow childEditWindow = new ChildEditWindow(ChildOfCard, DataGridToRefresh);
                    childEditWindow.ShowDialog();
                    this.Close();
                }
                if ((Button)sender == Delete_Btn)
                {
                    bl.DeleteChild(ChildOfCard.ChildID);
                    ChildOfCard = null;
                    DataGridToRefresh.ItemsSource = null;
                    DataGridToRefresh.ItemsSource = bl.GetAllChilds();
                    MessageBox.Show("Successfully deleted");
                    this.Close();
                }

            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }
        }
    }
}
