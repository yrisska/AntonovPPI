using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTO.User
{
    public class UserForRegistration
    {
        [MaxLength(15)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(15)]
        public string LastName { get; set; } = string.Empty;
        public int BirthYear { get; set; }
        [Range(1, 12)]
        public int BirthMonth { get; set; }
        [Range(1, 31)]
        public int BirthDay { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [MinLength(1)]
        public string Password { get; set; } = string.Empty;
        public Models.User ToModel() => new()
        {
            FirstName = FirstName,
            LastName = LastName,
            Birthday = DateOnly.FromDateTime(new DateTime(BirthYear, BirthMonth, BirthDay)),
            Email = Email
        };
    }
}
