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
    public enum Fruit
    {
        Apple,
        Banana,
        Orange,
        StrawberryA,
    }

    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.InitializeComponent();

            propertyGridWithControlSelector1.TargetControl = this;

        }
    }
}
