namespace Blog2023.Data.DTO
{
    public class AdministratorDTO
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
