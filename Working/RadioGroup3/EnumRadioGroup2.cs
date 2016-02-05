using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JFactory.CsSrcLib.Forms
{
    /// <summary>RadioGroupのEnum版。こんな感じに継承して使う<br/>
    /// public class EnumNameRadioGroup : EnumRadioGroup&lt;EnumName&gt;{} </summary>
    /// <typeparam name="T">Enum限定(NotSupportedException例外)</typeparam>
    public class EnumRadioGroup2<T> : RadioGroup2
    {
        /// <summary>EnumRadioGroupを初期化する</summary>
        /// <exception cref="NotSupportedException">T is only enum.</exception>
        public EnumRadioGroup2()
        {
            if (typeof(T).BaseType != typeof(System.Enum))
            {
                throw new NotSupportedException("T is only enum.");
            }

            //this.RadioButtons = Enum.GetNames(typeof(T))
            //    .Select(x => new RadioButton() { Text = x, Tag = Enum.Parse(typeof(T), x) }).ToList();
        } 

        public override string[] Names
        {
            get
            {
                return base.Names;
            }

            set
            {
                base.Names = value;
            }
        }

        public T EnumValue
        {
            //get { return 0; }
            set { }
        }
    }
}
