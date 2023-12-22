using ClassLibrary.Models;
using ClassLibrary.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CrossPlatformApp.ViewModels;

public partial class AddContactViewModel : ObservableObject
{
    private readonly ContactService _contactService;
    private readonly ContactListViewModel _contactListViewModel;

    public AddContactViewModel(ContactService contactService, ContactListViewModel contactListViewModel)
    {
        _contactService = contactService;
        _contactListViewModel = contactListViewModel;
    }

    [ObservableProperty]
    private ContactModel _registrationForm = new();


    [RelayCommand]
    public async Task AddToList()
    {
        _contactListViewModel.UpdateObsList();

        _contactService.AddContactToList(RegistrationForm);

        _contactListViewModel.UpdateObsList();

        RegistrationForm = new();

        await AppShell.Current.GoToAsync("//ContactListView");
    }

    [RelayCommand]
    public async Task GoBack()
    {
        bool result = await Shell.Current.DisplayAlert("Discard Changes", "Are you sure you want to cancel? Your progress will be lost.", "Yes", "No");

        if (result)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}