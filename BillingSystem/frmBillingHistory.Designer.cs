namespace BillingSystem
{
    partial class frmBillingHistory
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
            lblTitle = new Label();
            dgvBillingHistory = new DataGridView();
            BillingMonth = new DataGridViewTextBoxColumn();
            PreviousReading = new DataGridViewTextBoxColumn();
            PresentReading = new DataGridViewTextBoxColumn();
            Consumption = new DataGridViewTextBoxColumn();
            RatePerCubic = new DataGridViewTextBoxColumn();
            TotalAmount = new DataGridViewTextBoxColumn();
            Status = new DataGridViewTextBoxColumn();
            lblClose = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvBillingHistory).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Bookman Old Style", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(23, 23);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(203, 28);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Billing History -";
            // 
            // dgvBillingHistory
            // 
            dgvBillingHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBillingHistory.Columns.AddRange(new DataGridViewColumn[] { BillingMonth, PreviousReading, PresentReading, Consumption, RatePerCubic, TotalAmount, Status });
            dgvBillingHistory.Location = new Point(23, 87);
            dgvBillingHistory.Name = "dgvBillingHistory";
            dgvBillingHistory.RowHeadersWidth = 51;
            dgvBillingHistory.Size = new Size(928, 285);
            dgvBillingHistory.TabIndex = 1;
            // 
            // BillingMonth
            // 
            BillingMonth.DataPropertyName = "BillingMonth";
            BillingMonth.HeaderText = "Billing Month";
            BillingMonth.MinimumWidth = 6;
            BillingMonth.Name = "BillingMonth";
            BillingMonth.Width = 125;
            // 
            // PreviousReading
            // 
            PreviousReading.DataPropertyName = "PreviousReading";
            PreviousReading.HeaderText = "Previous Reading";
            PreviousReading.MinimumWidth = 6;
            PreviousReading.Name = "PreviousReading";
            PreviousReading.Width = 125;
            // 
            // PresentReading
            // 
            PresentReading.DataPropertyName = "PresentReading";
            PresentReading.HeaderText = "Present Reading ";
            PresentReading.MinimumWidth = 6;
            PresentReading.Name = "PresentReading";
            PresentReading.Width = 125;
            // 
            // Consumption
            // 
            Consumption.DataPropertyName = "Consumption";
            Consumption.HeaderText = "Consumption ";
            Consumption.MinimumWidth = 6;
            Consumption.Name = "Consumption";
            Consumption.Width = 125;
            // 
            // RatePerCubic
            // 
            RatePerCubic.DataPropertyName = "RatePerCubic";
            RatePerCubic.HeaderText = "Rate Per Cubic";
            RatePerCubic.MinimumWidth = 6;
            RatePerCubic.Name = "RatePerCubic";
            RatePerCubic.Width = 125;
            // 
            // TotalAmount
            // 
            TotalAmount.DataPropertyName = "TotalAmount";
            TotalAmount.HeaderText = "Total Amount ";
            TotalAmount.MinimumWidth = 6;
            TotalAmount.Name = "TotalAmount";
            TotalAmount.Width = 125;
            // 
            // Status
            // 
            Status.DataPropertyName = "Status";
            Status.HeaderText = "Status ";
            Status.MinimumWidth = 6;
            Status.Name = "Status";
            Status.Width = 125;
            // 
            // lblClose
            // 
            lblClose.Font = new Font("Baskerville Old Face", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblClose.Location = new Point(845, 395);
            lblClose.Name = "lblClose";
            lblClose.Size = new Size(106, 27);
            lblClose.TabIndex = 2;
            lblClose.Text = "Close";
            lblClose.UseVisualStyleBackColor = true;
            lblClose.Click += lblClose_Click;
            // 
            // frmBillingHistory
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(972, 450);
            Controls.Add(lblClose);
            Controls.Add(dgvBillingHistory);
            Controls.Add(lblTitle);
            Name = "frmBillingHistory";
            Text = "frmBillingHistory";
            Load += frmBillingHistory_Load;
            ((System.ComponentModel.ISupportInitialize)dgvBillingHistory).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dgvBillingHistory;
        private Button lblClose;
        private Label lblTitle;
        private DataGridViewTextBoxColumn BillingMonth;
        private DataGridViewTextBoxColumn PreviousReading;
        private DataGridViewTextBoxColumn PresentReading;
        private DataGridViewTextBoxColumn Consumption;
        private DataGridViewTextBoxColumn RatePerCubic;
        private DataGridViewTextBoxColumn TotalAmount;
        private DataGridViewTextBoxColumn Status;
    }
}