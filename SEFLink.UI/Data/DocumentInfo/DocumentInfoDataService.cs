using System;
using System.Linq;
using SEFLink.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SEFLink.UI.Data
{
    public class DocumentInfoDataService : IDocumentInfoDataService
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int NotificationsNumber { get; set; }

        public List<DocumentInfo> Documents { get; private set; }
        public List<DocumentInfo> IncomingDocuments { get; set; }
        public List<DocumentInfo> OutgoingDocuments { get; set; }


        public DocumentInfoDataService()
        {
            Documents = new List<DocumentInfo>
            {
                new DocumentInfo {
                    Id = 1,
                    Sender = "Pošiljalac-Firma-B-DOO",
                    Receiver = "Primalac-Firma-A-DOO",
                    Number = 1324,
                    Date = DateTime.Today,
                    Type = DocType.TIP,
                    Status = DocStatus.Saved
                },
                new DocumentInfo {
                    Id = 2,
                    Sender = "Pošiljalac-Firma-B-DOO",
                    Receiver = "Primalac-Firma-A-DOO",
                    Number = 1325,
                    Date = new DateTime(2022,6,10),
                    Type = DocType.KO,
                    Status = DocStatus.Saved
                },
                new DocumentInfo {
                    Id = 3,
                    Sender = "Pošiljalac-Firma-A-DOO",
                    Receiver = "Primalac-Firma-C-DOO",
                    Number = 1326,
                    Date = new DateTime(2022,6,1),
                    Type = DocType.KF,
                    Status = DocStatus.Pending
                },
                new DocumentInfo {
                    Id = 4,
                    Sender = "Pošiljalac-Firma-A-DOO",
                    Receiver = "Primalac-Firma-B-DOO",
                    Number = 1327,
                    Date = new DateTime(2022,5,7),
                    Type = DocType.KZ,
                    Status = DocStatus.Saved
                },
                new DocumentInfo {
                    Id = 5,
                    Sender = "Pošiljalac-Firma-B-DOO",
                    Receiver = "Primalac-Firma-A-DOO",
                    Number = 1623,
                    Date = new DateTime(2022,5,7),
                    Type = DocType.AF,
                    Status = DocStatus.Saved
                },
                new DocumentInfo {
                    Id = 6,
                    Sender = "Pošiljalac-ABC",
                    Receiver = "ABC",
                    Number = 1324,
                    Date = new DateTime(2022,5,7),
                    Type = DocType.TIP,
                    Status = DocStatus.Pending
                },
                new DocumentInfo {
                    Id = 7,
                    Sender = "Pošiljalac-AAA",
                    Receiver = "AAA",
                    Number = 1325,
                    Date = new DateTime(2022,3,25),
                    Type = DocType.KO,
                    Status = DocStatus.Saved
                },
                new DocumentInfo {
                    Id = 8,
                    Sender = "Pošiljalac-DDD",
                    Receiver = "DDD",
                    Number = 1326,
                    Date = new DateTime(2022,2,15),
                    Type = DocType.KF,
                    Status = DocStatus.Error
                },
                new DocumentInfo {
                    Id = 9,
                    Sender = "Pošiljalac-BBB",
                    Receiver = "BBB",
                    Number = 1327,
                    Date = new DateTime(2021,12,1),
                    Type = DocType.KZ,
                    Status = DocStatus.Saved
                },
                new DocumentInfo {
                    Id = 10,
                    Sender = "Pošiljalac-CCC",
                    Receiver = "CCC",
                    Number = 1623,
                    Date = new DateTime(2021,11,15),
                    Type = DocType.AF,
                    Status = DocStatus.Saved
                },
                new DocumentInfo {
                    Id = 11,
                    Sender = "Pošiljalac-Firma-B-DOO",
                    Receiver = "Primalac-Firma-A-DOO",
                    Number = 1324,
                    Date = new DateTime(2021,9,10),
                    Type = DocType.TIP,
                    Status = DocStatus.Saved
                },
                new DocumentInfo {
                    Id = 12,
                    Sender = "Pošiljalac-Firma-B-DOO",
                    Receiver = "Primalac-Firma-A-DOO",
                    Number = 1325,
                    Date = new DateTime(2021,6,18),
                    Type = DocType.KO,
                    Status = DocStatus.Saved
                },
                new DocumentInfo {
                    Id = 13,
                    Sender = "Pošiljalac-Firma-A-DOO",
                    Receiver = "Primalac-Firma-C-DOO",
                    Number = 1326,
                    Date = new DateTime(2021,3,15),
                    Type = DocType.KF,
                    Status = DocStatus.Pending
                },
                new DocumentInfo {
                    Id = 14,
                    Sender = "Pošiljalac-CDE",
                    Receiver = "Primalac-CDE",
                    Number = 1327,
                    Date = new DateTime(2020,12,28),
                    Type = DocType.KZ,
                    Status = DocStatus.Saved
                },
                new DocumentInfo {
                    Id = 15,
                    Sender = "Pošiljalac-Firma-B-DOO",
                    Receiver = "Primalac-Firma-A-DOO",
                    Number = 1623,
                    Date = new DateTime(2020,10,25),
                    Type = DocType.AF,
                    Status = DocStatus.Saved
                },
                new DocumentInfo {
                    Id = 16,
                    Sender = "Pošiljalac-ABC",
                    Receiver = "ABC",
                    Number = 1324,
                    Date = new DateTime(2020,5,1),
                    Type = DocType.TIP,
                    Status = DocStatus.Pending
                },
                new DocumentInfo {
                    Id = 17,
                    Sender = "Pošiljalac-Firma-AAA",
                    Receiver = "AAA",
                    Number = 1325,
                    Date = new DateTime(2020,4,5),
                    Type = DocType.KO,
                    Status = DocStatus.Saved
                },
                new DocumentInfo {
                    Id = 18,
                    Sender = "Pošiljalac-Firma-DDD",
                    Receiver = "Primalac-Firma-DDD",
                    Number = 1326,
                    Date = new DateTime(2020,4,5),
                    Type = DocType.KF,
                    Status = DocStatus.Error
                },
                new DocumentInfo {
                    Id = 19,
                    Sender = "Pošiljalac-Firma-BCD",
                    Receiver = "Primalac-Firma-BCD",
                    Number = 1327,
                    Date = new DateTime(2020,3,25),
                    Type = DocType.KZ,
                    Status = DocStatus.Saved
                },
                new DocumentInfo {
                    Id = 20,
                    Sender = "Pošiljalac-Firma-CCC",
                    Receiver = "Primalac-BCD",
                    Number = 1623,
                    Date = new DateTime(2020,2,15),
                    Type = DocType.AF,
                    Status = DocStatus.Saved
                }
            };

            IncomingDocuments = new List<DocumentInfo>();
            OutgoingDocuments = new List<DocumentInfo>();

            foreach (var doc in Documents)
            {
                IncomingDocuments.Add(new DocumentInfo(doc));
                OutgoingDocuments.Add(new DocumentInfo(doc));
            }

            foreach (var doc in IncomingDocuments)
            {
                doc.Receiver = "Me";
            }

            foreach (var doc in OutgoingDocuments)
            {
                doc.Sender = "Me";
            }
            
        }


        public int GetNotifications()
        {
            var x = new Random();

            NotificationsNumber = x.Next(1, 20);

            return NotificationsNumber;
        }


        #region Base Documents Methods

        public int GetCount()
        {
            return Documents.Count;
        }

        public bool ExistsById(int Id)
        {
            return Documents.Any(x => x.Id == Id);
        }

        public bool ExistsByNumber(int number)
        {
            return Documents.Any(x => x.Number == number);
        }

        public DocumentInfo GetDocumentById(int Id)
        {
            return Documents.SingleOrDefault(x => (x.Id == Id));
        }

        public DocumentInfo GetDocumentByNumber(int number)
        {
            return Documents.SingleOrDefault(x => (x.Number == number));
        }

        public async Task<DocumentInfo> GetDocumentByIdAsync(int Id)
        {
            await Task.Delay(300); /*simulating something heavy*/

            return Documents.SingleOrDefault(x => (x.Id == Id));
        }

        public async Task<DocumentInfo> GetDocumentByNumberAsync(int number)
        {
            await Task.Delay(300); /*simulating something heavy*/

            return Documents.SingleOrDefault(x => (x.Number == number));
        }

        public List<DocumentInfo> GetAllDocuments()
        {
            return Documents.ToList();
        }

        public async Task<List<DocumentInfo>> GetAllDocumentsAsync()
        {
            await Task.Delay(300); // simulating

            return Documents.ToList();
        }

        public bool DeleteById(int Id)
        {
            return Documents.Remove(GetDocumentById(Id));
        }

        public async Task<bool> DeleteByIdAsync(int Id)
        {
            await Task.Delay(100); // simulating

            return Documents.Remove(GetDocumentById(Id));
        }

        #endregion


        #region Incoming Documents Methods

        public int GetIncomingCount()
        {
            return IncomingDocuments.Count;
        }

        public bool ExistsByIncomingId(int Id)
        {
            return IncomingDocuments.Any(x => x.Id == Id);
        }

        public bool ExistsByIncomingNumber(int number)
        {
            return IncomingDocuments.Any(x => x.Number == number);
        }

        public DocumentInfo GetIncomingDocumentById(int Id)
        {
            return IncomingDocuments.SingleOrDefault(x => (x.Id == Id));
        }

        public DocumentInfo GetIncomingDocumentByNumber(int number)
        {
            return IncomingDocuments.SingleOrDefault(x => (x.Number == number));
        }

        public async Task<DocumentInfo> GetIncomingDocumentByIdAsync(int Id)
        {
            await Task.Delay(300); /*simulating something heavy*/

            return IncomingDocuments.SingleOrDefault(x => (x.Id == Id));
        }

        public async Task<DocumentInfo> GetIncomingDocumentByNumberAsync(int number)
        {
            await Task.Delay(300); /*simulating something heavy*/

            return IncomingDocuments.SingleOrDefault(x => (x.Number == number));
        }

        public List<DocumentInfo> GetAllIncomingDocuments()
        {
            return IncomingDocuments.ToList();
        }

        public async Task<List<DocumentInfo>> GetAllIncomingDocumentsAsync()
        {
            await Task.Delay(300); // simulating

            return IncomingDocuments.ToList();
        }

        public bool DeleteIncomingById(int Id)
        {
            return IncomingDocuments.Remove(GetIncomingDocumentById(Id));
        }

        public async Task<bool> DeleteIncomingByIdAsync(int Id)
        {
            await Task.Delay(100); // simulating

            return IncomingDocuments.Remove(GetIncomingDocumentById(Id));
        }

        #endregion


        #region Outgoing Documents Methods

        public int GetOutgoingCount()
        {
            return OutgoingDocuments.Count;
        }

        public bool ExistsByOutgoingId(int Id)
        {
            return OutgoingDocuments.Any(x => x.Id == Id);
        }

        public bool ExistsByOutgoingNumber(int number)
        {
            return OutgoingDocuments.Any(x => x.Number == number);
        }

        public DocumentInfo GetOutgoingDocumentById(int Id)
        {
            return OutgoingDocuments.SingleOrDefault(x => (x.Id == Id));
        }

        public DocumentInfo GetOutgoingDocumentByNumber(int number)
        {
            return OutgoingDocuments.SingleOrDefault(x => (x.Number == number));
        }

        public async Task<DocumentInfo> GetOutgoingDocumentByIdAsync(int Id)
        {
            await Task.Delay(300); /*simulating something heavy*/

            return OutgoingDocuments.SingleOrDefault(x => (x.Id == Id));
        }

        public async Task<DocumentInfo> GetOutgoingDocumentByNumberAsync(int number)
        {
            await Task.Delay(300); /*simulating something heavy*/

            return OutgoingDocuments.SingleOrDefault(x => (x.Number == number));
        }

        public List<DocumentInfo> GetAllOutgoingDocuments()
        {
            return OutgoingDocuments.ToList();
        }

        public async Task<List<DocumentInfo>> GetAllOutgoingDocumentsAsync()
        {
            await Task.Delay(300); // simulating

            return OutgoingDocuments.ToList();
        }

        public bool DeleteOutgoingById(int Id)
        {
            return OutgoingDocuments.Remove(GetOutgoingDocumentById(Id));
        }

        public async Task<bool> DeleteOutgoingByIdAsync(int Id)
        {
            await Task.Delay(100); // simulating

            return OutgoingDocuments.Remove(GetOutgoingDocumentById(Id));
        }

        #endregion
    }
}