using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerMusicGroup.Model
{
    public class SongComposer
    {
        [Key]
        public int Id { get; set; }
        public int SongId { get; set; }
        public int ComposerId { get; set; }
        public Song Song { get; set; }
        public Composer Composer { get; set; }
        public SongComposer()
        {

        }
        public SongComposer(int _id, string _name)
        {
            Composer = new Composer();
            Composer.Name = _name;
            SongId = _id;
        }
    }
}
