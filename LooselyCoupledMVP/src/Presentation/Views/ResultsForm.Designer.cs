namespace LooselyCoupledMVP.Presentation.Views
{
    partial class ResultsForm : IResultsForm
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
            this.loanNameLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.monthlyPaymentsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "The available loan is";
            // 
            // loanNameLabel
            // 
            this.loanNameLabel.AutoSize = true;
            this.loanNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loanNameLabel.Location = new System.Drawing.Point(227, 18);
            this.loanNameLabel.Name = "loanNameLabel";
            this.loanNameLabel.Size = new System.Drawing.Size(74, 26);
            this.loanNameLabel.TabIndex = 1;
            this.loanNameLabel.Text = "(none)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(318, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "The monthly repayments will be";
            // 
            // monthlyPaymentsLabel
            // 
            this.monthlyPaymentsLabel.AutoSize = true;
            this.monthlyPaymentsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monthlyPaymentsLabel.Location = new System.Drawing.Point(336, 57);
            this.monthlyPaymentsLabel.Name = "monthlyPaymentsLabel";
            this.monthlyPaymentsLabel.Size = new System.Drawing.Size(24, 26);
            this.monthlyPaymentsLabel.TabIndex = 3;
            this.monthlyPaymentsLabel.Text = "0";
            // 
            // ResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 126);
            this.Controls.Add(this.monthlyPaymentsLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.loanNameLabel);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(50, 400);
            this.MaximizeBox = false;
            this.Name = "ResultsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ResultsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label loanNameLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label monthlyPaymentsLabel;
    }
}