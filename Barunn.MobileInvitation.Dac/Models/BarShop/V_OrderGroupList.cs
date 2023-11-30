using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class V_OrderGroupList
    {
        public int? CompanySeq { get; set; }
        public int OrderSeq { get; set; }
        public string PayType { get; set; }
        public int? StatusSeq { get; set; }
        public int SettlePrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string MemberID { get; set; }
        public string OrderName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Hp { get; set; }
        public string Method { get; set; }
        public int? status { get; set; }
        public DateTime? payDate { get; set; }
        public int? orderPrice { get; set; }
        public int? totalPrice { get; set; }
        public int? deliveryPrice { get; set; }
        public string bankInfo { get; set; }
        public string depositor { get; set; }
        public string deliveryName { get; set; }
        public DateTime? deliveryDate { get; set; }
        public string deliveryNo { get; set; }
        public int? deliveryMethod { get; set; }
        public string deliveryPhone { get; set; }
        public string deliveryHp { get; set; }
        public string deliveryZip { get; set; }
        public string deliveryAddr { get; set; }
        public string deliveryAddrDetail { get; set; }
        public string deliveryCountry { get; set; }
        public int? Wed_Order_Seq { get; set; }
        public int? Wed_Order_Seq_CNT { get; set; }
        public int? Order_Seq { get; set; }
    }
}
