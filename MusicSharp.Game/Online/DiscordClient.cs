using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
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
        public event Action<string> OnClientLogReceived;
        public event Action<string> OnMessageReceived;

        public DiscordClient()
        {
            discordThread = createDiscordClientThread();
        }

        [BackgroundDependencyLoader]
        private void load(MusicSharpConfigManager localConfig)
        {
            token = localConfig.GetBindable<string>(MusicSharpSetting.Token);

            client = new DiscordSocketClient();
            client.Ready += onClientReady;
            client.Log += onClientLogReceived;
            client.SlashCommandExecuted += handleSlashCommand;
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
                discordThread.Start();
        }

        public async Task Stop()
        {
            if (discordThread.ThreadState != ThreadState.WaitSleepJoin)
                return;

            await client.LogoutAsync();
            discordThread?.Interrupt();
            discordThread = createDiscordClientThread();
        }

        private async Task botMain()
        {
            await client.LoginAsync(TokenType.Bot, token.Value);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task onClientReady()
        {
            var command = new SlashCommandBuilder();

            command.WithName("info");
            command.WithDescription("return bot info");

            await client.CreateGlobalApplicationCommandAsync(command.Build());

            OnClientReady?.Invoke();
        }

        private async Task handleSlashCommand(SocketSlashCommand command)
        {
            Logger.Log($"Used {command.User.Username}#{command.User.Discriminator} /{command.CommandName}");
            await command.RespondAsync("Running on Discord.NET v3.6.1 (API v9)");
        }

        private Task onClientLogReceived(LogMessage log)
        {
            OnClientLogReceived?.Invoke(log.Message);
            Logger.Log(log.Message);

            return Task.CompletedTask;
        }
    }
}
