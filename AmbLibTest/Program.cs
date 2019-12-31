using System;
using System.Collections.Generic;
using System.Text;
using Ambiesoft;
using System.Diagnostics;

namespace AmbLibTest
{
    using System.Windows.Forms;
    using System.IO;
    using System.Reflection;

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // AmbLib.ExitWin(AmbLib.EXITWINTYPE.EXITWIN_LOGOFF);
            FormMain form = new FormMain();
            form.ShowDialog();
        }
    }
}
