using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Globalization;


namespace ClassLibrary.Services
{
    public class ContactService
    {
        private static readonly string _filePath = "C:\\Education\\03._C-sharp\\AddressBook\\Files\\db.json";
        public List<Contact> contacts = new List<Contact>();
        public void GetContactsFromJson ()
        {
            try 
            {
                string json = File.ReadAllText(_filePath);
                contacts = JsonSerializer.Deserialize<List<Contact>>(json);
            } catch { }
        }
        public void AddContactToList (Contact contact)
        {
            GetContactsFromJson();
            contacts.Add(contact);

            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(contacts, options);
            File.WriteAllText(_filePath, json);
            GetContactsFromJson();
        }
        public void DeleteContactFromList(string email)
        {
            GetContactsFromJson();
            List<Contact> contactsToKeep = new List<Contact>();

            foreach (var contact in contacts)
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
            GetContactsFromJson();
        }
        public void ShowAllContacts () 
        { 

            foreach (var contact in contacts) 
            {
                Console.WriteLine(
                    $"{contact.FirstName} " +
                    $"{contact.LastName}, " +
                    $"{contact.Email}");
            }
        }
        public Contact GetSpecificContact(int id)
        {
            GetContactsFromJson();
            return contacts[id-1];
        }
        public string BeautifyName(string name)
        {
            if (!string.IsNullOrEmpty(name)) 
            {
                name = name.Trim().ToLower();
                name = char.ToUpper(name[0]) + name.Substring(1);
            }
            return name;
        }
        public string BeautifyEmail(string email)
        {
            email = email.Trim().ToLower();
            return email;
        }
    }
}