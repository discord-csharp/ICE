using System;
using NLog;
using NLog.Config;

namespace Ice.Discord
{
    static class Program
    {
        static Logger ProgramLogger { get; } = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            LogManager.ThrowConfigExceptions = true;
            LogManager.Configuration = new XmlLoggingConfiguration("NLog.config");
            LogManager.EnableLogging();
            try
            {
                new IceBot().RunBotAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                ProgramLogger.Error(ex);
            }
            Console.WriteLine("Bot Teriminated.");
            Console.ReadLine();
        }
    }
}