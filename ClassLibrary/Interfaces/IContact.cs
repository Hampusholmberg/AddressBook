namespace ClassLibrary.Interfaces
{
    public interface IContact
    {
        string Address { get; set; }
        string City { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Phone { get; set; }
        string PostalCode { get; set; }
    }
}