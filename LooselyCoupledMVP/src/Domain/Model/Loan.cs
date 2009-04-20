namespace LooselyCoupledMVP.Domain.Model
{
    public abstract class Loan
    {
        public abstract string LoadName{ get;}
        public abstract decimal GetMonthlyPayments(decimal loanAmount, int loanTerm);
    }
}