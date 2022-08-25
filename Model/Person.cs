using System;
using System.ComponentModel.DataAnnotations;

public class Person
{
    [Key]
    public int Id { get; set; }
    public int GroupId { get; set; }
    public int RoleId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public Role Role { get; set; }
    public Group Group { get; set; }
    public Person()
    {

    }
    public Person(Person _person)
    {
        Role = new Role();
        Role.Name = _person.Role.Name;
        Role.Id = _person.Role.Id;
    
        Id = _person.Id;
        GroupId = _person.GroupId;
        RoleId = _person.RoleId;
        Name = _person.Name;
        Age = _person.Age;
    }
}

