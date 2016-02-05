using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PaizaBench
{
    [TestClass]
    public class QuestionTests
    {
        [TestMethod]
        [TestCase("1,2,3", "6")]
        public void TestMethodSingle()
        {
            TestContext.Run((string input, string output) =>
            {
                var obj = new Question();
                obj.Func(input).Is(output);
            });
        }

        [TestMethod]
        [TestCase(new[] { "1", "2", "3" }, "1,2,3")]
        public void TestMethodMulti()
        {
            TestContext.Run((string []inputs, string output) =>
            {
                var obj = new Question();
                obj.Func(inputs).Is(output);
            });
        }

        public TestContext TestContext { get; set; }
    }
}
