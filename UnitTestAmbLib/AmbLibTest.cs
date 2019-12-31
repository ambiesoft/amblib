using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ambiesoft;

namespace UnitTestAmbLib
{
    [TestClass]
    public class AmbLibTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(true, AmbLib.HasFileExtension("aaa.exe", "exe"));
            Assert.AreEqual(true, AmbLib.HasFileExtension("aaa.EXE", "exe"));
            Assert.AreEqual(true, AmbLib.HasFileExtension("aaa.EXE", "EXE"));
            Assert.AreEqual(false, AmbLib.HasFileExtension("aaa.txt", "EXE"));
            Assert.AreEqual(false, AmbLib.HasFileExtension(@"C:\T\aaa.txt", "EXE"));
        }

        [TestMethod]
        public void TestFixSizedQueue()
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
        public void TestGetRatioString()
        {
            Assert.AreEqual<string>("100", AmbLib.GetRatioString(100, 100));
            Assert.AreEqual<string>("1", AmbLib.GetRatioString(1, 100));
            
            string s = AmbLib.GetRatioString(1, 0);
            Assert.IsTrue(s.Contains("∞") || s.Contains("Infinity"));
        }
    }
}
