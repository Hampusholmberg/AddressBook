using ClassLibrary.Interfaces;
using ClassLibrary.Models;
using System.Text.Json;


namespace ClassLibrary.Services
{
    /// <summary>
    /// 
    /// The purpose of this service class is to handle the connection between
    /// the application and the Json file stored on the disk. 
    /// 
    /// </summary>
    public class FileService : IFileService
    {

        static string _fileName = "db.json";
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), @$"..\..\..\..\Files\{_fileName}");

        /* Takes a list of Contact objects and returns a json formatted string with proper indentation */
        public string ToJson(List<Contact> contacts)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(contacts, options);
            return json;
        }

        /* Takes a string of json formatted text and returns it as a list of Contacts */
        public List<Contact> FromJson(string json)
        {
            List<Contact> jsonDeserialized = new List<Contact>();
            jsonDeserialized = JsonSerializer.Deserialize<List<Contact>>(json)!;
            return jsonDeserialized;
        }

        /* Takes an email address as a text string, tries to match it to contacts and deletes the user(s) from the json file and returns an updated list of contacts */
        public void DeleteContactFromJson(string email)
        {
            List<Contact> contactsToKeep = new List<Contact>();
            List<Contact> allContacts = new List<Contact>(UpdateListFromJson());

            foreach (var contact in allContacts)
            {
                if (contact.Email != email)
                {
                    contactsToKeep.Add(contact);
                }
            }
            UpdateJsonFromList(contactsToKeep);
        }

        /* Takes a list of contacts and replaces the content of the json file with it */
        public void UpdateJsonFromList(List<Contact> updatedList)
        {
            File.WriteAllText(_filePath, ToJson(updatedList));
        }

        /* Returns a list with the content that is in the json file */
        public List<Contact> UpdateListFromJson()
        {
            string json = File.ReadAllText(_filePath);
            return FromJson(json);
        }
    }
}
