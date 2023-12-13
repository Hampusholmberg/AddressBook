using ClassLibrary.Interfaces;

namespace ClassLibrary.Models
{
    public class ContactModel
    {
        public ContactModel() { }
        public ContactModel(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public ContactModel(string firstName, string lastName, string email,
            string address, string city, string postalCode, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            City = city;
            PostalCode = postalCode;
            Phone = phone;
        }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}