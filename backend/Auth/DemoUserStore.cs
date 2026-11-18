using Microsoft.AspNetCore.Identity;

namespace CustomerManagement.Api.Auth
{
    public class DemoUserStore
    {

        private readonly DemoUser _user;

        public DemoUserStore(IConfiguration configuration)
        {
            var username = configuration["DemoAuth:Username"];
            var password = configuration["DemoAuth:Password"];

            if (string.IsNullOrWhiteSpace(username) ||
               string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException(
                    "Development demo credential are not configured.");
            }

            _user = new DemoUser
            {
                Username = username,
                Role = "Admin"
            };

            var hasher = new PasswordHasher<DemoUser>();

            _user.PasswordHash = hasher.HashPassword(
                _user,
                password);              
        }


        public DemoUser? FindByUsername(string username)
        {
            return string.Equals(
                _user.Username,
                username,
                StringComparison.OrdinalIgnoreCase)
                ? _user
                : null;
        }

    }
}
