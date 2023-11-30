using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class antHall
    {
        public int wid { get; set; }
        public string Wname { get; set; }
        public string wedd_keyword { get; set; }
        public string Waddress { get; set; }
        public string Wphone { get; set; }
        public byte? Wdiv { get; set; }
        public byte? location { get; set; }
        public string iscorel { get; set; }
        public int? imgid { get; set; }
        public int? lsort { get; set; }
        public DateTime? reg_date { get; set; }
        public string admin_id { get; set; }
        public DateTime? mod_date { get; set; }
        public short? Wcnt { get; set; }
        public string isUpdate { get; set; }
        public string isAutoWeddInfo { get; set; }
        public string update_id { get; set; }
        public string WRoadNameAddress { get; set; }
        public string isColor { get; set; }
    }
}
