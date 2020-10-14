using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Runtime.InteropServices;

namespace Serial
{
    public delegate void ReceivedSerialDataEventHandler(String str);

    public partial class Main : Form
    {
        private static Serial serialForm = new Serial();
        public ReceivedSerialDataEventHandler ReceivedSerialDataEvent;

        public Main()
        {
            InitializeComponent();
            
            serialForm.Show();
            this.ReceivedSerialDataEvent += new ReceivedSerialDataEventHandler(serialForm.ReceivedSerialData);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            GetSerialPort();
            RateCombo.SelectedIndex = 0;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        protected override void WndProc(ref Message m)
        { 
            UInt32 WM_DEVICECHANGE = 0x0219;
            UInt32 DBT_DEVTUP_VOLUME = 0x02;
            UInt32 DBT_DEVICEARRIVAL = 0x8000;
            UInt32 DBT_DEVICEREMOVECOMPLETE = 0x8004;

            if ((m.Msg == WM_DEVICECHANGE) && (m.WParam.ToInt32() == DBT_DEVICEARRIVAL))
            {
                //int m_Count = 0;
                int devType = Marshal.ReadInt32(m.LParam, 4); 

                if (devType == DBT_DEVTUP_VOLUME)
                {
                    GetSerialPort();
                }
            }

            if ((m.Msg == WM_DEVICECHANGE) && (m.WParam.ToInt32() == DBT_DEVICEREMOVECOMPLETE))
            {
                int devType = Marshal.ReadInt32(m.LParam, 4);
                if (devType == DBT_DEVTUP_VOLUME)
                {
                    GetSerialPort();
                }
            }

            base.WndProc(ref m);
        }

        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            byte[] data = new byte[100];
            serialPort.Read(data, 0, data.Length);
            string str = ByteToString(data);
            ReceivedSerialDataEvent(str);
        }

        private string ByteToString(byte[] strByte)
        {
            string str = Encoding.Default.GetString(strByte);
            return str;
        }

        private byte[] StringToByte(string str)
        {
            byte[] StrByte = Encoding.UTF8.GetBytes(str);
            return StrByte;
        }


        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort.PortName = PortCombo.SelectedItem.ToString();
                serialPort.BaudRate = Convert.ToInt32(RateCombo.SelectedItem.ToString());
                serialPort.Open();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {

        }

        private void GetSerialPort()
        {
            PortCombo.Items.Clear();
            try
            {
                foreach (string str in SerialPort.GetPortNames())
                {
                    PortCombo.Items.Add(str);
                }
                if (PortCombo.Items.Count <= 0)
                {
                    PortCombo.Items.Add("연결 장치 없음");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
