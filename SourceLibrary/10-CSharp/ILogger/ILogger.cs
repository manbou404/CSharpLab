/*  [ILogger]
 *
 *  Copyright (C) 2015 JFactory(manbou404)
 *
 *  This software is released under the MIT License.
 *  http://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFactory.CsSrcLib.Library
{
    public interface ILogger
    {
        /// <summary>Fatal: Highest level: important stuff down.</summary>
        void Fatal(string message);

        /// <summary>Error: For example application crashes / exceptions.</summary>
        void Error(string message);

        /// <summary>Warn:  Incorrect behavior but the application can continue.</summary>
        void Warn(string message);

        /// <summary>Info:  Normal behavior like mail sent, user updated profile etc.</summary>
        void Info(string message);

        /// <summary>Debug: Executed queries, user authenticated, session expired.</summary>
        void Debug(string message);

        /// <summary>Trace: Begin method X, end method X etc.</summary>
        void Trace(string message);
    }
}
