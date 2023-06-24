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

        public List<UserResponse> GetUsers(string type)
        {
            if (type == null)
            {
                return null;
            } 
            else if (type == Role.Student)
            {
               return _context.Students.Select(GetUserResponse).ToList();
            }
            else if (type == Role.Teacher)
            {
                return _context.Teachers.Select(GetUserResponse).ToList();
            } 
            else
            {
                return _context.HeadAdmins.Select(GetUserResponse).ToList();
            }
        }

        public UserResponse GetUser(int id, string type)
        {
            User user;
            if (type == null)
            {
                return null;
            }
            else if (type == Role.Student)
            {
                user = _context.Students.FirstOrDefault(h => h.Id == id);
            }
            else if (type == Role.Teacher)
            {
                user = _context.Teachers.FirstOrDefault(h => h.Id == id);
            }
            else
            {
                user = _context.HeadAdmins.FirstOrDefault(h => h.Id == id);
            }
            if (user == null)
            {
                return null;
            }
            return GetUserResponse(user);
        }

        public bool PostUser(UserRequest user)
        {
            var hash = CreateHashPassword(user.Password);
            if (user.Role == Role.Student)
            {
                var newUser = new Student()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Login = user.Login,
                    Password = hash,
                    Birthday = user.Birthday.ToUniversalTime(),
                    Role = user.Role,
                };
                _context.Students.Add(newUser);
            }
            else if (user.Role == Role.Teacher)
            {
                var newUser = new Teacher()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Login = user.Login,
                    Password = hash,
                    Birthday = user.Birthday.ToUniversalTime(),
                    Role = user.Role,
                };
                _context.Teachers.Add(newUser);
            }
            else 
            {
                var newUser = new HeadAdmin()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Login = user.Login,
                    Password = hash,
                    Birthday = user.Birthday.ToUniversalTime(),
                    Role = user.Role,
                };
                _context.HeadAdmins.Add(newUser);
            }
            _context.SaveChanges();

            return true;
        }

        public bool PutUser(int id, UserRequest user)
        {
            if (user.Role == Role.Student)
            {
                var existingUser = _context.Students.FirstOrDefault(h => h.Id == id);
                existingUser.Login = user.Login;
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.Birthday = user.Birthday.ToUniversalTime();
                existingUser.Role = user.Role;
            }
            else if (user.Role == Role.Teacher)
            {
                var existingUser = _context.Teachers.FirstOrDefault(h => h.Id == id);
                existingUser.Login = user.Login;
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.Birthday = user.Birthday.ToUniversalTime();
                existingUser.Role = user.Role;
            }
            else
            {
                var existingUser = _context.HeadAdmins.FirstOrDefault(h => h.Id == id);
                existingUser.Login = user.Login;
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.Birthday = user.Birthday.ToUniversalTime();
                existingUser.Role = user.Role;
            }
            _context.SaveChanges();

            return true;
        }
        public bool DeleteUser(int id, string type)
        {
            if (type == Role.Student)
            {
                var existingUser = _context.Students.FirstOrDefault(h => h.Id == id);
                _context.Students.Remove(existingUser);
            }
            else if (type == Role.Teacher)
            {
                var existingUser = _context.Teachers.FirstOrDefault(h => h.Id == id);
                _context.Teachers.Remove(existingUser);
            }
            else
            {
                var existingUser = _context.HeadAdmins.FirstOrDefault(h => h.Id == id);
                _context.HeadAdmins.Remove(existingUser);
            }
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
            var stud = _context.Students.FirstOrDefault(h => h.Login == login);
            var teach = _context.Teachers.FirstOrDefault(h => h.Login == login);
            var admin = _context.HeadAdmins.FirstOrDefault(h => h.Login == login);
            if (stud != null)
            {
                return stud;
            } 
            else if (teach != null)
            {
                return teach;
            }
            else if (admin != null)
            {
                return admin;
            }
            return null;
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

        public List<ShortListResponse> GetShortListResponse(string type)
        {
            var users = new List<ShortListResponse>();
            if (type == Role.Student)
            {
                users = _context.Students.Select(CreateShortList).ToList();
            }
            else if (type == Role.Teacher)
            {
                users = _context.Teachers.Select(CreateShortList).ToList();
            }
            else if (type == Role.HeadAdmin)
            {
                users = _context.HeadAdmins.Select(CreateShortList).ToList();
            }
            
            if (users.Count == 0)
            {
                return null;
            }
            return users;
        }

        private ShortListResponse CreateShortList(User user)
        {
            return new ShortListResponse
            {
                Id = user.Id,
                FullName = user.FirstName + " " + user.LastName,
                Login = user.Login,
            };
        }
    }
}
