using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class custom_order_plist
    {
        public long id { get; set; }
        public int? sid { get; set; }
        public int order_seq { get; set; }
        public string isFPrint { get; set; }
        public string print_type { get; set; }
        public int card_seq { get; set; }
        public string title { get; set; }
        public int? print_count { get; set; }
        public string etc_comment { get; set; }
        public string etc_info_s { get; set; }
        public string order_filename { get; set; }
        public short option_price { get; set; }
        public string isNotSet { get; set; }
        public string isNotPrint { get; set; }
        public string env_zip { get; set; }
        public string env_addr { get; set; }
        public string env_addr_detail { get; set; }
        public string env_phone { get; set; }
        public string env_hphone { get; set; }
        public string env_person1_header { get; set; }
        public string env_person2_header { get; set; }
        public string env_person1 { get; set; }
        public string env_person2 { get; set; }
        public string env_person_Tail { get; set; }
        public string isEnv_person_tail { get; set; }
        public string env_person1_tail { get; set; }
        public string env_person2_tail { get; set; }
        public string isZipBox { get; set; }
        public string recv_tail { get; set; }
        public string isPostMark { get; set; }
        public string postname { get; set; }
        public byte? pstatus { get; set; }
        public int? preview_seq { get; set; }
        public string imgFolder { get; set; }
        public string imgName { get; set; }
        public DateTime? choan_date { get; set; }
        public DateTime? print_date { get; set; }
        public string print_admin_id { get; set; }
        public string choanmerge_date { get; set; }
        public long? up_id { get; set; }
        public string isQrcode { get; set; }
        public string postname_tail { get; set; }
        public string isBasic { get; set; }
        public string env_hphone2 { get; set; }
        public int? env_addr_type { get; set; }
        public string env_road_addr { get; set; }
        public string env_road_addr_detail { get; set; }
        public DateTime? REG_DATE { get; set; }
        public string isNotPrint_Addr { get; set; }
        public string EnvSpecialType { get; set; }
    }
}
