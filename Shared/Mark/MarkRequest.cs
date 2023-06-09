namespace Dziennik.Shared.Mark
{
    public class MarkRequest
    {
        public int Id { get; set; }
        public int Value { get; set; } = 0;
        public int Importance { get; set; } = 1;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
    }
}
