using Addressbook.Console.Services;
using Addressbook.shared.Interfaces;
using Addressbook.shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddSingleton<IFileService, FileService>();

    services.AddSingleton<IContactService, ContactServiceConsole>();

    services.AddSingleton<MenuService>();
}).Build();

builder.Start();
System.Console.Clear();

var menuService = builder.Services.GetRequiredService<MenuService>();
menuService.Show();