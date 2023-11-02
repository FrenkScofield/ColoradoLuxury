using ColoradoLuxury.Enums;
using ColoradoLuxury.Models.BLL;
using ColoradoLuxury.Models.DAL;
using ColoradoLuxury.Models.VM;

namespace ColoradoLuxury.Extensions
{
    public static class TimeRangeGenerator
    {
        public static List<string> GenerateTimeRange(DateTime pickupDate, DateTime endDate, string startTime, string endTime)
        {
            DateTime start = DateTime.Parse(startTime);
            DateTime end = DateTime.Parse(endTime);
            var b = start.ToString("hh:mm tt");

            var a = end.ToString("hh:mm tt");
            int c = b.CompareTo(a);

            start = pickupDate.Date + start.TimeOfDay;
            end = endDate.Date + end.TimeOfDay;
            List<string> timeRange = new List<string>();

            while (start <= end)
            {
                timeRange.Add(startTime.Substring(0, 2));
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

                if (ridePickupTime.PickupDate.Date == ChosenPickupDate.Date && startTimeFromDb.TimeOfDay <= ChosenPickupDate.TimeOfDay && ChosenPickupDate.TimeOfDay <= endTimeFromDb.TimeOfDay)
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

        public static bool IsvalidExpireDate(DateTime startDate, string startTime)
        {
            DateTime start = DateTime.Parse(startTime);
            DateTime ChosenPickupDate = startDate.Date + start.TimeOfDay;

            if (ChosenPickupDate < DateTime.Now)
                return true;

            return false;
        }

        public static DateTime ChangeUsaColoradoTimeZone(this DateTime pickupDate, string time)
        {



            DateTime start = DateTime.Parse(time);
            DateTime ChosenPickupDate = pickupDate.Date + start.TimeOfDay;
            var a = ChosenPickupDate.ToString("dd.MM.yyyy hh:mm tt");
            DateTime dateTime = DateTime.Parse(ChosenPickupDate.ToString("dd.MM.yyyy hh:mm"));

            return dateTime;
        }

        public static List<DateTime> GenerateTimeRange2(DateTime pickupDate, string startTime, DateTime endDate, string endTime)
        {
            DateTime start = DateTime.Parse(startTime);
            DateTime end = DateTime.Parse(endTime);
            var b = start.ToString("hh:mm tt");

            var a = end.ToString("hh:mm tt");
            int c = b.CompareTo(a);

            start = pickupDate.Date + start.TimeOfDay;
            end = endDate.Date + end.TimeOfDay;
            List<DateTime> timeRange = new List<DateTime>();

            while (start <= end)
            {
                timeRange.Add(start);
                start = start.AddHours(1);
            }

            return timeRange;
        }


        public static List<DateTime> ShowDateAndTimeToDoBron(DateTime date, string type, ColoradoContext _context)
        {
            List<DateTime> dateTimeRange = new List<DateTime>();

            var getDetailsForStartDate = _context.RideDetails.Where(x => (x.PickupDate == date.Date && x.PickupTime.Contains(type)) || (x.EndDate == date.Date && x.EndPickupTime.Contains(type))).Select(y => new RideDetail
            {
                Id = y.Id,
                PickupDate = y.PickupDate,
                PickupTime = y.PickupTime,
                EndDate = y.EndDate,
                EndPickupTime = y.EndPickupTime
            });

            if (getDetailsForStartDate != null)
            {
                if (type == TimeTypeEnum.AM.ToString())
                {
                    foreach (var choosenDate in getDetailsForStartDate)
                    {

                        if (choosenDate.PickupDate != date.Date)
                        {
                            dateTimeRange.AddRange(GenerateTimeRange2(date, date.TimeOfDay.ToString(), choosenDate.EndDate, choosenDate.EndPickupTime));
                        }
                        else
                        {
                            dateTimeRange.AddRange(GenerateTimeRange2(choosenDate.PickupDate, choosenDate.PickupTime, choosenDate.EndDate, choosenDate.EndPickupTime));
                        }

                    }
                }
                else
                {
                    foreach (var choosenDate in getDetailsForStartDate)
                    {

                        if (choosenDate.EndDate != date.Date)
                        {
                            dateTimeRange.AddRange(GenerateTimeRange2(choosenDate.PickupDate, choosenDate.PickupTime, choosenDate.PickupDate.Date.AddDays(1), choosenDate.PickupDate.Date.TimeOfDay.ToString()));
                        }
                        else
                        {
                            dateTimeRange.AddRange(GenerateTimeRange2(choosenDate.PickupDate, choosenDate.PickupTime, choosenDate.EndDate, choosenDate.EndPickupTime));
                        }

                    }
                }
               
            }


           

            return dateTimeRange;
        }
    }
}
