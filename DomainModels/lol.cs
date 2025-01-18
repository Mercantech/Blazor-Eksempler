namespace DomainModels;

public class Champion
{
    public string? Name { get; set; }
    public string? Title { get; set; }
    public List<string>? Classes { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? ImgUrl { get; set; }
    public string? Resource { get; set; }
    public string? RangeType { get; set; }
    public string? Origin { get; set; }
    public string? Region { get; set; }
    public bool ShowDetails { get; set; }

    public string GetClassTypes()
    {
        return Classes != null ? string.Join(", ", Classes) : string.Empty;
    }
}