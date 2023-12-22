using ClassLibrary.Models;
using System.Diagnostics;

namespace ClassLibrary.Services
{
    // The purpose of this service class is to handle add, delete, update and get requests from the json file that will store the contacts
    // This service class depends on FileService.cs class to work
    public class ContactService
    {
        public List<ContactModel> Contacts = [];

        public FileService _fileService;
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
                Contacts.Add(BeautifyContact(contact));

                _fileService.UpdateJsonFromList(Contacts);
                GetContactsFromJson();

                return true;
            }
            catch (Exception ex) 
            {
                Debug.Write(ex.Message);
                return false;
            }
            
        }

        /// <summary>
        /// Deletes a contact object based on the Guid (Id) value from the json file.
        /// </summary>
        /// <param name="id"></param>
        public bool DeleteContactFromList(string id)
        {
            try
            {
                _fileService.DeleteContactFromJson(id);
                GetContactsFromJson();
                return true;
            }
            catch (Exception ex) { Debug.Write(ex.Message); }
            return false;
        }

        /// <summary>
        /// Updates a Contact Object in the json file.
        /// </summary>
        /// <param name="contactToUpdate"></param>
        /// <returns></returns>
        public bool UpdateContactInList(ContactModel contactToUpdate)
        {
            try
            {
                int index = Contacts.FindIndex(x => x.Id == contactToUpdate.Id);

                Contacts[index] = BeautifyContact(contactToUpdate);

                _fileService.UpdateJsonFromList(Contacts);

                return true;
            }
            catch (Exception ex) { Debug.Write(ex.Message); }
            return false;
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
            if (!string.IsNullOrEmpty(email))
            {
                email = email.Trim().ToLower();
                return email;
            }
            return email;
        }

        /// <summary>
        /// Takes a Contact Object and returns it with beautified proporties.
        /// </summary>
        /// <param name="contactToUpdate"></param>
        /// <returns></returns>
        public ContactModel BeautifyContact(ContactModel contactToUpdate)
        {
            contactToUpdate.FirstName = BeautifyName(contactToUpdate.FirstName);
            contactToUpdate.LastName = BeautifyName(contactToUpdate.LastName);
            contactToUpdate.Email = BeautifyEmail(contactToUpdate.Email);
            contactToUpdate.Address = BeautifyName(contactToUpdate.Address);
            contactToUpdate.City = BeautifyName(contactToUpdate.City);
            contactToUpdate.PostalCode = BeautifyName(contactToUpdate.PostalCode);
            contactToUpdate.Phone = BeautifyName(contactToUpdate.Phone);

            return contactToUpdate;
        }

        /// <summary>
        /// Returns a contact object based on the index inserted. 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ContactModel GetSpecificContact(int index)
        {
            GetContactsFromJson();
            return Contacts[index];
        }
    }
}