using Dziennik.Client.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Dziennik.Client.Service.StudentService
{
    public class UserService : BaseService, IUserService
    {
        public UserService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, NavigationManager navigation) : base(httpClient, authenticationStateProvider, navigation) { }

        public List<User> Users { get; set; } = new List<User>();
        
        public User User { get; set; } = new User();

        public string CurrentRole { get; set; } = Role.Student;
        
        public async Task GetUsers(string type)
        {
            var token = await GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _httpClient.GetFromJsonAsync<List<UserResponse>>("api/user?&type=" + type);
            if (response != null)
            {
                Users = response.Select(CreateUserModel).ToList();
            }
        }

        public async Task GetUser(int id, string type)
        {
            var token = await GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _httpClient.GetFromJsonAsync<UserResponse>($"api/user/{id}?type=" + type);
            if (response != null)
            {
                User = CreateUserModel(response);
            }
        }

        public async Task<bool> PostUser(UserRequest student)
        {
            var token = await GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _httpClient.PostAsJsonAsync("api/user", student);
            return GetDeserialized(response);
        }

        public async Task<bool> PutUser(int id, UserRequest student)
        {
            var token = await GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _httpClient.PutAsJsonAsync($"api/user/{id}", student);
            return GetDeserialized(response);
        }

        public async Task<bool> DeleteUser(int id, string type)
        {
            var token = await GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _httpClient.DeleteAsync($"api/user/{id}?type=" + type);
            return GetDeserialized(response);
        }

        public User CreateUserModel(UserResponse response)
        {
            return new User()
            {
                Id = response.Id,
                FirstName = response.FirstName,
                LastName = response.LastName,
                Login = response.Login,
                Email = response.Email,
                Password = response.Password,
                Birthday = response.Birthday,
                Role = response.Role,
            };
        }

        public UserRequest CreateUserRequest(User user)
        {
            return new UserRequest()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Login = user.Login,
                Email = user.Email,
                Password = user.Password,
                Birthday = user.Birthday,
                Role = user.Role,
            };
        }

        private bool GetDeserialized(HttpResponseMessage response)
        { 
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<List<ShortListResponse>> ShortListResponse(string type)
        {
            var token = await GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _httpClient.GetFromJsonAsync<List<ShortListResponse>>("api/user/shortList?&type=" + type);
            Console.WriteLine(response);
            if (response != null && response.Count > 0)
            {
                return response.ToList();
            }
            return new List<ShortListResponse>();
        }
    }
}
