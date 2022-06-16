using System.Threading.Tasks;

namespace SEFLink.UI.Data
{
    public interface IUserDocumentsDataService
    {
        bool Exists(string Email, string Password);
        bool ExistsById(int Id);
        Task<bool> ExistsById_Async(int Id);
        Task<bool> Exists_Async(string Email, string Password);
        UserDocuments GetUserDocuments(int Id);
        Task<UserDocuments> GetUserDocumentsAsync(int Id);
    }
}