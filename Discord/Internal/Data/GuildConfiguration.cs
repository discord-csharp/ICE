﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Ice.Discord.Internal.Data
{
    [Table("GuildConfigurations")]
    public class GuildConfiguration
    {
        public ulong GuildId { get; set; }

        public string PrefixString { get; set; } = "~";

        public bool EnableLoggingChannel { get; set; } = false;

        public ulong LoggingChannelID { get; set; } = 0;

        public string LoggingNLogLayout { get; set; } = "${time} | **${level}** - ${message}";
    }
}
