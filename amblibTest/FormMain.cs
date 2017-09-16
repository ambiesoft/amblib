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
    }
}
