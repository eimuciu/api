using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class MessageRepository
    {
        DataContext _context;
        public MessageRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetGroupMessages(string groupname)
        {
            return await _context.Messages.Where(x => x.GroupName.ToLower() == groupname.ToLower()).ToListAsync();

        }
    }
}