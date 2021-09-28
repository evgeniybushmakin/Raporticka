using Raporticka.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raporticka.DataBase.Models
{
    public class Student : BaseVM
    {
        #region Fields
        private int iD;
        private string fio;
        private int groupID;
        #endregion

        #region Properties
        public int ID
        {
            set => SetProperty(ref iD, value);
            get => iD;
        }

        public string Fio
        {
            set => SetProperty(ref fio, value);
            get => fio;
        }

        public int GroupID
        {
            set => SetProperty(ref groupID, value);
            get => groupID;
        }
        #endregion

        #region Constructors
        public Student() { }

        public Student(int iD, string fio, int groupID)
        {
            ID = iD;
            Fio = fio;
            GroupID = groupID;
        }
        #endregion

        #region Commands / Methods
        public override string ToString() => $"ID: {ID}\nFio: {Fio}\nGroupID: {GroupID}";

        public static string IdToFio(int iD)
        {
            return MainWindowVM.Database.Students.FirstOrDefault((students) => students.ID == iD).Fio;
        }

        public static int NameToId(string fio)
        {
            return MainWindowVM.Database.Students.FirstOrDefault((students) => students.Fio == fio).ID;
        }

        #endregion
    }
}
