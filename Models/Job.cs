using System.ComponentModel.DataAnnotations;

namespace summer2020_dotnet_gregslist.Models
{
  public class Job
  {
    public int Id { get; set; }
    public string UserId { get; set; }
    [Required]
    public int Pay { get; set; }
    [Required]
    public string Body { get; set; }
  }
  public class ViewModelJobFavorite : Job
  {
    public int FavoriteId { get; set; }
  }
}
