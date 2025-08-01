namespace BlazorApp5.SQLServer.Services
{
    // Services/AccountService.cs
    public class AccountService
    {
        private readonly List<UserAccount> _users = new()
    {
        new UserAccount { Username = "admin", Password = "admin123" },
        new UserAccount { Username = "user", Password = "user123" }
    };

        public bool ValidateCredentials(string username, string password)
        {
            return _users.Any(u => u.Username == username && u.Password == password);
        }

        private class UserAccount
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
