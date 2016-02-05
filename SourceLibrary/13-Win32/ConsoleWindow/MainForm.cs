using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JFactory.CsSrcLib.Win32
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.InitializeComponent();

            this.openButton.Click += (se, ea) => ConsoleWindow.Open();
            this.open2Button.Click += (se, ea) => ConsoleWindow.Open(this.Handle);

            this.closeButton.Click += (se, ea) => ConsoleWindow.Close();

            this.printButton.Click += (se, ea) => Console.WriteLine(this.printTextBox.Text);

            this.isShowButton.Click += (se, ea) => MessageBox.Show(
                string.Format("ConsoleWindow.IsShow = [{0}]", ConsoleWindow.IsShow()));
        }
    }
}
