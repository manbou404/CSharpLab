/*  [PropertyGridHelper,PropertyGridWithControlSelector]
 *
 *  Copyright (C) 2015 JFactory(manbou404)
 *
 *  This software is released under the MIT License.
 *  http://opensource.org/licenses/mit-license.php
 */

using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace JFactory.CsSrcLib.Forms
{
    public static class PropertyGridHelper
    {
        /// <summary>PropertyGridのToolStripを返す拡張メソッド</summary>
        public static ToolStrip GetToolStrip(this PropertyGrid me)
        {
            return me.Controls.OfType<ToolStrip>().FirstOrDefault();
        }
    }

    /// <summary>コントロールセレクタ(ComboBox)付きのPropertyGrid</summary>
    public class PropertyGridWithControlSelector : PropertyGrid
    {
        private Control _TargetControl;

        private ToolStripComboBox selectorComboBox;

        /// <summary>PropertyGridWithControlSelectorの初期化</summary>
        public PropertyGridWithControlSelector()
        {
            // ToolStripにSelector用のComboBoxを追加
            selectorComboBox = new ToolStripComboBox();
            selectorComboBox.SelectedIndexChanged += Ctrl_SelectedIndexChanged;
            selectorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            //selectorComboBox.Width = 181;     // 変わらない・・・

            var ts = this.GetToolStrip();
            ts.Items.RemoveAt(2);               // 非表示の謎ボタン
            ts.Items.RemoveAt(3);               // プロパティボタン
            ts.Items.Add(selectorComboBox);
        }

        /// <summary>対象となるControl</summary>
        public Control TargetControl
        {
            [DebuggerStepThrough]
            get { return this._TargetControl; }
            set
            {
                // Detach
                if (this._TargetControl != null)
                {
                    this._TargetControl.ControlAdded -= _TargetControl_ControlAdded;
                    this.selectorComboBox.Items.Clear();
                }
                // Attach
                if (value != null)
                {
                    this._TargetControl = value;
                    this._TargetControl.ControlAdded += _TargetControl_ControlAdded;
                    this.AddSelectorCombo(value);
                }
            }
        }

        /// <summary>選択されたアイテムの設定/取得</summary>
        public Control SelectedItem
        {
            get { return this.SelectedObject as Control; }
            set
            {
                // valueをitemsから探す
                selectorComboBox.SelectedItem 
                    = selectorComboBox.Items.Cast<CustomComboItem>()
                                      .FirstOrDefault(x => x.Contlol == value);
            }
        }

        /// <summary>イベントハンドラ：TargetにControlが追加された</summary>
        /// <param name="sender">イベントソース</param><param name="e">イベントデータ</param>
        private void _TargetControl_ControlAdded(object sender, ControlEventArgs e)
        {
            var control = sender as Control;
            if (control != null)
            {
                this.AddSelectorCombo(control);
            }
        }

        /// <summary>SelectorComboにコントロールを追加</summary>
        /// <param name="control">The control.</param>
        private void AddSelectorCombo(Control control)
        {
            this.selectorComboBox.Items.Clear();

            // NameのあるControlのみ対象
            foreach (var x in control.Controls.Cast<Control>()
                                              .Where(x => !string.IsNullOrWhiteSpace(x.Name)))
            {
                this.selectorComboBox.Items.Add(new CustomComboItem() { Contlol = x });
            }
        }

        /// <summary>イベントハンドラ：SelectorComboが変更された</summary>
        /// <param name="sender">イベントソース</param><param name="e">イベントデータ</param>
        private void Ctrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combo = sender as ToolStripComboBox;
            if (combo != null && this.TargetControl != null)
            {
                this.SelectedObject = (combo.SelectedItem as CustomComboItem)?.Contlol;
            }
        }

        /// <summary>ComboBoxのCustomItem</summary>
        private class CustomComboItem
        {
            public Control Contlol { get; set; }

            public override string ToString()
            {
                return this.Contlol.Name;
            }
        }
    }
}
