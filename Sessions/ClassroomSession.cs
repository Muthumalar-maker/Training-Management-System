using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Sessions
{
    public class ClassroomSession : CourseSession
    {
        public ClassroomSession(
            string courseCode,
            DateTime startDate,
            SessionDuration duration)
            : base(courseCode, startDate, duration)
        {
        }

        public override decimal CalculateSessionCharge()
        {
            return 800m;
        }
    }
}