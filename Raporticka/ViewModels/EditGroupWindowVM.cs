using Prism.Commands;
using Raporticka.DataBase;
using Raporticka.DataBase.Models;
using Raporticka.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Raporticka.ViewModels
{
    class EditGroupWindowVM : BaseVM
    {
        #region Fields
        private List<string> groupItems;
        private string selectedGroup;
        private string selectedStatus;
        private int selectedIndex;
        private ObservableCollection<RaportGridItem> gridItems;
        private static ApplicationContext database;
        private ObservableCollection<string> statusItems;
        private DateTime? selectedDate;
        #endregion

        #region Properties

        public List<string> GroupItems
        {
            set => SetProperty(ref groupItems, value);
            get => groupItems;
        }

        public string SelectedStatus
        {
            set => SetProperty(ref selectedStatus, value);
            get => selectedStatus;
        }

        public DateTime? SelectedDate
        {
            set
            {
                SetProperty(ref selectedDate, value);

                SetItems();
            }
            get => selectedDate;
        }

        public string SelectedGroup
        {
            set
            {
                SetProperty(ref selectedGroup, value);

                SetItems();
            }
            get => selectedGroup;
        }

        public static ApplicationContext Database
        {
            get { return database; }
            set { database = value; }
        }

        public ObservableCollection<RaportGridItem> GridItems
        {
            set => SetProperty(ref gridItems, value);
            get => gridItems;
        }

        public ObservableCollection<string> StatusItems
        {
            set => SetProperty(ref statusItems, value);
            get => statusItems;
        }

        public int SelectedIndex 
        {
            set => SetProperty(ref selectedIndex, value);
            get => selectedIndex;
        }
        #endregion

        #region Constructors
        public EditGroupWindowVM()
        {
            Database = new ApplicationContext();
            StatusItems = new ObservableCollection<string>();
            foreach (var item in Database.Statuses)
                StatusItems.Add(item.State);
            GridItems = new ObservableCollection<RaportGridItem>();
            GroupItems = new List<string>();
            foreach (var item in Database.Groups.ToList())
            {
                GroupItems.Add(item.Name);
            }
        }
        #endregion

        #region Commands / Methods
        public DelegateCommand Update => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы уверены, что хотите сохранить изменения?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                Database.SaveChangesAsync().Await(() => { MessageBox.Show("Изменения успешно сохранены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information); });
        });

        public DelegateCommand Saving => new DelegateCommand(() => {
            var grId = MainWindowVM.Database.Groups.FirstOrDefault((groups) => groups.Name == SelectedGroup)?.ID;
            foreach(var item in GridItems) 
            {
                bool flag = true;
                foreach (var tmp in Database.Students)
                    if (tmp.Fio == item.UserName || item.UserName == "") 
                    {
                        flag = false;
                        break;
                    }

                if (flag) 
                {
                    Database.Students.Add(new Student() { 
                        Fio = item.UserName,
                        GroupID = (int)grId
                    });

                    Database.SaveChanges();

                    Database.Raports.Add(new Raport()
                    {
                        GroupID = (int)grId,
                        Data = "0001-01-01 00:00:00",
                        StatusID = 1,
                        Minutes = 0,
                        UserNameID = Database.Students.Where(s => item.UserName == s.Fio).ToList()[0].ID
                    });

                    Database.SaveChanges();
                }
            }
        });

        public DelegateCommand Removing => new DelegateCommand(() => {
            try
            {
                string fio = GridItems[SelectedIndex].UserName;
                int id = Database.Students.Where((student) => student.Fio == fio).ToList()[0].ID;
                var deletRaport = Database.Raports.Where(w => w.UserNameID == id).ToList();
                var deletStudent = Database.Students.Where(s => s.Fio == fio).ToList()[0];

                foreach (var item in deletRaport)
                    Database.Raports.Remove(item);

                Database.Students.Remove(deletStudent);

                Database.SaveChanges();
                GridItems.RemoveAt(SelectedIndex);
            }
            catch { }
        });

        public void SetItems()
        {
            GridItems?.Clear();
            var grId = MainWindowVM.Database.Groups.FirstOrDefault((groups) => groups.Name == SelectedGroup)?.ID;
            foreach (var item in MainWindowVM.Database.Students.Where((student) => student.GroupID == grId))
                GridItems.Add(new RaportGridItem()
                {
                    ID = item.ID,
                    UserName = item.Fio
                });
        }
        #endregion
    }
}
