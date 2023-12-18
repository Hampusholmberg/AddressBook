
using ClassLibrary.Models;
using ClassLibrary.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CrossPlatformApp.ViewModels;

public partial class SpecificContactViewModel : ObservableObject, IQueryAttributable
{
    private readonly ContactListViewModel _contactListViewModel;
    private readonly ContactService _contactService;

    public SpecificContactViewModel(ContactListViewModel contactListViewModel, ContactService contactService)
    {
        _contactListViewModel = contactListViewModel;
        _contactService = contactService;
    }

    [ObservableProperty]
    private ContactModel _contactToEdit = new();

    [RelayCommand]
    public async Task ToEditContact(ContactModel contactToEdit) 
    {
        var parameters = new ShellNavigationQueryParameters
        {
                {"Contact", ContactToEdit}
        };

        await Shell.Current.GoToAsync("//EditContactView", parameters);
    }

    [RelayCommand]
    public async Task DeleteContact(ContactModel contactToEdit)
    {
        bool result = await Shell.Current.DisplayAlert("Confirm Deletion", $"Are you sure you want to delete {ContactToEdit.FirstName} {ContactToEdit.LastName}?", "Yes", "No");

        if (result)
        {
            _contactListViewModel.UpdateObsList();

            _contactService.DeleteContactFromList(ContactToEdit.Id);

            _contactListViewModel.UpdateObsList();

            await Shell.Current.GoToAsync("//ContactListView");
        }
    }

    [RelayCommand]
    public async Task GoBack()
    {
        await Shell.Current.GoToAsync("//ContactListView");
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        ContactToEdit = (query["Contact"] as ContactModel)!;
    }
}