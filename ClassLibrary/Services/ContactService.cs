using ClassLibrary.Models;
using System.Diagnostics;




namespace ClassLibrary.Services
{
    /// <summary>
    /// 
    /// The purpose of this service class is to handle add, delete, update and get requests from the json file that will store the contacts
    /// 
    /// This service class depends on FileService.cs class to work
    /// 
    /// </summary>

    public class ContactService : IContactService
    {

        /* Dependency injection of the FileService class that will handle the link between the json file and the application */
        private readonly FileService _fileService;
        public ContactService(FileService fileService)
        {
            _fileService = fileService;
        }

        public List<Contact> contacts = new List<Contact>();

        // KLAR
        public void GetContactsFromJson()
        {
            try
            {
                contacts = _fileService.UpdateListFromJson();
            }
            catch (Exception ex) { Debug.Write(ex.Message); }
        }

        // KLAR
        public void AddContactToList(Contact contact)
        {
            try
            {
                GetContactsFromJson();
                contacts.Add(contact);

                _fileService.UpdateJsonFromList(contacts);

                GetContactsFromJson();
            }
            catch (Exception ex) { Debug.Write(ex.Message); }
        }

        // KLAR
        public void DeleteContactFromList(string email)
        {
            try
            {
                _fileService.DeleteContactFromJson(email);
                GetContactsFromJson();
            }
            catch (Exception ex) { Debug.Write(ex.Message); }
        }

        // FLYTTA TILL MENUSERVICE
        public bool ShowAllContactsInConsole()
        {
            try
            {
                foreach (var contact in contacts)
                {
                    Console.WriteLine(
                        $"{contact.FirstName} " +
                        $"{contact.LastName}, " +
                        $"{contact.Email}");
                }
                return true;
            }
            catch (Exception ex) { Debug.Write(ex.Message); }
            return false;
        }





        // FIXA
        public Contact GetSpecificContact(int id)
        {
            GetContactsFromJson();
            return contacts[id - 1];
        }

        // KLAR
        public string BeautifyName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim().ToLower();
                name = char.ToUpper(name[0]) + name.Substring(1);
            }
            return name;
        }

        // KLAR
        public string BeautifyEmail(string email)
        {
            email = email.Trim().ToLower();
            return email;
        }
    }
}