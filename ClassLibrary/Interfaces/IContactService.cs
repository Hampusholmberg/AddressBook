using ClassLibrary.Models;

namespace ClassLibrary.Services
{
    public interface IContactService
    {
        bool AddContactToList(ContactModel contact);
        string BeautifyEmail(string email);
        string BeautifyName(string name);
        void DeleteContactFromList(string email);
        void GetContactsFromJson();
        ContactModel GetSpecificContact(int id);
        bool ShowAllContactsInConsole();
    }
}