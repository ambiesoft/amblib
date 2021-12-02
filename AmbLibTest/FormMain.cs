using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ambiesoft;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace AmbLibTest
{
    public partial class FormMain : Form
    {
        readonly static string SECTION_OPTION = "Option";
        readonly static string KEY_SELECTED_TABINDEX = "SelectedTabIndex";
        public FormMain()
        {
            InitializeComponent();
            
            HashIni ini = Profile.ReadAll(AmbLib.GetIniPath());
            int ival;
            AmbLib.LoadListViewColumnWidth(listView1, "List", "Main", ini);
            AmbLib.LoadFormXYWH(this, "POSITION", ini);
            Profile.GetInt(SECTION_OPTION, KEY_SELECTED_TABINDEX, 0, out ival, ini);
            if(0 <= ival && ival < tabMain.TabCount)
                tabMain.SelectedIndex = ival;
            string allGpu = string.Join("\r\n", AmbLib.GetGpuNames());
            allGpu += Environment.NewLine;
            allGpu += Environment.NewLine;

            allGpu += AmbLib.GetGpuInfos();
            allGpu += Environment.NewLine;
            allGpu += Environment.NewLine;
            allGpu += "IsIntelUHD=" + AmbLib.IsGpuIntelUHD;

            txtGpuInfo.Text += allGpu;

        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            string iniPath = AmbLib.GetIniPath();
            HashIni ini = Profile.ReadAll(iniPath);
            AmbLib.SaveListViewColumnWidth(listView1, "List", "Main", ini);
            AmbLib.SaveFormXYWH(this, "POSITION", ini);
            Profile.WriteInt(SECTION_OPTION, KEY_SELECTED_TABINDEX, tabMain.SelectedIndex, ini);
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

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern uint GetDoubleClickTime();
        private void FormMain_Load(object sender, EventArgs e)
        {
            Ambiesoft.AmbLib.StretchToolItem(toolStrip1, tsbcMain);
            txtDNS.Text = Ambiesoft.AmbLib.GetDnsAdress();

            AmbLib.MakeTripleClickTextBox(txtTripleClick);
            // AmbLib.MakeTripleClickTextBox(cmbTripleClick, GetDoubleClickTime());
        }

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            Ambiesoft.AmbLib.StretchToolItem(toolStrip1, tsbcMain);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            checkPointIn();
        }

        private void btnOpenAmbiesoft_Click(object sender, EventArgs e)
        {
            AmbLib.OpenUrlWithBrowser("http://ambiesoft.mooo.com");
        }

        static void verify(bool b)
        {
            if (!b)
            {
                Console.WriteLine("BAD");
            }
        }

        private void btnSimpleTest_Click(object sender, EventArgs e)
        {
            Console.WriteLine(Ambiesoft.DA.VersionNetFramework.GetVersionDicription());

            verify(AmbLib.getAssemblyVersion(Assembly.GetExecutingAssembly()) == "1.2");
            verify(AmbLib.getAssemblyVersion(Assembly.GetExecutingAssembly(), 3) == "1.2.3");
            verify(AmbLib.getAssemblyVersion(Assembly.GetExecutingAssembly(), 4) == "1.2.3.4");
            //AmbLib.Alert("Alert OK?");
            verify(AmbLib.IsSameFile("a", "a"));
            verify(!AmbLib.IsSameFile(null, ""));
            verify(AmbLib.IsSameFile(@"./a", @".\a"));
            verify(AmbLib.IsSameFile(@"./a", @".\\a"));


            int retval;
            string output, err;
            try
            {
                AmbLib.OpenCommandGetResult(
                    Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "AmbLibTestCommandRunner.exe"),
                    "",
                    Encoding.Default,
                    out retval,
                    out output,
                    out err);

                Console.WriteLine("output:");
                Console.WriteLine(output);
                Console.WriteLine("err:");
                Console.WriteLine(err);


                // Delegate 
                StringBuilder sbOut = new StringBuilder();
                StringBuilder sbErr = new StringBuilder();

                Process pro;
                AmbLib.OpenCommandGetResultCallback(
                    Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "AmbLibTestCommandRunner.exe"),
                    "",
                    Encoding.Default,
                    out retval,
                    (s, ev) => { if (e != null) { sbOut.AppendLine(ev.Data); } },
                    (s, ev) => { if (e != null) { sbErr.AppendLine(ev.Data); } },
                    null,
                    out pro
                    );
                Console.WriteLine("output:");
                Console.WriteLine(sbOut.ToString());
                Console.WriteLine("err:");
                Console.WriteLine(sbErr.ToString());

            }
            catch // (Exception)
            {
                //AmbLib.Alert(ex);
            }


            // normal long file
            string f1 = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaae.bbb";
            string sl = AmbLib.GetMaxpathTrimmedPath(f1);
            Debug.Assert(sl.Length < 260);

            // long ext
            f1 = "aaae.aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaae";
            sl = AmbLib.GetMaxpathTrimmedPath(f1);
            Debug.Assert(sl == null || sl.Length < 260);

            // both long
            f1 = "aaaeaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaae.bffffffffffffffffffaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaae";
            sl = AmbLib.GetMaxpathTrimmedPath(f1);
            Debug.Assert(sl == null || sl.Length < 260);

            // long dir
            f1 = "c:\\aaaeaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaae.bffffffffffffffffffaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaae\\aaa.txt";
            sl = AmbLib.GetMaxpathTrimmedPath(f1);
            Debug.Assert(sl == null || sl.Length < 260);

            // strippable path
            f1 = @"C:\aaaaaaaaaaaaaaaaaaaaaaaaaaaa\aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaae.bbb";
            sl = AmbLib.GetMaxpathTrimmedPath(f1);
            Debug.Assert(sl.Length < 260);

            f1 = @"C:\Users\zelda\Desktop\watson-developer-cloud_natural-language-classifier-nodejs_ See how the classifier service uses natural language to determine the intent behind your question. Ask a question about the weather, and watch as the service classifies the intent as 'temperature' or 'condition' related..url";
            sl = AmbLib.GetMaxpathTrimmedPath(f1, 260 - 6 - 1);
            Debug.Assert(sl.Length < 260);

            object ob = AmbLib.EasyRegRead(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\FileSystem",
                "NtfsDisable8dot3NameCreation");


            verify(AmbLib.HasFileExtension("aaa.tXt", "txt"));

            verify(AmbLib.GetFileExtension("aaa.txt") == ".txt");
            verify(AmbLib.GetFileExtension(@"C:\Users\Bokkurin\Desktop\Passageway _ Define Passageway at Dictionary.com") == ".com");
            verify(AmbLib.IsFileNamable("aaa"));
            verify(AmbLib.IsFileNamable("aaa.txt"));
            verify(AmbLib.IsFileNamable("‚ ‚ ‚ ‚ B.aaa"));
            verify(!AmbLib.IsFileNamable("afeafe "));
            verify(AmbLib.IsFileNamable(".aaa"));
            verify(!AmbLib.IsFileNamable("aaa "));
            verify(!AmbLib.IsFileNamable("fnjsa<"));
            verify(AmbLib.IsFileNamable("aaan,.ji.jjjjjjjjjjjjjjjj"));
            verify(!AmbLib.IsFileNamable("lpt1.txt"));
            verify(!AmbLib.IsFileNamable("aux"));

            verify(AmbLib.GetFilaNamableName("aux") == AmbLib.DEFAULT_UNNAMABLED_FILENAME);
            verify(AmbLib.GetFilaNamableName("aaa bbb ?") == "aaa bbb _");

        }

        readonly string _longText = @"fjsaofjsodajfosdja
fsjdaofjsaojfosdjafojsafjosdajfosjafojsdfjowjfjw f ow jwo fjow jwefo fjowej wf 
fwefw j fowj ow fjwo wef joweifffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
jfowafjowej  jwj owe wef 
fsjdaofjsaojfosdjafojsafjosdajfosjafojsdfjowjfjw f ow jwo fjow jwefo fjowej wf 
fwefw j fowj ow fjwo wef joweifffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
jfowafjowej  jwj owe wef 
fsjdaofjsaojfosdjafojsafjosdajfosjafojsdfjowjfjw f ow jwo fjow jwefo fjowej wf 
fwefw j fowj ow fjwo wef joweifffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
jfowafjowej  jwj owe wef 
fsjdaofjsaojfosdjafojsafjosdajfosjafojsdfjowjfjw f ow jwo fjow jwefo fjowej wf 
fwefw j fowj ow fjwo wef joweifffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
jfowafjowej  jwj owe wef 
fsjdaofjsaojfosdjafojsafjosdajfosjafojsdfjowjfjw f ow jwo fjow jwefo fjowej wf 
fwefw j fowj ow fjwo wef joweifffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
jfowafjowej  jwj owe wef 
fsjdaofjsaojfosdjafojsafjosdajfosjafojsdfjowjfjw f ow jwo fjow jwefo fjowej wf 
fwefw j fowj ow fjwo wef joweifffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
jfowafjowej  jwj owe wef 
fsjdaofjsaojfosdjafojsafjosdajfosjafojsdfjowjfjw f ow jwo fjow jwefo fjowej wf 
fwefw j fowj ow fjwo wef joweifffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
jfowafjowej  jwj owe wef 
fsjdaofjsaojfosdjafojsafjosdajfosjafojsdfjowjfjw f ow jwo fjow jwefo fjowej wf 
fwefw j fowj ow fjwo wef joweifffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
jfowafjowej  jwj owe wef 
fsjdaofjsaojfosdjafojsafjosdajfosjafojsdfjowjfjw f ow jwo fjow jwefo fjowej wf 
fwefw j fowj ow fjwo wef joweifffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
jfowafjowej  jwj owe wef 
fsjdaofjsaojfosdjafojsafjosdajfosjafojsdfjowjfjw f ow jwo fjow jwefo fjowej wf 
fwefw j fowj ow fjwo wef joweifffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
jfowafjowej  jwj owe wef 
fsjdaofjsaojfosdjafojsafjosdajfosjafojsdfjowjfjw f ow jwo fjow jwefo fjowej wf 
fwefw j fowj ow fjwo wef joweifffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
jfowafjowej  jwj owe wef 
fsjdaofjsaojfosdjafojsafjosdajfosjafojsdfjowjfjw f ow jwo fjow jwefo fjowej wf 
fwefw j fowj ow fjwo wef joweifffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
jfowafjowej  jwj owe wef 
fsjdaofjsaojfosdjafojsafjosdajfosjafojsdfjowjfjw f ow jwo fjow jwefo fjowej wf 
fwefw j fowj ow fjwo wef joweifffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
jfowafjowej  jwj owe wef 
fsjdaofjsaojfosdjafojsafjosdajfosjafojsdfjowjfjw f ow jwo fjow jwefo fjowej wf 
fwefw j fowj ow fjwo wef joweifffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
jfowafjowej  jwj owe wef 
";
        private void btnShowText_Click(object sender, EventArgs e)
        {
           

            AmbLib.ShowTextDialog(this,
                Application.ProductName,
                "MY TEXT",
                _longText,
                true);
        }

        private void btnShowTexts_Click(object sender, EventArgs e)
        {
            List<KeyValuePair<string, string>> labelsAndTexts = new List<KeyValuePair<string, string>>();
            labelsAndTexts.Add(new KeyValuePair<string, string>("Title1", @"123
456
789"));
            labelsAndTexts.Add(new KeyValuePair<string, string>("Title2", @"abc
def
ghi"));
            labelsAndTexts.Add(new KeyValuePair<string, string>("Title3", @"XYZ
GHJ
LIE"));


            AmbLib.ShowTextDialog(this, Application.ProductName, labelsAndTexts, true);
        }

        private void btnI18NTest_Click(object sender, EventArgs e)
        {
            Ambiesoft.ResStringUtil rsu = new ResStringUtil("AmbLibTest.I18NStrings");
            Ambiesoft.ResStringUtil rsuJaJp = new ResStringUtil("AmbLibTest.I18NStrings",
                new System.Globalization.CultureInfo("ja-JP"));

            try
            {
                string test = rsu.getString("TEST");
                string testJaJp = rsuJaJp.getString("TEST");
                MessageBox.Show(test, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show(testJaJp, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            rsuJaJp.getString("NONONONOSTRING");
            rsuJaJp.showUnI18Ned();
        }

        private void cmbTripleClick_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void btnFormatSizeTest_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            long l = 1;
            sb.AppendLine(string.Format("{0} => {1}", l, AmbLib.FormatSize(l)));

            l = 10;
            sb.AppendLine(string.Format("{0} => {1}", l, AmbLib.FormatSize(l)));

            l = 100;
            sb.AppendLine(string.Format("{0} => {1}", l, AmbLib.FormatSize(l)));

            l = 1000;
            sb.AppendLine(string.Format("{0} => {1}", l, AmbLib.FormatSize(l)));

            l = 10000000;
            sb.AppendLine(string.Format("{0} => {1}", l, AmbLib.FormatSize(l)));

            l = 222222222;
            sb.AppendLine(string.Format("{0} => {1}", l, AmbLib.FormatSize(l)));

            l = 2222255555;
            sb.AppendLine(string.Format("{0} => {1}", l, AmbLib.FormatSize(l)));

            l = 33333333333;
            sb.AppendLine(string.Format("{0} => {1}", l, AmbLib.FormatSize(l)));
            MessageBox.Show(sb.ToString());
        }

        private void btnSelectApp_Click(object sender, EventArgs e)
        {
            var result = new StringBuilder();
            string all = AmbLib.GetOpenFileDialog(Application.ProductName);
            if (string.IsNullOrEmpty(all))
                result.AppendLine("cancel");
            result.AppendLine(all);

            var extentions = new Dictionary<string, string[]>();
            extentions["Application"] = new string[] { "exe", "com" };
            extentions["Image"] = new string[] { "gif", "jpg", "jpeg", "png", "bmp" };
            extentions["html"] = new string[] { "html", "htm" };
            string app = AmbLib.GetOpenFileDialog(Application.ProductName, extentions);
            if (string.IsNullOrEmpty(app))
                result.AppendLine("cancel");
            result.AppendLine(app);

            string image = AmbLib.GetOpenFileDialog("Choose Image", AmbLib.GETOPENFILEDIALOGTYPE.IMAGE);
            result.AppendLine(image);
            MessageBox.Show(result.ToString());
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            MessageBox.Show("m");
        }

        private void btnGetSaveFile_Click(object sender, EventArgs e)
        {
            string file = AmbLib.GetSaveFileDialog("savefile");
            MessageBox.Show(file);
        }

        void showSrcDstResult(
            string src, string dst,
            List<KeyValuePair<string,string>> lkv,
            List<string> dirs)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SRC=" + src);
            sb.AppendLine("DST=" + dst);
            sb.AppendLine("------------------------");
            foreach (var dir in dirs)
            {
                sb.AppendLine(dir);
            }
            sb.AppendLine("------------------------");
            foreach (var kv in lkv)
            {
                sb.AppendLine(string.Format("'{0}' -> '{1}'",
                    kv.Key, kv.Value));
            }
            txtSrcDstResult.Text = sb.ToString();
        }
        private void btnSrcDst_Click(object sender, EventArgs e)
        {
            List<string> dirs;
            var files = AmbLib.GetSourceAndDestFiles(txtSrc.Text, txtDst.Text, out dirs);

            showSrcDstResult(txtSrc.Text, txtDst.Text, files, dirs);
        }

        private void btnSrcDstFull_Click(object sender, EventArgs e)
        {
            string src = Path.GetDirectoryName(Application.ExecutablePath);
            string dst = Path.Combine(src, "..", "AAA");
            Directory.CreateDirectory(dst);

            List<string> dirs;
            var files = AmbLib.GetSourceAndDestFiles(src, dst, out dirs);
            showSrcDstResult(src,dst,files, dirs);
        }

        private void btnSrcDstRelative_Click(object sender, EventArgs e)
        {
            
            string src = "BBB";
            Directory.CreateDirectory(src);
            string srcFile = Path.Combine(src, "myfile.txt");
            File.WriteAllText(srcFile, "AAAAAAAAAAAAAAAAAAAAAAA");
            string dst = "DDD";
            Directory.CreateDirectory(dst);

            List<string> dirs;
            var files = AmbLib.GetSourceAndDestFiles(src, dst, out dirs);
            showSrcDstResult(src,dst,files, dirs);
        }

        private void btnCreateDir_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(txtDst.Text);
        }

        private void btnFileToFile_Click(object sender, EventArgs e)
        {
            List<string> dirs;
            string src = Application.ExecutablePath;
            string dst = Path.Combine(
                    Path.GetDirectoryName(Application.ExecutablePath),
                    "myexe.exe");

            var files = AmbLib.GetSourceAndDestFiles(src, dst, out dirs);
            showSrcDstResult(src, dst, files, dirs);
        }

        private void btnFileToDir_Click(object sender, EventArgs e)
        {
            List<string> dirs;
            string src = Application.ExecutablePath;
            string dst = Path.Combine(
                    Path.GetDirectoryName(Application.ExecutablePath),
                    "mydir\\");

            var files = AmbLib.GetSourceAndDestFiles(src, dst, out dirs);
            showSrcDstResult(src,dst,files, dirs);
        }

        private void btnGetFolder_Click(object sender, EventArgs e)
        {
            string folder = AmbLib.GetOpenFolderDialog("my title");
            MessageBox.Show(folder);
        }
    }
}
