using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cms.BLL.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.Common.Services.Tests
{
    [TestClass()]
    public class ArithmeticTests
    {
        public readonly Arithmetic a = new Arithmetic();
        [TestMethod()]
        public void doPrintTest()
        {
            //var a = new Arithmetic();
            a.doPrint();
            //Assert.Fail();
        }

        [TestMethod()]
        public void orderTest()
        {
            //var a = new Arithmetic();
            a.order();
        }

        [TestMethod()]
        public void RepeatStringTest()
        {
            //var a = new Arithmetic();
            Console.Write(a.RepeatString("ndfsvsxf",3));
        }
    }
}