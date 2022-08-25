using System.ComponentModel.DataAnnotations;

namespace ManagerMusicGroup.Model
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
