namespace TrainingManagementSystem.Services
{
    public class ClassroomPaymentProcessor : IPaymentProcessor
    {
        bool IPaymentProcessor.ProcessPayment(decimal amount)
        {
            Console.WriteLine("Classroom payment selected.");
            Console.WriteLine($"Amount: Rs. {amount:F2}");

            Console.Write("Simulate success? (y/n): ");
            string? choice = Console.ReadLine();

            if (choice?.Trim().ToLower() == "y")
            {
                Console.WriteLine("Simulated classroom payment successful.");
                return true;
            }

            Console.WriteLine("Simulated classroom payment failed.");
            return false;
        }
    }
}