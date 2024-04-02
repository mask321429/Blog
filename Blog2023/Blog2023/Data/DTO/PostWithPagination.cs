namespace Blog2023.Data.DTO
{
    public class PostWithPagination
    {
        public List<PostDTO> Posts { get; set; }
        public PageInfoModel PaginationInfo { get; set; }
    }
}
