namespace Application.RequestDTOs.MoMo
{
    public class MoMoIpnRequest
    {
        public string partnerCode { get; set; } = "";
        public string orderId { get; set; } = "";
        public string requestId { get; set; } = "";

        public string orderInfo { get; set; } = "";
        public string orderType { get; set; } = "momo_wallet";
        public long transId { get; set; } = 0;

        public string message { get; set; } = "";
        public string payType { get; set; } = "";
        public string signature { get; set; } = "";
        public string extraData { get; set; } = "";
        public int resultCode { get; set; } = 0;
        public long amount { get; set; } = 0;
        public long responseTime { get; set; } = 0;
        public string paymentOption { get; set; } = string.Empty;

        //public override string ToString()
        //{
        //    return $"{PartnerCode} - {OrderId} - {RequestId} - {OrderInfo} - {OrderType} - {TransId} - {Message} - {PayType} - {Signature} - {ExtraData} - {ResultCode} - {Amount} - {ResponseTime}";
        //}
    }
}
