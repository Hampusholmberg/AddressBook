using ClassLibrary.Models;
using ClassLibrary.Services;


namespace ConsoleApp.Services
{
    internal class MenuService
    {
        private readonly ContactService _contactService;
        public MenuService(ContactService contactService)
        {
            _contactService = contactService;
        }

        public void Run()
        {
            _contactService.GetContactsFromJson();
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
            Console.Clear();
        }
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
                        _contactService.DeleteContactFromList(_contactService.Contacts[index_int].Id);
                        Console.Clear();

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
                            Console.WriteLine("Please enter a valid option.");
                            WaitForUserInput();
                        }
                    }
                }
            } while (true);

            
        }
        public void MenuShowContacts()
        {
            Console.Clear();
            _contactService.ShowAllContactsInConsole();

            if (!_contactService.Contacts.Any())
            {
                Console.WriteLine("There are no contacts in the list.");
            }

            Console.Write("\n");
            WaitForUserInput();
        }
        public void MenuShowSpecificContact()
        {
            Console.Clear();

            for (int i = 0; i < _contactService.Contacts.Count(); i++)
            {
                int id = i + 1;
                Console.WriteLine(
                    $"{id}. {_contactService.Contacts[i].FirstName} " +
                    $"{_contactService.Contacts[i].LastName}");
            }
            Console.Write("\nPlease enter the number of the contact you want full details for: ");
            int index = int.Parse(Console.ReadLine());

            List<ContactModel> tempList = [_contactService.GetSpecificContact(index)];

            Console.Clear();
            Console.WriteLine(
                $"Name: {tempList[0].FirstName} {tempList[0].LastName}\n" +
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
