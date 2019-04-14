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
        static void verify(bool b)
        {
            if (!b)
            {
                Console.WriteLine("BAD");
            }
        }

        static void Main(string[] args)
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
                    (sender, e) => { if (e != null) { sbOut.AppendLine(e.Data); } },
                    (sender, e) => { if (e != null) { sbErr.AppendLine(e.Data); } },
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
            Debug.Assert(sl==null || sl.Length < 260);

            // both long
            f1 = "aaaeaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaae.bffffffffffffffffffaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaae";
            sl = AmbLib.GetMaxpathTrimmedPath(f1);
            Debug.Assert(sl==null || sl.Length < 260);

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

            object ob=AmbLib.EasyRegRead(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\FileSystem",
                "NtfsDisable8dot3NameCreation");


            verify(AmbLib.HasFileExtension("aaa.tXt","txt"));

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

            FormMain form = new FormMain();
            form.ShowDialog();


            //try
            //{
            //    int i = 1;
            //    i--;

            //    int j = 10 / i;
            //}
            //catch (Exception ex)
            //{
            //    AmbLib.ExceptionMessageBox(ex);
            //}


            //MessageBox.Show(AmbLib.GetIniPath());            
        }
    }
}
