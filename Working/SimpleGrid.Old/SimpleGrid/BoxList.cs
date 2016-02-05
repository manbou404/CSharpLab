using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFactory.CsSrcLib.Library
{
    public class BoxList<T>
    {
        /// <summary>バッキングストア</summary>
        private readonly List<List<T>> box;

        private Func<T> _NewObject;


        public Func<T> NewObject
        {
            get
            {
                return this._NewObject;
            }
            set
            {
                Check.ArgumentNull(value, nameof(value));
                this._NewObject = value;
            }
        }



        /// <summary>２次元コレクションを初期化する</summary>
        public BoxList()
        {
            box = new List<List<T>>();
            _NewObject = () => default(T);
        }

        /// <summary>２次元コレクションを指定サイズで初期化する</summary>
        /// <param name="maxRows">行数(横方向の要素数)</param>
        /// <param name="maxCols">列数(縦方向の要素数)</param>
        public BoxList(int maxRows, int maxCols) : this()
        {
            this.Resize(maxRows, maxCols);
        }

        /// <summary>コレクションの行数(縦方向の要素数)を固定する(Add, Remove, Insert</summary>
        public bool FixedRow { get; set; } = false;

        /// <summary>コレクションの列数(横方向の要素数)を固定する(Add, Remove, Insert</summary>
        public bool FixedCol { get; set; } = false;

        /// <summary>コレクションの行数(縦方向の要素数)</summary>
        public int MaxRows
        {
            get { return box.Count; }
            set { this.ResizeRows(value); }
        }

        /// <summary>コレクションの列数(横方向の要素数)</summary>
        public int MaxCols
        {
            get { return (box.Count != 0) ? box[0].Count : 0; }
            set { this.ResizeCols(value); }
        }

        /// <summary>指定位置の要素を返す</summary>
        /// <param name="row">行</param><param name="col">列</param>
        /// <returns>指定位置の要素</returns>
        public T this[int row, int col]
        {
            get { return this.box[row][col]; }
            set { this.box[row][col] = value; }
        }

        #region Operation

        /// <summary>行を末尾に追加する</summary>
        public void AddRow()
        {
            //if (this.FixedRow)
            //{
            //    throw new InvalidOperationException("FixedRow==true");
            //}

            //this._AddRow();
        }


        /// <summary>列を末尾に追加する</summary>
        public void AddCol()
        {
            //if (this.FixedCol)
            //{
            //    throw new InvalidOperationException("FixedCol==true");
            //}

            //this._AddCol();
        }

        public void InsertRow(int row)
        {
            if (this.FixedRow == false)
            {
                this.AddRow();
            }
        }

        public void InsertCol(int col)
        {
            if (this.FixedCol == false)
            {
                this.AddRow();
            }

        }

        #endregion

        /// <summary>コレクションのサイズを変更する</summary>
        /// <param name="newRows">新しい行数</param>
        /// <param name="newCols">新しい列数</param>
        /// <remarks>古いサイズより新しいサイズのほうが小さい場合、中身は捨てる</remarks>
        public void Resize(int newRows, int newCols)
        {
            if ((newRows == 0 && newCols == 0) == false)
            {
                Check.ArgumentOutOfRange((newRows != 0), nameof(newRows));
                Check.ArgumentOutOfRange((newCols != 0), nameof(newCols));
            }

            this.ResizeRows(newRows);
            this.ResizeCols(newCols);
        }

        /// <summary>行の反復処理</summary>
        /// <param name="col">列番号</param>
        /// <returns>列挙子</returns>
        public IEnumerable<T> Rows(int col)
        {
            Check.ArgumentOutOfRange(col.IsRange(0, this.MaxCols - 1), nameof(col));

            foreach (var x in box)
            {
                yield return x[col];
            }
        }

        /// <summary>列の反復処理</summary>
        /// <param name="row">行番号</param>
        /// <returns>列挙子</returns>
        public IEnumerable<T> Cols(int row)
        {
            Check.ArgumentOutOfRange(row.IsRange(0, this.MaxRows - 1), nameof(row));

            foreach (var x in box[row])
            {
                yield return x;
            }
        }

        /// <summary>コレクションの行数を変更する(MaxRows.setから呼ばれる</summary>
        /// <param name="newMaxRows">新しい行数</param>
        private void ResizeRows(int newMaxRows)
        {
            if (newMaxRows == 0)
            {
                this.box.Clear();
                return;
            }

            var diff = newMaxRows - this.MaxRows;
            if (diff == 0)
            {
                // サイズの変更なし
                return;
            }

            if (diff < 0)
            {
                this.RemoveRows(-diff);
            }
            else
            {
                this.AppendRows(diff);
            }
        }

        /// <summary>コレクションの列数を変更する(MaxCols.setから呼ばれる</summary>
        /// <param name="newMaxCols">新しい列数</param>
        private void ResizeCols(int newMaxCols)
        {
            if (newMaxCols == 0)
            {
                this.box.Clear();
                return;
            }

            var diff = newMaxCols - this.MaxCols;
            if (diff == 0)
            {
                // サイズの変更なし
                return;
            }

            if (diff < 0)
            {
                this.RemoveCols(-diff);
            }
            else
            {
                this.AppendCols(diff);
            }
        }

        /// <summary>行を末尾に追加する</summary>
        /// <param name="diff">指定列数</param>
        public virtual void AppendRows(int diff)
        {
            Debug.Assert(0 <= diff);

            // 増やす(外側のコレクションに、新しい内側のコレクションを作って追加する)
            for (int i = 0; i < diff; i++)
            {
                //box.Add(Enumerable.Range(0, this.MaxCols).Select(_ => default(T)).ToList());
                box.Add(Enumerable.Range(0, this.MaxCols).Select(_ => this.NewObject()).ToList());
            }

            if (this.MaxCols == 0)
            {
                this.ResizeCols(1);
            }
        }

        /// <summary>行を末尾から削除する</summary>
        /// <param name="diff">指定列数</param>
        public virtual void RemoveRows(int diff)
        {
            Debug.Assert(0 <= diff);

            // 減らす(外側のコレクションから)
            box.RemoveRange(this.MaxRows - diff, diff);
        }

        /// <summary>列を末尾に追加する</summary>
        /// <param name="diff">指定列数</param>
        public virtual void AppendCols(int diff)
        {
            Debug.Assert(0 <= diff);

            //もし行列が0,0なら１にする
            if (this.MaxRows == 0)
            {
                ResizeRows(1);
                diff--;
            }

            // 増やす(内側のコレクションすべて(全行数)に、新しい要素を追加)
            for (int i = 0; i < diff; i++)
            {
                //box.ForEach(x => x.Add(default(T)));
                box.ForEach(x => x.Add(this.NewObject()));
            }
        }

        /// <summary>列を末尾から削除する</summary>
        /// <param name="diff">指定列数</param>
        public virtual void RemoveCols(int diff)
        {
            Debug.Assert(0 <= diff);

            // 減らす(内側のコレクションから)
            box.ForEach(x => x.RemoveRange(this.MaxCols - diff, diff));
        }
    }
}
