using ManagerMusicGroup.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ManagerMusicGroup.ViewModel
{
    public class MoreViewModel
    {
        public Person Young { get; set; }
        public static MoreViewModel instance;

        public static MoreViewModel getInstance()
        {
            if (instance == null)
                instance = new MoreViewModel();
            return instance;
        }
        public static ICollectionView SourceCollectionGroup => GroupsCollection.View;
        public static CollectionViewSource GroupsCollection;

        public static ICollectionView SourceCollectionMusic => GroupCollectionSong.View;
        public static CollectionViewSource GroupCollectionSong;

        private List<Group> groups = new List<Group>();
        private List<List <Person>> groupsAVG = new List<List <Person>>();
        private List <Person> resultAVG = new List <Person>();
        private List<Song> songs = new List<Song>();

        public Group NameGroup { get; set; }

        private MoreViewModel()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var person = db.Persons.Min(a => a.Age);
                Young = db.Persons.Include(r => r.Role).Include(g => g.Group).Where(p => p.Age == person && p.Role.Name == "Вокалист").First();
                groups = db.Groups.Where(p => p.FondationDate.Year % 5 == 0).ToList();
                var id = db.Raitings.First();
                NameGroup = db.Groups.Where(g => g.Id == id.GroupId).First();
                songs = db.Songs.Where(p => p.GroupId == id.GroupId).ToList();
            }
            GroupsCollection = new CollectionViewSource { Source = groups };

            GroupCollectionSong = new CollectionViewSource { Source = songs };
        }
    }
}
