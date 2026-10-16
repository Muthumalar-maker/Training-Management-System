
using System;
using System.Collections.Generic;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class EnrollmentService
    {
        private List<Enrollment> enrollments = new List<Enrollment>();
        private Course[] courses;
        private LearnerService learnerService;
        private FeeCalculator feeCalculator = new FeeCalculator();
        private int nextEnrollmentId = 1;

        public EnrollmentService(Course[] courses, LearnerService learnerService)
        {
            this.courses = courses;
            this.learnerService = learnerService;
        }

        // ENROLL IN A COURSE
        public void EnrollCourse()
        {
            Console.WriteLine("\n--- Course Enrollment ---");

            Console.Write("Enter Learner ID: ");
            string learnerId = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(learnerId))
            {
                Console.WriteLine("Learner ID cannot be empty.");
                return;
            }

            if (!learnerService.LearnerExists(learnerId))
            {
                Console.WriteLine("Learner not found. Please register first.");
                return;
            }

            Console.Write("Enter Course Code: ");
            string courseCode = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(courseCode))
            {
                Console.WriteLine("Course code cannot be empty.");
                return;
            }

            Course? selectedCourse = null;

            for (int i = 0; i < courses.Length; i++)
            {
                if (string.Equals(courses[i].CourseCode, courseCode,
                    StringComparison.OrdinalIgnoreCase))
                {
                    selectedCourse = courses[i];
                    break;
                }
            }

            if (selectedCourse == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            for (int i = 0; i < enrollments.Count; i++)
            {
                if (string.Equals(enrollments[i].LearnerId, learnerId,
                        StringComparison.OrdinalIgnoreCase)
                    && string.Equals(enrollments[i].CourseCode,
                        selectedCourse.CourseCode,
                        StringComparison.OrdinalIgnoreCase)
                    && enrollments[i].Status == EnrollmentStatus.Active)
                {
                    Console.WriteLine("This learner is already actively enrolled in this course.");
                    return;
                }
            }

            if (selectedCourse.AvailableSeats <= 0)
            {
                Console.WriteLine("Sorry, no seats are available for this course.");
                return;
            }


            Console.Write("Days until course starts: ");

            if (!int.TryParse(Console.ReadLine(), out int daysUntilStart)
                || daysUntilStart < 0)
            {
                Console.WriteLine("Enter a valid non-negative number of days.");
                return;
            }

            Console.Write("Number of eligible learners enrolling together: ");

            if (!int.TryParse(Console.ReadLine(), out int eligibleLearnerCount)
                || eligibleLearnerCount < 1)
            {
                Console.WriteLine("Enter a valid learner count.");
                return;
            }

            Console.WriteLine("Select session type:");
            Console.WriteLine("1. Online");
            Console.WriteLine("2. Classroom");
            Console.Write("Enter choice: ");

            if (!int.TryParse(Console.ReadLine(), out int sessionChoice))
            {
                Console.WriteLine("Invalid session choice.");
                return;
            }

            SessionType sessionType;

            switch (sessionChoice)
            {
                case 1:
                    sessionType = SessionType.Online;
                    break;
                case 2:
                    sessionType = SessionType.Classroom;
                    break;
                default:
                    Console.WriteLine("Select 1 or 2.");
                    return;
            }

            FeeSummary feeSummary;

            try
            {
                feeSummary = feeCalculator.CalculateSummary(
                    selectedCourse.Fee,
                    daysUntilStart,
                    eligibleLearnerCount,
                    sessionType);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Fee calculation failed: " + ex.Message);
                return;
            }

            decimal baseFee = feeSummary.BaseFee;
            decimal discountAmount = feeSummary.TotalDiscount;
            decimal finalFee = feeSummary.FinalFee;

            feeCalculator.DisplayFeeBreakdown(in feeSummary);


            Enrollment enrollment = new Enrollment(
                nextEnrollmentId,
                learnerId,
                selectedCourse.CourseCode,
                DateTime.Now,
                baseFee,
                discountAmount,
                finalFee,
                EnrollmentStatus.Active
            );

            enrollments.Add(enrollment);
            selectedCourse.EnrolledCount++;
            nextEnrollmentId++;

            Console.WriteLine("\nEnrollment successful!");
            Console.WriteLine("------------------------------");
            Console.WriteLine("Enrollment ID: " + enrollment.EnrollmentId);
            Console.WriteLine("Learner ID: " + enrollment.LearnerId);
            Console.WriteLine("Course Code: " + enrollment.CourseCode);
            Console.WriteLine("Course Name: " + selectedCourse.Title);
            Console.WriteLine("Enrollment Date: " + enrollment.EnrollmentDate.ToString("dd-MM-yyyy"));
            Console.WriteLine("Base Fee: Rs. " + enrollment.BaseFee.ToString("F2"));
            Console.WriteLine("Discount: Rs. " + enrollment.DiscountAmount.ToString("F2"));
            Console.WriteLine("Final Fee: Rs. " + enrollment.FinalFee.ToString("F2"));
            Console.WriteLine("Status: " + enrollment.Status);
            Console.WriteLine("Available Seats: " + selectedCourse.AvailableSeats);
        }

        // CANCEL AN ENROLLMENT
        public void CancelEnrollment()
        {
            Console.WriteLine("\n--- Cancel Enrollment ---");

            Console.Write("Enter Enrollment ID: ");
            string input = (Console.ReadLine() ?? "").Trim();

            if (!int.TryParse(input, out int enrollmentId) || enrollmentId <= 0)
            {
                Console.WriteLine("Invalid Enrollment ID.");
                return;
            }

            Enrollment? selectedEnrollment = null;

            for (int i = 0; i < enrollments.Count; i++)
            {
                if (enrollments[i].EnrollmentId == enrollmentId)
                {
                    selectedEnrollment = enrollments[i];
                    break;
                }
            }

            if (selectedEnrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return;
            }

            if (selectedEnrollment.Status == EnrollmentStatus.Cancelled)
            {
                Console.WriteLine("This enrollment is already cancelled.");
                return;
            }

            if (selectedEnrollment.Status != EnrollmentStatus.Active)
            {
                Console.WriteLine("Only active enrollments can be cancelled.");
                return;
            }

            Course? selectedCourse = null;

            for (int i = 0; i < courses.Length; i++)
            {
                if (string.Equals(courses[i].CourseCode,
                    selectedEnrollment.CourseCode,
                    StringComparison.OrdinalIgnoreCase))
                {
                    selectedCourse = courses[i];
                    break;
                }
            }

            if (selectedCourse == null)
            {
                Console.WriteLine("Associated course not found. Cancellation failed.");
                return;
            }

            if (selectedCourse.EnrolledCount <= 0)
            {
                Console.WriteLine("Seat count is invalid. Cancellation failed.");
                return;
            }

            selectedEnrollment.Status = EnrollmentStatus.Cancelled;
            selectedCourse.EnrolledCount--;

            Console.WriteLine("\nEnrollment cancelled successfully!");
            Console.WriteLine("Enrollment ID: " + selectedEnrollment.EnrollmentId);
            Console.WriteLine("Course Code: " + selectedEnrollment.CourseCode);
            Console.WriteLine("Status: " + selectedEnrollment.Status);
            Console.WriteLine("Available Seats: " + selectedCourse.AvailableSeats);
        }
    }
}
