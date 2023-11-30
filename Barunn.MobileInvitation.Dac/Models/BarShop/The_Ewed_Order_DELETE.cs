using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class The_Ewed_Order_DELETE
    {
        public int Order_ID { get; set; }
        public int? Order_Seq { get; set; }
        public string Event_Year { get; set; }
        public string Event_Month { get; set; }
        public string Event_Day { get; set; }
        public string Event_WeekName { get; set; }
        public string Event_AmPm { get; set; }
        public string Event_Hour { get; set; }
        public string Event_Minute { get; set; }
        public string Groom_Name { get; set; }
        public string Bride_Name { get; set; }
        public string Wedd_Name { get; set; }
        public string Wedd_Place { get; set; }
        public string Wedd_Addr { get; set; }
        public string Wedd_Phone { get; set; }
        public int? Wedd_IDX { get; set; }
        public int? BGM_ID { get; set; }
        public int? ProductID { get; set; }
        public string Wedd_Url { get; set; }
        public string member_id { get; set; }
        public string order_name { get; set; }
        public string order_email { get; set; }
        public string order_phone { get; set; }
        public string order_hphone { get; set; }
        public string BGM_FileName { get; set; }
        public string order_result { get; set; }
        public DateTime? settle_date { get; set; }
        public DateTime? settle_cancel_date { get; set; }
        public string settle_method { get; set; }
        public int? settle_price { get; set; }
        public string pg_shopid { get; set; }
        public string pg_resultinfo { get; set; }
        public string pg_paydate { get; set; }
        public string pg_caldate { get; set; }
        public string pg_repaydate { get; set; }
        public string pg_recaldate { get; set; }
        public byte? settle_status { get; set; }
        public int? status_seq { get; set; }
        public DateTime? reg_date { get; set; }
        public bool? State { get; set; }
        public int Visit_cnt { get; set; }
        public string Admin_id { get; set; }
        public string ac_State { get; set; }
    }
}
