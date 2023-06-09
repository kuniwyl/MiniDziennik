namespace Dziennik.Client.Service.SubjectService
{
    public class SubjectService : ISubjectService
    {
        private readonly HttpClient _httpClient;

        public SubjectService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SubjectResponse>> GetSubjects()
        {
            var response = await _httpClient.GetFromJsonAsync<List<SubjectResponse>>("api/subject");
            return response;
        }

        public async Task<SubjectResponse> GetSubject(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<SubjectResponse>($"api/subject/{id}");
            return response;
        }

        public async Task<bool> PostSubject(SubjectRequest subject)
        {
            var response = await _httpClient.PostAsJsonAsync("api/subject", subject);
            return GetDeserialized(response);
        }

        public async Task<bool> PutSubject(int id, SubjectRequest subject)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/subject/{id}", subject);
            return GetDeserialized(response);
        }

        public async Task<bool> DeleteSubject(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/subject/{id}");
            return GetDeserialized(response);
        }

        public async Task<bool> SetTeacher(int id, int teacherId)
        {
            var requestContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsJsonAsync($"api/subject/{id}/setteacher/{teacherId}", requestContent);
            return GetDeserialized(response);
        }

        public async Task<bool> SetStudents(int id, List<int> studentsList)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/subject/{id}/setstudents", studentsList);
            return GetDeserialized(response);
        }

        private bool GetDeserialized(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                bool result = JsonSerializer.Deserialize<bool>(response.Content.ToString());
                return result;
            }
            return false;
        }
    }
}
