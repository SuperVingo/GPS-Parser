using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Threading;

namespace GPSParseTool
{
    public partial class GPXData : Form
    {
        private XmlDocument GPX_Xml = new XmlDocument();
        private double avglat = 37.531771f, avglon = 126.914171f;
        private String FileName = "";

        public GPXData()
        {
            InitializeComponent();
        }
        private void LoadGPXBtn_Click(object sender, EventArgs e)
        {
            Wait wait = new Wait();
            wait.Show();
            LoadGPX();
            wait.Close();
            this.Focus();
        }

        private void LoadGPX()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = ".";
            openFileDialog.Filter = "GPX files (*.gpx)|*.gpx";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = openFileDialog.FileName;
                GPX_Xml.Load(openFileDialog.FileName);

                GPXTree.Nodes.Clear();
                GPXTree.Nodes.Add(new TreeNode(GPX_Xml.DocumentElement.Name));
                TreeNode treeNode = new TreeNode();
                treeNode = GPXTree.Nodes[0];

                AddNode(GPX_Xml.DocumentElement, treeNode);
                GPXTree.ExpandAll();

                XmlElement bounds = GPX_Xml.DocumentElement["metadata"]["bounds"];
                avglat = (Convert.ToDouble(bounds.GetAttribute("maxlat")) + Convert.ToDouble(bounds.GetAttribute("minlat"))) / 2.0f;
                avglon = (Convert.ToDouble(bounds.GetAttribute("maxlon")) + Convert.ToDouble(bounds.GetAttribute("minlon"))) / 2.0f;
            }

            
        }

        private void ExportGPXBtn_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("file://G:/SerialConnection/Serial/GPS-Parser/Serial/GPXView.html");
            Object[] para = new Object[2];
            para[0] = (Object)(avglat);
            para[1] = (Object)(avglon);
            webBrowser1.Document.InvokeScript("SetCenter", para);
        }

        private void AddNode(XmlNode root, TreeNode treeNode)
        {
            XmlNode GPX_Xml_Node;
            TreeNode GPX_TreeNode;
            XmlNodeList GPX_Xml_NodeList;

            if (root.HasChildNodes)
            {
                GPX_Xml_NodeList = root.ChildNodes;
                for (int i = 0; i < GPX_Xml_NodeList.Count; i++)
                {
                    GPX_Xml_Node = root.ChildNodes[i];
                    treeNode.Nodes.Add(new TreeNode(GPX_Xml_Node.Name));
                    GPX_TreeNode = treeNode.Nodes[i];
                    Thread.Sleep(10);
                    AddNode(GPX_Xml_Node, GPX_TreeNode);
                }
            }
            else
            {
                treeNode.Text = (root.OuterXml).Trim();
            }
        }
    }
}
