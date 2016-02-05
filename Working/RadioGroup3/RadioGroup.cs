using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JFactory.CsSrcLib.Forms
{
    public class TableGroup : GroupBox
    {
        private readonly int MaxColRow = 28;
        private int _Rows;
        private int _Cols;

        private Orientation _ForcedLayout;

        private event EventHandler TableSizeChanged;
        private event EventHandler TableForcedLayoutChanged;

        public TableGroup()
        {
            this.Rows = 4;
            this.Cols = 2;
            this.ForcedLayout = Orientation.Vertical;
        }

        #region Properties

        [DefaultValue(4)]
        public int Rows
        {
            get { return this._Rows; }
            set { this.SetTableSize(ref this._Rows, value); }
        }

        [DefaultValue(2)]
        public int Cols
        {
            get { return this._Cols; }
            set { this.SetTableSize(ref this._Cols, value); }
        }

        [DefaultValue(typeof(Orientation), "Vertical")]
        public Orientation ForcedLayout
        {
            get { return this._ForcedLayout; }
            set
            {
                if (this._ForcedLayout != value)
                {
                    this._ForcedLayout = value;
                    this.TableLayout();
                    this.OnTableForcedLayoutChanged();
                }
            }
        }

        #endregion

        protected void TableLayout()
        {
            // RowsとColsを元に、
            ForcedLayout = Orientation.Horizontal;
        }

        protected virtual void OnTableSizeChanged()
            => this.TableSizeChanged?.Invoke(this, EventArgs.Empty);

        protected virtual void OnTableForcedLayoutChanged()
            => this.TableForcedLayoutChanged?.Invoke(this, EventArgs.Empty);

        /// <summary>Cols,Rowsプロパティの設定</summary>
        private void SetTableSize(ref int rowcol, int value)
        {
            if (1 < value)
            {
                value = 1;
            }
            else if (MaxColRow < value)
            {
                value = MaxColRow;
            }

            if (rowcol != value)
            {
                rowcol = value;
                this.TableLayout();
                this.OnTableSizeChanged();
            }
        }

        public class aaa
        {
            public Rectangle Rect { get; set; }

            public Control Control { get; set; }
        }
    }
}
