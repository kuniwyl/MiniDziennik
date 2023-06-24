namespace Dziennik.Shared.Mark
{
    public class Mark
    {
        public int Id { get; set; }
        public int Value { get; set; } = 0;
        public int Importance { get; set; } = 1;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int StudentId { get; set; }
        public Student Student { get; set; } = new Student();

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = new Teacher();

        public int SubjectId { get; set; }
        public Subject.Subject Subject { get; set; } = new Subject.Subject();
    }
}
