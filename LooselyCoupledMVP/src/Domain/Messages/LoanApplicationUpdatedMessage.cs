using LooselyCoupledMVP.Domain.Model;

namespace LooselyCoupledMVP.Domain.Messages
{
	public class LoanApplicationUpdatedMessage
	{
		private readonly LoanApplication _loanApplication;

		public LoanApplicationUpdatedMessage(LoanApplication loanApplication)
		{
			_loanApplication = loanApplication;
		}

		public LoanApplication LoanApplication
		{
			get { return _loanApplication; }
		}
	}
}