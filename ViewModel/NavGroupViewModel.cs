using ManagerMusicGroup.Model;
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
    public class NavGroupViewModel : INotifyPropertyChanged
    {
        private CollectionViewSource MenuItemsCollection;
        private CollectionViewSource GroupsCollection;
        
        // ICollectionView enables collections to have the functionalities of current record management,
        // custom sorting, filtering, and grouping.
        public ICollectionView SourceCollection => MenuItemsCollection.View;
        public ICollectionView SourceCollectionGroups => GroupsCollection.View;

        public NavGroupViewModel()
        {
            // ObservableCollection represents a dynamic data collection that provides notifications when items
            // get added, removed, or when the whole list is refreshed.
            ObservableCollection<MenuItems> menuItems = new ObservableCollection<MenuItems>
            {
                new MenuItems { MenuName = "Список групп", MenuCommand = "GroupList" },
                new MenuItems { MenuName = "Дополнительно", MenuCommand = "More" },
                new MenuItems { MenuName = "Поиск", MenuCommand = "Search" },
                new MenuItems { MenuName = "Редактор групп", MenuCommand = "Edit" }
            };

            MenuItemsCollection = new CollectionViewSource { Source = menuItems };
            MenuItemsCollection.Filter += MenuItems_Filter;

            // Set Startup Page
            SelectedViewModel = new StartPageView();
        }

        private void MenuItems_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                e.Accepted = true;
                return;
            }

            MenuItems _item = e.Item as MenuItems;
            if (_item.MenuName.ToUpper().Contains(FilterText.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        // Implement interface member for INotifyPropertyChanged.
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        // Text Search Filter.
        private string filterText;
        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                MenuItemsCollection.View.Refresh();
                OnPropertyChanged("FilterText");
            }
        }

        // Select ViewModel
        private object _selectedViewModel;
        public object SelectedViewModel
        {
            get => _selectedViewModel;
            set { _selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }
        }

        // Switch Views
        public void SwitchViews(object parameter)
        {
            switch (parameter)
            {
                case "GroupList":
                    SelectedViewModel = ManagerGroupViewModel.getInstance();
                    break;
                case "More":
                    SelectedViewModel = MoreViewModel.getInstance();
                    break;
                case "Search":
                    SelectedViewModel = SearchViewModel.getInstance(this);
                    break;
                case "Edit":
                    SelectedViewModel = EditGroupViewModel.getInstance();
                    break;
            }
        }

        private ICommand _menucommand;
        public ICommand MenuCommand
        {
            get
            {
                if (_menucommand == null)
                {
                    _menucommand = new RelayCommand(param => SwitchViews(param));
                }
                return _menucommand;
            }
        }
    }
}
