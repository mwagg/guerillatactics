using LooselyCoupledMVP.Domain.Model;

namespace LooselyCoupledMVP.Domain.Messages
{
    public class LoanSelectedMessage
    {
        private readonly Loan _loan;
        private readonly LoanApplication _loanApplication;

        public LoanSelectedMessage(Loan loan, LoanApplication loanApplication)
        {
            _loan = loan;
            _loanApplication = loanApplication;
        }

        public Loan Loan
        {
            get { return _loan; }
        }

        public LoanApplication LoanApplication
        {
            get { return _loanApplication; }
        }
    }
}