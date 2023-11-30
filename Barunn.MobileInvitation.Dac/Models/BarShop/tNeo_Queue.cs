using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class tNeo_Queue
    {
        public int mid { get; set; }
        public string barid { get; set; }
        public string sname { get; set; }
        public string smail { get; set; }
        public string mtype { get; set; }
        public string mstatus { get; set; }
        public string rname { get; set; }
        public string rmail { get; set; }
        public string mtitle { get; set; }
        public string mcontent { get; set; }
        public DateTime mdate { get; set; }
        public string msave { get; set; }
        public string isSend { get; set; }
        public string c_site { get; set; }
        public string c_category { get; set; }
        public string cardno { get; set; }
        public DateTime org_date { get; set; }
        public string c_hosting { get; set; }
        public string ERROR_MSG { get; set; }
        public DateTime? SEND_DATE { get; set; }
    }
}
