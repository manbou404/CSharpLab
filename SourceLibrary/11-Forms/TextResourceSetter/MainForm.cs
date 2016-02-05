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
        private Dummy dummy;

        public MainForm()
        {
            this.InitializeComponent();
            dummy = new Dummy();

            var logger = new Library.FakeLogger();
            //logger.Level = Library.FakeLogger.LogLevel.Debug;

            TextResourceSetter.Logger = logger;
            TextResourceSetter.Execute(this);

        }
    }

    public class Dummy
    {
        public string Text { get; }
    }

}
