using ClassLibrary.Models;
using ClassLibrary.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;

namespace MauiApp1.ViewModels;

public partial class MainViewModel : ObservableObject
{

    private readonly ContactService _contactService;

    public MainViewModel(ContactService contactService)
    {
        _contactService = contactService;
        UpdateMauiList();
    }

    [ObservableProperty]
    private ObservableCollection<ContactModel> _contactList = new();

    [ObservableProperty]
    private ContactModel _registrationForm = new();

    [RelayCommand]
    public void AddToList()
    {
        if (!string.IsNullOrWhiteSpace(RegistrationForm.Email) && !ContactList.Any(contact => contact.Email == RegistrationForm.Email))
        {
            _contactService.AddContactToList(RegistrationForm);
            UpdateMauiList();
            RegistrationForm = new();
        }
    }

    [RelayCommand]
    public void RemoveFromList(ContactModel contactToDelete) 
    {
        _contactService.DeleteContactFromList(contactToDelete.Email);
        UpdateMauiList();
        RegistrationForm = new();
    }

    public void UpdateMauiList()
    {
        ContactList.Clear();

        foreach (var contact in _contactService.Contacts)
        {
            ContactList.Add(contact);
        }
    }
}
