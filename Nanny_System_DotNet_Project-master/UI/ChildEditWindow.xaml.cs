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
    /// Interaction logic for ChildEditWindow.xaml
    /// </summary>
    public partial class ChildEditWindow : Window
    {
        BL.IBL bl;
        DataGrid DataGridToRefresh;
        public ChildEditWindow(Child childToEdit, DataGrid DataGridInput)
        {

            InitializeComponent();
            bl = BL.FactoryBL.GetBL();
            DataGridToRefresh = DataGridInput;

            ID_TextBox.Text = childToEdit.ChildID.ToString();
            MotherId_TextBox.Text = childToEdit.ChildMomID.ToString();
            Name_TextBox.Text = childToEdit.ChildName.ToString();
            birth_DatePicker.Text = childToEdit.ChildAge.Day + "/" + childToEdit.ChildAge.Month + "/" + childToEdit.ChildAge.Year;
            SpecialNeeds_CheckBox.IsChecked = childToEdit.ChildIsSpecialNeeds; // ido
            if (childToEdit.ChildIsSpecialNeeds)
            {
                SpecialNeeds_TextBox.Text = childToEdit.ChildTypesOfSpecialNeeds;
            }
         

            childToEdit = new Child();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Child EditedChild = new Child();

                EditedChild.ChildID = int.Parse(ID_TextBox.Text);
                EditedChild.ChildName = Name_TextBox.Text;
                EditedChild.ChildMomID = int.Parse(MotherId_TextBox.Text);
                EditedChild.ChildAge = new DateTime(birth_DatePicker.SelectedDate.Value.Year, birth_DatePicker.SelectedDate.Value.Month, birth_DatePicker.SelectedDate.Value.Day);
                EditedChild.ChildIsSpecialNeeds = (bool)SpecialNeeds_CheckBox.IsChecked;
                if (EditedChild.ChildIsSpecialNeeds)
                    SpecialNeeds_TextBox.SelectAll();
                EditedChild.ChildTypesOfSpecialNeeds = SpecialNeeds_TextBox.Text;

                bl.UpdateChildDetails(EditedChild);

                EditedChild = new Child();

                this.IsEnabled = false;


                DataGridToRefresh.ItemsSource = bl.GetAllChilds();
                MessageBox.Show("Update succeeded");
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }
        }    

        private void Name_TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //to restore the text box to his to previous state (by designing)
            if (Name_TextBox.Foreground == Brushes.Red)
            {
                UiTools.Restore_Text_Box(Name_TextBox);
            }
        }

        private void Name_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //if user inserted somthing else than numbers - he will be alerted
            UiTools.Name_Error_Alerter(Name_TextBox);
        }
    }
}
