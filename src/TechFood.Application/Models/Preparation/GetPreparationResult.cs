using System;
using TechFood.Domain.Enums;

namespace TechFood.Application.Models.Preparation
{
    public class GetPreparationResult
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public Guid OrderId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? FinishedAt { get; set; }

        public PreparationStatusType Status { get; set; }
    }
}
