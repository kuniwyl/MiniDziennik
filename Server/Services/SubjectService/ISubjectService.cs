namespace Dziennik.Server.Services.SubjectService
{
    public interface ISubjectService
    {
        List<SubjectResponse> GetSubjects();
        SubjectResponse GetSubject(int id);
        List<ShortSubjectResponse> GetShortSubjects();
        bool PostSubject(SubjectRequest subject);
        bool PutSubject(int id, SubjectRequest subject);
        bool DeleteSubject(int id);
        bool SetTeacher(int id, int teacherId);
        bool SetStudents(int id, List<int> studentsList);
        SubjectResponse GetSubjectResponse(Subject subject);
    }
}
