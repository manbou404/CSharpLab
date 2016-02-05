/*  [ExceptionHelper]
 *
 *  Copyright (C) 2015 JFactory(manbou404)
 *
 *  This software is released under the MIT License.
 *  http://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFactory.CsSrcLib.ExceptionHelper
{
    /// <summary>例外を便利にするクラス</summary>
    [DebuggerStepThrough]
    public static class Check
    {

        #region 汎用の例外ヘルパー

        /// <summary>汎用の例外ヘルパー</summary>
        /// <typeparam name="T">Exceptionを継承した例外クラスの型</typeparam>
        /// <param name="condition">チェック結果(Falseなら例外を送出する)</param>
        /// <param name="format">例外の原因を説明するエラーメッセージの拡張書式指定文字列</param>
        /// <param name="obj">例外の原因を説明するエラーメッセージの書式設定対象オブジェクト</param>
        public static void Do<T>(bool condition, string format, object obj) where T : Exception
        {
            var message = string.Format(format, obj);
            Do<T>(condition, message);
        }

        /// <summary>汎用の例外ヘルパー</summary>
        /// <typeparam name="T">Exceptionを継承した例外クラスの型</typeparam>
        /// <param name="condition">チェック結果(Falseなら例外を送出する)</param>
        /// <param name="format">例外の原因を説明するエラーメッセージの拡張書式指定文字列</param>
        /// <param name="objs">例外の原因を説明するエラーメッセージの書式設定対象オブジェクト</param>
        public static void Do<T>(bool condition, string format, params object[] objs) where T : Exception
        {
            var message = string.Format(format, objs);
            Do<T>(condition, message);
        }

        /// <summary>汎用の例外ヘルパー</summary>
        /// <typeparam name="T">Exceptionを継承した例外クラスの型</typeparam>
        /// <param name="condition">チェック結果(Falseなら例外を送出する)</param>
        /// <param name="message">例外の原因を説明するエラーメッセージ</param>
        public static void Do<T>(bool condition, string message) where T : Exception
        {
            if (!condition)
            {
                // TはException型制約なので、必ずコンストラクタは見つかる
                var ctor = typeof(T).GetConstructor(new Type[] { typeof(string) });
                throw (T)ctor.Invoke(new object[] { message });
            }
        }

        #endregion

        #region InvalidOperationExceptionヘルパー

        /// <summary>InvalidOperationExceptionヘルパー</summary>
        /// <param name="condition">チェック結果(Falseなら例外を送出する)</param>
        /// <param name="format">例外の原因を説明するエラーメッセージの拡張書式指定文字列</param>
        /// <param name="obj">例外の原因を説明するエラーメッセージの書式設定対象オブジェクト</param>
        public static void InvalidOperation(bool condition, string format, object obj)
        {
            InvalidOperation(condition, string.Format(format, obj));
        }

        /// <summary>InvalidOperationExceptionヘルパー</summary>
        /// <param name="condition">チェック結果(Falseなら例外を送出する)</param>
        /// <param name="format">例外の原因を説明するエラーメッセージの拡張書式指定文字列</param>
        /// <param name="objs">例外の原因を説明するエラーメッセージの書式設定対象オブジェクト</param>
        public static void InvalidOperation(bool condition, string format, params object[] objs)
        {
            InvalidOperation(condition, string.Format(format, objs));
        }

        /// <summary>InvalidOperationExceptionヘルパー</summary>
        /// <param name="condition">チェック結果(Falseなら例外を送出する)</param>
        /// <param name="message">例外の原因を説明するエラーメッセージ</param>
        public static void InvalidOperation(bool condition, string message)
        {
            if (!condition)
            {
                throw new InvalidOperationException(message);
            }
        }

        #endregion

        /// <summary>ファイルがないときFileNotFoundExceptionを送出する</summary>
        /// <param name="filePath">チェックするファイルパス文字列</param>
        public static void FileNotFound(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("", filePath);
            }
        }

        #region ArgumentExceptionヘルパー

        /// <summary>ArgumentExceptionヘルパー</summary>
        /// <param name="condition">チェック結果(Falseなら例外を送出する)</param>
        /// <param name="paramName">チェック対象オブジェクトの名前文字列</param>
        /// <param name="format">例外の原因を説明するエラーメッセージの拡張書式指定文字列</param>
        /// <param name="obj">例外の原因を説明するエラーメッセージの書式設定対象オブジェクト</param>
        public static void Argument(bool condition, string paramName, string format, object obj)
        {
            Argument(condition, paramName, string.Format(format, obj));
        }

        /// <summary>ArgumentExceptionヘルパー</summary>
        /// <param name="condition">チェック結果(Falseなら例外を送出する)</param>
        /// <param name="paramName">チェック対象オブジェクトの名前文字列</param>
        /// <param name="format">例外の原因を説明するエラーメッセージの拡張書式指定文字列</param>
        /// <param name="objs">例外の原因を説明するエラーメッセージの書式設定対象オブジェクト</param>
        public static void Argument(bool condition, string paramName, string format, params object[] objs)
        {
            Argument(condition, paramName, string.Format(format, objs));
        }

        /// <summary>ArgumentExceptionヘルパー</summary>
        /// <param name="condition">チェック結果(Falseなら例外を送出する)</param>
        /// <param name="paramName">チェック対象オブジェクトの名前文字列</param>
        public static void Argument(bool condition, string paramName)
        {
            Argument(condition, paramName, "");
        }

        /// <summary>ArgumentExceptionヘルパー</summary>
        /// <param name="condition">チェック結果(Falseなら例外を送出する)</param>
        /// <param name="paramName">チェック対象オブジェクトの名前文字列</param>
        /// <param name="message">例外の原因を説明するエラーメッセージ</param>
        public static void Argument(bool condition, string paramName, string message)
        {
            if (!condition)
            {
                throw new ArgumentException(message, paramName);
            }
        }

        #endregion

        #region ArgumentNullExceptionヘルパー

        /// <summary>ArgumentNullExceptionヘルパー</summary>
        /// <param name="value">チェック対象オブジェクト</param>
        /// <param name="paramName">チェック対象オブジェクトの名前文字列</param>
        public static void ArgumentNull(object value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        #endregion

        #region ArgumentOutOfRangeExceptionヘルパー

        /// <summary>ArgumentOutOfRangeExceptionヘルパー</summary>
        /// <param name="condition">チェック結果(Falseなら例外を送出する)</param>
        /// <param name="paramName">チェック対象オブジェクトの名前文字列</param>
        /// <param name="format">例外の原因を説明するエラーメッセージの拡張書式指定文字列</param>
        /// <param name="obj">例外の原因を説明するエラーメッセージの書式設定対象オブジェクト</param>
        public static void ArgumentOutOfRange(bool condition, string paramName, string format, object obj)
        {
            ArgumentOutOfRange(condition, paramName, string.Format(format, obj));
        }

        /// <summary>ArgumentOutOfRangeExceptionヘルパー</summary>
        /// <param name="condition">チェック結果(Falseなら例外を送出する)</param>
        /// <param name="paramName">チェック対象オブジェクトの名前文字列</param>
        /// <param name="format">例外の原因を説明するエラーメッセージの拡張書式指定文字列</param>
        /// <param name="objs">例外の原因を説明するエラーメッセージの書式設定対象オブジェクト</param>
        public static void ArgumentOutOfRange(bool condition, string paramName, string format, params object[] objs)
        {
            ArgumentOutOfRange(condition, paramName, string.Format(format, objs));
        }

        /// <summary>ArgumentOutOfRangeExceptionヘルパー</summary>
        /// <param name="condition">チェック結果(Falseなら例外を送出する)</param>
        /// <param name="paramName">チェック対象オブジェクトの名前文字列</param>
        public static void ArgumentOutOfRange(bool condition, string paramName)
        {
            if (!condition)
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        /// <summary>ArgumentOutOfRangeExceptionヘルパー</summary>
        /// <param name="condition">チェック結果(Falseなら例外を送出する)</param>
        /// <param name="paramName">チェック対象オブジェクトの名前文字列</param>
        /// <param name="message">例外の原因を説明するエラーメッセージ</param>
        public static void ArgumentOutOfRange(bool condition, string paramName, string message)
        {
            if (!condition)
            {
                throw new ArgumentOutOfRangeException(paramName, message);
            }
        }

        #endregion
    }
}
