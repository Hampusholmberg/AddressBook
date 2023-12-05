using ClassLibrary.Models;
using ClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    internal class MenuService
    {
        ContactService contactService = new();
        public void Run()
        {
            MenuDefault();
        }
        public void MenuDefault()
        {
            while (true)
            {
                Console.WriteLine("######## - MENU - ########");
                Console.WriteLine("1.   Add Contact");
                Console.WriteLine("2.   Remove Contact");
                Console.WriteLine("3.   Show All Contacts");
                Console.WriteLine("9.   Exit Application");
                Console.Write("\nPlease enter number: ");

                string menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        MenuAddContact();
                        break;
                    case "2":
                        MenuDeleteContact();
                        break;
                    case "3":
                        MenuShowContacts();
                        break;
                    case "9":
                        return;
                    default:
                        Console.Clear();
                        continue;
                }
            }
        }

        public void MenuAddContact() 
        {
            Console.Clear();

            Console.WriteLine("First Name: ");
            string? firstName= Console.ReadLine();

            Console.WriteLine("Last Name: ");
            string? lastName= Console.ReadLine();

            Console.WriteLine("Email Address: ");
            string? emailAddress = Console.ReadLine();

            Console.WriteLine("Address: ");
            string? address = Console.ReadLine();

            Console.WriteLine("City: ");
            string? city = Console.ReadLine();

            Console.WriteLine("Postal Code: ");
            string? postalCode = Console.ReadLine();
            
            Console.WriteLine("Phone number: ");
            string? phoneNumber = Console.ReadLine();

            Contact contact = new Contact(firstName, lastName, emailAddress, address, city, postalCode, phoneNumber);

            contactService.AddContactToList(contact);
        }
        public void MenuDeleteContact() 
        {
            contactService.ShowAllContacts();
            Console.Write("\nPlease enter the email address of the contact you wish to remove: ");
            string contactToRemove = Console.ReadLine();
            contactService.DeleteContactFromList(contactToRemove);
        }
        public void MenuShowContacts()
        {
            Console.Clear();
            contactService.ShowAllContacts();
            Console.Write("\n");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
