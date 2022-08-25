using System.ComponentModel.DataAnnotations;

namespace ManagerMusicGroup.Model
{
    public class Song
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
