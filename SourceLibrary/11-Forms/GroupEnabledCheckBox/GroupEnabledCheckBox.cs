/*  [GroupEnabledCheckBox]
 *
 *  Copyright (C) 2015 JFactory(manbou404)
 *
 *  This software is released under the MIT License.
 *  http://opensource.org/licenses/mit-license.php
 */

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace JFactory.CsSrcLib.Forms
{
    /// <summary>GroupBoxのEnabledをON/OFFするチェックボックス</summary>
    public class GroupEnabledCheckBox : CheckBox
    {
        private GroupBox _GroupBox;

        /// <summary>ドッキングするグループボックス</summary>
        public GroupBox GroupBox
        {
            [DebuggerStepThrough]
            get { return this._GroupBox; }
            set
            {
                if (this._GroupBox != value)
                {
                    this.Detach();          
                    this._GroupBox = value;
                    this.Attach();          
                    this.Adjust();
                }
            }
        }

        /// <summary>位置が変わったとき、親に合わせる</summary>
        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);

            this.Adjust();
        }

        /// <summary>チェックが変わったとき、親のEnabledを同期させる</summary>
        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);

            this.Adjust();
        }

        /// <summary>位置とチェックの同期</summary>
        private void Adjust()
        {
            if (this.GroupBox == null)
            {
                return;
            }

            this.Location = this.GroupBox.Location;
            this.GroupBox.Enabled = this.Checked;
        }

        /// <summary>接続（イベントハンドラの登録など</summary>
        private void Attach()
        {
            if (this.GroupBox == null)
            {
                return;
            }

            // 手前に表示
            this.BringToFront();    
            
            // GroupBoxのテキストを使わず、CheckBoxのテキストで表示する
            this.Text = this.GroupBox.Text;
            this.GroupBox.Text = string.Empty;

            // 状態の同期
            this.Checked = this.GroupBox.Enabled;

            this.GroupBox.LocationChanged += GroupBox_LocationChanged;
            this.GroupBox.TextChanged += GroupBox_TextChanged;
            this.GroupBox.EnabledChanged += GroupBox_EnabledChanged;
        }

        /// <summary>切断(イベントハンドラの切り離し、親の状態復元など)</summary>
        private void Detach()
        {
            if (this.GroupBox == null)
            {
                return;
            }

            this.GroupBox.LocationChanged -= GroupBox_LocationChanged;
            this.GroupBox.TextChanged -= GroupBox_TextChanged;
            this.GroupBox.EnabledChanged -= GroupBox_EnabledChanged;

            this.GroupBox.Text = this.Text;
        }

        private void GroupBox_LocationChanged(object sender, EventArgs e)
        {
            this.Adjust();
        }

        private void GroupBox_TextChanged(object sender, EventArgs e)
        {
            this.Text = this.GroupBox.Text;
            this.GroupBox.Text = string.Empty;
        }

        private void GroupBox_EnabledChanged(object sender, EventArgs e)
        {
            this.Checked = this.GroupBox.Enabled;
        }

    }
}
