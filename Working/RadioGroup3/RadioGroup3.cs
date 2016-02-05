using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//HasC = ab.HasFlag(Hoge.C);  // false

namespace JFactory.CsSrcLib.Forms
{
    public class RadioGroup3 : TableGroup3
    {
    }

    public class CheckGroup3 : TableGroup3
    {
        //public CheckGroup()
        //{

        //}

        //protected override void OnTableSizeChanged()
        //{
        //    this.table.Controls.Clear();

        //    for (int x = 0; x < this.Cols; x++)
        //    {
        //        for (int y = 0; y < this.Rows; y++)
        //        {
        //            var ctrl = new RadioButton();
        //            ctrl.Text = string.Format("@{0},{1}", x, y);
        //            ctrl.Anchor = AnchorStyles.Left;
        //            this.table.Controls.Add(ctrl, x, y);
        //        }
        //    }

        //    base.OnTableSizeChanged();
        //}
    }

    public class TableGroup3 : GroupBox
    {
        protected TableLayoutPanel table;

        public event EventHandler TableSizeChanging;
        public event EventHandler TableSizeChanged;

        public TableGroup3()
        {
            this.table = new TableLayoutPanel();
            this.table.RowCount = 3;
            this.table.ColumnCount = 2;
            this.SizeChanged += TableGroupBase_SizeChanged;
            this.Controls.Add(table);

            this.SetTableStyle();
            this.OnTableSizeChanged();

            // デバッグ用
            this.table.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            //this.BackColor = System.Drawing.Color.Beige;

        }


        private void SetTableStyle()
        {
            // 行スタイル
            for (; table.RowCount != table.RowStyles.Count;)
            {
                if (table.RowCount < table.RowStyles.Count)
                {
                    this.table.RowStyles.RemoveAt(this.table.RowStyles.Count - 1);
                }
                else
                {
                    var rowStyle = new RowStyle(SizeType.Percent, (float)100.0 / table.RowCount);
                    this.table.RowStyles.Add(rowStyle);
                }
            }

            // 列スタイル
            for (; table.ColumnCount != table.ColumnStyles.Count;)
            {
                if (table.ColumnCount < table.ColumnStyles.Count)
                {
                    this.table.ColumnStyles.RemoveAt(this.table.ColumnStyles.Count - 1);
                }
                else
                {
                    var colStyle = new ColumnStyle(SizeType.Percent, (float)100.0 / table.ColumnCount);
                    this.table.ColumnStyles.Add(colStyle);
                }
            }
        }

        [DefaultValue(3)]
        public int Rows
        {
            get { return this.table.RowCount; }
            set
            {
                if (this.table.RowCount != value)
                {
                    this.table.RowCount = value;
                    this.SetTableStyle();
                    this.OnTableSizeChanged();
                }
            }
        }

        [DefaultValue(2)]
        public int Cols
        {
            get { return this.table.ColumnCount; }
            set
            {
                if (this.table.ColumnCount != value)
                {
                    this.table.ColumnCount = value;
                    this.SetTableStyle();
                    this.OnTableSizeChanged();
                }
            }
        }

        // TODO: Items[,]
        public Control this[int col, int row]
        {
            get
            {
                // Controlsから

                return null;
            }
            set { }
        }

        protected virtual void OnTableSizeChanging()
            => TableSizeChanging?.Invoke(this, EventArgs.Empty);
        protected virtual void OnTableSizeChanged()
            => TableSizeChanged?.Invoke(this, EventArgs.Empty);

        private void TableGroupBase_SizeChanged(object sender, EventArgs e)
        {
            table.Location = this.DisplayRectangle.Location;
            table.Size = this.DisplayRectangle.Size;
        }
    }
}
