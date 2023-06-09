using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Dziennik.Server.Services.UserService
{
    public class UserService : BaseService, IUserService
    {
        private const int keySize = 64;
        private HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        private const int iterations = 35001;
        public UserService(DataContext context, IHttpContextAccessor accessor) : base(context, accessor) { }

        public List<UserResponse> GetUsers()
        {
            var users = _context.Users.Select(GetUserResponse).ToList();
            return users;
        }

        public UserResponse GetUser(int id)
        { 
            var user = _context.Users.FirstOrDefault(h => h.Id == id);
            if (user == null)
            {
                return null;
            }
            return GetUserResponse(user);
        }

        public bool PostUser(UserRequest user)
        {
            var hash = CreateHashPassword(user.Password);
            var newUser = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Login = user.Login,
                Password = hash,
                Birthday = user.Birthday,
                Role = user.Role,
            };

            Console.WriteLine(newUser);
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return true;
        }

        public bool PutUser(int id, UserRequest user)
        {
            var existingUser = _context.Users.FirstOrDefault(h => h.Id == id);

            existingUser.Login = user.Login;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Birthday = user.Birthday;
            existingUser.Role = user.Role;

            _context.SaveChanges();

            return true;
        }
        public bool DeleteUser(int id)
        {
            var existingUser = _context.Users.FirstOrDefault(h => h.Id == id);

            _context.Users.Remove(existingUser);
            _context.SaveChanges();
           
            return true;
        }

        public UserResponse GetUserResponse(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Login = user.Login,
                Email = user.Email,
                Birthday = user.Birthday,
                Role = user.Role,
            };
        }

        public User GetUserByLogin(string login)
        {
            var user = _context.Users.FirstOrDefault(h => h.Login == login);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public string GetMyName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }

        public string CreateHashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    builder.Append(b.ToString("x2")); // Convert each byte to a hexadecimal string
                }

                return builder.ToString();
            }
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    builder.Append(b.ToString("x2")); // Convert each byte to a hexadecimal string
                }

                string inputHashedPassword = builder.ToString();

                return inputHashedPassword.Equals(hashedPassword, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
