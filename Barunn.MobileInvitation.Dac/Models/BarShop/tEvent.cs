using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class tEvent
    {
        public int EventIdx { get; set; }
        public string EventNM { get; set; }
        public DateTime FromDt { get; set; }
        public DateTime ToDt { get; set; }
        public string EventGB { get; set; }
        public string CatLCD { get; set; }
        public string Contents { get; set; }
        public string ItemCD { get; set; }
        public string ViewYN { get; set; }
        public string Banner { get; set; }
        public DateTime InsertDT { get; set; }
        public DateTime UpdateDT { get; set; }
        public string templateYN { get; set; }
        public string templateUrl { get; set; }
        public string PageWidth { get; set; }
        public string NaviYN { get; set; }
        public string MainImage { get; set; }
        public string MainHtml { get; set; }
        public string MainText { get; set; }
        public string projectYN { get; set; }
        public string TitleBarType { get; set; }
        public int? TopBannerType { get; set; }
        public int? MiddleBannerType { get; set; }
        public int? BottomBannerType { get; set; }
        public string FSEventYN { get; set; }
        public string Top6Image1 { get; set; }
        public string Top6Image2 { get; set; }
        public string Top6Image3 { get; set; }
        public string Top6Image4 { get; set; }
        public string Top6Image5 { get; set; }
        public string Top6Image6 { get; set; }
        public string Top6ImageURL1 { get; set; }
        public string Top6ImageURL2 { get; set; }
        public string Top6ImageURL3 { get; set; }
        public string Top6ImageURL4 { get; set; }
        public string Top6ImageURL5 { get; set; }
        public string Top6ImageURL6 { get; set; }
        public string SnsYN { get; set; }
        public string SnsImageURL { get; set; }
    }
}
