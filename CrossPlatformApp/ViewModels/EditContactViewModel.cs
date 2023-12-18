
using ClassLibrary.Models;
using ClassLibrary.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CrossPlatformApp.ViewModels;

public partial class EditContactViewModel : ObservableObject, IQueryAttributable
{
    private readonly ContactListViewModel _contactListViewModel;
    private readonly ContactService _contactService;

    public EditContactViewModel(ContactListViewModel contactListViewModel, ContactService contactService)
    {
        _contactListViewModel = contactListViewModel;
        _contactService = contactService;
    }

    [ObservableProperty]
    private ContactModel _contactToEdit = new();

    [ObservableProperty]
    private ContactModel _oldContact = new();


    [RelayCommand]
    public async Task UpdateContact() 
    {

        bool result = await Shell.Current.DisplayAlert("Confirm Changes","Are you sure you want to update the contact?","Yes","No");

        if (result) 
        {
            _contactListViewModel.UpdateObsList();
            _contactService.UpdateContactInList(ContactToEdit);
            _contactListViewModel.UpdateObsList();

            await Shell.Current.GoToAsync("//ContactListView");
        }
    }

    [RelayCommand]
    public async Task GoBack() 
    {
        await Shell.Current.GoToAsync("//SpecificContactView");
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        ContactToEdit = (query["Contact"] as ContactModel)!;
    }
}
