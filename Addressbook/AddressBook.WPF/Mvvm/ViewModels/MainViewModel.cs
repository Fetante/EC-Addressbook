
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace AddressBook.WPF.Mvvm.ViewModels;

public partial class MainViewModel : ObservableObject
{

    private readonly IServiceProvider _serviceProvider;

    public MainViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        CurrentViewModel = _serviceProvider.GetRequiredService<ListViewModel>();
    }

    [ObservableProperty]
    private ObservableObject _currentViewModel = null!;
}
