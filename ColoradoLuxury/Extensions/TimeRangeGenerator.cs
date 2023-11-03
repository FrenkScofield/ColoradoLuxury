using ColoradoLuxury.Enums;
using ColoradoLuxury.Models.BLL;
using ColoradoLuxury.Models.DAL;
using ColoradoLuxury.Models.VM;
using System;

namespace ColoradoLuxury.Extensions
{
    public static class TimeRangeGenerator
    {

        public static DateTime GetDateAsDateTime(DateTime date, string time)
        {
            DateTime dateTimeWithoutTime = DateTime.Parse(time);
            DateTime dateTime = date.Date + dateTimeWithoutTime.TimeOfDay;

            return dateTime;
        }

        public static DateTime GetEndDateTime(string startTime, int? durationValue)
        {
            DateTime startDate = DateTime.Parse(startTime);

            DateTime endDate = DateTime.MinValue;

            endDate = startDate.AddHours(Convert.ToInt32(durationValue));

            return endDate;
        }

        public static bool CheckDisabledForPickupTime(DateTime pickupDate, string startTime, DateTime endDate, string endTime, IQueryable<RidePickupTimeDetails> ridePickupTimes)
        {

            DateTime ChosenPickupDate = GetDateAsDateTime(pickupDate, startTime);

            DateTime ChosenEndDate = GetDateAsDateTime(endDate, endTime);

            List<KeyValuePair<DateTime, DateTime>> ridePickupDateTimeList = new List<KeyValuePair<DateTime, DateTime>>();

            foreach (var ridePickupTime in ridePickupTimes)
            {
                var startDateTime = GetDateAsDateTime(ridePickupTime.PickupDate, ridePickupTime.StartTime);
                var endDateTime = GetDateAsDateTime(ridePickupTime.EndDate, ridePickupTime.EndTime);

                ridePickupDateTimeList.Add(new KeyValuePair<DateTime, DateTime>(startDateTime, endDateTime));
            }

            bool CheckIsTimeFull = Enumerable.Range(0, 1 + (int)ChosenEndDate.Subtract(ChosenPickupDate).TotalMinutes)
                                    .Select(offset => ChosenPickupDate.AddMinutes(offset))
                                    .Any(date => ridePickupDateTimeList.Any(x => x.Key <= date && date <= x.Value));

            return CheckIsTimeFull;
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


        public static List<DateTime> GenerateTimeRange(DateTime pickupDate, string startTime, DateTime endDate, string endTime)
        {
            DateTime start = GetDateAsDateTime(pickupDate, startTime);
            DateTime end = GetDateAsDateTime(endDate, endTime);
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
                            dateTimeRange.AddRange(GenerateTimeRange(date, date.TimeOfDay.ToString(), choosenDate.EndDate, choosenDate.EndPickupTime));
                        }
                        else
                        {
                            dateTimeRange.AddRange(GenerateTimeRange(choosenDate.PickupDate, choosenDate.PickupTime, choosenDate.EndDate, choosenDate.EndPickupTime));
                        }

                    }
                }
                else
                {
                    foreach (var choosenDate in getDetailsForStartDate)
                    {

                        if (choosenDate.EndDate != date.Date)
                        {
                            dateTimeRange.AddRange(GenerateTimeRange(choosenDate.PickupDate, choosenDate.PickupTime, choosenDate.PickupDate.Date.AddDays(1), choosenDate.PickupDate.Date.TimeOfDay.ToString()));
                        }
                        else
                        {
                            dateTimeRange.AddRange(GenerateTimeRange(choosenDate.PickupDate, choosenDate.PickupTime, choosenDate.EndDate, choosenDate.EndPickupTime));
                        }

                    }
                }

            }




            return dateTimeRange;
        }
    }
}
