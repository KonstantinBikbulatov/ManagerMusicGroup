using ManagerMusicGroup.EditModel;
using ManagerMusicGroup.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace ManagerMusicGroup.ViewModel
{
    public class EditGroupViewModel : CommandEdit
    {
        ManagerGroupViewModel managerGroup;
        static List<Group> listGroup = new List<Group>();
        public static ICollectionView SourceCollectionGroup => GroupCollection.View;
        public static CollectionViewSource GroupCollection;
        public static ObservableCollection<Group> groupListObservable = new ObservableCollection<Group>();

        string groupDelete;
        ConfirmDeleteGroupView confirmDelete;


        string nameViewModel = "EditGroup";
        Group newGroup = new Group();
        List<Group> deleteGroupList = new List<Group>();
        public List<Group> newGroupList = new List<Group>();
        public static EditGroupViewModel instance;
        public static EditGroupViewModel getInstance()
        {
            if (instance == null)
                instance = new EditGroupViewModel();
            return instance;
        }
        public Group NewGroup
        {
            get { return newGroup; }
            set { newGroup = value; }
        }
        public void GetListGroup()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                listGroup = db.Groups.ToList();
            }
            foreach (Group item in listGroup)
            {
                groupListObservable.Add(new Group(item));
            }
            GroupCollection = new CollectionViewSource { Source = groupListObservable };
        }
        public override void Add()
        {
            Group group = new Group(newGroup);

            //newGroupList.Add(newGroup);
            groupListObservable.Add(group);
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Groups.Add(group);
                db.SaveChanges();
            }
            NewGroup.Name = string.Empty;
        }

        public void RunDelete()
        {
            confirmDelete.Close();
            var group = groupListObservable.ToList().Find(x => x.Name.Contains(groupDelete));

            groupListObservable.Remove(group);
            using (ApplicationContext db = new ApplicationContext())
            {
                var songs = db.Songs.Where(p => p.GroupId == group.Id).ToList();
                var persons = db.Persons.Where(p => p.GroupId == group.Id).ToList();

                foreach (var item in persons)
                {
                    db.Persons.Remove(item);
                }
                foreach (var item in songs)
                {
                    db.Songs.Remove(item);
                }
                db.Groups.Remove(group);
                db.SaveChanges();
            }
        }
        public override void Save()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var item in groupListObservable.ToList())
                {
                    db.Groups.Update(item);
                }
                db.SaveChanges();
            }
        }

        public override void Delete(object param)
        {
            groupDelete = (string)param;
            confirmDelete = new ConfirmDeleteGroupView();
            confirmDelete.Owner = Application.Current.MainWindow;
            confirmDelete.ShowDialog();
        }

        private EditGroupViewModel()
        {
            GetListGroup();
            managerGroup = ManagerGroupViewModel.getInstance();
            NameViewModel = nameViewModel;
        }
    }
}
