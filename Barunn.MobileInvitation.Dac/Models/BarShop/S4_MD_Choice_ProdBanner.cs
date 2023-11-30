using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class S4_MD_Choice_ProdBanner
    {
        public int seq { get; set; }
        public int md_seq { get; set; }
        public int company_seq { get; set; }
        public string banner_title { get; set; }
        public string target_type { get; set; }
        public string item_type1_yorn { get; set; }
        public string item_type2_yorn { get; set; }
        public string card_code { get; set; }
        public string pc_show_yorn { get; set; }
        public string pc_banner_image { get; set; }
        public string pc_move_url { get; set; }
        public int pc_click_count { get; set; }
        public string pc_new_win_yorn { get; set; }
        public string mo_show_yorn { get; set; }
        public string mo_banner_image { get; set; }
        public string mo_move_url { get; set; }
        public int mo_click_count { get; set; }
        public string mo_new_win_yorn { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime end_date { get; set; }
        public DateTime? reg_date { get; set; }
        public string reg_admin_id { get; set; }
        public DateTime? mod_date { get; set; }
        public string mod_admin_id { get; set; }
        public string banner_status { get; set; }
        public string use_yorn { get; set; }
        public int sort { get; set; }
        public string pc_title { get; set; }
        public string pc_content { get; set; }
        public string mo_title { get; set; }
        public string mo_content { get; set; }
    }
}
