namespace Dziennik.Server.Services.SubjectService
{
    public class SubjectServicev2 : BaseService, ISubjectService
    {
        public SubjectServicev2(DataContext context, IHttpContextAccessor accessor) : base(context, accessor) { }

        public async Task<List<SubjectResponse>> GetSubjects(CancellationToken cancellationToken)
        {
            List<Subject> subjects;
            try
            {
                 subjects = await _context.Subjects.Include(s => s.Teachers).Include(s => s.Students).ToListAsync(cancellationToken);
                var x = _context.Subjects.AsQueryable();
                var s = _context.Subjects
                    .Select(x => GetSubjectResponse(x)).ToList();
            }
            catch (OperationCanceledException e)
            {
                return null;
            }
            
            var subjectResponses = subjects.Select(s => GetSubjectResponse(s)).ToList();

            return subjectResponses;
        }

        public SubjectResponse GetSubject(int id)
        {
            var subject = _context.Subjects
                .Include(s => s.Teachers)
                .Include(s => s.Students)
                .FirstOrDefault(s => s.Id == id);
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
                Teachers = _context.Teachers.Where(s => s.Id == subject.TeacherId).ToList(),
                Students = _context.Students.Where(s => subject.StudentsId.Contains(s.Id)).ToList(),
            };

            _context.Subjects.Add(newSubject);
            _context.SaveChanges();

            return true;
        }

        public bool PutSubject(int id, SubjectRequest subject)
        {
            var existingSubject = _context.Subjects
                .Include(s => s.Teachers)
                .Include(s => s.Students)
                .FirstOrDefault(s => s.Id == id);
            if (existingSubject == null)
            {
                return false;
            }

            existingSubject.Name = subject.Name;
            existingSubject.Description = subject.Description;
            existingSubject.Teachers = _context.Teachers.Where(s => s.Id == subject.TeacherId).ToList();

            var existingStudentIds = existingSubject.Students.Select(s => s.Id).ToList();
            var studentsToRemove = existingStudentIds.Except(subject.StudentsId).ToList();
            foreach (var studentId in studentsToRemove)
            {
                var studentToRemove = existingSubject.Students.FirstOrDefault(s => s.Id == studentId);
                if (studentToRemove != null)
                {
                    existingSubject.Students.Remove(studentToRemove);
                }
            }

            var newStudentIds = subject.StudentsId.Except(existingStudentIds).ToList();
            var studentsToAdd = _context.Students.Where(s => newStudentIds.Contains(s.Id)).ToList();
            existingSubject.Students.AddRange(studentsToAdd);

            _context.SaveChanges();

            return true;
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

            subject.Teachers = _context.Teachers.Where(s => s.Id == teacherId).ToList();
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

            subject.Students = _context.Students.Where(s => studentsList.Contains(s.Id)).ToList();
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
                TeacherId = subject.Teachers.Select(s => s.Id).ToList().FirstOrDefault(),
                StudentsId = subject.Students.Select(s => s.Id).ToList(),
            };
        }

        public List<ShortSubjectResponse> GetShortSubjects()
        {
            var subjects = _context.Subjects.ToList();
            var subjectResponses = subjects.Select(s => GetShortListResponse(s)).ToList();
            return subjectResponses;
        }

        private ShortSubjectResponse GetShortListResponse(Subject subject)
        {
            return new ShortSubjectResponse
            {
                Id = subject.Id,
                Name = subject.Name,
            };
        }
    }
}
