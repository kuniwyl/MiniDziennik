namespace Dziennik.Server.Services.MarkService
{
    public class MarkServicev2 : BaseService, IMarkService
    {

        public MarkServicev2(DataContext context, IHttpContextAccessor httpContext) : base(context, httpContext) { }

        public List<MarkResponse> GetMarks()
        {
            var marks = _context.Marks.ToList();
            var markResponses = marks.Select(GetMarkResponse).ToList();

            return markResponses;
        }

        public MarkResponse GetMark(int? id = null)
        {
            var mark = _context.Marks.FirstOrDefault(m => m.Id == id);
            if (mark == null)
            {
                return null;
            }

             return GetMarkResponse(mark);
        }

        public bool PostMark(MarkRequest mark)
        {
            var student = _context.Users.FirstOrDefault(s => s.Id == mark.StudentId);
            var teacher = _context.Users.FirstOrDefault(t => t.Id == mark.TeacherId);
            var subject = _context.Subjects.FirstOrDefault(s => s.Id == mark.SubjectId);

            if (student == null || teacher == null || subject == null)
            {
                return false;
            }

            var newMark = new Mark
            {
                Id = mark.Id,
                Value = mark.Value,
                Importance = mark.Importance,
                Title = mark.Title,
                Description = mark.Description,
                CreatedAt = mark.CreatedAt,
                UpdatedAt = mark.UpdatedAt,
                Student = student,
                Teacher = teacher,
                Subject = subject,
            };

            _context.Marks.Add(newMark);
            _context.SaveChanges();

            return true;
        }

        public bool PutMark(int id, MarkRequest newMark)
        {
            var mark = _context.Marks.FirstOrDefault(m => m.Id == id);
            if (mark == null)
            { 
                return false;
            }

            var student = _context.Users.FirstOrDefault(s => s.Id == newMark.StudentId);
            var teacher = _context.Users.FirstOrDefault(t => t.Id == newMark.TeacherId);
            var subject = _context.Subjects.FirstOrDefault(s => s.Id == newMark.SubjectId);

            if (student == null || teacher == null || subject == null)
            { 
                return false;
            }

            mark.Value = newMark.Value;
            mark.Importance = newMark.Importance;
            mark.Title = newMark.Title;
            mark.Description = newMark.Description;
            mark.UpdatedAt = newMark.UpdatedAt;
            mark.Student = student;
            mark.Teacher = teacher;
            mark.Subject = subject;

            _context.SaveChanges();


            return true;
        }

        public bool DeleteMark(int id)
        {
            var mark = _context.Marks.FirstOrDefault(m => m.Id == id);
            if (mark == null)
            { 
                return false;
            }

            _context.Marks.Remove(mark);
            _context.SaveChanges();

            return true;
        }

        public MarkResponse GetMarkResponse(Mark mark)
        {
            return new MarkResponse
            {
                Id = mark.Id,
                Value = mark.Value,
                Importance = mark.Importance,
                Title = mark.Title,
                Description = mark.Description,
                CreatedAt = mark.CreatedAt,
                UpdatedAt = mark.UpdatedAt,
                StudentId = mark.Student.Id,
                TeacherId = mark.Student.Id,
                SubjectId = mark.Student.Id
            };
        }
    }
}
