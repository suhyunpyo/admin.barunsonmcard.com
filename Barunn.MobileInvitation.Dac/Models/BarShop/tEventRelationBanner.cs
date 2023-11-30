using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class tEventRelationBanner
    {
        public long idx { get; set; }
        public int? eventIdx { get; set; }
        public int? bannerGb { get; set; }
        public int ViewLocation { get; set; }
        public int? bannerType { get; set; }
        public int? projectView { get; set; }
        public int? Seq { get; set; }
        public int? r_eventIdx { get; set; }
        public string Comment { get; set; }
        public string UseYN { get; set; }
        public string PartnerYN { get; set; }
        public string BannerFile { get; set; }
        public string BannerLink { get; set; }
        public string Contents { get; set; }
        public string UserID { get; set; }
        public DateTime? InsertDT { get; set; }
        public DateTime? UpdateDT { get; set; }
    }
}
