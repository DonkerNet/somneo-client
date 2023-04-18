using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.DemoApp.CommandRunner;
using Donker.Home.Somneo.DemoApp.JsonConverters;

namespace Donker.Home.Somneo.DemoApp;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ISomneoApiClient>(new SomneoApiClient(_configuration.GetValue<string>("SomneoHost")!));
        services.AddSingleton<ISomneoCommandRunner, SomneoCommandRunner>();

        services.AddControllersWithViews()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new IPAddressJsonConverter());
            });

        services
            .AddRazorPages()
            .AddRazorRuntimeCompilation();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
        app.UseDeveloperExceptionPage();
        
        app.UseStaticFiles();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
