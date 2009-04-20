using LooselyCoupledMVP.Domain.Messages;
using LooselyCoupledMVP.Domain.Model;

namespace LooselyCoupledMVP.Presentation.Presenters
{
    public class DataEntryPresenter
    {
        private readonly ApplicationController _applicationController;

        public DataEntryPresenter(ApplicationController applicationController)
        {
            _applicationController = applicationController;
        }

        public void LoanApplicationValueChanged(int loanAmount, int loanTerm, CreditRating creditRating)
        {
            var application = new LoanApplication
                                  {
                                      LoanAmount = loanAmount,
                                      Term = loanTerm,
                                      CreditRating = creditRating
                                  };
            _applicationController.ExecuteCommand(new LoanApplicationUpdated(application));
        }
    }
}