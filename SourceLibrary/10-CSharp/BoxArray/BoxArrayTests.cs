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
    public class BoxArrayTests
    {
        //TODO: 最後の１行を削除
        //TODO: 最後の１列を削除

        [TestMethod]
        public void RemoveColsErrorTests()
        {
            RemoveColsErrorTestsImpl(new BoxList<string>());
            RemoveColsErrorTestsImpl(new BoxArray<string>());
        }
        public void RemoveColsErrorTestsImpl(IBoxArray<string> box)
        {
            var cols = 1;
            var rows = 2;
            this.InitValue(box, cols, rows);

            var ex = AssertEx.Throws<ArgumentOutOfRangeException>(() => box.RemoveCols(99));
            ex.ParamName.Is("col");

            box.RemoveCols(0);
            box.Width.Is(0);
            box.Height.Is(0);
        }

        [TestMethod]
        public void RemoveRowsErrorTests()
        {
            RemoveRowsErrorTestsImpl(new BoxList<string>());
            RemoveRowsErrorTestsImpl(new BoxArray<string>());
        }
        public void RemoveRowsErrorTestsImpl(IBoxArray<string> box)
        {
            var cols = 2;
            var rows = 1;
            this.InitValue(box, cols, rows);

            var ex = AssertEx.Throws<ArgumentOutOfRangeException>(() => box.RemoveRows(99));
            ex.ParamName.Is("row");

            box.RemoveRows(0);
            box.Width.Is(0);
            box.Height.Is(0);
        }

        [TestMethod]
        public void RemoveColsTests()
        {
            RemoveColsTestsImpl(new BoxList<string>());
            RemoveColsTestsImpl(new BoxArray<string>());

        }
        public void RemoveColsTestsImpl(IBoxArray<string> box)
        {
            var cols = 4;
            var rows = 2;

            this.InitValue(box, cols, rows);

            box.RemoveCols(1);          // 1列目を削除
            box.Width.Is(3);            // 列が 4 -> 3 に縮んだ
            Loop(rows).ForEach(r => box[0, r].Is($"0,{r}"));
            Loop(rows).ForEach(r => box[1, r].Is($"2,{r}"));
            Loop(rows).ForEach(r => box[2, r].Is($"3,{r}"));

            box.FixedWidth = true;      // 列数を固定
            box.RemoveCols(1, (c, r) => $"<{c},{r}>");
            box.Width.Is(3);            // 列数は変わらない
            Loop(rows).ForEach(r => box[0, r].Is($"0,{r}"));
            Loop(rows).ForEach(r => box[1, r].Is($"3,{r}"));
            Loop(rows).ForEach(r => box[2, r].Is($"<2,{r}>"));
        }

        [TestMethod]
        public void RemoveRowsTests()
        {
            RemoveRowsTestsImpl(new BoxList<string>());
            RemoveRowsTestsImpl(new BoxArray<string>());

        }
        public void RemoveRowsTestsImpl(IBoxArray<string> box)
        {
            var cols = 2;
            var rows = 4;

            this.InitValue(box, cols, rows);

            box.RemoveRows(1);          // 1行目を削除
            box.Height.Is(3);           // 行が 4 -> 3 に縮んだ
            Loop(cols).ForEach(c => box[c, 0].Is($"{c},0"));
            Loop(cols).ForEach(c => box[c, 1].Is($"{c},2"));
            Loop(cols).ForEach(c => box[c, 2].Is($"{c},3"));

            box.FixedHeight = true;     // 行数を固定
            box.RemoveRows(1, (c, r) => $"<{c},{r}>");
            box.Height.Is(3);           // 列数は変わらない
            Loop(cols).ForEach(c => box[c, 0].Is($"{c},0"));
            Loop(cols).ForEach(c => box[c, 1].Is($"{c},3"));
            Loop(cols).ForEach(c => box[c, 2].Is($"<{c},2>"));

        }

        [TestMethod]
        public void InsertColsTest()
        {
            InsertColsTestImpl(new BoxList<string>());
            InsertColsTestImpl(new BoxArray<string>());
        }
        public void InsertColsTestImpl(IBoxArray<string> box)
        {
            var cols = 3;
            var rows = 2;
            this.InitValue(box, cols, rows);  // 3列2行
            box.Width.Is(3);

            box.InsertCols(1);          // 1列目に挿入
            box.Width.Is(4);            // 列が 3->4 に伸びた
            Loop(rows).ForEach(r => box[0, r].Is($"0,{r}"));
            Loop(rows).ForEach(r => box[1, r].Is(default(string)));
            Loop(rows).ForEach(r => box[2, r].Is($"1,{r}"));

            box.FixedWidth = true;      // 列数を固定
            box.InsertCols(1, (c, r) => $"<{c},{r}>");
            box.Width.Is(4);
            Loop(rows).ForEach(r => box[0, r].Is($"0,{r}"));
            Loop(rows).ForEach(r => box[1, r].Is($"<1,{r}>"));
            Loop(rows).ForEach(r => box[2, r].Is(default(string)));
            Loop(rows).ForEach(r => box[3, r].Is($"1,{r}"));
        }

        [TestMethod]
        public void InsertRowsTest()
        {
            InsertRowsTestImpl(new BoxList<string>());
            InsertRowsTestImpl(new BoxArray<string>());
        }
        public void InsertRowsTestImpl(IBoxArray<string> box)
        {
            this.InitValue(box, 2, 3);

            box.InsertRows(1);
            box.Height.Is(4);
            Loop(2).ForEach(c => box[c, 0].Is($"{c},0"));
            Loop(2).ForEach(c => box[c, 1].Is(default(string)));
            Loop(2).ForEach(c => box[c, 2].Is($"{c},1"));
            Loop(2).ForEach(c => box[c, 3].Is($"{c},2"));

            box.FixedHeight = true;
            box.InsertRows(1, (c, r) => $"<{c},{r}>");
            box.Height.Is(4);
            Loop(2).ForEach(c => box[c, 0].Is($"{c},0"));
            Loop(2).ForEach(c => box[c, 1].Is($"<{c},1>"));
            Loop(2).ForEach(c => box[c, 2].Is(default(string)));
            Loop(2).ForEach(c => box[c, 3].Is($"{c},1"));
        }

        [TestMethod]
        public void ConstractionTests()
        {
            ConstractionTestsImpl(new BoxList<string>());
            ConstractionTestsImpl(new BoxArray<string>());
        }
        public void ConstractionTestsImpl(IBoxArray<string> box)
        {
            box.IsNotNull();
            box.Width.Is(0);
            box.Height.Is(0);
            box.IsEmpty().Is(true);

            box.Width = 1;      // サイズ(0,0)は許されない
            box.Width.Is(1);    // かならず(1,1)になる
            box.Height.Is(1);
            box.IsEmpty().Is(false);

            box.Width = 2;
            box.Height = 3;
            box.Width.Is(2);
            box.Height.Is(3);

            // 全要素にアクセスする確認
            for (var row = 0; row < box.Height; row++)
            {
                for (var col = 0; col < box.Width; col++)
                {
                    // データを代入して、３通りの方法で読み出してみる
                    var data = $"({col},{row})";
                    box[col, row] = data;

                    box[col, row].Is(data, "[,] + data");
                    box.Rows(col).ToList()[row].Is(data, "Rows" + data);
                    box.Cols(row).ToList()[col].Is(data, "Cols" + data);
                }
            }
        }

        [TestMethod]
        public void InitValueTests()
        {
            var box = new BoxList<string>();
            this.InitValue(box, 2, 3);
            box.Width.Is(2);
            box.Height.Is(3);
            for (var r = 0; r < box.Height; r++)
            {
                for (var c = 0; c < box.Width; c++)
                {
                    box[c, r].Is($"{c},{r}");
                }
            }
        }
        private void InitValue(IBoxArray<string> box, int width, int height)
        {
            box.Width = width;
            box.Height = height;

            Loop(box.Height).SelectMany(
                row => Loop(box.Width),
                (row, col) => box[col,row] = $"{col},{row}").ToList();
        }

        private IEnumerable<int> Loop(int count) => Enumerable.Range(0, count);

        public TestContext TestContext { get; set; }
    }
}
