using System.Collections.Concurrent;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NLog;
using Ice.Discord.Internal.Configuration;
using System;

namespace Ice.Discord
{
    public class IceBot
    {
        DiscordSocketClient _client;
        DependencyMap _map;
        GlobalConfiguration _config;
        CommandHandler _commands;
        ConcurrentDictionary<IGuild, GuildConfiguration> _serverConfigurations;

        public async Task RunBotAsync()
        {
            try
            {
                // Create the dependency map and inject the client and config into it
                _map = new DependencyMap();
                _config = await new ConfigManager("Configs").GetConfigAsync<GlobalConfiguration>("global");
                _serverConfigurations = new ConcurrentDictionary<IGuild, GuildConfiguration>();
                _client = new DiscordSocketClient(new DiscordSocketConfig
                {
                    AudioMode = global::Discord.Audio.AudioMode.Disabled,
                    LogLevel = LogSeverity.Debug,
                    MessageCacheSize = 2000,
                    DefaultRetryMode = RetryMode.AlwaysRetry
                });

                _map.Add(this);
                _map.Add(_client);
                _map.Add(_serverConfigurations);

                _commands = new CommandHandler(_map);

                await _client.LoginAsync(TokenType.Bot, _config.Token);
                await _client.ConnectAsync();

                await _client.SetGameAsync(_config.Game);

                await Task.Delay(-1);
            }
            catch(Exception ex)
            {
                // todo log exceptions
            }
        }
    }
}
