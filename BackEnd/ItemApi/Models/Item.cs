namespace ItemApi.Models
{
    public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // Default value
    public string Description { get; set; } = string.Empty; // Default value

    // Alternatively, you could create a constructor
    public Item()
    {
        Name = string.Empty;
        Description = string.Empty;
    }
}

}
