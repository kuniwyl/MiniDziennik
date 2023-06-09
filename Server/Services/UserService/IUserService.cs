namespace Dziennik.Server.Services.UserService
{
    public interface IUserService
    {
        List<UserResponse> GetUsers();
        UserResponse GetUser(int id);
        User GetUserByLogin(string login);
        bool PostUser(UserRequest Teacher);
        bool PutUser(int id, UserRequest Teacher);
        bool DeleteUser(int id);
        UserResponse GetUserResponse(User user);
        string CreateHashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
