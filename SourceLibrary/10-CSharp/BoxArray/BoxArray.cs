/*  [BoxArray<T>]
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
    public interface IBoxArray<T>
    {
        int Width { get; set; }
        int Height { get; set; }
        bool FixedWidth { get; set; }
        bool FixedHeight { get; set; }

        T this[int col, int row] { get; set; }
        IEnumerable<T> Cols(int row);
        IEnumerable<T> Rows(int col);

        bool IsEmpty();
        void Clear();

        void InsertCols(int col, Func<int, int, T> getNew = null);
        void InsertRows(int row, Func<int, int, T> getNew = null);

        void RemoveCols(int col, Func<int, int, T> getNew = null);
        void RemoveRows(int row, Func<int, int, T> getNew = null);
    }

    /// <summary>２次元配列</summary>
    public class BoxArray<T> : IBoxArray<T>
    {
        /*  box[row, col]
         *  +-------+-------+-------+-------
         *  | [0,0] | [0,1] | [0,2] |  ...
         *  +-------+-------+-------+-------
         *  | [1,0] | [1,1] | [1,2] |  ...
         *  +-------+-------+-------+-------
         *  | [2,0] | [2,1] | [2,2] |  ...
         *  +-------+-------+-------+-------
         *  |       |       |       |       
        */


        private static readonly int MaxWidth = 100;
        private static readonly int MaxHeight = 100;

        /// <summary>保持データ</summary>
        public T[,] box = new T[0,0];

        /// <summary>２次元配列を初期化する</summary>
        public BoxArray()
        {
            //
        }

        /// <summary>２次元配列を指定サイズで初期化する</summary>
        public BoxArray(int width, int height) : this()
        {
            this.ResizeImpl(width, height);
        }



        /// <summary>インデクサの実装</summary>
        public T this [int col, int row]
        {
            get { return this.box[row, col]; }
            set { this.box[row, col] = value; }
        }

        /// <summary>列の反復処理</summary>
        public IEnumerable<T> Cols(int row)
        {
            Check.ArgumentOutOfRange(row.IsRange(0, this.Height - 1), nameof(row));

            for (var col = 0; col < this.Width; col++)
            {
                yield return box[row, col];
            }
        }

        /// <summary>行の反復処理</summary>
        public IEnumerable<T> Rows(int col)
        {
            Check.ArgumentOutOfRange(col.IsRange(0, this.Width - 1), nameof(col));

            for (var row = 0; row < this.Height; row ++)
            {
                yield return box[row, col];
            }
        }



        /// <summary>コレクションの行数(縦方向の要素数)を固定する(Add, Remove, Insert</summary>
        public bool FixedHeight { get; set; } = false;

        /// <summary>コレクションの列数(横方向の要素数)を固定する(Add, Remove, Insert</summary>
        public bool FixedWidth { get; set; } = false;

        /// <summary>幅の取得と設定(Resizeメソッド推奨)</summary>
        public int Width
        {
            get { return box.GetLength(1); }
            set { this.ResizeImpl(value, this.Height); }
        }

        /// <summary>高さの取得と設定(Resizeメソッド推奨)</summary>
        public int Height
        {
            get { return box.GetLength(0); }
            set { this.ResizeImpl(this.Width, value); }
        }

        /// <summary>クリア(Width=0 && Height=0)</summary>
        public void Clear() => this.ResizeImpl(0, 0);
        
        /// <summary>中身が空(width==0 && height==0)</summary>
        public bool IsEmpty() => this.Height == 0;

        /// <summary>列の挿入</summary>
        public void InsertCols(int col, Func<int, int, T> getNew)
        {
            Check.ArgumentOutOfRange(col.IsRange(0, this.Width - 1), nameof(col));
            getNew = getNew ?? new Func<int, int, T>((c, r) => default(T));

            // 幅が可変の時は、あたらしい配列を用意して、挿入位置までコピーしておく。
            var src = this.box;
            if (this.FixedWidth == false)
            {
                this.box = this.Create(this.Width + 1, this.Height);
                this.Copy(src, 0, 0, col, this.GetHeight(src), this.box, 0, 0);
            }

            // 挿入位置から後ろをコピーする。
            Copy(src, col, 0, this.GetWidth(src) - col, this.GetHeight(src), this.box, col+1, 0);

            // 挿入した列に、新しいオブジェクトをセットする
            this.Loop(this.Height).ForEach(r => this.box[r, col] = getNew(col, r));

        }

        /// <summary>行の挿入</summary>
        public void InsertRows(int row, Func<int, int, T> getNew)
        {
            Check.ArgumentOutOfRange(row.IsRange(0, this.Height - 1), nameof(row));
            getNew = getNew ?? new Func<int, int, T>((c, r) => default(T));

            // 高さが可変の時は、あたらしい配列を用意して、挿入位置までコピーしておく。
            var src = this.box;
            if (this.FixedHeight == false)
            {
                this.box = this.Create(this.Width, this.Height + 1);
                this.Copy(src, 0, 0, this.GetWidth(src), row,  this.box, 0, 0);
            }

            // 挿入位置から後ろをコピーする。
            Copy(src, 0, row, this.GetWidth(src), this.GetHeight(src) - row, this.box, 0, row + 1);

            // 挿入した行に、新しいオブジェクトをセットする
            this.Loop(this.Width).ForEach(c => box[row, c] = getNew(c, row));
        }

        /// <summary>列の削除</summary>
        public void RemoveCols(int col, Func<int, int, T> getNew)
        {
            Check.ArgumentOutOfRange(col.IsRange(0, this.Width - 1), nameof(col));
            getNew = getNew ?? new Func<int, int, T>((c, r) => default(T));

            if (this.Width == 1)
            {
                this.Clear();
                return;
            }

            // 幅が可変の時は、あたらしい配列を用意して、削除位置までコピーしておく。
            var src = this.box;
            if (this.FixedWidth == false)
            {
                this.box = this.Create(this.Width-1, this.Height);
                this.Copy(src, 0, 0, col, this.GetHeight(src), this.box, 0, 0);
            }

            // コピー処理 colから右を、ひとつ左上にコピーする。
            this.Copy(src, col + 1, 0, this.GetWidth(src) - col, this.GetHeight(src), this.box, col, 0);

            // 高さが可変の時は、最終行に新しいオブジェクトをセットする
            var newCol = this.Width - 1;
            this.FixedWidth.True(() => this.Loop(this.Height).ForEach(r => box[r, newCol] = getNew(newCol, r)));
        }

        /// <summary>行の削除</summary>
        public void RemoveRows(int row, Func<int, int, T> getNew)
        {
            Check.ArgumentOutOfRange(row.IsRange(0, this.Height - 1), nameof(row));
            getNew = getNew ?? new Func<int, int, T>((c, r) => default(T));

            if (this.Height == 1)
            {
                this.Clear();
                return;
            }

            // 高さが可変の時は、あたらしい配列を用意して、削除位置までコピーしておく。
            var src = this.box;
            if (this.FixedHeight == false)
            {
                this.box = this.Create(this.Width, this.Height - 1);
                this.Copy(src, 0, 0, this.GetWidth(src), row, this.box, 0, 0);
            }

            // コピー処理 rowから下を、ひとつ上にコピーする。
            this.Copy(src, 0, row + 1, this.GetWidth(src), this.GetHeight(src) - row, this.box, 0, row);

            // 高さが可変の時は、最終行に新しいオブジェクトをセットする
            var newRow = this.Height - 1;
            this.FixedHeight.True(() => this.Loop(this.Width).ForEach(c => box[newRow, c] = getNew(c, newRow)));
        }



        /// <summary>２次元配列ユーティリティ：コピー</summary>
        private void Copy(T[,] src, int left, int top, int wid, int hei, T[,] dst, int dstLeft, int dstTop)
        {
            // はみ出さないように、適宜調整する
            (this.GetWidth(src) < left + wid).True(() => wid = this.GetWidth(src) - left);
            (this.GetWidth(dst) < dstLeft + wid).True(() => wid = this.GetWidth(dst) - dstLeft);
            (this.GetHeight(src) < top + hei).True(() => hei = this.GetHeight(src) - top);
            (this.GetHeight(dst) < dstTop + hei).True(() => hei = this.GetHeight(dst) - dstTop);

            for (var r = 0; r < hei; r ++)
            {
                // 上書きしないように、コピーする向き(上下)を変える
                var rr = (top < dstTop) ? hei - r-1 : r; 
                for (var c = 0; c < wid; c ++)
                {
                    // 上書きしないように、コピーする向き(左右)を変える
                    var cc = (left < dstLeft) ? wid - c - 1: c;
                    dst[dstTop + rr, dstLeft + cc] = src[top + rr, left + cc];
                }
            }
        }

        private void Copy(T[,] src, T[,] dst) =>
            this.Copy(src, 0, 0, this.GetWidth(src), this.GetHeight(src), dst, 0, 0);

        /// <summary>２次元配列ユーティリティ：生成</summary>
        private T[,] Create(int width, int height)
        {
            Check.ArgumentOutOfRange(width.IsRange(0, MaxWidth), nameof(width));
            Check.ArgumentOutOfRange(height.IsRange(0, MaxHeight), nameof(height));

            if (width == 0 && height != 0) width = 1;
            else if (width != 0 && height == 0) height = 1;

            return new T[height, width];
        }

        [DebuggerStepThrough]
        private int GetWidth(T[,] src) => src.GetLength(1);

        [DebuggerStepThrough]
        private int GetHeight(T[,] src) => src.GetLength(0);

        /// <summary>保持データのリサイズ</summary>
        public void ResizeImpl(int newCol, int newRow)
        {
            var newBox = Create(newCol, newRow);
            this.Copy(this.box, newBox);

            this.box = newBox;
        }



        /// <summary>内部：Enumerable.Rangeのエイリアス(start=0)</summary>
        private IEnumerable<int> Loop(int count) => Enumerable.Range(0, count);

        /// <summary>内部：Enumerable.Rangeのエイリアス</summary>
        private IEnumerable<int> Loop(int start, int count) => Enumerable.Range(start, count);
    }
}
