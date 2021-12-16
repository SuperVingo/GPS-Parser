using System;
using System.Windows.Forms;
using System.IO;

namespace GPSParseTool
{
    public partial class Serial : Form
    {
        public ReceivedSerialDataEventHandler ReceivedSerialDataEvent;

        public Serial()
        {
            InitializeComponent();
            SerialText.ScrollBars = ScrollBars.Both;
            SerialText.ReadOnly = true;
        }

        public void ReceivedSerialData(String str)
        {
            if (this.InvokeRequired)
            {
                ReceivedSerialDataEventHandler eh = new ReceivedSerialDataEventHandler(ReceivedSerialData);
                this.Invoke(eh, new object[] { str });
            }
            else
            {
                this.SerialText.AppendText(str);
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            SerialText.Clear();
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            string FileName;
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Title = "Save GPS Log";
            saveFile.OverwritePrompt = true;
            saveFile.Filter = "Text File(*.txt)|*.txt ";

            if(saveFile.ShowDialog() == DialogResult.OK)
            {
                FileName = saveFile.FileName;
                using (StreamWriter streamWrite = new StreamWriter(FileName))
                {
                    streamWrite.Write(SerialText.Text);
                }
            }
        }
    }
}
