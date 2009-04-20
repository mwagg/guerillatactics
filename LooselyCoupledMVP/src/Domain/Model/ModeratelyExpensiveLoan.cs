namespace LooselyCoupledMVP.Domain.Model
{
    public class ModeratelyExpensiveLoan : Loan
    {
        public override string LoadName
        {
            get
            {
                return "Moderately expensive loan";
            }
        }

        public override decimal GetMonthlyPayments(decimal loanAmount, int loanTerm)
        {
            return loanAmount*1.5m/loanTerm;
        }
    }
}