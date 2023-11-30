using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S2_eCardOrder
    {
        public int Order_Seq { get; set; }
        public int Company_Seq { get; set; }
        public string Uid { get; set; }
        public string order_name { get; set; }
        public string order_email { get; set; }
        public string Addr { get; set; }
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
        public int? Wedding_Seq { get; set; }
        public string XmlBackgroundData { get; set; }
        public DateTime? RegDate { get; set; }
        public DateTime? xmlBackgroundModiDate { get; set; }
        public string XmlMovieData { get; set; }
        public DateTime? XmlMovieModiDate { get; set; }
        public string XmlPictureData { get; set; }
        public DateTime? XmlPictureModiDate { get; set; }
        public string IsOpen { get; set; }
        public string Skin_Seq { get; set; }
        public string UploadImageURL { get; set; }
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
        public string weddinghall_type { get; set; }
        public string sales_gubun { get; set; }
    }
}
