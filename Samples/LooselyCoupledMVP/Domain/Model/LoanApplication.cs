namespace LooselyCoupledMVP.Domain.Model
{
    public class LoanApplication
    {
        public decimal Amount { get; set; }
        public int Term { get; set; }
        public CreditRating CreditRating { get; set; }
    }
}