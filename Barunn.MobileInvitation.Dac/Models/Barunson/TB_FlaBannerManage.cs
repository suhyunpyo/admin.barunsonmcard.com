using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barunn.MobileInvitation.Dac.Models.Barunson
{
    public partial class TB_FlaBannerManage
    {
        [Key]
        public int Id { get; set; }

        [StringLength(200)]
        public string GroupName { get; set; } = "";

        public bool Allowed { get; set; } = false;

        public string? Keywords { get; set; } = null;

        [Column(TypeName = "smalldatetime")]
        public DateTime RegistDateTime { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime UpdateDateTime { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string RegistUserId { get; set; } = "";
        [StringLength(50)]
        public string RegistUserName { get; set; } = "";
    }
}
