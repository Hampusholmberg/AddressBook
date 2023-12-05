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
            contactService.GetContactsFromJson();
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
                Console.WriteLine("4.   Show Specific Contact");
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
                    case "4":
                        MenuShowSpecificContact();
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

            int countBefore = contactService.contacts.Count(); 

            Console.Write("First Name: ");
            string? firstName = contactService.BeautifyName(Console.ReadLine());

            Console.Write("Last Name: ");
            string? lastName = contactService.BeautifyName(Console.ReadLine());

            Console.Write("Email Address: ");
            string? emailAddress = contactService.BeautifyEmail(Console.ReadLine());

            Console.Write("Address: ");
            string? address = contactService.BeautifyName(Console.ReadLine());

            Console.Write("City: ");
            string? city = contactService.BeautifyName(Console.ReadLine());

            Console.Write("Postal Code: ");
            string? postalCode = Console.ReadLine();
            
            Console.Write("Phone number: ");
            string? phoneNumber = Console.ReadLine();

            Contact contact = new Contact(firstName, lastName, emailAddress, address, city, postalCode, phoneNumber);

            contactService.AddContactToList(contact);
            Console.Clear();

            int countAfter = contactService.contacts.Count();

            if (countAfter > countBefore)
            {
                Console.WriteLine($"{firstName} {lastName} was successfully added to the contact list.\nPress any key to continue...");
            } 
            else
            {
                Console.WriteLine("There was an error. Contact as not been added to the list.\nPress any key to continue...");
            }
            Console.ReadKey();
            Console.Clear();
        }
        public void MenuDeleteContact() 
        {
            Console.Clear();
            contactService.ShowAllContacts();
            Console.Write("\nPlease enter the email address of the contact you wish to remove: ");
            string contactToRemove = Console.ReadLine();
            contactService.DeleteContactFromList(contactToRemove);

            WaitForUserInput();
        }
        public void MenuShowContacts()
        {
            Console.Clear();
            contactService.ShowAllContacts();

            if (!contactService.contacts.Any())
            {
                Console.WriteLine("There are no contacts in the list.");
            }

            Console.Write("\n");
            WaitForUserInput();
        }
        public void MenuShowSpecificContact() 
        {
            Console.Clear();

            for (int i = 0; i < contactService.contacts.Count();i++)
            {
                int id = i + 1;
                Console.WriteLine(
                    $"{id}. {contactService.contacts[i].FirstName} " +
                    $"{contactService.contacts[i].LastName}");
            }
            Console.Write("\nPlease enter the number of the contact you want full details for: ");
            int index = int.Parse(Console.ReadLine());

            List<Contact> tempList = [contactService.GetSpecificContact(index)];

            Console.Clear();
            Console.WriteLine(
                $"Name: {tempList[0].FirstName} {tempList[0].LastName}\n"+
                $"Email: {tempList[0].Email}\n" +
                $"Address: {tempList[0].Address}, {tempList[0].City}, {tempList[0].PostalCode}\n" +
                $"Email: {tempList[0].Phone}\n");

            WaitForUserInput();
        }
        public void WaitForUserInput() 
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
