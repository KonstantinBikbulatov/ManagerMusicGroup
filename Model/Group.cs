
using ManagerMusicGroup.Model;
using System;
using System.ComponentModel.DataAnnotations;

public class Group
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public DateTime FondationDate { get; set; }
    public string? NameBackTour { get; set; }

    public Group()
    {

    }

    public Group(Group _group)
    {
        Id = _group.Id;
        Name = _group.Name;
        Country = _group.Country;
        FondationDate = _group.FondationDate;
    }
}

