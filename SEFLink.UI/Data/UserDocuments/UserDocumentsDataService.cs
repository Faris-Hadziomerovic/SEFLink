using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SEFLink.UI.Data
{
    public class UserDocumentsDataService : IUserDocumentsDataService
    {
        public ObservableCollection<UserDocuments> UserDocuments { get; set; }


        #region Constructor

        public UserDocumentsDataService(IUserDataService dataService)
        {
            UserDocuments = new ObservableCollection<UserDocuments>();

            var users = dataService.GetAllUsers();

            foreach (var user in users)
            {
                UserDocuments.Add(new UserDocuments(user, new DocumentInfoDataService()));
            }
        }

        #endregion


        #region Methods

        public bool Exists(string Email, string Password)
        {
            return UserDocuments.Any(x => x.User.Email == Email && x.User.Password == Password);
        }

        public bool ExistsById(int Id)
        {
            return UserDocuments.Any(x => x.User.Id == Id);
        }

        public UserDocuments GetUserDocuments(int Id)
        {
            return UserDocuments.SingleOrDefault(x => x.User.Id == Id);
        }
        
        public IDocumentInfoDataService GetDocumentsByUserId(int Id)
        {
            return UserDocuments.SingleOrDefault(x => x.User.Id == Id).GetDocuments();
        }

        public async Task<bool> Exists_Async(string Email, string Password)
        {
            await Task.Delay(200); /*simulating something heavy*/

            return UserDocuments.Any(x => x.User.Email == Email && x.User.Password == Password);
        }

        public async Task<bool> ExistsById_Async(int Id)
        {
            await Task.Delay(200); /*simulating something heavy*/

            return UserDocuments.Any(x => x.User.Id == Id);
        }

        public async Task<UserDocuments> GetUserDocumentsAsync(int Id)
        {
            await Task.Delay(500); /*simulating something heavy*/

            return UserDocuments.SingleOrDefault(x => x.User.Id == Id);
        }

        public async Task<IDocumentInfoDataService> GetDocumentsByUserIdAsync(int Id)
        {
            await Task.Delay(500); /*simulating something heavy*/

            return UserDocuments.SingleOrDefault(x => x.User.Id == Id).GetDocuments();
        }
        
        #endregion
    }
}
