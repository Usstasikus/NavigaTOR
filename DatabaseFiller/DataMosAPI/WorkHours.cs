using System;
namespace DataMosAPI
{
    public class WorkHours
    {
        public string Hours { get; set; }
        public string DayOfWeek { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", DayOfWeek, Hours);
        }
    }
}
