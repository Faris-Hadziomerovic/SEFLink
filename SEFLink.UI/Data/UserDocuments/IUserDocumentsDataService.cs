using System.Threading.Tasks;

namespace SEFLink.UI.Data
{
    public interface IUserDocumentsDataService
    {
        bool ExistsById(int Id);
        bool Exists(string Email, string Password);
        UserDocuments GetUserDocuments(int Id);
        IDocumentInfoDataService GetDocumentsByUserId(int Id);

        Task<bool> ExistsById_Async(int Id);
        Task<bool> Exists_Async(string Email, string Password);
        Task<UserDocuments> GetUserDocumentsAsync(int Id);
        Task<IDocumentInfoDataService> GetDocumentsByUserIdAsync(int Id);
    }
}