using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class tCouponTermCategory
    {
        public int Idx { get; set; }
        public string CouponCD { get; set; }
        public string CatLCD { get; set; }
        public string CatMCD { get; set; }
        public string CatSCD { get; set; }
        public string CatVCD { get; set; }
        public string ProcType { get; set; }
        public DateTime InsertDT { get; set; }
    }
}
