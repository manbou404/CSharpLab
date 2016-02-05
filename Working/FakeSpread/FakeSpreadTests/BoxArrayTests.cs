using Microsoft.VisualStudio.TestTools.UnitTesting;
using JFactory.CsSrcLib.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFactory.CsSrcLib.Forms.Tests
{
    [TestClass()]
    public class BoxArrayTests
    {
        [TestMethod()]
        public void BoxArrayTest()
        {
            var obj = new BoxArray<int>();
            obj.Width.Is(0);
            obj.Height.Is(0);

            obj.Height = 5;
            obj.Height.Is(5);
        }
    }
}