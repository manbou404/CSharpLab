using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFactory.CsSrcLib.Forms
{
    public partial class SimpleGrid
    {


        protected class DataStrage :  JFactory.CsSrcLib.Library.BoxList<ICell>
        {
            private List<LineInfo> _RowsInfo = new List<LineInfo>();
            private List<LineInfo> _ColsInfo = new List<LineInfo>();

            public LineInfo RowsInfo(int index)
            {
                return _RowsInfo[index];
            }

            public LineInfo ColsInfo(int index)
            {
                return _ColsInfo[index];
            }

            public Size GetMinSize()
            {
                return new Size(_ColsInfo.Sum(x => x.Size) + 1,
                                _RowsInfo.Sum(x => x.Size) + 1);
            }
            
            //
            // BoxListに合わせて、RowsInfo, ColsInfoも伸縮させる
            //

            public override void AppendRows(int diff)
            {
                base.AppendRows(diff);
                Enumerable.Repeat(0, diff).ToList()
                    .ForEach(_ => this._RowsInfo.Add(new LineInfo() { Size = 20 }));
            }
            public override void RemoveRows(int diff)
            {
                base.RemoveRows(diff);
                _RowsInfo.RemoveRange(this.MaxCols - diff, diff);
            }
            public override void AppendCols(int diff)
            {
                base.AppendCols(diff);
                Enumerable.Repeat(0, diff).ToList()
                    .ForEach(_ => this._ColsInfo.Add(new LineInfo() { Size = 50 }));
            }
            public override void RemoveCols(int diff)  
            {
                base.RemoveCols(diff);
                _ColsInfo.RemoveRange(this.MaxCols - diff, diff);
            }
        }
    }
}
