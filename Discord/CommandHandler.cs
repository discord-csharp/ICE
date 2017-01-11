using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NLog;

namespace Ice.Discord
{
    public class CommandHandler
    {
        private IDependencyMap _map;
        private DiscordSocketClient _client;
        private CommandService _commands;
        private ConcurrentDictionary<IGuild, GuildConfiguration> _serverConfigs;

        public CommandHandler(IDependencyMap map)
        {
            _map = map;
            _commands = new CommandService();
            map.Add(_commands);
            _serverConfigs = map.Get<ConcurrentDictionary<IGuild, GuildConfiguration>>();
            _commands.AddModulesAsync(Assembly.GetEntryAssembly()).Wait();
        }
        
        private async Task HandleCommandAsync(SocketMessage msg)
        {
            var message = msg as SocketUserMessage;
            if (message == null) return;
            if (!_serverConfigs.TryGetValue((message.Channel as IGuildChannel).Guild, out var config))
                return;

            int argPos = 0;

            if (message.HasStringPrefix(config.PrefixString, ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new ServerCommandContext(_client, message, config);

                // Provide the dependency map when executing commands
                var result = await _commands.ExecuteAsync(context, argPos, _map);
                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    await message.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
        }
    }
}
