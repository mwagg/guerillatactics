using System.Windows.Forms;
using LooselyCoupledMVP.Presentation.Presenters;

namespace LooselyCoupledMVP.Presentation.Views
{
    public partial class ResultsForm : Form, IResultsForm
    {
        public ResultsForm()
        {
            InitializeComponent();
        }

        public ResultsForm(ResultsPresenter presenter):this()
        {
            presenter.SetView(this);
        }

        public string LoanName
        {
            get { return loanNameLabel.Text; }
            set { loanNameLabel.Text = value; }
        }

        public decimal MonthlyPayments
        {
            get { return decimal.Parse(monthlyPaymentsLabel.Text); }
            set { monthlyPaymentsLabel.Text = value.ToString("C"); }
        }
    }
}