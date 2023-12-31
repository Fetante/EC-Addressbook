using Addressbook.shared.Interfaces;
using Addressbook.shared.Services;
using AddressBook.WPF.Mvvm.ViewModels;
using AddressBook.WPF.Mvvm.Views;
using AddressBook.WPF.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;

namespace AddressBook.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static IHost? builder;

    public App()
    {
        builder = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainViewModel>();

                services.AddSingleton<Services.ContactServiceWPF>();

                services.AddTransient<ListViewModel>();
                services.AddTransient<ListView>();

                services.AddTransient<EditViewModel>();
                services.AddTransient<EditView>();

                services.AddTransient<AddViewModel>();
                services.AddTransient<AddView>();

                services.AddTransient<ContactInfoViewModel>();
                services.AddTransient<ContactInfoView>();

                services.AddSingleton<IFileService, FileService>();


            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        builder!.Start();

        var mainWindow = builder!.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

}
