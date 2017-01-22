using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cms.BLL.Common.Services.Tests
{
    [TestClass()]
    public class AsyncTests
    {
        [TestMethod()]
        public void AsyncTest()
        {
            var a = new Async();
            a.DisplayValue();
            System.Diagnostics.Debug.WriteLine("MyClass() End.");
        }


        [TestMethod()]
        public void ListTest()
        {


        }
    }
}