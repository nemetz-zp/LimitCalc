using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LimitCalc
{
    public partial class WaitForm : Form
    {
        public override void Refresh()
        {
            Thread.Sleep(progressBar1.MarqueeAnimationSpeed);
            base.Refresh();
        }

        public WaitForm(Form parentForm = null)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.Manual;
            //Width = 477;
            //Height = 128;

            if (parentForm != null)
            {
                Location = new Point(parentForm.Location.X + (parentForm.Width - Width) / 2, parentForm.Location.Y + (parentForm.Height - Height) / 2);
            }
        }
    }
}
