using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_mCardOrder
    {
        public int Order_Seq { get; set; }
        public int? Company_Seq { get; set; }
        public string Uid { get; set; }
        public string order_name { get; set; }
        public string order_email { get; set; }
        public string GroomName { get; set; }
        public string BrideName { get; set; }
        public string event_year { get; set; }
        public string event_month { get; set; }
        public string event_Day { get; set; }
        public string event_weekname { get; set; }
        public string event_ampm { get; set; }
        public string event_hour { get; set; }
        public string event_minute { get; set; }
        public string WeddingHall { get; set; }
        public string WeddingAddr { get; set; }
        public DateTime? RegDate { get; set; }
        public string IsOpen { get; set; }
        public byte? status_seq { get; set; }
        public byte? settle_status { get; set; }
        public string settle_method { get; set; }
        public int? settle_price { get; set; }
        public DateTime? settle_date { get; set; }
        public DateTime? settle_cancel_date { get; set; }
        public string pg_shopid { get; set; }
        public string pg_tid { get; set; }
        public string dacom_tid { get; set; }
        public string pg_resultinfo { get; set; }
        public string pg_resultinfo2 { get; set; }
        public double? pg_fee { get; set; }
        public string order_state { get; set; }
        public int? worder_seq { get; set; }
        public string order_phone { get; set; }
        public string order_hphone { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string Qrcode { get; set; }
        public string addr { get; set; }
        public string poi_id { get; set; }
        public string poi_x { get; set; }
        public string poi_y { get; set; }
        public string groom_pf_name { get; set; }
        public string bride_pf_name { get; set; }
        public string groom_pm_name { get; set; }
        public string bride_pm_name { get; set; }
        public string poi_weddinghall { get; set; }
        public string poi_weddingaddr { get; set; }
    }
}
