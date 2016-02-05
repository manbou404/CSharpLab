using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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


            var obj = new Label();
            obj.Left = 1000;
            obj.Top = 1000;

            //fakeSpread1.Controls.Add(obj);
            //fakeSpread1.AutoScroll = true;

            this.propertyGrid1.SelectedObject = fakeSpread1;


            
        }

    }


}
