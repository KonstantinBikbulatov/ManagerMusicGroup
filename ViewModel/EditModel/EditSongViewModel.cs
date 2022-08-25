using ManagerMusicGroup.EditModel;
using ManagerMusicGroup.Model;
using ManagerMusicGroup.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ManagerMusicGroup.ViewModel
{
    public class EditSongViewModel : SongGroupViewModel
    {
        Song newSong = new Song();
        List <Song> deleteSongList = new List<Song>();
        public List<Song> NewSongList = new List<Song>();
        ConfirmAddSongView confirm;
        public Song NewSong
        {
            get { return newSong; }
            set { newSong = value; }
        }

        string nameViewModel = "EditSongGroup";
        public override void Save()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var item in deleteSongList)
                {
                    db.Songs.Remove(item);
                }
                deleteSongList.Clear();
                foreach (var item in groupListDetail.ToList())
                {
                    if (item.Id == 0)
                    {
                        item.GroupId = id;
                        db.Songs.Add(item);
                        continue;
                    }
                    db.Songs.Update(item);
                }
                db.SaveChanges();
            }
            base.Save();
        }
        public override void Cancel()
        {
            foreach (var person in NewSongList)
            {
                groupListDetail.Remove(person);
            }
            foreach (var item in deleteSongList)
            {
                groupListDetail.Add(item);
            }
            NewSongList.Clear();
            deleteSongList.Clear();
        }
        public override void Delete(object param)
        {
            var item = groupListDetail.ToList().Find(x => x.Name.Contains((string)param));

            if (item == null)
            {
                item = NewSongList.Find(x => x.Name.Contains((string)param));
            }

            var itemDb = db.Songs.Find(item.Id);
            if(itemDb != null)
            {
                deleteSongList.Add(itemDb);
            }

            groupListDetail.Remove(item);
            db.SaveChanges();
        }
        public override void Add()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var a = db.Songs.Where(s => s.Name == newSong.Name).FirstOrDefault();
                if (a == null)
                {
                    Song song = new Song();
                    song.GroupId = id;
                    song.Name = newSong.Name;
                    groupListDetail.Add(song);
                    NewSongList.Add(song);
                }
                else
                {
                    confirm = new ConfirmAddSongView();
                    confirm.Owner = Application.Current.MainWindow;
                    confirm.ShowDialog();
                }
            }
        }
        public EditSongViewModel()
        {
            NameViewModel = nameViewModel;
        }
    }
}
