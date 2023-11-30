using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class mcard_Invitation
    {
        public mcard_Invitation()
        {
            mcard_Comments = new HashSet<mcard_Comment>();
            mcard_Galleries = new HashSet<mcard_Gallery>();
            mcard_InvitationBabies = new HashSet<mcard_InvitationBaby>();
            mcard_InvitationGiftRels = new HashSet<mcard_InvitationGiftRel>();
            mcard_InvitationParties = new HashSet<mcard_InvitationParty>();
            mcard_InvitationWeddings = new HashSet<mcard_InvitationWedding>();
        }

        public int InvitationID { get; set; }
        public string InvitationCode { get; set; }
        public string InvitationType { get; set; }
        public string AuthYN { get; set; }
        public int SkinID { get; set; }
        public string OrdererName { get; set; }
        public string OrdererMobile { get; set; }
        public string OrdererEmail { get; set; }
        public string InvitationTitle { get; set; }
        public string MainImage { get; set; }
        public string CommentYN { get; set; }
        public string HostYN { get; set; }
        public string VideoYN { get; set; }
        public string GiftYN { get; set; }
        public string GalleryYN { get; set; }
        public string GalleryType { get; set; }
        public string GuideYN { get; set; }
        public DateTime? RegisterTime { get; set; }
        public string OnlineYN { get; set; }
        public string Greeting { get; set; }
        public string EventTime { get; set; }
        public string LocationName { get; set; }
        public string LocationAddr { get; set; }
        public string LocationDetail { get; set; }
        public string LocationMapType { get; set; }
        public string LocationMapImage { get; set; }
        public string LocationMapLat { get; set; }
        public string LocationMapLng { get; set; }
        public string LocationTel { get; set; }
        public string VideoURL { get; set; }
        public string EtcSubway { get; set; }
        public string EtcBus { get; set; }
        public string EtcCar { get; set; }
        public string EtcParking { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public string RegisterIP { get; set; }
        public int MainImageSize { get; set; }
        public int MainImageWidth { get; set; }
        public int MainImageHeight { get; set; }
        public int LocationMapImageSize { get; set; }
        public int LocationMapImageWidth { get; set; }
        public int LocationMapImageHeight { get; set; }
        public string AuthCode { get; set; }
        public string EventDate { get; set; }
        public string EtcGuide { get; set; }
        public string VideoType { get; set; }
        public string EtcAfter { get; set; }
        public string EtcInfo { get; set; }
        public string SiteCode { get; set; }
        public string OrderSeq { get; set; }
        public string SkinCode { get; set; }
        public DateTime? CompletedTime { get; set; }
        public string PublishYN { get; set; }
        public string SmsInvitationYN { get; set; }
        public string SmsMypageYN { get; set; }
        public string ExpireYN { get; set; }
        public string DeleteYN { get; set; }
        public string EventEndTime { get; set; }
        public string MoneyGiftYN { get; set; }
        public string AdDisplayYN { get; set; }

        public virtual mcard_Skin Skin { get; set; }
        public virtual ICollection<mcard_Comment> mcard_Comments { get; set; }
        public virtual ICollection<mcard_Gallery> mcard_Galleries { get; set; }
        public virtual ICollection<mcard_InvitationBaby> mcard_InvitationBabies { get; set; }
        public virtual ICollection<mcard_InvitationGiftRel> mcard_InvitationGiftRels { get; set; }
        public virtual ICollection<mcard_InvitationParty> mcard_InvitationParties { get; set; }
        public virtual ICollection<mcard_InvitationWedding> mcard_InvitationWeddings { get; set; }
    }
}
