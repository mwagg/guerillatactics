using GuerillaTactics.Common.Eventing;
using LooselyCoupledMVP.Domain.Messages;
using LooselyCoupledMVP.Presentation.Views;

namespace LooselyCoupledMVP.Presentation.Presenters
{
    public class ResultsPresenter :IMessageSubscriber<LoanSelectedMessage>
    {
        private IResultsForm _view;

        public void SetView(IResultsForm view)
        {
            _view = view;
        }

    	public void HandleMessage(LoanSelectedMessage message)
    	{
    		_view.LoanName = message.Loan.Name;
    		_view.MonthlyPayments = message.Loan.GetMonthlyPayments(message.LoanApplication.Amount,
    		                                                        message.LoanApplication.Term);
    	}
    }
}