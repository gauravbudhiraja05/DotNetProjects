using System;
using System.Globalization;

namespace HiveReport.WebAdmin.Utility
{
    public class DateFilterParam
    {
        public DateTime DateValue { get; set; }
        public bool FilterEnabled { get; set; }

        public DateFilterParam()
        {
            DateValue = new DateTime();
            FilterEnabled = false;
        }
        public DateFilterParam(int year, int month, int day, bool filterEnabled = true)
        {
            DateValue = new DateTime(year, month, day);
            FilterEnabled = filterEnabled;
        }
        public DateFilterParam(DateTime date, bool filterEnabled = true)
        {
            DateValue = date;
            FilterEnabled = filterEnabled;
        }
        public DateFilterParam(string angularDate)
        {
            if (DateTime.TryParse(angularDate, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out DateTime localDate))
            {
                DateValue = localDate;
                FilterEnabled = true;
            }
            else
            {
                DateValue = new DateTime(1900, 12, 31);
                FilterEnabled = false;
            }
        }
    }
}
