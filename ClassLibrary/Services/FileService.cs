using ClassLibrary.Interfaces;
using ClassLibrary.Models;
using System.Text.Json;

namespace ClassLibrary.Services
{
    /// The purpose of this service class is to handle the connection between
    /// the application and the Json file stored on the disk. 

    public class FileService : IFileService
    {

        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), @$"C:\Education\03._C-sharp\AddressBook\Files\db.json");

        /// <summary>
        /// Takes a list of Contact objects and returns a json formatted string with proper indentation.
        /// </summary>
        /// <param name="contacts"></param>
        /// <returns> json format string </returns>
        public string ToJson(List<ContactModel> contacts)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(contacts, options);
            return json;
        }

        /// <summary>
        /// Takes a string of json formatted text and returns it as a list of Contacts.
        /// </summary>
        /// <param name="json"></param>
        /// <returns> List<Contact> </returns>
        public List<ContactModel> FromJson(string json)
        {
            List<ContactModel> jsonDeserialized = new List<ContactModel>();
            jsonDeserialized = JsonSerializer.Deserialize<List<ContactModel>>(json)!;
            return jsonDeserialized;
        }

        /// <summary>
        /// Takes an email address as a text string, tries to match it to contacts and deletes the user(s) from the json file and returns an updated list of contacts.
        /// </summary>
        /// <param name="email"></param>
        public void DeleteContactFromJson(Guid id)
        {
            List<ContactModel> contactsToKeep = new List<ContactModel>();
            List<ContactModel> allContacts = new List<ContactModel>(UpdateListFromJson());

            foreach (var contact in allContacts)
            {
                if (contact.Id != id)
                {
                    contactsToKeep.Add(contact);
                }
            }
            UpdateJsonFromList(contactsToKeep);
        }

        /// <summary>
        /// Takes a list of contacts and replaces the content of the json file with it.
        /// </summary>
        /// <param name="updatedList"></param>
        public void UpdateJsonFromList(List<ContactModel> updatedList)
        {
            File.WriteAllText(_filePath, ToJson(updatedList));
        }

        /// <summary>
        /// Returns a list with the content that is in the json file
        /// </summary>
        /// <returns> List<Contact> </returns>
        public List<ContactModel> UpdateListFromJson()
        {
            string json = File.ReadAllText(_filePath);
            return FromJson(json);
        }
    }
}