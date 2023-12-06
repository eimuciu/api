namespace api.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int SenderId { get; set; }
        public string SenderNickname { get; set; }
        // public User Sender { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        // public Group Group { get; set; }
        public DateTime MessageSent { get; set; } = DateTime.UtcNow;
    }
}