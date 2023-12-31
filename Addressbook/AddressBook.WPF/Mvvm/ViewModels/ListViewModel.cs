using Addressbook.shared.Interfaces;
using Addressbook.shared.Models;
using AddressBook.WPF.Interfaces;
using AddressBook.WPF.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;

namespace AddressBook.WPF.Mvvm.ViewModels;

public partial class ListViewModel : ObservableObject, IListViewModel
{
    public readonly IServiceProvider _serviceProvider;
    public readonly ContactServiceWPF _contactService;   

    [ObservableProperty]
    public ObservableCollection<IContact> contactList = [];


    public ListViewModel(IServiceProvider serviceProvider, ContactServiceWPF contactService)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;

        contactList = new ObservableCollection<IContact>(_contactService.GetAll());
    }

    [RelayCommand]
    public void NavigateToAddView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddViewModel>();
    }

    [RelayCommand]
    public void NavigateToEditView(Contact contact) 
    {
        _contactService.CurrentContact = contact;
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<EditViewModel>();
    }

    [RelayCommand]
    public void NavigateToContactInfoView(Contact contact)
    {
        _contactService.CurrentContact = contact;
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactInfoViewModel>();
    }

    [RelayCommand]
    public void RemoveContact(Contact contact)
    {
        _contactService.RemoveContact(contact);
        ContactList = new ObservableCollection<IContact>(_contactService.GetAll());
    }

}
