static class Program
{
    static void Main(string[] args) => new Ice.Discord.IceBot().RunBotAsync().GetAwaiter().GetResult();
}