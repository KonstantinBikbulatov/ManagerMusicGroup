using System.ComponentModel.DataAnnotations;

public class Raiting
{
    [Key]
    public int Id { get; set; }
    public int  GroupId { get; set; }
    public Group Group { get; set; }
}

