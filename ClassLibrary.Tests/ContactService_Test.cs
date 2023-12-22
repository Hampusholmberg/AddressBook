using ClassLibrary.Models;
using ClassLibrary.Services;
using Moq;

namespace ClassLibrary.Tests;

public class ContactService_Test
{

    [Fact]
    public void AddContactToList_ShouldAddOneContactToList_ThenReturnTrue()
    {
        // Arrange
        var fileServiceMock = new Mock<FileService>();
        ContactService contactService = new ContactService(fileServiceMock.Object);
        ContactModel testContact = new ContactModel("test", "test", "test", "test", "test", "test", "test");
        int countBefore = contactService.Contacts.Count();

        // Act
        var result = contactService.AddContactToList(testContact);
        int countAfter = contactService.Contacts.Count();

        // Assert
        Assert.True(result);
        Assert.Equal(countBefore + 1, countAfter);

        // Cleanup
        contactService.DeleteContactFromList(testContact.Id);
    }

    [Fact]
    public void DeleteContactFromList_ShouldDeleteContactFromList_ThenReturnTrue()
    {
        // Arrange
        var fileServiceMock = new Mock<FileService>();
        ContactService contactService = new ContactService(fileServiceMock.Object);
        ContactModel testContact = new ContactModel("test", "test", "test", "test", "test", "test", "test");
        contactService.AddContactToList(testContact);
        int countBefore = contactService.Contacts.Count();

        // Act
        var result = contactService.DeleteContactFromList(testContact.Id);
        int countAfter = contactService.Contacts.Count();

        // Assert
        Assert.True(result);
        Assert.Equal(countBefore -1, countAfter);
    }

    [Fact]
    public void GetContactsFromJson_ShouldPopulateTheContactListWithContacts()
    {
        // Arrange
        List<ContactModel> testContacts = new List<ContactModel>();
        int countBefore = testContacts.Count();

        var fileServiceMock = new Mock<FileService>();
        ContactService contactService = new ContactService(fileServiceMock.Object);

        // Act
        contactService.GetContactsFromJson();
        testContacts = contactService.Contacts;
        int countAfter = testContacts.Count();

        // Assert
        Assert.NotEqual(countBefore, countAfter);
    }

    [Fact]
    public void UpdateContactInList_ShouldUpdateSelectedContactInList()
    {
        // Arrange
        var fileServiceMock = new Mock<FileService>();
        ContactService contactService = new ContactService(fileServiceMock.Object);
        ContactModel testContact = new("test", "test", "test", "test", "test", "test", "test");
        contactService.AddContactToList(testContact);

        // Act
        testContact.FirstName = "updated";
        testContact.LastName = "updated";
        testContact.Email = "updated";
        testContact.Address = "updated";
        testContact.City = "updated";
        testContact.PostalCode = "updated";
        testContact.Phone = "updated";
        contactService.UpdateContactInList(testContact);

        // Assert
        Assert.True(contactService.Contacts.Last().Id == testContact.Id);
        Assert.True(contactService.Contacts.Last().FirstName == testContact.FirstName);

        // Cleanup
        contactService.DeleteContactFromList(testContact.Id);
    }

    [Fact]
    public void GetSpecificContact_ShouldReturnSpecificContactBasedOnIndex()
    {
        // Arrange
        var fileServiceMock = new Mock<FileService>();
        ContactService contactService = new ContactService(fileServiceMock.Object);
        ContactModel testContact = new("test", "test", "test", "test", "test", "test", "test");
        contactService.AddContactToList(testContact);
        int lastIndexInList = contactService.Contacts.Count - 1;

        // Act
        var contactFromMethod = contactService.GetSpecificContact(lastIndexInList);

        // Assert
        Assert.Equal(testContact.Id, contactFromMethod.Id);
        Assert.True(contactService.Contacts.Last().Id == testContact.Id);

        // Cleanup
        contactService.DeleteContactFromList(testContact.Id);
    }
}