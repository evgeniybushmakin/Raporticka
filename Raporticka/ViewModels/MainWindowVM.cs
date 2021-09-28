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

namespace Raporticka.ViewModels
{
    class MainWindowVM : BaseVM
    {
        #region Fields
        private List<string> groupItems;
        private string selectedGroup;
        private string selectedStatus;
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
        #endregion

        #region Constructors
        public MainWindowVM()
        {
            Database = new ApplicationContext();
            StatusItems = new ObservableCollection<string>();
            foreach (var item in Database.Statuses)
                StatusItems.Add(item.State);
            GridItems = new ObservableCollection<RaportGridItem>();
            GroupItems = new List<string>();
            foreach(var item in Database.Groups.ToList())
            {
                GroupItems.Add(item.Name);
            }
        }
        #endregion

        #region Commands / Methods

        public void CloneData()
        {
            GridItems?.Clear();

            var grId = MainWindowVM.Database.Groups.FirstOrDefault((group) => group.Name == SelectedGroup)?.ID;
            var date = DateConverter.DateTimeToString(SelectedDate ?? DateConverter.StringToDateTime("0001-01-01 00:00:00"));
            foreach (var item in MainWindowVM.Database.Raports.Where((raport) => raport.GroupID == grId && raport.Data == "0001-01-01 00:00:00").ToList())
            {
                var usId = item.UserNameID;
                var userName = MainWindowVM.Database.Students.FirstOrDefault((student) => student.ID == usId).Fio;
                var groupId = item.GroupID;
                var group = MainWindowVM.Database.Groups.FirstOrDefault((gr) => gr.ID == groupId).Name;
                string status = "";
                int minutes = 0;
                string note = "";
                string usDate = "";

                var stId = item.StatusID;
                status = (string)MainWindowVM.Database.Statuses.FirstOrDefault((st) => st.ID == stId).State.Clone();
                minutes = item.Minutes;
                note = (string)item.Note.Clone();
                usDate = DateConverter.DateTimeToString(DateTime.Today);

                MainWindowVM.Database.Raports.Add(new Raport(usId, stId, minutes, note, usDate, groupId));
            }
        }

        public void SetItems()
        {
            GridItems?.Clear();
            int groupeID = 0;
            if (selectedGroup != null)
                groupeID = Database.Groups.Where(g => g.Name == SelectedGroup).ToArray()[0].ID;
            string data = "0001-01-01 00:00:00";
            if (SelectedDate.HasValue)
                data = ((DateTime)SelectedDate).ToString("yyyy-MM-dd hh:mm:ss");

            var dates = Database.Raports.Where(r => r.Data == data && r.GroupID == groupeID).ToList();
            if (dates.Count != 0)
            {
                foreach (var item in dates)
                    GridItems.Add(new RaportGridItem()
                    {
                        Data = item.Data,
                        UserName = Database.Students.Where(s => item.UserNameID == s.ID).ToList()[0].Fio,
                        Status = Database.Statuses.Where(s => item.StatusID == s.ID).ToList()[0].State,
                        Minutes = item.Minutes,
                        Note = item.Note,
                        ID = item.ID
                    });
            }
            else
                foreach (var item in Database.Students.Where(s => s.GroupID == groupeID))
                {
                    GridItems.Add(new RaportGridItem() 
                    {
                        Data = data,
                        UserName = item.Fio,
                        Status = "",
                        Minutes = 0,
                        Note = ""
                    });
                }
        }

        public DelegateCommand Saving => new DelegateCommand(() => {
            int groupeID = 0;

            if (selectedGroup != null)
                groupeID = Database.Groups.Where(g => g.Name == SelectedGroup).ToArray()[0].ID;
            
            string data = "0001-01-01 00:00:00";
            if (SelectedDate.HasValue)
                data = ((DateTime)SelectedDate).ToString("yyyy-MM-dd hh:mm:ss");
            
            var datas = Database.Raports.Where(r => r.GroupID == groupeID && r.Data == data).ToList();
            
            if(GridItems.Count != 0) 
                foreach (var item in datas)
                    Database.Raports.Remove(item);

            Database.SaveChanges();

            datas = Database.Raports.Where(r => r.GroupID == groupeID && r.Data == data).ToList();

            foreach (var item in GridItems) 
            {
                bool flag = true;
                foreach (var temp in datas)
                    if (temp.UserNameID == Database.Students.Where(s => s.Fio == item.UserName).ToArray()[0].ID &&
                        temp.StatusID == Database.Raports.Where(r => r.UserNameID == temp.UserNameID).ToList()[0].StatusID)
                    {
                        flag = false;
                        return;
                    }

                if(flag)
                    Database.Raports.Add(new Raport() { 
                        GroupID = groupeID,
                        UserNameID = Database.Students.Where(s => s.Fio == item.UserName).ToArray()[0].ID,
                        Data = data, 
                        Minutes = item.Minutes, 
                        Note = item.Note, 
                        StatusID = Database.Statuses.Where(s => s.State == item.Status).ToList()[0].ID
                    });
            }

            Database.SaveChanges();
        });
        #endregion
    }
}