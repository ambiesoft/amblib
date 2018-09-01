using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ambiesoft;

namespace AmbLibTest
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            HashIni ini = Profile.ReadAll(AmbLib.GetIniPath());
            AmbLib.LoadListViewColumnWidth(listView1, "List", "Main", ini);
            AmbLib.LoadFormXYWH(this, "POSITION", ini);
        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            string iniPath = AmbLib.GetIniPath();
            HashIni ini = Profile.ReadAll(iniPath);
            AmbLib.SaveListViewColumnWidth(listView1, "List", "Main", ini);
            AmbLib.SaveFormXYWH(this, "POSITION", ini);
            if (!Profile.WriteAll(ini,iniPath))
            {
                MessageBox.Show("ERROR");
            }
        }

        void checkPointIn()
        {
            string ptins = "TopLeft:";
            if (AmbLib.IsPointInScreen(Location))
                ptins += "yes";
            else
                ptins += "NO";

            ptins += ", BottomRight:";
            Point ptBottomRight = new Point();
            ptBottomRight.X = Location.X + Size.Width;
            ptBottomRight.Y = Location.Y + Size.Height;
            if (AmbLib.IsPointInScreen(ptBottomRight))
                ptins += "yes";
            else
                ptins += "NO";

            txtPointInScreen.Text = ptins;

        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            Ambiesoft.AmbLib.StretchToolItem(toolStrip1, toolStripComboBox1);
            txtDNS.Text = Ambiesoft.AmbLib.GetDnsAdress();

            
        }

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            Ambiesoft.AmbLib.StretchToolItem(toolStrip1, toolStripComboBox1);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            checkPointIn();
        }

        private void btnOpenAmbiesoft_Click(object sender, EventArgs e)
        {
            AmbLib.OpenUrlWithBrowser("http://ambiesoft.fam.cx");
        }

        
    }
}
