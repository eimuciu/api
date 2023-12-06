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

        public async void GetGroupMessages(string nickname, string groupname)
        {
            User userData = await _context.Users.FirstOrDefaultAsync(x => x.Nickname.ToLower() == nickname.ToLower());
            Group groupData = await _context.Groups.FirstOrDefaultAsync(x => x.Name.ToLower() == groupname.ToLower());

            // await _context.Messages.Include(x => x.GroupId).FirstOrDefaultAsync

        }
    }
}