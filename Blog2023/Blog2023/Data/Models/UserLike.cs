namespace Blog2023.Data.Models
{
    public class UserLike
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
    }
}
