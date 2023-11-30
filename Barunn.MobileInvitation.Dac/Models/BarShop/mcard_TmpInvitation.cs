using System;
using System.Collections.Generic;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.BarShop
{
    public partial class mcard_TmpInvitation
    {
        public mcard_TmpInvitation()
        {
            mcard_TmpInvitationGiftRels = new HashSet<mcard_TmpInvitationGiftRel>();
            mcard_TmpInvitationOptions = new HashSet<mcard_TmpInvitationOption>();
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
        public DateTime RegisterTime { get; set; }
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
        public string BabyName { get; set; }
        public string BabyBirth { get; set; }
        public string DadName { get; set; }
        public string DadMobile { get; set; }
        public string MomName { get; set; }
        public string MomMobile { get; set; }
        public string GroupName { get; set; }
        public string GroupMobile { get; set; }
        public string GuideName { get; set; }
        public string GuideNote { get; set; }
        public string GroomName { get; set; }
        public string GroomMobile { get; set; }
        public string BrideName { get; set; }
        public string BrideMobile { get; set; }
        public string GroomHostRel1 { get; set; }
        public string GroomHostName1 { get; set; }
        public string GroomHostMobile1 { get; set; }
        public string GroomHostRel2 { get; set; }
        public string GroomHostName2 { get; set; }
        public string GroomHostMobile2 { get; set; }
        public string BrideHostRel1 { get; set; }
        public string BrideHostName1 { get; set; }
        public string BrideHostMobile1 { get; set; }
        public string BrideHostRel2 { get; set; }
        public string BrideHostName2 { get; set; }
        public string BrideHostMobile2 { get; set; }
        public string EventDate { get; set; }
        public string EtcGuide { get; set; }
        public string VideoType { get; set; }
        public string EtcAfter { get; set; }
        public string EtcInfo { get; set; }
        public DateTime? CompletedTime { get; set; }
        public string PublishYN { get; set; }
        public string SmsInvitationYN { get; set; }
        public string SmsMypageYN { get; set; }
        public string ExpireYN { get; set; }
        public string EventEndTime { get; set; }

        public virtual mcard_Skin Skin { get; set; }
        public virtual ICollection<mcard_TmpInvitationGiftRel> mcard_TmpInvitationGiftRels { get; set; }
        public virtual ICollection<mcard_TmpInvitationOption> mcard_TmpInvitationOptions { get; set; }
    }
}
