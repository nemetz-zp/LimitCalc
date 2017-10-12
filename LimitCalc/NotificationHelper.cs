using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LimitCalc
{
    public class NotificationHelper
    {
        public static void ShowError(string msg)
        {
            MessageBox.Show(msg, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowInfo(string msg)
        {
            MessageBox.Show(msg, "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult AskYesNo(string question)
        {
            return MessageBox.Show(question, "Підстердження дії", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
