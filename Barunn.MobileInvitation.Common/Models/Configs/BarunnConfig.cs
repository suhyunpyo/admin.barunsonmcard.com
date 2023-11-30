using System;
using System.Collections.Generic;
using System.Text;

namespace Barunn.MobileInvitation.Common.Models.Configs
{
    public class BarunnConfig
    {
        public FileConfig FileConfig { get; set; }

        public NaverMapConfig Map { get; set; }

        public ImageSizeConfig MaxSize { get; set; }

        public LoopConfig LoopCnt { get; set; }

        /// <summary>
        /// ASE 암호화 키 - 비밀에 저장 
        /// </summary>
        public string AesKey { get; set; }

        public StaticContent StaticContent { get; set; }

        public StaticContent DefaultStaticContent { get; set; }

        public Uri UserSiteUrl { get; set; }
        public AuthCookie AuthCookie { get; set; }
    }

    public class FileConfig
    {
        public long FileSizeLimit { get; set; }
        public string UploadPath { get; set; }
        public string UploadContainer { get; set; }
    }
    public class NaverMapConfig
    {
        public string NaverCloudId { get; set; }
        public string NaverCloudKey { get; set; }
        public double DefaultMapLat { get; set; }
        public double DefaultMapLot { get; set; }
    }
    public class ImageSizeConfig
    {
        public int Thumnail { get; set; }
        public int Photo { get; set; }
        public int SNS { get; set; }
        public int Gallery { get; set; }
    }

    public class LoopConfig
    {
        public int SerialCoupon { get; set; }
    }
    /// <summary>
    /// 정적 콘텐츠 경로
    /// </summary>
    public  class StaticContent
    {
        public Uri BaseUrl { get; set; }
        public Uri ServiceUri { get; set; }
        public Uri CDNUri { get; set; }
    }
    public class AuthCookie
    {
        public string DefaultScheme { get; set; }
        public string Domain { get; set; }
    }

}
