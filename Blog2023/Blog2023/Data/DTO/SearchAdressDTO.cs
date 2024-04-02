namespace Blog2023.Data.DTO
{
    public class SearchAdressDTO
    {
        public long objectId { get; set; }
        public Guid objectGuid { get; set; }
        public string text { get; set; }
        public string objectLevel { get; set; }
        public string objectLevelText { get; set; }
        public string[] path { get; set; }
    }
}
