using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AmbLibTest
{
    class TestListView : ListView
    {
        protected override void WndProc(ref Message m)
        {
            if(m.Msg == 0x020A)
            {
                Ambiesoft.AmbLib.ChangeFontSize(this, m);
            }
            base.WndProc(ref m);
        }
    }
}
