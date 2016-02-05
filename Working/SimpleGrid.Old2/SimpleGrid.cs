/*  [SimpleGrid]
 *
 *  Copyright (C) 2016 JFactory(manbou404)
 *
 *  This software is released under the MIT License.
 *  http://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JFactory.CsSrcLib.Library;

namespace JFactory.CsSrcLib.Forms.Grid
{
    public enum CellType
    {
        Text,
        CheckBox,
    }

    public class SimpleGrid : Control
    {
        readonly DataGridView grid;

        /// <summary>SimpleGridを初期化する</summary>
        public SimpleGrid()
        {
            //
            // グリッドの生成と設定
            //
            this.grid = new DataGridView();
            this.grid.Dock = DockStyle.Fill;
            this.grid.RowHeadersVisible = false;    // ヘッダは独自に表示するので、DataGridViewのヘッダは使わない
            this.grid.ColumnHeadersVisible = false;
            this.grid.AutoGenerateColumns = false;      // 列を自動で作らない
            //this.grid.AllowUserToAddRows = false;     // 行ヘッダの▲マーク
            this.Controls.Add(grid);
            this.grid.CellPainting += Grid_CellPainting;
            //
            // ヘッダのデフォルトスタイル
            //

            this.Cells = new Indexer2<object>(
                (c, r) => this.DataCell(c, r).Value, (c, r, v) => this.DataCell(c, r).Value = v);

            this.CellType = new Indexer2<CellType>(GetCellType, SetCellType);
            this.CellStyle = new Indexer2<DataGridViewCellStyle>((c, r) => this.DataCell(c, r).Style);

            this.ColWidth = new Indexer1<int>(c => DataCol(c).Width, (c, v) => DataCol(c).Width = v);
            this.RowHeight = new Indexer1<int>(r => DataRow(r).Height, (r, v) => DataRow(r).Height = v);

            this.ColHeaderHeight = new Indexer1<int>(
                r => this.HeadRow(r).Height, (r, v) => this.HeadRow(r).Height = v);

            this.RowHeaderWidth = new Indexer1<int>(
                c => this.HeadCol(c).Width, (c, v) => this.HeadCol(c).Width = v);


            // DataGridViewColumn.Frozen


        }

        public void Combine(int col, int row, int cols, int rows)
        {
            var range = this.Range(col, row, cols, rows);
            Check.Argument(!range.Any(x => x.Tag is Point), "col,row,cols,rows", "");

            range.ForEach(x => x.Tag = new Point(col - x.ColumnIndex, row - x.RowIndex));
            range.First().Tag = new Point(cols, rows);
        }

        private IEnumerable<DataGridViewCell> Range(int col, int row, int cols, int rows)
        {
            return grid.Rows.Cast<DataGridViewRow>().Skip(row).Take(rows)
                        .Select(x => x.Cells.Cast<DataGridViewCell>().Skip(col).Take(cols))
                        .SelectMany(x => x);

        }
        

        // 指定範囲のチェック (1. TagがPoint型 
        public bool bbb(int col, int row, int cols, int rows)
        {
            // grid.Range(col, row, cols, rows).Select(x => x.row);



            for (var r = row; r < rows; r ++)
            {
                for (var c = 0; c < cols; c++)
                {
                    if (grid[c, r].Tag is Point)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private enum CombineStatus
        {
            NotCombine,     // 結合セルではない
            CombineParent,  // 結合セルの親
            CombineChild,   // 結合セルの子
        }

        private CombineStatus IsCombineStatus(int col, int row)
        {
            if (this.grid[col, row].Tag is Point == false)
            {
                return CombineStatus.NotCombine;
            }

            var pt = (Point)this.grid[col, row].Tag;
            return (pt.X < 0 || pt.Y < 0) ? CombineStatus.CombineChild : CombineStatus.CombineParent;
        }



        private void Grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            var col = e.ColumnIndex;
            var row = e.RowIndex;
            var dgv = (DataGridView)sender;

            var combineStatus = IsCombineStatus(col, row);
            
            if (combineStatus == CombineStatus.NotCombine)
            {
                // 結合セルではないので、描画はシステムに任せる。
                return;     
            }
            if (combineStatus == CombineStatus.CombineChild)
            {
                // 結合セルだが、親が描画済みなので、描画完了
                Debug.Print($"skip ({col},{row})");

                var pt2 = (Point)dgv[col,row].Tag;
                //var cel = grid[e.ColumnIndex - pt2.X, e.RowIndex - pt2.Y];

                var repx = e.ColumnIndex + pt2.X;
                var repy = e.RowIndex + pt2.Y;

                e.Graphics.FillRectangle(Brushes.Blue, new Rectangle(10, 20, 150, 70));

                //grid.InvalidateCell(repx, repy);
                //Debug.Print($"rep {repx},{repy}");

                e.Handled = true;
                return;
            }

            Debug.Print($"draw ({col},{row})");

            // ここから結合セルの親
            var pt = (Point)dgv[col,row].Tag;
            var w = grid.Columns.Cast<DataGridViewColumn>().Skip(col).Take(pt.X).Sum(v => v.Width);
            var h = grid.Rows.Cast<DataGridViewRow>().Skip(row).Take(pt.Y).Sum(v => v.Height);

            Debug.Print($"({e.CellBounds.Location}) - ({w},{h}) {e.ClipBounds}");
            e.Graphics.FillRectangle(Brushes.Red, new Rectangle(e.CellBounds.Left, e.CellBounds.Top, w, h));
            

            e.Handled = true;
            return;



//            var dgv = (DataGridView)sender;
//            var cell = dgv[e.ColumnIndex, e.RowIndex];

//            if (cell.Tag is Point)
//            {
//                var p = (Point)cell.Tag;
//                if (0 <= p.X && 0 <= p.Y)
//                {
//                    var left = e.CellBounds.Left;
//                    var top = e.CellBounds.Top;
//                    var cell2 = ((DataGridView)sender)[p.X-1, p.Y-1];
//                    //var right = cell2.ContentBounds.Right;
//                    var width = cell.ContentBounds.Width + cell2.ContentBounds.Width;
////                    var height = cell2.ContentBounds.Height;
//                    var height = this.RowHeight[0];

//                    e.Graphics.FillRectangle(Brushes.Aqua, new Rectangle(left, top, 200, height));
//                    Debug.WriteLine($"({left},{top})-({width},{height})");


//                }
//                e.Handled = true;


//            }

//            return;

            




            //ヘッダー以外のセルで、背景を描画する時
            //if (e.ColumnIndex >= 0 && e.RowIndex >= 0 &&
            //    (e.PaintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
            //{
            //    //選択されているか調べ、色を決定する
            //    //bColor1が開始色、bColor2が終了色
            //    Color bColor1, bColor2;
            //    if ((e.PaintParts & DataGridViewPaintParts.SelectionBackground) == DataGridViewPaintParts.SelectionBackground 
            //        && (e.State & DataGridViewElementStates.Selected) ==  DataGridViewElementStates.Selected)
            //    {
            //        bColor1 = e.CellStyle.SelectionBackColor;
            //        bColor2 = Color.Black;
            //    }
            //    else
            //    {
            //        bColor1 = e.CellStyle.BackColor;
            //        bColor2 = Color.LemonChiffon;
            //    }

            //    //グラデーションブラシを作成
            //    using (var b = new LinearGradientBrush(e.CellBounds, bColor1, bColor2,LinearGradientMode.Horizontal))
            //    {
            //        //セルを塗りつぶす
            //        e.Graphics.FillRectangle(b, e.CellBounds);
            //    }

            //    //背景以外が描画されるようにする
            //    DataGridViewPaintParts paintParts = e.PaintParts & ~DataGridViewPaintParts.Background;
            //    //セルを描画する
            //    e.Paint(e.ClipBounds, paintParts);

            //    //描画が完了したことを知らせる
            //    e.Handled = true;
            //}
        }


        #region アドレス変換 DataCol,DataRow,DataCell, HeadCol,HeadRow,HeadCell

        private DataGridViewColumn DataCol(int col)
        {
            Check.ArgumentOutOfRange(col.IsRange(0, this.MaxCols - 1), nameof(col));

            return this.grid.Columns[col + this.RowHeaderCols];
        }
        private DataGridViewRow DataRow(int row)
        {
            Check.ArgumentOutOfRange(row.IsRange(0, this.MaxRows - 1), nameof(row));

            return this.grid.Rows[row + this.ColHeaderRows];
        }
        private DataGridViewCell DataCell(int col, int row)
        {
            Check.ArgumentOutOfRange(col.IsRange(0, this.MaxCols - 1), nameof(col));
            Check.ArgumentOutOfRange(row.IsRange(0, this.MaxRows - 1), nameof(row));

            return this.grid[this.RowHeaderCols + col, this.ColHeaderRows + row];
        }
        private DataGridViewRow HeadRow(int row)
        {
            Check.ArgumentOutOfRange(row.IsRange(0, this.ColHeaderRows-1), nameof(row));

            return this.grid.Rows[row];
        }
        private DataGridViewColumn HeadCol(int col)
        {
            Check.ArgumentOutOfRange(col.IsRange(0, this.RowHeaderCols-1), nameof(col));

            return this.grid.Columns[col];
        }
        private DataGridViewCell HeadCell(int col, int row)
        {
            Check.ArgumentOutOfRange(col.IsRange(0, this.RowHeaderCols - 1), nameof(col));
            Check.ArgumentOutOfRange(row.IsRange(0, this.ColHeaderRows - 1), nameof(row));

            return this.grid[col, row];
        }

        #endregion

        
        #region ヘッダ関連

        /// <summary>ヘッダのデフォルトスタイル(行列共通)</summary>
        public DataGridViewCellStyle DefaultHeaderCellStyle => this.grid.RowHeadersDefaultCellStyle;

        private int _RowHeaderCols;
        private int _ColHeaderRows;

        /// <summary>列ヘッダ(グリッドの上側)の行数</summary>
        public int ColHeaderRows
        {
            get
            {
                //Debug.Assert(grid.RowCount == _ColHeaderRows + _MaxRows);

                return this._ColHeaderRows;
            }
            set
            {
                if (this.grid.RowCount != (value + this.MaxRows))
                {
                    var diff = value - this._ColHeaderRows;

                    if (0 < diff)   // 行が増えた(増えた行にデフォルトヘッダ属性を追加)
                    {
                        for (int i = 0; i < diff; i ++)
                        {
                            this.grid.Rows.Insert(this._ColHeaderRows);
                            this.grid.Rows[this._ColHeaderRows].Cells.Cast<DataGridViewCell>().ToList().
                                ForEach(x => x.Style = new DataGridViewCellStyle(this.DefaultHeaderCellStyle));
                        }
                    }
                    else            // 行が減った
                    {
                        Enumerable.Repeat(0,-diff).ToList().ForEach(_ => this.grid.Rows.RemoveAt(value));
                    }

                    this._ColHeaderRows = value;

                    //this.grid.RowCount = rows;
                }
            }
        }

        /// <summary>行ヘッダ(グリッドの左側)の列数</summary>
        public int RowHeaderCols
        {
            get
            {
                //Debug.Assert(grid.ColumnCount == _RowHeaderCols + _MaxCols);

                return this._RowHeaderCols;
            }
            set
            {
                if (this.grid.ColumnCount != (value + this.MaxCols))
                {
                    var diff = value - this._RowHeaderCols;
                    
                    if (0 < diff)   // 行が増えた(増えた列にデフォルトヘッダ属性を追加)
                    {
                        Enumerable.Repeat(0, diff).ToList()
                            .ForEach(_ => this.grid.Columns.Insert(
                                this._RowHeaderCols,
                                new DataGridViewTextBoxColumn() { DefaultCellStyle = DefaultHeaderCellStyleFactory() }));
                    }
                    else            // 行が減った
                    {
                        Enumerable.Repeat(0, -diff).ToList()
                            .ForEach(_=> this.grid.Columns.RemoveAt(value));
                    }

                    this._RowHeaderCols = value;
                    
                    // this.grid.ColumnCount = cols;
                }
            }
        }

        #endregion

        public Indexer2<object> Cells { get; private set; }
        public Indexer2<CellType> CellType { get; private set; }
        public Indexer2<DataGridViewCellStyle> CellStyle { get; private set; }
        public Indexer1<int> ColWidth { get; private set; }
        public Indexer1<int> RowHeight { get; private set; }
        public Indexer1<int> ColHeaderHeight { get; private set; }
        public Indexer1<int> RowHeaderWidth { get; private set; }

        #region CellType[,]

        public CellType GetCellType(int col, int row)
        {
            switch(grid[col, row].GetType().Name)
            {
                case "DataGridViewTextBoxCell":
                    return Grid.CellType.Text;

                case "DataGridViewCheckBoxCell":
                    return Grid.CellType.CheckBox;

                default:
                    throw new NotImplementedException();

            }
        }

        public void SetCellType(int col, int row, Grid.CellType value)
        {
            switch (value)
            {
                case Grid.CellType.Text:
                    grid[col, row] = new DataGridViewCheckBoxCell();
                    grid[col, row].Value = "";
                    break;
                case Grid.CellType.CheckBox:
                    grid[col, row] = new DataGridViewCheckBoxCell();
                    grid[col, row].Value = 0;
                    grid[col, row].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    break;
            }
        }

        #endregion

        #region MaxRows, MaxCols 本体のみヘッダ含まず

        private int _MaxRows;
        private int _MaxCols;

        /// <summary>行数の指定</summary>
        public int MaxRows
        {
            get
            {
                Debug.Assert(grid.RowCount == _ColHeaderRows + _MaxRows);

                return this._MaxRows;
            }
            set
            {
                var rows = this.ColHeaderRows + value;
                if (this.grid.RowCount != rows)
                {
                    this._MaxRows = value;
                    this.grid.RowCount = rows;

                    // 行ヘッダ(左)に、ヘッダ属性が自動でコピーされる
                }
            }
        }

        /// <summary>列数の指定</summary>
        public int MaxCols
        {
            get
            {
                Debug.Assert(grid.ColumnCount == _RowHeaderCols + _MaxCols);

                return this._MaxCols;
            }
            set
            {
                var newMaxCols = this.RowHeaderCols + value;
                if (this.grid.ColumnCount != newMaxCols)
                {
                    var diff = newMaxCols - this.grid.ColumnCount;
                    this._MaxCols = value;
                    this.grid.ColumnCount = newMaxCols;

                    if (0 < diff)
                    {
                        // 増えた分の列ヘッダ(上)が自動でコピーされないので、自分でセットする。
                        for (int r = 0; r < this.ColHeaderRows; r ++)
                        {
                            for (int c = 0; c < diff; c ++)
                            {
                                this.grid[this.grid.ColumnCount - c - 1, r].Style = DefaultHeaderCellStyleFactory();
                            }
                        }

                    }


                }
            }
        }

        #endregion
        
        private DataGridViewCellStyle DefaultHeaderCellStyleFactory()
            => new DataGridViewCellStyle(this.DefaultHeaderCellStyle);

        #region Indexer1, Indexer2

        public class Indexer1<T> : IEnumerable<T>
        {
            private readonly Func<int, T> getter;
            private readonly Action<int, T> setter;
            private readonly Func<int> count;

            public Indexer1(Func<int, T> getter, Action<int, T> setter = null, Func<int> count = null)
            {
                this.getter = getter;
                this.setter = setter;
                this.count = count;
            }

            public T this[int index1]
            {
                get { return this.getter(index1); }
                set
                {
                    if (this.setter == null)
                    {
                        throw new NotSupportedException();
                    }

                    this.setter(index1, value);
                }
            }

            public int Count => this.count();

            public bool IsReadOnly
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

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

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class Indexer2<T>
        {
            private readonly Func<int, int, T> getter;
            private readonly Action<int, int, T> setter;

            public Indexer2(Func<int, int, T> getter, Action<int, int, T> setter = null)
            {
                this.getter = getter;
                this.setter = setter;
            }

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
        }

        #endregion

    }
}
