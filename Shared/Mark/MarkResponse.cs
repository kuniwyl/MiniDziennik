using System.Text.Json.Serialization;

namespace Dziennik.Shared.Mark
{
    public class MarkResponse
    {
        //[JsonPropertyName("id")]
        public int Id { get; set; }
        //[JsonPropertyName("value")]
        public int Value { get; set; } = 1;
        //[JsonPropertyName("importance")]
        public int Importance { get; set; } = 1;
        //[JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        //[JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        //[JsonPropertyName("createAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        //[JsonPropertyName("updateAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        //[JsonPropertyName("studentId")]
        public int StudentId { get; set; }
        //[JsonPropertyName("teacherId")]
        public int TeacherId { get; set; }
        //[JsonPropertyName("subjectId")]
        public int SubjectId { get; set; }
    }
}
