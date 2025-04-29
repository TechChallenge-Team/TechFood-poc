using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using TechFood.Application.Common.Data;
using TechFood.Application.Common.Services.Interfaces;

namespace TechFood.Infra.Services.MercadoPago
{
    internal class MercadoPagoPaymentService(
        IOptions<MercadoPagoOptions> options,
        IHttpClientFactory httpClientFactory,
        IHttpContextAccessor httpContextAccessor) : IQrCodePaymentService
    {
        private readonly MercadoPagoOptions _options = options.Value;
        private readonly HttpClient _client = httpClientFactory.CreateClient(MercadoPagoOptions.ClientName);
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };

        public async Task<QrCodePaymentResult> GeneratePaymentAsync(QrCodePaymentRequest data)
        {
            var http = _httpContextAccessor.HttpContext!.Request;
            var notificationUrl = $"{http.Scheme}://{http.Host}/v1/notifications/mercadopago";

            var response = await _client.PostAsJsonAsync(
                string.Format(
                    "instore/orders/qr/seller/collectors/{0}/pos/{1}/qrs",
                    _options.UserId,
                    data.PosId),
                new QrCodeRequest(
                    data.OrderId.ToString(),
                    data.Title,
                    data.Title,
                    notificationUrl,
                    data.Amount,
                    data.Items.ConvertAll(
                        i => new PaymentItem(
                            i.Title,
                            i.Quantity,
                            i.Unit,
                            i.UnitPrice,
                            i.Amount
                            ))
                    ), _jsonOptions);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ErrorResult>();

                throw new Exception(error!.Message);
            }

            var result = await response.Content.ReadFromJsonAsync<QrCodeResult>(_jsonOptions);

            return new(
                result!.InStoreOrderId,
                result.QrData);
        }
    }

    record QrCodeRequest(
        string ExternalReference,
        string Title,
        string Description,
        string NotificationUrl,
        decimal TotalAmount,
        List<PaymentItem> Items
        );

    record QrCodeResult(
        string QrData,
        string InStoreOrderId
        );

    record ErrorResult(
        string Message,
        string Error
        );

    record PaymentItem(
        string Title,
        int Quantity,
        string UnitMeasure,
        decimal UnitPrice,
        decimal TotalAmount
        );
}
