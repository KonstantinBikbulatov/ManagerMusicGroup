using ManagerMusicGroup.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ManagerMusicGroup.EditModel
{
    public abstract class CommandEdit : BaseDate
    {
        //public static ApplicationContext db = new ApplicationContext();
        public string NameViewModel { get; set; }
        public int CountNewModel { get; set; }
        public virtual void SwitchComand(object parameter)
        {
            switch (parameter)
            {
                case "Save":
                    Save();
                    break;
                case "Add":
                    Add();
                    break;
                case "ChangeMode":
                    Cancel();
                    ManagerGroupViewModel.instance.SwitchComand(NameViewModel);
                    SetNewPerson();
                    break;
                case "Report":
                    Report();
                    break;
            }
        }

        public virtual void SetNewPerson(){}
        public virtual void Report(){}
        public virtual void Add(){}
        public virtual void Delete(object param)
        {
            Console.WriteLine();
        }
        public virtual void Cancel()
        {

        }

        public virtual void Save()
        {
            ManagerGroupViewModel.instance.SwitchComand(NameViewModel);
        }

        protected ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(param => Delete(param));
                }
                return _deleteCommand;
            }

        }

        protected ICommand _editcommand;
        public ICommand EditCommand
        {
            get
            {
                if (_editcommand == null)
                {
                    _editcommand = new RelayCommand(param => SwitchComand(param));
                }
                return _editcommand;
            }

        }
    }
}
