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
    public class SearchViewModel
    {
        public static ICollectionView SourceCollectionDetail => GroupsCollectionDetail.View;
        public static CollectionViewSource GroupsCollectionDetail;
        static NavGroupViewModel nav;
        public static ObservableCollection<Song> ListSongs = new ObservableCollection<Song>();
        public static ObservableCollection<Person> ListPersons = new ObservableCollection<Person>();
        public static ObservableCollection<Composer> ListComposers = new ObservableCollection<Composer>();
        public static ObservableCollection<Author> ListAuthors = new ObservableCollection<Author>();
        public static SearchViewModel instance;
        public string TextSearch { get; set; }

        private string chooseMode = "";

        object selectViewModel;

        List <Song> songs;
        Person persons;
        Composer composers;
        Author authors;

        ApplicationContext db = new ApplicationContext();

        private void SearchSongs()
        {
            ListSongs.Clear();
            songs = db.Songs.Where(p => EF.Functions.Like(p.Name, "%"+TextSearch+"%")).ToList();
            foreach (Song item in songs)
            {
                ListSongs.Add(new Song { Name = item.Name });
            }
            GroupsCollectionDetail = new CollectionViewSource { Source = ListSongs };
        }
        private Person SearchPersons(object param)
        {
            return (Person)db.Persons.Where(p => EF.Functions.Like(p.Name, (string)param));
        }
        private Composer SearchComposers(object param)
        {
            return (Composer)db.Composers.Where(p => EF.Functions.Like(p.Name, (string)param));
        }
        private Author SearchAuthors(object param)
        {
            return (Author)db.Authors.Where(p => EF.Functions.Like(p.Name, (string)param));
        }

        public static SearchViewModel getInstance(NavGroupViewModel _nav)
        {
            nav = _nav;
            if (instance == null)
                instance = new SearchViewModel();
            return instance;
        }

        private void SwitchComand(object param)
        {
            switch (param)
            {
                case "Song":
                    SearchSongs();
                    chooseMode = "Song";
                    break;
                case "Person":
                    persons = SearchPersons(param);
                    GroupsCollectionDetail = new CollectionViewSource { Source = persons };
                    break;
                case "Composer":
                    composers = SearchComposers(param);
                    GroupsCollectionDetail = new CollectionViewSource { Source = composers };
                    break;
                case "Author":
                    authors = SearchAuthors(param);
                    GroupsCollectionDetail = new CollectionViewSource { Source = authors };
                    break;
                default:
                    break;
            }   
        }

        protected ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(param => SwitchComand(param));
                }
                return _searchCommand;
            }
        }

        public void SwitchFollowComand(object param)
        {
            switch (chooseMode)
            {
                case "Song":
                    nav.SelectedViewModel = ManagerGroupViewModel.getInstance();
                    break;
                default:
                    break;
            }
        }

        protected ICommand _followCommand;
        public ICommand FollowCommand
        {
            get
            {
                if (_followCommand == null)
                {
                    _followCommand = new RelayCommand(param => SwitchFollowComand(param));
                }
                return _followCommand;
            }
        }

        private SearchViewModel()
        {
            SearchSongs();
        }

    }
}
