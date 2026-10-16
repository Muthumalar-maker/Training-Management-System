using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Sessions
{
    public class OnlineSession : CourseSession
    {
        public OnlineSession(
            string courseCode,
            DateTime startDate,
            SessionDuration duration)
            : base(courseCode, startDate, duration)
        {
        }

        public override decimal CalculateSessionCharge()
        {
            return 200m;
        }
    }
}