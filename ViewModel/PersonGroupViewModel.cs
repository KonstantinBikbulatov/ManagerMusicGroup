using ManagerMusicGroup.EditModel;
using ManagerMusicGroup.View;
using ManagerMusicGroup.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using MSWord = Microsoft.Office.Interop.Word;
namespace ManagerMusicGroup.ViewModel
{
    public class PersonGroupViewModel : CommandEdit
    {
        string nameViewModel = "PersonGroup";
        public List<Person> person;
        public static ICollectionView SourceCollectionDetail => GroupsCollectionDetail.View;
        public static CollectionViewSource GroupsCollectionDetail;
        public static ObservableCollection<Person> groupListDetail = new ObservableCollection<Person>();
        public static CollectionViewSource CollectionNewItem;
        public static int id;
        private string nameGroup = "";

        public override void Report()
        {
            string[,] items = new string[3, groupListDetail.Count];

            for (int i = 0; i < items.GetLength(1); i++)
            {
                items[0, i] = groupListDetail[i].Name;
                items[1, i] = groupListDetail[i].Age.ToString();
                items[2, i] = groupListDetail[i].Role.Name;
            }

            string[] headerColumns = {"Имя", "Возраст", "Роль" };

            string header = "Состав группы " + nameGroup;

            ModulReport.WriteReport(header, headerColumns, items, "PersonsGroup");
        }
        public ICollectionView SourceCollectionGroups => CollectionNewItem.View;

        public void GetDataDetail(object parameter, string name)
        {
            nameGroup = name;
            id = (int)parameter;
            person = db.Persons.Include(p => p.Role).Where(p => p.GroupId == id).ToList();
            groupListDetail.Clear();

            foreach (Person item in person)
            {
                groupListDetail.Add(new Person { Name = item.Name, Age = item.Age, Id = item.Id, Role = item.Role, GroupId = item.GroupId, RoleId = item.RoleId });
            }

            GroupsCollectionDetail = new CollectionViewSource { Source = groupListDetail };
            //CollectionNewItem = new CollectionViewSource { Source = newPerson };
        }
        public PersonGroupViewModel()
        {
            NameViewModel = nameViewModel;
            GroupsCollectionDetail = new CollectionViewSource { Source = groupListDetail };
        }
    }
}
