using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JFactory.CsSrcLib.Library
{
    [TestClass]
    public class NewSourceLibraryTest
    {
        public enum Test
        {
            [DisplayName("猫")]
            dog,
            [DisplayName("犬")]
            cat,

        }

        [TestMethod]
        [DisplayName("てすと")]
        public void TestMethod()
        {
            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;
            foreach(var x in this.GetType().GetMembers(flags))
            {

            }
        }
    }
}
