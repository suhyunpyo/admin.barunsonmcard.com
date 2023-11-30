using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class custom_order_plistAddD
    {
        public int pid { get; set; }
        public string event_year { get; set; }
        public string event_month { get; set; }
        public string event_day { get; set; }
        public string event_weekname { get; set; }
        public string lunar_yes_or_no { get; set; }
        public string lunar_event_date { get; set; }
        public string event_ampm { get; set; }
        public string event_hour { get; set; }
        public string event_minute { get; set; }
        public string wedd_name { get; set; }
        public string wedd_place { get; set; }
        public string wedd_addr { get; set; }
        public string wedd_phone { get; set; }
        public int? wedd_idx { get; set; }
        public int? wedd_imgidx { get; set; }
        public int? map_id { get; set; }
        public int? traffic_id { get; set; }
        public string map_trans_method { get; set; }
        public string map_uploadfile { get; set; }
        public string isNotMapPrint { get; set; }
        public string wedd_road_Addr { get; set; }
        public string addr_gb { get; set; }
        public string AddrDirectInd { get; set; }
    }
}
