using ManagerMusicGroup.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ManagerMusicGroup.ViewModel
{
    public class EditSongAuthorViewModel : SongAuthorViewModel
    {
        SongComposer newComposer;
        Composer composer = new Composer();
        List<SongComposer> deleteComposerList = new List<SongComposer>();
        public List<SongComposer> newComposerList = new List<SongComposer>();
        public Composer Composer
        {
            get { return composer; }
            set { composer = value; }
        }
        Songwriter newSongwriter;
        Author author = new Author();
        List<Songwriter> deleteSongwriterList = new List<Songwriter>();
        public List<Songwriter> newSongwriterList = new List<Songwriter>();
        public Author Author
        {
            get { return author; }
            set { author = value; }
        }
        string nameViewModel = "EditSongGroup";

        public void SaveComposer(ApplicationContext db)
        {
            foreach (var item in deleteComposerList.ToList())
            {
                db.SongComposers.Remove(item);
            }
            deleteComposerList.Clear();
            foreach (var item in listComposer.ToList())
            {
                var composer = db.Composers.Where(b => b.Name == item.Composer.Name).FirstOrDefault();
                if (composer != null)
                {
                    item.Composer = composer;
                }
                if (item.Id == 0)
                {
                    db.SongComposers.Add(item);
                    continue;
                }
                db.SongComposers.Update(item);
            }
        }

        public void SaveSongwriter(ApplicationContext db)
        {
            foreach (var item in deleteSongwriterList.ToList())
            {
                db.Songwriters.Remove(item);
            }
            deleteSongwriterList.Clear();
            foreach (var item in listAuthor.ToList())
            {
                var author = db.Authors.Where(b => b.Name == item.Author.Name).FirstOrDefault();
                if (author != null)
                {
                    item.Author = author;
                }
                if (item.Id == 0)
                {
                    db.Songwriters.Add(item);
                    continue;
                }
                db.Songwriters.Update(item);
            }
        }

        public override void Save()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                SaveComposer(db);
                SaveSongwriter(db);
                db.SaveChanges();
            }
            //ClearList();
            base.Save();
        }
        public override void Cancel()
        {
            /*
            foreach (var person in NewSongList)
            {
                groupListDetail.Remove(person);
            }
            foreach (var item in deleteSongList)
            {
                groupListDetail.Add(item);
            }
            NewSongList.Clear();
            deleteSongList.Clear();*/
        }
        public void DeleteAuthor(object param)
        {
            var item = listAuthor.ToList().Find(x => x.Author.Name.Contains((string)param));
            listAuthor.Remove(item);
            deleteSongwriterList.Add(item);
        }
        public void DeleteComposer(object param)
        {

            var item = listComposer.ToList().Find(x => x.Composer.Name.Contains((string)param));
            listComposer.Remove(item);
            deleteComposerList.Add(item);
        }
        public void AddAuthor(object parameter)
        {
            Songwriter newSongwriter = new Songwriter(id, Author.Name);

            newSongwriterList.Add(newSongwriter);
            listAuthor.Add(newSongwriter);
        }
        public void AddComposer(object parameter)
        {
            SongComposer newSongComposer = new SongComposer(id, Composer.Name);

            newComposerList.Add(newSongComposer);
            listComposer.Add(newSongComposer);
        }


        protected ICommand _deleteAuthorCommand;
        public ICommand DeleteAuthorCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(param => DeleteAuthor(param));
                }
                return _deleteCommand;
            }
        }

        protected ICommand _deleteComposerCommand;
        public ICommand DeleteComposerCommand
        {
            get
            {
                if (_deleteComposerCommand == null)
                {
                    _deleteComposerCommand = new RelayCommand(param => DeleteComposer(param));
                }
                return _deleteComposerCommand;
            }
        }

        public override void SwitchComand(object parameter)
        {
            switch (parameter)
            {
                case "AddAuthor":
                    AddAuthor(parameter);
                    break;
                case "AddComposer":
                    AddComposer(parameter);
                    break;
                case "DeleteAuthor":
                    DeleteAuthor(parameter);
                    break;
                case "DeleteComposer":
                    DeleteComposer(parameter);
                    break;
                default:
                    base.SwitchComand(parameter);
                    break;
            }
        }

        public EditSongAuthorViewModel()
        {
            NameViewModel = nameViewModel;
        }
    }
}
