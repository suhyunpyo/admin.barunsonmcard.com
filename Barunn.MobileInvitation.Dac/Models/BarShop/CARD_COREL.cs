using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class CARD_COREL
    {
        public int id { get; set; }
        public string card_type { get; set; }
        public string card_code { get; set; }
        public string isOsiGaro { get; set; }
        public string isReverse { get; set; }
        public int BlankSpace { get; set; }
        public int print_size { get; set; }
        public string isFBackColor { get; set; }
        public string isBackColor { get; set; }
        public string isBackPoint { get; set; }
        public string env_num { get; set; }
        public double? pageSizeW { get; set; }
        public double? pageSizeH { get; set; }
        public double? Hstandard { get; set; }
        public double? HstandardMax { get; set; }
        public double? Vstandard { get; set; }
        public double? Lstandard { get; set; }
        public double? Rstandard { get; set; }
        public string guideLineH { get; set; }
        public string guideLineV { get; set; }
        public string osiH { get; set; }
        public string osiV { get; set; }
        public double? mapPositionX { get; set; }
        public double? mapPositionY { get; set; }
        public string IsMapGaro { get; set; }
        public string IsMapFront { get; set; }
        public string IsExPreview { get; set; }
        public string IsFPrint { get; set; }
        public string IsInpaperBothSide { get; set; }
        public string IsAlbaDown { get; set; }
        public string PrintGroup { get; set; }
        public double? FPrintX { get; set; }
        public double? FPrintY { get; set; }
        public double? WhenX { get; set; }
        public double? WhenY { get; set; }
        public string WhenFont { get; set; }
        public short? WhenFontSize { get; set; }
        public double? WhereX { get; set; }
        public double? WhereY { get; set; }
        public string WhereFont { get; set; }
        public short? WhereFontSize { get; set; }
        public double? GBX { get; set; }
        public double? GBY { get; set; }
        public string GBFont { get; set; }
        public short? GBFontSize1 { get; set; }
        public short? GBFontSize2 { get; set; }
        public short? GBFontSize3 { get; set; }
        public double? GreetX { get; set; }
        public double? GreetY { get; set; }
        public string GreetFont { get; set; }
        public short? GreetFontSize { get; set; }
        public int? printSizeW { get; set; }
        public int? printSizeH { get; set; }
        public string BOTH_SIDE_YORN { get; set; }
        public string INNER_WORK_YORN { get; set; }
        public string WEPOD_YORN { get; set; }
        public string PAPER_SIZE_TYPE { get; set; }
        public string PAPER_NAME { get; set; }
        public int? PAPER_TICKNESS { get; set; }
        public int? PAPER_COMPOSITION { get; set; }
        public string admin_id { get; set; }
        public DateTime? mod_date { get; set; }
        public string CUTTING_LINE_DIRECTION { get; set; }
        public string AUTO_CHOAN_YORN { get; set; }
        public string AUTO_CHOAN_REGISTER_YORN { get; set; }
        public string isColorInpaper { get; set; }
        public double? EnvJacketSizeW { get; set; }
        public double? EnvJacketSizeH { get; set; }
    }
}
