namespace StyleWardrobe.Api.Models;

public class User
{
    public int Id { get; set; }
    public string FirebaseUid { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Clothes> Clothes { get; set; } = new List<Clothes>();
}
