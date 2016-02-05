using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JFactory.CsSrcLib.Library
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.InitializeComponent();

            TimestampButton.Click += TimestampButton_Click;
        }

        private void TimestampButton_Click(object sender, EventArgs e)
        {

            var filePath = this.GetFileName();
            var fileName = Path.GetFileName(filePath);
            try
            {
                var asm = Assembly.LoadFrom(filePath);
                var timestamp = ReflectionUtils.GetTimeStampBuilding(asm);
                MessageBox.Show(
                    string.Format("{0}は、{1}に作られた", fileName, timestamp.ToString()));
            }
            catch(BadImageFormatException)
            {
                MessageBox.Show(string.Format("{0}は有効なアセンブリではない", fileName));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetFileName()
        {
            var filePath = string.Empty;
            using (var dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    return dlg.FileName;
                }
            }

            return string.Empty;
        }
    }
}
