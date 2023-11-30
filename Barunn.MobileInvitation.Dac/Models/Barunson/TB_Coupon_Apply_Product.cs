using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Barunn.MobileInvitation.Dac.Models.Barunson
{
    public partial class TB_Coupon_Apply_Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_Apply_ID { get; set; }
        public int? Coupon_ID { get; set; }
    }
}
