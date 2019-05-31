using System;
using Autofac;
using Framework.Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils.Helper;

namespace UnitTest.Utils
{
    [TestClass]
    public class RTDBInterfaceTest
    {
        private IContainer _container;

        [TestInitialize]
        public void TestInit()
        {
            _container = AutofacModule.InitTest();
        }

        [TestMethod]
        public void RTDBTest()
        {
            RTDBHelper.GetDataByTagAndTime("HLU1_HLU1_FIC53101", DateTime.Now);
        }
    }
}