using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFactory.CsSrcLib.Forms
{
    /// <summary></summary>
    public enum Optimize
    {
        /// <summary>The vertical</summary>
        Vertical,
        /// <summary>The horizontal</summary>
        Horizontal,
    }


    /*
        # Optimize.Virtical 縦方向の追加/挿入/削除が速い

        List+   +-------+-------+-------+
        |   |-->| (0,0) | (1,0) | (2,0) |
        |   |   +-------+-------+-------+
        +---+   +-------+-------+-------+
        |   |-->| (0,1) | (1,1) | (2,1) |
        |   |   +-------+-------+-------+
        +---+   +-------+-------+-------+
        |   |-->| (0,2) | (1,2) | (2,2) |
        |   |   +-------+-------+-------+
        +---+   +-------+-------+-------+
        |   |-->| (0,3) | (1,3) | (2,3) |
        |   |   +-------+-------+-------+
        +---+        
        
        # Optimize.Horizontal   横方向の追加/挿入/削除が速い

        List----+-------+-------+        
        |       |       |       |
        +-------+-------+-------+        
            |       |       |
            V       V       V
         +-----+ +-----+ +-----+        
         |(0,0)| |(1,0)| |(2,0)|
         +-----+ +-----+ +-----+        
         |(0,1)| |(1,1)| |(2,1)|
         +-----+ +-----+ +-----+        
         |(0,2)| |(1,2)| |(2,2)|
         +-----+ +-----+ +-----+        
         |(0,3)| |(1,3)| |(2,3)|
         +-----+ +-----+ +-----+        
    */

    // Fields / Constructors / Finalizers (Destructors) / Delegates / Events /
    //      Enums / Interfaces / Properties / Indexers / Methods / Structs / Classes

    /// <summary>２次元データ(配列を使わない)</summary>
    /// <typeparam name="T">リスト内の要素の型</typeparam>
    public class BoxArray<T>
    {
        /// <summary>バッキングストア</summary>
        private readonly List<List<T>> box;


        /// <summary>縦方向か横方向かの最適化(未実装：縦方向に固定)</summary>
        private Optimize Optimize { get; set; } = Optimize.Vertical;

        /// <summary>BoxArrayを初期化する</summary>
        public BoxArray()
        {
            this.box = new List<List<T>>();
        }

        public int Width
        {
            get
            {
                return (this.Optimize == Optimize.Horizontal)
                    ? box.Count
                    : box.Count == 0 ? 0 : box[0].Count;
            }
            set
            {
                if (this.Optimize == Optimize.Horizontal)
                {
                    this.Redim1(value);
                }
                else
                {
                    this.Redim2(value);
                }
            }
        }

        public int Height
        {
            get
            {
                return (this.Optimize == Optimize.Vertical)
                    ? box.Count
                    : box.Count == 0 ? 0 : box[0].Count;
            }
            set
            {
                if (this.Optimize == Optimize.Vertical)
                {
                    this.Redim1(value);
                }
                else
                {
                    this.Redim2(value);
                }
            }
        }

        //public T this[int x, int y]
        //{
        //    return null;
        //}

        /// <summary>内部：親コレクションのリサイズ(2次元配列の1次元目に相当)</summary>
        /// <param name="newSize">新しいサイズ</param>
        /// <remarks>古いサイズより新しいサイズのほうが小さい場合、中身は捨てる</remarks>
        private void Redim1(int newSize)
        {
            int size1 = box.Count();
            var diff = newSize - size1;
            if (diff == 0)
            {
                // サイズの変更なし
                return;
            }

            if (diff < 0)
            {
                // 減らす
                box.RemoveRange(size1 + diff, -diff);
            }
            else
            {
                // 増やす(外のコレクションに、新しい内のコレクションを作って追加する)
                Enumerable.Range(0, diff).ToList()
                    .ForEach(x => box.Add(Enumerable.Repeat(default(T), newSize).ToList()));
            }
        }

        /// <summary>内部：子コレクションのリサイズ(2次元配列の2次元目に相当)</summary>
        /// <param name="newSize">新しいサイズ</param>
        /// <remarks>古いサイズより新しいサイズのほうが小さい場合、中身は捨てる</remarks>
        private void Redim2(int newSize)
        {
            int size2 =  box.Count == 0 ? 0 : box[0].Count;
            var diff = newSize - size2;
            if (diff == 0)
            {
                // サイズの変更なし
                return;
            }

            if (diff < 0)
            {
                // 減らす
                box.ForEach(x => x.RemoveRange(size2 + diff, -diff));
            }
            else
            {
                // 増やす(内のコレクションすべてに、新しい要素を追加)
                box.ForEach(x => Enumerable.Range(0, diff).ToList()
                                            .ForEach(_ => x.Add(default(T))));
            }
        }

    }
}
