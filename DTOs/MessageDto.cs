namespace api.DTOs
{
    public class MessageDto
    {
        public string Content { get; set; }
        public int SenderId { get; set; }
        public string SenderNickname { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }
}