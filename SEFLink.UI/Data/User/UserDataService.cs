using SEFLink.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEFLink.UI.Data
{
    public class UserDataService : IUserDataService
    {
        public UserDataService()
        {
            Users = new ObservableCollection<User>
            {
                new User { 
                    Id = 1, 
                    Username = "Admin_1", 
                    Email = "Admin_1@sefLink.com", 
                    Password = "Admin123", 
                    IsAdmin = true 
                },
                new User { 
                    Id = 2, 
                    Username = "Admin_2", 
                    Email = "Admin_2@sefLink.com", 
                    Password = "Admin321", 
                    IsAdmin = true 
                },
                new User { 
                    Id = 3, 
                    Username = "user_1", 
                    Email = "user_1@gmail.com", 
                    Password = "qwe123", 
                    IsAdmin = false 
                },
                new User { 
                    Id = 4, 
                    Username = "user_2", 
                    Email = "user_2@gmail.com", 
                    Password = "qwe123", 
                    IsAdmin = false 
                },
                new User { 
                    Id = 5, 
                    Username = "user_3", 
                    Email = "user_3@gmail.com", 
                    Password = "qwe123", 
                    IsAdmin = false 
                }
            };
        }

        public ObservableCollection<User> Users { get; set; }
        
        public ObservableCollection<User> GetAllUsers() => Users;

        public async Task<ObservableCollection<User>> GetAllUsersAsync()
        {
            await Task.Delay(1000); /*simulating something heavy*/

            return Users;
        }

        public bool Exists(string Email, string Password)
        {
            return Users.Any(x => x.Email == Email && x.Password == Password);
        }

        public async Task<bool> ExistsAsync(string Email, string Password)
        {
            await Task.Delay(500); /*simulating something heavy*/

            return Users.Any(x => x.Email == Email && x.Password == Password);
        }

        public User GetUser(string Email, string Password)
        {
            return Users.SingleOrDefault(x => (x.Email == Email && x.Password == Password) ||
                                (x.Username == Email && x.Password == Password));
        }

        public async Task<User> GetUserAsync(string Email, string Password)
        {
            await Task.Delay(1000); /*simulating something heavy*/

            return Users.SingleOrDefault(x => (x.Email == Email && x.Password == Password) ||
                               (x.Username == Email && x.Password == Password));
        }
        
        public User GetUserById(int Id)
        {
            return Users.SingleOrDefault(x => (x.Id == Id));
        }

        public async Task<User> GetUserByIdAsync(int Id)
        {
            await Task.Delay(300); /*simulating something heavy*/

            return Users.SingleOrDefault(x => (x.Id == Id));
        }

    }
}
