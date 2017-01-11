using System;
using Ice.Discord.Internal.Configuration;
using Ice.Discord.Internal.Data;
using NLog;
using NLog.Targets;

namespace Ice.Discord.Internal.NLogTargets
{
    /// <summary>
    /// An Internally Developed NLog Target that uses Entity Frameworks for
    /// Logging datas
    /// </summary>
    [Target("PSQLEntityFramework")]
    public sealed class PSQLEntityFrameworkNLogTarget : TargetWithLayout
    {
        private PostgreSQLConfiguration DbConfig { get; set; }
        private static Logger PSQLEntityLogger { get; } = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Attempts to retrieve PostgreSQL configuration information
        /// independently from IceBot instance.
        /// </summary>
        public PSQLEntityFrameworkNLogTarget()
        {
            var config = new ConfigManager("Configs");
            try
            {
                DbConfig = config.GetConfigAsync<PostgreSQLConfiguration>("default").GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                PSQLEntityLogger.Error(ex, ex.Message);
            }
        }

        protected override void Write(LogEventInfo info)
        {
            using (var context = new ContactsBotDbContext())
            {
                context.Logs.Add(new Log
                {
                    Level = info.Level.Name,
                    Exception = info.Exception?.ToString(),
                    Message = Layout.Render(info),
                    Timestamp = info.TimeStamp.ToUniversalTime()
                });
                context.SaveChanges();
            }
        }
    }
}
