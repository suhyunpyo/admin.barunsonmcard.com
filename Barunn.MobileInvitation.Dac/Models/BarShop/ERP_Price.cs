using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ERP_Price
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public decimal? ERP_Daeri { get; set; }
        public decimal? ERP_Sobi { get; set; }
        public decimal? ERP_Chool { get; set; }
        public string DesignCode { get; set; }
        public string UpdateDate { get; set; }
    }
}
