namespace Dziennik.Shared.Subject
{
    public class SubjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<int> StudentsId { get; set; } = new List<int>();
        public List<int> MarksId { get; set; } = new List<int>();
        public int TeacherId { get; set; }
    }
}
