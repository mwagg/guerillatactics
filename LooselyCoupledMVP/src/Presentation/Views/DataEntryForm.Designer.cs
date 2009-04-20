namespace LooselyCoupledMVP.Presentation.Views
{
    partial class DataEntryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.loanAmountTextBox = new System.Windows.Forms.TextBox();
            this.loanTermDropDown = new System.Windows.Forms.ComboBox();
            this.creditRatingDropDown = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loan amount";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Loan term (months)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Credit rating";
            // 
            // loanAmountTextBox
            // 
            this.loanAmountTextBox.Location = new System.Drawing.Point(131, 12);
            this.loanAmountTextBox.Name = "loanAmountTextBox";
            this.loanAmountTextBox.Size = new System.Drawing.Size(121, 20);
            this.loanAmountTextBox.TabIndex = 3;
            this.loanAmountTextBox.TextChanged += new System.EventHandler(this.HandleLoanApplicationValueChanged);
            // 
            // loanTermDropDown
            // 
            this.loanTermDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.loanTermDropDown.FormattingEnabled = true;
            this.loanTermDropDown.Location = new System.Drawing.Point(131, 38);
            this.loanTermDropDown.Name = "loanTermDropDown";
            this.loanTermDropDown.Size = new System.Drawing.Size(121, 21);
            this.loanTermDropDown.TabIndex = 5;
            this.loanTermDropDown.SelectedIndexChanged += new System.EventHandler(this.HandleLoanApplicationValueChanged);
            // 
            // creditRatingDropDown
            // 
            this.creditRatingDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.creditRatingDropDown.FormattingEnabled = true;
            this.creditRatingDropDown.Location = new System.Drawing.Point(131, 65);
            this.creditRatingDropDown.Name = "creditRatingDropDown";
            this.creditRatingDropDown.Size = new System.Drawing.Size(121, 21);
            this.creditRatingDropDown.TabIndex = 6;
            this.creditRatingDropDown.SelectedIndexChanged += new System.EventHandler(this.HandleLoanApplicationValueChanged);
            // 
            // DataEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 115);
            this.Controls.Add(this.creditRatingDropDown);
            this.Controls.Add(this.loanTermDropDown);
            this.Controls.Add(this.loanAmountTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(50, 30);
            this.MaximizeBox = false;
            this.Name = "DataEntryForm";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DataEntryForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox loanAmountTextBox;
        private System.Windows.Forms.ComboBox loanTermDropDown;
        private System.Windows.Forms.ComboBox creditRatingDropDown;
    }
}