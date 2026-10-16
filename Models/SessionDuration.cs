namespace TrainingManagementSystem.Models
{
    public struct SessionDuration
    {
        public int Days { get; set; }

        public SessionDuration(int days)
        {
            if (days <= 0)
                throw new ArgumentException("Duration must be greater than zero.");

            Days = days;
        }
    }
}