namespace Cancun.Booking.Domain.Resources.Helpers
{
    public static class DatesHelper
    {
        public static List<DateTime> GetDatesBetween(DateTime start, DateTime end)
        {
            if (end < start) return new List<DateTime>();
            return Enumerable.Range(0, 1 + end.Subtract(start).Days)
           .Select(m => start.AddDays(m).Date)
           .ToList();
        }

        public static double GetInterval(DateTime start, DateTime end)
        {
            if (end < start) return 0;
            return end.Subtract(start).TotalDays;
        }

        public static DateTime GetMinReservationDate(int minDaysRule)
        {
            return DateTime.Now.AddDays(minDaysRule).Date;
        }

        public static DateTime GetMaxReservationDate(int maxDateRule)
        {
            return DateTime.Now.AddDays(maxDateRule).Date;
        }

        public static IEnumerable<DateTime> GetPossibleDates(int minDaysRule, int maxDateRule)
        {
            return GetDatesBetween(GetMinReservationDate(minDaysRule), GetMaxReservationDate(maxDateRule));
        }
    }
}
