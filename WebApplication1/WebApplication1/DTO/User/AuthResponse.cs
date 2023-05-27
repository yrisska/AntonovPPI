namespace WebApplication1.DTO.User
{
    public class AuthResponse
    {
        public bool IsSuccesfull { get; set; }
        public string Token { get; set; } = string.Empty;
        public Guid UserId { get; set; } = Guid.Empty;
    }
}
