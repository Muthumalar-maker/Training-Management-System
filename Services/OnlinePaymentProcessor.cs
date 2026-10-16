namespace TrainingManagementSystem.Services
{
    public class OnlinePaymentProcessor : IPaymentProcessor
    {
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine("Online payment selected.");
            Console.WriteLine($"Amount: Rs. {amount:F2}");

            Console.Write("Simulate success? (y/n): ");
            string? choice = Console.ReadLine();

            if (choice?.Trim().ToLower() == "y")
            {
                Console.WriteLine("Simulated online payment successful.");
                return true;
            }

            Console.WriteLine("Simulated online payment failed.");
            return false;
        }
    }
}