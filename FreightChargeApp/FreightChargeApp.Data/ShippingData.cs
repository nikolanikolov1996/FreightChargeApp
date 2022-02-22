using System;

namespace FreightChargeApp.Data
{
    public class ShippingData
    {
        public ShippingData(int courierId, DateTimeOffset timeStamp, float weight, float length, float width,
            float height, float shippingCost)
        {
            CourierId = courierId;
            TimeStamp = timeStamp;
            Weight = weight;
            Length = length;
            Width = width;
            Height = height;
            ShippingCost = shippingCost;
        }

        public long Id { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public int CourierId { get; set; }
        public Courier? Courier { get; set; }
        public float Weight { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float ShippingCost { get; set; }
    }
}
