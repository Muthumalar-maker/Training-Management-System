
using System;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class FeeCalculator
    {
        // Method 1: Calculate fee without discounts
        public (decimal BaseFee, decimal TotalDiscount, decimal FinalFee)
            CalculateFee(decimal baseFee, decimal surcharge)
        {
            ValidateFee(baseFee, surcharge);

            return (
                BaseFee: baseFee,
                TotalDiscount: 0m,
                FinalFee: baseFee + surcharge
            );
        }

        // Method 2: Calculate fee with discounts
        public (decimal BaseFee, decimal TotalDiscount, decimal FinalFee)
            CalculateFee(
                decimal baseFee,
                decimal earlyBirdDiscount,
                decimal groupDiscount,
                decimal surcharge)
        {
            ValidateFee(baseFee, surcharge);

            if (earlyBirdDiscount < 0m || groupDiscount < 0m)
            {
                throw new ArgumentException(
                    "Discounts cannot be negative.");
            }

            decimal totalDiscount = earlyBirdDiscount + groupDiscount;

            if (totalDiscount > baseFee)
            {
                throw new ArgumentException(
                    "Total discount cannot exceed the base fee.");
            }

            return (
                BaseFee: baseFee,
                TotalDiscount: totalDiscount,
                FinalFee: baseFee - totalDiscount + surcharge
            );
        }

        // Apply early-bird and group discount rules
        public FeeSummary CalculateSummary(
            decimal baseFee,
            int daysUntilStart,
            int eligibleLearnerCount,
            SessionType sessionType)
        {
            ValidateFee(baseFee, 0m);

            if (daysUntilStart < 0)
            {
                throw new ArgumentException(
                    "Days until start cannot be negative.");
            }

            if (eligibleLearnerCount < 1)
            {
                throw new ArgumentException(
                    "Eligible learner count must be at least 1.");
            }

            decimal earlyBirdDiscount =
                daysUntilStart >= 14 ? baseFee * 0.10m : 0m;

            decimal groupDiscount =
                eligibleLearnerCount >= 3 ? baseFee * 0.05m : 0m;

            decimal surcharge;

            switch (sessionType)
            {
                case SessionType.Online:
                    surcharge = 200m;
                    break;

                case SessionType.Classroom:
                    surcharge = 800m;
                    break;

                default:
                    throw new ArgumentException("Invalid session type.");
            }

            var result = CalculateFee(
                baseFee,
                earlyBirdDiscount,
                groupDiscount,
                surcharge);

            return new FeeSummary(
                result.BaseFee,
                earlyBirdDiscount,
                groupDiscount,
                surcharge,
                result.FinalFee);
        }

        // Pass summary using 'in' (read-only reference)
        public void DisplayFeeBreakdown(in FeeSummary summary)
        {
            Console.WriteLine("\n========== FEE BREAKDOWN ==========");
            Console.WriteLine($"Base Fee:             Rs. {summary.BaseFee:F2}");
            Console.WriteLine($"Early-Bird Discount:  Rs. {summary.EarlyBirdDiscount:F2}");
            Console.WriteLine($"Group Discount:       Rs. {summary.GroupDiscount:F2}");
            Console.WriteLine($"Total Discount:       Rs. {summary.TotalDiscount:F2}");
            Console.WriteLine($"Session Surcharge:    Rs. {summary.Surcharge:F2}");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"Final Fee:            Rs. {summary.FinalFee:F2}");
            Console.WriteLine("===================================");
        }

        private void ValidateFee(decimal baseFee, decimal surcharge)
        {
            if (baseFee < 0m)
            {
                throw new ArgumentException(
                    "Base fee cannot be negative.");
            }

            if (surcharge < 0m)
            {
                throw new ArgumentException(
                    "Surcharge cannot be negative.");
            }
        }
    }
}
