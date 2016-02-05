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
    public class BoxListTests
    {
        //[TestMethod]
        //publc void InsertTest()
        //{
        //    var ll = new List<int>();

        //    var obj = new BoxList<int>(5, 3);

        //    obj.InsertRow(3);
        //    obj.InsertCol(3, true);
        //}

        [TestMethod]
        public void ResizeTest()
        {
            var obj = new BoxList<int>();
            obj.MaxRows.Is(0);      // 作成直後は、サイズが０
            obj.MaxCols.Is(0);

            obj.MaxRows = 3;        // 行を増やした時、
            obj.MaxRows.Is(3);      
            obj.MaxCols.Is(1);      // 列が０だと、１に増やす

            obj.MaxRows = 0;        // 行を０にすると、
            obj.MaxRows.Is(0);      // 中身がからになる
            obj.MaxCols.Is(0);      

            obj.MaxCols = 3;        // 列も同様
            obj.MaxRows.Is(1);
            obj.MaxCols.Is(3);

            obj.MaxCols = 0;
            obj.MaxRows.Is(0);
            obj.MaxCols.Is(0);
        }

        [TestMethod]
        [TestCase(5, 3)]    // maxrow, maxcol
        [TestCase(1, 10)]
        [TestCase(10, 1)]
        public void ConstructionTest()
        {
            TestContext.Run((int maxrow, int maxcol) =>
            {
                var box = new BoxList<int>(maxrow, maxcol);
                box.MaxRows.Is(maxrow);
                box.MaxCols.Is(maxcol);

                // 全要素にアクセスする確認
                for (var row = 0; row < maxrow; row++)
                {
                    for (var col = 0; col < maxcol; col++)
                    {
                        // データを代入して、３通りの方法で読み出してみる
                        var data = row*0x1000 + col;
                        box[row, col] = data;
                        box[row, col].Is(data,"[,]");
                        box.Rows(col).ToList()[row].Is(data,"Rows");
                        box.Cols(row).ToList()[col].Is(data,"Cols");
                    }
                }
            });
        }

        [TestMethod]
        public void ConstructionTest2()
        {
            // インスタンス生成時の、例外テスト

            {
                var obj = new BoxList<int>(0, 0);
                obj.MaxRows.Is(0);
                obj.MaxCols.Is(0);
            }

            AssertEx.Throws<ArgumentOutOfRangeException>(() => 
            {
                var obj = new BoxList<int>(3, 0);
            })
            .ParamName.Is("newCols");

            AssertEx.Throws<ArgumentOutOfRangeException>(() => 
            {
                var obj = new BoxList<int>(0, 3);
            })
            .ParamName.Is("newRows");
        }

        public TestContext TestContext { get; set; }
    }
}
