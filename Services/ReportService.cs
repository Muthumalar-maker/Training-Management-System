using System;
using System.Collections.Generic;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class ReportService
    {
        private readonly Course[] courses;
        private readonly List<Enrollment> enrollments;
        private readonly List<Payment> payments;

        public ReportService(
            Course[] courses,
            List<Enrollment> enrollments,
            List<Payment> payments)
        {
            this.courses = courses;
            this.enrollments = enrollments;
            this.payments = payments;
        }

        // Report A: Course Occupancy
        public void DisplayCourseOccupancy()
        {
            Console.WriteLine("\n===== COURSE OCCUPANCY =====");

            foreach (Course course in courses)
            {
                int totalSeats = course.TotalSeats;
                int occupiedSeats = course.EnrolledCount;
                int availableSeats = course.AvailableSeats;

                double occupancyPercentage = totalSeats > 0
                    ? (double)occupiedSeats / totalSeats * 100
                    : 0;

                Console.WriteLine($"Course: {course.Title}");
                Console.WriteLine($"Total Seats: {totalSeats}");
                Console.WriteLine($"Occupied Seats: {occupiedSeats}");
                Console.WriteLine($"Available Seats: {availableSeats}");
                Console.WriteLine($"Occupancy: {occupancyPercentage:F2}%");
                Console.WriteLine("-----------------------------");
            }
        }

        // Report B: Expected Fees
        public void DisplayExpectedFees()
        {
            decimal expectedFees = 0m;

            foreach (Enrollment enrollment in enrollments)
            {
                if (enrollment.Status == EnrollmentStatus.Active)
                {
                    expectedFees += enrollment.FinalFee;
                }
            }

            Console.WriteLine("\n===== EXPECTED FEES =====");
            Console.WriteLine(
                $"Expected fees from active enrollments: Rs. {expectedFees:F2}");
        }

        // Report C: Payment Summary
        public void DisplayPaymentSummary()
        {
            int successfulCount = 0;
            int failedCount = 0;
            decimal totalCollected = 0m;

            foreach (Payment payment in payments)
            {
                if (payment.Status == PaymentStatus.Successful)
                {
                    successfulCount++;
                    totalCollected += payment.Amount;
                }
                else if (payment.Status == PaymentStatus.Failed)
                {
                    failedCount++;
                }
            }

            Console.WriteLine("\n===== PAYMENT SUMMARY =====");
            Console.WriteLine($"Successful attempts: {successfulCount}");
            Console.WriteLine($"Failed attempts: {failedCount}");
            Console.WriteLine($"Total collected: Rs. {totalCollected:F2}");
        }

        // Report D: Course Enrollment Summary
        public void DisplayCourseEnrollmentSummary()
        {
            Console.WriteLine("\n===== COURSE ENROLLMENT SUMMARY =====");

            foreach (Course course in courses)
            {
                int activeCount = 0;

                foreach (Enrollment enrollment in enrollments)
                {
                    if (enrollment.CourseCode == course.CourseCode &&
                        enrollment.Status == EnrollmentStatus.Active)
                    {
                        activeCount++;
                    }
                }

                Console.WriteLine($"Course: {course.Title}");
                Console.WriteLine($"Active enrollments: {activeCount}");

                if (course.AvailableSeats <= 0)
                {
                    Console.WriteLine("Status: No seats remaining");
                }
                else
                {
                    Console.WriteLine(
                        $"Available seats: {course.AvailableSeats}");
                }

                Console.WriteLine("-----------------------------");
            }
        }

        // Display all four reports
        public void DisplayAllReports()
        {
            DisplayCourseOccupancy();
            DisplayExpectedFees();
            DisplayPaymentSummary();
            DisplayCourseEnrollmentSummary();
        }
    }
}