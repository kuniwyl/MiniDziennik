namespace Dziennik.Client.Service.SubjectService
{
    public interface ISubjectService
    {
        List<SubjectResponse> Subjects { get; set; }
        SubjectResponse Subject { get; set; }
        Task GetSubjects();
        Task GetSubject(int id);
        Task<bool> PostSubject(SubjectRequest subject);
        Task<bool> PutSubject(int id, SubjectRequest subject);
        Task<List<ShortSubjectResponse>> GetShortList();
        Task<bool> DeleteSubject(int id);
        Task<bool> SetTeacher(int id, int teacherId);
        Task<bool> SetStudents(int id, List<int> studentsList);
        SubjectRequest CreateSubjectRequest(SubjectResponse response);
    }
}
