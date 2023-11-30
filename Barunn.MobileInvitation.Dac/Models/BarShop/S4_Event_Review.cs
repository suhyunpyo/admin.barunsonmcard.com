using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_Event_Review
    {
        public int ER_Idx { get; set; }
        public int ER_Company_Seq { get; set; }
        public int ER_Order_Seq { get; set; }
        public int ER_Type { get; set; }
        public int? ER_Card_Seq { get; set; }
        public string ER_Card_Code { get; set; }
        public string ER_UserId { get; set; }
        public DateTime ER_Regdate { get; set; }
        public int? ER_Recom_Cnt { get; set; }
        public string ER_Review_Title { get; set; }
        public string ER_Review_Url { get; set; }
        public string ER_Review_Url2 { get; set; }
        public string ER_Review_Content { get; set; }
        public int? ER_Review_Star { get; set; }
        public int? ER_Review_Price { get; set; }
        public int? ER_Review_Design { get; set; }
        public int? ER_Review_Quality { get; set; }
        public int? ER_Review_Satisfaction { get; set; }
        public int? ER_Status { get; set; }
        public int? ER_View { get; set; }
        public string ER_UserName { get; set; }
        public string ER_Review_Url_a { get; set; }
        public string ER_Review_Url_b { get; set; }
        public string ER_isBest { get; set; }
        public string ER_isPhoto { get; set; }
        public int? ER_Gift_Code { get; set; }
        public string ER_Review_Reply { get; set; }
        public string ER_Review_Url3 { get; set; }
        public string inflow_route { get; set; }
        public string ER_Comm_Div { get; set; }
        public string device_type { get; set; }
        public string ER_Comment { get; set; }
    }
}
