using System.ComponentModel.DataAnnotations;

public class Composer
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}

