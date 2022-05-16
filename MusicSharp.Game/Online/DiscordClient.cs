using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using MusicSharp.Game.Commands;
using MusicSharp.Game.Configuration;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Logging;

namespace MusicSharp.Game.Online
{
    public class DiscordClient : Component
    {
        private DiscordSocketClient client;

        private Bindable<string> token;

        private Thread discordThread;

        public event Action OnClientReady;
        public event Action OnClientLoggedOut;
        public event Action<LogMessage> OnClientLogReceived;
        public event Action<SocketSlashCommand> OnSlashCommandExecuted;

        public BindableBool IsRunning { get; private set; }

        public SocketSelfUser User => client?.CurrentUser;

        private CommandStore commands;

        public DiscordClient()
        {
            discordThread = createDiscordClientThread();
            IsRunning = new BindableBool(false);
            commands = new CommandStore();
        }

        [BackgroundDependencyLoader]
        private void load(MusicSharpConfigManager localConfig)
        {
            token = localConfig.GetBindable<string>(MusicSharpSetting.Token);
        }

        private Thread createDiscordClientThread()
        {
            return new Thread(() =>
                {
                    try
                    {
                        botMain().GetAwaiter().GetResult();
                    }
                    catch
                    {
                        Logger.Log("Discord client thread was interrupted.");
                    }
                }
            );
        }

        public void Start()
        {
            if (discordThread.ThreadState == ThreadState.Unstarted)
            {
                discordThread.Start();
                IsRunning.Value = true;
            }
        }

        public async Task Stop()
        {
            if (discordThread.ThreadState != ThreadState.WaitSleepJoin)
                return;

            await client.LogoutAsync();
            discordThread?.Interrupt();
            discordThread = createDiscordClientThread();
            IsRunning.Value = false;
        }

        private async Task botMain()
        {
            client = new DiscordSocketClient();
            client.Ready += onClientReady;
            client.LoggedOut += onClientLoggedOut;
            client.Log += onClientLogReceived;
            client.SlashCommandExecuted += handleSlashCommand;

            await client.LoginAsync(TokenType.Bot, token.Value);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task onClientReady()
        {
            var appCommandProperties = new List<ApplicationCommandProperties>();

            foreach (var command in commands.GetCommands())
            {
                var buildedCommand = command.Build();
                appCommandProperties.Add(buildedCommand);

                if (command.IsGlobalAppCommand)
                {
                    await client.CreateGlobalApplicationCommandAsync(buildedCommand);
                }
                else
                {
                    foreach (var guildId in command.SpecificGuilds)
                    {
                        var guild = client.GetGuild(guildId);
                        await guild.CreateApplicationCommandAsync(buildedCommand);
                    }
                }    
            }

            await client.BulkOverwriteGlobalApplicationCommandsAsync(appCommandProperties.ToArray());
            OnClientReady?.Invoke();
        }

        private Task onClientLoggedOut()
        {
            OnClientLoggedOut?.Invoke();

            return Task.CompletedTask;
        }

        private async Task handleSlashCommand(SocketSlashCommand command)
        {
            var cmd = commands.GetCommands().FirstOrDefault(c => c.Name == command.CommandName);

            if (cmd != null)
                await cmd.ExecutedCommand(command);

            Logger.Log($"Used {command.User.Username}#{command.User.Discriminator} /{command.CommandName}");
            OnSlashCommandExecuted?.Invoke(command);
        }

        private Task onClientLogReceived(LogMessage log)
        {
            if (string.IsNullOrEmpty(log.Message))
                return Task.CompletedTask;

            OnClientLogReceived?.Invoke(log);
            Logger.Log($"[{log.Source}]: {log.Message}");

            if (log.Exception != null)
                Logger.Log(log.Exception.ToString());

            return Task.CompletedTask;
        }
    }
}
