using ClassLibrary.Models;
using ClassLibrary.Services;
using System.Diagnostics;


namespace ConsoleApp.Services
{
    internal class MenuService
    {
        private readonly ContactService _contactService;
        public MenuService(ContactService contactService)
        {
            _contactService = contactService;
        }

        /// <summary>
        /// Gets all the contacts stored in the Json file and runs the main menu to control what to do in the program.
        /// </summary>
        public void Run()
        {
            _contactService.GetContactsFromJson();
            MenuDefault();
        }

        /// <summary>
        /// Main menu to control all the options in the console application.
        /// </summary>
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

        /// <summary>
        /// Logic for adding a new contact to the contact list.
        /// </summary>
        public void MenuAddContact()
        {
            Console.Clear();
            Console.WriteLine("Are you sure you want to add a new contact?");
            Console.WriteLine("1.  Yes");
            Console.WriteLine("2.  No");
            Console.WriteLine("\nPlease enter number: ");

            string? menuChoice = Console.ReadLine();

            if (menuChoice == "1")
            {
                Console.Clear();

                int countBefore = _contactService.Contacts.Count()!;

                Console.Write("First Name: ");
                string? firstName = Console.ReadLine()!;

                Console.Write("Last Name: ");
                string? lastName = Console.ReadLine()!;

                Console.Write("Email Address: ");
                string? emailAddress = Console.ReadLine()!;

                Console.Write("Address: ");
                string? address = Console.ReadLine()!;

                Console.Write("City: ");
                string? city = Console.ReadLine()!;

                Console.Write("Postal Code: ");
                string? postalCode = Console.ReadLine()!;

                Console.Write("Phone number: ");
                string phoneNumber = Console.ReadLine()!;

                ContactModel contact = new(firstName, lastName, emailAddress, address, city, postalCode, phoneNumber);

                _contactService.AddContactToList(contact);
                Console.Clear();

                int countAfter = _contactService.Contacts.Count();

                if (countAfter > countBefore)
                {
                    Console.WriteLine($"{firstName} {lastName} was successfully added to the contact list.\nPress any key to continue...");
                }
                else
                {
                    Console.WriteLine("There was an error. Contact as not been added to the list.\nPress any key to continue...");
                }
                Console.ReadKey();
            }
            Console.Clear();
        }

        /// <summary>
        /// Logic for deleting a new contact to the contact list.
        /// </summary>
        public void MenuDeleteContact()
        {
            do
            {
                Console.Clear();

                for (int i = 0; i < _contactService.Contacts.Count(); i++)
                {
                    int id = i + 1;
                    Console.WriteLine(
                        $"{id}. {_contactService.Contacts[i].FirstName} " +
                        $"{_contactService.Contacts[i].LastName}");
                }

                Console.WriteLine("\nPlease enter the number of the contact you wish to delete from the list. \nPress enter without entering a value to abort.");

                string index = Console.ReadLine()!;

                if (index == "")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    try
                    {
                        int index_int = Convert.ToInt32(index) - 1;

                        ContactModel deletedContact = _contactService.Contacts[index_int];
                        _contactService.DeleteContactFromList(_contactService.Contacts[index_int].Id);

                        Console.Clear();
                        Console.WriteLine($"{deletedContact.FirstName} {deletedContact.LastName} was successfully deleted from the contact list.");

                        WaitForUserInput();
                        break;
                    }
                    catch
                    {
                        if (index == null)
                        {
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"You can only enter a number between 1 and {_contactService.Contacts.Count()}.");
                            WaitForUserInput();
                        }
                    }
                }
            } while (true);
        }

        /// <summary>
        /// Logic for displaying all contacts from the contact list in the console.
        /// </summary>
        public void MenuShowContacts()
        {
            Console.Clear();

            if (!_contactService.Contacts.Any())
            {
                Console.WriteLine("There are no contacts in the list.");
            } 
            else
            {
                try
                {
                    foreach (var contact in _contactService.Contacts)
                    {
                        Console.WriteLine(
                            $"{contact.FirstName} " +
                            $"{contact.LastName}, " +
                            $"{contact.Email}");
                    }
                }
                catch (Exception ex) { Debug.Write(ex.Message); }
            }

            Console.Write("\n");
            WaitForUserInput();
        }

        /// <summary>
        /// Displays a list of all contacts and lets the user choose which contact to see full details for.
        /// </summary>
        public void MenuShowSpecificContact()
        {
            Console.Clear();

            do
            {
                int index;

                for (int i = 0; i < _contactService.Contacts.Count(); i++)
                {
                    int id = i + 1;
                    Console.WriteLine(
                        $"{id}. {_contactService.Contacts[i].FirstName} " +
                        $"{_contactService.Contacts[i].LastName}");
                }

                try
                {
                    Console.Write("\nPlease enter the number of the contact you want full details for: ");
                    index = int.Parse(Console.ReadLine()!) - 1;

                    var specificContact = _contactService.GetSpecificContact(index);

                    Console.Clear();
                    Console.WriteLine(
                        $"Name: {specificContact.FirstName} {specificContact.LastName}\n" +
                        $"Email: {specificContact.Email}\n" +
                        $"Address: {specificContact.Address}, {specificContact.City}, {specificContact.PostalCode}\n" +
                        $"Phone Number: {specificContact.Phone}\n");

                    WaitForUserInput();
                    return;
                } 
                catch 
                {
                    Console.Clear();
                    Console.WriteLine($"You can only enter a number between 1 and {_contactService.Contacts.Count()}."); 
                    Console.WriteLine(); 
                }

            } while (true);
        }

        /// <summary>
        /// Method for waiting for user to press any key to continue.
        /// </summary>
        public void WaitForUserInput()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}