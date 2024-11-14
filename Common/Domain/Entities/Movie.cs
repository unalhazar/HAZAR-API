namespace Domain.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public string Genres { get; set; }
    public int? ReleaseYear { get; set; }
    public string ImdbId { get; set; }
    public double? ImdbAverageRating { get; set; } // Nullable yapıldı
    public int ImdbNumVotes { get; set; }
    public string AvailableCountries { get; set; }
}
