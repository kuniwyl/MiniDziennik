namespace Dziennik.Client.Service.MarkService
{
    public class MarkService : IMarkService
    {
        private readonly HttpClient _httpClient;

        public MarkService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MarkResponse>> GetMarks()
        {
            var response = await _httpClient.GetFromJsonAsync<List<MarkResponse>>("api/mark");
            return response;
        }

        public async Task<MarkResponse> GetMark(int? id = null)
        {
            var url = id.HasValue ? $"api/mark/{id}" : "api/mark";
            var response = await _httpClient.GetFromJsonAsync<MarkResponse>(url);
            return response;
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
                bool result = JsonSerializer.Deserialize<bool>(response.Content.ToString());
                return result;
            }
            return false;
        }
    }
}
