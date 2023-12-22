using ClassLibrary.Interfaces;
using ClassLibrary.Models;
using ClassLibrary.Services;
using Moq;
using System.Xml.Serialization;


namespace ClassLibrary.Tests;

public class FileService_Test
{
    [Fact]
    public void ToJson_ShouldConvertAListOfContactsToAJsonTextString()
    {
        // Arrange
        FileService fileService = new FileService();
        List<ContactModel> testList = new List<ContactModel>
        {
            new ContactModel {FirstName = "John", LastName = "Doe" },
            new ContactModel {FirstName = "Jane", LastName = "Doe" }
        };
        testList[0].Id = "test";
        testList[1].Id = "test";

        // Act
        var jsonResult = fileService.ToJson(testList);

        // Assert
        Assert.True(jsonResult != null);
        Assert.True(jsonResult == 
            "[\r\n  " +
                "{\r\n    \"" +
                    "Id\": \"test\",\r\n    \"" +
                    "FirstName\": \"John\",\r\n    \"" +
                    "LastName\": \"Doe\",\r\n    \"" +
                    "Email\": null,\r\n    \"" +
                    "Address\": null,\r\n    \"" +
                    "City\": null,\r\n    \"" +
                    "PostalCode\": null,\r\n    \"" +
                    "Phone\": null\r\n  " +
                "},\r\n  " +
                "{\r\n    \"" +
                    "Id\": \"test\",\r\n    \"" +
                    "FirstName\": \"Jane\",\r\n    \"" +
                    "LastName\": \"Doe\",\r\n    \"" +
                    "Email\": null,\r\n    \"" +
                    "Address\": null,\r\n    \"" +
                    "City\": null,\r\n    \"" +
                    "PostalCode\": null,\r\n    \"" +
                    "Phone\": null\r\n  " +
                "}\r\n" +
            "]");
    }

    [Fact]
    public void FromJson_ShouldConvertAJsonTextStringToAListOfContacts()
    {
        // Arrange
        FileService fileService = new FileService();
        string jsonFormatString = "[\r\n  {\r\n    \"Id\": \"test\",\r\n    \"FirstName\": \"John\",\r\n    \"LastName\": \"Doe\",\r\n    \"Email\": null,\r\n    \"Address\": null,\r\n    \"City\": null,\r\n    \"PostalCode\": null,\r\n    \"Phone\": null\r\n  },\r\n  {\r\n    \"Id\": \"test\",\r\n    \"FirstName\": \"Jane\",\r\n    \"LastName\": \"Doe\",\r\n    \"Email\": null,\r\n    \"Address\": null,\r\n    \"City\": null,\r\n    \"PostalCode\": null,\r\n    \"Phone\": null\r\n  }\r\n]";


        // Act
        List<ContactModel> testList = fileService.FromJson(jsonFormatString);

        // Assert
        Assert.True(testList != null);

        Assert.True(testList[0].FirstName == "John");
        Assert.True(testList[0].LastName == "Doe");
        Assert.True(testList[0].Id == "test");

        Assert.True(testList[1].FirstName == "Jane");
        Assert.True(testList[1].LastName == "Doe");
        Assert.True(testList[1].Id == "test");
    }

    [Fact]
    public void UpdateListFromJson_ShouldUpdateTheContactListWithTheContactsInTheJsonFile()
    {
        // Arrange 
        string filePath = @"C:\Education\03._C-sharp\AddressBook\Files\test_db.json";
        FileService fileService = new FileService();
        List<ContactModel> testList = new List<ContactModel> 
        {
            new ContactModel {FirstName = "test1", LastName = "test1"},
            new ContactModel {FirstName = "test2", LastName = "test2"}
        };
        File.WriteAllText(filePath, fileService.ToJson(testList));

        // Act
        List<ContactModel> listToAssert = new(fileService.UpdateListFromJson(filePath));

        //Assert 
        Assert.NotEmpty(listToAssert);
        ContactModel testContact = listToAssert.FirstOrDefault(contact => contact.Id == testList[0].Id)!;
        Assert.Equal(testContact.Id, testList[0].Id);

        // Clean up
        testList.Clear();
        fileService.UpdateJsonFromList(testList, filePath);
    }

    [Fact]
    public void UpdateJsonFromList_ShouldUpdateTheJsonFileWithTheCurrentContactList()
    {
        // Arrange
        string filePath = @"C:\Education\03._C-sharp\AddressBook\Files\test_db.json";
        FileService fileService = new FileService();

        List<ContactModel> testListBeforeUpdate = new List<ContactModel>
        {
            new ContactModel {FirstName = Guid.NewGuid().ToString(), LastName = "test1"},
            new ContactModel {FirstName = Guid.NewGuid().ToString(), LastName = "test2"}
        };

        // Act
        fileService.UpdateJsonFromList(testListBeforeUpdate, filePath);
        List<ContactModel> testListAfterUpdate = new(fileService.UpdateListFromJson(filePath));

        // Assert
        Assert.True(testListBeforeUpdate[0].FirstName == testListAfterUpdate[0].FirstName);

        // Clean up
        testListAfterUpdate.Clear();
        fileService.UpdateJsonFromList(testListAfterUpdate, filePath);
    }

    [Fact]
    public void DeleteContactFromJson_ShouldDeleteSelectedContactFromJsonFile()
    {
        // Arrange




        // Act




        // Assert
    }
}