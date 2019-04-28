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
    }
}
