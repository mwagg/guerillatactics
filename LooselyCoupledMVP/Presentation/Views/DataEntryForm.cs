using System;
using System.Windows.Forms;
using LooselyCoupledMVP.Domain.Model;
using LooselyCoupledMVP.Presentation.Presenters;

namespace LooselyCoupledMVP.Presentation.Views
{
    public partial class DataEntryForm : Form
    {
        private readonly DataEntryPresenter _presenter;
        private readonly bool _updating;

        public DataEntryForm()
        {
            InitializeComponent();

            _updating = true;
            SetAvailableLoanTerms();
            SetAvailableCreditRatings();
            _updating = false;
        }

        public DataEntryForm(DataEntryPresenter presenter) : this()
        {
            _presenter = presenter;
        }

        private void SetAvailableCreditRatings()
        {
            foreach (object creditRating in Enum.GetValues(typeof (CreditRating)))
            {
                creditRatingDropDown.Items.Add(creditRating);
            }

            creditRatingDropDown.SelectedIndex = 0;
        }

        private void SetAvailableLoanTerms()
        {
            for (int i = 1; i <= 48; i++)
            {
                loanTermDropDown.Items.Add(i);
            }

            loanTermDropDown.SelectedIndex = 0;
        }

        private void HandleLoanApplicationValueChanged(object sender, EventArgs e)
        {
            if (_updating == false && loanTermDropDown.Text.Length > 0)
            {
                _presenter.LoanApplicationValueChanged(Int32.Parse(loanAmountTextBox.Text),
                                                       GetSelectedTerm(),
                                                       GetSelectedCreditRating());
            }
        }

        private CreditRating GetSelectedCreditRating()
        {
            return (CreditRating) creditRatingDropDown.Items[creditRatingDropDown.SelectedIndex];
        }

        private int GetSelectedTerm()
        {
            return (int) loanTermDropDown.Items[loanTermDropDown.SelectedIndex];
        }
    }
}