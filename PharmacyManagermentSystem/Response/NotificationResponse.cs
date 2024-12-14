namespace PharmacyManagermentSystem.Response
{
    public class NotificationResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Isread { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UserId { get; set; }
    }
}
