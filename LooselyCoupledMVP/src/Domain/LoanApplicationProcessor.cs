using LooselyCoupledMVP.Domain.Model;

namespace LooselyCoupledMVP.Domain
{
    internal class LoanApplicationProcessor
    {
        private readonly LoanApplication _application;

        public LoanApplicationProcessor(LoanApplication application)
        {
            _application = application;
        }

        public Loan GetLoan()
        {
            switch (_application.CreditRating)
            {
                case CreditRating.Good:
                    return new CheapLoan();
                case CreditRating.Poor:
                    return new ModeratelyExpensiveLoan();
                case CreditRating.Terrible:
                    return new ExpensiveLoan();
                default:
                    goto case CreditRating.Poor;
            }
        }
    }
}