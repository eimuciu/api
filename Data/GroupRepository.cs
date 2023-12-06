using Microsoft.EntityFrameworkCore;
using api.Entities;

namespace api.Data
{
    public class GroupRepository
    {
        DataContext _context;
        public GroupRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Group>> GetAllGroups()
        {
            return await _context.Groups.ToListAsync();
        }
    }
}