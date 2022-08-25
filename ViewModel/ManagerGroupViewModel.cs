using ManagerMusicGroup.View;
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
    public class ManagerGroupViewModel : INotifyPropertyChanged
    {
        public CollectionViewSource GroupsCollection;
        private CollectionViewSource GroupsCollectionDetail;
        //ApplicationContext db = new ApplicationContext();
        public ICollectionView SourceCollectionGroups => GroupsCollection.View;
        public ICollectionView SourceCollectionDetail => GroupsCollectionDetail.View;
        public ObservableCollection<Person> groupListDetail = new ObservableCollection<Person>();

        public EditPersonGroupViewModel editData;
        private PersonGroupViewModel personGroup;
        private EditSongViewModel editSong;
        public SongGroupViewModel songGroup;
        public TourGroupViewModel tourGroup;
        public EditTourGroupViewModel editTour;
        public SongAuthorViewModel songAuthor;
        public EditSongAuthorViewModel editSongAuthor;

        public static ManagerGroupViewModel instance;
        private List<Person> persons = new List<Person>();
        public static ManagerGroupViewModel getInstance()
        {
            if (instance == null)
                instance = new ManagerGroupViewModel();

            instance.GetListGroups();
            return instance;
        }

        private List<Group> groups;
        public void GetListGroups()
        {
            groups = GetData();

            ObservableCollection<Group> groupList = new ObservableCollection<Group>();

            foreach (Group item in groups)
            {
                groupList.Add(new Group { Name = item.Name, FondationDate = item.FondationDate, Country = item.Country, Id = item.Id });
            }

            GroupsCollection = new CollectionViewSource { Source = groupList };
        }

        private ManagerGroupViewModel()
        {
            GetListGroups();
            songAuthor = new SongAuthorViewModel();
            personGroup = new PersonGroupViewModel();
            editData = new EditPersonGroupViewModel();
            editTour = new EditTourGroupViewModel();
            editSong = new EditSongViewModel();
            songGroup = new SongGroupViewModel();
            songGroup.SongAuthor = songAuthor;
            tourGroup = new TourGroupViewModel();
            editSongAuthor = new EditSongAuthorViewModel();
            
            SelectedViewPerson = personGroup;
            SelectedViewSong = songGroup;
            SelectedViewTour = tourGroup;

            //SelectedViewPerson = new View.EditPersonGroupView();
            //PersonGroupViewModel.GetDataDetail();
        }

        public List<Group> GetData()
        {
            using (ApplicationContext db = new ApplicationContext())
            {   //Группы          
                //DataTable.SetData(db);
                return db.Groups.ToList();
            }
        }

        public void SwitchComand(object parameter)
        {
            switch (parameter)
            {
                case "OpenSongAuthor":
                    SelectedViewSong = songAuthor;
                    break;
                case "SongAuthor":
                    SelectedViewSong = editSongAuthor;
                    break;
                case "EditPersonGroup":
                    SelectedViewPerson = personGroup;
                    break;
                case "PersonGroup":
                    SelectedViewPerson = editData;
                    break;
                case "EditSongGroup":
                case "BackSongAuthor":
                    SelectedViewSong = songGroup;
                    break;
                case "SongGroup":
                    SelectedViewSong = editSong;
                    break;
                case "EditTourGroup":
                    SelectedViewTour = tourGroup;
                    break;
                case "TourGroup":
                    SelectedViewTour = editTour;
                    break;
            }
        }

        public void GetDataGroup(object parameter)
        {
            Group group;
            using (ApplicationContext db = new ApplicationContext())
            {
                group = db.Groups.Where(p => p.Id == (int)parameter).First();
            }

            personGroup.GetDataDetail(parameter, group.Name);
            songGroup.GetDataDetail(parameter, group.Name);
            tourGroup.GetDataDetail(parameter, group.Name);
        }

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private object _selectedViewModel;
        public object SelectedViewPerson
        {
            get => _selectedViewModel;
            set { _selectedViewModel = value; OnPropertyChanged("SelectedViewPerson"); }
        }
        private object _selectedViewTour;
        public object SelectedViewTour
        {
            get => _selectedViewTour;
            set { _selectedViewTour = value; OnPropertyChanged("SelectedViewTour"); }
        }

        private object _selectedViewSong;
        public object SelectedViewSong
        {
            get => _selectedViewSong;
            set { _selectedViewSong = value; OnPropertyChanged("SelectedViewSong"); }
        }

        private ICommand _groupcommand;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand GroupCommand
        {
            get
            {
                if (_groupcommand == null)
                {
                    _groupcommand = new RelayCommand(param => GetDataGroup(param));
                }
                return _groupcommand;
            }
        }
    }
}
