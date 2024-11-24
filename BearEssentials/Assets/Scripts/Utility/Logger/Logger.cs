using System;
using UnityEngine;

namespace Bears.Core
{
    public class Logger
    {
        private readonly string _name;
        private readonly Func<string, bool> _muteDelegate;
        private readonly Func<LogLevel> _verbosityDelegate;

        public Logger(string name, Func<string, bool> muteDelegate, Func<LogLevel> verbosityDelegate)
        {
            _name = name;
            _muteDelegate = muteDelegate;
            _verbosityDelegate = verbosityDelegate;
        }

        #region Main Log Functions

        /// <summary>
        /// Logs a message in the Unity Console
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="channel">Channel to log to</param>
        /// <param name="context">Unity Object that the message applies to</param>
        /// <param name="message">Message string</param>
        public void Msg(LogLevel level, string channel, string message, UnityEngine.Object context = null)
        {
            if (string.IsNullOrEmpty(channel))
            {
                channel = CommonChannels.General;
            }

            if (level == LogLevel.None || level > _verbosityDelegate() || _muteDelegate(channel))
            {
                return;
            }

            string msg = Log.Util.ChannelMessage(_name, channel, message);
            switch (level)
            {
                case LogLevel.Error:
                    if (context == null)
                    {
                        Debug.LogError(msg);
                        break;
                    }
                    Debug.LogError(msg, context);
                    break;
                case LogLevel.Warning:
                    if (context == null)
                    {
                        Debug.LogWarning(msg);
                        break;
                    }
                    Debug.LogWarning(msg, context);
                    break;
                default:
                    if (context == null)
                    {
                        Debug.Log(msg);
                        break;
                    }
                    Debug.Log(msg, context);
                    break;
            }
        }

        /// <summary>
        /// Logs an exception to the Unity console. Exceptions are logged to the error level.
        /// Exceptions are not logged to channels and will there be logged regardless of
        /// which channels are muted.
        /// </summary>
        /// <param name="context">Unity object that the exception applies to</param>
        /// <param name="exception">Exception to be logged</param>
        public void Exception(Exception exception, UnityEngine.Object context = null)
        {
            if (_verbosityDelegate() < LogLevel.Error)
            {
                return;
            }

            if (context != null)
            {
                Debug.LogException(exception, context);
            }
            else
            {
                Debug.LogException(exception);
            }
        }
        #endregion // main log functions

        #region Leveled overrides
        /// <summary>
        /// Logs a message in the Unity Console
        /// </summary>
        /// <param name="level">Log Level</param>
        /// <param name="message">>Message string</param>
        public void Msg(LogLevel level, string message)
        {
            Msg(level, channel:null, message);
        }
        #endregion // Leveled overrides

        #region Convenience overrides
        /// <summary>
        /// Logs a message in the Unity console at the normal level
        /// </summary>
        /// <param name="channel">Channel to log to</param>
        /// <param name="message">Message to log</param>
        public void Msg(string channel, string message)
        {
            Msg(LogLevel.Normal, channel, message);
        }

        /// <summary>
        /// Logs a message in the Unity console at the normal level
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Msg(string message)
        {
            Msg(LogLevel.Normal, channel:null, message);
        }
        
        /// <summary>
        /// Logs a message in the Unity console at the warning level
        /// </summary>
        /// <param name="channel">Channel to log to</param>
        /// <param name="message">Message to log</param>
        public void Warn(string channel, string message)
        {
            Msg(LogLevel.Warning, channel, message);
        }

        /// <summary>
        /// Logs a message in the Unity console at the warning level to the general channel
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Warn(string message)
        {
            Msg(LogLevel.Warning, channel: null, message);
        }

        /// <summary>
        /// Logs a message in the Unity console at the error level
        /// </summary>
        /// <param name="channel">Channel to log to</param>
        /// <param name="message">Message to log</param>
        public void Error(string channel, string message)
        {
            Msg(LogLevel.Error, channel, message);
        }

        /// <summary>
        /// Logs a message in the Unity console at the error level to the general channel
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Error(string message)
        {
            Msg(LogLevel.Error, channel:null, message);
        }
        #endregion // Convenience overrides
        

    }
}