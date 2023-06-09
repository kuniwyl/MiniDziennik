using System.Text.Json.Serialization;

namespace Dziennik.Shared.Mark
{
    public class MarkResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("id")]
        public int Value { get; set; } = 0;
        [JsonPropertyName("id")]
        public int Importance { get; set; } = 1;
        [JsonPropertyName("id")]
        public string Title { get; set; } = string.Empty;
        [JsonPropertyName("id")]
        public string Description { get; set; } = string.Empty;
        [JsonPropertyName("id")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [JsonPropertyName("id")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        [JsonPropertyName("id")]
        public int StudentId { get; set; }
        [JsonPropertyName("id")]
        public int TeacherId { get; set; }
        [JsonPropertyName("id")]
        public int SubjectId { get; set; }
    }
}
