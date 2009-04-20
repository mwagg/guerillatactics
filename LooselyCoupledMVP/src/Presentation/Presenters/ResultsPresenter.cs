using GuerillaTactics.Common.Eventing;
using LooselyCoupledMVP.Domain.Messages;
using LooselyCoupledMVP.Presentation.Views;

namespace LooselyCoupledMVP.Presentation.Presenters
{
    public class ResultsPresenter : IMessageSubscriber<LoanSelected>
    {
        private IResultsForm _view;

        #region IMessageSubscriber<LoanSelected> Members

        public void HandleMessage(LoanSelected message)
        {
            _view.LoanName = message.Loan.LoadName;
            _view.MonthlyPayments = message.Loan.GetMonthlyPayments(message.LoanApplication.LoanAmount,
                                                                    message.LoanApplication.Term);
        }

        #endregion

        public void SetView(IResultsForm view)
        {
            _view = view;
        }
    }
}