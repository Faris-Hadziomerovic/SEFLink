using Prism.Events;
using SEFLink.Model;

namespace SEFLink.UI.Events
{
    public class LoggedInEvent : PubSubEvent<LoggedInEventArgs> { }

    public class LoggedInEventArgs
    {
        public LoggedInEventArgs() { }

        public LoggedInEventArgs(int id, string username = null, string email = null, bool rememberUser = false, bool isAdmin = false, UserSettings userSettings = null)
        {
            Id = id;
            UserName = username;
            Email = email;
            RememberUser = rememberUser;
            IsAdmin = isAdmin;
            UserSettings = userSettings;
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool RememberUser { get; set; }
        public bool IsAdmin { get; set; }
        public UserSettings UserSettings { get; set; }
    }
}
