using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPSParseTool
{
    public partial class LoadGPSLog : Form
    {
        private int bufLength = 0;
        public LoadGPSLog(int length)
        {
            InitializeComponent();
            bufLength = length;
            ProgressBar1.Style = ProgressBarStyle.Continuous;
            ProgressBar1.Minimum = 0;
            ProgressBar1.Maximum = bufLength;
            ProgressBar1.Step = 1;
        }

        public void UpdateBar(int currentLength)
        {
            ProgressBar1.Value = bufLength - currentLength;
            PercentLabel.Text = bufLength.ToString() + " / " + ProgressBar1.Value.ToString();
        }

        public void EndBar()
        {
            this.Close();
        }
    }
}
