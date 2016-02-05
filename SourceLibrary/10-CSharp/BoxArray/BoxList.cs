/*  [BoxList<T>]
 *
 *  Copyright (C) 2016 JFactory(manbou404)
 *
 *  This software is released under the MIT License.
 *  http://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFactory.CsSrcLib.Library
{
    /// <summary>２次元配列</summary>
    public class BoxList<T> : IBoxArray<T>
    {
        /* List(List(T)) --> Column
         *     +---+
         *  |  |   |   +-------+-------+-------+        
         *  |  |   |-->| (0,0) | (1,0) | (2,0) | List(T)
         *  V  +---+   +-------+-------+-------+
         * Row |   |   +-------+-------+-------+        
         *     |   |-->| (0,1) | (1,1) | (2,1) | List(T)
         *     +---+   +-------+-------+-------+        
         *     |   |   +-------+-------+-------+        
         *     |   |-->| (0,2) | (1,2) | (2,2) | List(T)
         *     +---+   +-------+-------+-------+
         *     |   |   +-------+-------+-------+
         *     |   |-->| (0,3) | (1,3) | (2,3) | List(T)
         *     +---+   +-------+-------+-------+     
         */

        private static readonly int MaxWidth = 100;
        private static readonly int MaxHeight = 100;

        /// <summary>バッキングストア</summary>
        private readonly List<List<T>> box = new List<List<T>>();

        /// <summary>２次元コレクションを初期化する</summary>
        public BoxList()
        {
            //
        }

        /// <summary>２次元コレクションを指定サイズで初期化する</summary>
        public BoxList(int width, int height) : this()
        {
            this.Width = width;
            this.Height = height;
        }

        /// <summary>インデクサ</summary>
        public T this[int col, int row]
        {
            get { return this.box[row][col]; }
            set { this.box[row][col] = value; }
        }

        /// <summary>列の反復処理</summary>
        public IEnumerable<T> Cols(int row)
        {
            Check.ArgumentOutOfRange(row.IsRange(0, this.Height - 1), nameof(row));

            return box[row].AsEnumerable<T>();
        }
        
        /// <summary>行の反復処理</summary>
        public IEnumerable<T> Rows(int col)
        {
            Check.ArgumentOutOfRange(col.IsRange(0, this.Width - 1), nameof(col));

            foreach (var x in box)
            {
                yield return x[col];
            }
        }



        /// <summary>コレクションの行数(縦方向の要素数)を固定する(Add, Remove, Insert</summary>
        public bool FixedHeight { get; set; }

        /// <summary>コレクションの列数(横方向の要素数)を固定する(Add, Remove, Insert</summary>
        public bool FixedWidth { get; set; }

        /// <summary>幅(列)の設定・取得</summary>
        public int Width
        {
            get { return box.Count == 0 ? 0 : box[0].Count; }
            set
            {
                Check.ArgumentOutOfRange(value.IsRange(0, BoxList<T>.MaxWidth), nameof(Width));

                // ResizeImpl(設定値・値の取得・増やす処理・減らす処理
                this.ResizeImpl(value, () => this.Width,
                    x => Loop(x).ForEach(_ => this.AppendCol()),
                    x => box.ForEach(v => v.RemoveRange(this.Width - x, x))
                );
            }
        }

        /// <summary>高さ(行)の設定・取得</summary>
        public int Height
        {
            get { return box.Count; }
            set
            {
                Check.ArgumentOutOfRange(value.IsRange(0, BoxList<T>.MaxHeight), nameof(Height));

                // ResizeImpl(設定値・値の取得・増やす処理・減らす処理
                this.ResizeImpl(value, () => this.Height,
                    x => Loop(x).ForEach(_ => AppendRow()),
                    x => box.RemoveRange(this.Height - x, x)
                );
            }
        }

        /// <summary>クリア(Width=0 && Height=0)</summary>
        public void Clear() => this.box.Clear();

        /// <summary>中身が空(width==0 && height==0)</summary>
        public bool IsEmpty() => this.Height == 0;

        /// <summary>列の挿入</summary>
        public void InsertCols(int col, Func<int, int, T> getNew)
        {
            // InserCols(1, getNew(col,row))
            //             V                   +-- default(T) or getNew(col,row)
            //      (0,0)(1,1)(2,1)     (0,0)(New)(1,1)(2,1)
            //      (0,1)(1,2)(2,2)     (0,1)(New)(1,2)(2,2)
            //      (0,2)(1,3)(2,3)     (0,2)(New)(1,3)(2,3)

            Check.ArgumentOutOfRange(col.IsRange(0, this.Width - 1), nameof(col));
            getNew = getNew ?? new Func<int, int, T>((c, r) => default(T));

            // 列の挿入
            box.Indexed().ForEach(v => v.Element.Insert(col, getNew(col, v.Index)));

            // 列数固定なら、最後の列を削除
            this.FixedWidth.True(RemoveLastCol);
        }

        /// <summary>行の挿入</summary>
        public void InsertRows(int row, Func<int,int,T> getNew)
        {
            // InserRow(1, getNew(col,row))
            //      (0,0)(1,1)(2,1)     (0,0)(1,1)(2,1)
            // 1 => (0,1)(1,2)(2,2)     (New)(New)(New) --- default(T) or getNew(col,row)
            //      (0,2)(1,3)(2,3)     (0,1)(1,2)(2,2)
            //                          (0,2)(1,3)(2,3)

            Check.ArgumentOutOfRange(row.IsRange(0, this.Height - 1), nameof(row));
            getNew = getNew ?? new Func<int, int, T>((c, r) => default(T));

            box.Insert(row, Loop(this.Width).Select(c => getNew(c, row)).ToList());

            // 行数固定なら、最後の行を削除
            this.FixedHeight.True(() => this.RemoveLastRow());
        }

        /// <summary>列の削除</summary>
        public void RemoveCols(int col, Func<int, int, T> getNew)
        {
            Check.ArgumentOutOfRange(col.IsRange(0, this.Width - 1), nameof(col));

            if (this.Width == 1)
            {
                this.Clear();
                return;
            }

            // 列の削除
            box.Indexed().ForEach(v => v.Element.RemoveAt(col));

            // 列数固定なら、末尾に列を追加
            this.FixedWidth.True(() => this.AppendCol(getNew));
        }

        /// <summary>行の削除</summary>
        public void RemoveRows(int row, Func<int, int, T> getNew)
        {
            Check.ArgumentOutOfRange(row.IsRange(0, this.Height - 1), nameof(row));

            if (this.Height == 1)
            {
                this.Clear();
                return;
            }

            // 行の削除
            box.RemoveAt(row);

            // 行数固定なら、末尾に行を追加
            this.FixedHeight.True(() => this.AppendRow(getNew));
        }

        
        
        /// <summary>列の末尾追加</summary>
        private void AppendCol(Func<int, int, T> getNew = null)
        {
            getNew = getNew ?? new Func<int, int, T>((c, r) => default(T));

            var lastIndex = this.Width;
            box.Indexed().ForEach(v => v.Element.Add(getNew(lastIndex, v.Index)));
        }

        /// <summary>行の末尾追加</summary>
        private void AppendRow(Func<int, int, T> getNew = null)
        {
            getNew = getNew ?? new Func<int, int, T>((c, r) => default(T));

            box.Add(Loop(this.Width).Select(c => getNew(c, this.Height)).ToList());
        }

        /// <summary>列の末尾削除</summary>
        private void RemoveLastCol()
        {
            var lastIndex = this.Width - 1;
            this.FixedWidth.True(() => box.ForEach(x => x.RemoveAt(lastIndex)));
        }

        /// <summary>行のの末尾削除</summary>
        private void RemoveLastRow()
        {
            box.RemoveAt(this.Height - 1);
        }

        /// <summary>リサイズの実装 (設定値・値の取得・増やす処理・減らす処理</summary>
        private void ResizeImpl(int newValue, Func<int> size, Action<int> inc, Action<int> dec)
        {
            if (newValue == 0)
            {
                this.box.Clear();       // width,height共に 0 になる
            }
            else
            {
                if (this.IsEmpty())     // width,height共に 1 になる
                {
                    box.Add(new List<T>());
                    box[0].Add(default(T));
                }

                var diff = newValue - size();
                if (diff == 0)
                {
                    return;             // サイズ変更なし(イベント発生せず)
                }

                if (0 < diff)
                {
                    inc(diff);
                }
                else
                {
                    dec(-diff);
                }
            }

            // イベント発生
            // this.Resize?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>内部：Enumerable.Rangeのエイリアス</summary>
        private IEnumerable<int> Loop(int count) => Enumerable.Range(0, count);
    }
}
