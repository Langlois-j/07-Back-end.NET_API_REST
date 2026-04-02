namespace Dot.Net.WebApi.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string? Fullname { get; set; }
        public string? Role { get; set; }
    }
}