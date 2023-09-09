using ColoradoLuxury.Models.BLL;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ColoradoLuxury.Models.DAL
{
    public class ColoradoContext : DbContext
    {
        public ColoradoContext(DbContextOptions<ColoradoContext> options) : base(options) { }


        public DbSet<AirLine> AirLines { get; set; }
        public DbSet<ArrivalAirlineInfo> ArrivalAirlineInfos { get; set; }
        public DbSet<BillingAddress> BillingAddress { get; set; }
        public DbSet<ChildSeat> ChildSeats { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CustomerTravelType> CustomerTravelTypes { get; set; }
        public DbSet<Duration> Durations { get; set; }
        public DbSet<ExtraTime> ExtraTimes { get; set; }
        public DbSet<ForDriverBetting> ForDriverBettings { get; set; }
        public DbSet<RideDetail> RideDetails { get; set; }
        public DbSet<RoofTopCargoBox> RoofTopCargoBoxs { get; set; }
        public DbSet<TransferType> TransferTypes { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }

    }
}
