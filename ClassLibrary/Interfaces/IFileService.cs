using ClassLibrary.Models;

namespace ClassLibrary.Interfaces
{
    public interface IFileService
    {
        string ToJson(List<ContactModel> contacts);
        List<ContactModel> FromJson(string json);
        List<ContactModel> UpdateListFromJson(string filepath);
        void UpdateJsonFromList(List<ContactModel> updatedList, string filePath);
        void DeleteContactFromJson(string id);
    }
}