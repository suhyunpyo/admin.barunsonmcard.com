using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class OUTSOURCING_ORDER_MST
    {
        public int OUTSOURCING_ORDER_SEQ { get; set; }
        public string ORDER_STATUS_CODE { get; set; }
        public string ORDER_TYPE_CODE { get; set; }
        public string ORDER_SUB_TYPE_CODE { get; set; }
        public int? ORDER_SEQ { get; set; }
        public string SITE_TYPE_CODE { get; set; }
        public string ERP_PART_TYPE_CODE { get; set; }
        public string CARD_CODE { get; set; }
        public string ORDER_NAME { get; set; }
        public int? ORDER_QTY { get; set; }
        public string PAPER_TYPE_NAME { get; set; }
        public string PAPER_SIZE { get; set; }
        public decimal? PAGES_PER_SHEET_VALUE { get; set; }
        public decimal? PRINT_LOSS_VALUE { get; set; }
        public string BOTH_SIDE_YORN { get; set; }
        public string OSI_YORN { get; set; }
        public string CUTOUT_YORN { get; set; }
        public string GLOSSY_YORN { get; set; }
        public string PRESS_YORN { get; set; }
        public string FOIL_TYPE_NAME { get; set; }
        public string LASER_CUT_YORN { get; set; }
        public string REQUESTOR_NAME { get; set; }
        public string COMPANY_TYPE_CODE { get; set; }
        public string DELIVERY_TYPE_CODE { get; set; }
        public string PRINT_FILE_URL { get; set; }
        public string IMAGE_FILE_URL { get; set; }
        public DateTime? RECEIPT_DATE { get; set; }
        public DateTime? REG_DATE { get; set; }
        public string MEMO { get; set; }
        public DateTime? EXPECT_DATE { get; set; }
        public string EDGE_YORN { get; set; }
        public string EDGE_COLOR { get; set; }
        public string PRINT_CHASU { get; set; }
        public string DEV_FLAG { get; set; }
        public string MEMO_EX { get; set; }
    }
}
