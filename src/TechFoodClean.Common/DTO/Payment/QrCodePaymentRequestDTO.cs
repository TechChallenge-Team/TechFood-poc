namespace TechFoodClean.Common.DTO.Payment
{
    public class QrCodePaymentRequestDTO
    {
        public string PosId { get; set; }

        public Guid OrderId { get; set; }

        public string Title { get; set; }

        public decimal Amount { get; set; }

        public List<PaymentItemDTO> Items { get; set; } = [];

        public QrCodePaymentRequestDTO(string posId, Guid orderId, string title, decimal amount, List<PaymentItemDTO> items)
        {
            PosId = posId;
            OrderId = orderId;
            Title = title;
            Amount = amount;
            Items = items;
        }

    }
}
