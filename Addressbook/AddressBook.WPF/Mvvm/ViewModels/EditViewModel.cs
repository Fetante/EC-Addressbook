

using Addressbook.shared.Models;
using AddressBook.WPF.Interfaces;
using AddressBook.WPF.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace AddressBook.WPF.Mvvm.ViewModels;

public partial class EditViewModel : ObservableObject, IEditViewModel
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ContactServiceWPF _contactService;
    public EditViewModel(IServiceProvider serviceProvider, ContactServiceWPF contactService)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;

        Contact = _contactService.CurrentContact;
    }

    [ObservableProperty]
    private Contact contact = new();

    [RelayCommand]
    public void NavigateToListView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ListViewModel>();
    }

    [RelayCommand]
    public void Update()
    {
        _contactService.UpdateContact(Contact);
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ListViewModel>();
    }
}
