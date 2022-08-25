using ManagerMusicGroup.EditModel;
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
using System.Windows.Input;

namespace ManagerMusicGroup.ViewModel
{
    public class SongGroupViewModel : CommandEdit
    {
        string nameViewModel = "SongGroup";
        public static List<Song> songs;
        public static ICollectionView SourceCollectionDetail => GroupCollectionSong.View;
        public static CollectionViewSource GroupCollectionSong;
        public static ObservableCollection<Song> groupListDetail = new ObservableCollection<Song>();
        public static int id;
        private string nameGroup = "";
        const string NAMEFILEREPORT = "music";

        public SongAuthorViewModel SongAuthor { get; set; }
        //public SongAuthorViewModel songAuthor = new SongAuthorViewModel();
        public override void Report()
        {
            string[,] items = new string[1, groupListDetail.Count];

            for (int i = 0; i < items.GetLength(1); i++)
            {
                items[0, i] = groupListDetail[i].Name;
            }

            string[] headerColumns = { "Имя композиции" };
            string header = "Музыкальные композиции группы " + nameGroup;

            ModulReport.WriteReport(header, headerColumns, items, NAMEFILEREPORT);
        }
        public void GetDataDetail(object parameter, string name)
        {
            nameGroup = name;
            id = (int)parameter;
            using (ApplicationContext db = new ApplicationContext())
            {
                songs = db.Songs.Where(p => p.GroupId == id).ToList();
            }

            groupListDetail.Clear();

            foreach (Song item in songs)
            {
                groupListDetail.Add(new Song { Name = item.Name, GroupId = item.GroupId, Id = item.Id });
            }
            GroupCollectionSong = new CollectionViewSource { Source = groupListDetail };
        }

        public void DetailSong(object param)
        {
            SongAuthor.GetDataSong(param);
            foreach (var item in groupListDetail.ToList())
            {
                if(item.Id == (int)param)
                {
                    SongAuthor.SongName = item.Name;
                    break;
                }
            }

            ManagerGroupViewModel.instance.SwitchComand("OpenSongAuthor");   
        }

        protected ICommand _detailSongCommand;
        public ICommand DetailSongCommand
        {
            get
            {
                if (_detailSongCommand == null)
                {
                    _detailSongCommand = new RelayCommand(param => DetailSong(param));
                }
                return _detailSongCommand;
            }
        }

        public SongGroupViewModel()
        {
            NameViewModel = nameViewModel;          
            GroupCollectionSong = new CollectionViewSource { Source = groupListDetail }; 
        }
    }
}
