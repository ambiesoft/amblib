using System;
using System.Collections.Generic;
using System.Text;

namespace Ambiesoft
{
    using System.Drawing;
    using System.Windows.Forms;
    using System.IO;
    using Microsoft.Win32;

    public class AmbLib
    {
        public static bool IsFileNamable(string fn)
        {
            if (string.IsNullOrEmpty(fn))
                return false;

            if (fn.EndsWith(".") || fn.EndsWith(" "))
                return false;

            foreach (char c in fn)
            {
                if (c == '<' ||
                     c == '>' ||
                     c == ':' ||
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
            }

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



        public static void ExceptionMessageBox(System.Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(ex.Message,
            System.Windows.Forms.Application.ProductName,
            System.Windows.Forms.MessageBoxButtons.OK,
            System.Windows.Forms.MessageBoxIcon.Exclamation);
        }

        public static bool IsPointInMonitor(int x, int y)
        {
            return IsPointInMonitor(new Point(x, y));
        }
        public static bool IsPointInMonitor(Point pt)
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


        public static string GetIniPath()
        {
            FileInfo fi = new FileInfo(Application.ExecutablePath);
            string dir = fi.DirectoryName;
            string file = Path.GetFileNameWithoutExtension(fi.Name) + ".ini";
            return Path.Combine(dir, file);
        }


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


    }  // class Amblib
}  // namespace Ambiesoft