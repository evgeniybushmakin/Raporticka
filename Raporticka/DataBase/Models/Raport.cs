using Raporticka.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raporticka.DataBase.Models
{
    public class Raport : BaseVM, ICloneable
    {
        #region Fields
        private int id;
        private int userNameID;
        private int statusID;
        private int minutes;
        private string note;
        private string data;
        private int groupID;
        #endregion

        #region Properties

        public int ID
        {
            set => SetProperty(ref id, value);
            get => id;
        }

        public int UserNameID
        {
            set => SetProperty(ref userNameID, value);
            get => userNameID;
        }

        public int StatusID
        {
            set => SetProperty(ref statusID, value);
            get => statusID;
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

        public int GroupID
        {
            set => SetProperty(ref groupID, value);
            get => groupID;
        }
        #endregion

        #region Constructors
        public Raport() { }

        public Raport(int userNameID, int statusID, int minutes, string note, string data, int groupID)
        {
            UserNameID = userNameID;
            StatusID = statusID;
            Minutes = minutes;
            Note = note;
            Data = data;
            GroupID = groupID;
        }
        #endregion

        #region Commands / Methods
        public override string ToString() => $"ID: {ID}\nUserNameID: {UserNameID}\nStatusID: {StatusID}\nMinutes: {Minutes}\nNote: {Note}\nData: {Data}\nGroupID:{GroupID}";

        public object Clone() => new Raport(UserNameID, StatusID, Minutes, Note, Data, GroupID);

        #endregion
    }
}