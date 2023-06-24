namespace Dziennik.Shared.User
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; }
        public DateTime Birthday { get; set; } = DateTime.Now;
        public string Role { get; set; }
        public List<Subject.Subject> Subjects { get; set; } = new List<Subject.Subject>();
        public List<Mark.Mark> Marks { get; set; } = new List<Mark.Mark>();
    }


}
