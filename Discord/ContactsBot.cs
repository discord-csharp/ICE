﻿using System.Collections.Concurrent;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using ICE.Discord.Internal.Configuration;
using ICE.Discord.Internal.NLogTargets;
using NLog;

namespace ICE.Discord
{
    public class ContactsBot
    {
        DiscordSocketClient _client;
        DependencyMap _map;
        GlobalConfiguration _config;
        ConfigManager _cfgMgr;
        ConcurrentDictionary<IGuild, ServerConfiguration> _serverConfigurations;
        internal static Logger BotLogger { get; set; }
        private DiscordNLogTarget _discordNLogTarget;

        static ContactsBot()
        {
            BotLogger = LogManager.GetCurrentClassLogger();
        }

        public ContactsBot()
        {
            
        }

        public async Task RunBotAsync()
        {
            // Create the dependency map and inject the client and config into it
            _map = new DependencyMap();
            _cfgMgr = new ConfigManager("Configs");

            _map.Add(_cfgMgr);
            _map.Add(this);
            
            _config = await _cfgMgr.GetConfigAsync<GlobalConfiguration>("global");

            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                AudioMode = global::Discord.Audio.AudioMode.Disabled,
                LogLevel = LogSeverity.Debug,
                MessageCacheSize = 2000
            });

            _map.Add(_client);

            await _client.LoginAsync(TokenType.Bot, _config.Token);
            await _client.ConnectAsync();

            await _client.SetGameAsync(_config.Game);

            await Task.Delay(-1);
        }
    }
}
