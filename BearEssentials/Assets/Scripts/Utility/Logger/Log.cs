using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Bears.Core
{
    public enum LogLevel
    {
        None = 0,
        Error = 1,
        Warning = 2,
        Normal = 3,
        Verbose = 4,
        VeryVerbose = 5
    }
    
    [PublicAPI]
	public static class Log
    {
        public static class Util
        {
            public static string ChannelMessage(string loggerName, string channel, string message) => $"<b>[{loggerName}.{channel}]</b> {message}";
        }
        
        public static LogLevel Verbosity { get; set; } = LogLevel.Normal;
        private static LogLevel GetVerbosity() => Verbosity;
        
        private static readonly HashSet<string> _MutedChannels = new HashSet<string>();
        
        public static readonly Logger Core = new Logger("Core", IsChannelMuted, GetVerbosity);
        public static readonly Logger Shared = new Logger("Shared", IsChannelMuted, GetVerbosity);
        public static readonly Logger Unified = new Logger("Unified", IsChannelMuted, GetVerbosity);
        private static readonly Logger _Game = new Logger("Game", IsChannelMuted, GetVerbosity);

        #region Main Log Functions

        /// <summary>
        /// Logs a message in the Unity Console
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="channel">Channel to log to</param>
        /// <param name="context">Unity Object that the message applies to</param>
        /// <param name="message">Message string</param>
        public static void Msg(LogLevel level, string channel, string message, UnityEngine.Object context) =>
            _Game.Msg(level, channel, message, context);
        

        /// <summary>
        /// Logs an exception to the Unity console. Exceptions are logged to the error level.
        /// Exceptions are not logged to channels and will there be logged regardless of
        /// which channels are muted.
        /// </summary>
        /// <param name="context">Unity object that the exception applies to</param>
        /// <param name="exception">Exception to be logged</param>
        public static void Exception(Exception exception, UnityEngine.Object context = null) =>
            _Game.Exception(exception, context);
        
        #endregion // main log functions

        #region Leveled overrides
        /// <summary>
        /// Logs a message in the Unity Console
        /// </summary>
        /// <param name="level">Log Level</param>
        /// <param name="message">>Message string</param>
        public static void Msg(LogLevel level, string message) => _Game.Msg(level, message);

        /// <summary>
        /// Logs a message in the Unity Console
        /// </summary>
        /// <param name="level">Log Level</param>
        /// <param name="channel">Channel to log to</param>
        /// <param name="message">>Message string</param>
        public static void Msg(LogLevel level, string channel, string message) => _Game.Msg(level, channel, message);

        #endregion // Leveled overrides

        #region Convenience overrides

        /// <summary>
        /// Logs a message in the Unity console at the normal level
        /// </summary>
        /// <param name="channel">Channel to log to</param>
        /// <param name="message">Message to log</param>
        public static void Msg(string channel, string message) => _Game.Msg(channel, message);

        /// <summary>
        /// Logs a message in the Unity console at the normal level
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Msg(string message) => _Game.Msg(message);


        /// <summary>
        /// Logs a message in the Unity console at the warning level
        /// </summary>
        /// <param name="channel">Channel to log to</param>
        /// <param name="message">Message to log</param>
        public static void Warn(string channel, string message) => _Game.Warn(channel, message);

        /// <summary>
        /// Logs a message in the Unity console at the warning level to the general channel
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Warn(string message) => _Game.Warn(message);

        /// <summary>
        /// Logs a message in the Unity console at the error level
        /// </summary>
        /// <param name="channel">Channel to log to</param>
        /// <param name="message">Message to log</param>
        public static void Error(string channel, string message) => _Game.Error(channel, message);

        /// <summary>
        /// Logs a message in the Unity console at the error level to the general channel
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Error(string message) => _Game.Error(message);
        #endregion // Convenience overrides

        #region Channels
        /// <summary>
        /// Queries muted channels
        /// </summary>
        /// <param name="channel">Channel name</param>
        /// <returns>True if the channel is muted</returns>
        public static bool IsChannelMuted(string channel) => _MutedChannels.Contains(channel);

        /// <summary>
        /// Mutes a channels so that any messages logged to that channel will not be displayed in the Unity console.
        /// </summary>
        /// <param name="channel">Channel name</param>
        public static void MuteChannel(string channel)
        {
            if (string.IsNullOrEmpty(channel))
            {
                throw new ArgumentNullException();
            }
            
            _MutedChannels.Add(channel);
        }
        
        /// <summary>
        /// Un-mutes a channels so that any messages logged to that channel will be displayed to the Unity console.
        /// If the channel was not previously muted this has no affect
        /// </summary>
        /// <param name="channel">Channel name</param>
        public static void UnmuteChannel(string channel) => _MutedChannels.Remove(channel);

        /// <summary>
        /// Un-mutes all muted channels
        /// </summary>
        public static void UnmuteAll() => _MutedChannels.Clear();

        #endregion // Channels
    }
}
