/*  [BasicLibrary]
 *
 *  Copyright (C) 2015 JFactory(manbou404)
 *
 *  This software is released under the MIT License.
 *  http://opensource.org/licenses/mit-license.php
 *
 *  [BasicExtensionMethods]
 *      string.IsEmpty, 
 *      As<T>, Bool.True, Bool.False, Clamp<T>, IsRange<T>, ForEach<T>, Indexed<T>
 *  [Indexer<T, Indexer2<T>>]
 *  [ExceptionHelper]
 *      Check<Exception>.Do(cond, message)
 *      Check.InvalidOperation(cond, ...
 *      Check.FileNotFound(path, ...
 *      Check.Argument(
 *      Check.ArgumentNull(
 *      Check.ArgumentOutOfRange(
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JFactory.CsSrcLib.Library;

namespace JFactory.CsSrcLib.Library
{
    /// <summary>よく使う拡張メソッド集</summary>
    [DebuggerStepThrough]
    public static class BasicExtensionMethods
    {
        /// <summary>string.IsNullOrWhiteSpaceのラップ</summary>
        /// <param name="src">string</param>
        /// <returns>IsNullOrWhiteSpaceの結果</returns>
        public static bool IsEmpty(this string src)
        {
            return string.IsNullOrWhiteSpace(src);
        }

        /// <summary>キャスト：成功したらActionを実行する</summary>
        public static T As<T>(this object src, Action<T> action) where T : class
        {
            var obj = src as T;
            if (obj != null)
            {
                action(obj);
            }

            return obj;
        }

        /// <summary>TrueならActionを実行する</summary>
        public static void True(this bool me, Action action)
        {
            if (me == true)
            {
                action();
            }
        }

        /// <summary>FalseならActionを実行する</summary>
        public static void False(this bool me, Action action)
        {
            if (me == false)
            {
                action();
            }
        }

        /// <summary>クリッピング：min≦val≦maxに収める</summary>
        /// <returns>min＜valならmin, max＜valならmax</returns>
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        /// <summary>範囲判定：min≦val≦max</summary>
        /// <returns>true:範囲内 / false:範囲外</returns>
        public static bool IsRange<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return false;
            else if (val.CompareTo(max) > 0) return false;
            else return true;
        }

        /// <summary>Linq:指定Actionを繰り返す</summary>
        /// <param name="action">繰り返したいAction</param>
        public static void ForEach<T>(this IEnumerable<T> src, Action<T> action)
        {
            foreach (var item in src)
            {
                action(item);
            }
        }

        /// <summary>Linq:要素とインデックスを格納するクラス()</summary>
        /// <typeparam name="T">Indexedで使用</typeparam>
        public class IndexedItem<T>
        {
            public T Element { get; private set; }
            public int Index { get; private set; }
            public IndexedItem(T element, int index)
            {
                this.Element = element;
                this.Index = index;
            }
            public override string ToString() => $"[{Index}] {Element}";
        }

        /// <summary>Linq:要素とインデックスを格納するクラスにパックする</summary>
        public static IEnumerable<IndexedItem<T>> Indexed<T>(this IEnumerable<T> source)
        {
            return source.Select((x, i) => new IndexedItem<T>(x, i));
        }
    }

    /// <summary>インデクサのラップ(自作クラス内でインデクサを使ったプロパティ公開等で使用する)</summary>
    [DebuggerStepThrough]
    public class Indexer<T> : IEnumerable<T>
    {
        private readonly Func<int> count;
        private readonly Func<int, T> getter;
        private readonly Action<int, T> setter;

        /// <summary>インデクサを初期化する</summary>
        /// <param name="getter">The getter.</param>
        /// <param name="setter">The setter.</param>
        /// <param name="count">The count.</param>
        public Indexer(Func<int> count, Func<int, T> getter, Action<int, T> setter = null)
        {
            this.count = count;
            this.getter = getter;
            this.setter = setter;
        }

        /// <summary>インデクサの提供</summary>
        public T this[int index1]
        {
            get { return (this.getter != null) ? this.getter(index1) : default(T); }
            set
            {
                if (this.setter == null)
                {
                    
                    throw new NotSupportedException();      // readonly
                }

                this.setter(index1, value);
            }
        }

        /// <summary>要素の数を返す</summary>
        public int Count => this.count?.Invoke() ?? 0;

        /// <summary>読み込み専用かを返す</summary>
        public bool IsReadOnly => setter == null;

        /// <summary>列挙子を返す</summary>
        public IEnumerator<T> GetEnumerator()
        {
            if (this.count == null)
            {
                throw new NotSupportedException();
            }

            for (int i = 0; i < this.Count; i++)
            {
                yield return getter(i);
            }
        }

        /// <summary>列挙子を返す</summary>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    /// <summary>２次元インデクサのラップ(自作クラス内でインデクサを使ったプロパティ公開等で使用する)</summary>
    [DebuggerStepThrough]
    public class Indexer2<T>
    {
        private readonly Func<int> width;
        private readonly Func<int> height;
        private readonly Func<int, int, T> getter;
        private readonly Action<int, int, T> setter;

        /// <summary>インデクサを初期化する</summary>
        public Indexer2(Func<int> width, Func<int> height, 
            Func<int, int, T> getter, Action<int, int, T> setter = null)
        {
            this.width = width;
            this.height = height;
            this.getter = getter;
            this.setter = setter;
        }

        /// <summary>インデクサの提供</summary>
        public T this[int index1, int index2]
        {
            get { return this.getter(index1, index2); }
            set
            {
                if (this.setter == null)
                {
                    throw new NotSupportedException();
                }

                this.setter(index1, index2, value);
            }
        }

        /// <summary>要素の数を返す</summary>
        public int Width => this.width?.Invoke() ?? 0;
        public int Height => this.height?.Invoke() ?? 0;
        public int Count => this.Width * this.Height;

        /// <summary>列挙子を返す</summary>
        public IEnumerable<IndexedItem> Indexed()
        {
            for (int r = 0; r < this.Height; r++)
            {
                for (int c = 0; c < this.Width; c++)
                {
                    yield return new IndexedItem(this[c, r], c, r);
                }
            }
        }

        /// <summary>Indexedの戻り値</summary>
        public class IndexedItem
        {
            public T Element { get; private set; }
            public int Col { get; private set; }
            public int Row { get; private set; }
            public IndexedItem(T element, int col, int row)
            {
                this.Element = element;
                this.Col = col;
                this.Row = row;
            }

            public override string ToString() => $"[{Col},{Row}] {Element}";
        }
    }

    /// <summary>例外を便利にするクラス(ExceptionHelper)</summary>
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
