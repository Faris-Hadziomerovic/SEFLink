using SEFLink.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SEFLink.UI.Data
{
    public interface IDocumentInfoDataService
    {
        int GetNotifications();

        int GetIncomingCount();
        int GetOutgoingCount();

        bool ExistsByIncomingId(int Id);
        bool ExistsByOutgoingId(int Id);

        bool ExistsByIncomingNumber(int number);
        bool ExistsByOutgoingNumber(int number);

        bool DeleteIncomingById(int Id);
        bool DeleteOutgoingById(int Id);
        
        Task <bool> DeleteIncomingByIdAsync(int Id);
        Task <bool> DeleteOutgoingByIdAsync(int Id);

        DocumentInfo GetIncomingDocumentById(int Id);
        DocumentInfo GetOutgoingDocumentById(int Id);

        Task<DocumentInfo> GetIncomingDocumentByIdAsync(int Id);
        Task<DocumentInfo> GetOutgoingDocumentByIdAsync(int Id);

        DocumentInfo GetIncomingDocumentByNumber(int number);
        DocumentInfo GetOutgoingDocumentByNumber(int number);

        Task<DocumentInfo> GetIncomingDocumentByNumberAsync(int number);
        Task<DocumentInfo> GetOutgoingDocumentByNumberAsync(int number);

        List<DocumentInfo> GetAllIncomingDocuments();
        List<DocumentInfo> GetAllOutgoingDocuments();

        Task< List<DocumentInfo> > GetAllIncomingDocumentsAsync();
        Task< List<DocumentInfo> > GetAllOutgoingDocumentsAsync();

    }
}