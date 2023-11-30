using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class Mcard_MoneyGift
    {
        public int InvitationID { get; set; }
        public string InvitationCode { get; set; }
        public string Groom_BankCode { get; set; }
        public string Groom_Account { get; set; }
        public string Groom_URL { get; set; }
        public string Birde_BankCode { get; set; }
        public string Birde_Account { get; set; }
        public string Birde_URL { get; set; }
        public string Wedding_Date { get; set; }
        public DateTime? Created_Tmstmp { get; set; }
        public DateTime? UPDATED_Tmstmp { get; set; }
        public string Return_Groom_AccountCode { get; set; }
        public string Return_Groom_AccountMessage { get; set; }
        public string Return_Birde_AccountCode { get; set; }
        public string Return_Birde_AccountMessage { get; set; }
        public string Return_AccountGroomName { get; set; }
        public string Return_AccountBirdeName { get; set; }
        public string Return_Code { get; set; }
        public string Return_Message { get; set; }
        public string Return_CoupleKey { get; set; }
        public DateTime? Return_Updated_Tmstmp { get; set; }
        public string DisableYN { get; set; }
        public string Return_Disable_Code { get; set; }
        public string Return_Disable_Message { get; set; }
        public DateTime? DisableY_Tmstmp { get; set; }
    }
}
