using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S5_nmCardShowInfo
    {
        public int Order_Seq { get; set; }
        public int ShowIndex { get; set; }
        public string ShowHash { get; set; }
        public string ShakrInstanceId { get; set; }
        public string StyleSlug { get; set; }
        public string ShowViewUrl { get; set; }
        public string ShowViewAliasUrl { get; set; }
        public string ShowViewAliasHdUrl { get; set; }
        public string ShowViewAliasSdUrl { get; set; }
        public string ShowHdDownUrl { get; set; }
        public string ShowSdDownUrl { get; set; }
        public short EditSIstatus { get; set; }
        public short EditSSstatus { get; set; }
        public short EditSCstatus { get; set; }
        public short EditECstatus { get; set; }
        public int RenderProgress { get; set; }
        public int ShowStatus { get; set; }
        public string PurchasedStatus { get; set; }
        public DateTime ModDate { get; set; }
        public DateTime RegDate { get; set; }
        public string DelFlag { get; set; }
        public string Skin_img { get; set; }
    }
}
