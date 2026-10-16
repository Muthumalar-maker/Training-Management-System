using System;
using TrainingManagementSystem.Models;
using TrainingManagementSystem.Services;
using TrainingManagementSystem.Sessions;

namespace TrainingManagementSystem
{
    internal class Program
    {
        static Course[] courses =
        {
            new Course("C101", "C# Programming", 5000m, 30, 30),
            new Course("C102", "Java Programming", 6000m, 45, 25),
            new Course("C103", "Python Programming", 4500m, 25, 20),
            new Course("C104", "Web Development", 7000m, 40, 15),
            new Course("C105", "Database Management", 4000m, 20, 20)
        };

        static void Main(string[] args)
        {
            //RunMenu();
            int choice = 0;

            GenericHelper helper = new GenericHelper();

            helper.DisplayItems(courses);

            LearnerService learnerService = new LearnerService();

            EnrollmentService enrollmentService =
                new EnrollmentService(courses, learnerService);

            IPaymentProcessor onlineProcessor =
    new OnlinePaymentProcessor();

            IPaymentProcessor classroomProcessor =
                new ClassroomPaymentProcessor();

            bool onlineResult = onlineProcessor.ProcessPayment(2000m);
            bool classroomResult = classroomProcessor.ProcessPayment(3000m);

            while (choice != 12)
            {
                Console.WriteLine("\n===== TRAINING MANAGEMENT SYSTEM =====");
                Console.WriteLine("1. Display All Courses");
                Console.WriteLine("2. Search by Course Code");
                Console.WriteLine("3. Search by Course Title");
                Console.WriteLine("4. Sort Fee Low to High");
                Console.WriteLine("5. Sort Fee High to Low");
                Console.WriteLine("6. Register Learner");
                Console.WriteLine("7. Display All Learners");
                Console.WriteLine("8. Search Learner by ID");
                Console.WriteLine("9. Remove Learner");
                Console.WriteLine("10. Enroll in a Course");
                Console.WriteLine("11. Cancel Enrollment");
                Console.WriteLine("12. Exit");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Please enter a valid number.");
                    choice = 0;
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        DisplayCourses(courses);
                        break;

                    case 2:
                        Console.Write("Enter course code: ");
                        string code = Console.ReadLine() ?? "";
                        SearchByCode(code.Trim());
                        break;

                    case 3:
                        Console.Write("Enter course title: ");
                        string title = Console.ReadLine() ?? "";
                        SearchByTitle(title.Trim());
                        break;

                    case 4:
                        SortCourses(true);
                        break;

                    case 5:
                        SortCourses(false);
                        break;

                    case 6:
                        learnerService.RegisterLearner();
                        break;

                    case 7:
                        learnerService.DisplayAllLearners();
                        break;

                    case 8:
                        learnerService.SearchLearner();
                        break;

                    case 9:
                        learnerService.RemoveLearner();
                        break;

                    case 10:
                        enrollmentService.EnrollCourse();
                        break;

                    case 11:
                        enrollmentService.CancelEnrollment();
                        break;

                    case 12:
                        Console.WriteLine(
                            "Thank you for using the Training Management System!");
                        break;

                    default:
                        Console.WriteLine(
                            "Invalid choice. Select 1 to 12.");
                        break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\n===== TRAINING MANAGEMENT SYSTEM =====");
            Console.WriteLine("1. List all courses");
            Console.WriteLine("2. Search courses");
            Console.WriteLine("3. Sort courses by fee");
            Console.WriteLine("4. Register a learner");
            Console.WriteLine("5. List learners");
            Console.WriteLine("6. Search for a learner");
            Console.WriteLine("7. Enroll in a course");
            Console.WriteLine("8. View enrollment records");
            Console.WriteLine("9. Cancel an enrollment");
            Console.WriteLine("10. Display a fee breakdown");
            Console.WriteLine("11. Process a simulated payment");
            Console.WriteLine("12. View payment history");
            Console.WriteLine("13. Display reports");
            Console.WriteLine("14. Exit");
            Console.Write("Enter your choice: ");
        }

        static void RunMenu()
        {
            bool running = true;

            while (running)
            {
                DisplayMenu();

                string? input = Console.ReadLine();

                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("List all courses selected.");
                        break;

                    case 2:
                        Console.WriteLine("Search courses selected.");
                        break;

                    case 3:
                        Console.WriteLine("Sort courses by fee selected.");
                        break;

                    case 4:
                        Console.WriteLine("Register a learner selected.");
                        break;

                    case 5:
                        Console.WriteLine("List learners selected.");
                        break;

                    case 6:
                        Console.WriteLine("Search for a learner selected.");
                        break;

                    case 7:
                        Console.WriteLine("Enroll in a course selected.");
                        break;

                    case 8:
                        Console.WriteLine("View enrollment records selected.");
                        break;

                    case 9:
                        Console.WriteLine("Cancel an enrollment selected.");
                        break;

                    case 10:
                        Console.WriteLine("Fee breakdown selected.");
                        break;

                    case 11:
                        Console.WriteLine("Simulated payment selected.");
                        break;

                    case 12:
                        Console.WriteLine("Payment history selected.");
                        break;

                    case 13:
                        Console.WriteLine("Reports selected.");
                        break;

                    case 14:
                        running = false;
                        Console.WriteLine("Thank you!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Enter 1 to 14.");
                        break;
                }
            }
        }

        // Display course details
        static void DisplayCourses(Course[] list)
        {
            Console.WriteLine(
                "\n--------------------------------------------------------------------------");

            Console.WriteLine(
                "{0,-8} {1,-25} {2,10} {3,10} {4,10} {5,10}",
                "Code", "Title", "Fee", "Days", "Seats", "Available");

            Console.WriteLine(
                "--------------------------------------------------------------------------");

            for (int i = 0; i < list.Length; i++)
            {
                Console.WriteLine(
                    "{0,-8} {1,-25} {2,10:F2} {3,10} {4,10} {5,10}",
                    list[i].CourseCode,
                    list[i].Title,
                    list[i].Fee,
                    list[i].DurationDays,
                    list[i].TotalSeats,
                    list[i].AvailableSeats);

            }
        }

        static void SearchByCode(string code)
        {
            bool found = false;

            for (int i = 0; i < courses.Length; i++)
            {
                if (string.Equals(
                    courses[i].CourseCode,
                    code,
                    StringComparison.OrdinalIgnoreCase))
                {
                    DisplayCourses(new Course[] { courses[i] });
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("Course not found.");
            }
        }

        // Search course by title
        static void SearchByTitle(string title)
        {
            bool found = false;

            for (int i = 0; i < courses.Length; i++)
            {
                if (courses[i].Title.IndexOf(
                    title,
                    StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    DisplayCourses(new Course[] { courses[i] });
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Course not found.");
            }
        }

        static void SortCourses(bool ascending)
        {
            Course[] sorted = (Course[])courses.Clone();

            for (int i = 0; i < sorted.Length - 1; i++)
            {
                for (int j = 0; j < sorted.Length - 1 - i; j++)
                {
                    bool swap;

                    if (ascending)
                    {
                        swap = sorted[j].Fee > sorted[j + 1].Fee;
                    }
                    else
                    {
                        swap = sorted[j].Fee < sorted[j + 1].Fee;
                    }

                    if (swap)
                    {
                        Course temp = sorted[j];
                        sorted[j] = sorted[j + 1];
                        sorted[j + 1] = temp;
                    }
                }
            }

            DisplayCourses(sorted);
        }

        static void TestCourseSummary()
        {
            CourseSummary summary1 = new CourseSummary(
                "C101", "C# Programming", 5000m, 25);

            CourseSummary summary2 = new CourseSummary(
                "C101", "C# Programming", 5000m, 25);

            Console.WriteLine(summary1 == summary2);

            CourseSummary modifiedSummary = summary1 with
            {
                Fee = 4500m
            };

            Console.WriteLine("Original Fee: " + summary1.Fee);
            Console.WriteLine("Modified Fee: " + modifiedSummary.Fee);
        }

        static void TestSessions()
        {
            CourseSession online = new OnlineSession(
                "C101",
                DateTime.Today.AddDays(20),
                new SessionDuration(30)
            );

            CourseSession classroom = new ClassroomSession(
                "C102",
                DateTime.Today.AddDays(25),
                new SessionDuration(45)
            );

            Console.WriteLine("Online Session Charge: Rs. " +
                online.CalculateSessionCharge());

            Console.WriteLine("Classroom Session Charge: Rs. " +
                classroom.CalculateSessionCharge());
        }
    }
}
