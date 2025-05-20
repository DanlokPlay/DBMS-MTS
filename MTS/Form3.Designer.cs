namespace MTS
{
    partial class CallDetailsForm
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
            this.maskedTextBoxPhone = new System.Windows.Forms.MaskedTextBox();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.buttonSubmitRequest = new System.Windows.Forms.Button();
            this.listBoxSuggestions = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // maskedTextBoxPhone
            // 
            this.maskedTextBoxPhone.Location = new System.Drawing.Point(12, 26);
            this.maskedTextBoxPhone.Mask = "8 (000) 000-00-00";
            this.maskedTextBoxPhone.Name = "maskedTextBoxPhone";
            this.maskedTextBoxPhone.Size = new System.Drawing.Size(152, 20);
            this.maskedTextBoxPhone.TabIndex = 0;
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.Location = new System.Drawing.Point(200, 26);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerStart.TabIndex = 1;
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.Location = new System.Drawing.Point(434, 26);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerEnd.TabIndex = 2;
            // 
            // buttonSubmitRequest
            // 
            this.buttonSubmitRequest.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.buttonSubmitRequest.Location = new System.Drawing.Point(659, 24);
            this.buttonSubmitRequest.Name = "buttonSubmitRequest";
            this.buttonSubmitRequest.Size = new System.Drawing.Size(151, 29);
            this.buttonSubmitRequest.TabIndex = 3;
            this.buttonSubmitRequest.Text = "Отправить запрос";
            this.buttonSubmitRequest.UseVisualStyleBackColor = true;
            // 
            // listBoxSuggestions
            // 
            this.listBoxSuggestions.FormattingEnabled = true;
            this.listBoxSuggestions.Location = new System.Drawing.Point(12, 63);
            this.listBoxSuggestions.Name = "listBoxSuggestions";
            this.listBoxSuggestions.Size = new System.Drawing.Size(152, 173);
            this.listBoxSuggestions.TabIndex = 4;
            this.listBoxSuggestions.Visible = false;
            // 
            // CallDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 348);
            this.Controls.Add(this.listBoxSuggestions);
            this.Controls.Add(this.buttonSubmitRequest);
            this.Controls.Add(this.dateTimePickerEnd);
            this.Controls.Add(this.dateTimePickerStart);
            this.Controls.Add(this.maskedTextBoxPhone);
            this.Name = "CallDetailsForm";
            this.Text = "Заказ Детализации Звонков";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox maskedTextBoxPhone;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.Button buttonSubmitRequest;
        private System.Windows.Forms.ListBox listBoxSuggestions;
    }
}