using ClassLibrary.Interfaces;
using ClassLibrary.Models;
using ClassLibrary.Services;
using System.Runtime.CompilerServices;
using Xunit;

namespace ClassLibrary_Tests;

public class ContactService_Test
{
    private ContactService _contactService;
    public ContactService_Test(ContactService contactService)
    {
        _contactService = contactService;        
    }

    [Fact]
    public void GetContactFromJson_ShouldUpdateListWithObjectsInJsonFile()
    {
        // ---- ARRANGE ---- // 
        int before = _contactService.Contacts.Count;

        // ---- ACT ---- // 
        _contactService.GetContactsFromJson();
        int after = _contactService.Contacts.Count;
        
        // ---- ASSERT ---- // 
        Assert.NotEqual(before, after);
    }

    [Fact]
    public void AddContactToList_ShouldAddContactToList ()
    {
        // ---- ARRANGE ---- // 
        string testEmail = Guid.NewGuid().ToString();
        ContactModel testContact = new ContactModel() 
        {
            FirstName = "Test",
            LastName = "Testson",
            Email = testEmail,
            Address = "Test Rd. 2",
            City = "Testville",
            PostalCode = "12345",
            Phone = "123456789"
        };

        // ---- ACT ---- // 
        _contactService.AddContactToList(testContact);

        // ---- ASSERT ---- // 
        Assert.Equal(testEmail, _contactService.Contacts.Last().Email);

        // ---- CLEANUP ---- // 
        _contactService.DeleteContactFromList(_contactService.Contacts.Last().Email);
    }

    [Fact]
    public void DeleteContactFromList_ShouldDeleteContactsThatHaveMatchingEmailAddress ()
    {
        // ---- ARRANGE ---- // 
        string testEmail = Guid.NewGuid().ToString();

        ContactModel testContact = new ContactModel()
        {
            FirstName = "Test",
            LastName = "Testson",
            Email = testEmail,
            Address = "Test Rd. 2",
            City = "Testville",
            PostalCode = "12345",
            Phone = "123456789"
        };

        // ---- ACT ---- // 
        _contactService.AddContactToList(testContact);
        _contactService.DeleteContactFromList(testEmail);

        // ---- ASSERT ---- // 
        Assert.NotEqual(testEmail, _contactService.Contacts.Last().Email);
    }

    [Fact]
    public bool ShowAllContactsInConsole_ShouldLoopOverAllContactsAndWriteToConsole()
    {
        // ---- ARRANGE ---- //
        FileService fileService = new FileService();

        // ---- ACT ---- //
        _contactService.ShowAllContactsInConsole();

        // ---- ASSERT ---- //
        Assert.True(_contactService.ShowAllContactsInConsole());
        return _contactService.ShowAllContactsInConsole();
    }
}