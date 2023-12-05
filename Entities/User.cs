namespace api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public DateTime Joined { get; set; } = DateTime.UtcNow;
        public bool IsGuest { get; set; }
    }
}