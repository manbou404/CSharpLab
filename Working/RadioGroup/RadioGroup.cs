/*  [Module name]
 *
 *  Copyright (C) 2015 JFactory(manbou404)
 *
 *  This software is released under the MIT License.
 *  http://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JFactory.CsSrcLib.Library;

namespace JFactory.CsSrcLib.Forms
{
    /// <summary>Flag属性のついたEnumGroup</summary>
    public class FlagGroup : CheckGroup
    {

    }

    /// <summary>チェックボックス</summary>
    public class CheckGroup : GridGroup
    {

    }

    public class EnumGroup : RadioGroup
    {

    }

    public class RadioGroup : GridGroup
    {

    }

    /// <summary>Gridレイアウトに対応したGroupBox</summary>
    public class GridGroup : GroupBox
    {
        /// <summary>行と列の最大値(32に深い意味はない)</summary>
        private readonly int MaxColRow = 32;

        private int _Rows = 1;  // デザイン時にLayoutが走ってゼロ割になるのを回避
        private int _Cols = 1;  //      同上
        private Orientation _Direction;
        private ContentAlignment _Align;

        /// <summary>GridGroupを初期化する</summary>
        public GridGroup()
        {
            // プロパティ初期値(DefaultValueと同じにしないとダメ)
            this.Rows = 4;
            this.Cols = 2;
            this.Direction = Orientation.Vertical;
            this.Align = ContentAlignment.TopLeft;
        }

        #region Properties

        [Description("仮想的に分割する行数(1~32)を指定します。")]
        [DefaultValue(4)]
        public int Rows
        {
            get { return this._Rows; }
            set
            {
                if (this._Rows != value.Clamp(1, MaxColRow))
                {
                    this._Rows = value;
                    this.TableLayout();
                    this.OnTableSizeChanged();
                }
            }
        }

        [Description("仮想的に分割する列数(1~32)を指定します。")]
        [DefaultValue(2)]
        public int Cols
        {
            get { return this._Cols; }
            set
            {
                if (this._Cols != value.Clamp(1, MaxColRow))
                {
                    this._Cols = value;
                    this.TableLayout();
                    this.OnTableSizeChanged();
                }
            }
        }

        [Description("レイアウトする方向を指定します(順番はControlsに依存します)。")]
        [DefaultValue(Orientation.Vertical)]
        public Orientation Direction
        {
            get { return this._Direction; }
            set
            {
                if (this._Direction != value)
                {
                    this._Direction = value;
                    this.TableLayout();
                    this.OnTableForcedLayoutChanged();
                }
            }
        }

        [Description("仮想的に分割した領域内での、Controlの配置位置を指定します")]
        [DefaultValue(ContentAlignment.TopLeft)]
        public ContentAlignment Align
        {
            get { return this._Align; }
            set
            {
                if (this._Align != value)
                {
                    this._Align = value;
                    this.TableLayout();
                    this.OnTableForcedLayoutChanged();
                }
            }
        }

        #endregion

        #region Events

        private event EventHandler TableSizeChanged;
        private event EventHandler TableForcedLayoutChanged;

        protected virtual void OnTableSizeChanged()
            => this.TableSizeChanged?.Invoke(this, EventArgs.Empty);

        protected virtual void OnTableForcedLayoutChanged()
            => this.TableForcedLayoutChanged?.Invoke(this, EventArgs.Empty);

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            this.TableLayout();
        }
        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            this.TableLayout();
        }
        //protected override void OnResize(EventArgs e)
        //{
        //    base.OnResize(e);
        //    this.TableLayout();
        //}
        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            this.TableLayout();
        }
        #endregion

        /// <summary>Controlsの順番で、Direction方向にレイアウトする</summary>
        protected void TableLayout()
        {
            var size = new Size(this.DisplayRectangle.Width / this.Cols,
                                this.DisplayRectangle.Height / this.Rows);

            // (0,0),(0,1),(0,2) ... VirtかHorzで並び順を決める
            var cols = Enumerable.Range(0, this.Cols);
            var rows = Enumerable.Range(0, this.Rows);
            var crxy = this.Direction == Orientation.Vertical
                        ? cols.SelectMany(x => rows, (x, y) => new { x, y })
                        : rows.SelectMany(x => cols, (y, x) => new { x, y });

            foreach (var v in crxy.Zip(this.Controls.Cast<Control>(), (xy, ctrl) => new { xy, ctrl }))
            {
                var loc = new Point(this.DisplayRectangle.Left + v.xy.x * size.Width,
                                    this.DisplayRectangle.Top + v.xy.y * size.Height);
                loc.Offset(CalcOffset(size, v.ctrl.Size, this.Align));
                v.ctrl.Location = loc;
                v.ctrl.Size = size;
            }
        }

        /// <summary>オフセットの計算：Alignによりouterとinnerのオフセットが変わる</summary>
        private Point CalcOffset(Size outer, Size inner, ContentAlignment align)
        {
            int vert = this.VertJudge(align);     // 上(0),中(1),下(2)の判定
            int horz = this.HorzJudge(align);     // 左(0),中(1),右(2)の判定

            // 縦方向：上中下
            int top = 0;            // 縦の上段         if (vert == 0) ... 
            if (vert == 1)          // 縦の中段
            {
                top = (outer.Height - inner.Height) / 2;
            }
            else if (vert == 2)     // 縦の下段
            {
                top = outer.Height - inner.Height;
            }

            // 横方向：左中右
            int left = 0;           // 横の左寄せ        if (horz == 0) ...   
            if (horz == 1)          // 横の中央寄せ
            {
                left = (outer.Width - inner.Width) / 2;
            }
            else if (horz == 2)     // 横の右寄せ
            {
                left = outer.Width - inner.Width;
            }

            return new Point(left, top);
        }

        /// <summary>ContentAlignmentの縦方向判定：上(0),中(1),下(2)</summary>
        private int VertJudge(ContentAlignment align)
        {
            // enum ContentAlignment の定義内容
            // TopLeft = 1,         TopCenter = 2,          TopRight = 4,
            // MiddleLeft = 16,     MiddleCenter = 32,      MiddleRight = 64,
            // BottomLeft = 256,    BottomCenter = 512,     BottomRight = 1024
            int cc = (int)align;
            if ((cc & 0x000f) != 0) return 0;
            if ((cc & 0x00f0) != 0) return 1;
            // if ((cc & 0x0f00) != 0) return 2;
            return 2;
        }

        /// <summary>ContentAlignmentの横方向判定：左(0),中(1),右(2)</summary>
        private int HorzJudge(ContentAlignment align)
        {
            // enum ContentAlignment の定義内容
            // TopLeft = 1,         TopCenter = 2,          TopRight = 4,
            // MiddleLeft = 16,     MiddleCenter = 32,      MiddleRight = 64,
            // BottomLeft = 256,    BottomCenter = 512,     BottomRight = 1024
            int cc = (int)align;
            if ((cc & 0x0111) != 0) return 0;
            if ((cc & 0x0222) != 0) return 1;
            //if ((cc & 0x0444) != 0) return 2;
            return 2;
        }
    }
}
