using ClassLibrary.Models;
using ClassLibrary.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CrossPlatformApp.ViewModels
{
    public partial class ContactListViewModel : ObservableObject
    {
        private readonly ContactService _contactService;

        public ContactListViewModel(ContactService contactService)
        {
            _contactService = contactService;
            UpdateObsList();
        }

        [ObservableProperty]
        private ContactModel _registrationForm = new();

        [ObservableProperty]
        private ObservableCollection<ContactModel> _contactList = new ObservableCollection<ContactModel>();

        public void UpdateObsList()
        {
            ContactList.Clear();
            _contactService.GetContactsFromJson();
            foreach (ContactModel contact in _contactService.Contacts)
            {
                ContactList.Add(contact);
            }
        }


        [RelayCommand]
        private async Task ToSpecificContactView(ContactModel contactToEdit)
        {
            var parameters = new ShellNavigationQueryParameters
            {
                {"Contact", contactToEdit}
            };

            await Shell.Current.GoToAsync("SpecificContactView", parameters);
        }

        [RelayCommand]
        private async Task ToAddContactView()
        {
            await Shell.Current.GoToAsync("AddContactView");
        }
    }
}