using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

using Microsoft.Win32;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Reflection;
using System.Web;
using System.Globalization;
using System.Resources;
using System.Linq;

using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using System.Management;
using System.Collections.Specialized;

namespace Ambiesoft
{
    public class ResStringUtil
    {
        CultureInfo _cultureInfo;
        ResourceManager _theResource;
        List<string> da_ = new List<string>();

        void DTRACE(string s)
        {
            Debug.WriteLine(s);
        }

        public ResStringUtil(string sss, CultureInfo ci)
        {
            _theResource = new ResourceManager(sss, Assembly.GetEntryAssembly());
            _cultureInfo = ci;
        }
        public ResStringUtil(string sss) : this(sss, null) { }
        
        public void showUnI18Ned() 
		{
			DTRACE("IIIIIIIIIIIIIIIIIIIIIIIIIIIIIII18NNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN");
			foreach(string o in da_)
			{
				DTRACE(o);
			}
			DTRACE("IIIIIIIIIIIIIIIIIIIIIIIIIIIIIII18NNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN");
		}


		public string getString(string s)
		{
			string ret = _theResource.GetString(s, _cultureInfo);

			if ( string.IsNullOrEmpty(ret) && !da_.Contains(s) )
			{
				DTRACE("\"" + s + "\" is not I18Ned.");
				da_.Add(s);
			}

			return string.IsNullOrEmpty(ret) ? s : ret;
		}
	};



































    public class AmbLib
    {
        public static readonly String DEFAULT_UNNAMABLED_FILENAME = "NewFile";

        public static bool HasFileExtension(string filename, string ext)
        {
            if (filename == null)
                filename = string.Empty;
            if (ext == null)
                ext = string.Empty;

            if (ext[0] != '.')
                ext = '.' + ext;
            string fileext = GetFileExtension(filename, true);
            return string.Compare(ext, fileext, true) == 0;
        }


        public static string GetFileExtension(string filename, bool lowercase)
        {
            if (string.IsNullOrEmpty(filename))
                return string.Empty;

            try
            {
                FileInfo fi = new FileInfo(filename);
                string ret = fi.Extension;
                if (ret == null)
                    ret = string.Empty;
                return lowercase ? ret.ToLower() : ret;
            }
            catch (Exception) { }
            return string.Empty;
        }

        public static string GetFileExtension(string filename)
        {
            return GetFileExtension(filename, false);
        }

        public static string GetFirstLine(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            s = s.TrimStart(new char[] { ' ', '\r', '\n', '\t' });

            int idx = s.IndexOf('\r');
            if (idx != -1)
            {
                s = s.Substring(0, idx);
            }

            idx = s.IndexOf('\n');
            if (idx != -1)
            {
                s = s.Substring(0, idx);
            }


            return s;
        }
        public static String GetFilaNamableName(String s)
        {
            if (string.IsNullOrEmpty(s))
                return DEFAULT_UNNAMABLED_FILENAME;

            s = GetFirstLine(s);

            StringBuilder sb = new StringBuilder();
            foreach (char c in s)
            {
                if (IsFileNamable(c))
                    sb.Append(c);
                else
                {
                    if (c == ':')
                    { }
                    else
                        sb.Append('_');
                }
            }
            string ret = sb.ToString();

            if (!IsFileNamableSpecial(ret))
            {
                ret = DEFAULT_UNNAMABLED_FILENAME;
            }
            return ret;
        }
        public static bool IsFileNamable(char c)
        {
            if (c == '<' ||
                c == '>' ||
                c == ':' ||
                c == ';' ||
                c == '\"' ||
                c == '/' ||
                c == '\\' ||
                c == '|' ||
                c == '?' ||
                c == '*')
            {
                return false;
            }

            if (0 <= c && c <= 31)
            {
                return false;
            }

            return true;
        }
        private static bool IsFileNamableSpecial(string fn)
        {
            string lfn = fn.ToUpper();
            int lp = lfn.LastIndexOf('.');
            if (lp != -1)
            {
                lfn = lfn.Substring(0, lp);
            }

            if (
                lfn == "CON" ||
                lfn == "PRN" ||
                lfn == "AUX" ||
                lfn == "NUL" ||
                lfn == "COM1" ||
                lfn == "COM2" ||
                lfn == "COM3" ||
                lfn == "COM4" ||
                lfn == "COM5" ||
                lfn == "COM6" ||
                lfn == "COM7" ||
                lfn == "COM8" ||
                lfn == "COM9" ||
                lfn == "LPT1" ||
                lfn == "LPT2" ||
                lfn == "LPT3" ||
                lfn == "LPT4" ||
                lfn == "LPT5" ||
                lfn == "LPT6" ||
                lfn == "LPT7" ||
                lfn == "LPT8" ||
                lfn == "LPT9"
                )
            {
                return false;
            }
            return true;
        }
        public static bool IsFileNamable(string fn)
        {
            if (string.IsNullOrEmpty(fn))
                return false;

            if (fn.EndsWith(".") || fn.EndsWith(" "))
                return false;

            foreach (char c in fn)
            {
                if (!IsFileNamable(c))
                    return false;
            }

            if (!IsFileNamableSpecial(fn))
                return false;

            return true;
        }



        public static void ExceptionMessageBox(System.Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(ex.Message,
            System.Windows.Forms.Application.ProductName,
            System.Windows.Forms.MessageBoxButtons.OK,
            System.Windows.Forms.MessageBoxIcon.Exclamation);
        }




        public static string GetIniPath()
        {
            FileInfo fi = new FileInfo(Application.ExecutablePath);
            string dir = fi.DirectoryName;
            string file = Path.GetFileNameWithoutExtension(fi.Name) + ".ini";
            return Path.Combine(dir, file);
        }

        // https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed
        public static string GetInstalledDotNetVersionFromRegistry()
        {
            StringBuilder sb = new StringBuilder();

            // Opens the registry key for the .NET Framework entry.
            using (RegistryKey ndpKey =
                RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, "").
                OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
            {
                // As an alternative, if you know the computers you will query are running .NET Framework 4.5 
                // or later, you can use:
                // using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, 
                // RegistryView.Registry32).OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
                foreach (string versionKeyName in ndpKey.GetSubKeyNames())
                {
                    if (versionKeyName.StartsWith("v"))
                    {

                        RegistryKey versionKey = ndpKey.OpenSubKey(versionKeyName);
                        string name = (string)versionKey.GetValue("Version", "");
                        string sp = versionKey.GetValue("SP", "").ToString();
                        string install = versionKey.GetValue("Install", "").ToString();
                        if (install == "") //no install info, must be later.
                            sb.AppendLine(versionKeyName + "  " + name);
                        else
                        {
                            if (sp != "" && install == "1")
                            {

                                sb.AppendLine(versionKeyName + "  " + name + "  SP" + sp);
                            }

                        }
                        if (name != "")
                        {
                            continue;
                        }
                        foreach (string subKeyName in versionKey.GetSubKeyNames())
                        {
                            RegistryKey subKey = versionKey.OpenSubKey(subKeyName);
                            name = (string)subKey.GetValue("Version", "");
                            if (name != "")
                                sp = subKey.GetValue("SP", "").ToString();
                            install = subKey.GetValue("Install", "").ToString();
                            if (install == "") //no install info, must be later.
                                sb.AppendLine(versionKeyName + "  " + name);
                            else
                            {
                                if (sp != "" && install == "1")
                                {
                                    sb.AppendLine("  " + subKeyName + "  " + name + "  SP" + sp);
                                }
                                else if (install == "1")
                                {
                                    sb.AppendLine("  " + subKeyName + "  " + name);
                                }
                            }
                        }
                    }
                }
            }


            return sb.ToString();
        }

        public static void StretchToolItem(ToolStrip bar, ToolStripItem itemstretch)
        {
            StretchToolItem(bar, itemstretch, false);
        }
        public static void StretchToolItem(ToolStrip bar, ToolStripItem itemstretch, bool noUPdate)
        {
            int all = bar.Size.Width;

            int other = 0;
            other += bar.Margin.Left;
            other += bar.Margin.Right;
            other += bar.GripRectangle.Width;
            other += bar.GripMargin.Left + bar.GripMargin.Right;

            foreach (ToolStripItem item in bar.Items)
            {
                if (item != itemstretch)
                {
                    other += item.Size.Width;
                    other += (item.Margin.Left + item.Margin.Right);
                    other += (item.Padding.Left + item.Padding.Right);
                }
            }

            itemstretch.Size = new Size(all - other, itemstretch.Size.Height);
            if (!noUPdate)
            {
                itemstretch.Visible = false;
                itemstretch.Visible = true;
            }
        }

        public static void setRegMaxIE(int val)
        {
            try
            {
                string exename = System.IO.Path.GetFileName(Application.ExecutablePath);
                Microsoft.Win32.RegistryKey regkey =
                    Microsoft.Win32.Registry.CurrentUser.CreateSubKey(
                        @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION");

                regkey.SetValue(exename, (int)val, Microsoft.Win32.RegistryValueKind.DWord);
                regkey.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public static string getAllArgs(int start)
        {
            return getAllArgs(Environment.CommandLine, start);
        }
        public static string getAllArgs(string cmdline, int start)
        {
            return getAllArgs(cmdline, start, false);
        }
        public static string getAllArgs(string cmdline, int start, bool bGetFirst)
        {
            cmdline = cmdline.TrimStart();
            string org = cmdline;

            char dq = '\0';
            int phase = 0;
            string result = string.Empty;
            int i = 0;
            for (i = 0; i < cmdline.Length; ++i)
            {
                char c = cmdline[i];
                if (phase == 0)
                {
                    if (c == '"' || c == '\'')
                    {
                        dq = c;
                    }
                    else
                    {
                        result += c;
                    }
                    phase = 1;
                }
                else if (phase == 1)
                {
                    if (dq != '\0')
                    {
                        if (dq == c)
                        {
                            break;
                        }
                        else
                        {
                            result += c;
                            continue;
                        }
                    }
                    else
                    {
                        if (char.IsWhiteSpace(c))
                        {
                            break;
                        }
                        else
                        {
                            result += c;
                            continue;
                        }
                    }
                }
            }

            string remain = string.Empty;
            if (cmdline.Length > i)
            {
                remain = cmdline.Substring(i + 1);
            }
            remain = remain.TrimStart();

            if (bGetFirst)
            {
                if (start <= 0)
                {
                    return result;
                }

                string prev = string.Empty;
                if (dq != '\0')
                {
                    prev += dq;
                    prev += result;
                    prev += dq;
                }
                else
                {
                    prev = result;
                }

                return prev + " " + getAllArgs(remain, start - 1, bGetFirst);
            }
            else
            {
                if (start <= 1)
                {
                    return remain;
                }

                return getAllArgs(remain, start - 1);
            }
        }

        public static string unDoubleQuote(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            if (s[0] == '"' || s[0] == '\'')
            {
                s = s.Trim(s[0]);
            }

            return s;
        }


        public static string GetDnsAdress()
        {
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();
                    IPAddressCollection dnsAddresses = ipProperties.DnsAddresses;

                    foreach (IPAddress dnsAdress in dnsAddresses)
                    {
                        if (dnsAdress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            return dnsAdress.ToString();
                    }
                }
            }

            // throw new InvalidOperationException("Unable to find DNS Address");
            return null;
        }

        // https://stackoverflow.com/a/8809437
        public static string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        //https://stackoverflow.com/a/14826068
        public static string ReplaceLast(string Source, string Find, string Replace)
        {
            int place = Source.LastIndexOf(Find);

            if (place == -1)
                return Source;

            string result = Source.Remove(place, Find.Length).Insert(place, Replace);
            return result;
        }

        public static object EasyRegRead(string path, string keyname)
        {
            try
            {
                return Registry.GetValue(path, keyname, null);
            }
            catch (Exception)
            {
            }
            return null;
        }

        //public static DialogResult Info(Control c, string message)
        //{
        //    if (c != null)
        //    {
        //        return CenteredMessageBox.Show(
        //            c,
        //            message,
        //            Application.ProductName,
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Information);
        //    }
        //    else
        //    {
        //        return CenteredMessageBox.Show(
        //            message,
        //            Application.ProductName,
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Information);
        //    }
        //}
        //public static DialogResult Info(string message)
        //{
        //    return Info(Form.ActiveForm, message);
        //}
        //public static DialogResult Alert(Control c, string message)
        //{
        //    if (c != null)
        //    {
        //        return CenteredMessageBox.Show(
        //            c,
        //            message,
        //            Application.ProductName,
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Exclamation);
        //    }
        //    else
        //    {
        //        return CenteredMessageBox.Show(
        //        message,
        //        Application.ProductName,
        //        MessageBoxButtons.OK,
        //        MessageBoxIcon.Exclamation);
        //    }
        //}
        ///// <summary>
        ///// Show alert message box
        ///// </summary>
        ///// <param name="message"></param>
        ///// <returns></returns>
        //public static DialogResult Alert(string message)
        //{
        //    return Alert(Form.ActiveForm, message);
        //}

        //public static DialogResult Alert(Control c, Exception ex)
        //{
        //    return Alert(c, ex.Message);
        //}

        ///// <summary>
        ///// Show alert message box with text of ex.message
        ///// </summary>
        ///// <param name="ex"></param>
        ///// <returns></returns>
        //public static DialogResult Alert(Exception ex)
        //{
        //    return Alert(ex.Message);
        //}

        public static bool IsPointInScreen(int x, int y)
        {
            return IsPointInScreen(new Point(x, y));
        }
        public static bool IsPointInScreen(Point pt)
        {
            foreach (Screen s in Screen.AllScreens)
            {
                if (s.WorkingArea.Contains(pt))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsRectAppearInScreen(Rectangle rect)
        {
            Point topleft = new Point(rect.X, rect.Y);
            if (IsPointInScreen(topleft))
                return true;

            Point topright = new Point(rect.X + rect.Width, rect.Y);
            if (IsPointInScreen(topright))
                return true;

            Point bottomleft = new Point(rect.X, rect.Y + rect.Height);
            if (IsPointInScreen(bottomleft))
                return true;

            Point bottomright = new Point(rect.X + rect.Width, rect.Y + rect.Bottom);
            if (IsPointInScreen(bottomright))
                return true;

            return false;
        }
        public static bool IsAllPointInScreen(Rectangle rect)
        {
            Point topleft = new Point(rect.X, rect.Y);
            if (!IsPointInScreen(topleft))
                return false;

            Point topright = new Point(rect.X + rect.Width, rect.Y);
            if (!IsPointInScreen(topright))
                return false;

            Point bottomleft = new Point(rect.X, rect.Y + rect.Height);
            if (!IsPointInScreen(bottomleft))
                return false;

            Point bottomright = new Point(rect.X + rect.Width, rect.Y + rect.Bottom);
            if (!IsPointInScreen(bottomright))
                return false;

            return true;
        }
        public static bool IsAllPointNotInScreen(Rectangle rect)
        {
            Point topleft = new Point(rect.X, rect.Y);
            if (IsPointInScreen(topleft))
                return false;

            Point topright = new Point(rect.X + rect.Width, rect.Y);
            if (IsPointInScreen(topright))
                return false;

            Point bottomleft = new Point(rect.X, rect.Y + rect.Height);
            if (IsPointInScreen(bottomleft))
                return false;

            Point bottomright = new Point(rect.X + rect.Width, rect.Y + rect.Bottom);
            if (IsPointInScreen(bottomright))
                return false;

            return true;
        }
        public static void SetFontAll(Control cnt)
        {
            if (cnt is Form)
            {
                // cnt.Font = SystemFonts.CaptionFont;
            }
            else if (cnt is Button)
            {
                // cnt.Font = SystemFonts.DialogFont;// MenuFont;
            }
            else if (cnt is TextBox)
            {
                // cnt.Font = SystemFonts.IconTitleFont;
            }
            else if (cnt is Label)
            {
                // cnt.Font = SystemFonts.MenuFont;
            }
            else if (cnt is CheckBox)
            {
                // cnt.Font = SystemFonts.MenuFont;
            }

            foreach (Control c in cnt.Controls)
            {
                SetFontAll(c);
            }
        }

        static string GetTrimmedFilename(string filename, int maxlen)
        {
            int lastdot = filename.LastIndexOf('.');
            if (lastdot < 0)
            {
                // no extention just strip
                return filename.Substring(0, maxlen);
            }


            // namepart.extpart
            string namepart = filename.Substring(0, lastdot);
            string extpart = filename.Substring(lastdot);
            if (extpart.Length > maxlen)
            {
                // extention itself over maxlen, give up
                return null;
            }

            string newnamepart = namepart.Substring(0, maxlen - extpart.Length);
            string ret = newnamepart + extpart;
            Debug.Assert(ret.Length <= maxlen);
            return ret;
        }
        public static string GetMaxpathTrimmedPath(string path, int maxlen)
        {
            if (string.IsNullOrEmpty(path))
                return path;

            if (path.Length <= maxlen)
            {
                // upto 259 char
                return path;
            }

            // continuous separator should be one
            path = path.Replace('/', '\\');
            while (path.IndexOf(@"\\") >= 0)
            {
                path = path.Replace(@"\\", @"\");
            }
            if (path.Length <= maxlen)
            {
                // upto 259 char
                return path;
            }

            // find directory
            int lastbackslash = path.LastIndexOf('\\');
            if (lastbackslash < 0)
            {
                // no directory
                return GetTrimmedFilename(path, maxlen);
            }

            string dir = path.Substring(0, lastbackslash);
            string filepart = path.Substring(lastbackslash + 1);
            if (dir.Length >= maxlen)
            {
                // dir itself over MAX_PATH, give up
                return null;
            }

            string nameret = GetTrimmedFilename(filepart, maxlen - dir.Length - 1);
            if (string.IsNullOrEmpty(nameret))
                return null;

            return dir + '\\' + nameret;
        }
        public static string GetMaxpathTrimmedPath(string path)
        {
            return GetMaxpathTrimmedPath(path, 259);
        }

        static readonly string KEY_X = "X";
        static readonly string KEY_Y = "Y";
        static readonly string KEY_WIDTH = "Width";
        static readonly string KEY_HEIGHT = "Height";

        public static bool LoadFormXYWH(Form f, string section, HashIni ini)
        {
            if (f == null)
                return false;

            int x = 0, y = 0;
            int width = 0, height = 0;
            if (Profile.GetInt(section, KEY_X, 0, out x, ini) &&
                Profile.GetInt(section, KEY_Y, 0, out y, ini) &&
                Profile.GetInt(section, KEY_WIDTH, 0, out width, ini) &&
                Profile.GetInt(section, KEY_HEIGHT, 0, out height, ini))
            {
                Point pt = new Point(x, y);
                Size size = new Size(width, height);

                Rectangle r = new Rectangle(pt, size);
                if (IsRectAppearInScreen(r))
                {
                    f.Location = new Point(x, y);
                    f.Size = new Size(width, height);
                    f.StartPosition = FormStartPosition.Manual;
                    return true;
                }
            }
            return false;
        }
        public static bool SaveFormXYWH(Form f, string section, HashIni ini)
        {
            if (f == null)
                return false;

            if (f.WindowState == FormWindowState.Normal)
            {
                Profile.WriteInt(section, KEY_X, f.Location.X, ini);
                Profile.WriteInt(section, KEY_Y, f.Location.Y, ini);
                Profile.WriteInt(section, KEY_WIDTH, f.Size.Width, ini);
                Profile.WriteInt(section, KEY_HEIGHT, f.Size.Height, ini);
            }
            return true;
        }
        public static void LoadListViewColumnWidth(ListView lv, string section, string key, HashIni ini)
        {
            foreach (ColumnHeader ch in lv.Columns)
            {
                string thiskey = key;
                if (!string.IsNullOrEmpty(ch.Name))
                {
                    thiskey += ch.Name;
                }
                else
                {
                    thiskey += lv.Columns.IndexOf(ch).ToString();
                }
                int colwidth = 0;
                if (Profile.GetInt(section, thiskey, 0, out colwidth, ini))
                    ch.Width = colwidth;
            }
        }
        public static bool SaveListViewColumnWidth(ListView lv, string section, string key, Ambiesoft.HashIni ini)
        {
            bool failed = false;
            foreach (ColumnHeader ch in lv.Columns)
            {
                string thiskey = key;
                if (!string.IsNullOrEmpty(ch.Name))
                {
                    thiskey += ch.Name;
                }
                else
                {
                    thiskey += lv.Columns.IndexOf(ch).ToString();
                }
                failed |= !Profile.WriteInt(section, thiskey, ch.Width, ini);
            }
            return !failed;
        }

        static readonly string KEY_COMBO_CURRENT = "ComboCurrentText";
        static readonly string KEY_COMBO_ITEMS = "ComboTexts";
        public static void LoadComboBox(ComboBox cmb, string section, int max, HashIni ini)
        {
            if (cmb == null || cmb.IsDisposed)
                return;

            if (max > 0)
            {
                string[] items;
                Profile.GetStringArray(section, KEY_COMBO_ITEMS, out items, ini);

                var itemsToAdd = new List<string>(items);
                itemsToAdd = itemsToAdd.GetRange(0, Math.Min(items.Length,max));
                cmb.Items.AddRange(itemsToAdd.ToArray());
            }


            string current;
            Profile.GetString(section, KEY_COMBO_CURRENT, string.Empty, out current, ini);
            cmb.Text = current;
        }
        public static bool SaveComboBox(ComboBox cmb, string section, int max, HashIni ini)
        {
            if (cmb == null || cmb.IsDisposed)
                return false;

            Profile.WriteString(section, KEY_COMBO_CURRENT, cmb.Text, ini);

            List<string> itemsToSave = new List<string>();
            if(!string.IsNullOrEmpty(cmb.Text) && !cmb.Items.Contains(cmb.Text))
                itemsToSave.Add(cmb.Text);
            int saveCount = 0;
            foreach(var item in cmb.Items)
            {
                if (!string.IsNullOrEmpty(item.ToString()))
                {
                    itemsToSave.Add(item.ToString());
                    ++saveCount;
                }
                if (saveCount >= max)
                    break;
            }

            Profile.WriteStringArray(section, KEY_COMBO_ITEMS, itemsToSave.ToArray(),ini);
            return true;
        }



        // https://stackoverflow.com/a/26558102
        public static string GetSha1(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        static StringBuilder sbOut;
        static StringBuilder sbErr;
        static void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
                sbErr.AppendLine(e.Data);
        }

        static void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
                sbOut.AppendLine(e.Data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="arguments"></param>
        /// <param name="encoding"></param>
        /// <param name="retval"></param>
        /// <param name="output"></param>
        /// <param name="err"></param>
        public static void OpenCommandGetResult(
            string filename,
            string arguments,
            Encoding encoding,
            out int retval,
            out string output,
            out string err)
        {
            ProcessStartInfo si = new ProcessStartInfo();
            si.FileName = filename;
            si.Arguments = arguments;
            si.StandardOutputEncoding = encoding;
            si.RedirectStandardOutput = true;
            si.StandardErrorEncoding = encoding;
            si.RedirectStandardError = true;
            si.UseShellExecute = false;
            si.CreateNoWindow = true;

            Process process = new Process();
            process.OutputDataReceived += new DataReceivedEventHandler(process_OutputDataReceived);
            process.ErrorDataReceived += new DataReceivedEventHandler(process_ErrorDataReceived);
            process.StartInfo = si;

            sbOut = new StringBuilder();
            sbErr = new StringBuilder();

            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();

            retval = process.ExitCode;
            output = sbOut.ToString();
            err = sbErr.ToString();
        }

        public static void OpenCommandGetResultCallback(
            string filename,
            string arguments,
            Encoding encoding,
            out int retval,
            DataReceivedEventHandler outputDelegate,
            DataReceivedEventHandler errputDelegate,
            EventHandler OnProcessCreated,
            out Process processRet)
        {
            ProcessStartInfo si = new ProcessStartInfo();
            si.FileName = filename;
            si.Arguments = arguments;
            si.StandardOutputEncoding = encoding;
            si.RedirectStandardOutput = true;
            si.StandardErrorEncoding = encoding;
            si.RedirectStandardError = true;
            si.UseShellExecute = false;
            si.CreateNoWindow = true;

            Process process = new Process();
            process.OutputDataReceived += new DataReceivedEventHandler(outputDelegate);
            process.ErrorDataReceived += new DataReceivedEventHandler(errputDelegate);
            process.StartInfo = si;

            process.Start();
            processRet = process;
            if (OnProcessCreated != null)
                OnProcessCreated(process, new EventArgs());

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();

            retval = process.ExitCode;
        }


        public static string doubleQuoteIfSpace(string input)
        {
            if (input == null)
                return null;

            if (input.Length == 0)
                return "\"\"";

            string t = input.TrimStart();
            if (t[0] == '"')
                return input;

            foreach (char c in t)
            {
                if (char.IsWhiteSpace(c))
                    return "\"" + input + "\"";
            }
            return input;
        }

        public static string pathToFileProtocol(string input)
        {
            try
            {
                Uri u = new Uri(input);
                return u.AbsoluteUri;
            }
            catch (Exception)
            { }
            return input;
        }

        public static string getAssemblyVersion(Assembly assembly, int keta)
        {
            StringBuilder sb = new StringBuilder();
            if (keta >= 1)
            {
                sb.Append(assembly.GetName().Version.Major.ToString());
                sb.Append(".");
            }
            if (keta >= 2)
            {
                sb.Append(assembly.GetName().Version.Minor.ToString());
                sb.Append(".");
            }
            if (keta >= 3)
            {
                sb.Append(assembly.GetName().Version.Build.ToString());
                sb.Append(".");
            }
            if (keta >= 4)
            {
                sb.Append(assembly.GetName().Version.Revision.ToString());
                sb.Append(".");
            }

            return sb.ToString().TrimEnd('.');
        }
        public static string getAssemblyVersion(Assembly assembly)
        {
            return getAssemblyVersion(assembly, 2);
        }
        public static string getAssemblyCompany(Assembly assembly)
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
            var companyName = versionInfo.CompanyName;
            return companyName;
        }
        public static string getAssemblyCopyright(Assembly assembly)
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
            return versionInfo.LegalCopyright;
        }
        
        public static string getPathFromUrl(string url)
        {
            try
            {
                Uri u = new Uri(url);
                return u.AbsolutePath;
            }
            catch (Exception)
            { }
            return url;
        }

        // https://stackoverflow.com/a/918162
        public static string UpperCaseUrlEncode(string s, Encoding encoding)
        {
            char[] temp = HttpUtility.UrlEncode(s, encoding).ToCharArray();
            for (int i = 0; i < temp.Length - 2; i++)
            {
                if (temp[i] == '%')
                {
                    temp[i + 1] = char.ToUpper(temp[i + 1]);
                    temp[i + 2] = char.ToUpper(temp[i + 2]);
                }
            }
            return new string(temp);
        }

        public static string GetSimpleVersion(Assembly asm)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(asm.GetName().Version.Major.ToString());
            sb.Append(".");
            sb.Append(asm.GetName().Version.Minor.ToString());

            return sb.ToString();
        }

        public static bool IsSameFile(string file1, string file2)
        {
            if (string.IsNullOrEmpty(file1) || string.IsNullOrEmpty(file2))
                return false;

            file1 = file1.Replace("/", @"\");
            file2 = file2.Replace("/", @"\");

            try
            {
                string full1 = Path.GetFullPath(file1);
                string full2 = Path.GetFullPath(file2);

                return string.Compare(full1, full2, true) == 0;
            }
            catch
            { }
            return false;
        }

        public static bool OpenUrlWithBrowser(string url)
        {
            try
            {
                Process.Start(url);
                return true;
            }
            catch(Exception)
            { }
            return false;
        }
        //private static System.Reflection.Assembly CustomResolve(object sender, System.ResolveEventArgs args)
        //{
        //    if (args.Name.StartsWith("Ambiesoft.AmbLibcpp.x86"))
        //    {
        //        string filename = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
        //            "platform",
        //            string.Format("Ambiesoft.AmbLibcpp.{0}.dll",
        //                Environment.Is64BitProcess ? "x64" : "x86"));

        //        return System.Reflection.Assembly.LoadFile(filename);
        //    }
        //    return null;
        //}
        //public static void PrepareAmblibCppLoader()
        //{
        //    System.AppDomain.CurrentDomain.AssemblyResolve += CustomResolve;
        //}

        public static bool StartAsAdmin()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(Application.ExecutablePath);
                psi.Verb = "runas";
                string args = string.Empty;

                for (int i = 1; i < Environment.GetCommandLineArgs().Length; ++i)
                {
                    string line = Environment.GetCommandLineArgs()[i];
                    args += doubleQuoteIfSpace(line);
                    args += " ";
                }
                args = args.TrimEnd();
                psi.Arguments = args;
                
                Process.Start(psi);
                return true;
            }
            catch (Exception)
            { }
            return false;
        }
        
        // https://dobon.net/vb/dotnet/system/isadmin.html
        /// <summary>
        /// 現在アプリケーションを実行しているユーザーに管理者権限があるか調べる
        /// </summary>
        /// <returns>管理者権限がある場合はtrue。</returns>
        public static bool IsAdministrator()
        {
            //現在のユーザーを表すWindowsIdentityオブジェクトを取得する
            System.Security.Principal.WindowsIdentity wi =
                System.Security.Principal.WindowsIdentity.GetCurrent();
            //WindowsPrincipalオブジェクトを作成する
            System.Security.Principal.WindowsPrincipal wp =
                new System.Security.Principal.WindowsPrincipal(wi);
            //Administratorsグループに属しているか調べる
            return wp.IsInRole(
                System.Security.Principal.WindowsBuiltInRole.Administrator);
        }

        static Dictionary<Control, int> _dicTextBoxToDbCount;
        static Dictionary<Control, int> _dicTextBoxToDbTime;
        private static void MakeTripleClickTextBoxInternal(Control cont, int dbtime)
        {
            if (cont == null)
                return;

            if (_dicTextBoxToDbCount == null)
                _dicTextBoxToDbCount = new Dictionary<Control, int>();
            if (_dicTextBoxToDbTime == null)
                _dicTextBoxToDbTime = new Dictionary<Control, int>();

            _dicTextBoxToDbTime[cont] = dbtime;

            TextBox tb = cont as TextBox;
            if (tb != null)
            {
                tb.MouseClick += tb_MouseClick;
                tb.MouseDoubleClick += tb_MouseDoubleClick;
            }
            ComboBox cb = cont as ComboBox;
            if (cb != null)
            {
                cb.MouseClick += tb_MouseClick;
                cb.MouseDoubleClick += tb_MouseDoubleClick;
            }
        }

        static void tb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Control cont = sender as Control;
            if (cont == null)
                return;
            _dicTextBoxToDbCount[cont] = Environment.TickCount;
        }

        static void tb_MouseClick(object sender, MouseEventArgs e)
        {
            Control cont = sender as Control;
            if (cont == null)
                return;

            if (!_dicTextBoxToDbCount.ContainsKey(cont))
                return;
            if (!_dicTextBoxToDbTime.ContainsKey(cont))
                return;

            int dbTick = _dicTextBoxToDbCount[cont];
            int dbtime = _dicTextBoxToDbTime[cont];

            if (dbtime > (Environment.TickCount - dbTick))
            {
                TextBox tb = cont as TextBox;
                ComboBox cb = cont as ComboBox;
                if (tb != null)
                    tb.SelectAll();
                if (cb != null)
                    cb.SelectAll();
            }
        }

        public static void MakeTripleClickTextBox(TextBox tb, int dbtime)
        {
            MakeTripleClickTextBoxInternal(tb, dbtime);
        }
        public static void MakeTripleClickTextBox(TextBox tb)
        {
            MakeTripleClickTextBoxInternal(tb, SystemInformation.DoubleClickTime);
        }
        //public static void MakeTripleClickTextBox(ComboBox cb, uint dbtime)
        //{
        //    MakeTripleClickTextBoxInternal(cb, dbtime); 
        //}
        public static void ShowTextDialog(IWin32Window owner, string title, string label, string text, bool bReadOnly)
        {
            using (TextDialog td = new TextDialog())
            {
                td.txtBody.Font = new Font(FontFamily.GenericMonospace, td.txtBody.Font.Size + 1);

                td.panelTabRoot.Visible = false;
                td.lblLable.Visible = true;
                td.txtBody.Visible = true;

                td.Text = title;
                td.lblLable.Text = label;
                td.txtBody.Text = text;
                td.txtBody.ReadOnly = bReadOnly;
                td.ShowDialog(owner);
            }
        }
        public static void ShowTextDialog(IWin32Window owner, string title, List<KeyValuePair<string, string>> labelsAndTexts, bool bReadOnly)
        {
            using (TextDialog td = new TextDialog())
            {
                td.panelTabRoot.Visible = true;
                td.lblLable.Visible = false;
                td.txtBody.Visible = false;

                td.Text = title;

                TabControl tabControl = new TabControl();
                tabControl.Location = new Point(0,0);
                tabControl.Dock = DockStyle.Fill;
                td.panelTabRoot.Controls.Add(tabControl);

                for (int i = 0; i < labelsAndTexts.Count; ++i)
                {
                    TextBox textBox = new TextBox();
                    textBox.Font = new Font(FontFamily.GenericMonospace, textBox.Font.Size+1);
                    textBox.Multiline = true;
                    textBox.ReadOnly = bReadOnly;
                    textBox.Dock = DockStyle.Fill;
                    textBox.ScrollBars = ScrollBars.Both;

                    TabPage tabPage = new TabPage();

                    // Add TabPage to TabControl
                    tabControl.TabPages.Add(tabPage);

                    // Add TextBox to TabPage
                    tabPage.Controls.Add(textBox);

                    tabPage.Text = labelsAndTexts[i].Key;
                    textBox.Text = labelsAndTexts[i].Value;
                }

                
                td.ShowDialog(owner);
            }
        }
    
        static readonly string[] suffixes =  { "Bytes", "KB", "MB", "GB", "TB", "PB" };
        public static string FormatSize(Int64 bytes)
        {
            int counter = 0;
            decimal number = (decimal)bytes;
            //while (Math.Round(number / 1024) >= 1)
            while (number > 1024)
            {
                number = number / 1024;
                counter++;
            }
            return string.Format(counter==0 ? "{0}{1}" : "{0:n1}{1}", number, suffixes[counter]);
        }
        /// <summary>
        /// 1.234 => "1.23"
        /// </summary>
        /// <param name="dvalue"></param>
        /// <param name="keta"></param>
        /// <returns>Formatted String</returns>
        public static string FormatSize(double dvalue, UInt32 keta)
        {
            return string.Format(string.Format("{{0:n{0}}}", keta), dvalue);
        }
        public static string FormatSize(double dvalue)
        {
            return FormatSize(dvalue, 2);
        }

        public static string GetOpenFileDialog(string title, Dictionary<string, string[]> extensions)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                StringBuilder sbFilter = new StringBuilder();
                if (extensions != null)
                {
                    foreach (KeyValuePair<string, string[]> entry in extensions)
                    {
                        StringBuilder sbExts = new StringBuilder();
                        foreach (string t in entry.Value)
                        {
                            // *.exe,*.com
                            sbExts.Append(";*." + t.TrimStart('*').TrimStart('.'));
                        }
                        sbFilter.AppendFormat("|{0} ({1})|{1}",
                            entry.Key,
                            sbExts.ToString().TrimStart(';'));
                    }
                }
                sbFilter.Append("|All Files(*.*)|*.*");
                ofd.Filter = sbFilter.ToString().TrimStart('|');
                ofd.Title = title;
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;

                if (ofd.ShowDialog() != DialogResult.OK)
                    return null;

                return ofd.FileName;
            }
        }
        public static string[] GetOpenFilesDialog(string title, Dictionary<string, string[]> extensions)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                StringBuilder sbFilter = new StringBuilder();
                if (extensions != null)
                {
                    foreach (KeyValuePair<string, string[]> entry in extensions)
                    {
                        StringBuilder sbExts = new StringBuilder();
                        foreach (string t in entry.Value)
                        {
                            // *.exe,*.com
                            sbExts.Append(";*." + t.TrimStart('*').TrimStart('.'));
                        }
                        sbFilter.AppendFormat("|{0} ({1})|{1}",
                            entry.Key,
                            sbExts.ToString().TrimStart(';'));
                    }
                }
                sbFilter.Append("|All Files(*.*)|*.*");
                ofd.Filter = sbFilter.ToString().TrimStart('|');
                ofd.Title = title;
                ofd.Multiselect = true;
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;

                if (ofd.ShowDialog() != DialogResult.OK)
                    return null;

                return ofd.FileNames;
            }
        }
        public static string GetOpenFolderDialog(string description)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = description;
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    return fbd.SelectedPath;
                }
            }
            return null;
        }
        public static string GetSaveFileDialog(string title, Dictionary<string, string[]> extensions)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                // ofd.FileName = "default.html";
                //ofd.InitialDirectory = @"C:\";
                StringBuilder sbFilter = new StringBuilder();
                if (extensions != null)
                {
                    foreach (KeyValuePair<string, string[]> entry in extensions)
                    {
                        StringBuilder sbExts = new StringBuilder();
                        foreach (string t in entry.Value)
                        {
                            // *.exe,*.com
                            sbExts.Append(";*." + t.TrimStart('*').TrimStart('.'));
                        }
                        sbFilter.AppendFormat("|{0} ({1})|{1}",
                            entry.Key,
                            sbExts.ToString().TrimStart(';'));
                    }
                }
                sbFilter.Append("|All Files(*.*)|*.*");
                sfd.Filter = sbFilter.ToString().TrimStart('|');
                //ofd.Filter = "Application(*.exe;*.com)|*.exe;*.com|All Files(*.*)|*.*";
                //ofd.FilterIndex = 2;
                sfd.Title = title;
                //ofd.RestoreDirectory = true;
                //ofd.CheckFileExists = true;
                //ofd.CheckPathExists = true;

                if (sfd.ShowDialog() != DialogResult.OK)
                    return null;

                return sfd.FileName;
            }
        }

        [Flags]
        public enum GETOPENFILEDIALOGTYPE
        {
            NONE =0,
            APP       = 1 << 0,
            IMAGE     = 1 << 1,
            HTML      = 1 << 2
        };
        public static string GetOpenFileDialog(string title, GETOPENFILEDIALOGTYPE gofdt)
        {
            var extentions = new Dictionary<string, string[]>();
            if (gofdt.HasFlag(GETOPENFILEDIALOGTYPE.APP))
                extentions["Application"] = new string[] { "exe", "com" };
            if (gofdt.HasFlag(GETOPENFILEDIALOGTYPE.IMAGE))
                extentions["Image"] = new string[] { "bmp", "jpg", "jpeg", "gif", "png" };
            if (gofdt.HasFlag(GETOPENFILEDIALOGTYPE.HTML))
                extentions["html"] = new string[] { "html", "htm" };

            return GetOpenFileDialog(title, extentions);
        }
        public static string GetSaveFileDialog(string title, GETOPENFILEDIALOGTYPE gofdt)
        {
            var extentions = new Dictionary<string, string[]>();
            if (gofdt.HasFlag(GETOPENFILEDIALOGTYPE.APP))
                extentions["Application"] = new string[] { "exe", "com" };
            if (gofdt.HasFlag(GETOPENFILEDIALOGTYPE.IMAGE))
                extentions["Image"] = new string[] { "bmp", "jpg", "jpeg", "gif", "png" };
            if (gofdt.HasFlag(GETOPENFILEDIALOGTYPE.HTML))
                extentions["html"] = new string[] { "html", "htm" };

            return GetSaveFileDialog(title, extentions);
        }
        public static string GetOpenFileDialog(string title)
        {
            return GetOpenFileDialog(title, null);
        }
        public static string GetSaveFileDialog(string title)
        {
            return GetSaveFileDialog(title, null);
        }

        [Flags]
        public enum EXITWINTYPE
        {
            EXITWIN_LOGOFF = 0x00000000,
            EXITWIN_SHUTDOWN = 0x00000001,
            EXITWIN_REBOOT = 0x00000002,
            EXITWIN_FORCE = 0x00000004,
            EXITWIN_POWEROFF = 0x00000008,
            EXITWIN_FORCEIFHUNG = 0x00000010,
        }
        public static bool ExitWin(EXITWINTYPE exitType)
        {
            return Win32.DoExitWin((int)exitType);
        }

        public static string GetRatioString(double part, double all, int keta)
        {
            if (keta <= 0)
                return ((part / all) * 100.0).ToString("#");

            string format = "#.";
            while (keta-- > 0)
                format += "#";
            return ((part / all) * 100.0).ToString(format);
        }
        public static string GetRatioString(double part, double all)
        {
            return GetRatioString(part, all, 2);
        }
        public static string GetRatioString(long part, long all, int keta)
        {
            return GetRatioString((double)part, (double)all, keta);
        }
        public static string GetRatioString(long part, long all)
        {
            return GetRatioString((double)part, (double)all, 2);
        }
        //public static string GetRatioString(int part, int all)
        //{
        //    return GetRatioString((double)part, (double)all);
        //}

        // https://stackoverflow.com/questions/8517159/how-do-i-detect-at-runtime-that-net-version-4-5-is-currently-running-your-code
        public static bool IsNet45OrNewer()
        {
            // Class "ReflectionContext" exists from .NET 4.5 onwards.
            return Type.GetType("System.Reflection.ReflectionContext", false) != null;
        }

        // https://stackoverflow.com/a/43495701
        public static bool IsNet46OrNewer()
        {
            // API changes in 4.6: https://github.com/Microsoft/dotnet/blob/master/releases/net46/dotnet46-api-changes.md
            return Type.GetType("System.AppContext, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", false) != null;
        }

        public static bool IsNet461OrNewer()
        {
            // API changes in 4.6.1: https://github.com/Microsoft/dotnet/blob/master/releases/net461/dotnet461-api-changes.md
            return Type.GetType("System.Data.SqlClient.SqlColumnEncryptionCngProvider, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", false) != null;
        }

        public static bool IsNet462OrNewer()
        {
            // API changes in 4.6.2: https://github.com/Microsoft/dotnet/blob/master/releases/net462/dotnet462-api-changes.md
            return Type.GetType("System.Security.Cryptography.AesCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", false) != null;
        }

        public static bool IsNet47OrNewer()
        {
            // API changes in 4.7: https://github.com/Microsoft/dotnet/blob/master/releases/net47/dotnet47-api-changes.md
            return Type.GetType("System.Web.Caching.CacheInsertOptions, System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", false) != null;
        }
        public static bool IsNet48OrNewer()
        {
            // API changes in 4.8: https://github.com/Microsoft/dotnet/blob/main/releases/net48/dotnet48-api-changes.md
            return Type.GetType("System.ServiceModel.Description.ServiceHealthModel", false) != null;
        }


        public static bool IsAlmostSame(double d1, double d2, double diff)
        {
            if (d2 == 0 && d1 == 0)
            {
                return true;
            }
            if (d2 == 0 || d1 == 0)
            {
                return false;
            }
            double mustOne = d1 / d2;
            return (1-diff) < mustOne && mustOne < (1+diff);
        }
        public static bool IsAlmostSame(double d1, double d2)
        {
            return IsAlmostSame(d1, d2, 0.05);
        }


        private class FileNameComparer : IEqualityComparer<string>
        {
            public bool Equals(string left, string right)
            {
                return AmbLib.IsSameFile(left, right);
            }

            public int GetHashCode(string s)
            {
                return s.GetHashCode();
            }
        }
        public static bool HasSameFile(string[] files)
        {
            return files.Length != files.Distinct(new FileNameComparer()).Count();
        }

        enum ListViewMove
        {
            ListViewMoveUp,
            ListViewMoveDown,
        }
        static bool ListViewItemMoveCommon(ListView list, ListViewItem item, ListViewMove move)
        {
            if(move==ListViewMove.ListViewMoveUp && (item.Index == 0))
				return false;
			if (move == ListViewMove.ListViewMoveDown && (item.Index >= list.Items.Count-1))
				return false;

			Debug.Assert(move==ListViewMove.ListViewMoveUp||move==ListViewMove.ListViewMoveDown);
			int targetI = item.Index + (move == ListViewMove.ListViewMoveUp ? -1 : 1);
			Debug.Assert(0 <= targetI && targetI < list.Items.Count);

            try
			{
                list.BeginUpdate();
				list.Items.RemoveAt(item.Index);
				list.Items.Insert(targetI, item);
				list.EnsureVisible(item.Index);
			}
            finally
            {
                list.EndUpdate();
            }
            return true;
        }
        public static bool ListViewItemMoveUp(ListView list, ListViewItem item)
        {
            return ListViewItemMoveCommon(list, item, ListViewMove.ListViewMoveUp);
        }
        public static bool ListViewItemMoveDown(ListView list, ListViewItem item)
        {
            return ListViewItemMoveCommon(list, item, ListViewMove.ListViewMoveDown);
        }

        static CultureInfo jpCI_ = new CultureInfo("ja-Jp", false);
        public static string ToZenkaku(string text)
        {
            return Strings.StrConv(text, VbStrConv.Wide, jpCI_.LCID);
        }
        public static string ToHankaku(string text)
        {
            return Strings.StrConv(text, VbStrConv.Narrow, jpCI_.LCID);
        }

        public static bool IsFolderEmpty(string folder)
        {
            if (!Directory.Exists(folder))
                return true;
            try
            {
                DirectoryInfo di = new DirectoryInfo(folder);
                return di.GetFiles("*.*", SearchOption.TopDirectoryOnly).Length == 0 &&
                    di.GetDirectories().Length == 0;
            }
            catch
            { }
            return false;
        }

        public static string toWindowsNewLine(string s)
        {
            if (s == null)
                return null;
            return s.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
        }

        public static string truncateString(string text, int size)
        {
            if (string.IsNullOrEmpty(text))
                return text;
            if (text.Length <= size)
                return text;

            string suffix = "...";
            int truncatedSize = size - suffix.Length;
            if (truncatedSize <= 0)
            {
                suffix = string.Empty;
                truncatedSize = size;
            }
            if (truncatedSize <= 0)
                return text.Substring(0, size);
            return text.Substring(0, truncatedSize) + suffix;
        }
        static string GetNonExistantFile(string folder, int tried)
        {
            if (tried >= 100)
                return null;
            if (string.IsNullOrEmpty(folder))
                return null;
            Directory.CreateDirectory(folder);
            if (!Directory.Exists(folder))
                return null;

            string id = Guid.NewGuid().ToString();
            if ((folder.Length + id.Length) >= 260)
            {
                id = Environment.TickCount.ToString();
                if ((folder.Length + id.Length) >= 260)
                    return null;
            }
            string ret = Path.Combine(folder, id);
            if (File.Exists(ret))
                return GetNonExistantFile(folder, ++tried);
            return ret;
        }
        public static string GetNonExistantFile(string folder)
        {
            return GetNonExistantFile(folder, 0);
        }

        public static string GetNumberedFile(string file, int number)
        {
            if (string.IsNullOrEmpty(file))
                return file;

            string filenamenoext = Path.GetFileNameWithoutExtension(file);
            string ext = Path.GetExtension(file);

            Regex reg = new Regex(@"\[\d+\]$");
            if(reg.Match(filenamenoext).Success)
            {
                filenamenoext = reg.Replace(filenamenoext, "") + "[" + number.ToString() + "]";
            }
            else
            {
                filenamenoext += "[" + number.ToString() + "]";
            }
            return Path.Combine(Path.GetDirectoryName(file),
                filenamenoext + ext);
        }
        public static string MoveFileAsNew(string sourcefile, string destFolderOrFile)
        {
            if (!File.Exists(sourcefile))
                return null;
            string sourceFileName = Path.GetFileName(sourcefile);
            string destFile;
            if(Directory.Exists(destFolderOrFile))
            {
                destFile = Path.Combine(destFolderOrFile, sourceFileName);
            }
            else
            {
                destFile = destFolderOrFile;
            }

            int number = 1;
            do
            {
                try
                {
                    File.Move(sourcefile, destFile);
                    return destFile;
                }
                catch (IOException)
                {
                    // destFile already exists
                    destFile = GetNumberedFile(destFile, number++);
                    if (string.IsNullOrEmpty(destFile))
                        return null;
                }
                catch(Exception)
                {
                    return null;
                }
            } while (number < 100);

            return null;
        }

        public static bool ChangeFontSize(Control cont, bool bLarger)
        {
            try
            {
                float newsize = cont.Font.Size + (bLarger ? 1 : -1);
                if (newsize <= 1.0)
                    return false;
                Font font = new Font(cont.Font.FontFamily, newsize);
                cont.Font = font;
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }
        public static bool ChangeFontSize(Control cont, Message m)
        {
            // WM_MOUSEWHEEL
            if (m.Msg != 0x020A)
                return false;

            long w = m.WParam.ToInt64();
            ushort vk = (ushort)(w & 0xffff);
            if ((vk & 0x0008) == 0)  // MK_CONTROL
                return false;

            short distance = (short)((w >> 16) & 0xffff);
            return ChangeFontSize(cont, distance > 0);
        }

        // https://stackoverflow.com/a/29668285
        public static string GetGpuInfos()
        {
            StringBuilder sb = new StringBuilder();
            using (var searcher = new ManagementObjectSearcher("select * from Win32_VideoController"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    sb.AppendLine("Name  -  " + obj["Name"]);
                    sb.AppendLine("DeviceID  -  " + obj["DeviceID"]);
                    sb.AppendLine("AdapterRAM  -  " + obj["AdapterRAM"]);
                    sb.AppendLine("AdapterDACType  -  " + obj["AdapterDACType"]);
                    sb.AppendLine("Monochrome  -  " + obj["Monochrome"]);
                    sb.AppendLine("InstalledDisplayDrivers  -  " + obj["InstalledDisplayDrivers"]);
                    sb.AppendLine("DriverVersion  -  " + obj["DriverVersion"]);
                    sb.AppendLine("VideoProcessor  -  " + obj["VideoProcessor"]);
                    sb.AppendLine("VideoArchitecture  -  " + obj["VideoArchitecture"]);
                    sb.AppendLine("VideoMemoryType  -  " + obj["VideoMemoryType"]);
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }
        public static string[] GetGpuNames()
        {
            List<string> ret = new List<string>();
            using (var searcher = new ManagementObjectSearcher("select * from Win32_VideoController"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    ret.Add(obj["Name"].ToString());
                }
            }
            return ret.ToArray();
        }
        public static bool IsGpuIntelUHD
        {
            get
            {
                using (var searcher = new ManagementObjectSearcher("select * from Win32_VideoController"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        string name = obj["Name"].ToString().ToLower();
                        int i1 = name.IndexOf("intel");
                        int i2 = name.IndexOf("uhd");
                        if (i1 < 0 || i2 < 0)
                            return false;
                        return i1 < i2;
                    }
                }
                return false;
            }
        }

        static string GetLastPartFromPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;
            path = path.TrimEnd("/\\".ToCharArray());
            int pos = path.LastIndexOfAny("/\\".ToCharArray());
            if (pos < 0)
                return string.Empty;
            return path.Substring(pos + 1);
        }
        public static List<KeyValuePair<string,string>> GetSourceAndDestFiles(
            string src, 
            string dest,
            out List<string> dstDirs)
        {
            dstDirs = new List<string>();
            var ret = new List<KeyValuePair<string, string>>();
            if (string.IsNullOrEmpty(src))
                return ret;
            if (string.IsNullOrEmpty(dest))
                return ret;
            src = Path.GetFullPath(src);
            dest = Path.GetFullPath(dest);
            if (File.Exists(src))
            {
                if (Directory.Exists(dest) ||
                    (dest.EndsWith("/") || dest.EndsWith("\\")))
                {
                    // File2Dir
                    string newDest = Path.Combine(
                        dest,
                        Path.GetFileName(src));
                    ret.Add(new KeyValuePair<string, string>(src, newDest));
                    dstDirs.Add(Path.GetDirectoryName(newDest));
                    return ret;
                }

                // File2File
                ret.Add(new KeyValuePair<string, string>(src, dest));
                dstDirs.Add(Path.GetDirectoryName(dest));
                return ret;
            }
            if(Directory.Exists(src))
            {
                if(!Directory.Exists(dest))
                {
                    return ret;
                }
                // Dest Dir exists
                string curDirBack = Environment.CurrentDirectory;
                Environment.CurrentDirectory = src;
                string srcDirName = GetLastPartFromPath(src);

                var dupCheck = new SortedSet<string>();
                string[] srcdirs = Directory.GetDirectories(".", "*.*", SearchOption.AllDirectories);
                foreach (string tmp in srcdirs)
                {
                    string s = tmp;
                    if (s.Length > 2 && s[0] == '.' && s[1] == '\\')
                        s = s.Substring(2);
                    string newDest = Path.Combine(dest, srcDirName, s);
                    dupCheck.Add(newDest);
                    dstDirs.Add(newDest);
                }
                  
                string[] srces = Directory.GetFiles(".", "*.*", SearchOption.AllDirectories);

                foreach(string tmp in srces)
                {
                    string s = tmp;
                    if (s.Length > 2 && s[0] == '.' && s[1] == '\\')
                        s = s.Substring(2);
                    string newDest = Path.Combine(dest, srcDirName, s);
                    ret.Add(new KeyValuePair<string, string>(
                        Path.Combine(src, s),
                        newDest));
                    string newDir = Path.GetDirectoryName(newDest);
                    if(!dupCheck.Contains(newDir))
                    {
                        dupCheck.Add(newDir);
                        dstDirs.Add(newDir);
                    }
                }
                Environment.CurrentDirectory = curDirBack;
            }
            return ret;
        }

        public enum CFT
        {
            None = 0,
            Creation = 1,
            LastWrite = 2,
            LastAccess = 4,
            All = Creation | LastWrite | LastAccess,
        }
        public static bool CopyFileTime(string srcFile, string dstFile, CFT cft)
        {
            try
            {
                FileInfo fiSrc = new FileInfo(srcFile);
                FileInfo fiDst = new FileInfo(dstFile);
                if ((cft & CFT.Creation) == CFT.Creation)
                {
                    fiDst.CreationTime = fiSrc.CreationTime;
                }
                if ((cft & CFT.LastWrite) == CFT.LastWrite)
                {
                    fiDst.LastWriteTime = fiSrc.LastWriteTime;
                }
                if ((cft & CFT.LastAccess) == CFT.LastAccess)
                {
                    fiDst.LastAccessTime = fiSrc.LastAccessTime;
                }
                return true;
            }
            catch (Exception) { }
            return false;
        }
        public static bool CopyFileTime(string srcFile, string dstFile)
        {
            return CopyFileTime(srcFile, dstFile, CFT.All);
        }
        static bool DeleteAllEmptyDirectory(string dir, ref bool ok, ref List<KeyValuePair<string, Exception>> errors)
        {
            foreach (string d in Directory.GetDirectories(dir))
            {
                DeleteAllEmptyDirectory(d, ref ok, ref errors);
            }
            try
            {
                Directory.Delete(dir);
            }
            catch (Exception ex)
            {
                if (errors == null)
                    errors = new List<KeyValuePair<string, Exception>>();
                errors.Add(new KeyValuePair<string, Exception>(dir, ex));
                ok = false;
            }
            return ok;
        }
        public static bool DeleteAllEmptyDirectory(string dir, ref List<KeyValuePair<string, Exception>> errors)
        {
            bool ok = true;
            DeleteAllEmptyDirectory(dir, ref ok, ref errors);
            return ok;
        }
        public static bool DeleteAllEmptyDirectory(string dir)
        {
            bool ok = true;
            List<KeyValuePair<string, Exception>> errors = null;
            DeleteAllEmptyDirectory(dir, ref ok, ref errors);
            return ok;
        }
    }  // class Amblib
}  // namespace Ambiesoft