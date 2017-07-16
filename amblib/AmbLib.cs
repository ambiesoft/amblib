using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;

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
                        if(dnsAdress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            return dnsAdress.ToString();
                    }
                }
            }

            // throw new InvalidOperationException("Unable to find DNS Address");
            return null;
        }

           
        //public static string GetDnsAdress2()
        //{
        //    foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
        //    {
        //        if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
        //        {
        //            // Console.WriteLine(ni.Name);
        //            foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
        //            {
        //                if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        //                {
        //                    // Console.WriteLine(ip.Address.ToString());
        //                    // return ip.Address.ToString();
        //                    IPAddressCollection dnsAddresses = ni.GetIPProperties().DnsAddresses;
        //                    foreach (IPAddress dnsAdress in dnsAddresses)
        //                    {
        //                        return dnsAdress.ToString();
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return null;
        //}



    }  // class Amblib
}  // namespace Ambiesoft