// Partial Copyright (c) LogoUI Software Solutions LTD
// Autor: Vladislav Spivak
// This source file is the part of LogoFX Framework http://logofx.codeplex.com
// See accompanying licences and credits.

//same as Caliburn.Micro logging facilities http://caliburnmicro.codeplex.com

namespace System
{
    /// <summary>
    /// A logger.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the message as info.
        /// </summary>
        /// <param name="format">A formatted message.</param>
        /// <param name="args">Parameters to be injected into the formatted message.</param>
        void Info(string format, params object[] args);

        /// <summary>
        /// Logs the message as a warning.
        /// </summary>
        /// <param name="format">A formatted message.</param>
        /// <param name="args">Parameters to be injected into the formatted message.</param>
        void Warn(string format, params object[] args);

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void Error(Exception exception);
    }

    /// <summary>
    /// Used to manage logging.
    /// </summary>
    public static class Log
    {
        static readonly ILogger NullLogInstance = new NullLog();

        /// <summary>
        /// Creates an <see cref="ILogger"/> for the provided type.
        /// </summary>
        public static Func<Type, ILogger> GetLog = type => NullLogInstance;

        private class NullLog : ILogger
        {
            public void Info(string format, params object[] args) { }
            public void Warn(string format, params object[] args) { }
            public void Error(Exception exception) { }
        }
    }
}