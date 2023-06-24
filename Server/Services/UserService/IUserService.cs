namespace Dziennik.Server.Services.UserService
{
    public interface IUserService
    {
        List<UserResponse> GetUsers(string type);
        UserResponse GetUser(int id, string type);
        User GetUserByLogin(string login);
        bool PostUser(UserRequest Teacher);
        bool PutUser(int id, UserRequest Teacher);
        bool DeleteUser(int id, string type);
        UserResponse GetUserResponse(User user);
        List<ShortListResponse> GetShortListResponse(string type);
        string CreateHashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
