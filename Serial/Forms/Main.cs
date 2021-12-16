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
using System.IO;
using System.Xml;

using LibGPS.GPSProtocol;
using LibGPS.GPSData;

namespace GPSParseTool
{
    public delegate void ReceivedSerialDataEventHandler(String str);
    public delegate void LoadGPXDataEventHandler(String str);

    public partial class Main : Form
    {
        private static Serial serialForm = new Serial();
        private static GPXData GPXViewer = new GPXData();
        public ReceivedSerialDataEventHandler ReceivedSerialDataEvent;
        public LoadGPXDataEventHandler LoadGPXDataEvent;

        private String SerialBuffer;
        private List<GPSData> GPSDataList = new List<GPSData>();

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
            serialPort.Close();
            serialForm.Close();
            GPXViewer.Close();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        #region Serial
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
            string str = ByteToString(data).Replace("\0", "");
            SerialBuffer += str;
            ReceivedSerialDataEvent(str);

            if (SerialBuffer.Contains("$GPRMC") && SerialBuffer.Contains("$GPGLL")) // Check Existing $GPRMC ~ $GPGLL Protocol
            {
                int GPRMC_Index = SerialBuffer.IndexOf("$GPRMC");
                int GPGLL_Index = SerialBuffer.LastIndexOf("$GPGLL");

                String temp = SerialBuffer.Substring(GPGLL_Index + 1); // Check The End
                if(temp.IndexOf('$') == -1) // Not the end
                {
                    return;
                }    
                if (GPRMC_Index > GPGLL_Index)
                {
                    SerialBuffer = SerialBuffer.Replace("\0", "").Substring(GPRMC_Index);
                    return;
                }

                String[] Protocols = SerialBuffer.Replace("\0", "").Replace("\r", "").Split('\n');

                List<String> GPSSignal = new List<String>();
                for (int i = 0; i < Protocols.Length; i++)
                {
                    GPSSignal.Add(Protocols[i]);
                    if (Protocols[i].StartsWith("$GPGLL"))
                        break;
                }

                GPSDataList.Add(new GPSData(GPSSignal));

                if (DataList.InvokeRequired)
                {
                    DataList.Invoke((MethodInvoker)delegate
                    {
                        GetGPSDataList();
                    });
                }
                else
                {
                    GetGPSDataList();
                }

                SerialBuffer = SerialBuffer.Substring(GPGLL_Index);
                int nlindex = SerialBuffer.IndexOf("\r\n");
                SerialBuffer = SerialBuffer.Substring(nlindex + 2);
            }
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
                if (PortCombo.SelectedItem != null)
                {
                    serialPort.PortName = PortCombo.SelectedItem.ToString();
                    serialPort.BaudRate = Convert.ToInt32(RateCombo.SelectedItem.ToString());
                    serialPort.Open();
                }
                else
                {
                    MessageBox.Show("포트가 선택되지 않았습니다.");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
        #endregion

        private void GetGPSDataList()
        {
            for(int i = DataList.Items.Count; i < GPSDataList.Count; i++)
            {
                DataList.Items.Add("GPS Data " + i.ToString());
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            SerialBuffer = "";
            serialPort.Close();
        }

        private void DataList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataList.SelectedIndex >= 0 && DataList.SelectedIndex < GPSDataList.Count)
            { 
                RawText.Text = GPSDataList[DataList.SelectedIndex].GetRawData() + "\r\n\r\n" + GPSDataList[DataList.SelectedIndex].GetErorrMessage(); 
                if(DataTree.InvokeRequired)
                {
                    DataTree.Invoke((MethodInvoker)delegate
                    {
                        UpdateDateTree(DataList.SelectedItem.ToString(), GPSDataList[DataList.SelectedIndex]);
                    });
                }
                else
                {
                    UpdateDateTree(DataList.SelectedItem.ToString(), GPSDataList[DataList.SelectedIndex]);
                }
            }
        }

        private void UpdateDateTree(String DataItem, GPSData Data)
        {
            DataTree.Nodes.Clear();
            
            ImageList imgList = new ImageList();
            imgList.Images.Add(Bitmap.FromFile("satellite.png"));
            DataTree.ImageList = imgList;

            TreeNode GPSTree = new TreeNode(DataItem, 0, 0);
            if(Data.GetGPRMC() != null)
                GPSTree.Nodes.Add("GPRMC", Data.GetGPRMC().Header, 0, 0);
            if (Data.GetGPVTG() != null)
                GPSTree.Nodes.Add("GPVTG", Data.GetGPVTG().Header, 0, 0);
            if (Data.GetGPGGA() != null)
                GPSTree.Nodes.Add("GPGGA", Data.GetGPGGA().Header, 0, 0);
            if (Data.GetGPGSA() != null)
                GPSTree.Nodes.Add("GPGSA", Data.GetGPGSA().Header, 0, 0);
            if (Data.GetGPGSV().Count != 0)
                for (int i = 0; i < Data.GetGPGSV().Count; i++)
                    GPSTree.Nodes.Add("GPGSV " + i.ToString(), Data.GetGPGSV()[i].Header, 0, 0);
            if (Data.GetGPGLL() != null)
                GPSTree.Nodes.Add("GPGLL", Data.GetGPGLL().Header, 0, 0);

            DataTree.Nodes.Add(GPSTree);
        }

        private void DataTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Name.Equals("GPRMC"))
            {
                ListView.Items.Clear();

                GPRMC temp = GPSDataList[DataList.SelectedIndex].GetGPRMC();
                ListViewItem item;
                ListView.BeginUpdate();

                item = new ListViewItem("Header");
                item.SubItems.Add(temp.Header);
                item.SubItems.Add("Header Of GPRMC");
                ListView.Items.Add(item);

                item = new ListViewItem("Time");
                if (temp.GetTimeToken() != null)
                    item.SubItems.Add(temp.GetTimeToken().GetAttributes());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Time in UTC");
                ListView.Items.Add(item);

                item = new ListViewItem("Status");
                if (temp.GetTimeToken() != null)
                    item.SubItems.Add(temp.GetStatus().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Responsibility of GPS Signal. A = Trusted / V = Untrusted");
                ListView.Items.Add(item);

                item = new ListViewItem("Latitude");
                if (temp.GetLatitude() != null && temp.GetLatitudeDirection() != null)
                    item.SubItems.Add(temp.GetLatitudeCalculator().ToString() + ", " + temp.GetLatitudeDirection().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Latitude and Direction");
                ListView.Items.Add(item);

                item = new ListViewItem("Longitude");
                if (temp.GetLongitude() != null && temp.GetLongitudeDirection() != null)
                    item.SubItems.Add(temp.GetLongitudeCalculator().ToString() + ", " + temp.GetLongitudeDirection().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Longitude and Direction");
                ListView.Items.Add(item);

                item = new ListViewItem("Speed In Knots");
                if (temp.GetSpeedInKnots() != null)
                    item.SubItems.Add(temp.GetSpeedInKnots().ToString() + "kn");
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Speed In Knots");
                ListView.Items.Add(item);

                item = new ListViewItem("Speed In Km/h");
                if (temp.GetSpeedInKmh() != null)
                    item.SubItems.Add(temp.GetSpeedInKmh().ToString() + "Km/h");
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Speed In Km/h");
                ListView.Items.Add(item);

                item = new ListViewItem("Angle In Degree");
                if (temp.GetAngleInDegree() != null)
                    item.SubItems.Add(temp.GetAngleInDegree() + "Degree");
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Angle In Degree. Basically North is 0 Degree");
                ListView.Items.Add(item);

                item = new ListViewItem("Date");
                if (temp.GetDateToken() != null)
                    item.SubItems.Add(temp.GetDateToken().GetAttributes());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Date");
                ListView.Items.Add(item);

                item = new ListViewItem("Magnetic Variation");
                if (temp.GetMagneticVariation() != null && temp.GetMagneticVariationDirection() != null)
                    item.SubItems.Add(temp.GetMagneticVariation().ToString() + ", " + temp.GetMagneticVariationDirection().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Magnetic Variation and Direction");
                ListView.Items.Add(item);

                item = new ListViewItem("CheckSum");
                if (temp.GetCheckSum() != null)
                    item.SubItems.Add(temp.GetCheckSum().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("CheckSum");
                ListView.Items.Add(item);

                ListView.EndUpdate();
            }
            else if (e.Node.Name.Equals("GPVTG"))
            {
                ListView.Items.Clear();

                GPVTG temp = GPSDataList[DataList.SelectedIndex].GetGPVTG();
                ListViewItem item;
                ListView.BeginUpdate();

                item = new ListViewItem("Header");
                item.SubItems.Add(temp.Header);
                item.SubItems.Add("Header of GPVTG");
                ListView.Items.Add(item);

                item = new ListViewItem("True Track");
                if (temp.GetTrueTrack() != null && temp.GetTrueTrack_Direction() != null)
                    item.SubItems.Add(temp.GetTrueTrack().ToString() + ", " + temp.GetTrueTrack_Direction().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("True Track and Direction");
                ListView.Items.Add(item);

                item = new ListViewItem("Magnetic Track");
                if (temp.GetMagneticTrack() != null && temp.GetMagneticTrack_Direction() != null)
                    item.SubItems.Add(temp.GetMagneticTrack().ToString() + ", " + temp.GetMagneticTrack_Direction().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Magnetic Track and Direction");
                ListView.Items.Add(item);

                item = new ListViewItem("Speed In Knots");
                if (temp.GetSpeedInKnots() != null)
                    item.SubItems.Add(temp.GetSpeedInKnots().ToString() + "kn");
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Speed In Knots");
                ListView.Items.Add(item);

                item = new ListViewItem("Speed In Km/h");
                if (temp.GetSpeedInKmh() != null)
                    item.SubItems.Add(temp.GetSpeedInKmh().ToString() + "Km/h");
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Speed In Km/h");
                ListView.Items.Add(item);

                item = new ListViewItem("CheckSum");
                if (temp.GetCheckSum() != null)
                    item.SubItems.Add(temp.GetCheckSum().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("CheckSum");
                ListView.Items.Add(item);

                ListView.EndUpdate();
            }
            else if (e.Node.Name.Equals("GPGGA"))
            {
                ListView.Items.Clear();

                GPGGA temp = GPSDataList[DataList.SelectedIndex].GetGPGGA();
                ListViewItem item;
                ListView.BeginUpdate();

                item = new ListViewItem("Header");
                item.SubItems.Add(temp.Header);
                item.SubItems.Add("Header of GPGGA");
                ListView.Items.Add(item);

                item = new ListViewItem("Time");
                if (temp.GetTimeToken() != null)
                    item.SubItems.Add(temp.GetTimeToken().GetAttributes());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Time In UTC");
                ListView.Items.Add(item);

                item = new ListViewItem("Latitude");
                if (temp.GetLatitude() != null && temp.GetLatitudeDirection() != null)
                    item.SubItems.Add(temp.GetLatitude().ToString() + ", " + temp.GetLatitudeDirection().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Latitude and Direction");
                ListView.Items.Add(item);

                item = new ListViewItem("Longitude");
                if (temp.GetLongitude() != null && temp.GetLongitudeDirection() != null)
                    item.SubItems.Add(temp.GetLongitude().ToString() + ", " + temp.GetLongitudeDirection().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Longitude and Direction");
                ListView.Items.Add(item);

                item = new ListViewItem("Quality");
                if (temp.GetFixQuality() != null)
                    item.SubItems.Add(temp.GetFixQuality().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Quality of Fix. 0 : Invaild / 1 : Data With Basic Satellite / 2 : Data With DGPS Correction");
                ListView.Items.Add(item);

                item = new ListViewItem("Satellites");
                if (temp.GetSatellites() != null)
                    item.SubItems.Add(temp.GetSatellites().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Number Of Satellites");
                ListView.Items.Add(item);

                item = new ListViewItem("HDOP");
                if (temp.GetHDOP() != null)
                    item.SubItems.Add(temp.GetHDOP().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("HDOP. Error In Horizontal.");
                ListView.Items.Add(item);

                item = new ListViewItem("Horizontal Sea Level");
                if (temp.GetHorizontalSeaLevel() != null && temp.GetHorizontalSeaLevelUnit() != null)
                    item.SubItems.Add(temp.GetHorizontalSeaLevel().ToString() + temp.GetHorizontalSeaLevelUnit().ToString().ToLower());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Altitude Above Sea Level");
                ListView.Items.Add(item);

                item = new ListViewItem("Horizontal WGS84");
                if (temp.GetHorizontalWGS84() != null && temp.GetHorizontalWGS84Unit() != null)
                    item.SubItems.Add(temp.GetHorizontalWGS84().ToString() + temp.GetHorizontalWGS84Unit().ToString().ToLower());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Altitude Above WGS84 Sphere");
                ListView.Items.Add(item);

                item = new ListViewItem("DGPS Update Time");
                if (temp.GetDGPSUpdateTime() != null)
                    item.SubItems.Add(temp.GetDGPSUpdateTime().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("DGPS Update Time");
                ListView.Items.Add(item);

                item = new ListViewItem("DGPS ID");
                if (temp.GetDGPSID() != null)
                    item.SubItems.Add(temp.GetDGPSID().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("DGPS ID");
                ListView.Items.Add(item);

                item = new ListViewItem("CheckSum");
                if (temp.GetCheckSum() != null)
                    item.SubItems.Add(temp.GetCheckSum().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("CheckSum");
                ListView.Items.Add(item);

                ListView.EndUpdate();
            }
            else if (e.Node.Name.Equals("GPGSA"))
            {
                ListView.Items.Clear();

                GPGSA temp = GPSDataList[DataList.SelectedIndex].GetGPGSA();
                ListViewItem item;
                ListView.BeginUpdate();

                item = new ListViewItem("Header");
                item.SubItems.Add(temp.Header);
                item.SubItems.Add("Header of GPGSA");
                ListView.Items.Add(item);

                item = new ListViewItem("AutoSelection");
                if (temp.GetAutoSelection() != null)
                    item.SubItems.Add(temp.GetAutoSelection().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Mode of Selection. A : 2D/3D Auto / M : Manual");
                ListView.Items.Add(item);

                item = new ListViewItem("nDFix");
                if (temp.GetnDFix() != null)
                    item.SubItems.Add(temp.GetnDFix().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Mode of Dimension. 2 : 2D / 3 : 3D");
                ListView.Items.Add(item);

                item = new ListViewItem("Used Satellites");
                if (temp.GetUsedSatellites() != null)
                {
                    String str = "";
                    for (int i = 0; i < temp.GetUsedSatellites().Length; i++)
                    {
                        if (temp.GetUsedSatellites()[i] != null)
                            str += temp.GetUsedSatellites()[i].ToString();
                        str += ", ";
                    }
                    item.SubItems.Add(str);
                }
                else
                    item.SubItems.Add("null");
                
                item.SubItems.Add("Satellites ID");
                ListView.Items.Add(item);

                item = new ListViewItem("PDOP");
                if (temp.GetPDOP() != null)
                    item.SubItems.Add(temp.GetPDOP().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("PDOP. Error In Position.");
                ListView.Items.Add(item);

                item = new ListViewItem("HDOP");
                if (temp.GetHDOP() != null)
                    item.SubItems.Add(temp.GetHDOP().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("HDOP. Error In Horizontal.");
                ListView.Items.Add(item);

                item = new ListViewItem("VDOP");
                if (temp.GetVDOP() != null)
                    item.SubItems.Add(temp.GetVDOP().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("HDOP. Error In Vertical.");
                ListView.Items.Add(item);

                item = new ListViewItem("CheckSum");
                if (temp.GetCheckSum() != null)
                    item.SubItems.Add(temp.GetCheckSum().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("CheckSum");
                ListView.Items.Add(item);

                ListView.EndUpdate();
            }
            else if (e.Node.Name.StartsWith("GPGSV"))
            {
                ListView.Items.Clear();

                int index = Convert.ToInt32(e.Node.Name.Split(' ')[1]);
                GPGSV temp = GPSDataList[DataList.SelectedIndex].GetGPGSV()[index];
                ListViewItem item;
                ListView.BeginUpdate();

                item = new ListViewItem("Header");
                item.SubItems.Add(temp.Header);
                item.SubItems.Add("Header of GPGSV");
                ListView.Items.Add(item);

                item = new ListViewItem("All Sentence");
                if (temp.GetAllSentence() != null)
                    item.SubItems.Add(temp.GetAllSentence().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Count of All Sentence ");
                ListView.Items.Add(item);

                item = new ListViewItem("Sentence Index");
                if (temp.GetSentenceIndex() != null)
                    item.SubItems.Add(temp.GetSentenceIndex().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Sentence Index");
                ListView.Items.Add(item);

                item = new ListViewItem("Number of Satellites");
                if (temp.GetNumOfSatellites() != null)
                    item.SubItems.Add(temp.GetNumOfSatellites().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Total Satellites");
                ListView.Items.Add(item);

                int?[][] satellites = temp.GetSatellites();
                if(satellites != null)
                    for (int i = 0; i < satellites.Length; i++)
                    { 
                        item = new ListViewItem("Satellite " + i.ToString());
                        String str = "(";
                        for (int j = 0; j < satellites[i].Length; j++)
                        {
                            if (satellites[i][j] != null)
                                str += satellites[i][j].ToString();
                            if(j != satellites[i].Length - 1)
                                str += ", ";
                        }
                        str += ")";
                        item.SubItems.Add(str);
                        ListView.Items.Add(item);
                    }

                item = new ListViewItem("CheckSum");
                if (temp.GetCheckSum() != null)
                    item.SubItems.Add(temp.GetCheckSum().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("CheckSum");
                ListView.Items.Add(item);

                ListView.EndUpdate();
            }
            else if (e.Node.Name.Equals("GPGLL"))
            {
                ListView.Items.Clear();

                GPGLL temp = GPSDataList[DataList.SelectedIndex].GetGPGLL();
                ListViewItem item;
                ListView.BeginUpdate();

                item = new ListViewItem("Header");
                item.SubItems.Add(temp.Header);
                item.SubItems.Add("Header of GPGLL");
                ListView.Items.Add(item);

                item = new ListViewItem("Latitude");
                if (temp.GetLatitude() != null && temp.GetLatitudeDirection() != null)
                    item.SubItems.Add(temp.GetLatitude().ToString() + ", " + temp.GetLatitudeDirection().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Latitude and Direction");
                ListView.Items.Add(item);

                item = new ListViewItem("Longitude");
                if (temp.GetLongitude() != null && temp.GetLongitudeDirection() != null)
                    item.SubItems.Add(temp.GetLongitude().ToString() + ", " + temp.GetLongitudeDirection().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Longitude and Direction");
                ListView.Items.Add(item);

                item = new ListViewItem("Time");
                if (temp.GetTimeToken() != null)
                    item.SubItems.Add(temp.GetTimeToken().GetAttributes());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Time in UTC");
                ListView.Items.Add(item);

                item = new ListViewItem("Status");
                if (temp.GetTimeToken() != null)
                    item.SubItems.Add(temp.GetStatus().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("Responsibility of GPS Signal. A = Trusted / V = Untrusted");
                ListView.Items.Add(item);

                item = new ListViewItem("CheckSum");
                if (temp.GetCheckSum() != null)
                    item.SubItems.Add(temp.GetCheckSum().ToString());
                else
                    item.SubItems.Add("null");
                item.SubItems.Add("CheckSum");
                ListView.Items.Add(item);

                ListView.EndUpdate();
            }
        }

        private void ListClrBtn_Click(object sender, EventArgs e)
        {
            DataList.Items.Clear();
            GPSDataList.Clear();
        }

        private void LoadGPXBtn_Click(object sender, EventArgs e)
        {
            bool flag = false;
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.GetType() == typeof(GPXData))
                {
                    flag = true;
                }
            }
            if(flag)
            {
                GPXViewer.Focus();
            }
            else
            {
                GPXViewer.Show();
            }
        }

        private void ExportGPXBtn_Click(object sender, EventArgs e)
        {
            if (GPSDataList.Count <= 0)
            {
                MessageBox.Show("GPS 데이터가 부족합니다.");
                return;
            }
            List<double> Lat = new List<double>();
            List<double> Lng = new List<double>();
            double maxlat = 0.0f, minlat = 0.0f, maxlon = 0.0f, minlon = 0.0f;

            for(int i = 1; i < GPSDataList.Count; i++)
            {
                double? lat = null, lng = null;
                GPRMC temp = GPSDataList[i].GetGPRMC();
                if (temp == null)
                    continue;

                lat = temp.GetLatitudeCalculator();
                lng = temp.GetLongitudeCalculator();
                if (lat == null || lng == null)
                    continue;

                if (temp.GetLatitudeDirection() != 'N')
                    lat = -lat;
                if (temp.GetLongitudeDirection() != 'E')
                    lng = -lng;

                Lat.Add((double)lat);
                Lng.Add((double)lng);
            }
            if (Lat.Count <= 0)
            {
                MessageBox.Show("GPS 위치 데이터가 부족합니다.");
                return;
            }
            maxlat = Lat.Max();
            maxlon = Lng.Max();
            minlat = Lat.Min();
            minlon = Lng.Min();

            XmlDocument GPX = new XmlDocument();

            XmlDeclaration GPX_Decl = GPX.CreateXmlDeclaration("1.0", "UTF-8", null);
            GPX.AppendChild(GPX_Decl);

            XmlNode GPX_gpx = GPX.CreateElement("gpx");
            
            XmlAttribute GPX_gpx_creator = GPX.CreateAttribute("creator");
            XmlAttribute GPX_gpx_version = GPX.CreateAttribute("version");
            XmlAttribute GPX_gpx_xalan = GPX.CreateAttribute("xalan");
            XmlAttribute GPX_gpx_xmlns = GPX.CreateAttribute("xmlns");
            XmlAttribute GPX_gpx_xsi = GPX.CreateAttribute("xsi");
            GPX_gpx_creator.Value = "GPSParserTool";
            GPX_gpx_version.Value = "1.1";
            GPX_gpx_xalan.Value = "http://xml.apache.org/xalan";
            GPX_gpx_xmlns.Value = "http://www.topografix.com/GPX/1/1";
            GPX_gpx_xsi.Value = "http://www.w3.org/2001/XMLSchema-instance";
            GPX_gpx.Attributes.Append(GPX_gpx_creator);
            GPX_gpx.Attributes.Append(GPX_gpx_version);
            GPX_gpx.Attributes.Append(GPX_gpx_xalan);
            GPX_gpx.Attributes.Append(GPX_gpx_xmlns);
            GPX_gpx.Attributes.Append(GPX_gpx_xsi);

            XmlNode GPX_gpx_metadata = GPX.CreateElement("metadata");
            XmlNode GPX_gpx_metadata_desc = GPX.CreateElement("desc");
            XmlNode GPX_gpx_metadata_bounds = GPX.CreateElement("bounds");
            XmlAttribute GPX_gpx_meatadata_bounds_maxlat = GPX.CreateAttribute("maxlat");
            XmlAttribute GPX_gpx_meatadata_bounds_minlat = GPX.CreateAttribute("minlat");
            XmlAttribute GPX_gpx_meatadata_bounds_maxlon = GPX.CreateAttribute("maxlon");
            XmlAttribute GPX_gpx_meatadata_bounds_minlon = GPX.CreateAttribute("minlon");
            GPX_gpx_metadata_desc.InnerText = "GPSParseTool";
            GPX_gpx_meatadata_bounds_maxlat.Value = maxlat.ToString("F14");
            GPX_gpx_meatadata_bounds_minlat.Value = minlat.ToString("F14");
            GPX_gpx_meatadata_bounds_maxlon.Value = maxlon.ToString("F14");
            GPX_gpx_meatadata_bounds_minlon.Value = minlon.ToString("F14");
            GPX_gpx_metadata_bounds.Attributes.Append(GPX_gpx_meatadata_bounds_maxlat);
            GPX_gpx_metadata_bounds.Attributes.Append(GPX_gpx_meatadata_bounds_minlat);
            GPX_gpx_metadata_bounds.Attributes.Append(GPX_gpx_meatadata_bounds_maxlon);
            GPX_gpx_metadata_bounds.Attributes.Append(GPX_gpx_meatadata_bounds_minlon);
            GPX_gpx_metadata.AppendChild(GPX_gpx_metadata_desc);
            GPX_gpx_metadata.AppendChild(GPX_gpx_metadata_bounds);
            GPX_gpx.AppendChild(GPX_gpx_metadata);

            XmlNode GPX_gpx_trk = GPX.CreateElement("trk");
            XmlNode GPX_gpx_trkseg = GPX.CreateElement("trkseg");
            for(int i = 0; i < Lat.Count; i++)
            {
                XmlNode GPX_gpx_trkpt = GPX.CreateElement("trkpt");
                XmlNode GPX_gpx_ele = GPX.CreateElement("ele");
                XmlAttribute GPX_gpx_trkpt_lat = GPX.CreateAttribute("lat");
                XmlAttribute GPX_gpx_trkpt_lon = GPX.CreateAttribute("lon");

                GPX_gpx_trkpt_lat.Value = Lat[i].ToString("F14");
                GPX_gpx_trkpt_lon.Value = Lng[i].ToString("F14");
                GPX_gpx_ele.InnerText = "0";

                GPX_gpx_trkpt.Attributes.Append(GPX_gpx_trkpt_lat);
                GPX_gpx_trkpt.Attributes.Append(GPX_gpx_trkpt_lon);
                GPX_gpx_trkpt.AppendChild(GPX_gpx_ele);

                GPX_gpx_trkseg.AppendChild(GPX_gpx_trkpt);
            }
            GPX_gpx_trk.AppendChild(GPX_gpx_trkseg);
            GPX_gpx.AppendChild(GPX_gpx_trk);
            GPX.AppendChild(GPX_gpx);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "GPX files (*.gpx)|*.gpx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                GPX.Save(saveFileDialog.FileName);
            }
        }

        private void OpenLogBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = ".";
            openFileDialog.Filter = "Text files (*.txt)|*.txt";

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String[] buf = File.ReadAllLines(openFileDialog.FileName);

                LoadGPSLog loadGPSLog = new LoadGPSLog(buf.Length);
                loadGPSLog.Show();

                bool bGPRMC = false;
                List<String> GPSSignal = new List<String>();

                for (int i = 0; i < buf.Length; i++)
                {
                    loadGPSLog.UpdateBar(buf.Length - i);
                    if(buf[i].StartsWith("$GPRMC") && !bGPRMC)
                    {
                        bGPRMC = true;
                        GPSSignal.Add(buf[i]);
                    }
                    else if(buf[i].StartsWith("$GPGLL") && bGPRMC)
                    {
                        bGPRMC = false;
                        GPSSignal.Add(buf[i]);
                        
                        GPSDataList.Add(new GPSData(GPSSignal));

                        if (DataList.InvokeRequired)
                        {
                            DataList.Invoke((MethodInvoker)delegate
                            {
                                GetGPSDataList();
                            });
                        }
                        else
                        {
                            GetGPSDataList();
                        }

                        GPSSignal.Clear();
                    }
                    else if (bGPRMC)
                    {
                        GPSSignal.Add(buf[i]);
                    }
                }
                loadGPSLog.EndBar();
            }
        }
    }
}
