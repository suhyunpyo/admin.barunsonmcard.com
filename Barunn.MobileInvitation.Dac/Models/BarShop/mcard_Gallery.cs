using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class mcard_Gallery
    {
        public int GalleryID { get; set; }
        public int InvitationID { get; set; }
        public string InvitationCode { get; set; }
        public string ImagePath { get; set; }
        public int ImageOrder { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public int ImageSize { get; set; }
        public DateTime RegisterTime { get; set; }

        public virtual mcard_Invitation Invitation { get; set; }
    }
}
