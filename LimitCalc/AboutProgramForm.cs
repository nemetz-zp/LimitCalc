using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LimitCalc
{
    public partial class AboutProgramForm : Form
    {
        private Form1 form1;

        public AboutProgramForm(Form1 parentForm)
        {
            InitializeComponent();
            label1.Text += "\nІдеальне рішення для тих кого вже дістало ОБЛЕНЕРГО або власна бухгалтерія.";

            StartPosition = FormStartPosition.Manual;

            if(parentForm != null)
            {
                Location = new Point(parentForm.Location.X + (parentForm.Width - Width) / 2, parentForm.Location.Y + (parentForm.Height - Height) / 2);
            }
        }
    }
}
