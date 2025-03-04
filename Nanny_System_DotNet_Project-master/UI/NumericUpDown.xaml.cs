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
    /// Interaction logic for NumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown : UserControl
    {

        private int? hoursValue;
        private int? minutesValue;


        public int? HoursValue
        {
            get { return hoursValue; }
            set
            {
                if (value > 23)
                    hoursValue = 23;

                else if (value < 0)
                    hoursValue = 0;
                else
                    hoursValue = value;

                if (hoursValue != null)
                {
                    if (hoursValue < 10)
                        Hour_TextBox.Text = "0" + hoursValue.ToString();
                    else
                        Hour_TextBox.Text = hoursValue.ToString();
                }
                else
                    Hour_TextBox.Text = "";
            }
        }

        public int? MinutesValue
        {
            get { return minutesValue; }

            set
            {
                if (value > 59)
                    minutesValue = 59;

                else if (value < 0)
                    minutesValue = 0;
                else
                    minutesValue = value;

                if (minutesValue != null)
                {
                    if (minutesValue<10)
                        Minute_TextBox.Text = "0" + minutesValue.ToString();
                    else
                        Minute_TextBox.Text = minutesValue.ToString();
                }
                else
                    Minute_TextBox.Text = "";
            }
        }

        public NumericUpDown()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((Button)sender == LeftTop_Btn)
                HoursValue++;
            else if ((Button)sender == LeftBottom_Btn)
                HoursValue--;
            else if ((Button)sender == RightTop_Btn)
                MinutesValue++;
            else if ((Button)sender == RightBottom_Btn)
                MinutesValue--;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int myVal;
            TextBox a = (TextBox)sender;
            if (a == Hour_TextBox)
            {
                if (a.Text == null || a.Text == "" || a.Text == "-")
                {
                    HoursValue = 0;
                    return;
                }

                if (!int.TryParse(Hour_TextBox.Text, out myVal))
                    Hour_TextBox.Text = HoursValue.ToString();
                else HoursValue = myVal;
            }

            if (a == Minute_TextBox)
            {
                if (a.Text == null || a.Text == "" || a.Text == "-")
                {
                    minutesValue = 0;
                    return;
                }

                if (!int.TryParse(Minute_TextBox.Text, out myVal))
                    Minute_TextBox.Text = MinutesValue.ToString();
                else MinutesValue = myVal;
            }
           
          
        }

        //private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if (hoursValue<10)
        //        Hour_TextBox.Text ="0" + Convert.ToString(hoursValue);
        //    if (minutesValue < 10)
        //        Minute_TextBox.Text = "0" + minutesValue.ToString();

        //}
    }
}
