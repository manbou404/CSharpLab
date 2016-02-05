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

            // プロパティグリッドの設定
            this.propertyGrid1.SelectedObject = this.simpleGrid1;

            // スプリッターにマウス来たら赤くする。
            this.splitter1.MouseEnter += (se, ea) => this.splitter1.BackColor = Color.Red;
            this.splitter1.MouseLeave += (se, ea) => this.splitter1.BackColor = SystemColors.Control;

            //var x = simpleGrid1.Items[0,0];

            //var xxx = this.simpleGrid1.RowHeight.Count;
            //this.simpleGrid1.RowHeight[0] = 3;
            //var yyy = this.simpleGrid1.RowHeight[0];

            //foreach (var x in this.simpleGrid1.RowHeight)
            //{

            //}

            //var vv = this.simpleGrid1.RowHeight.Select(x => x).ToList();


            //var query = from row in Enumerable.Range(0, 10)
            //            from col in Enumerable.Range(0, 5)
            //            select string.Format("{0},{1}", row, col);

            //var query = Enumerable.Range(0, 10).SelectMany(
            //        (row) =>  Enumerable.Range(0, 5),
            //        (row, col) => string.Format("{0},{1}", row, col));


            var src = new [] { 1, 2, 3, 4, 5 };
            //var dst = new List<int>();
            var add = 0;
            //foreach (var x in src)
            //{
            //    dst.Add(add += x);
            //}
            var dst = src.Select(x => add += x);
            add = 3;

            Debug.WriteLine("");



        }

    }
}
