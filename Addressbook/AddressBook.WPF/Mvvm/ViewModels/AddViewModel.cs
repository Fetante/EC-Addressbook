
using Addressbook.shared.Models;
using AddressBook.WPF.Interfaces;
using AddressBook.WPF.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace AddressBook.WPF.Mvvm.ViewModels;

public partial class AddViewModel : ObservableObject, IAddViewModel
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ContactServiceWPF _personService;

    public AddViewModel(IServiceProvider serviceProvider, ContactServiceWPF personService)
    {
        _serviceProvider = serviceProvider;
        _personService = personService;
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
    public void AddContact()
    {
        _personService.AddContact(Contact);
        Contact = new Contact();
        NavigateToListView();
    }

}
