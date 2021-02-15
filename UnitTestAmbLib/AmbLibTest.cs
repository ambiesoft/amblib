using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ambiesoft;

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
    }
}
