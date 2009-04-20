namespace LooselyCoupledMVP.Domain.Model
{
    public class CheapLoan : Loan
    {
        public override string Name
        {
            get { return "Cheap loan"; }
        }

        public override decimal GetMonthlyPayments(decimal loanAmount, int loanTerm)
        {
            return loanAmount*1.1m/loanTerm;
        }
    }
}