using Application.ResponseDTOs.MoMo;
using Application.Utilities;
using Application.Utilities.OptionsSetup;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace Application.RequestDTOs.MoMo
{
    public class CreateMoMoPaymentRequest
    {
        public string PartnerCode { get; set; } = string.Empty;
        public string RequestId { get; set; } = string.Empty;
        public long Amount { get; set; }
        public string OrderId { get; set; } = string.Empty;
        public string OrderInfo { get; set; } = string.Empty;
        public string RedirectUrl { get; set; } = string.Empty;
        public string IpnUrl { get; set; } = string.Empty;
        public string RequestType { get; set; } = "captureWallet";
        public string ExtraData { get; set; } = string.Empty;
        public string Lang { get; set; } = "vi";
        public string Signature { get; set; } = string.Empty;

        public CreateMoMoPaymentRequest()
        {

        }

        public CreateMoMoPaymentRequest(string partnerCode, string requestId, long amount, string orderId, string orderInfo, string redirectUrl, string ipnUrl, string requestType, string extraData, string lang)
        {
            this.PartnerCode = partnerCode;
            this.RequestId = requestId;
            this.Amount = amount;
            this.OrderId = orderId;
            this.OrderInfo = orderInfo;
            this.RedirectUrl = redirectUrl;
            this.IpnUrl = ipnUrl;
            this.RequestType = requestType;
            this.ExtraData = extraData;
            this.Lang = lang;
        }

        public void MakeSignature(string accessKey, string secretKey)
        {
            var rawHash = "accessKey=" + accessKey +
                "&amount=" + Amount +
                "&extraData=" + ExtraData +
                "&ipnUrl=" + IpnUrl +
                "&orderId=" + OrderId +
                "&orderInfo=" + OrderInfo +
                "&partnerCode=" + PartnerCode +
                "&redirectUrl=" + RedirectUrl +
                "&requestId=" + RequestId +
                "&requestType=" + RequestType;
            Signature = HashHelper.HmacSHA256(rawHash, secretKey);
        }

        public void SetMoMoOptions(MoMoOptions options)
        {
            PartnerCode = options.PartnerCode;
            RedirectUrl = options.ReturnUrl;
            IpnUrl = options.IpnUrl;
        }

        public void SetTransactionData(string requestId, long amount, string orderId, string orderInfo, string extraData)
        {
            this.RequestId = requestId;
            this.Amount = amount;
            this.OrderId = orderId;
            this.OrderInfo = orderInfo;
            this.ExtraData = extraData;
        }

        public (bool, string?) GetPaymentMethod(string paymentUrl)
        {
            using HttpClient client = new HttpClient();
            var requestData = JsonConvert.SerializeObject(this, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            });
            var requestContent = new StringContent(requestData, Encoding.UTF8, "application/json");

            var createPaymentResponse = client.PostAsync(paymentUrl, requestContent).Result;

            if (createPaymentResponse.IsSuccessStatusCode)
            {
                var responseContent = createPaymentResponse.Content.ReadAsStringAsync().Result;
                var responseData = JsonConvert.DeserializeObject<MoMoCreatePaymentResponse>(responseContent);
                if (responseData!.ResultCode == "0")
                {
                    return (true, responseContent);
                }
                else
                {
                    return (false, responseData.Message);
                }
            }
            else
            {
                return (false, createPaymentResponse.ReasonPhrase);
            }
        }
    }
}
