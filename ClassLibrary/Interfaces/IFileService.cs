using ClassLibrary.Models;

namespace ClassLibrary.Interfaces
{
    public interface IFileService
    {
        string ToJson(List<ContactModel> contacts);
        List<ContactModel> FromJson(string json);
        List<ContactModel> UpdateListFromJson();
        void UpdateJsonFromList(List<ContactModel> updatedList);
        void DeleteContactFromJson(Guid id);
    }
}