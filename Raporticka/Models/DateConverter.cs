using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raporticka.Models
{
    public static class DateConverter
    {
        public static string DateTimeToString(DateTime date) => date.ToString("yyyy-MM-dd HH:mm:ss");

        public static DateTime StringToDateTime (string date) => DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss", null);
    }
}
