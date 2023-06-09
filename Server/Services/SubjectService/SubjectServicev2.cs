namespace Dziennik.Server.Services.SubjectService
{
    public class SubjectServicev2 : BaseService, ISubjectService
    {
        public SubjectServicev2(DataContext context, IHttpContextAccessor accessor) : base(context, accessor) { }

        public List<SubjectResponse> GetSubjects()
        {
            var subjects = _context.Subjects.ToList();
            var subjectResponses = subjects.Select(s => GetSubjectResponse(s)).ToList();

            return subjectResponses;
        }

        public SubjectResponse GetSubject(int id)
        {

            var subject = _context.Subjects.FirstOrDefault(s => s.Id == id);
            var subjectResponse = GetSubjectResponse(subject);

            return subjectResponse;
        }

        public bool PostSubject(SubjectRequest subject)
        {

            var newSubject = new Subject
            {
                Id = subject.Id,
                Name = subject.Name,
                Description = subject.Description,
                Teacher = _context.Users.FirstOrDefault(s => s.Id == subject.TeacherId),
                Students = _context.Users.Where(s => subject.StudentsId.Contains(s.Id)).ToList(),
            };

            _context.Subjects.Add(newSubject);
            _context.SaveChanges();

            return true;
        }

        public bool PutSubject(int id, SubjectRequest subject)
        {
            var existingSubject = _context.Subjects.FirstOrDefault(s => s.Id == id);
            if (existingSubject == null)
            {
                return false;
            }

            existingSubject.Name = subject.Name;
            existingSubject.Description = subject.Description;
            existingSubject.Teacher = _context.Users.FirstOrDefault(s => s.Id == subject.TeacherId);
            existingSubject.Students = _context.Users.Where(s => subject.StudentsId.Contains(s.Id)).ToList();

            _context.SaveChanges();

            return false;
        }

        public bool DeleteSubject(int id)
        {
            var subject = _context.Subjects.FirstOrDefault(s => s.Id == id);
            if (subject == null)
            { 
                return false;
            }

            _context.Subjects.Remove(subject);
            _context.SaveChanges();

            return false;
        }

        public bool SetTeacher(int id, int teacherId)
        {
            var subject = _context.Subjects.FirstOrDefault(s => s.Id == id);
            if (subject == null)
            {
                return false;
            }

            subject.Teacher = _context.Users.FirstOrDefault(s => s.Id == teacherId);
            _context.SaveChanges();

            return true;
        }

        public bool SetStudents(int id, List<int> studentsList)
        {
            var subject = _context.Subjects.FirstOrDefault(s => s.Id == id);
            if (subject == null)
            {
                return false;
            }

            subject.Students = _context.Users.Where(s => studentsList.Contains(s.Id)).ToList();
            _context.SaveChanges();

            return true;
        }

        public SubjectResponse GetSubjectResponse(Subject subject)
        {
            return new SubjectResponse
            {
                Id = subject.Id,
                Name = subject.Name,
                Description = subject.Description,
                TeacherId = subject.Teacher.Id,
                StudentsId = subject.Students.Select(s => s.Id).ToList(),
            };
        }
    }
}
