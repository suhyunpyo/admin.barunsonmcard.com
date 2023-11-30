using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class ata_mmt_log_201808
    {
        public int mt_pr { get; set; }
        public string mt_refkey { get; set; }
        public string priority { get; set; }
        public DateTime date_client_req { get; set; }
        public string subject { get; set; }
        public string content { get; set; }
        public string callback { get; set; }
        public string msg_status { get; set; }
        public string recipient_num { get; set; }
        public DateTime? date_mt_sent { get; set; }
        public DateTime? date_rslt { get; set; }
        public DateTime? date_mt_report { get; set; }
        public string report_code { get; set; }
        public string rs_id { get; set; }
        public string country_code { get; set; }
        public int msg_type { get; set; }
        public string crypto_yn { get; set; }
        public string ata_id { get; set; }
        public DateTime? reg_date_tran { get; set; }
        public DateTime? reg_date { get; set; }
        public string sender_key { get; set; }
        public string template_code { get; set; }
        public string response_method { get; set; }
        public string ad_flag { get; set; }
        public string kko_btn_type { get; set; }
        public string kko_btn_info { get; set; }
        public string img_url { get; set; }
        public string img_link { get; set; }
        public string etc_text_1 { get; set; }
        public string etc_text_2 { get; set; }
        public string etc_text_3 { get; set; }
        public int? etc_num_1 { get; set; }
        public int? etc_num_2 { get; set; }
        public int? etc_num_3 { get; set; }
        public DateTime? etc_date_1 { get; set; }
    }
}
