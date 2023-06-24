using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;

namespace Dziennik.Client.Service.SubjectService
{
    public class SubjectService : BaseService, ISubjectService
    { 

        public SubjectService(HttpClient httpClient, AuthenticationStateProvider authenticationState, NavigationManager navigation) : base(httpClient, authenticationState, navigation) { }

        public List<SubjectResponse> Subjects { get; set; } = new List<SubjectResponse>();
        public SubjectResponse Subject { get; set; } 

        public async Task GetSubjects()
        {
            var token = await GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _httpClient.GetFromJsonAsync<List<SubjectResponse>>("api/subject");
            if (response != null && response.Count > 0)
            {
                Subjects = response.ToList();    
            }
        }

        public async Task GetSubject(int id)
        {
            var token = await GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _httpClient.GetFromJsonAsync<SubjectResponse>($"api/subject/{id}");
            Subject = response;
        }

        public async Task<bool> PostSubject(SubjectRequest subject)
        {
            var token = await GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _httpClient.PostAsJsonAsync("api/subject", subject);
            return GetDeserialized(response);
        }

        public async Task<bool> PutSubject(int id, SubjectRequest subject)
        {
            var token = await GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _httpClient.PutAsJsonAsync($"api/subject/{id}", subject);
            return GetDeserialized(response);
        }

        public async Task<bool> DeleteSubject(int id)
        {
            var token = await GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _httpClient.DeleteAsync($"api/subject/{id}");
            return GetDeserialized(response);
        }

        public async Task<bool> SetTeacher(int id, int teacherId)
        {
            var token = await GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var requestContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsJsonAsync($"api/subject/{id}/setteacher/{teacherId}", requestContent);
            return GetDeserialized(response);
        }

        public async Task<bool> SetStudents(int id, List<int> studentsList)
        {
            var token = await GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _httpClient.PutAsJsonAsync($"api/subject/{id}/setstudents", studentsList);
            return GetDeserialized(response);
        }

        public async Task<List<ShortSubjectResponse>> GetShortList()
        {
            var token = await GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _httpClient.GetFromJsonAsync<List<ShortSubjectResponse>>($"api/subject/shortList");
            return response;
        }

        private bool GetDeserialized(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public SubjectRequest CreateSubjectRequest(SubjectResponse response)
        {
            return new SubjectRequest
            {
                Id = response.Id,
                Name = response.Name,
                Description = response.Description,
                TeacherId = response.TeacherId,
                StudentsId = response.StudentsId,
            };
        }
    }
}
