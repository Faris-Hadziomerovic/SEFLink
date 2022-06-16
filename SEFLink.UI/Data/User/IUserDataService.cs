using SEFLink.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SEFLink.UI.Data
{
    public interface IUserDataService
    {
        bool Exists(string Email, string Password);
        Task<bool> ExistsAsync(string Email, string Password);

        User GetUser(string Email, string Password);
        Task<User> GetUserAsync(string Email, string Password);

        User GetUserById(int Id);
        Task<User> GetUserByIdAsync(int Id);

        ObservableCollection<User> GetAllUsers();
        Task < ObservableCollection<User> > GetAllUsersAsync();
    }
}