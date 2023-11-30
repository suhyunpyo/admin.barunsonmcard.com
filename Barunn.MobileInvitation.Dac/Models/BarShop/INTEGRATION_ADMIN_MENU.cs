using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class INTEGRATION_ADMIN_MENU
    {
        public string MENU_SEQ { get; set; }
        public string MENU_TITLE { get; set; }
        public string PMENU_SEQ { get; set; }
        public int DEPTH { get; set; }
        public int SORT_NUM { get; set; }
        public string FOLDER_YORN { get; set; }
        public string PUBLIC_YORN { get; set; }
        public string LINK { get; set; }
        public DateTime? REG_DATE { get; set; }
        public string FONT_AWESOME { get; set; }
    }
}
