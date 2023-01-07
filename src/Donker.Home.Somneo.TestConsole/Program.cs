using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.TestConsole.Services;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.json", true, true)
    .AddEnvironmentVariables()
    .Build();

string somneoHost = config.GetValue<string>("SomneoHost")!;

using var somneoApiClient = new SomneoApiClient(somneoHost);
var testService = new TestService(somneoApiClient);
testService.Run();