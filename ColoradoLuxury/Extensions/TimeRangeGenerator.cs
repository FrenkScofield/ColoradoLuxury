namespace ColoradoLuxury.Extensions
{
    public static class TimeRangeGenerator
    {
        public static List<DateTime> GenerateTimeRange(DateTime pickupDate, string startTime, string endTime)
        {
            DateTime start = DateTime.Parse(startTime);
            DateTime end = DateTime.Parse(endTime);
            start = pickupDate.Date + start.TimeOfDay;
            end = pickupDate.Date + end.TimeOfDay;
            List<DateTime> timeRange = new List<DateTime>();

            while (start <= end)
            {
                timeRange.Add(start);
                start = start.AddHours(1);
            }

            return timeRange;
        }

        public static DateTime GetEndDateTime(string startTime, int? durationValue)
        {
            var startDate = DateTime.Parse(startTime);
            DateTime endDate = DateTime.MinValue;
           
                endDate = startDate.AddHours(Convert.ToInt32(durationValue));
            


            return endDate;
        }
    }
}
