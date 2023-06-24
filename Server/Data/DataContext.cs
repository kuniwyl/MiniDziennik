using Dziennik.Shared;

namespace MiniDziennik.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Mark> Marks { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<HeadAdmin> HeadAdmins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Subjects)
                .WithMany(c => c.Students)
                .UsingEntity(j => j.ToTable("StudentSubject"));

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Marks)
                .WithOne(s => s.Student)
                .HasForeignKey(s => s.StudentId);

            modelBuilder.Entity<Teacher>()
                .HasMany(s => s.Subjects)
                .WithMany(s => s.Teachers)
                .UsingEntity(j => j.ToTable("TeachersSubject"));

            modelBuilder.Entity<Teacher>()
                .HasMany(s => s.Marks)
                .WithOne(s => s.Teacher)
                .HasForeignKey(s => s.TeacherId);

            modelBuilder.Entity<Mark>()
                .HasOne(s => s.Subject)
                .WithMany(s => s.Marks)
                .HasForeignKey(j => j.SubjectId);
        }
    }
}
