//REFIT
/*
 [Get(url)]
Task<ApiResponse<Object>> NAzwaMetody([Body] request)
 */

namespace Dziennik.Client.Service.MarkService
{
    public class MarkService : IMarkService
    {
        private readonly HttpClient _httpClient;

        public MarkService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<MarkResponse> Marks { get; set; } = new List<MarkResponse>();
        public MarkResponse Mark { get; set; } = new MarkResponse();

        public async Task GetMarks()
        {
            var response = await _httpClient.GetFromJsonAsync<List<MarkResponse>>("api/mark");
            Marks = response;
        }

        public async Task GetMark(int? id = null)
        {
            var url = id.HasValue ? $"api/mark/{id}" : "api/mark";
            var response = await _httpClient.GetFromJsonAsync<MarkResponse>(url);
            Mark = response;
        }

        public async Task<bool> PostMark(MarkRequest mark)
        {
            var response = await _httpClient.PostAsJsonAsync("api/mark", mark);
            return GetDeserialized(response);
        }

        public async Task<bool> PutMark(int id, MarkRequest newMark)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/mark/{id}", newMark);
            return GetDeserialized(response);
        }

        public async Task<bool> DeleteMark(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/mark/{id}");
            return GetDeserialized(response);
        }

        private bool GetDeserialized(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
