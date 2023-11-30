using Barunn.MobileInvitation.Common.Models.Configs;
using Barunn.MobileInvitation.Common.Helpers;
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using Barunn.MobileInvitation.Common.Models;
using Barunn.MobileInvitation.Web.Admin.Services;

namespace Barunn.MobileInvitation.Web.Admin.Controllers
{
    /// <summary>
    /// 상품등록
    /// </summary>
    [Authorize]
    public class ProductRegistController : BaseController
    {
        private readonly IStaticContentHelper _staticContent;
        public ProductRegistController(ILogger<ProductRegistController> logger, IOptions<BarunnConfig> barunnConfig, BarunsonContext barunsonDb, BarShopContext barshopDb,
            IStaticContentHelper staticContent)
             : base(logger, barunnConfig, barunsonDb, barshopDb)
        {
            _staticContent = staticContent;
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
        /// 모델 초기화
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task InitModelAsync(ProductRegistViewModel model, bool isNew)
        {
            model.ProductBarnds = await GetSelectListsCommonCodesAsync("Product_Brand_Code");
            model.ProductCategoryCodes = await GetSelectListsCommonCodesAsync("Product_Category_Code");

            model.CodeProductCategories = new List<SelectListItem>();
            model.ProductCategories = new List<ProductCategoryCode>();

            var parentCategories = await GetSelectListsCategoryAsync(null);
            foreach (var preantCategory in parentCategories)
            {
                model.CodeProductCategories.Add(new SelectListItem { Value = preantCategory.Value, Text = preantCategory.Text });
                var cateNode = new ProductCategoryCode { Code = int.Parse(preantCategory.Value), Name = preantCategory.Text };
                model.ProductCategories.Add(cateNode);

                var childCategories = await GetSelectListsCategoryAsync(int.Parse(preantCategory.Value));
                foreach (var childCategory in childCategories)
                {
                    model.CodeProductCategories.Add(new SelectListItem { Value = childCategory.Value, Text = $"{preantCategory.Text} > {childCategory.Text}" });
                        
                    if (cateNode.Childs == null)
                        cateNode.Childs = new List<ProductCategoryCode>();

                    cateNode.Childs.Add(new ProductCategoryCode { Code = int.Parse(childCategory.Value), Name = childCategory.Text });
                }
            }

            if (model.Main_Categories == null || model.Main_Categories.Count == 0)
                model.Main_Categories = (await GetSelectListsMainCategoryAsync()).ToList();

            if (model.Main_Icons == null || model.Main_Icons.Count == 0)
            {
                var iconQuery = from a in _barunsonDb.TB_Icons
                                orderby a.Icon_ID
                                select new
                                {
                                    a.Icon_ID,
                                    a.Icon_URL
                                };
                var iconItems = await iconQuery.ToListAsync();

                model.Main_Icons = iconItems.Select(m => new SelectListItem
                {
                    Value = m.Icon_ID.ToString(),
                    Text = _staticContent.AbsoluteUri(m.Icon_URL).AbsoluteUri
                }).ToList();
            }
            model.Image_Category_Codes = await GetSelectListsCommonCodesAsync("Image_Category_Code");

            if (model.TemplateModel == null)
                model.TemplateModel = new TemplateViewModel();

            var defaultInfos = await (from a in _barunsonDb.TB_ReservationWords select new { a.ReserveWord, a.MappingField, a.DefaultValue, a.Mapping_YN, a.Product_Category_Codes }).ToListAsync();
            model.TemplateModel.MappingFields = JsonSerializer.Serialize(defaultInfos);
           
            if (isNew)
            {
                if (model.TemplateModel.Template == null)
                    model.TemplateModel.Template = new Dac.Models.Barunson.TB_Template();

                if (model.TemplateModel.TemplateDetail == null)
                {
                    model.TemplateModel.TemplateDetail = new Dac.Models.Barunson.TB_Template_Detail();

                    model.TemplateModel.TemplateDetail.Groom_Name = defaultInfos.Where(m => m.MappingField == "Groom_Name").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.Groom_Phone = defaultInfos.Where(m => m.MappingField == "Groom_Phone").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.Bride_Name = defaultInfos.Where(m => m.MappingField == "Bride_Name").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.Bride_Phone = defaultInfos.Where(m => m.MappingField == "Bride_Phone").Select(m => m.DefaultValue).FirstOrDefault();

                    model.TemplateModel.TemplateDetail.Groom_Parents1_Name = defaultInfos.Where(m => m.MappingField == "Groom_Parents1_Name").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.Groom_Parents1_Phone = defaultInfos.Where(m => m.MappingField == "Groom_Parents1_Phone").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.Groom_Parents2_Name = defaultInfos.Where(m => m.MappingField == "Groom_Parents2_Name").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.Groom_Parents2_Phone = defaultInfos.Where(m => m.MappingField == "Groom_Parents2_Phone").Select(m => m.DefaultValue).FirstOrDefault();

                    model.TemplateModel.TemplateDetail.Bride_Parents1_Name = defaultInfos.Where(m => m.MappingField == "Groom_Parents1_Name").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.Bride_Parents1_Phone = defaultInfos.Where(m => m.MappingField == "Bride_Parents1_Phone").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.Bride_Parents2_Name = defaultInfos.Where(m => m.MappingField == "Bride_Parents2_Name").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.Bride_Parents2_Phone = defaultInfos.Where(m => m.MappingField == "Bride_Parents2_Phone").Select(m => m.DefaultValue).FirstOrDefault();

                    model.TemplateModel.TemplateDetail.Baby_Name = defaultInfos.Where(m => m.MappingField == "Baby_Name").Select(m => m.DefaultValue).FirstOrDefault();
                    var babyBirthday = defaultInfos.Where(m => m.MappingField == "Baby_Birthday").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.Baby_Birthday = string.IsNullOrEmpty(babyBirthday) ? DateTime.Now : DateTime.Parse(babyBirthday);

                    model.TemplateModel.TemplateDetail.WeddingDate = defaultInfos.Where(m => m.MappingField == "WeddingDate").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.Time_Type_Code = defaultInfos.Where(m => m.MappingField == "Time_Type_Code").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.WeddingYY = defaultInfos.Where(m => m.MappingField == "WeddingYY").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.WeddingMM = defaultInfos.Where(m => m.MappingField == "WeddingMM").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.WeddingDD = defaultInfos.Where(m => m.MappingField == "WeddingDD").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.WeddingWeek = defaultInfos.Where(m => m.MappingField == "WeddingWeek").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.WeddingHour = defaultInfos.Where(m => m.MappingField == "WeddingHour").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.WeddingMin = defaultInfos.Where(m => m.MappingField == "WeddingMin").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.WeddingWeek_Eng_YN = "N";
                    model.TemplateModel.TemplateDetail.Time_Type_Eng_YN = "N";

                    model.TemplateModel.TemplateDetail.Greetings = defaultInfos.Where(m => m.MappingField == "Greetings").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.Weddinghall_Address = defaultInfos.Where(m => m.MappingField == "Weddinghall_Address").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.Weddinghall_Name = defaultInfos.Where(m => m.MappingField == "Weddinghall_Name").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.WeddingHallDetail = defaultInfos.Where(m => m.MappingField == "WeddingHallDetail").Select(m => m.DefaultValue).FirstOrDefault();
                    model.TemplateModel.TemplateDetail.Weddinghall_PhoneNumber = defaultInfos.Where(m => m.MappingField == "Weddinghall_PhoneNumber").Select(m => m.DefaultValue).FirstOrDefault();
                }
                var sampleQuery = from a in _barunsonDb.TB_Products
                                  join b in _barunsonDb.TB_Templates on a.Template_ID equals b.Template_ID
                                  where a.Product_ID > 40
                                  orderby b.Template_ID
                                  select new TemplateSample
                                  {
                                      Template_ID = b.Template_ID,
                                      Product_Code = a.Product_Code,
                                      Product_Category_Code = a.Product_Category_Code,
                                      Main_Image_URL = a.Main_Image_URL,
                                      Photo_YN = b.Photo_YN == "Y"
                                  };
                model.TemplateModel.TemplateSamples = await sampleQuery.ToListAsync();
                model.TemplateModel.TemplateSamples.ForEach(m =>
                {
                    m.Main_Image_URL = _staticContent.AbsoluteUri(m.Main_Image_URL).AbsoluteUri;
                });
                if (string.IsNullOrEmpty(model.TemplateModel.MoneyGift_Remit_Title_URL))
                    model.TemplateModel.MoneyGift_Remit_Title_URL = Url.Content("~/img/title/tit_remittance.png");

                if (model.TemplateModel.TemplateRepeats == null)
                {
                    model.TemplateModel.TemplateRepeats = new Dictionary<string, Dictionary<string, string>>();
                    var babyProperty = defaultInfos.Where(m => m.MappingField == "Baby_Info_List").Select(m => m.DefaultValue).FirstOrDefault();
                    if (!string.IsNullOrEmpty(babyProperty))
                    {
                        var jsonVal = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(babyProperty, new JsonSerializerOptions { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) });
                        model.TemplateModel.TemplateRepeats.Add(jsonVal.First().Key, jsonVal.First().Value);
                    }
                        
                }
            }
        }

        /// <summary>
        /// ProductCode 생성
        /// </summary>
        /// <param name="Product_Brand_Code"></param>
        /// <returns></returns>
        private async Task<string> GetNewProductCodeAsync(string Product_Brand_Code)
        {
            //MC{당해연도의 마지막 1자}{브렌드코드 구분 1자}{순차증가 2자}
            //ex) MC1604

            var year = DateTime.Now.Year.ToString().Last();
            var startCode = $"MC{year}";
            var lastProdCode = await (from a in _barunsonDb.TB_Products
                                      where a.Product_Brand_Code == Product_Brand_Code && a.Original_Product_Code.StartsWith(startCode)
                                      orderby a.Original_Product_Code descending
                                      select a.Original_Product_Code
                                      ).FirstOrDefaultAsync();
            int seq = 0;
            if (!string.IsNullOrEmpty(lastProdCode))
                seq = int.Parse(lastProdCode.Substring(4, 2)) + 1;

            string code = "";
            switch (Product_Brand_Code)
            {
                case "PBC01":
                    code = "2";
                    break;
                case "PBC02":
                    code = "0";
                    break;
                case "PBC03":
                    code = "4";
                    break;
                case "PBC04":
                    code = "6";
                    break;
            }

            return $"MC{year}{code}{seq:D2}";
        }

        /// <summary>
        /// Area 표시 영역
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        private async Task<List<TamplateArea>> GetAreasAsync(int templateId)
        {
            var areasQuery = from a in _barunsonDb.TB_Areas
                            select new TamplateArea
                            {
                                Area_ID = a.Area_ID,
                                Area_Name = a.Area_Name,
                                Edit_YN = a.Edit_YN == "Y",
                                Product_Category_Codes = a.Product_Category_Codes ?? "",
                                Size_Height = 100,
                                Size_Width = 800,
                                Color = "",
                                Sort = 0
                            };
            var areas = await areasQuery.ToListAsync();
            if (templateId > 0)
            {
                var tempAreas = from a in _barunsonDb.TB_Template_Areas
                                where a.Template_ID == templateId
                                select a;
                foreach(var ta in await tempAreas.ToListAsync())
                {
                    var area = areas.FirstOrDefault(m => m.Area_ID == ta.Area_ID);
                    if (area != null)
                    {
                        
                        area.Size_Height = ta.Size_Height ?? 100;
                        area.Size_Width = ta.Size_Width ?? 800;
                        area.Color = ta.Color ?? "";
                        area.Sort = ta.Sort ?? 0;

                        if (area.Area_ID == 14 && area.Color == "")
                            area.Color = "#F7F8F9";
                    }
                }
            }
            return areas;
        }
        /// <summary>
        /// 상품 등록
        /// </summary>
        /// <param name="id">Product_ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new ProductRegistViewModel();
            await InitModelAsync(model, true);
            model.ProductBarnd = model.ProductBarnds.First().Value;
            model.ProductCategoryCode = model.ProductCategoryCodes.First().Value;

            model.Original_Product_Code = await GetNewProductCodeAsync(model.ProductBarnd);

            model.Main_Image_URL = "/img/108x108_1.gif";
            model.Main_ImageAbsoluteUri = _staticContent.AbsoluteUri(model.Main_Image_URL);
            model.Preview_Image_URL = "/img/108x108_1.gif";
            model.Preview_ImageAbsoluteUri = _staticContent.AbsoluteUri(model.Preview_Image_URL);
            model.Photo_YN = false;

            model.TemplateModel.TemplateAreas = await GetAreasAsync(0);
            model.TemplateModel.TemplateItemResources = new List<TemplateItemResource>();

            return View("Index", model);
        }

        /// <summary>
        /// 상품 수정
        /// </summary>
        /// <param name="id">Product_ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = new ProductRegistViewModel();
            await InitModelAsync(model, false);

            var existsOrderQuery = from a in _barunsonDb.TB_Order_Products
                                   where a.Product_ID == id
                                   orderby a.Order_ID
                                   select a.Order_ID;
            var existsOrder = await existsOrderQuery.Take(1).ToListAsync();
            //주문이 없으면 삭제 가능
            model.IsDelete = ((existsOrder == null || existsOrder.Count == 0) && CurrentUserHasRole(AdminRole.Admin | AdminRole.Product));

            var prodQuery = from a in _barunsonDb.TB_Products
                        join b in _barunsonDb.TB_Templates on a.Template_ID equals b.Template_ID
                        join c in _barunsonDb.TB_Template_Details on b.Template_ID equals c.Template_ID
                        where a.Product_ID == id
                        select new
                        {
                            a.Product_ID,
                            b.Template_ID,
                            a.Product_Code,
                            a.Original_Product_Code,
                            a.Product_Brand_Code,
                            a.Product_Category_Code,
                            b.Photo_YN,
                            a.Product_Name,
                            a.Product_Description,
                            a.Price,
                            a.Display_YN,
                            a.Main_Image_URL,
                            a.Preview_Image_URL,
                            a.SetCard_Display_YN,
                            a.SetCard_URL,
                            a.SetCard_Mobile_URL,
                            Template = b,
                            TemplateDetail = c
                        };
            var prodItem = await prodQuery.FirstAsync();

            model.Product_ID = prodItem.Product_ID;
            model.Template_ID = prodItem.Template_ID;
            model.Product_Code = prodItem.Product_Code;
            model.Original_Product_Code = prodItem.Original_Product_Code;
            model.ProductBarnd = prodItem.Product_Brand_Code;
            model.ProductCategoryCode = prodItem.Product_Category_Code;
            model.Photo_YN = prodItem.Photo_YN == "Y";
            model.Product_Name = prodItem.Product_Name;
            model.Product_Description = prodItem.Product_Description;
            model.Price = prodItem.Price;
            model.Display_YN = prodItem.Display_YN;
            model.Main_Image_URL = string.IsNullOrEmpty(prodItem.Main_Image_URL) ? "/img/108x108_1.gif" : prodItem.Main_Image_URL;
            model.Main_ImageAbsoluteUri = _staticContent.AbsoluteUri(model.Main_Image_URL);
            model.Preview_Image_URL = string.IsNullOrEmpty(prodItem.Preview_Image_URL) ? "/img/108x108_1.gif" : prodItem.Preview_Image_URL;
            model.Preview_ImageAbsoluteUri = _staticContent.AbsoluteUri(model.Preview_Image_URL);
            model.SetCard_Display_YN = prodItem.SetCard_Display_YN == "Y";
            model.SetCard_URL = prodItem.SetCard_URL;
            model.SetCard_Mobile_URL = prodItem.SetCard_Mobile_URL;

            model.TemplateModel.Template = prodItem.Template;
            model.TemplateModel.Template_Name = prodItem.Template.Template_Name;
            model.TemplateModel.Attached_File1_absoluteUri = !string.IsNullOrEmpty(prodItem.Template.Attached_File1_URL) ? _staticContent.AbsoluteUri(prodItem.Template.Attached_File1_URL)?.ToString() : null;
            model.TemplateModel.Attached_File2_absoluteUri = !string.IsNullOrEmpty(prodItem.Template.Attached_File2_URL) ? _staticContent.AbsoluteUri(prodItem.Template.Attached_File2_URL)?.ToString() : null;
            model.TemplateModel.TemplateDetail = prodItem.TemplateDetail;
            if (string.IsNullOrEmpty(prodItem.TemplateDetail.RepeatData))
                model.TemplateModel.TemplateRepeats = new Dictionary<string, Dictionary<string, string>>();
            else
            {
                var jsonVal = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(prodItem.TemplateDetail.RepeatData, new JsonSerializerOptions { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) });
                model.TemplateModel.TemplateRepeats = jsonVal;
            }

            //Product Category
            var cateQuery = from a in _barunsonDb.TB_Product_Categories
                            join b in _barunsonDb.TB_Categories on a.Category_ID equals b.Category_ID
                            where a.Product_ID == id
                            orderby b.Sort
                            select new { a.Category_ID, b.Category_Type_Code };
            var cateItems = await cateQuery.ToListAsync();

            //Product Category
            model.SelectedProductCategories = cateItems.Where(m => m.Category_Type_Code == "CTC02").Select(m => m.Category_ID).ToList();
            //Main Category
            model.Main_Categories.ForEach(m =>
            {
                m.Selected = cateItems.Exists(x => x.Category_ID == int.Parse(m.Value) && x.Category_Type_Code == "CTC01");
            });

            //ICon
            var prodIconQuery = from a in _barunsonDb.TB_Product_Icons
                                join b in _barunsonDb.TB_Icons on a.Icon_ID equals b.Icon_ID
                                where a.Product_ID == id
                                select a.Icon_ID;
            var prodIconItems = await prodIconQuery.ToListAsync();
            model.Main_Icons.ForEach(m =>
            {
                m.Selected = prodIconItems.Exists(x => x == int.Parse(m.Value));
            });

            //Prodcut Image
            var prodImageQuery = from a in _barunsonDb.TB_Product_Images
                                 where a.Product_ID == id
                                 select new ProductRegistViewModel.ProductImage
                                 {
                                     Image_ID = a.Image_ID,
                                     Image_URL = a.Image_URL,
                                     Image_Type_Code = a.Image_Type_Code.TrimStart('/')
                                 };
            model.ProductImages = await prodImageQuery.ToListAsync();
            model.ProductImages.ForEach(m =>
            {
                m.ImageAbsoluteUri = _staticContent.AbsoluteUri(m.Image_URL);
            });

            //Template Area
            model.TemplateModel.TemplateAreas = await GetAreasAsync(model.Template_ID);

            //Template Item
            var objectQuery = from a in _barunsonDb.TB_Template_Items
                              join b in _barunsonDb.TB_Item_Resources on a.Resource_ID equals b.Resource_ID
                              where a.Template_ID == model.Template_ID
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
            model.TemplateModel.TemplateItemResources = await objectQuery.ToListAsync();
            model.TemplateModel.TemplateItemResources.ForEach(m =>
            {
                m.type = ItemTypeCodeToResourceTypeCode(m.type);
                if (!string.IsNullOrEmpty(m.resource_url))
                {
                    m.resource_absoluteurl = _staticContent.AbsoluteUri(m.resource_url).AbsoluteUri;
                }
            });
                        
            return View("Index", model);
        }
        private bool VaildProdRegModel(ProductRegistViewModel model)
        {
            if (string.IsNullOrEmpty(model.TemplateModel.TemplateDetail.Groom_Name))
                ModelState.AddModelError("TemplateModel.TemplateDetail.Groom_Name", "템플릿: 신랑명을 입력하세요.");
            if (string.IsNullOrEmpty(model.TemplateModel.TemplateDetail.Bride_Name))
                ModelState.AddModelError("TemplateModel.TemplateDetail.Bride_Name", "템플릿: 신부명을 입력하세요.");
            if (string.IsNullOrEmpty(model.TemplateModel.TemplateDetail.Weddinghall_Name))
                ModelState.AddModelError("TemplateModel.TemplateDetail.Weddinghall_Name", "템플릿: 예식장명을 입력하세요.");

            return ModelState.IsValid;
        }

        [RequestFormLimits(ValueCountLimit = 4096)]
        [HttpPost]
        public async Task<IActionResult> Save(ProductRegistViewModel model)
        {
            bool isNew = true;
            // Product Image 저장.
            // Image_ID == 0 은 신규 등록. Temp 폴더에 저장된 이미지를 서비스폴더로 복사 및 URL 변경 필요. temp -> Product_ID
            // 기존 Image와 변경 사항 검사하여, 수정, 삭제 수행.

            if (VaildProdRegModel(model))
            {
                var now = DateTime.Now;
                var ipAddr = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                var currUser = CurrentUserId();
                var deleteImageFiles = new List<string>();

                using var trans = await _barunsonDb.Database.BeginTransactionAsync();

                TB_Product product;
                
                if (model.Product_ID > 0) 
                {
                    isNew = false;
                    //수정

                    #region Product

                    product = await (from a in _barunsonDb.TB_Products where a.Product_ID == model.Product_ID select a).FirstAsync();
                    product.Product_Category_Code = model.ProductCategoryCode;
                    product.Product_Code = model.Product_Code;
                    product.Product_Brand_Code = model.ProductBarnd;
                    
                    product.Product_Name = model.Product_Name;
                    product.Product_Description = model.Product_Description;
                    product.SetCard_Display_YN = model.SetCard_Display_YN ? "Y" : "N";
                    product.SetCard_URL = model.SetCard_URL;
                    product.SetCard_Mobile_URL = model.SetCard_Mobile_URL;
                    product.Price = model.Price.Value;
                    product.Display_YN = model.Display_YN;

                    if (!string.IsNullOrEmpty(product.Main_Image_URL) && product.Main_Image_URL != model.Main_Image_URL)
                        deleteImageFiles.Add(product.Main_Image_URL);
                    product.Main_Image_URL = await FileMoveToProductAsync(model.TemporaryId, model.Main_Image_URL, product.Original_Product_Code);
                    //product.Preview_Image_URL = await FileMoveToProductAsync(model.TemporaryId, model.Preview_Image_URL, product.Original_Product_Code); 
                    product.Update_User_ID = currUser;
                    product.Update_DateTime = now;
                    product.Update_IP = ipAddr;

                    #endregion

                    #region  Main Category & Product Categories

                    var allCategories = new List<int>();
                    allCategories.AddRange(model.Main_Categories.Where(m => m.Selected).Select(m => int.Parse(m.Value)));
                    allCategories.AddRange(model.SelectedProductCategories);

                    var productCategories = await (from a in _barunsonDb.TB_Product_Categories where a.Product_ID == model.Product_ID select a).ToListAsync();

                    //삭제 항목
                    var removeProductCategories = productCategories.Where(m => allCategories.Contains(m.Category_ID) == false );
                    _barunsonDb.TB_Product_Categories.RemoveRange(removeProductCategories);

                    int sort = 1;
                    foreach(var cateId in allCategories.OrderBy(m => m))
                    {
                        var cateItem = productCategories.FirstOrDefault(m => m.Category_ID == cateId);
                        if (cateItem != null)
                            cateItem.Sort = sort;
                        else
                        {
                            //신규
                            cateItem = new TB_Product_Category
                            {
                                Category_ID = cateId,
                                Product_ID = product.Product_ID,
                                Sort = sort,
                                Regist_User_ID = currUser,
                                Regist_DateTime = now,
                                Regist_IP = ipAddr,
                                Update_User_ID = currUser,
                                Update_DateTime = now,
                                Update_IP = ipAddr,
                            };
                            _barunsonDb.TB_Product_Categories.Add(cateItem);
                        }
                        sort++;
                    }
                    #endregion

                    #region Product Icon

                    var selectedIcons = model.Main_Icons.Where(m => m.Selected).Select(m => int.Parse(m.Value));
                    var productIcons = await (from a in _barunsonDb.TB_Product_Icons where a.Product_ID == model.Product_ID select a).ToListAsync();
                    //삭제 항목
                    var removeProductIcons = productIcons.Where(m => selectedIcons.Contains(m.Icon_ID) == false);
                    _barunsonDb.TB_Product_Icons.RemoveRange(removeProductIcons);

                    //추가
                    foreach(var newId in selectedIcons.Where(m => !productIcons.Any(x => x.Icon_ID == m)))
                    {
                        _barunsonDb.TB_Product_Icons.Add(new TB_Product_Icon
                        {
                            Product_ID = product.Product_ID,
                            Icon_ID = newId,
                            Regist_User_ID = currUser,
                            Regist_DateTime = now,
                            Regist_IP = ipAddr,
                            Update_User_ID = currUser,
                            Update_DateTime = now,
                            Update_IP = ipAddr,
                        });
                    }

                    #endregion

                    #region Product Image

                    var productIamges = await (from a in _barunsonDb.TB_Product_Images where a.Product_ID == model.Product_ID select a).ToListAsync();
                    //삭제 항목
                    var removeProductIamges = productIamges.Where(m => !model.ProductImages.Any(x => x.Image_ID == m.Image_ID));
                    _barunsonDb.TB_Product_Images.RemoveRange(removeProductIamges);
                    deleteImageFiles.AddRange(removeProductIamges.Where(m => !string.IsNullOrEmpty(m.Image_URL)).Select(m => m.Image_URL));

                    //추가
                    foreach (var newItem in model.ProductImages.Where(m => m.Image_ID == 0))
                    {
                        // Image_ID == 0 은 신규 등록. Temp 폴더에 저장된 이미지를 서비스폴더로 복사 및 URL 변경 필요. temp -> Product_ID
                        _barunsonDb.TB_Product_Images.Add(new TB_Product_Image
                        {
                            Product_ID = product.Product_ID,
                            Image_URL = await FileMoveToProductAsync(model.TemporaryId, newItem.Image_URL, model.Original_Product_Code),
                            Image_Type_Code = newItem.Image_Type_Code,
                            Regist_User_ID = currUser,
                            Regist_DateTime = now,
                            Regist_IP = ipAddr,
                            Update_User_ID = currUser,
                            Update_DateTime = now,
                            Update_IP = ipAddr,
                        });
                    }

                    #endregion

                    #region Template

                    var template = await (from a in _barunsonDb.TB_Templates where a.Template_ID == model.Template_ID select a).FirstAsync();

                    template.Template_Name = model.TemplateModel.Template_Name;
                    template.Preview_URL = await FileMoveToProductAsync(model.TemporaryId, model.TemplateModel.Preview_URL, model.Original_Product_Code);
                    template.Background_Color = model.TemplateModel.Background_Color;

                    if (!string.IsNullOrEmpty(template.Attached_File1_URL) && template.Attached_File1_URL != model.TemplateModel.Template.Attached_File1_URL)
                        deleteImageFiles.Add(template.Attached_File1_URL);
                    if (!string.IsNullOrEmpty(template.Attached_File2_URL) && template.Attached_File2_URL != model.TemplateModel.Template.Attached_File2_URL)
                        deleteImageFiles.Add(template.Attached_File2_URL);

                    template.Attached_File1_URL = await FileMoveToProductAsync(model.TemporaryId, model.TemplateModel.Template.Attached_File1_URL, model.Original_Product_Code);
                    template.Attached_File2_URL = await FileMoveToProductAsync(model.TemporaryId, model.TemplateModel.Template.Attached_File2_URL, model.Original_Product_Code);
                    template.Update_User_ID = currUser;
                    template.Update_DateTime = now;
                    template.Update_IP = ipAddr;

                    #endregion

                    #region Template Detail

                    var templateDetail = await (from a in _barunsonDb.TB_Template_Details where a.Template_ID == model.Template_ID select a).FirstAsync();
                    //등록정보는 기존 정보로 변경되지 않도록.
                    model.TemplateModel.TemplateDetail.Regist_DateTime = templateDetail.Regist_DateTime;
                    model.TemplateModel.TemplateDetail.Regist_User_ID = templateDetail.Regist_User_ID;
                    model.TemplateModel.TemplateDetail.Regist_IP = templateDetail.Regist_IP;

                    model.TemplateModel.TemplateDetail.WeddingHHmm = model.TemplateModel.TemplateDetail.WeddingHour + model.TemplateModel.TemplateDetail.WeddingMin;

                    _barunsonDb.Entry(templateDetail).CurrentValues.SetValues(model.TemplateModel.TemplateDetail);

                    if (model.TemplateModel.TemplateRepeats != null)
                    {
                        var jsonVal = JsonSerializer.Serialize(model.TemplateModel.TemplateRepeats, new JsonSerializerOptions { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) });
                        templateDetail.RepeatData = jsonVal;
                    }

                    templateDetail.Update_User_ID = currUser;
                    templateDetail.Update_DateTime = now;
                    templateDetail.Update_IP = ipAddr;
                    #endregion

                    #region Template Area
                    var templateAreas = await (from a in _barunsonDb.TB_Template_Areas where a.Template_ID == model.Template_ID select a).ToListAsync();
                    foreach(var item in model.TemplateModel.TemplateAreas)
                    {
                        var area = templateAreas.FirstOrDefault(m => m.Area_ID == item.Area_ID);
                        if (area != null)
                        {
                            area.Area_ID = item.Area_ID;
                            area.Size_Height = item.Size_Height;
                            area.Size_Width = item.Size_Width;
                            area.Color = item.Color;
                            area.Sort = item.Area_ID;

                            area.Update_User_ID = currUser;
                            area.Update_DateTime = now;
                            area.Update_IP = ipAddr;
                        }
                        else
                        {
                            area = new TB_Template_Area();
                            area.Template_ID = model.Template_ID;
                            area.Area_ID = item.Area_ID;
                            area.Size_Height = item.Size_Height;
                            area.Size_Width = item.Size_Width;
                            area.Color = item.Color;
                            area.Sort = item.Area_ID;

                            area.Regist_User_ID = currUser;
                            area.Regist_DateTime = now;
                            area.Regist_IP = ipAddr;
                            area.Update_User_ID = currUser;
                            area.Update_DateTime = now;
                            area.Update_IP = ipAddr;

                            _barunsonDb.TB_Template_Areas.Add(area);
                        }
                    }
                    #endregion

                    #region Template Item Resource

                    var templateItems = await (from a in _barunsonDb.TB_Template_Items where a.Template_ID == model.Template_ID select a).ToListAsync();
                    var resIds = templateItems.Select(m => m.Resource_ID).ToList();
                    var itemResources = await (from a in _barunsonDb.TB_Item_Resources where resIds.Contains(a.Resource_ID) select a).ToListAsync();
                    //삭제 항목
                    var resource_ids = model.TemplateModel.TemplateItemResources.Where(m => m.resource_id.HasValue).Select(m => m.resource_id);
                    var removeitemResources = itemResources.Where(m => !resource_ids.Contains(m.Resource_ID));
                    var removeTempleateItems = templateItems.Where(m => !resource_ids.Contains(m.Resource_ID));

                    if (removeitemResources.Count() > 0)
                    {
                        _barunsonDb.TB_Template_Items.RemoveRange(removeTempleateItems);
                        _barunsonDb.TB_Item_Resources.RemoveRange(removeitemResources);

                        deleteImageFiles.AddRange(removeitemResources.Where(m => !string.IsNullOrEmpty(m.Resource_URL)).Select(m => m.Resource_URL));

                    }

                    var orderTemplateItemResources = model.TemplateModel.TemplateItemResources
                        .Where(m => !string.IsNullOrEmpty(m.pid))
                        .OrderBy(m => int.Parse(m.pid.Replace("area","")))
                        .ThenBy(m => m.zindex);

                    int itemindex = 1;
                    foreach (var item in orderTemplateItemResources)
                    {
                        if (string.IsNullOrEmpty(item.pid))
                            continue;

                        TB_Template_Item template_item;
                        TB_Item_Resource item_resource;
                        if (!item.resource_id.HasValue)
                        {
                            template_item = new TB_Template_Item();
                            item_resource = new TB_Item_Resource();

                            template_item.Template_ID = model.Template_ID;
                            template_item.Regist_User_ID = currUser;
                            template_item.Regist_DateTime = now;
                            template_item.Regist_IP = ipAddr;
                            template_item.Resource = item_resource;

                            item_resource.Regist_User_ID = currUser;
                            item_resource.Regist_DateTime = now;
                            item_resource.Regist_IP = ipAddr;

                            _barunsonDb.TB_Item_Resources.Add(item_resource);
                            _barunsonDb.TB_Template_Items.Add(template_item);
                        }
                        else
                        {
                            template_item = templateItems.Single(m => m.Resource_ID == item.resource_id);
                            item_resource = itemResources.Single(m => m.Resource_ID == item.resource_id);
                        }
                        template_item.Area_ID = Int32.Parse(item.pid.Replace("area", ""));
                        template_item.Item_Type_Code = ResourceTypeCodeToItemTypeCode(item.type);
                        template_item.Location_Top = item.top;
                        template_item.Location_Left = item.left;
                        template_item.Size_Height = item.height;
                        template_item.Size_Width = item.width;
                        template_item.Update_User_ID = currUser;
                        template_item.Update_DateTime = now;
                        template_item.Update_IP = ipAddr;

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
                        item_resource.Sort = itemindex;
                        item_resource.Font = item.font;

                        if (!string.IsNullOrEmpty(item_resource.Resource_URL) && item_resource.Resource_URL != item.resource_url)
                            deleteImageFiles.Add(item_resource.Resource_URL);

                        item_resource.Resource_URL = await FileMoveToProductAsync(model.TemporaryId, item.resource_url, model.Original_Product_Code);
                        item_resource.Resource_Height = item.org_height;
                        item_resource.Resource_Width = item.org_width;
                        item_resource.Resource_Type_Code = item.type;
                        item_resource.Update_User_ID = currUser;
                        item_resource.Update_DateTime = now;
                        item_resource.Update_IP = ipAddr;

                        itemindex++;
                    }

                    #endregion
                }
                else
                {
                    //신규 등록

                    #region Product
                    if (string.IsNullOrEmpty(model.Original_Product_Code))
                        model.Original_Product_Code = await GetNewProductCodeAsync(model.ProductBarnd);
                    if (string.IsNullOrEmpty(model.Product_Code))
                        model.Product_Code = model.Original_Product_Code;

                    product = new TB_Product();
                    product.Original_Product_Code = model.Original_Product_Code;
                    product.Regist_User_ID = currUser;
                    product.Regist_DateTime = now;
                    product.Regist_IP = ipAddr;

                    product.Product_Category_Code = model.ProductCategoryCode;
                    product.Product_Code = model.Product_Code;
                    product.Product_Brand_Code = model.ProductBarnd;
                    product.Product_Name = model.Product_Name;
                    product.Product_Description = model.Product_Description;
                    product.Price = model.Price.Value;
                    product.Display_YN = model.Display_YN;
                    product.Main_Image_URL = await FileMoveToProductAsync(model.TemporaryId, model.Main_Image_URL, product.Original_Product_Code);
                    product.Preview_Image_URL = await FileMoveToProductAsync(model.TemporaryId, model.Preview_Image_URL, product.Original_Product_Code);
                    product.Update_User_ID = currUser;
                    product.Update_DateTime = now;
                    product.Update_IP = ipAddr;

                    //저장하여 Product ID 생성
                    _barunsonDb.TB_Products.Add(product);
                    await _barunsonDb.SaveChangesAsync();
                    model.Product_ID = product.Product_ID;

                    #endregion

                    #region Product Categories

                    var allCategories = new List<int>();
                    allCategories.AddRange(model.Main_Categories.Where(m => m.Selected).Select(m => int.Parse(m.Value)));
                    allCategories.AddRange(model.SelectedProductCategories);

                    int sort = 1;
                    foreach (var cateId in allCategories.OrderBy(m => m))
                    {
                        var cateItem = new TB_Product_Category
                        {
                            Category_ID = cateId,
                            Product_ID = product.Product_ID,
                            Sort = sort,
                            Regist_User_ID = currUser,
                            Regist_DateTime = now,
                            Regist_IP = ipAddr,
                            Update_User_ID = currUser,
                            Update_DateTime = now,
                            Update_IP = ipAddr,
                        };
                        _barunsonDb.TB_Product_Categories.Add(cateItem);
                    }
                    #endregion

                    #region Product Icon

                    var selectedIcons = model.Main_Icons.Where(m => m.Selected).Select(m => int.Parse(m.Value));
                    foreach (var newId in selectedIcons)
                    {
                        _barunsonDb.TB_Product_Icons.Add(new TB_Product_Icon
                        {
                            Product_ID = product.Product_ID,
                            Icon_ID = newId,
                            Regist_User_ID = currUser,
                            Regist_DateTime = now,
                            Regist_IP = ipAddr,
                            Update_User_ID = currUser,
                            Update_DateTime = now,
                            Update_IP = ipAddr,
                        });
                    }

                    #endregion

                    #region Product Image

                    foreach (var newItem in model.ProductImages.Where(m => m.Image_ID == 0))
                    {
                        // Image_ID == 0 은 신규 등록. Temp 폴더에 저장된 이미지를 서비스폴더로 복사 및 URL 변경 필요. temp -> Product_ID
                        _barunsonDb.TB_Product_Images.Add(new TB_Product_Image
                        {
                            Product_ID = product.Product_ID,
                            Image_URL = await FileMoveToProductAsync(model.TemporaryId, newItem.Image_URL, model.Original_Product_Code),
                            Image_Type_Code = newItem.Image_Type_Code,
                            Regist_User_ID = currUser,
                            Regist_DateTime = now,
                            Regist_IP = ipAddr,
                            Update_User_ID = currUser,
                            Update_DateTime = now,
                            Update_IP = ipAddr,
                        });
                    }

                    #endregion

                    #region Template
                    var template = new TB_Template();

                    template.Template_Name = model.TemplateModel.Template_Name;
                    template.Photo_YN = model.Photo_YN ? "Y" : "N";
                    template.Preview_URL = await FileMoveToProductAsync(model.TemporaryId, model.TemplateModel.Preview_URL, model.Original_Product_Code);
                    template.Background_Color = model.TemplateModel.Background_Color;
                    template.Attached_File1_URL = await FileMoveToProductAsync(model.TemporaryId, model.TemplateModel.Template.Attached_File1_URL, model.Original_Product_Code);
                    template.Attached_File2_URL = await FileMoveToProductAsync(model.TemporaryId, model.TemplateModel.Template.Attached_File2_URL, model.Original_Product_Code);
                    template.Regist_User_ID = currUser;
                    template.Regist_DateTime = now;
                    template.Regist_IP = ipAddr;
                    template.Update_User_ID = currUser;
                    template.Update_DateTime = now;
                    template.Update_IP = ipAddr;

                    //저장하여 Template ID 생성
                    _barunsonDb.TB_Templates.Add(template);
                    await _barunsonDb.SaveChangesAsync();

                    model.Template_ID = template.Template_ID;
                    // Product에 Template_ID Update
                    product.Template_ID = template.Template_ID;

                    #endregion

                    #region Template Detail
                    var templateDetail = model.TemplateModel.TemplateDetail;

                    templateDetail.Template_ID = model.Template_ID;
                    
                    templateDetail.Regist_User_ID = currUser;
                    templateDetail.Regist_DateTime = now;
                    templateDetail.Regist_IP = ipAddr;
                    templateDetail.Update_User_ID = currUser;
                    templateDetail.Update_DateTime = now;
                    templateDetail.Update_IP = ipAddr;

                    if (model.TemplateModel.TemplateRepeats != null)
                    {
                        var jsonVal = JsonSerializer.Serialize(model.TemplateModel.TemplateRepeats, new JsonSerializerOptions { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) });
                        templateDetail.RepeatData = jsonVal;
                    }
                    _barunsonDb.TB_Template_Details.Add(templateDetail);

                    #endregion

                    #region Template Area
                    foreach (var item in model.TemplateModel.TemplateAreas)
                    {
                        var area = new TB_Template_Area();
                        area.Template_ID = model.Template_ID;
                        area.Area_ID = item.Area_ID;
                        area.Size_Height = item.Size_Height;
                        area.Size_Width = item.Size_Width;
                        area.Color = item.Color;
                        area.Sort = item.Area_ID;

                        area.Regist_User_ID = currUser;
                        area.Regist_DateTime = now;
                        area.Regist_IP = ipAddr;
                        area.Update_User_ID = currUser;
                        area.Update_DateTime = now;
                        area.Update_IP = ipAddr;

                        _barunsonDb.TB_Template_Areas.Add(area);
                    }
                    #endregion

                    #region Template Item Resource

                    foreach (var item in model.TemplateModel.TemplateItemResources)
                    {
                        var template_item = new TB_Template_Item();
                        var item_resource = new TB_Item_Resource();

                        template_item.Template_ID = model.Template_ID;
                        template_item.Regist_User_ID = currUser;
                        template_item.Regist_DateTime = now;
                        template_item.Regist_IP = ipAddr;
                        template_item.Area_ID = Int32.Parse(item.pid.Replace("area", ""));
                        template_item.Item_Type_Code = ResourceTypeCodeToItemTypeCode(item.type);
                        template_item.Location_Top = item.top;
                        template_item.Location_Left = item.left;
                        template_item.Size_Height = item.height;
                        template_item.Size_Width = item.width;
                        template_item.Update_User_ID = currUser;
                        template_item.Update_DateTime = now;
                        template_item.Update_IP = ipAddr;

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
                        item_resource.Resource_URL = await FileMoveToProductAsync(model.TemporaryId, item.resource_url, model.Original_Product_Code);
                        item_resource.Resource_Height = item.org_height;
                        item_resource.Resource_Width = item.org_width;
                        item_resource.Resource_Type_Code = item.type;
                        item_resource.Update_User_ID = currUser;
                        item_resource.Update_DateTime = now;
                        item_resource.Update_IP = ipAddr;
                        item_resource.Regist_User_ID = currUser;
                        item_resource.Regist_DateTime = now;
                        item_resource.Regist_IP = ipAddr;

                        template_item.Resource = item_resource;

                        _barunsonDb.TB_Item_Resources.Add(item_resource);
                        _barunsonDb.TB_Template_Items.Add(template_item);
                    }

                    #endregion
                }

                await _barunsonDb.SaveChangesAsync();

                await trans.CommitAsync();

                //이미지 파일 삭제
                foreach (var delFile in deleteImageFiles)
                    await _staticContent.DeleteFileAsync(delFile);

                return RedirectToAction("Index", "ProductList");
            }
            else
            {
                //모델 오류시 모델정보 채우기 및 화면 렌더링
                await InitModelAsync(model, isNew);
                if (model.isNew)
                {
                    model.Product_ID = 0;
                    model.Template_ID = 0;
                }

                //Area 데이터, Post로 전달 된 데이터는 데이터값이 있는 경우만 전송됨으로
                //전체 Area정보에 Post로 된 데이터를 적용하여 화면에 출력 해야 함.
                var postAreas = model.TemplateModel.TemplateAreas;
                var areas = await GetAreasAsync(model.Template_ID);
                foreach (var ta in postAreas)
                {
                    var area = areas.FirstOrDefault(m => m.Area_ID == ta.Area_ID);
                    if (area != null)
                    {
                        area.Size_Height = ta.Size_Height;
                        area.Size_Width = ta.Size_Width;
                        area.Color = ta.Color;
                        area.Sort = ta.Sort;
                    }
                }
                model.TemplateModel.TemplateAreas = areas;
                return View("Index", model);
            }
        }

        /// <summary>
        /// 이미지 업로드
        /// </summary>
        /// <param name="files"></param>
        /// <param name="TemporaryId"></param>
        /// <param name="TypeCode"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadImage(List<IFormFile> files, string TemporaryId, string TypeCode, string RootPath, string LeafPath)
        {
            try
            {
                var result = new List<ProductRegistViewModel.ProductImage>();
                if (LeafPath == null)
                    LeafPath = "";

                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        
                        var ext = Path.GetExtension(formFile.FileName).ToLower();
                        var filename = Guid.NewGuid().ToString() + ext;
                        var rPath = Path.Combine(_barunnConfig.FileConfig.UploadContainer, RootPath, TemporaryId, LeafPath, filename);
                        var (status, message) = await _staticContent.UploadFileAsync(formFile, rPath);

                        var absoluteUri = _staticContent.AbsoluteUri(message);

                        result.Add(new ProductRegistViewModel.ProductImage
                        {
                            Image_ID = 0,
                            Image_Type_Code = TypeCode,
                            Image_URL = message,
                            ImageAbsoluteUri = absoluteUri,
                            UpdateDateTime = DateTime.Now
                        });
                    }
                }
                return Json(new { status = true, typeCode = TypeCode, files = result, message = "" });
            }
            catch (Exception e)
            {
                return Json(new { status = false, typeCode = TypeCode, message = e.Message });
            }

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

                if ((TypeCode == "photo" || TypeCode == "profile" ) && !string.IsNullOrEmpty(imageData))
                {
                    filename = Guid.NewGuid().ToString() + ".png";
                    var rPath = Path.Combine(_barunnConfig.FileConfig.UploadContainer, "template", TemporaryId, filename);
                    (status, message) = await _staticContent.UploadFileAsync(imageData, rPath);
                }
                else if (TypeCode == "mainimage" && !string.IsNullOrEmpty(imageData))
                {
                    filename = Guid.NewGuid().ToString() + ".png";
                    var rPath = Path.Combine(_barunnConfig.FileConfig.UploadContainer, "product", TemporaryId, filename);
                    (status, message) = await _staticContent.UploadFileAsync(imageData, rPath);
                }
                else if (TypeCode == "css" && files != null && files.Count > 0 && files[0].Length > 0)
                {
                    filename = Guid.NewGuid().ToString() + ".css";
                    var rPath = Path.Combine(_barunnConfig.FileConfig.UploadContainer, "template", TemporaryId, filename);
                    (status, message) = await _staticContent.UploadFileAsync(files[0], rPath);
                }
                else if (TypeCode == "js" && files != null && files.Count > 0 && files[0].Length > 0)
                {
                    filename = Guid.NewGuid().ToString() + ".js";
                    var rPath = Path.Combine(_barunnConfig.FileConfig.UploadContainer, "template", TemporaryId, filename);
                    (status, message) = await _staticContent.UploadFileAsync(files[0], rPath);
                }
                else if (files != null && files.Count > 0 && files[0].Length > 0)
                {
                    var ext = Path.GetExtension(files[0].FileName).ToLower();
                    filename = Guid.NewGuid().ToString() + ext;
                    var rPath = Path.Combine(_barunnConfig.FileConfig.UploadContainer, "template", TemporaryId, filename);
                    (status, message) = await _staticContent.UploadFileAsync(files[0], rPath);
                }

                if (status)
                {
                    if (TypeCode == "templateImage" || TypeCode == "photo" || TypeCode == "profile")
                    {

                        try
                        {
                            using (var image = Image.FromFile(_staticContent.GetFileFullName(message)))
                            {
                                width = image.Width;
                                height = image.Height;
                            }
                        }
                        catch (Exception e)
                        {
                            width = 100;
                            height = 100;
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

        public async Task<IActionResult> GetTemplateList(int Template_ID, string Product_Code, string Product_Category_Code)
        {
            var originalProdCode = await (from a in _barunsonDb.TB_Products where a.Template_ID == Template_ID select a.Original_Product_Code).FirstOrDefaultAsync();
            //Template Item
            var objectQuery = from a in _barunsonDb.TB_Template_Items
                              join b in _barunsonDb.TB_Item_Resources on a.Resource_ID equals b.Resource_ID
                              where a.Template_ID == Template_ID
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
            var itemResources = await objectQuery.ToListAsync();
            foreach(var item in itemResources)
            {
                item.type = ItemTypeCodeToResourceTypeCode(item.type);
                if (!string.IsNullOrEmpty(item.resource_url))
                {
                    item.resource_url = await FileMoveToProductAsync(originalProdCode, item.resource_url, "temp");
                    item.resource_absoluteurl = _staticContent.AbsoluteUri(item.resource_url).AbsoluteUri;
                }
            }

            var TemplateAreas = await GetAreasAsync(Template_ID);

            var jsonOption = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = null 
            };
            return Json(new { area = TemplateAreas, template = itemResources }, jsonOption);

        }
        [HttpPost]
        public async Task<IActionResult> GetNewProductCode(string productBarnd)
        {
            var result = await GetNewProductCodeAsync(productBarnd);
            return Json(new { code = result });
        }

        /// <summary>
        /// 주문이 없는 상품 삭제
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AdminRoleActionFilter(AdminRole.Admin | AdminRole.Product)]
        public async Task<IActionResult> Delete(int id)
        {

            var existsOrderQuery = from a in _barunsonDb.TB_Order_Products
                                   where a.Product_ID == id
                                   select a.Order_ID;
            var existsOrder = await existsOrderQuery.Take(1).ToListAsync();
            //주문이 없으면 삭제 가능
            if (existsOrder == null || existsOrder.Count == 0)
            {
                var deleteImageFiles = new List<string>();

                using var trans = await _barunsonDb.Database.BeginTransactionAsync();

                var product = await (from a in _barunsonDb.TB_Products where a.Product_ID == id select a).SingleAsync();
                if (!string.IsNullOrEmpty(product.Main_Image_URL))
                    deleteImageFiles.Add(product.Main_Image_URL);

                var productCategories = await (from a in _barunsonDb.TB_Product_Categories where a.Product_ID == id select a).ToListAsync();
                var productIcons = await (from a in _barunsonDb.TB_Product_Icons where a.Product_ID == id select a).ToListAsync();
                var productIamges = await (from a in _barunsonDb.TB_Product_Images where a.Product_ID == id select a).ToListAsync();
                productIamges.ForEach(m =>
                {
                    deleteImageFiles.Add(m.Image_URL);
                });
                var template = await (from a in _barunsonDb.TB_Templates where a.Template_ID == product.Template_ID select a).FirstAsync();
                if (!string.IsNullOrEmpty(template.Attached_File1_URL))
                    deleteImageFiles.Add(template.Attached_File1_URL);
                if (!string.IsNullOrEmpty(template.Attached_File2_URL))
                    deleteImageFiles.Add(template.Attached_File2_URL);
                var templateDetail = await (from a in _barunsonDb.TB_Template_Details where a.Template_ID == product.Template_ID select a).FirstAsync();
                var templateAreas = await (from a in _barunsonDb.TB_Template_Areas where a.Template_ID == product.Template_ID select a).ToListAsync();
                var templateItems = await (from a in _barunsonDb.TB_Template_Items where a.Template_ID == product.Template_ID select a).ToListAsync();
                var resIds = templateItems.Select(m => m.Resource_ID).ToList();
                var itemResources = await (from a in _barunsonDb.TB_Item_Resources where resIds.Contains(a.Resource_ID) select a).ToListAsync();
                deleteImageFiles.AddRange(itemResources.Where(m => !string.IsNullOrEmpty(m.Resource_URL)).Select(m => m.Resource_URL));


                _barunsonDb.TB_Template_Items.RemoveRange(templateItems);
                _barunsonDb.TB_Item_Resources.RemoveRange(itemResources);
                _barunsonDb.TB_Template_Areas.RemoveRange(templateAreas);
                _barunsonDb.TB_Template_Details.Remove(templateDetail);
                _barunsonDb.TB_Templates.Remove(template);
                _barunsonDb.TB_Product_Images.RemoveRange(productIamges);
                _barunsonDb.TB_Product_Icons.RemoveRange(productIcons);
                _barunsonDb.TB_Product_Categories.RemoveRange(productCategories);
                _barunsonDb.TB_Products.Remove(product);

                await _barunsonDb.SaveChangesAsync();

                await trans.CommitAsync();

                //이미지 파일 삭제
                foreach (var delFile in deleteImageFiles)
                    await _staticContent.DeleteFileAsync(delFile);
            }

            return RedirectToAction("Index", "ProductList");

        }

    }
}
