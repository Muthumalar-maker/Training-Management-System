namespace TrainingManagementSystem.Services
{
    public interface IPaymentProcessor
    {
        bool ProcessPayment(decimal amount);
    }
}