namespace Dziennik.Client.Service.SubjectService
{
    public interface ISubjectService
    {
        Task<List<SubjectResponse>> GetSubjects();
        Task<SubjectResponse> GetSubject(int id);
        Task<bool> PostSubject(SubjectRequest subject);
        Task<bool> PutSubject(int id, SubjectRequest subject);
        Task<bool> DeleteSubject(int id);
        Task<bool> SetTeacher(int id, int teacherId);
        Task<bool> SetStudents(int id, List<int> studentsList);
    }
}
