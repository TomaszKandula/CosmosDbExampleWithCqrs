using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CosmosDbExample
{
    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] AArgs) =>
            Host.CreateDefaultBuilder(AArgs)
                .ConfigureWebHostDefaults(AWebBuilder =>
                {
                    AWebBuilder.UseStartup<Startup>();
                });

        public static void Main(string[] AArgs)
        {
            CreateHostBuilder(AArgs).Build().Run();
        }
    }
}
