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
    public partial class LoadingProgramForm : Form
    {
        public void ChangeLoadProgress(string msg, int loadingValue)
        {
            if (!string.IsNullOrWhiteSpace(msg))
            {
                progressMessage.Text += msg + "\n";
                panel1.Update();
            }

            currentProgressBar.Value += loadingValue;
        }

        public void CompleteProcess()
        {
            currentProgressBar.Value = 100;
        }

        public LoadingProgramForm(Form parentForm = null)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.Manual;
            Width = 462;
            Height = 245;

            if (parentForm != null)
            {
                Location = new Point(parentForm.Location.X + (parentForm.Width - Width) / 2, parentForm.Location.Y + (parentForm.Height - Height) / 2);
            }
        }
    }
}
