using System.Collections.Generic;
using SEFLink.Model;
using SEFLink.UI.Events;

namespace SEFLink.UI.Data
{
    public class UserDocuments : Observable
    {
        #region Fields

        private User _user;

        private IDocumentInfoDataService _documentInfoDataService;

        #endregion


        #region Constructors

        public UserDocuments() { }

        public UserDocuments(User user, IDocumentInfoDataService service)
        {
            User = user;
            DocumentInfoDataService = service;
        }

        #endregion


        #region Properties

        public User User
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(); }
        }

        public IDocumentInfoDataService DocumentInfoDataService
        {
            get { return _documentInfoDataService; }
            set { _documentInfoDataService = value; OnPropertyChanged(); }
        }

        #endregion


        #region Methods

        public User GetUser() => User;

        public UserSettings GetUserSettings() => User.UserSettings;

        public IDocumentInfoDataService GetDocuments() => DocumentInfoDataService;
       
        public List<DocumentInfo> GetIncomingDocuments() => DocumentInfoDataService.GetAllIncomingDocuments();

        public List<DocumentInfo> GetOutgoingDocuments() => DocumentInfoDataService.GetAllOutgoingDocuments();

        #endregion
    }
}
