namespace TechFood.Domain.Enums;

public enum OrderStatusType
{
    Created,
    WaitingPayment,
    Received,
    RefusedPayment,
    InPreparation,
    Done,
    Finished
}
