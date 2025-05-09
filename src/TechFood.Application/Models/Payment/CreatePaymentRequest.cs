using System;
using System.ComponentModel.DataAnnotations;
using TechFood.Domain.Enums;

namespace TechFood.Application.Models.Payment
{
    public class CreatePaymentRequest
    {
        [Required]
        public Guid? OrderId { get; set; }

        public PaymentType Type { get; set; }
    }
}
