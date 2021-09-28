using Raporticka.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raporticka.DataBase.Models
{
    public class Status : BaseVM
    {
        #region Fields
        private int iD;
        private string state;
        #endregion

        #region Properties
        public int ID
        {
            set => SetProperty(ref iD, value);
            get => iD;
        }

        public string State
        {
            set => SetProperty(ref state, value);
            get => state;
        }
        #endregion

        #region Constructors
        public Status() { }

        public Status(int iD, string state)
        {
            ID = iD;
            State = state;
        }
        #endregion

        #region Commands / Methods
        public override string ToString() => $"ID: {ID}\nState: {State}\n";

        public static string IdToState(int iD)
        {
            return MainWindowVM.Database.Statuses.FirstOrDefault((status) => status.ID == iD).State;
        }

        public static int StateToId(string state)
        {
            return MainWindowVM.Database.Statuses.FirstOrDefault((status) => status.State == state).ID;
        }

        #endregion
    }
}
