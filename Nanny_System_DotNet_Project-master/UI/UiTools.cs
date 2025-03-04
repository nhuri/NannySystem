using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace UI
{
    static class UiTools
    {
        public static string NumToDay(int num)
        {
            switch (num)
            {
                case 0:
                    return "Sunday";
                case 1:
                    return "Monday";
                case 2:
                    return "Tuesday";
                case 3:
                    return "Wednesday";
                case 4:
                    return "Thursday";
                case 5:
                    return "Friday";
                default:
                    return "";
            }
        }

        public static bool Id_Error_Alerter(TextBox textBox)
        {
            int id = 0;
            if (!int.TryParse(textBox.Text, out id) && textBox.Text != "")
            {
                textBox.BorderBrush = Brushes.Red;
                textBox.Foreground = Brushes.Red;
                textBox.Text = "Please enter numbers only";
                return true;
            }
            return false;
        }

        public static bool Name_Error_Alerter(TextBox textBox)
        {
            string TextBoxText = textBox.Text.ToLower();
            int TextBoxLength = TextBoxText.Length;
            if (TextBoxLength < 2 && textBox.Text != "")
            {
                textBox.BorderBrush = Brushes.Red;
                textBox.Foreground = Brushes.Red;
                textBox.FontSize = 13;
                textBox.Text = "Please enter a name with at least 2 letters";
                return true;
            }
            for (int i = 0; i < TextBoxLength; i++)
            {
                if (TextBoxText[i] < 97 || TextBoxText[i] > 122)
                {
                    textBox.BorderBrush = Brushes.Red;
                    textBox.Foreground = Brushes.Red;
                    textBox.FontSize = 14;
                    textBox.Text = "Please enter a english letters only";
                    return true;
                }
            }
            return false;
        }

        public static void Restore_Text_Box(TextBox textBox)
        {
            textBox.Text = "";
            textBox.Foreground = Brushes.Black;
            textBox.BorderBrush = Brushes.Gray;
        }


    }
}
