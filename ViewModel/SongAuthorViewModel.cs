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
    public class SongAuthorViewModel : CommandEdit
    {
        string nameViewModel = "SongAuthor";
        public List<SongComposer> songComposer;
        public List<Songwriter> songwriter;

        public string SongName  { get; set; }

        public static ICollectionView SourceCollectionAuthor => CollectionAutor.View;
        public static CollectionViewSource CollectionAutor;
        public static ObservableCollection<Songwriter> listAuthor = new ObservableCollection<Songwriter>();

        public static ICollectionView SourceCollectionComposer => CollectionComposer.View;
        public static CollectionViewSource CollectionComposer;
        public static ObservableCollection<SongComposer> listComposer = new ObservableCollection<SongComposer>();
        public static CollectionViewSource CollectionNewItem;
        public static int id;

        public ICollectionView SourceCollectionGroups => CollectionNewItem.View;
        public void GetDataSong(object parameter)
        {
            id = (int)parameter;
            using (ApplicationContext db = new ApplicationContext())
            {
                songComposer = db.SongComposers.Include(p => p.Composer).Where(p => p.SongId == id).ToList();
                songwriter = db.Songwriters.Include(p => p.Author).Where(p => p.SongId == id).ToList();
            }
            listComposer.Clear();
            listAuthor.Clear();
            foreach (SongComposer item in songComposer)
            {
                listComposer.Add(new SongComposer { Composer = item.Composer, Id = item.Id, SongId = item.SongId, ComposerId = item.ComposerId });
            }
            foreach (Songwriter item in songwriter)
            {
                listAuthor.Add(new Songwriter { Author = item.Author, Id = item.Id, Song = item.Song, SongId = item.SongId });
            }
            CollectionComposer = new CollectionViewSource { Source = listComposer };
            CollectionAutor = new CollectionViewSource { Source = listAuthor };
        }

        public void DetailSong(object param)
        {
            ManagerGroupViewModel.instance.SwitchComand("BackSongAuthor");
        }

        protected ICommand _backCommand;
        public ICommand BackCommand
        {
            get
            {
                if (_backCommand == null)
                {
                    _backCommand = new RelayCommand(param => DetailSong(param));
                }
                return _backCommand;
            }
        }

        public SongAuthorViewModel()
        {
            NameViewModel = nameViewModel;
            GetDataSong(1);
        }
    }
}
