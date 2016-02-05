using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DataGridViewEx
{
    public class DataGridViewEx : DataGridView
    {
        //コンストラクタ
        public DataGridViewEx()
        {
            //セル描画イベントハンドラの追加
            this.CellPainting += new DataGridViewCellPaintingEventHandler(DataGridViewEx_CellPainting);
        }

        //ドッキングセルを表す内部クラス

        //第0カラムから2カラム分結合し、上段テキストを"A1"とする場合、

        //new DataGridViewEx.DockingCell(0, 2, "A1")のようにインスタンスを作成
        public class DockingCell
        {
            //開始セル
            private int start;
            public int Start
            {
                get
                {
                    return start;
                }
                set
                {
                    start = value;
                }
            }

            //セル数
            private int count;
            public int Count
            {
                get
                {
                    return count;
                }
                set
                {
                    count = value;
                }
            }

            //ヘッダテキスト
            private string text;
            public string Text
            {
                get
                {
                    return text;
                }
                set
                {
                    text = value;
                }
            }

            //コンストラクタ
            public DockingCell(int start, int count, string text)
            {
                this.start = start;
                this.count = count;
                this.text = text;
            }
        }

        //ドッキングセルのリスト

        //作成したDockingCell型オブジェクトをAdd()メソッドで追加する

        //DockingCellList.Add(new DataGridViewEx.DockingCell(0, 2, "A1"))といった具合

        private List<DockingCell> dockingCellList = new List<DockingCell>();
        public List<DockingCell> DockingCellList
        {
            get
            {
                return dockingCellList;
            }
            set
            {
                dockingCellList = value;
            }
        }

        //セル描画イベントハンドラ
        private void DataGridViewEx_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //このメソッドでは、ヘッダの描画のみを行う
            if (e.ColumnIndex >= 0 && e.RowIndex == -1)
            {
                //そのヘッダがどのドッキングセルに属しているか調べる
                int index = -1;
                for (int i = 0; i < DockingCellList.Count; i++)
                {
                    if (e.ColumnIndex >= DockingCellList[i].Start 
                        && e.ColumnIndex < DockingCellList[i].Start + DockingCellList[i].Count)
                    {
                        index = i;
                        break;
                    }
                }

                //とりあえず塗りつぶす
                e.Graphics.FillRectangle(new SolidBrush(e.CellStyle.BackColor), e.CellBounds);

                //ドッキングしている場合
                if (index >= 0)
                {
                    //上段
                    int x = e.CellBounds.X;
                    for (int i = e.ColumnIndex - 1; i >= DockingCellList[index].Start; i--)
                    {
                        x -= this.Columns[i].Width;
                    }
                    int width = 0;
                    for (int i = DockingCellList[index].Start; i < DockingCellList[index].Start + DockingCellList[index].Count; i++)
                    {
                        width += this.Columns[i].Width;
                    }
                    Rectangle rect1 = new Rectangle(x, e.CellBounds.Y, width, e.CellBounds.Height / 2);
                    DrawCell(e.Graphics, rect1);
                    e.Graphics.DrawString(DockingCellList[index].Text, e.CellStyle.Font, Brushes.Black, rect1.X + 2, rect1.Y + 2);

                    //下段
                    Rectangle rect2 = new Rectangle(e.CellBounds.X, e.CellBounds.Y + e.CellBounds.Height / 2, e.CellBounds.Width, e.CellBounds.Height / 2);
                    DrawCell(e.Graphics, rect2);
                    e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, rect2.X + 2, rect2.Y + 2);
                }

                //ドッキングしていない場合
                else
                {
                    DrawCell(e.Graphics, e.CellBounds);
                    e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, e.CellBounds.X + 2, e.CellBounds.Y + 2);
                }

                //ハンドルされたことを報告
                e.Handled = true;
            }
        }

        //セルの描画に用いるメソッド
        private void DrawCell(Graphics g, Rectangle rect)
        {
            g.DrawLine(new Pen(SystemColors.ControlDark), rect.Left - 1, rect.Top, rect.Right - 2, rect.Top);
            g.DrawLine(new Pen(SystemColors.ControlDark), rect.Left - 1, rect.Top, rect.Left - 1, rect.Bottom - 1);
            g.DrawLine(Pens.White, rect.Left, rect.Top + 1, rect.Right - 2, rect.Top + 1);
            g.DrawLine(Pens.White, rect.Left, rect.Top + 1, rect.Left, rect.Bottom - 1);
        }
    }
}