using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JFactory.CsSrcLib.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.InitializeComponent();

            // スプリッターにマウス来たら赤くする。
            this.splitter1.MouseEnter += (se, ea) => this.splitter1.BackColor = Color.Red;
            this.splitter1.MouseLeave += (se, ea) => this.splitter1.BackColor = SystemColors.Control;

            // プロパティグリッドの設定
            this.propertyGrid1.SelectedObject = this.grid;

            this.grid.Dock = DockStyle.Fill;


            this.grid.MaxCols = 5;
            this.grid.MaxRows = 10;

            //var xxx = grid.CellType[0, 0];
            //grid.CellType[1, 3] = Grid.CellType.CheckBox;
            //grid.CellStyle[1,3].Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.grid.Cells[0, 0] = "abc";
            this.grid.ColHeaderRows = 3;
            this.grid.RowHeaderCols = 2;
            this.grid.Cells[0, 1] = "def";

            this.grid.ColWidth[0] = 50;
            this.grid.RowHeight[0] = 50;

            this.grid.RowHeaderWidth[0] = 30;
            this.grid.ColHeaderHeight[0] = 30;

            for (int c = 0; c < grid.MaxCols; c ++)
            {
                for (int r = 0; r < grid.MaxRows; r++)
                {
                    grid.Cells[c, r] = $"({c:x2},{r:x2})";
                }
            }



            this.grid.Combine(0, 0, 2, 1);

            //this.grid.Combine(3, 1, 2, 2);


            //this.grid.aaa();


            Debug.WriteLine("");
        }
    }
}
