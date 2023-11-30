using Barunn.MobileInvitation.Common.Helpers;
using Barunn.MobileInvitation.Common.Models.Configs;
using Barunn.MobileInvitation.Dac.Contexts;
using Barunn.MobileInvitation.Dac.Models.Barunson;
using Barunn.MobileInvitation.Web.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Controllers
{
    [Authorize]
    public class InvitationController : BaseController
    {
        private readonly IStaticContentHelper _staticContent;

        public InvitationController(ILogger<InvitationController> logger, IOptions<BarunnConfig> barunnConfig, BarunsonContext barunsonDb, BarShopContext barshopDb,
            IStaticContentHelper staticContent)
             : base(logger, barunnConfig, barunsonDb, barshopDb)
        {
            _staticContent = staticContent;
        }

        private async Task InitModelAsync(InvitationViewModel model)
        {
            model.BarunsonmCardUrl = _barunnConfig.StaticContent.BaseUrl;
            model.BarunsonmCardMobileUrl = new Uri(_barunnConfig.UserSiteUrl, "m/");

            var defaultInfos = await (from a in _barunsonDb.TB_ReservationWords select a).ToListAsync();
            ViewData["defaultInfos"] = defaultInfos;
            model.MappingFields = JsonSerializer.Serialize(defaultInfos.Select(a => new { a.ReserveWord, a.MappingField, a.DefaultValue, a.Mapping_YN, a.Product_Category_Codes }));

            var orderItem = await (from o in _barunsonDb.TB_Orders where o.Order_ID == model.Order_ID select o).FirstAsync();
            model.Order_Code = orderItem.Order_Code;
            model.Name = orderItem.Name;

            if(model.Invitation_ID == 0 )
            {
                var invitaionQuery = from r in _barunsonDb.TB_Invitations where r.Order_ID == model.Order_ID select new { r.Invitation_ID, r.Template_ID };
                var invitaionItem = await invitaionQuery.FirstAsync();
                model.Invitation_ID = invitaionItem.Invitation_ID;
                model.Template_ID = invitaionItem.Template_ID;
            }
            var templateQuery = from t in _barunsonDb.TB_Templates
                                where t.Template_ID == model.Template_ID
                                select new
                                {
                                    t.Template_ID,
                                    t.Photo_YN,
                                    t.Attached_File1_URL,
                                    t.Attached_File2_URL
                                };
            var templateItem = await templateQuery.FirstAsync();
            model.Attached_File1_URL = templateItem.Attached_File1_URL;
            model.Attached_File2_URL = templateItem.Attached_File2_URL;
            model.Photo_YN = templateItem.Photo_YN == "Y";

            var productQuery = from p in _barunsonDb.TB_Products
                               join op in _barunsonDb.TB_Order_Products on p.Product_ID equals op.Product_ID
                               join c in _barunsonDb.TB_Common_Codes on p.Product_Category_Code equals c.Code
                               where op.Order_ID == model.Order_ID && c.Code_Group == "Product_Category_Code"
                               select new
                               {
                                   Product_ID = p.Product_ID,
                                   Product_Category_Code = p.Product_Category_Code,
                                   Product_Name = p.Product_Name,
                                   Product_Code = p.Product_Code,
                                   Original_Product_Code = p.Original_Product_Code,
                                   Product_Category_Name = c.Code_Name
                               };
            var productItem = await productQuery.FirstAsync();
            model.Product_ID = productItem.Product_ID;
            model.ProductCategoryCode = productItem.Product_Category_Code;
            model.ProductCategoryName = productItem.Product_Category_Name;
            model.Product_Name = productItem.Product_Name;
            model.Product_Code = productItem.Product_Code;
            model.Original_Product_Code = productItem.Original_Product_Code;

            var accountQuery = from ae in _barunsonDb.TB_Account_Extras
                               join b in _barunsonDb.TB_Banks on ae.Bank_Code equals b.Bank_Code into bc
                               from bank in bc.DefaultIfEmpty()
                               where ae.Invitation_ID == model.Invitation_ID && bank.Use_YN == "Y"
                               select new InvitationViewModel.Account
                               {
                                   Invitation_ID = ae.Invitation_ID,
                                   Sort = ae.Sort,
                                   Send_Target_Code = ae.Send_Target_Code,
                                   Send_Name = ae.Send_Name,
                                   Bank_Code = ae.Bank_Code,
                                   Bank_Name = bank.Bank_Name,
                                   Account_Number = ae.Account_Number,
                                   Account_Holder = ae.Account_Holder,
                                   Catetory = ae.Catetory
                               };
            var accountItems = await accountQuery.ToListAsync();
            model.Accounts = accountItems;
            model.GroomAccounts = accountItems.Where(m => m.Catetory == 1).ToList();
            model.BrideAccounts = accountItems.Where(m => m.Catetory == 2).ToList();

            var etcQuery = from a in _barunsonDb.TB_Invitation_Detail_Etcs where a.Invitation_ID == model.Invitation_ID select a;
            model.ETCs = await etcQuery.ToListAsync();

            var galleryQuery = from a in _barunsonDb.TB_Galleries where a.Invitation_ID == model.Invitation_ID orderby a.Sort select a;
            model.Gallery = await galleryQuery.ToListAsync();
            //정적 콘텐츠 URL을 절대 경로로 변경
            foreach (var item in model.Gallery)
            {
                item.Image_URL = _staticContent.AbsoluteUri(item.Image_URL).ToString();
            }
        }

        public async Task<IActionResult> IndexAsync(int id)
        {
            var model = new InvitationViewModel
            {
                Order_ID = id
            };
            await InitModelAsync(model);

            var invitationDetailQuery = from r in _barunsonDb.TB_Invitation_Details where r.Invitation_ID == model.Invitation_ID select r;
            model.invitation_Detail = await invitationDetailQuery.FirstAsync();
            //정적 콘텐츠 URL을 절대 경로로 변경 ex) /upload~~ 
            if (!string.IsNullOrEmpty(model.invitation_Detail.Delegate_Image_URL))
                model.Delegate_Image_AbsoluteUri = _staticContent.AbsoluteUri(model.invitation_Detail.Delegate_Image_URL).ToString();

            if (model.invitation_Detail.ExtendData != null) //확장 정보 
            {
                if (model.ProductCategoryCode == "PCC03") //초대장
                {
                    model.BabyInfos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BabyFirstBirthViewModel>>(model.invitation_Detail.ExtendData); 
                }
            }

            var areaQuery = from a in _barunsonDb.TB_Invitation_Areas 
                            join b in _barunsonDb.TB_Areas on a.Area_ID equals b.Area_ID into ar
                            from dear in ar.DefaultIfEmpty() 
                            where a.Invitation_ID == model.Invitation_ID 
                            select new InvitationViewModel.InvitationArea
                            {
                                Invitation_ID = a.Invitation_ID,
                                Area_ID = a.Area_ID,
                                Size_Height = a.Size_Height ?? 100,
                                Size_Width = a.Size_Width ?? 800,
                                Color = a.Color ?? "",
                                Sort = a.Sort ?? 0,
                                Product_Category_Codes = dear.Product_Category_Codes ?? ""
                            };
            model.InvitationAreas = await areaQuery.ToListAsync();

            var objectQuery = from a in _barunsonDb.TB_Invitation_Items
                                join b in _barunsonDb.TB_Item_Resources on a.Resource_ID equals b.Resource_ID
                                where a.Invitation_ID == model.Invitation_ID
                                orderby b.Sort
                                select new TemplateItemResource
                                {
                                    item_id = a.Item_ID,
                                    resource_id = b.Resource_ID,
                                    pid = "area" + a.Area_ID,
                                    id = "item_" + b.Sort,
                                    type = a.Item_Type_Code,
                                    top = a.Location_Top,
                                    left = a.Location_Left,
                                    height = a.Size_Height,
                                    width = a.Size_Width,
                                    chracterset = b.CharacterSet,
                                    fontsize = b.Character_Size,
                                    fontcolor = b.Color,
                                    bgcolor = b.Background_Color,
                                    bold_yn = b.Bold_YN == "Y",
                                    italic_yn = b.Italic_YN == "Y",
                                    underline_yn = b.Underline_YN == "Y",
                                    between_text = b.BetweenText,
                                    between_line = b.BetweenLine,
                                    vertical_align = b.Vertical_Alignment,
                                    horizontal_align = b.Horizontal_Alignment,
                                    zindex = b.Sort,
                                    font = b.Font,
                                    resource_url = b.Resource_URL,
                                    org_height = b.Resource_Height,
                                    org_width = b.Resource_Width
                                };
            model.TemplateItemResources = await objectQuery.ToListAsync();
            model.TemplateItemResources.ForEach(m =>
            {
                m.type = ItemTypeCodeToResourceTypeCode(m.type);
                if (!string.IsNullOrEmpty(m.resource_url))
                {
                    m.resource_absoluteurl = _staticContent.AbsoluteUri(m.resource_url).AbsoluteUri;
                }
            });

            return View(model);
        }

        private bool VaildProdRegModel(InvitationViewModel model)
        {
            return ModelState.IsValid;
        }

        [RequestFormLimits(ValueCountLimit = 4096)]
        [HttpPost]
        public async Task<IActionResult> Save(InvitationViewModel model)
        {

            // Product Image 저장.
            // Image_ID == 0 은 신규 등록. Temp 폴더에 저장된 이미지를 서비스폴더로 복사 및 URL 변경 필요. temp -> Product_ID
            // 기존 Image와 변경 사항 검사하여, 수정, 삭제 수행.

            if (VaildProdRegModel(model) && model.Invitation_ID > 0)
            {
                var now = DateTime.Now;
                var ipAddr = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                var currUser = CurrentUserId();
                var deleteImageFiles = new List<string>();

                using var trans = await _barunsonDb.Database.BeginTransactionAsync();

                #region Invitation

                var invitation = await (from a in _barunsonDb.TB_Invitations where a.Invitation_ID == model.Invitation_ID select a).FirstAsync();

                invitation.Update_User_ID = currUser;
                invitation.Update_DateTime = now;
                invitation.Update_IP = ipAddr;

                #endregion

                #region Invitation Detail

                var invitationDetail = await (from a in _barunsonDb.TB_Invitation_Details where a.Invitation_ID == model.Invitation_ID select a).FirstAsync();

                if (!string.IsNullOrEmpty(invitationDetail.Invitation_URL) && invitationDetail.Invitation_URL != model.invitation_Detail.Invitation_URL)
                {
                    var item = new TB_Kakao_Cache
                    {
                        Cache_URL = new Uri(_barunnConfig.UserSiteUrl, "m/"+ invitationDetail.Invitation_URL).ToString(),
                        Progress_YN = "N",
                        Regist_DateTime = DateTime.Now
                    };
                    _barunsonDb.TB_Kakao_Cache.Add(item);
                }
                    

                invitationDetail.Invitation_Title = model.invitation_Detail.Invitation_Title;
                invitationDetail.Invitation_URL = model.invitation_Detail.Invitation_URL;
                invitationDetail.Greetings = model.invitation_Detail.Greetings;
                invitationDetail.Groom_Name = model.invitation_Detail.Groom_Name;
                invitationDetail.Groom_Phone = model.invitation_Detail.Groom_Phone;
                invitationDetail.Bride_Name = model.invitation_Detail.Bride_Name;
                invitationDetail.Bride_Phone = model.invitation_Detail.Bride_Phone;
                invitationDetail.Groom_Parents1_Name = model.invitation_Detail.Groom_Parents1_Name;
                invitationDetail.Groom_Parents1_Phone = model.invitation_Detail.Groom_Parents1_Phone;
                invitationDetail.Groom_Parents2_Name = model.invitation_Detail.Groom_Parents2_Name;
                invitationDetail.Groom_Parents2_Phone = model.invitation_Detail.Groom_Parents2_Phone;
                invitationDetail.Bride_Parents1_Name = model.invitation_Detail.Bride_Parents1_Name;
                invitationDetail.Bride_Parents1_Phone = model.invitation_Detail.Bride_Parents1_Phone;
                invitationDetail.Bride_Parents2_Name = model.invitation_Detail.Bride_Parents2_Name;
                invitationDetail.Bride_Parents2_Phone = model.invitation_Detail.Bride_Parents2_Phone;

                invitationDetail.WeddingDate = model.invitation_Detail.WeddingDate;
                if (model.invitation_Detail.WeddingHHmm != null)
                {
                    invitationDetail.WeddingHHmm = model.invitation_Detail.WeddingHHmm;
                }
                invitationDetail.Time_Type_Code = model.invitation_Detail.Time_Type_Code;
                invitationDetail.WeddingYY = model.invitation_Detail.WeddingYY;
                invitationDetail.WeddingMM = model.invitation_Detail.WeddingMM;
                invitationDetail.WeddingDD = model.invitation_Detail.WeddingDD;
                invitationDetail.WeddingWeek = model.invitation_Detail.WeddingWeek;
                invitationDetail.WeddingHour = model.invitation_Detail.WeddingHour;
                invitationDetail.WeddingMin = model.invitation_Detail.WeddingMin;

                invitationDetail.Weddinghall_Name = model.invitation_Detail.Weddinghall_Name ?? "";
                invitationDetail.WeddingHallDetail = model.invitation_Detail.WeddingHallDetail;
                invitationDetail.Weddinghall_Address = model.invitation_Detail.Weddinghall_Address;
                invitationDetail.Weddinghall_PhoneNumber = model.invitation_Detail.Weddinghall_PhoneNumber;
                invitationDetail.WeddingWeek_Eng_YN = model.invitation_Detail.WeddingWeek_Eng_YN;
                invitationDetail.Time_Type_Eng_YN = model.invitation_Detail.Time_Type_Eng_YN;

                if (!string.IsNullOrEmpty(model.invitation_Detail.Flower_gift_YN))
                {
                    if (invitationDetail.Flower_gift_YN == "Y" && model.invitation_Detail.Flower_gift_YN == "N")
                    {
                        // 취소
                        invitationDetail.Flower_gift_YN = "C";
                    }
                    else if (!(invitationDetail.Flower_gift_YN == "C" && model.invitation_Detail.Flower_gift_YN == "N") &&
                        (invitationDetail.Flower_gift_YN != model.invitation_Detail.Flower_gift_YN))
                    {
                        //기존값이 C(취소)이고 입력값이 N이 아니고, 기존 값과 입력값이 다를 경우만 업데이트
                        invitationDetail.Flower_gift_YN = model.invitation_Detail.Flower_gift_YN;
                    }
                }

                if (invitationDetail.Delegate_Image_URL != null)
                {
                    invitationDetail.Delegate_Image_URL = model.invitation_Detail.Delegate_Image_URL;
                    invitationDetail.Delegate_Image_Height = model.invitation_Detail.Delegate_Image_Height;
                    invitationDetail.Delegate_Image_Width = model.invitation_Detail.Delegate_Image_Width;
                }
                //아기정보 추가
                if (model.ProductCategoryCode == "PCC03")
                    invitationDetail.ExtendData = Newtonsoft.Json.JsonConvert.SerializeObject(model.BabyInfos);

                invitationDetail.Update_User_ID = currUser;
                invitationDetail.Update_DateTime = now;
                invitationDetail.Update_IP = ipAddr;

                #endregion

                #region Invitation Area
                var invitationAreas = await (from a in _barunsonDb.TB_Invitation_Areas where a.Invitation_ID == model.Invitation_ID select a).ToListAsync();
                foreach (var item in model.InvitationAreas)
                {
                    var area = invitationAreas.First(m => m.Area_ID == item.Area_ID);
                    area.Size_Height = item.Size_Height;
                    area.Size_Width = item.Size_Width;
                    area.Color = item.Color;
                    area.Sort = item.Area_ID;

                    area.Update_User_ID = currUser;
                    area.Update_DateTime = now;
                    area.Update_IP = ipAddr;
                }
                #endregion

                #region Invitation Item Resource

                var InvitationItems = await (from a in _barunsonDb.TB_Invitation_Items where a.Invitation_ID == model.Invitation_ID select a).ToListAsync();
                var resIds = InvitationItems.Select(m => m.Resource_ID).ToList();
                var itemResources = await (from a in _barunsonDb.TB_Item_Resources where resIds.Contains(a.Resource_ID) select a).ToListAsync();
                //삭제 항목
                var resource_ids = model.TemplateItemResources.Select(m => m.resource_id);
                var removeitemResources = itemResources.Where(m => !resource_ids.Contains(m.Resource_ID));
                var removeItems = InvitationItems.Where(m => !resource_ids.Contains(m.Resource_ID));

                _barunsonDb.TB_Invitation_Items.RemoveRange(removeItems);
                _barunsonDb.TB_Item_Resources.RemoveRange(removeitemResources);

                foreach (TemplateItemResource item in model.TemplateItemResources)
                {
                    var Invitation_item = InvitationItems.FirstOrDefault(m => m.Item_ID == item.item_id);
                    var item_resource = itemResources.FirstOrDefault(m => m.Resource_ID == item.resource_id);
                    if (Invitation_item == null || item_resource == null) //추가
                    {
                        Invitation_item = new TB_Invitation_Item();
                        item_resource = new TB_Item_Resource();

                        Invitation_item.Invitation_ID = model.Invitation_ID;
                        Invitation_item.Regist_User_ID = currUser;
                        Invitation_item.Regist_DateTime = now;
                        Invitation_item.Regist_IP = ipAddr;
                        Invitation_item.Resource = item_resource;

                        item_resource.Regist_User_ID = currUser;
                        item_resource.Regist_DateTime = now;
                        item_resource.Regist_IP = ipAddr;

                        _barunsonDb.TB_Item_Resources.Add(item_resource);
                        _barunsonDb.TB_Invitation_Items.Add(Invitation_item);
                    }
                    Invitation_item.Area_ID = Int32.Parse(item.pid.Replace("area", ""));
                    Invitation_item.Item_Type_Code = ResourceTypeCodeToItemTypeCode(item.type);
                    Invitation_item.Location_Top = item.top;
                    Invitation_item.Location_Left = item.left;
                    Invitation_item.Size_Height = item.height;
                    Invitation_item.Size_Width = item.width;
                    Invitation_item.Update_User_ID = currUser;
                    Invitation_item.Update_DateTime = now;
                    Invitation_item.Update_IP = ipAddr;

                    item_resource.CharacterSet = item.chracterset;
                    item_resource.Character_Size = item.fontsize;
                    item_resource.Color = item.fontcolor;
                    item_resource.Background_Color = item.bgcolor;
                    item_resource.Bold_YN = item.bold_yn ? "Y" : "N";
                    item_resource.Italic_YN = item.italic_yn ? "Y" : "N";
                    item_resource.Underline_YN = item.underline_yn ? "Y" : "N";
                    item_resource.BetweenText = item.between_text;
                    item_resource.BetweenLine = item.between_line;
                    item_resource.Vertical_Alignment = item.vertical_align;
                    item_resource.Horizontal_Alignment = item.horizontal_align;
                    item_resource.Sort = item.zindex;
                    item_resource.Font = item.font;

                    if (!string.IsNullOrEmpty(item_resource.Resource_URL) && item_resource.Resource_URL != item.resource_url)
                        deleteImageFiles.Add(item_resource.Resource_URL);

                    item_resource.Resource_URL = await FileMoveToProductAsync(model.TemporaryId, item.resource_url, model.FileRelativePath);
                    item_resource.Resource_Height = item.org_height;
                    item_resource.Resource_Width = item.org_width;
                    item_resource.Resource_Type_Code = item.type;
                    item_resource.Update_User_ID = currUser;
                    item_resource.Update_DateTime = now;
                    item_resource.Update_IP = ipAddr;
                }
                #endregion

                await _barunsonDb.SaveChangesAsync();

                await trans.CommitAsync();

                //이미지 파일 삭제
                foreach (var delFile in deleteImageFiles)
                    await _staticContent.DeleteFileAsync(delFile);

                return RedirectToAction("Index", new { id = model.Order_ID });
            }
            else
            {
                await InitModelAsync(model);
                return View("Index", model);
            }
        }

        /// <summary>
        /// 임시 파일을 운영환경으로 이동
        /// </summary>
        private async Task<string> FileMoveToProductAsync(string temporaryId, string tempPath, string original_Product_Code)
        {
            string result = tempPath;
            var tempFolderName = $"/{temporaryId}/";

            if (!string.IsNullOrEmpty(tempPath) && tempPath.Contains(tempFolderName))
            {
                var prodFile = tempPath.Replace(tempFolderName, $"/{original_Product_Code}/");
                if (_staticContent.ExistsFile(prodFile))
                {
                    //이미 복사 되어 있을 경우
                    result = prodFile;
                }
                else
                {
                    var (status, message) = await _staticContent.MoveToFileAsync(tempPath, prodFile);

                    if (status == true)
                    {
                        if (!message.StartsWith("/"))
                            message = $"/{message}";
                        result = message;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Template 이미지 업로드
        /// </summary>
        /// <param name="files"></param>
        /// <param name="TemporaryId"></param>
        /// <param name="TypeCode"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadTemplateImage(List<IFormFile> files, string TemporaryId, string TypeCode, string imageData)
        {
            try
            {
                Uri absoluteUri = null;
                int width = 0;
                int height = 0;
                bool status = false;
                string message = "";
                string filename = "";

                if (TypeCode == "photo" && !string.IsNullOrEmpty(imageData))
                {
                    filename = Guid.NewGuid().ToString() + ".png";
                    var rPath = Path.Combine(_barunnConfig.FileConfig.UploadContainer, "invitation", TemporaryId, filename);
                    (status, message) = await _staticContent.UploadFileAsync(imageData, rPath);
                }
                else if (files != null && files.Count > 0 && files[0].Length > 0)
                {
                    var ext = Path.GetExtension(files[0].FileName).ToLower();
                    filename = Guid.NewGuid().ToString() + ext;
                    var rPath = Path.Combine(_barunnConfig.FileConfig.UploadContainer, "invitation", TemporaryId, filename);
                    (status, message) = await _staticContent.UploadFileAsync(files[0], rPath);
                }

                if (status)
                {
                    if (TypeCode == "Image" || TypeCode == "photo")
                    {
                        using (var image = Image.FromFile(_staticContent.GetFileFullName(message)))
                        {
                            width = image.Width;
                            height = image.Height;
                        }
                    }
                    absoluteUri = _staticContent.AbsoluteUri(message);

                    return Json(new { success = true, typeCode = TypeCode, resource_url = message, resource_absoluteurl = absoluteUri, message = filename, width = width, height = height });
                }
                else
                {
                    return Json(new { success = false, typeCode = TypeCode, message = "" });
                }

            }
            catch (Exception e)
            {
                return Json(new { success = false, typeCode = TypeCode, message = e.Message });
            }

        }

        public async Task<IActionResult> GuestBook(int InvitationId, string Type, int Id)
        {
            var query = from a in _barunsonDb.TB_GuestBooks
                        where a.Invitation_ID == InvitationId &&
                        a.Display_YN == "Y" &&
                        ((Id == 0) || (a.GuestBook_ID < Id) )
                        orderby a.GuestBook_ID descending
                        select a;

            var items = await query.Take(10).ToListAsync();

            return View("_GuestBook", items);
        }
    }
}
