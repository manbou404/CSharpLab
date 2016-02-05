using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JFactory.CsSrcLib.Library
{
    [TestClass]
    public class BasicLibraryTest
    {
        [TestMethod]
        public void IsEmptyTests()
        {
            var a = (string)null;
            a.IsEmpty().IsTrue();

            var b = "";
            b.IsEmpty().IsTrue();

            var c = "    ";
            c.IsEmpty().IsTrue();

            var d = "abc";
            d.IsEmpty().IsFalse();
        }

        [TestMethod]
        public void CastTests()
        {
            var obj = new MemoryStream();

            // Cast成功の場合
            var b1 = false;
            var e1 = obj.As<Stream>(x => b1 = true);
            Assert.IsTrue(b1);
            Assert.AreEqual(e1.GetType(), typeof(MemoryStream));
            
            // Cast失敗の場合
            var b2 = false;
            var e2 = obj.As<FileStream>(x => b1 = true);
            Assert.IsFalse(b2);
            Assert.AreEqual(e2, null);
        }

        [TestMethod]
        [TestCase(5, 1, 10, 5)]
        [TestCase(0, 1, 10, 1)]
        [TestCase(1, 1, 10, 1)]
        [TestCase(2, 1, 10, 2)]
        [TestCase(9, 1, 10, 9)]
        [TestCase(10, 1, 10, 10)]
        [TestCase(11, 1, 10, 10)]
        public void ClampTests()
        {
            TestContext.Run((int value, int min, int max, int excpected) =>
            {
                value.Clamp(min, max).Is(excpected);
            });
        }

        [TestMethod]
        [TestCase(5, 1, 10, true)]
        [TestCase(0, 1, 10, false)]
        [TestCase(1, 1, 10, true)]
        [TestCase(2, 1, 10, true)]
        [TestCase(9, 1, 10, true)]
        [TestCase(10, 1, 10, true)]
        [TestCase(11, 1, 10, false)]
        public void IsRangeTests()
        {
            TestContext.Run((int value, int min, int max, bool excpected) =>
            {
                value.IsRange(min, max).Is(excpected);
            });
        }


        [TestMethod]
        public void ForEachTests()
        {
            Enumerable.Repeat(0, 5).ForEach(x => { });
        }

        [TestMethod]
        public void IndexedTests()
        {
            Enumerable.Range(0, 10).Indexed().ToList()
                .ForEach(x => Assert.AreEqual(x.Element, x.Index));
        }


        public TestContext TestContext { get; set; }
    }
}

