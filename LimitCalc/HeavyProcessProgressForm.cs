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
    public partial class HeavyProcessProgressForm : Form
    {
        public void ChangeProgress(string msg, int step)
        {
            if(!string.IsNullOrWhiteSpace(msg))
            {
                msgLabel.Text = msg;
            }

            progressBar1.Value += step;
        }

        public int ProgressRest
        {
            get
            {
                return progressBar1.Maximum - progressBar1.Value;
            }
        }

        public void CompleteProgress()
        {
            progressBar1.Value = progressBar1.Maximum;
        }

        public HeavyProcessProgressForm(Form parentForm = null)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.Manual;
            Width  = 676;
            Height = 82;

            if (parentForm != null)
            {
                Location = new Point(parentForm.Location.X + (parentForm.Width - Width) / 2, parentForm.Location.Y + (parentForm.Height - Height) / 2);
            }
        }
    }
}
