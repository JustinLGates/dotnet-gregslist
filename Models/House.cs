using System.ComponentModel.DataAnnotations;

namespace summer2020_dotnet_gregslist.Models
{
  public class House
  {
    public int Id { get; set; }
    public string UserId { get; set; }
    [Required]
    public string Make { get; set; }
    [Required]
    public string Bedrooms { get; set; }
    [Required]
    public string Bathrooms { get; set; }
    [Required]
    public int Year { get; set; }
    [Required]
    public int Price { get; set; }
    [Required]
    public string ImgUrl { get; set; }
    [Required]
    public string Body { get; set; }
  }
}

public class ViewModelHouseFavorite : House
{
  public int FavoriteId { get; set; }
}
}
}