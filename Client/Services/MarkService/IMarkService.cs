namespace Dziennik.Client.Service.MarkService
{
    public interface IMarkService
    {
        Task<List<MarkResponse>> GetMarks();
        Task<MarkResponse> GetMark(int? id = null);
        Task<bool> PostMark(MarkRequest mark);
        Task<bool> PutMark(int id, MarkRequest newMark);
        Task<bool> DeleteMark(int id);
    }
}
