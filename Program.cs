namespace MasterArmsDiscordBot
{
	class Program
	{
		static void Main(string[] args)
		{
			var bot = new MasterArmsDiscordBot();
			bot.RunAsync().GetAwaiter().GetResult();
		}
	}
}
