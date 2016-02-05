using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JFactory.CsSrcLib.Library;

namespace JFactory.CsSrcLib.Forms
{
    public partial class SimpleGrid : ScrollableControl
    {
        private DataStrage ds = new DataStrage();

        /// <summary>SimpleGridを初期化する</summary>
        public SimpleGrid()
        {
            ds.NewObject = () => new LabelCell(this);


            this.MaxRows = 15;
            this.MaxCols = 10;
        }

        #region MaxRows, MaxCols

        /// <summary>行数</summary>
        public int MaxRows
        {
            get
            {
                return ds.MaxRows;
            }
            set
            {
                if (ds.MaxRows != value)
                {
                    ds.MaxRows = value;
                    this.AutoScrollMinSize = ds.GetMinSize();
                    //this.Invalidate();
                }

                ds.NewObject = () => new LabelCell(this);
            }
        }

        /// <summary>列数</summary>
        public int MaxCols
        {
            get
            {
                return ds.MaxCols;
            }
            set
            {
                if (ds.MaxCols != value)
                {
                    ds.MaxCols = value;
                    this.AutoScrollMinSize = ds.GetMinSize();
                    //this.Invalidate();
                }
            }
        }

        #endregion

        int paintCounter = 0;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            paintCounter++;

            Debug.WriteLine(paintCounter);
            Debug.WriteLine(AutoScrollPosition);
            Debug.WriteLine(e.ClipRectangle);

            // 再描画すべき親座標
            var obj = new Rectangle(
                Add(e.ClipRectangle.Location, AutoScrollPosition), e.ClipRectangle.Size);
            Debug.WriteLine(obj);

            //var query = Enumerable.Range(0, this.MaxRows).SelectMany(
            //        (row) =>  Enumerable.Range(0, 5),
            //        (row, col) => string.Format("{0},{1}", row, col));



            // IntersectsWith : 矩形vs矩形の重なり判定
            int left = 0, top = 0;
            for (int row = 0; row < this.MaxRows; row++)
            {
                left = 0;
                for (int col = 0; col < this.MaxCols; col++)
                {
                    var r = new Rectangle(
                        left + AutoScrollPosition.X,
                        top + AutoScrollPosition.Y,
                        ds.ColsInfo(col).Size,
                        ds.RowsInfo(row).Size

                        );

                    //this.ds[row, col].Text = string.Format("({0},{1})", row, col);
                    this.ds[row, col].Draw(e.Graphics, r);

                    var t = string.Format("({0},{1}).{2}", row, col, paintCounter);
                    var f = new Font(this.Font.FontFamily, 7);
                    e.Graphics.DrawString(t, f, Brushes.Blue, r.X, r.Y);
                    // +"\n" + DateTime.Now.ToString("mm:ss.fff")

                    left += ds.ColsInfo(col).Size;
                }

                top += ds.RowsInfo(row).Size;
            }
        }


        //
        //  ユーティリティ
        //
        private static Point Add(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);
        private static Point Sub(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);

        //
        //  データ定義
        //
        public class LabelCell : ICell
        {
            private SimpleGrid parent;

            public LabelCell(SimpleGrid parent)
            {
                this.parent = parent;
            }

            public string Text { get; set; }

            public void Draw(Graphics g, Rectangle r)
            {
                g.DrawString(Text, parent.Font, Brushes.Blue, r.X, r.Y);
                g.DrawRectangle(Pens.Red, r);
            }
        }

        public interface ICell
        {
            string Text { get; set; }
            void Draw(Graphics g, Rectangle r);
        }


        public class LineInfo
        {
            public int Size { get; set; }
            public bool Visible { get; set; }
        }
    }
}
