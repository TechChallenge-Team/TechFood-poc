namespace TechFood.Domain.Enums;

public enum OrderStatusType
{
    Created,
    WaitingPayment,
    Paid,
    RefusedPayment,
    InPreparation,
    Done,
    Finished
}
