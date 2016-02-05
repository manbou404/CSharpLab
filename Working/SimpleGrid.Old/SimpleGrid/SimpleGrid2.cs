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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/*
namespace JFactory.CsSrcLib.Forms
{
    public class IndexedProperty<T> : IEnumerable<T>
    {
        private IList<T> collection;
        Func<int, T, bool> varl;

        public void Validation(Func<int, T, bool> a)
        {

        }

        public IndexedProperty(List<T> list)
        {
            collection = list;
        }

        public IndexedProperty(T[] array)
        {
            collection = array;
        }

        public T this[int index]
        {
            get { return collection[index]; }
            set
            {
                
                if (collection[index].Equals(value) == false && varl?.Invoke(index, value) == true)
                {
                    collection[index] = value;
                    this.ValueChanged(this, EventArgs.Empty);
                }
            }
        }

        public int Count {  get { return collection.Count; } }

        public IEnumerator<T> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        public event EventHandler ValueChanged;
    }

    /// <summary>SimpleGrid</summary>
    public class SimpleGrid2 : ScrollableControl
    {
        private List<List<ICell>> cells;
        private List<int> rowsInfo;         // 行の情報(高さ・非表示・
        private List<int> colsInfo;         // 列の情報

        private readonly List<int> _RowHeight;
        private readonly List<int> _ColWidth;

        public SimpleGrid2()
        {
            var x = this._RowHeight.Count;
            this._RowHeight = new List<int>();
            this._ColWidth = new List<int>();

            this.RowHeight = new IndexedProperty<int>(this._ColWidth);
            //            this.RowHeight.Validation((i, v) => false);
            this.RowHeight.Validation(aaa);

        }
        private bool aaa(int i, int v)
        {
            return false;

            var h = rowInfo[0].Height;

        }

        public IndexedProperty<int> RowHeight { get; private set; }



        //
        //  ユーティリティ
        //
        private static Point Add(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);
        private static Point Sub(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);

        public interface ICell
        {
            void Draw();

        }

        private List<RowInfo> rowInfo;
        private List<ColInfo> colInfo;

        public class RowInfo
        {
            public int Height { get; set; }
            public bool Visible { get; set; }
        }

        public class ColInfo
        {
            public int Width { get; set; }
            public bool Visible { get; set; }
        }


    }
}
*/