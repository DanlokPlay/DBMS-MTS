namespace MTS
{
    partial class MTS
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.dataGridViewTariff = new System.Windows.Forms.DataGridView();
            this.dataGridViewClient = new System.Windows.Forms.DataGridView();
            this.dataGridViewCalls = new System.Windows.Forms.DataGridView();
            this.dataGridViewCallDetailRequest = new System.Windows.Forms.DataGridView();
            this.buttonAddClient = new System.Windows.Forms.Button();
            this.buttonCallDetails = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTariff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCalls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCallDetailRequest)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.buttonRefresh.Location = new System.Drawing.Point(12, 380);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(140, 90);
            this.buttonRefresh.TabIndex = 0;
            this.buttonRefresh.Text = "Обновить";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTariff
            // 
            this.dataGridViewTariff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTariff.Location = new System.Drawing.Point(2, 2);
            this.dataGridViewTariff.Name = "dataGridViewTariff";
            this.dataGridViewTariff.Size = new System.Drawing.Size(364, 359);
            this.dataGridViewTariff.TabIndex = 1;
            // 
            // dataGridViewClient
            // 
            this.dataGridViewClient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewClient.Location = new System.Drawing.Point(372, 2);
            this.dataGridViewClient.Name = "dataGridViewClient";
            this.dataGridViewClient.Size = new System.Drawing.Size(364, 359);
            this.dataGridViewClient.TabIndex = 2;
            // 
            // dataGridViewCalls
            // 
            this.dataGridViewCalls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCalls.Location = new System.Drawing.Point(742, 2);
            this.dataGridViewCalls.Name = "dataGridViewCalls";
            this.dataGridViewCalls.Size = new System.Drawing.Size(364, 359);
            this.dataGridViewCalls.TabIndex = 3;
            // 
            // dataGridViewCallDetailRequest
            // 
            this.dataGridViewCallDetailRequest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCallDetailRequest.Location = new System.Drawing.Point(1114, 2);
            this.dataGridViewCallDetailRequest.Name = "dataGridViewCallDetailRequest";
            this.dataGridViewCallDetailRequest.Size = new System.Drawing.Size(364, 359);
            this.dataGridViewCallDetailRequest.TabIndex = 4;
            // 
            // buttonAddClient
            // 
            this.buttonAddClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.buttonAddClient.Location = new System.Drawing.Point(170, 380);
            this.buttonAddClient.Name = "buttonAddClient";
            this.buttonAddClient.Size = new System.Drawing.Size(196, 90);
            this.buttonAddClient.TabIndex = 5;
            this.buttonAddClient.Text = "Добавить пользователя";
            this.buttonAddClient.UseVisualStyleBackColor = true;
            // 
            // buttonCallDetails
            // 
            this.buttonCallDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.buttonCallDetails.Location = new System.Drawing.Point(391, 380);
            this.buttonCallDetails.Name = "buttonCallDetails";
            this.buttonCallDetails.Size = new System.Drawing.Size(186, 90);
            this.buttonCallDetails.TabIndex = 6;
            this.buttonCallDetails.Text = "Детализация звонков";
            this.buttonCallDetails.UseVisualStyleBackColor = true;
            // 
            // MTS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1490, 597);
            this.Controls.Add(this.buttonCallDetails);
            this.Controls.Add(this.buttonAddClient);
            this.Controls.Add(this.dataGridViewCallDetailRequest);
            this.Controls.Add(this.dataGridViewCalls);
            this.Controls.Add(this.dataGridViewClient);
            this.Controls.Add(this.dataGridViewTariff);
            this.Controls.Add(this.buttonRefresh);
            this.Name = "MTS";
            this.Text = "MTS";
            this.Load += new System.EventHandler(this.MTS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTariff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCalls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCallDetailRequest)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.DataGridView dataGridViewTariff;
        private System.Windows.Forms.DataGridView dataGridViewClient;
        private System.Windows.Forms.DataGridView dataGridViewCalls;
        private System.Windows.Forms.DataGridView dataGridViewCallDetailRequest;
        private System.Windows.Forms.Button buttonAddClient;
        private System.Windows.Forms.Button buttonCallDetails;
    }
}

