using LooselyCoupledMVP.Domain.Model;

namespace LooselyCoupledMVP.Domain.Messages
{
    public class LoanApplicationUpdated
    {
        private readonly LoanApplication _loanApplication;

        public LoanApplication LoanApplication
        {
            get { return _loanApplication; }
        }

        public LoanApplicationUpdated(LoanApplication loanApplication)
        {
            _loanApplication = loanApplication;
        }
    }
}