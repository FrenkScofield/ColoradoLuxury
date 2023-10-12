using ColoradoLuxury.Models.VM;

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
            DateTime startDate = DateTime.Parse(startTime);

            DateTime endDate = DateTime.MinValue;

            endDate = startDate.AddHours(Convert.ToInt32(durationValue));

            return endDate;
        }

        public static bool CheckDisabledForPickupTime(DateTime pickupDate, string startTime, IQueryable<RidePickupTimeDetails> ridePickupTimes)
        {

            DateTime start = DateTime.Parse(startTime);
            DateTime ChosenPickupDate = pickupDate.Date + start.TimeOfDay;

            foreach (var ridePickupTime in ridePickupTimes)
            {
                DateTime startTimeParseDateTime = DateTime.Parse(ridePickupTime.StartDate);
                DateTime startTimeFromDb = ridePickupTime.PickupDate.Date + startTimeParseDateTime.TimeOfDay;
                DateTime endTimeParseDateTime = DateTime.Parse(ridePickupTime.EndDate);
                DateTime endTimeFromDb = ridePickupTime.PickupDate.Date + endTimeParseDateTime.TimeOfDay;

                if (DateTime.Now.Date == ChosenPickupDate.Date && startTimeFromDb.TimeOfDay <= ChosenPickupDate.TimeOfDay && ChosenPickupDate.TimeOfDay <= endTimeFromDb.TimeOfDay)
                {
                    return true;
                }
            }


            return false;
        }

        public static bool Isvalid(string startTime)
        {
            if (DateTime.TryParse(startTime, out DateTime startTimeResult))
            {
                return true;
            }

            return false;

        }
    }
}
