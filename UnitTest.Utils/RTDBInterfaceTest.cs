using System;
using Framework.Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils.Helper;

namespace UnitTest.Utils
{
    [TestClass]
    public class RTDBInterfaceTest
    {
        [TestInitialize]
        public void TestInit()
        {
            AutofacModule.InitTest();
        }

        [TestMethod]
        public void RTDBTest()
        {
            RTDBHelper.GetDataByTagAndTime("HLU1_HLU1_FIC53101", DateTime.Now);
        }
    }
}