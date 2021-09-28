using Raporticka.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raporticka.DataBase.Models
{
    public class Group : BaseVM
    {
        #region Fields
        private string name;
        private int iD;
        #endregion

        #region Properties
        public string Name
        {
            set => SetProperty(ref name, value);
            get => name;
        }

        public int ID
        {
            set => SetProperty(ref iD, value);
            get => iD;
        }
        #endregion

        #region Constructors
        public Group() { }

        public Group(string name, int iD)
        {
            Name = name;
            ID = iD;
        }
        #endregion

        #region Commands / Methods
        public override string ToString() => $"ID: {ID}\nName: {Name}";

        public static string IdToName(int iD)
        {
            return MainWindowVM.Database.Groups.FirstOrDefault((group) => group.ID == iD).Name;
        }

        public static int NameToId(string name)
        {
            return MainWindowVM.Database.Groups.FirstOrDefault((group) => group.Name == name).ID;
        }
        #endregion
    }
}