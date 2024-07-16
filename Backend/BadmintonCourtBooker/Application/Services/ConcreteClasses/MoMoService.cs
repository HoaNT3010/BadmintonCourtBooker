using Application.RequestDTOs.MoMo;
using Application.ResponseDTOs.MoMo;
using Application.Services.Interfaces;
using Application.Utilities;
using Application.Utilities.OptionsSetup;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Application.Services.ConcreteClasses
{
    public class MoMoService : IMoMoService
    {
        private readonly MoMoOptions moMoOptions;

        public MoMoService(IOptions<MoMoOptions> moMoOptions)
        {
            this.moMoOptions = moMoOptions.Value;
        }

        public MoMoCreatePaymentResponse? CreateMoMoPaymentForBookingTransaction(Transaction transaction)
        {
            string orderInfo = $"Customer '{transaction.CreatorId}' pay for booking transaction '{transaction.Id}'";

            CreateMoMoPaymentRequest paymentRequest = new CreateMoMoPaymentRequest();
            paymentRequest.SetMoMoOptions(moMoOptions);

            MoMoExtraData extraData = new MoMoExtraData()
            {
                TransactionId = transaction.Id.ToString(),
                CustomerId = transaction.CreatorId.ToString()!,
                TransactionType = transaction.TransactionDetails.ToList()[0].Type.ToString(),
            };
            var extraDataString = HashHelper.EncodeToBase64(JsonConvert.SerializeObject(extraData));

            paymentRequest.SetTransactionData(transaction.Id.ToString(), (long)transaction.TotalAmount, transaction.Id.ToString(), orderInfo, extraDataString);
            paymentRequest.MakeSignature(moMoOptions.AccessKey, moMoOptions.SecretKey);

            (bool result, string? paymentData) = paymentRequest.GetPaymentMethod(moMoOptions.PaymentUrl);
            if (!result)
            {
                throw new Exception($"Failed to retrieve MoMo payment method for transaction '{transaction.Id.ToString()}'");
            }
            return JsonConvert.DeserializeObject<MoMoCreatePaymentResponse>(paymentData!);
        }
    }
}
