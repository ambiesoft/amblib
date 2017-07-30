using System;
using System.Collections.Generic;
using System.Text;
using Ambiesoft;
using System.Diagnostics;

namespace amblibTest
{
    using System.Windows.Forms;

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

            verify(AmbLib.HasFileExtension("aaa.tXt","txt"));

            verify(AmbLib.GetFileExtension("aaa.txt") == ".txt");
            verify(AmbLib.GetFileExtension(@"C:\Users\Bokkurin\Desktop\Passageway _ Define Passageway at Dictionary.com") == ".com");
            verify(AmbLib.IsFileNamable("aaa"));
            verify(AmbLib.IsFileNamable("aaa.txt"));
            verify(AmbLib.IsFileNamable("Ç†Ç†Ç†Ç†ÅB.aaa"));
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


            try
            {
                int i = 1;
                i--;

                int j = 10 / i;
            }
            catch (Exception ex)
            {
                AmbLib.ExceptionMessageBox(ex);
            }


            MessageBox.Show(AmbLib.GetIniPath());            
        }
    }
}
