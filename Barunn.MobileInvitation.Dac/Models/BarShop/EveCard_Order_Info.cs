using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class EveCard_Order_Info
    {
        public int SEQ { get; set; }
        public string CP_LoginDate { get; set; }
        public string CP_Name { get; set; }
        public string CP_Number { get; set; }
        public int Order_Price { get; set; }
        public int Order_SalePrice { get; set; }
        public int Order_FixPrice { get; set; }
        public string Order_Status { get; set; }
        public DateTime? Order_Date { get; set; }
        public string Admin_Name { get; set; }
        public string Memo { get; set; }
        public string Company_Number { get; set; }
        public string Address { get; set; }
    }
}
