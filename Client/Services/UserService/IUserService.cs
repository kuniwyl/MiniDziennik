namespace Dziennik.Client.Service.StudentService
{
    public interface IUserService
    {
        List<User> Users { get; set; }
        User User { get; set; }
        string CurrentRole { get; set; }
        Task GetUsers(string type);
        Task GetUser(int id, string type);
        Task<List<ShortListResponse>> ShortListResponse(string type);
        Task<bool> PostUser(UserRequest user);
        Task<bool> PutUser(int id, UserRequest user);
        Task<bool> DeleteUser(int id, string type);
        User CreateUserModel(UserResponse response);
        UserRequest CreateUserRequest(User user);
    }
}
