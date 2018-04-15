using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M4_Console_UI.Interfaces
{
    /// <summary>
    /// interface which is used for logging messages
    /// </summary>
    public interface ILogger
    {
        #region methods
        /// <summary>
        /// write information about regular actions
        /// </summary>
        /// <param name="message">message</param>
        void Debug(string message);

        /// <summary>
        /// write information about single operation
        /// </summary>
        /// <param name="message">message</param>
        void Info(string message);

        /// <summary>
        /// write fatal error
        /// </summary>
        /// <param name="message">message</param>
        void Fatal(string message);

        /// <summary>
        /// write warnings
        /// </summary>
        /// <param name="message">message</param>
        void Warn(string message);

        /// <summary>
        /// write all information
        /// </summary>
        /// <param name="message">message</param>
        void Trace(string message);

        /// <summary>
        /// write error
        /// </summary>
        /// <param name="message">message</param>
        void Error(string message);
        #endregion
    }
}
