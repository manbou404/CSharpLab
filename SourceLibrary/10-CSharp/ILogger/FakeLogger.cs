using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace JFactory.CsSrcLib.Library
{
    public class FakeLogger : ILogger
    {
        public enum LogLevel { Trace, Debug, Info, Warn, Error, Fatal, }
        public LogLevel Level { get; set; }

        public void Fatal(string message)
        {
            if (this.Level <= LogLevel.Fatal)
            {
                System.Diagnostics.Debug.WriteLine("FATAL: " + message);
            }
        }

        public void Error(string message)
        {
            if (this.Level <= LogLevel.Error)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + message);
            }
        }

        public void Warn(string message)
        {
            if (this.Level <= LogLevel.Warn)
            {
                System.Diagnostics.Debug.WriteLine("WARN : " + message);
            }
        }

        public void Info(string message)
        {
            if (this.Level <= LogLevel.Info)
            {
                System.Diagnostics.Debug.WriteLine("INFO : " + message);
            }
        }

        public void Debug(string message)
        {
            if (this.Level <= LogLevel.Debug)
            {
                System.Diagnostics.Debug.WriteLine("DEBUG: " + message);
            }
        }

        public void Trace(string message)
        {
            if (this.Level <= LogLevel.Trace)
            {
                System.Diagnostics.Debug.WriteLine("TRACE: " + message);
            }
        }
    }
}
