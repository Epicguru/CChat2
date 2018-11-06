using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrapChat
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
        }

        public void SetTitle(string s)
        {
            this.Text = s.Trim();
        }

        /// <summary>
        /// Sets the progress bar percentage here p is in the range 0 to 1. 
        /// </summary>
        /// <param name="p"></param>
        public void SetPercentage(float p)
        {
            progressBar.Value = (int)Math.Round(p * 100f);
        }

        public void SetDetailsText(string s)
        {
            detailsText.Text = s.Trim();
        }

        public void SetProgressText(string s)
        {
            progressText.Text = s.Trim();
        }

        public void SetStyle(ProgressBarStyle style)
        {
            progressBar.Style = style;
        }

        /// <summary>
        /// Enables or disables the top bar (that contains the minimize, maximize and 'red X' buttons).
        /// </summary>
        /// <param name="enabled"></param>
        public void SetControlsEnabled(bool enabled)
        {
            this.ControlBox = enabled;
        }
    }
}
