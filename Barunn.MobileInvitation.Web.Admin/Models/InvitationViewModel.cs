using Barunn.MobileInvitation.Dac.Models.Barunson;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Models
{
    public class InvitationViewModel
    {
        public Uri BarunsonmCardUrl { get; set; }
        public Uri BarunsonmCardMobileUrl { get; set; }
        /// <summary>
        /// 파일 저장소 폴더용 임시 ID
        /// </summary>
        public string TemporaryId { get { return "temp"; } }

        public string FileRelativePath { get { return $"{Order_Code.Substring(1, 6)}/{Invitation_ID}";  } }

        /// <summary>
        /// 주문 번호
        /// </summary>
        public int Order_ID { get; set; }

        /// <summary>
        /// 주문 코드
        /// </summary>
        public string Order_Code { get; set; }

        /// <summary>
        /// 이름
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 초대장 ID
        /// </summary>
        public int Invitation_ID { get; set; }

        /// <summary>
        /// 제품 ID (Key)
        /// </summary>
        public int Product_ID { get; set; }

        /// <summary>
        /// 제품 분류
        /// </summary>
        public string ProductCategoryCode { get; set; }

        /// <summary>
        /// 제품 분류 명
        /// </summary>
        public string ProductCategoryName { get; set; }

        /// <summary>
        /// 제품 이름
        /// </summary>
        public string Product_Name { get; set; }

        /// <summary>
        /// 제품코드
        /// </summary>
        public string Product_Code { get; set; }

        /// <summary>
        /// original 제품코드
        /// </summary>
        public string Original_Product_Code { get; set; }

        /// <summary>
        /// 템플릿 ID
        /// </summary>
        public int Template_ID { get; set; }
        public string Attached_File1_URL { get; set; }
        public string Attached_File2_URL { get; set; }

        /// <summary>
        /// 포토형
        /// </summary>
        public bool Photo_YN { get; set; }

        public TB_Invitation_Detail invitation_Detail { get; set; }
        public string Delegate_Image_AbsoluteUri { get; set; }

        public List<Account> Accounts { get; set; }
        public List<Account> GroomAccounts { get; set; }
        public List<Account> BrideAccounts { get; set; }
        public class Account
        {
            public int Invitation_ID { get; set; }
            public int Sort { get; set; }
            public string Send_Target_Code { get; set; }
            public string Send_Name { get; set; }
            public string Bank_Code { get; set; }
            public string Bank_Name { get; set; }
            public string Account_Number { get; set; }
            public string Account_Holder { get; set; }
            public int? Catetory { get; set; }
        }

        public List<InvitationArea> InvitationAreas { get; set; }
        public class InvitationArea : TamplateArea
        {
            public int Invitation_ID { get; set; }

        }

        public List<TemplateItemResource> TemplateItemResources { get; set; }

        public List<TB_Invitation_Detail_Etc> ETCs { get; set; }

        public List<TB_Gallery> Gallery { get; set; }

        public string MappingFields { get; set; }

        public List<BabyFirstBirthViewModel> BabyInfos { get; set; }

    }
}