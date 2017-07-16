using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace amblibTest
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
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
    }
}
