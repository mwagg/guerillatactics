using LooselyCoupledMVP.Domain.Messages;
using LooselyCoupledMVP.Domain.Model;

namespace LooselyCoupledMVP.Domain.Commands
{
	public class ProcessLoanApplicationCommand : ICommand<LoanApplicationUpdatedMessage>
	{
		private readonly ApplicationController _applicationController;

		public ProcessLoanApplicationCommand(ApplicationController applicationController)
		{
			_applicationController = applicationController;
		}

		public void Execute(LoanApplicationUpdatedMessage argument)
		{
			Loan loan = new LoanApplicationProcessor(argument.LoanApplication).GetLoan();

			_applicationController.PublishMessage(new LoanSelectedMessage(loan, argument.LoanApplication));
		}
	}
}