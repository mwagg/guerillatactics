namespace LooselyCoupledMVP.Domain.Model
{
    public class ExpensiveLoan : Loan
    {
        public override string LoadName
        {
            get
            {
                return "Expensive Loan";
            }
        }

        public override decimal GetMonthlyPayments(decimal loanAmount, int loanTerm)
        {
            return loanAmount*2/loanTerm;
        }
    }
}