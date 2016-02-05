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
        private Point firstPos;

        public MainForm()
        {
            InitializeComponent();
            this.propertyGridWithControlSelector1.SelectedItem = this.groupEnabledCheckBox1;
            this.firstPos = groupEnabledCheckBox1.Location;

            this.attachButton.Click += (_, __) => 
                    groupEnabledCheckBox1.GroupBox = groupBox1;
            this.detachButton.Click += (_, __) => {
                if (groupEnabledCheckBox1.GroupBox != null) {
                    groupEnabledCheckBox1.GroupBox = null;
                    groupEnabledCheckBox1.Location = firstPos;
                }
            };
        }
    }
}
