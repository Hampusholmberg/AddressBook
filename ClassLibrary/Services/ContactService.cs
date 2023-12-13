using ClassLibrary.Models;
using System.Diagnostics;




namespace ClassLibrary.Services
{
    /// The purpose of this service class is to handle add, delete, update and get requests from the json file that will store the contacts
    /// This service class depends on FileService.cs class to work
    public class ContactService
    {
        public List<ContactModel> Contacts = [];

        private readonly FileService _fileService;
        public ContactService(FileService fileService)
        {
            _fileService = fileService;
            GetContactsFromJson();
        }

        /// <summary>
        /// Updates the list of contacts with the information in the json file. 
        /// </summary>
        public void GetContactsFromJson()
        {
            try
            {
                Contacts = _fileService.UpdateListFromJson();
            }
            catch (Exception ex) { Debug.Write(ex.Message); }
        }


        /// <summary>
        /// Adds a contact object to the json file.
        /// </summary>
        /// <param name="contact"></param>
        public bool AddContactToList(ContactModel contact)
        {
            try
            {
                GetContactsFromJson();
                Contacts.Add(contact);

                _fileService.UpdateJsonFromList(Contacts);
                
                GetContactsFromJson();
                return true;
            }
            catch (Exception ex) { Debug.Write(ex.Message); }
            return false;
        }


        /// <summary>
        /// Deletes a contact object based on the email value from the json file.
        /// </summary>
        /// <param name="email"></param>
        public void DeleteContactFromList(string email)
        {
            try
            {
                _fileService.DeleteContactFromJson(email);
                GetContactsFromJson();
            }
            catch (Exception ex) { Debug.Write(ex.Message); }
        }


        /// <summary>
        /// Removes blank spaces before and after the name and converts the first letter to uppercase and the rest to lowercase.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string BeautifyName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim().ToLower();
                name = char.ToUpper(name[0]) + name.Substring(1);
            }
            return name;
        }


        /// <summary>
        /// Removes blank spaces before and after the email address and converts all letters to lowercase.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string BeautifyEmail(string email)
        {
            email = email.Trim().ToLower();
            return email;
        }




        // FLYTTA TILL MENUSERVICE
        public bool ShowAllContactsInConsole()
        {
            try
            {
                foreach (var contact in Contacts)
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
        public ContactModel GetSpecificContact(int id)
        {
            GetContactsFromJson();
            return Contacts[id - 1];
        }
    }
}