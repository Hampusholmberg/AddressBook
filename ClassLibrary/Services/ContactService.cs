using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace ClassLibrary.Services
{
    public class ContactService
    {
        private static readonly string _filePath = "C:\\Education\\03._C-sharp\\AddressBook\\Files\\db.json";
        private List<Contact> _contacts = new List<Contact>();
        public void GetContactFromJson ()
        {
            try 
            {
                string json = File.ReadAllText(_filePath);
                _contacts = JsonSerializer.Deserialize<List<Contact>>(json);
            }
            catch {}
        }
        public void AddContactToList (Contact contact)
        {
            GetContactFromJson();
            _contacts.Add(contact);

            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(_contacts, options);
            File.WriteAllText(_filePath, json);
        }

        public void DeleteContactFromList(string email)
        {
            GetContactFromJson();

            List<Contact> contactsToKeep = new List<Contact>();

            foreach (var contact in _contacts)
            {
                if (contact.Email != email)
                {
                    contactsToKeep.Add(contact);
                }
            }

            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(contactsToKeep, options);
            File.WriteAllText(_filePath, json);

        }
        public void ShowAllContacts () 
        { 
            GetContactFromJson();

            foreach (var contact in _contacts) 
            {
                Console.WriteLine(
                    $"{contact.FirstName} " +
                    $"{contact.LastName}, " +
                    $"{contact.Email}");
            }
        }
    }
}