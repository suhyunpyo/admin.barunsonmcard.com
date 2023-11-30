using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S5_nmCardOrder
    {
        public int Order_Seq { get; set; }
        public int Company_Seq { get; set; }
        public int Mobile_Skin_Seq { get; set; }
        public string Uid { get; set; }
        public string order_name { get; set; }
        public string order_email { get; set; }
        public string order_phone { get; set; }
        public string order_hphone { get; set; }
        public string Addr { get; set; }
        public string groom_name_kor { get; set; }
        public string groom_name_eng { get; set; }
        public string groom_hphone { get; set; }
        public string bride_name_kor { get; set; }
        public string bride_name_eng { get; set; }
        public string bride_hphone { get; set; }
        public string greeting_content { get; set; }
        public string event_year { get; set; }
        public string event_month { get; set; }
        public string event_Day { get; set; }
        public string event_weekname { get; set; }
        public string event_ampm { get; set; }
        public string event_hour { get; set; }
        public string event_minute { get; set; }
        public string lunar_yorn { get; set; }
        public string WeddingHall { get; set; }
        public string wedd_phone { get; set; }
        public string wedd_place { get; set; }
        public string WeddingAddr { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string Qrcode { get; set; }
        public string show_hash { get; set; }
        public int? worder_seq { get; set; }
        public byte? status_seq { get; set; }
        public byte? settle_status { get; set; }
        public string order_state { get; set; }
        public DateTime? ModDate { get; set; }
        public DateTime? RegDate { get; set; }
        public string map_type { get; set; }
    }
}
