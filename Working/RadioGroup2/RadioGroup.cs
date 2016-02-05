/*  [RadioGroup]
 *
 *  Copyright (C) 2015 JFactory(manbou404)
 *
 *  This software is released under the MIT License.
 *  http://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JFactory.CsSrcLib.Forms
{
    public class RadioGroup : Component
    {
        protected Control _Parent;

        protected List<RadioButton> RadioButtons;

        /// <summary>RadioGroupを初期化する</summary>
        public RadioGroup()
        {
            this.RadioButtons = new List<RadioButton>();
        }

        /// <summary>寄生する親Control</summary>
        [DefaultValue(null)]
        public Control Parent
        {
            get { return this._Parent; }
            set
            {
                if (this._Parent != value)
                {
                    this.Detach();
                    this._Parent = value;
                    this.Attach();
                    this.Layout();
                }
            }
        }

        public event EventHandler ValueChanged;

        [DefaultValue(null)]
        public virtual string[] Names
        {
            get
            {
                return RadioButtons.Select(x => x.Text).ToArray();
            }
            set
            {
                this.RemoveRadioButton();

                //this.RadioButtons = value.Select((x, i) => 
                //                new RadioButton() { Text = x, Tag = i } ).ToList();
                foreach (var v in value.Select((x, i) => new { x, i }))
                {
                    var obj = new RadioButton();
                    obj.Text = v.x;
                    obj.Tag = v.i;
                    obj.Click += Obj_Click;
                    this.RadioButtons.Add(obj);
                }

                this.Layout();
            }
        }

        protected void Obj_Click(object sender, EventArgs e)
        {
            this.ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>Check要素(n番目)の設定/取得</summary>
        [DefaultValue(0)]
        public int Value
        {
            get { return 0; }
            set
            {

            }
        }

        /// <summary>Invaridates this instance.</summary>
        protected virtual void Layout()
        {
            if (this.Parent == null || RadioButtons.Count == 0)
            {
                return;
            }

            var rect = this.Parent.DisplayRectangle;
            var height = rect.Height - RadioButtons[0].Height;

            for (int i = 0; i < RadioButtons.Count; i++)
            {
                //RadioButtons[i].BackColor = System.Drawing.Color.Red;
                RadioButtons[i].Left = rect.Left + this.Parent.Padding.Left;
                RadioButtons[i].Top = rect.Top + (height / (RadioButtons.Count - 1)) * i;

                if (this.Parent.Controls.Contains(RadioButtons[i]) == false)
                {
                    this.Parent.Controls.Add(RadioButtons[i]);
                }
            }
        }

        [DefaultValue(typeof(Padding), "3; 3; 3; 3")]
        public Padding Margin { get; set; }

        [DefaultValue(0)]
        public int Rows { get; set; } = 0;

        [DefaultValue(0)]
        public int Cols { get; set; } = 0;

        /// <summary>親コントロールからRadioButtonを削除する</summary>
        private void RemoveRadioButton()
        {
            if (this.Parent != null && this.RadioButtons.Count != 0)
            {
                foreach (var x in this.RadioButtons)
                {
                    this.Parent.Controls.Remove(x);
                    x.Click -= Obj_Click;
                }

                this.RadioButtons.Clear();
            }
        }

        protected virtual void Attach()
        {
        }

        protected virtual void Detach()
        {
        }
    }

#if false
    public class RadioGroupX<T> : Component where T : struct 
    {
        private Control _Parent;

        private List<RadioButton> list;

        public RadioGroupX()
        {
            if (typeof(T).BaseType != typeof(System.Enum))
            {
                throw new NotSupportedException("T is only enum.");
            }

            list = Enum.GetNames(typeof(T))
                .Select(x => new RadioButton()
                    { Text = x, Tag = Enum.Parse(typeof(T), x) }).ToList();

            
        }

        public virtual string GetName(T t)
        {
            return "";
        }

        /// <summary>寄生する親Control</summary>
        public Control Parent
        {
            get { return this._Parent; }
            set
            {
                if (this._Parent != value)
                {
                    this.Detach();
                    this._Parent = value;
                    this.Attach();
                }
            }
        }

        public T Value
        {
            get
            {
                return (T)list[0].Tag;
            }
            set
            {
                var z = list.FirstOrDefault(x => value.Equals(x.Tag));
                z.Checked = true;
            }
        }



        /// <summary>Attaches this instance.</summary>
        private void Attach()
        {
            var rect = this.Parent.DisplayRectangle;
            var height = rect.Height - list[0].Height;

            for (int i = 0; i < list.Count; i++)
            {
                list[i].BackColor = System.Drawing.Color.Red;
                list[i].Left = rect.Left + this.Parent.Padding.Left;
                list[i].Top = rect.Top + (height / (list.Count-1)) * i;
            }

            this.Parent.Controls.AddRange(list.ToArray());

            //var obj = new Button();
            //obj.Left = this.Parent.DisplayRectangle.Left;
            //obj.Top = this.Parent.DisplayRectangle.Top;
            //obj.Width = this.Parent.DisplayRectangle.Width;
            //obj.Height = this.Parent.DisplayRectangle.Height;
            //this.Parent.Controls.Add(obj);

        }

        /// <summary>Detaches this instance.</summary>
        private void Detach()
        {
            // Controlsから取り除く

        }
    }
#endif
}
