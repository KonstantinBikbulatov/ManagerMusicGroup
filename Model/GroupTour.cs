using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerMusicGroup.Model
{
    public class GroupTour
    {
        [Key]
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int CityId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public Group Group { get; set; }
        public City City { get; set; }
        [NotMapped]
        public int Price { get; set; }
    }
}
