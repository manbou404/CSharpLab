namespace JFactory.CsSrcLib.ExceptionHelper
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;


    [TestClass]
    public class ExceptionHelperTest
    {
        //public static T AssertThrows<T>(Action task) where T : Exception
        //{
        //    try
        //    {
        //        task.Invoke();
        //    }
        //    catch(T ex)
        //    {
        //        return ex;
        //    }

        //    return null;
        //}

        [TestMethod]
        public void DoTest()
        {
            // 普通に呼んでも例外が発生しないことを確認
            Check.Do<NotImplementedException>(true, "@1");
            Check.Do<NotImplementedException>(true, "{0}", "@1");
            Check.Do<NotImplementedException>(true, "{0},{1}", "@1", "@2");

            // Exceptionから継承したクラスでなければ、コンパイルできない
            // Check.Do<ExceptionHelperTest>(true, "@1");

            // メッセージのみ
            //var ex = Assert.Throws<NotImplementedException>(
            //    () => Check.Do<NotImplementedException>(false, "@1"));
            var ex = AssertEx.Throws<NotImplementedException>(
                () => Check.Do<NotImplementedException>(false, "@1"));
            Assert.AreEqual(ex.Message, "@1");

            // フォーマット＋引数１個
            ex = AssertEx.Throws<NotImplementedException>(
                () => Check.Do<NotImplementedException>(false, "{0}", "@1"));
            Assert.AreEqual(ex.Message, "@1");

            // フォーマット＋引数２個以上
            ex = AssertEx.Throws<NotImplementedException>(
                () => Check.Do<NotImplementedException>(false, "{0},{1}", "@1", "@2"));
            Assert.AreEqual(ex.Message, "@1,@2");
        }

        [TestMethod]
        public void InvalidOperationExceptionTest()
        {
            // 普通に呼んでも例外が発生しないことを確認
            Check.InvalidOperation(true, "@1");
            Check.InvalidOperation(true, "{0}", "@1");
            Check.InvalidOperation(true, "{0},{1}", "@1", "@2");

            // メッセージのみ
            var ex = AssertEx.Throws<InvalidOperationException>(
                () => Check.InvalidOperation(false, "@1"));
            Assert.AreEqual(ex.Message, "@1");

            // フォーマット＋引数１個
            ex = AssertEx.Throws<InvalidOperationException>(
                () => Check.InvalidOperation(false, "{0}", "@1"));
            Assert.AreEqual(ex.Message, "@1");

            // フォーマット＋引数２個以上
            ex = AssertEx.Throws<InvalidOperationException>(
                () => Check.InvalidOperation(false, "{0},{1}", "@1", "@2"));
            Assert.AreEqual(ex.Message, "@1,@2");
        }

        [TestMethod]
        public void ArgumentExceptionTest()
        {
            var paramName = "dummy";
            var message = "\r\nパラメーター名:";

            // 普通に呼んでも例外が発生しないことを確認
            Check.Argument(true, paramName);
            Check.Argument(true, paramName, "@1");
            Check.Argument(true, paramName, "{0}", "@1");
            Check.Argument(true, paramName, "{0},{1}", "@1", "@2");

            // パラメータ名のみ
            var ex = AssertEx.Throws<ArgumentException>(
                () => Check.Argument(false, paramName));
            Assert.AreEqual(ex.ParamName, paramName);
            Assert.AreEqual(ex.Message, message + paramName);

            // パラメータ名＋メッセージ
            ex = AssertEx.Throws<ArgumentException>(
                () => Check.Argument(false, paramName, "@1"));
            Assert.AreEqual(ex.ParamName, paramName);
            Assert.AreEqual(ex.Message, "@1" + message + paramName);

            // パラメータ名＋フォーマット＋引数１個
            ex = AssertEx.Throws<ArgumentException>(
                () => Check.Argument(false, paramName, "{0}", "@1"));
            Assert.AreEqual(ex.ParamName, paramName);
            Assert.AreEqual(ex.Message, "@1" + message + paramName);

            // パラメータ名＋フォーマット＋引数２個以上
            ex = AssertEx.Throws<ArgumentException>(
                () => Check.Argument(false, paramName, "{0},{1}", "@1", "@2"));
            Assert.AreEqual(ex.ParamName, paramName);
            Assert.AreEqual(ex.Message, "@1,@2" + message + paramName);
        }

        [TestMethod]
        public void ArgumentNullTest()
        {
            var paramName = "dummy";

            // 普通に呼んでも例外が発生しないことを確認
            Check.ArgumentNull(paramName, paramName);

            // パラメータ名のみ
            var ex = AssertEx.Throws<ArgumentNullException>(
                () => Check.ArgumentNull(null, paramName));
            Assert.AreEqual(ex.ParamName, paramName);
        }
    
        [TestMethod]
        public void ArgumentOutOfRangeTest()
        {
            var paramName = "dummy";
            var message1 = "指定された引数は、有効な値の範囲内にありません。\r\nパラメーター名:";
            var message2 = "\r\nパラメーター名:";

            // 普通に呼んでも例外が発生しないことを確認
            Check.ArgumentOutOfRange(true, paramName);
            Check.ArgumentOutOfRange(true, paramName, "@1");
            Check.ArgumentOutOfRange(true, paramName, "{0}", "@1");
            Check.ArgumentOutOfRange(true, paramName, "{0},{1}", "@1", "@2");

            // パラメータ名のみ
            var ex = AssertEx.Throws<ArgumentOutOfRangeException>(
                () => Check.ArgumentOutOfRange(false, paramName));
            Assert.AreEqual(ex.ParamName, paramName);
            Assert.AreEqual(ex.Message, message1 + paramName);

            // パラメータ名＋メッセージ
            ex = AssertEx.Throws<ArgumentOutOfRangeException>(
                () => Check.ArgumentOutOfRange(false, paramName, "@1"));
            Assert.AreEqual(ex.ParamName, paramName);
            Assert.AreEqual(ex.Message, "@1" + message2 + paramName);

            // パラメータ名＋フォーマット＋引数１個
            ex = AssertEx.Throws<ArgumentOutOfRangeException>(
                () => Check.ArgumentOutOfRange(false, paramName, "{0}", "@1"));
            Assert.AreEqual(ex.ParamName, paramName);
            Assert.AreEqual(ex.Message, "@1" + message2 + paramName);

            // パラメータ名＋フォーマット＋引数２個以上
            ex = AssertEx.Throws<ArgumentOutOfRangeException>(
                () => Check.ArgumentOutOfRange(false, paramName, "{0},{1}", "@1", "@2"));
            Assert.AreEqual(ex.ParamName, paramName);
            Assert.AreEqual(ex.Message, "@1,@2" + message2 + paramName);
        }
    }
}
