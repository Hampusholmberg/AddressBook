using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ConsoleApp.Services;
using ClassLibrary.Services;

class Program
{
    static void Main(string[] args)
    {
        // Create the host
        var builder = Host.CreateDefaultBuilder(args);

        // Register services
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<MenuService>();
            services.AddSingleton<ContactService>();
            services.AddSingleton<FileService>();
        });

        // Build the host
        var host = builder.Build();

        // Resolve MenuService from the container
        var menuService = host.Services.GetRequiredService<MenuService>();
        var contactService = host.Services.GetRequiredService<ContactService>();
        var fileService = host.Services.GetRequiredService<FileService>();

        // Run the program
        menuService.Run();
    }
}
