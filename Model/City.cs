
using System.ComponentModel.DataAnnotations;

public class City
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
}

