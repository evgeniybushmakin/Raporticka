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
    class CreateGroupVM : BaseVM
    {
        #region Fields
        private string groupName;
        #endregion

        #region Properties

        public string GroupName
        {
            set => SetProperty(ref groupName, value);
            get => groupName;
        }
        #endregion

        #region Constructors

        #endregion

        #region Commands / Methods
        
        public DelegateCommand Adding => new DelegateCommand(() => {
            try
            {
                MainWindowVM.Database.Groups.Add(new DataBase.Models.Group()
                {
                    Name = groupName
                });

                MainWindowVM.Database.SaveChanges();
                MessageBox.Show("Группа успешно добавлена!");
            }
            catch(Exception e)
            {
                MessageBox.Show("Группа с таким именем уже существует!");
            }
        });

        #endregion
    }
}
