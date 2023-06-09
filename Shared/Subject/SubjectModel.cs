namespace Dziennik.Shared.Subject
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<User.User> Students { get; set; }
        public User.User Teacher { get; set; }
    }
}
