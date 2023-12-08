using ClassLibrary.Models;

namespace ClassLibrary.Interfaces
{
    public interface IFileService
    {
        string ToJson(List<Contact> contacts);
        List<Contact> FromJson(string json);
        List<Contact> UpdateListFromJson();
        void UpdateJsonFromList(List<Contact> updatedList);
        void DeleteContactFromJson(string email);
    }
}