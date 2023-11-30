using Barunn.MobileInvitation.Dac.Models.Barunson;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barunn.MobileInvitation.Web.Admin.Models
{
    public class TemplateViewModel
    {
        public string Template_Name { get; set; }
        public string Preview_URL { get; set; }
        public string Background_Color { get; set; }
        public string Photo_YN { get; set; }
        public string Attached_File1_absoluteUri { get; set; }
        public string Attached_File2_absoluteUri { get; set; }

        public TB_Template Template { get; set; }
        public TB_Template_Detail TemplateDetail { get; set; }

        public List<TamplateArea> TemplateAreas { get; set; }

        public List<TemplateItemResource> TemplateItemResources { get; set; }

        public string MappingFields { get; set; }
        
        public string MoneyGift_Remit_Title_URL { get; set; }
        public List<TemplateSample> TemplateSamples { get; set; }

        public Dictionary<string, Dictionary<string, string>> TemplateRepeats  { get; set; }
    }
    public class TemplateItemResource
    {
        /// <summary>
        /// Item ID
        /// </summary>
        public int? item_id { get; set; }
        /// <summary>
        /// Resource ID
        /// </summary>
        public int? resource_id { get; set; }
        /// <summary>
        /// Area id: "area" + a.Area_ID,
        /// </summary>
        public string pid { get; set; }
        /// <summary>
        /// Sort: "item_" + b.Sort
        /// </summary>
        public string id { get; set; }
        public string type { get; set; }
        public double? top { get; set; }
        public double? left { get; set; }
        public double? height { get; set; }
        public double? width { get; set; }
        public string chracterset { get; set; }
        public double? fontsize { get; set; }
        public string fontcolor { get; set; }
        public string bgcolor { get; set; }
        public bool bold_yn { get; set; }
        public bool italic_yn { get; set; }
        public bool underline_yn { get; set; }
        public double? between_text { get; set; }
        public double? between_line { get; set; }
        public string vertical_align { get; set; }
        public string horizontal_align { get; set; }
        public int? zindex { get; set; }
        public string font { get; set; }
        public string resource_url { get; set; }
        public string resource_absoluteurl { get; set; }
        public double? org_height { get; set; }
        public double? org_width { get; set; }
        
    }

    public class TamplateArea
    {
        public int Area_ID { get; set; }
        public string Area_Name { get; set; }
        public bool Edit_YN { get; set; }

        public double Size_Height { get; set; }
        public double Size_Width { get; set; }

        public string Color { get; set; }

        public int Sort { get; set; }
        public string Product_Category_Codes { get; set; }
    }

    public class TemplateSample
    {
        public int Template_ID { get; set; }
        public string Product_Code { get; set; }
        public string Product_Category_Code { get; set; }
        public string Main_Image_URL { get; set; }
        public bool Photo_YN { get; set; }
    }

}
