using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JFactory.CsSrcLib.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.InitializeComponent();

            // スプリッターにマウス来たら赤くする。
            this.splitter1.MouseEnter += (se, ea) => this.splitter1.BackColor = Color.Red;
            this.splitter1.MouseLeave += (se, ea) => this.splitter1.BackColor = SystemColors.Control;

            // プロパティグリッドの設定
            this.propertyGrid1.SelectedObject = this;   // ここにコントロールを入れる

        }
    }
}
