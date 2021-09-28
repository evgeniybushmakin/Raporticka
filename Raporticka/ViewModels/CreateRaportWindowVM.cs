using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Raporticka.ViewModels
{
    class CreateRaportWindowVM : BaseVM
    {
        #region Fields
        private List<string> groupItems;
        private string groupeName;
        #endregion

        #region Properties
        public List<string> GroupItems
        {
            set => SetProperty(ref groupItems, value);
            get => groupItems;
        }

        public string GroupName
        {
            set => SetProperty(ref groupeName, value);
            get => groupeName;
        }

        #endregion

        #region Constructors
        public CreateRaportWindowVM()
        {
            GroupItems = new List<string>();
            foreach (var item in MainWindowVM.Database.Groups.ToList())
            {
                GroupItems.Add(item.Name);
            }
        }
        #endregion

        #region Commands / Methods

        public DelegateCommand DataExport => new DelegateCommand(() =>
       {
           Excel.Application application = new Excel.Application();
           var ID = MainWindowVM.Database.Groups.Where(g => g.Name == GroupName).ToList()[0].ID;
           var students = MainWindowVM.Database.Students.Where(s => s.GroupID == ID).ToList();
           var workbook = (Excel._Workbook)application.Workbooks.Add(Missing.Value);
           var sheet = (Excel._Worksheet)workbook.ActiveSheet;
           sheet.Columns[1].ColumnWidth = 40;
           sheet.Cells[1, 1] = $"Группа: {GroupName}";
           bool flag = true;
           for (int i = 0; i < students.Count; i++)
           {
               sheet.Cells[i + 2, 1] = students[i].Fio;
               var temp = students[i].ID;
               var dates = MainWindowVM.Database.Raports.Where(r => r.UserNameID == temp).ToList();
               for (int j = 0; j < dates.Count; j++)
               {
                   if (flag)
                   {
                       sheet.Cells[i + 1, j + 2].NumberFormat = "ДД.ММ";
                       sheet.Cells[i + 1, j + 2] = dates[j].Data;
                   }

                   string status = "";

                   switch (dates[j].StatusID)
                   {
                       case 2:
                           status = dates[j].Minutes.ToString();
                           break;

                       case 3:
                           status = "H";
                           break;
                   }
                   sheet.Cells[i + 2, j + 2].NumberFormat = null;
                   sheet.Cells[i + 2, j + 2] = status;
               }
               flag = false;
           }

           application.Visible = true;
           application.UserControl = true;
       });
        #endregion
    }
}
