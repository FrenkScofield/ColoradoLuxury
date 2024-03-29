﻿using ColoradoLuxury.Models.BLL;

namespace ColoradoLuxury.Models.VM
{
    public class HomeInfoDetailsVM
    {
        public List<VehicleType> VehicleTypes { get; set; }
        public List<TransferType> TransferTypes { get; set; }

        public List<Country> Countries { get; set; }
        public List<AirLine> AirLines { get; set; }
        public ValueOfTipButton ValueOfTipButtons { get; set; }

        public List<string> TimesRange { get; set; }
    }
}
