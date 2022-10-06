using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ambiesoft;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace UnitTestAmbLib
{
    [TestClass]
    public class AmbLibTest
    {
        [TestMethod]
        public void TestFileExtention()
        {
            Assert.AreEqual(true, AmbLib.HasFileExtension("aaa.exe", "exe"));
            Assert.AreEqual(true, AmbLib.HasFileExtension("aaa.EXE", "exe"));
            Assert.AreEqual(true, AmbLib.HasFileExtension("aaa.EXE", "EXE"));
            Assert.AreEqual(false, AmbLib.HasFileExtension("aaa.txt", "EXE"));
            Assert.AreEqual(false, AmbLib.HasFileExtension(@"C:\T\aaa.txt", "EXE"));
        }

        [TestMethod]
        public void TestFixSizedQueueBasic()
        {
            FixSizedQueue<int> iq = new FixSizedQueue<int>(3);
            iq.Enqueue(1);
            Assert.IsFalse(iq.Filled);
            iq.Enqueue(2);
            iq.Enqueue(3);
            Assert.IsTrue(iq.Filled);
            int[] iqa = iq.ToArray();
            Assert.AreEqual(iqa[0], 1);
            Assert.AreEqual(iqa[1], 2);
            Assert.AreEqual(iqa[2], 3);

            iq.Enqueue(4);
            iqa = iq.ToArray();
            Assert.AreEqual(iq.Count, 3);
            Assert.AreEqual(iqa[0], 2);
            Assert.AreEqual(iqa[1], 3);
            Assert.AreEqual(iqa[2], 4);

            int dequed = iq.Enqueue(5);
            Assert.AreEqual(dequed, 2);

        }

        [TestMethod]
        public void TestFixSizedQueueChangeSize()
        {
            FixSizedQueue<string> sq = new FixSizedQueue<string>(5);
            sq.Enqueue("a");
            sq.Enqueue("b");
            sq.Enqueue("c");
            sq.Enqueue("d");
            sq.Enqueue("e");
            sq.Size = 3;
            Assert.AreEqual(sq.Count, 3);
            Assert.AreEqual(sq.Dequeue(), "c");
            Assert.AreEqual(sq.Dequeue(), "d");
            Assert.AreEqual(sq.Dequeue(), "e");
        }

        [TestMethod]
        public void TestGetRatioString()
        {
            Assert.AreEqual<string>("100", AmbLib.GetRatioString(100, 100));
            Assert.AreEqual<string>("1", AmbLib.GetRatioString(1, 100));
            Assert.IsTrue(AmbLib.GetRatioString(821874467, 4017871354).StartsWith("20"));
            Assert.IsTrue(AmbLib.GetRatioString((double)821874467, (double)4017871354).StartsWith("20"));
            Assert.IsTrue(AmbLib.GetRatioString(821874467L, 4017871354L).StartsWith("20"));
            Assert.AreEqual<string>(AmbLib.GetRatioString(821324874467, 39344017871354), "2.09");
            Assert.AreEqual<string>(AmbLib.GetRatioString(821324874467, 39344017871354, 0), "2");


            string s = AmbLib.GetRatioString(1, 0);
            Assert.IsTrue(s.Contains("∞") || s.Contains("Infinity"));
        }

        [TestMethod]
        public void TestFormatSize()
        {
            Assert.AreEqual(AmbLib.FormatSize(0), "0Bytes");
            Assert.AreEqual(AmbLib.FormatSize(1), "1Bytes");
            Assert.AreEqual(AmbLib.FormatSize(107008), "104.5KB");
            Assert.AreEqual(AmbLib.FormatSize(605652789), "577.6MB");
            Assert.AreEqual(AmbLib.FormatSize(600 * 1024 * 1024), "600.0MB");
            Assert.AreEqual(AmbLib.FormatSize(623 * 1024 * 1024), "623.0MB");
            Assert.AreEqual(AmbLib.FormatSize(6681167515), "6.2GB");

            Assert.AreEqual(AmbLib.FormatSize(0, 2), "0.00");
            Assert.AreEqual(AmbLib.FormatSize(1.345, 2), "1.35");
        }

        [TestMethod]
        public void TestIsAlmostSame()
        {
            Assert.IsTrue(AmbLib.IsAlmostSame(100, 99));
            Assert.IsTrue(AmbLib.IsAlmostSame(98, 99));
            Assert.IsTrue(AmbLib.IsAlmostSame(0.98, 0.99));
            Assert.IsFalse(AmbLib.IsAlmostSame(0.98, 3));
            Assert.IsFalse(AmbLib.IsAlmostSame(123, 456));

            Assert.IsTrue(AmbLib.IsAlmostSame(0, 0));
            Assert.IsFalse(AmbLib.IsAlmostSame(0, 0.001));
            Assert.IsFalse(AmbLib.IsAlmostSame(0.00103, 0));
            Assert.IsFalse(AmbLib.IsAlmostSame(1, -1));
        }

        [TestMethod]
        public void TestZenkakuHankaku()
        {
            Assert.AreEqual(AmbLib.ToHankaku("1"), "1");
            Assert.AreEqual(AmbLib.ToHankaku("１１１"), "111");
            Assert.AreEqual(AmbLib.ToHankaku("あああ"), "あああ");
            Assert.AreEqual(AmbLib.ToHankaku("　"), " ");
            Assert.AreEqual(AmbLib.ToHankaku("ａ"), "a");

            Assert.AreEqual(AmbLib.ToZenkaku("２２２"), "２２２");
            Assert.AreEqual(AmbLib.ToZenkaku("222"), "２２２");
        }

        [TestMethod]
        public void TestIsFolderEmpty()
        {
            {
                string dir = Environment.TickCount.ToString();
                Directory.CreateDirectory(dir);
                Assert.IsTrue(AmbLib.IsFolderEmpty(dir));


                string file = Path.Combine(dir, (Environment.TickCount + 1).ToString());
                File.WriteAllText(file, "aaa");
                Assert.IsFalse(AmbLib.IsFolderEmpty(dir));
                File.Delete(file);

                Directory.Delete(dir);
            }
            {
                string dir = Environment.TickCount.ToString();
                Directory.CreateDirectory(dir);
                Assert.IsTrue(AmbLib.IsFolderEmpty(dir));

                string dir2 = Path.Combine(dir, (Environment.TickCount + 1).ToString());
                Directory.CreateDirectory(dir2);
                Assert.IsFalse(AmbLib.IsFolderEmpty(dir));
                Directory.Delete(dir2);

                Directory.Delete(dir);
            }
        }

        [TestMethod]
        public void TestString()
        {
            string linux = "aaa\nbbb\nccc";
            string win = "aaa\r\nbbb\r\nccc";
            string mac = "aaa\rbbb\rccc";
            string mixed = "aaa\nbbb\nccc\r\nddd\r\neee\rzzz";
            string mixedWin = "aaa\r\nbbb\r\nccc\r\nddd\r\neee\r\nzzz";
            Assert.AreEqual(win, AmbLib.toWindowsNewLine(linux));
            Assert.AreEqual(win, AmbLib.toWindowsNewLine(mac));
            Assert.AreEqual(win, AmbLib.toWindowsNewLine(win));
            Assert.AreEqual(mixedWin, AmbLib.toWindowsNewLine(mixed));
        }
        [TestMethod]
        public void TestTrancateString()
        {
            Assert.AreEqual(null, AmbLib.truncateString(null, 0));
            Assert.AreEqual(null, AmbLib.truncateString(null, 1));
            Assert.AreEqual("", AmbLib.truncateString("a", 0));
            Assert.AreEqual("a", AmbLib.truncateString("a", 1));

            Assert.AreEqual("a", AmbLib.truncateString("a", 1));
            Assert.AreEqual("a", AmbLib.truncateString("ab", 1));
            Assert.AreEqual("a", AmbLib.truncateString("abc", 1));

            Assert.AreEqual("ab", AmbLib.truncateString("abc", 2));
            Assert.AreEqual("abc", AmbLib.truncateString("abc", 3));
            Assert.AreEqual("abc", AmbLib.truncateString("abcd", 3));
            Assert.AreEqual("abc", AmbLib.truncateString("abcdefg", 3));

            Assert.AreEqual("a...", AmbLib.truncateString("abcdefg", 4));
            Assert.AreEqual("ab...", AmbLib.truncateString("abcdefg", 5));

            Assert.AreEqual("aaaaaaa...", AmbLib.truncateString("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", 10));
        }

        [TestMethod]
        public void TestGetNonExistantFile()
        {
            string file = AmbLib.GetNonExistantFile(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            Assert.IsNotNull(file);
            Assert.IsFalse(File.Exists(file));
        }

        [TestMethod]
        public void TestGetNumberedFile()
        {
            {
                string file = "MyFile.pdf";
                Assert.AreEqual("MyFile[1].pdf", AmbLib.GetNumberedFile(file, 1));
                Assert.AreEqual("MyFile[2].pdf", AmbLib.GetNumberedFile(file, 2));
            }
            {
                string file = @"C:\T\TT TT\My File.txt";
                Assert.AreEqual(@"C:\T\TT TT\My File[1].txt", AmbLib.GetNumberedFile(file, 1));
                Assert.AreEqual(@"C:\T\TT TT\My File[2].txt", AmbLib.GetNumberedFile(file, 2));
            }
            {
                string file = @".\My File.txt";
                Assert.AreEqual(@".\My File[1].txt", AmbLib.GetNumberedFile(file, 1));
                Assert.AreEqual(@".\My File[2].txt", AmbLib.GetNumberedFile(file, 2));
            }
            {
                string file = @"..\..\.\My File.txt";
                Assert.AreEqual(@"..\..\.\My File[1].txt", AmbLib.GetNumberedFile(file, 1));
                Assert.AreEqual(@"..\..\.\My File[2].txt", AmbLib.GetNumberedFile(file, 2));
            }
            {
                string file = @"\\server\aaa\My File.txt";
                Assert.AreEqual(@"\\server\aaa\My File[1].txt", AmbLib.GetNumberedFile(file, 1));
                Assert.AreEqual(@"\\server\aaa\My File[2].txt", AmbLib.GetNumberedFile(file, 2));
            }

            {
                string file = "MyFile[10].pdf";
                Assert.AreEqual("MyFile[11].pdf", AmbLib.GetNumberedFile(file, 11));
                Assert.AreEqual("MyFile[12].pdf", AmbLib.GetNumberedFile(file, 12));
            }
            {
                string file = @"C:\T\TT TT\My File[123].txt";
                Assert.AreEqual(@"C:\T\TT TT\My File[1].txt", AmbLib.GetNumberedFile(file, 1));
                Assert.AreEqual(@"C:\T\TT TT\My File[2].txt", AmbLib.GetNumberedFile(file, 2));
            }
            {
                string file = @".\My File[5].txt";
                Assert.AreEqual(@".\My File[551].txt", AmbLib.GetNumberedFile(file, 551));
                Assert.AreEqual(@".\My File[211].txt", AmbLib.GetNumberedFile(file, 211));
            }
            {
                string file = @"..\..\.\My File[11111].txt";
                Assert.AreEqual(@"..\..\.\My File[133].txt", AmbLib.GetNumberedFile(file, 133));
                Assert.AreEqual(@"..\..\.\My File[2].txt", AmbLib.GetNumberedFile(file, 2));
            }
            {
                string file = @"\\server\aaa\My File.txt";
                Assert.AreEqual(@"\\server\aaa\My File[1].txt", AmbLib.GetNumberedFile(file, 1));
                Assert.AreEqual(@"\\server\aaa\My File[2].txt", AmbLib.GetNumberedFile(file, 2));
            }
        }

        [TestMethod]
        public void TestMoveFileAsNew()
        {
            {
                File.WriteAllText("aaa.txt", "aaa");
                File.WriteAllText("bbb.txt", "bbb");

                string newfile = AmbLib.MoveFileAsNew("aaa.txt", "bbb.txt");
                Assert.IsTrue(Regex.Match(Path.GetFileNameWithoutExtension(newfile), @"\[\d+\]$").Success);
            }

            {
                File.WriteAllText("aaa.txt", "aaa");
                File.WriteAllText("bbb.txt", "bbb");

                string fulla = Path.GetFullPath("aaa.txt");
                string fullb = Path.GetFullPath("bbb.txt");
                string newfile = AmbLib.MoveFileAsNew(fulla, fullb);
                Assert.IsTrue(Regex.Match(Path.GetFileNameWithoutExtension(newfile), @"\[\d+\]$").Success);
            }
        }

        [TestMethod]
        public void TestCopyFileTime1()
        {
            File.WriteAllText("aaa.txt", "aaa");
            File.WriteAllText("bbb.txt", "aaa");
            AmbLib.CopyFileTime("aaa.txt", "bbb.txt");

            FileInfo fiAAA = new FileInfo("aaa.txt");
            FileInfo fiBBB = new FileInfo("bbb.txt");
            Assert.AreEqual(fiAAA.CreationTime, fiBBB.CreationTime);
            Assert.AreEqual(fiAAA.CreationTimeUtc, fiBBB.CreationTimeUtc);
            Assert.AreEqual(fiAAA.LastWriteTime, fiBBB.LastWriteTime);
            Assert.AreEqual(fiAAA.LastWriteTimeUtc, fiBBB.LastWriteTimeUtc);
            Assert.AreEqual(fiAAA.LastAccessTime, fiBBB.LastAccessTime);
            Assert.AreEqual(fiAAA.LastAccessTimeUtc, fiBBB.LastAccessTimeUtc);
        }
        [TestMethod]
        public void TestCopyFileTime2()
        {
            File.Delete("aaa.txt");
            File.Delete("bbb.txt");
            File.WriteAllText("aaa.txt", "aaa");
            File.WriteAllText("bbb.txt", "aaa");

            {
                FileInfo fiAAA = new FileInfo("aaa.txt");
                FileInfo fiBBB = new FileInfo("bbb.txt");
                fiAAA.CreationTime = DateTime.Now.AddDays(1);
                fiAAA.LastWriteTime = DateTime.Now.AddDays(1);
                fiAAA.LastAccessTime = DateTime.Now.AddDays(1);

                Assert.AreNotEqual(fiAAA.CreationTime, fiBBB.CreationTime);
                Assert.AreNotEqual(fiAAA.CreationTimeUtc, fiBBB.CreationTimeUtc);
                Assert.AreNotEqual(fiAAA.LastWriteTime, fiBBB.LastWriteTime);
                Assert.AreNotEqual(fiAAA.LastWriteTimeUtc, fiBBB.LastWriteTimeUtc);
                Assert.AreNotEqual(fiAAA.LastAccessTime, fiBBB.LastAccessTime);
                Assert.AreNotEqual(fiAAA.LastAccessTimeUtc, fiBBB.LastAccessTimeUtc);
            }

            AmbLib.CopyFileTime("aaa.txt", "bbb.txt", AmbLib.CFT.LastAccess);

            {
                FileInfo fiAAA = new FileInfo("aaa.txt");
                FileInfo fiBBB = new FileInfo("bbb.txt");

                Assert.AreNotEqual(fiAAA.CreationTime, fiBBB.CreationTime);
                Assert.AreNotEqual(fiAAA.CreationTimeUtc, fiBBB.CreationTimeUtc);
                Assert.AreNotEqual(fiAAA.LastWriteTime, fiBBB.LastWriteTime);
                Assert.AreNotEqual(fiAAA.LastWriteTimeUtc, fiBBB.LastWriteTimeUtc);
                Assert.AreEqual(fiAAA.LastAccessTime, fiBBB.LastAccessTime);
                Assert.AreEqual(fiAAA.LastAccessTimeUtc, fiBBB.LastAccessTimeUtc);
            }
        }


        [TestMethod]
        public void TestDeleteAllEmptyDirectory()
        {
            {
                Directory.CreateDirectory("X");
                Directory.CreateDirectory("X\\Y");
                Directory.CreateDirectory("X\\Z");
                Directory.CreateDirectory("X\\Z\\zz");
                Directory.CreateDirectory("X\\Z\\zz2");
                Directory.CreateDirectory("X\\Z\\zz\\zzz");
                File.WriteAllText("X\\Z\\zz\\zzz\\file.txt", "text");
                Assert.IsFalse(AmbLib.DeleteAllEmptyDirectory("X"));

                File.Delete("X\\Z\\zz\\zzz\\file.txt");
                Assert.IsTrue(AmbLib.DeleteAllEmptyDirectory("X"));
            }
        }

        /// <summary>
        /// Compares two string lists using a loop.
        /// </summary>
        public bool CompareLists(List<string> list1, List<string> list2)
        {
            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i] != list2[i])
                    return false;
            }

            return true;
        }
        public bool CompareKVS(List<KeyValuePair<string, string>> kvs1, List<KeyValuePair<string, string>> kvs2)
        {
            if (kvs1.Count != kvs2.Count)
                return false;

            for (int i = 0; i < kvs1.Count; ++i)
            {
                if (kvs1[i].Key != kvs2[i].Key)
                    return false;
                if (kvs1[i].Value != kvs2[i].Value)
                    return false;
            }
            return true;
        }

        [TestMethod]
        public void TestGetSourceAndDestFiles()
        {
            List<string> dirs1;
            var files1 = AmbLib.GetSourceAndDestFiles(@"C:\Linkout", @"C:\T", out dirs1);

            List<string> dirs2;
            var files2 = AmbLib.GetSourceAndDestFiles(@"C:\Linkout\", @"C:\T", out dirs2);

            List<string> dirs3;
            var files3 = AmbLib.GetSourceAndDestFiles(@"C:\Linkout", @"C:\T\", out dirs3);

            List<string> dirs4;
            var files4 = AmbLib.GetSourceAndDestFiles(@"C:\Linkout\", @"C:\T\", out dirs4);

            Assert.IsTrue(CompareLists(dirs1, dirs2));
            Assert.IsTrue(CompareLists(dirs2, dirs3));
            Assert.IsTrue(CompareLists(dirs3, dirs4));

            Assert.IsTrue(CompareKVS(files1, files2));
            Assert.IsTrue(CompareKVS(files2, files3));
            Assert.IsTrue(CompareKVS(files3, files4));
        }

        [TestMethod]
        public void TestUrlEncode()
        {
            Assert.AreEqual(AmbLib.UpperCaseUrlEncode(null, Encoding.UTF8), null);
            Assert.AreEqual(AmbLib.UpperCaseUrlEncode(string.Empty, Encoding.UTF8), string.Empty);
        }
    }
}