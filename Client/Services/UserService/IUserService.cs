namespace Dziennik.Client.Service.StudentService
{
    public interface IUserService
    {
        List<User> Users { get; set; }
        User User { get; set; }
        Task GetUsers();
        Task GetUser(int id);
        Task<bool> PostUser(UserRequest user);
        Task<bool> PutUser(int id, UserRequest user);
        Task<bool> DeleteUser(int id);
        User CreateUserModel(UserResponse response);
        UserRequest CreateUserRequest(User user);
    }
}
