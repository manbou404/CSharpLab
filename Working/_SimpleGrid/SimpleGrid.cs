/*  [SimpleGrid]
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
using System.Windows.Forms;

namespace JFactory.CsSrcLib.Forms.SimpleGrid
{
    public class Grid : ScrollableControl
    {
        private Func<int, IEnumerable<int>> seq = new Func<int, IEnumerable<int>>(x => Enumerable.Range(0, x));


        private Cell[,] cells;

        public Grid()
        {
            this.cells = new Cell[3, 5];

            seq(10).SelectMany(x => seq(5), (x, y) => new { x, y }).ToList()
                .ForEach(v => this.cells[v.x, v.y] = new Cell());


            foreach (var v in seq(10).SelectMany(x => seq(5), (x, y) => new { x, y }))
            {
                this.cells[v.x, v.y] = new Cell();
            }

            Debug.WriteLine("");

        }

        //private IEnumerable<DataGridViewCell> Range(int col, int row, int cols, int rows)
        //{   
        //    //return this.cells.Rows.Cast<DataGridViewRow>().Skip(row).Take(rows)
        //    //            .Select(x => x.Cells.Cast<DataGridViewCell>().Skip(col).Take(cols))
        //    //            .SelectMany(x => x);

        //}
    }
}
