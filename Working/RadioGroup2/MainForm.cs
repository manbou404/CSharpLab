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
            propertyGridWithControlSelector1.SelectedItem = this.groupBox1;

            this.radioGroup1.Names = new[] { "Apple", "Banana", "Orange", "Strawberry" };
            this.radioGroup1.ValueChanged += (_, __) => MessageBox.Show("RadioGroup");

            this.fruitRadioGroup1.ValueChanged += (_, __) => MessageBox.Show("FruitRadioGroup");
        }
    }

    public class FruitRadioGroup : EnumRadioGroup<Fruit>
    {
    }
}
