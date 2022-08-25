using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerMusicGroup.Model
{
    public class Songwriter
    {
        [Key]
        public int Id { get; set; }
        public int SongId { get; set; }
        public int AuthorId { get; set; }
        public Song Song { get; set; }
        public Author Author { get; set; }
        public Songwriter()
        {

        }
        public Songwriter(int _id, string _name)
        {
            Author = new Author();
            Author.Name = _name;
            SongId = _id;
        }
    }
}
