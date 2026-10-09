namespace BillingSystem
{
    partial class frmAnalytics
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
            lblTotalCustomers = new Label();
            lblTotalRevenue = new Label();
            lblTotalUnpaid = new Label();
            plotMonthlyRevenue = new ScottPlot.WinForms.FormsPlot();
            plotPaidUnpaid = new ScottPlot.WinForms.FormsPlot();
            lblTop5Title = new Label();
            dgvTop5 = new DataGridView();
            FullName = new DataGridViewTextBoxColumn();
            TotalConsumption = new DataGridViewTextBoxColumn();
            TotalBilled = new DataGridViewTextBoxColumn();
            btnCloseAnalytics = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvTop5).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Bookman Old Style", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(579, 13);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(258, 28);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Analytics Dashboard";
            lblTitle.Click += label1_Click;
            // 
            // lblTotalCustomers
            // 
            lblTotalCustomers.AutoSize = true;
            lblTotalCustomers.Font = new Font("Baskerville Old Face", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTotalCustomers.Location = new Point(366, 61);
            lblTotalCustomers.Name = "lblTotalCustomers";
            lblTotalCustomers.Size = new Size(138, 19);
            lblTotalCustomers.TabIndex = 1;
            lblTotalCustomers.Text = "Total Customers: 0";
            lblTotalCustomers.Click += lblTotalCustomers_Click;
            // 
            // lblTotalRevenue
            // 
            lblTotalRevenue.AutoSize = true;
            lblTotalRevenue.Font = new Font("Baskerville Old Face", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTotalRevenue.Location = new Point(635, 61);
            lblTotalRevenue.Name = "lblTotalRevenue";
            lblTotalRevenue.Size = new Size(153, 19);
            lblTotalRevenue.TabIndex = 2;
            lblTotalRevenue.Text = "Total Revenue: ₱0.00";
            // 
            // lblTotalUnpaid
            // 
            lblTotalUnpaid.AutoSize = true;
            lblTotalUnpaid.Font = new Font("Baskerville Old Face", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTotalUnpaid.Location = new Point(937, 61);
            lblTotalUnpaid.Name = "lblTotalUnpaid";
            lblTotalUnpaid.Size = new Size(144, 19);
            lblTotalUnpaid.TabIndex = 3;
            lblTotalUnpaid.Text = "Total Unpaid: ₱0.00";
            // 
            // plotMonthlyRevenue
            // 
            plotMonthlyRevenue.Location = new Point(82, 112);
            plotMonthlyRevenue.Name = "plotMonthlyRevenue";
            plotMonthlyRevenue.Size = new Size(624, 279);
            plotMonthlyRevenue.TabIndex = 4;
            // 
            // plotPaidUnpaid
            // 
            plotPaidUnpaid.Font = new Font("Baskerville Old Face", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            plotPaidUnpaid.Location = new Point(739, 114);
            plotPaidUnpaid.Name = "plotPaidUnpaid";
            plotPaidUnpaid.Size = new Size(596, 277);
            plotPaidUnpaid.TabIndex = 5;
            // 
            // lblTop5Title
            // 
            lblTop5Title.AutoSize = true;
            lblTop5Title.Font = new Font("Baskerville Old Face", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTop5Title.Location = new Point(176, 408);
            lblTop5Title.Name = "lblTop5Title";
            lblTop5Title.Size = new Size(245, 19);
            lblTop5Title.TabIndex = 6;
            lblTop5Title.Text = "Top 5 Customers by Consumption";
            // 
            // dgvTop5
            // 
            dgvTop5.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTop5.Columns.AddRange(new DataGridViewColumn[] { FullName, TotalConsumption, TotalBilled });
            dgvTop5.Location = new Point(176, 436);
            dgvTop5.Name = "dgvTop5";
            dgvTop5.RowHeadersWidth = 51;
            dgvTop5.Size = new Size(1103, 193);
            dgvTop5.TabIndex = 7;
            dgvTop5.CellContentClick += dgvTop5_CellContentClick;
            // 
            // FullName
            // 
            FullName.HeaderText = "FullName";
            FullName.MinimumWidth = 10;
            FullName.Name = "FullName";
            FullName.Width = 350;
            // 
            // TotalConsumption
            // 
            TotalConsumption.HeaderText = "TotalConsumption";
            TotalConsumption.MinimumWidth = 10;
            TotalConsumption.Name = "TotalConsumption";
            TotalConsumption.Width = 350;
            // 
            // TotalBilled
            // 
            TotalBilled.HeaderText = "TotalBilled";
            TotalBilled.MinimumWidth = 10;
            TotalBilled.Name = "TotalBilled";
            TotalBilled.Width = 350;
            // 
            // btnCloseAnalytics
            // 
            btnCloseAnalytics.Font = new Font("Baskerville Old Face", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCloseAnalytics.Location = new Point(1185, 639);
            btnCloseAnalytics.Name = "btnCloseAnalytics";
            btnCloseAnalytics.Size = new Size(94, 29);
            btnCloseAnalytics.TabIndex = 8;
            btnCloseAnalytics.Text = "Close";
            btnCloseAnalytics.UseVisualStyleBackColor = true;
            btnCloseAnalytics.Click += btnCloseAnalytics_Click;
            // 
            // frmAnalytics
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1392, 683);
            Controls.Add(btnCloseAnalytics);
            Controls.Add(dgvTop5);
            Controls.Add(lblTop5Title);
            Controls.Add(plotPaidUnpaid);
            Controls.Add(plotMonthlyRevenue);
            Controls.Add(lblTotalUnpaid);
            Controls.Add(lblTotalRevenue);
            Controls.Add(lblTotalCustomers);
            Controls.Add(lblTitle);
            Name = "frmAnalytics";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Billing System - Analytics Dashboard ";
            Load += frmAnalytics_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTop5).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblTotalCustomers;
        private Label lblTotalRevenue;
        private Label lblTotalUnpaid;
        private ScottPlot.WinForms.FormsPlot plotMonthlyRevenue;
        private ScottPlot.WinForms.FormsPlot plotPaidUnpaid;
        private Label lblTop5Title;
        private DataGridView dgvTop5;
        private Button btnCloseAnalytics;
        private DataGridViewTextBoxColumn FullName;
        private DataGridViewTextBoxColumn TotalConsumption;
        private DataGridViewTextBoxColumn TotalBilled;
    }
}