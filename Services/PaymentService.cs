using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class PaymentService
    {
        private readonly List<Payment> payments = new();
        private readonly List<Enrollment> enrollments;
        private int nextPaymentId = 1;

        public PaymentService(List<Enrollment> enrollments)
        {
            this.enrollments = enrollments;
        }

        public void ProcessPayment(
            int enrollmentId,
            decimal amount,
            IPaymentProcessor processor)
        {
            Enrollment? enrollment = null;

            foreach (Enrollment item in enrollments)
            {
                if (item.EnrollmentId == enrollmentId)
                {
                    enrollment = item;
                    break;
                }
            }

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return;
            }

            if (enrollment.Status == EnrollmentStatus.Cancelled)
            {
                Console.WriteLine("Payment rejected: enrollment is cancelled.");
                return;
            }

            if (amount <= 0m)
            {
                Console.WriteLine("Payment amount must be greater than zero.");
                return;
            }

            bool success = processor.ProcessPayment(amount);

            PaymentStatus status = success
                ? PaymentStatus.Successful
                : PaymentStatus.Failed;

            Payment payment = new Payment(
                nextPaymentId++,
                enrollmentId,
                amount,
                DateTime.Now,
                status
            );

            // Record both successful and failed attempts.
            payments.Add(payment);

            if (success)
            {
                enrollment.PaymentStatus = PaymentStatus.Successful;
                Console.WriteLine("Payment record saved.");
            }
            else
            {
                Console.WriteLine("Failed payment attempt recorded.");
            }
        }

        public void DisplayPaymentHistory()
        {
            Console.WriteLine("\n===== PAYMENT HISTORY =====");

            if (payments.Count == 0)
            {
                Console.WriteLine("No payment attempts recorded.");
                return;
            }

            foreach (Payment payment in payments)
            {
                Console.WriteLine(
                    $"Payment ID: {payment.PaymentId}, " +
                    $"Enrollment ID: {payment.EnrollmentId}, " +
                    $"Amount: Rs. {payment.Amount:F2}, " +
                    $"Date: {payment.PaymentDate}, " +
                    $"Status: {payment.Status}");
            }
        }

        public List<Payment> GetPayments()
        {
            return payments;
        }
    }
}