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

namespace ManagerMusicGroup.ViewModel
{
    public class TourGroupViewModel : CommandEdit
    {
        string nameViewModel = "TourGroup";
        public List<GroupTour> tours;
        public static ICollectionView SourceCollectionDetail => GroupCollectionTour.View;
        public static CollectionViewSource GroupCollectionTour;
        public static ObservableCollection<GroupTour> groupListDetail = new ObservableCollection<GroupTour>();
        public static int id;
        public int placeInRaiting;
        private string nameGroup = "";
        const string NAMEFILEREPORT = "Tours Group";

        public override void Report()
        {
            string[,] items = new string[4, groupListDetail.Count];

            for (int i = 0; i < items.GetLength(1); i++)
            {
                items[0, i] = groupListDetail[i].City.Name;
                items[1, i] = groupListDetail[i].DateStart.ToString();
                items[2, i] = groupListDetail[i].DateEnd.ToString();
                items[3, i] = groupListDetail[i].Price.ToString();
            }

            string[] headerColumns = { "Город", "Начало", "Конец", "Цена билета" };

            string header = "Тур группы " + nameGroup;

            ModulReport.WriteReport(header, headerColumns, items, NAMEFILEREPORT);
        }
        private void GetRaiting()
        {
            var place = db.Raitings.Where(p => p.GroupId == id).ToList();
            placeInRaiting = place[0].Id;
        }

        public void GetDataDetail(object parameter, string name)
        {
            nameGroup = name;
            id = (int)parameter;
            using (ApplicationContext db = new ApplicationContext())
            {
                tours = db.GroupTours.Include(p => p.City).Where(p => p.GroupId == id).ToList();
            }

            groupListDetail.Clear();

            foreach (GroupTour item in tours)
            {
                int priceTicket = item.City.Price + ((item.City.Price / 100) * (placeInRaiting * 5));
                groupListDetail.Add(new GroupTour { City = item.City, DateStart = item.DateStart, DateEnd = item.DateEnd,
                                                    Id = item.Id, Price = priceTicket, GroupId = item.GroupId });
            }

            GroupCollectionTour = new CollectionViewSource { Source = groupListDetail };
        }
        public TourGroupViewModel()
        {
            id = 1;
            NameViewModel = nameViewModel;
            GetRaiting();
            GroupCollectionTour = new CollectionViewSource { Source = groupListDetail };
        }
    }
}
