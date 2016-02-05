using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JFactory.CsSrcLib.Library
{
    [TestClass]
    public class IndexerTests
    {
        [TestMethod]
        public void IndexerTest()
        {
            const int length = 5;
            var list = Enumerable.Range(0, length).ToList();
            var indexer = new Indexer<int>(()=>list.Count, (i)=>list[i], (i,v) => list[i] = v);

            indexer.Count.Is(length);
            for (int i = 0; i < indexer.Count; i++)
            {
                indexer[i].Is(i);
                indexer[i] = i * i;
                indexer[i].Is(i*i);
            }
        }

        [TestMethod]
        public void IndexerTest2()
        {
            const int length = 5;
            var list = Enumerable.Range(5, length).ToList();

            var indexerAllNull = new Indexer<int>(null, null);
            indexerAllNull[0].Is(0);        // 未定義はdefault(T)が返る
            indexerAllNull.Count.Is(0);     // 未定義は０が返る
            AssertEx.Throws<NotSupportedException>(() => indexerAllNull[0] = 0);

            var indexerCountNull = new Indexer<int>(null, i => list[i]);
            indexerCountNull[0].Is(5);
            indexerCountNull.Count.Is(0);   // 未定義は０が返る
            AssertEx.Throws<NotSupportedException>(() => indexerCountNull[0] = 0);
            // この例外は、Indexerが出しているのではなく、setterのActionがListにアクセスして発生する
            AssertEx.Throws <ArgumentOutOfRangeException>(() => indexerCountNull[5].Is(0));
        }

        [TestMethod]
        public void Indexer2Test()
        {
            // ３列５行 {0,0}, {1,0}, {2,0}, {0,1}, {1,1}, {2,1}, ... {0,4},{1,4,1},{2,4}  
            const int maxcols = 3, maxrows = 5;
            var seq = Enumerable.Range(0, maxcols).SelectMany(
                 col => Enumerable.Range(0, maxrows), (col, row) => new { col, row }).ToList();
            // ３列５行の２次元配列を作り、初期値(col*row)を入れる
            var arr = new int[maxcols,maxrows];
            seq.ForEach(v => arr[v.col, v.row] = (v.col+1) * (v.row+1));

            var indexer2 = new Indexer2<int>(()=> arr.GetLength(0), ()=> arr.GetLength(1),
                (c,r) => arr[c,r], (c,r,v) => arr[c,r] = v );

            // サイズ
            indexer2.Width.Is(maxcols);
            indexer2.Height.Is(maxrows);
            indexer2.Count.Is(maxcols * maxrows);

            // 列挙子による、元の配列との中身のチェック
            foreach (var v in indexer2.Indexed())
            {
                v.Element.Is(arr[v.Col, v.Row]);
            }

            // ループによる、元の配列との中身のチェック
            for (int r = 0; r < indexer2.Height; r++)
            {
                for (int c = 0; c < indexer2.Width; c++)
                {
                    indexer2[c, r].Is((c+1) * (r+1));
                    indexer2[c, r] = 0;
                    indexer2[c, r].Is(0);
                }
            }
        }
    }
}
