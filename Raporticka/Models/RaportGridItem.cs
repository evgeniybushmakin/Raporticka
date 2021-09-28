using Raporticka.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raporticka.Models
{
    class RaportGridItem : BaseVM
    {
        #region Fields
        private int id;
        private string userName;
        private string status;
        private int minutes;
        private string note;
        private string data;
        private string group;
        #endregion

        #region Properties

        public int ID
        {
            set => SetProperty(ref id, value);
            get => id;
        }

        public string UserName
        {
            set => SetProperty(ref userName, value);
            get => userName;
        }

        public string Status
        {
            set => SetProperty(ref status, value);
            get => status;
        }

        public int Minutes
        {
            set => SetProperty(ref minutes, value);
            get => minutes;
        }

        public string Note
        {
            set => SetProperty(ref note, value);
            get => note;
        }

        public string Data
        {
            set => SetProperty(ref data, value);
            get => data;
        }

        public string Group
        {
            set => SetProperty(ref group, value);
            get => group;
        }
        #endregion

        #region Constructors
        public RaportGridItem() { }

        public RaportGridItem(string userName, string status, int minutes, string note, string data, string group, int id = default)
        {
            UserName = userName;
            Status = status;
            Minutes = minutes;
            Note = note;
            Data = data;
            Group = group;
            ID = id;
        }
        #endregion

        #region Commands / Methods
        //public override string ToString() => $"ID: {ID}\nUserNameID: {UserNameID}\nStatusID: {StatusID}\nMinutes: {Minutes}\nNote: {Note}\nData: {Data}\nGroupID:{GroupID}";
        #endregion
    }
}
