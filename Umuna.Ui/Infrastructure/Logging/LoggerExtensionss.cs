using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace Umuna.Ui.Infrastructure.Logging
{
    public static class LoggerExtensions
    {
        public static void Trace(this ILogger logger, string messageTemplate, params object?[] args)
        {
#pragma warning disable CA1873
            logger.LogTrace(messageTemplate, args);
#pragma warning restore CA1873
        }

        // Trace level with caller info
        public static void LogTraceWithCaller(
            this ILogger logger,
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (!logger.IsEnabled(LogLevel.Trace))
            {
                return;
            }
            logger.LogTrace("[{MemberName}] {SourceFile}:line {LineNumber} - {Message}",
                memberName, System.IO.Path.GetFileName(sourceFilePath), sourceLineNumber, message);
        }

        public static void Debug(this ILogger logger, string messageTemplate, params object?[] args)
        {
#pragma warning disable CA1873
            logger.LogDebug(messageTemplate, args);
#pragma warning restore CA1873
        }

        // Debug level with caller info
        public static void LogDebugWithCaller(
            this ILogger logger,
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (!logger.IsEnabled(LogLevel.Debug))
            {
                return;
            }
            logger.LogDebug("[{MemberName}] {SourceFile}:line {LineNumber} - {Message}",
                memberName, System.IO.Path.GetFileName(sourceFilePath), sourceLineNumber, message);
        }

        public static void Info(this ILogger logger, string messageTemplate, params object?[] args)
        {
#pragma warning disable CA1873
            logger.LogInformation(messageTemplate, args);
#pragma warning restore CA1873
        }

        // Information level with caller info
        public static void LogInfoWithCaller(
            this ILogger logger,
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (!logger.IsEnabled(LogLevel.Information))
            {
                return;
            }
            logger.LogInformation("[{MemberName}] {SourceFile}:line {LineNumber} - {Message}",
                memberName, System.IO.Path.GetFileName(sourceFilePath), sourceLineNumber, message);
        }

        public static void Warning(this ILogger logger, string messageTemplate, params object?[] args)
        {
#pragma warning disable CA1873
            logger.LogWarning(messageTemplate, args);
#pragma warning restore CA1873
        }

        // Warning level with caller info
        public static void LogWarningWithCaller(
            this ILogger logger,
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (!logger.IsEnabled(LogLevel.Warning))
            {
                return;
            }
            logger.LogWarning("[{MemberName}] {SourceFile}:line {LineNumber} - {Message}",
                memberName, System.IO.Path.GetFileName(sourceFilePath), sourceLineNumber, message);
        }

        public static void Error(this ILogger logger, string messageTemplate, params object?[] args)
        {
#pragma warning disable CA1873
            logger.LogError(messageTemplate, args);
#pragma warning restore CA1873
        }

        // Error level with caller info
        public static void LogErrorWithCaller(
            this ILogger logger,
            Exception exception,
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (!logger.IsEnabled(LogLevel.Error))
            {
                return;
            }
            logger.LogError(exception, "[{MemberName}] {SourceFile}:line {LineNumber} - {Message}",
                memberName, System.IO.Path.GetFileName(sourceFilePath), sourceLineNumber, message);
        }

        // Critical level with caller info
        public static void LogCriticalWithCaller(
            this ILogger logger,
            object? sender,
            Exception exception,
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (!logger.IsEnabled(LogLevel.Critical))
            {
                return;
            }
            logger.LogCritical(exception, "[{MemberName}] {SourceFile}:line {LineNumber} - {Message} (Sender: {Sender})",
                memberName, System.IO.Path.GetFileName(sourceFilePath), sourceLineNumber, message, sender?.GetType().FullName);
        }
    }
}