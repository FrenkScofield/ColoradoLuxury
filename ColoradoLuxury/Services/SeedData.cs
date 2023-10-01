using ColoradoLuxury.Models.BLL;
using ColoradoLuxury.Models.DAL;

namespace ColoradoLuxury.Services
{
    public static class SeedData
    {
        public static void AddTransferType(ColoradoContext context)
        {
            if (!context.TransferTypes.Any())
            {
                List<TransferType> transferTypes = new List<TransferType>() {
                    new TransferType()
                    {
                        Name = "One Way",
                        Status = false
                    },
                    new TransferType()
                    {
                        Name = "Return",
                        Status = false
                    },
                    new TransferType()
                    {
                        Name = "Return (new ride)",
                        Status = false
                    }
                };

                foreach (var transferType in transferTypes)
                {
                    context.TransferTypes.Add(transferType);
                    context.SaveChanges();
                }

            }
        }

        public static void AddDuration(ColoradoContext context)
        {
            if (!context.Durations.Any())
            {
                List<Duration> durations = new List<Duration>() {
                    new Duration()
                    {
                        Time = "1 hour"
                    },
                    new Duration()
                    {
                        Time = "2 hour"
                    },
                    new Duration()
                    {
                        Time = "3 hour"
                    },
                    new Duration()
                    {
                        Time = "4 hour"
                    },
                    new Duration()
                    {
                        Time = "5 hour"
                    },
                    new Duration()
                    {
                        Time = "6 hour"
                    },
                    new Duration()
                    {
                        Time = "7 hour"
                    },
                    new Duration()
                    {
                        Time = "8 hour"
                    },
                    new Duration()
                    {
                        Time = "9 hour"
                    },
                };

                foreach (var duration in durations)
                {
                    context.Durations.Add(duration);
                    context.SaveChanges();
                }

            }
        }


        public static void AddCustomTravelType(ColoradoContext context)
        {
            if (!context.CustomerTravelTypes.Any())
            {
                List<CustomerTravelType> customerTravelTypes = new List<CustomerTravelType>() {
                    new CustomerTravelType()
                    {
                        Name = "Distance"
                    },
                    new CustomerTravelType()
                    {
                        Name = "Hourly"
                    }
                };

                foreach (var customerTravelType in customerTravelTypes)
                {
                    context.CustomerTravelTypes.Add(customerTravelType);
                    context.SaveChanges();
                }
            }
        }

        public static void AddCountries(ColoradoContext context)
        {
            if (!context.Countries.Any())
            {
                List<Country> countries = new List<Country>() {
                    new Country()
                    {
                        Name = "USA1"
                    },
                    new Country()
                    {
                        Name = "USA2"
                    },
                    new Country()
                    {
                        Name = "USA3"
                    },
                    new Country()
                    {
                        Name = "USA4"
                    }
                };

                foreach (var country in countries)
                {
                    context.Countries.Add(country);
                    context.SaveChanges();
                }
            }
        }


        public static void AddAirlines(ColoradoContext context)
        {
            if (!context.AirLines.Any())
            {
                List<AirLine> airlines = new List<AirLine>() {
                    new AirLine()
                    {
                        Name = "Airline1"
                    },
                    new AirLine()
                    {
                        Name = "Airline2"
                    },
                    new AirLine()
                    {
                        Name = "Airline3"
                    },
                    new AirLine()
                    {
                        Name = "Airline4"
                    }
                };

                foreach (var airLine in airlines)
                {
                    context.AirLines.Add(airLine);
                    context.SaveChanges();
                }
            }
        }

        public static void AddValueOfTipBtn(ColoradoContext context)
        {
            if (!context.ValueOfTipButtons.Any())
            {
                List<ValueOfTipButton> valueOfTipButtons = new List<ValueOfTipButton>() {
                    new ValueOfTipButton()
                    {
                        lowInterest = 15,
                        MiddleInterest = 20,
                        HighInterest = 30,
                    },
                    
                };

                foreach (var valueOfTipButton in valueOfTipButtons)
                {
                    context.ValueOfTipButtons.Add(valueOfTipButton);
                    context.SaveChanges();
                }
            }
        }
    }
}
