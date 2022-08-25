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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ManagerMusicGroup.ViewModel
{
    public class EditPersonGroupViewModel : PersonGroupViewModel
    {
        string nameViewModel = "EditPersonGroup";
        Person newPerson = new Person();
        Role role = new Role();
        public Role Role
        {
            get { return role; }
            set { role = value; }
        }
        public string newName { get; set; }
        public Person NewPerson
        {
            get { return newPerson; }
            set { newPerson = value; }
        }
        public List<Person> NewPersonList = new List<Person>();
        public List<Person> deletePersonList = new List<Person>();
        public static List<Role> roles = new List<Role>();
        private void GetRoles()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                roles = db.Roles.ToList();
            }
        }
        public override void Delete(object param)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var item = groupListDetail.ToList().Find(x => x.Name.Contains((string)param));
                if (item == null)
                {
                    item = NewPersonList.Find(x => x.Name.Contains((string)param));
                }
                var itemDb = db.Persons.Find(item.Id);
                if (itemDb != null)
                {
                    deletePersonList.Add(itemDb);
                }
                groupListDetail.Remove(item);
                db.SaveChanges();
            }
        }
        public override void SetNewPerson()
        {

        }
        private void ClearList()
        {
            NewPersonList.Clear();
            deletePersonList.Clear();
        }
        public override void Cancel()
        {
            foreach (var person in NewPersonList)
            {
                groupListDetail.Remove(person);
            }
            foreach (var item in deletePersonList)
            {
                groupListDetail.Add(item);
            }
            ClearList();
        }
        public override void Add()
        {
            newPerson.GroupId = id;
            Person person = new Person(newPerson);
            groupListDetail.Add(person);
            NewPersonList.Add(person);
        }
        public override void Save()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var item in deletePersonList)
                {
                    db.Persons.Remove(item);
                }
                foreach (var item in groupListDetail.ToList())
                {
                    var role = roles.Find(x => x.Name.Contains(item.Role.Name));
                    if (role != null)
                    {
                        item.RoleId = role.Id;
                    }
                    if(item.Id == 0)
                    {
                        db.Persons.Add(item);
                        continue;
                    }
                    db.Persons.Update(item);
                }
                db.SaveChanges();
            }
            ClearList();
            base.Save();
        }
        public EditPersonGroupViewModel()
        {
            GetRoles();
            newPerson.Role = role;
            NameViewModel = nameViewModel;
        }
    }
}
