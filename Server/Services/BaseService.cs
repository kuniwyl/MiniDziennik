public class BaseService
{
    protected readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly DataContext _context;

    public BaseService(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }
}
