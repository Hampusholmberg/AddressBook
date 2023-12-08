using ClassLibrary.Models;

namespace ClassLibrary.Services
{
    public interface IContactService
    {
        void AddContactToList(Contact contact);
        string BeautifyEmail(string email);
        string BeautifyName(string name);
        void DeleteContactFromList(string email);
        void GetContactsFromJson();
        Contact GetSpecificContact(int id);
        bool ShowAllContactsInConsole();
    }
}