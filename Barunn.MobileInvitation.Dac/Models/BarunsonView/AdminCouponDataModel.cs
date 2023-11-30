using System;
using System.Collections.Generic;
using System.Text;

namespace Barunn.MobileInvitation.Dac.Models.BarunsonView
{
    public class AdminCoupon
    {
        public int Coupon_ID { get; set; }
        public string Coupon_Name { get; set; }
        public string Publish_Name { get; set; }
        public int? Discount_Price { get; set; }
        public double? Discount_Rate { get; set; }
        public string Discount_Flag { get; set; }
        public int Publsh_Count { get; set; }
        public int Use_Count { get; set; }
        public int Dis_Use_Count { get; set; }
        public int Retrieve_Count { get; set; }
        public DateTime? Regist_Datetime { get; set; }


        public AdminCoupon()
        { }

        public AdminCoupon(int coupon_ID, string coupon_Name, string publish_Name, int? discount_Price, double? discount_Rate, string discount_Flag, int publsh_Count, int use_Count, int dis_Use_Count, int retrieve_Count, DateTime? regist_Datetime)
        {
            Coupon_ID = coupon_ID;
            Coupon_Name = coupon_Name;
            Publish_Name = publish_Name;
            Discount_Price = discount_Price;
            Discount_Rate = discount_Rate;
            Discount_Flag = discount_Flag;
            Publsh_Count = publsh_Count;
            Use_Count = use_Count;
            Dis_Use_Count = dis_Use_Count;
            Retrieve_Count = retrieve_Count;
            Regist_Datetime = regist_Datetime;
        }

        public override bool Equals(object obj)
        {
            return obj is AdminCoupon other &&
                   Coupon_ID == other.Coupon_ID &&
                   Coupon_Name == other.Coupon_Name &&
                   Publish_Name == other.Publish_Name &&
                   Discount_Price == other.Discount_Price &&
                   Discount_Rate == other.Discount_Rate &&
                   Discount_Flag == other.Discount_Flag &&
                   Publsh_Count == other.Publsh_Count &&
                   Use_Count == other.Use_Count &&
                   Dis_Use_Count == other.Dis_Use_Count &&
                   Retrieve_Count == other.Retrieve_Count &&
                   Regist_Datetime == other.Regist_Datetime;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Coupon_ID);
            hash.Add(Coupon_Name);
            hash.Add(Publish_Name);
            hash.Add(Discount_Price);
            hash.Add(Discount_Rate);
            hash.Add(Discount_Flag);
            hash.Add(Publsh_Count);
            hash.Add(Use_Count);
            hash.Add(Dis_Use_Count);
            hash.Add(Retrieve_Count);
            hash.Add(Regist_Datetime);
            return hash.ToHashCode();
        }
    }
}
