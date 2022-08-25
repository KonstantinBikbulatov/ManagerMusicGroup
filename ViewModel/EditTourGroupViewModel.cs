using ManagerMusicGroup.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerMusicGroup.ViewModel
{
    public class EditTourGroupViewModel : TourGroupViewModel
    {
        string nameViewModel = "EditTourGroup";
        GroupTour newTour = new GroupTour();
        City city = new City();
        public GroupTour NewTour
        {
            get { return newTour; }
            set { newTour = value; }
        }

        public List<GroupTour> NewTourList = new List<GroupTour>();
        public List<GroupTour> deleteList = new List<GroupTour>();

        public static List<City> cities = new List<City>();

        private void GetCitys()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                cities = db.Cities.ToList();
            }
        }
        public override void Delete(object param)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                GroupTour item = new GroupTour();
                foreach (var i in groupListDetail.ToList())
                {
                    if(i.City.Name == param)
                    {
                        item = i;
                    }
                }
                var itemDb = db.GroupTours.Find(item.Id);
                if (itemDb != null)
                {
                    deleteList.Add(itemDb);
                }
                groupListDetail.Remove(item);

                db.SaveChanges();
            }
        }
        public override void Cancel()
        {
            foreach (var person in NewTourList)
            {
                groupListDetail.Remove(person);
            }
        }
        public override void Add()
        {
            GroupTour tour = new GroupTour();
            /*tour.City = new City();
            tour.City.Name = newTour.City.Name;
            tour.DateStart = newTour.DateStart;
            tour.DateEnd = newTour.DateEnd;
            tour.GroupId = id;*/
            newTour.GroupId = id;
            groupListDetail.Add(newTour);
            NewTourList.Add(newTour);
        }
        public override void Save()
        {
            if (NewTourList != null)
            {
                foreach (var tour in NewTourList)
                {
                    //groupListDetail.Add(tour);
                }
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var item in groupListDetail.ToList())
                {
                    var city = cities.Find(x => x.Name.Contains(item.City.Name));

                    if (city != null)
                    {
                        //item.City = city;
                        item.CityId = city.Id;
                    }
                    if (item.Id == 0)
                    {
                        item.CityId = city.Id;
                        db.GroupTours.Add(item);
                        continue;
                    }
                    db.GroupTours.Update(item);
                }
                db.SaveChanges();
            }
            ClearList();
            base.Save();
        }
        private void ClearList()
        {
            NewTourList.Clear();
            deleteList.Clear();
        }
        public EditTourGroupViewModel()
        {
            GetCitys();
            newTour.City = city;
            NameViewModel = nameViewModel;
        }
    }
}
