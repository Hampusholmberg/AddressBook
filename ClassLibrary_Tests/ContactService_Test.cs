using ClassLibrary.Interfaces;
using ClassLibrary.Models;
using ClassLibrary.Services;
using System.Runtime.CompilerServices;
using Xunit;

namespace ClassLibrary_Tests;

public class ContactService_Test
{
    [Fact]
    public void GetContactFromJson_ShouldUpdateListWithObjectsInJsonFile()
    {
        // ---- ARRANGE ---- // 
        ContactService contactService = new ContactService();

        // ---- ACT ---- // 
        int before = contactService.contacts.Count;
        contactService.GetContactsFromJson();
        int after = contactService.contacts.Count;

        
        // ---- ASSERT ---- // 

        // Makes sure the list is not empty after gett
        Assert.NotEqual(before, after);

        // Makes sure the filepath is valid
        // Assert. 
    }

    [Fact]
    public void AddContactToList_ShouldAddContactToList ()
    {
        // ---- ARRANGE ---- // 
        ContactService contactService = new ContactService();
        string testEmail = Guid.NewGuid().ToString();
        Contact testContact = new Contact() 
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
        contactService.AddContactToList(testContact);

        // ---- ASSERT ---- // 
        Assert.Equal(testEmail, contactService.contacts.Last().Email);

        // ---- CLEANUP ---- // 
        contactService.DeleteContactFromList(contactService.contacts.Last().Email);
    }

    [Fact]
    public void DeleteContactFromList_ShouldDeleteContactsThatHaveMatchingEmailAddress ()
    {
        // ---- ARRANGE ---- // 
        ContactService contactService = new ContactService();
        string testEmail = Guid.NewGuid().ToString();

        Contact testContact = new Contact()
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
        contactService.AddContactToList(testContact);
        contactService.DeleteContactFromList(testEmail);

        // ---- ASSERT ---- // 
        Assert.NotEqual(testEmail, contactService.contacts.Last().Email);
    }

    [Fact]
    public bool ShowAllContactsInConsole_ShouldLoopOverAllContactsAndWriteToConsole()
    {
        // ---- ARRANGE ---- //
        ContactService contactService = new ContactService();

        // ---- ACT ---- //
        contactService.ShowAllContactsInConsole();

        // ---- ASSERT ---- //
        Assert.True(contactService.ShowAllContactsInConsole());
        return contactService.ShowAllContactsInConsole();
    }
} 