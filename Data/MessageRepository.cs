using api.DTOs;
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

        public async Task<Message> AddNewMessage(MessageDto msg)
        {
            Message message = new Message
            {
                Content = msg.Content,
                SenderId = msg.SenderId,
                SenderNickname = msg.SenderNickname,
                GroupId = msg.GroupId,
                GroupName = msg.GroupName
            };
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }
    }
}