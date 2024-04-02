namespace Blog2023.Data.DTO
{
    public class GetPostListQuery
    {
        public Guid Id { get; set; }
        public string? sortType { get; set; } = null;
        public int Page { get; set; } = 1;
        public int SizePage { get; set; } = 5;
    }
}
