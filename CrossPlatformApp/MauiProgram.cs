using ClassLibrary.Models;
using ClassLibrary.Services;
using CrossPlatformApp.ViewModels;
using CrossPlatformApp.Views;
using Microsoft.Extensions.Logging;

namespace CrossPlatformApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<ContactService>();
            builder.Services.AddSingleton<FileService>();
            builder.Services.AddSingleton<ContactModel>();

            builder.Services.AddSingleton<AddContactViewModel>();
            builder.Services.AddSingleton<AddContactView>();
            
            builder.Services.AddSingleton<ContactListViewModel>();
            builder.Services.AddSingleton<ContactListView>();

            builder.Services.AddSingleton<EditContactViewModel>();
            builder.Services.AddSingleton<EditContactView>();

            builder.Services.AddSingleton<SpecificContactViewModel>();
            builder.Services.AddSingleton<SpecificContactView>();

    		builder.Logging.AddDebug();
            return builder.Build();
        }
    }
}
