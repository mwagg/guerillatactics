using LooselyCoupledMVP.Domain.Messages;
using LooselyCoupledMVP.Domain.Model;

namespace LooselyCoupledMVP.Domain.Commands
{
    public class ProcessLoanApplicationCommand : ICommand<LoanApplicationUpdated>
    {
        private ApplicationController _applicationController;

        public ProcessLoanApplicationCommand(ApplicationController applicationController)
        {
            _applicationController = applicationController;
        }

        public void Execute(LoanApplicationUpdated argument)
        {
            Loan loan = new LoanApplicationProcessor(argument.LoanApplication).GetLoan();

            _applicationController.PublishMessage(new LoanSelected(loan, argument.LoanApplication));
        }
    }
}