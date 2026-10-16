namespace TrainingManagementSystem.Models
{
    public readonly struct FeeSummary
    {
        public decimal BaseFee { get; }
        public decimal EarlyBirdDiscount { get; }
        public decimal GroupDiscount { get; }
        public decimal TotalDiscount { get; }
        public decimal Surcharge { get; }
        public decimal FinalFee { get; }

        public FeeSummary(
            decimal baseFee,
            decimal earlyBirdDiscount,
            decimal groupDiscount,
            decimal surcharge,
            decimal finalFee)
        {
            BaseFee = baseFee;
            EarlyBirdDiscount = earlyBirdDiscount;
            GroupDiscount = groupDiscount;
            TotalDiscount = earlyBirdDiscount + groupDiscount;
            Surcharge = surcharge;
            FinalFee = finalFee;
        }
    }
}