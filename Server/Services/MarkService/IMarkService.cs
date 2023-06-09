namespace Dziennik.Server.Services.MarkService
{
    public interface IMarkService
    {
        List<MarkResponse> GetMarks();
        MarkResponse GetMark(int? id = null);
        bool PostMark(MarkRequest mark);
        bool PutMark(int id, MarkRequest newMark);
        bool DeleteMark(int id);
        MarkResponse GetMarkResponse(Mark mark);
    }
}
