using ColoradoLuxury.Models.BLL;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ColoradoLuxury.Models.DAL
{
    public class ColoradoContext : IdentityDbContext<User>
    {
        public ColoradoContext(DbContextOptions<ColoradoContext> options) : base(options) { }


        public DbSet<AirLine> AirLines { get; set; }
        public DbSet<ArrivalAirlineInfo> ArrivalAirlineInfos { get; set; }
        public DbSet<BillingAddress> BillingAddress { get; set; }
        public DbSet<ChooseVehicle> ChooseVehicle { get; set; }
        public DbSet<ContractDetail> ContractDetail { get; set; }
        public DbSet<DurationInHour> DurationInHour { get; set; }
        public DbSet<ExtraTime> ExtraTime { get; set; }
        public DbSet<ErrorLog> ErrorLog { get; set; }
        public DbSet<RediDetails> RediDetails { get; set; }
        public DbSet<ServiceTab> ServiceTab { get; set; }
        public DbSet<TransferType> TransferType { get; set; }
        public DbSet<VehicleType> VehicleType { get; set; }
    }
}
