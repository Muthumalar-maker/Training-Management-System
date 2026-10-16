using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class TrainingService
    {
        private readonly Course[] courses;

        public TrainingService(Course[] courses)
        {
            this.courses = courses;
        }

        public void ListCourses()
        {
            Console.WriteLine("\n===== AVAILABLE COURSES =====");

            foreach (Course course in courses)
            {
                Console.WriteLine(
                    $"Code: {course.CourseCode} | " +
                    $"Title: {course.Title} | " +
                    $"Fee: Rs. {course.Fee:F2} | " +
                    $"Available Seats: {course.AvailableSeats}");
            }
        }

        public Course? FindCourse(string courseCode)
        {
            foreach (Course course in courses)
            {
                if (course.CourseCode.Equals(
                    courseCode,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return course;
                }
            }

            return null;
        }

        public void SortCoursesByFee()
        {
            // Sort a temporary array so the original array is unchanged.
            Course[] sortedCourses = (Course[])courses.Clone();

            Array.Sort(sortedCourses, (first, second) =>
                first.Fee.CompareTo(second.Fee));

            Console.WriteLine("\n===== COURSES SORTED BY FEE =====");

            foreach (Course course in sortedCourses)
            {
                Console.WriteLine(
                    $"{course.CourseCode} - {course.Title} - Rs. {course.Fee:F2}");
            }
        }
    }
}