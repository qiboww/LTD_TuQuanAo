using StyleWardrobe.Api.Models.Enums;

namespace StyleWardrobe.Api.Models;

public class Clothes
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }

    public string Name { get; set; } = string.Empty;
    public ClothesType Type { get; set; }
    public ClothesColor Color { get; set; }
    public ClothesStyle? Style { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
