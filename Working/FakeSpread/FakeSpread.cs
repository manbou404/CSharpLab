using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Forms.VisualStyles;

namespace JFactory.CsSrcLib.Forms
{
    // 1. BoxArrayがセルの中身(未定)
    // 2. RowsHeight[], ColsWidth[]     // レンダリングのためのデータ
    // 3. Cellクラス
    //      Textセル
    //      フォーカス。編集

    // 4. セルの結合 めんどくさい


    public class FakeSpread : ScrollableControl
    {

        public FakeSpread()
        {
            //this.AutoScrollMinSize = new Size(1000,1000);

            // セルを更新したら、this.Invalidate(Region); で場所を指定して再描画させる。
        }

        //protected override void OnScroll(ScrollEventArgs se)
        //{
        //    base.OnScroll(se);

        //    Debug.WriteLine("{0},{1},{2}", se.ScrollOrientation, se.OldValue, se.NewValue);
        //    //this.VerticalScroll.Value = se.NewValue;
        //}




        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //e.ClipRectangle

            //e.Graphics.DrawLine(Pens.Red,
            //    new Point(0, 0),
            //    new Point(e.ClipRectangle.Width, e.ClipRectangle.Height));

            Debug.WriteLine(AutoScrollPosition);
            Debug.WriteLine(e.ClipRectangle);

            // 再描画すべき親座標
            var obj = new Rectangle(
                Add(e.ClipRectangle.Location, AutoScrollPosition), e.ClipRectangle.Size);
            Debug.WriteLine(obj);

            // IntersectsWith : 矩形vs矩形の重なり判定

            for (int col = 0; col < this.MaxCols; col++)
            {
                for (int row = 0; row < this.MaxRows; row++)
                {
                    DrawCell(col, row, e.Graphics);
                    //if (obj.IntersectsWith(CellRect(col,row)))
                    //{
                    //    this.DrawCell(col, row, AutoScrollPosition, e.Graphics);
                    //}
                }
            }
        }

        private void DrawCell(int col, int row, Graphics g)
        {
            // 再描画が必要か？

            var rect = new Rectangle(
                col * ColWidth + AutoScrollPosition.X,
                row * RowHeight+ AutoScrollPosition.Y,
                ColWidth,
                RowHeight
                );
            //g.DrawRectangle(Pens.Red, rect);

            var text = string.Format("({0},{1})", col, row);
            //g.DrawString(text,
            //    this.Font, Brushes.Blue, rect.X, rect.Y);

            //ButtonRenderer.DrawButton(g, rect, PushButtonState.Default);

            ButtonRenderer.DrawButton(g, rect, text, Font, false, PushButtonState.Normal);
        }


        private Rectangle CellRect(int col, int row)
        {
            return new Rectangle();
        }

        private void DrawCell(int col, int row, Point offset, Graphics g)
        {
            var rect = new Rectangle(
                col * ColWidth + offset.X,
                row * RowHeight + offset.Y, 
                ColWidth, 
                RowHeight
                );



//            g.DrawRectangle(Pens.Red, rect);
        }

        // ButtonRenderer、CheckBoxRenderer、GroupBoxRenderer、RedioButtonRenderer

        // MaxRows, MaxCols
        private void GridResize()
        {
             this.AutoScrollMinSize = new Size(MaxWidth, MaxHeighth);

            this.Invalidate();
        }

        private int MaxWidth { get { return MaxCols * ColWidth + 1; } }
        private int MaxHeighth { get { return MaxRows * RowHeight + 1; } }


        /// <summary></summary>
        public int ColWidth
        {
            get
            {
                return this._ColWidth;
            }
            set
            {
                this._ColWidth = value;
                this.GridResize();

            }
        }

        private int _ColWidth = 10;

        /// <summary></summary>
        public int RowHeight
        {
            get
            {
                return this._RowHeight;
            }
            set
            {
                this._RowHeight = value;
                this.GridResize();

            }
        }

        private int _RowHeight = 10;

        /// <summary></summary>
        public int MaxRows
        {
            get
            {
                return this._MaxRows;
            }
            set
            {
                this._MaxRows = value;
                this.GridResize();
            }
        }

        private int _MaxRows = 10;

        /// <summary></summary>
        public int MaxCols
        {
            get
            {
                return this._MaxCols;
            }
            set
            {
                this._MaxCols = value;
                this.GridResize();
            }
        }

        private int _MaxCols = 5;


        //
        //  ユーティリティ
        //

        private static Point Add(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);
        private static Point Sub(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);

    }
}
