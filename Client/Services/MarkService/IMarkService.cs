namespace Dziennik.Client.Service.MarkService
{
    public interface IMarkService
    {
        List<MarkResponse> Marks { get; set; }
        MarkResponse Mark { get; set; }
        Task GetMarks();
        Task GetMark(int? id = null);
        Task<bool> PostMark(MarkRequest mark);
        Task<bool> PutMark(int id, MarkRequest newMark);
        Task<bool> DeleteMark(int id);
    }
}
