using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using System.Threading.Tasks;

namespace MasterArmsDiscordBot
{
	class MasterArmsDiscordBot
	{
		public DiscordClient Client { get; private set; }
		public InteractivityExtension Interactivity { get; private set; }
		public CommandsNextExtension Commands { get; private set; }

		public async Task RunAsync()
		{
			var token = ""; // Put your Discord Bot token here
			var config = new DiscordConfiguration
			{
				Token = token, // Use variable "token" (line 22)
				TokenType = TokenType.Bot, // Define token type as Bot
				AutoReconnect = true, // Automatically attempt to reconnect if lost connection
				// MinimumLogLevel = LogLevel.Debug // Debug level logs
			};

			Client = new DiscordClient(config); // Create new DiscordClient using parameters defined in config (line 23)

			Client.Ready += OnClientReady; // No idea what this does

			var commandsConfig = new CommandsNextConfiguration
			{
				StringPrefixes = new string[] { "!" }, // Command prefix is an exclamation point
				EnableDms = false, // Can not send commands in DM to bot
				EnableMentionPrefix = true, // Allow @Bot as alternative to using prefix
				DmHelp = true // Send !help message in DMs
			};
			Commands = Client.UseCommandsNext(commandsConfig); // Create new UseCommandsNext using commandsConfig (line 35)
			Commands.RegisterCommands<CommandsList>(); // Register all commands in class CommandsList

			await Client.ConnectAsync(); // Connect client

			await Task.Delay(-1); // Infinite delay
		}
		private Task OnClientReady(object sender, ReadyEventArgs e)
		{
			return Task.CompletedTask; // No idea what this does
		}
	}
}
