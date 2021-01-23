using Donker.Home.Somneo.ApiClient;
using Microsoft.Extensions.Configuration;

namespace Donker.Home.Somneo.TestConsole
{
    class Program
    {
        static void Main()
        {
            // TODO: alarmen, RelaxBreathe, Zonsondergang, FM radio

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            string somneoHost = config.GetValue<string>("SomneoHost");

            ISomneoApiClient somneoApiClient = new SomneoApiClient(somneoHost);
            TestService testService = new TestService(somneoApiClient);
            testService.Run();
        }
    }
}
