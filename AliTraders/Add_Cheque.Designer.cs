namespace AhmadSanitary
{
    partial class Add_Cheque
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
            this.txtamtaddcheque = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtbnknameaddcheque = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnaddaddcheque = new System.Windows.Forms.Button();
            this.txtchnoaddcheque = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtcnameaddcheque = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtamtaddcheque
            // 
            this.txtamtaddcheque.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtamtaddcheque.Location = new System.Drawing.Point(161, 103);
            this.txtamtaddcheque.Name = "txtamtaddcheque";
            this.txtamtaddcheque.ReadOnly = true;
            this.txtamtaddcheque.Size = new System.Drawing.Size(210, 23);
            this.txtamtaddcheque.TabIndex = 3;
            this.txtamtaddcheque.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(76, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 23);
            this.label1.TabIndex = 89;
            this.label1.Text = "Amount:";
            // 
            // txtbnknameaddcheque
            // 
            this.txtbnknameaddcheque.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbnknameaddcheque.Location = new System.Drawing.Point(161, 74);
            this.txtbnknameaddcheque.MaxLength = 16;
            this.txtbnknameaddcheque.Name = "txtbnknameaddcheque";
            this.txtbnknameaddcheque.Size = new System.Drawing.Size(210, 23);
            this.txtbnknameaddcheque.TabIndex = 1;
            this.txtbnknameaddcheque.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtbnknameaddcheque_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(50, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 23);
            this.label5.TabIndex = 87;
            this.label5.Text = "Bank Name:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(12, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 23);
            this.label6.TabIndex = 86;
            this.label6.Text = "Customer Name:";
            // 
            // btnaddaddcheque
            // 
            this.btnaddaddcheque.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(134)))), ((int)(((byte)(178)))));
            this.btnaddaddcheque.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnaddaddcheque.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnaddaddcheque.ForeColor = System.Drawing.Color.White;
            this.btnaddaddcheque.Location = new System.Drawing.Point(311, 133);
            this.btnaddaddcheque.Name = "btnaddaddcheque";
            this.btnaddaddcheque.Size = new System.Drawing.Size(60, 24);
            this.btnaddaddcheque.TabIndex = 2;
            this.btnaddaddcheque.Text = "Add";
            this.btnaddaddcheque.UseVisualStyleBackColor = false;
            this.btnaddaddcheque.Click += new System.EventHandler(this.btnaddaddcheque_Click);
            // 
            // txtchnoaddcheque
            // 
            this.txtchnoaddcheque.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtchnoaddcheque.Location = new System.Drawing.Point(161, 16);
            this.txtchnoaddcheque.MaxLength = 16;
            this.txtchnoaddcheque.Name = "txtchnoaddcheque";
            this.txtchnoaddcheque.Size = new System.Drawing.Size(210, 23);
            this.txtchnoaddcheque.TabIndex = 0;
            this.txtchnoaddcheque.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtchnoaddcheque_KeyPress);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(53, 16);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(102, 23);
            this.label16.TabIndex = 85;
            this.label16.Text = "Cheque No:";
            // 
            // txtcnameaddcheque
            // 
            this.txtcnameaddcheque.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcnameaddcheque.Location = new System.Drawing.Point(161, 45);
            this.txtcnameaddcheque.Name = "txtcnameaddcheque";
            this.txtcnameaddcheque.ReadOnly = true;
            this.txtcnameaddcheque.Size = new System.Drawing.Size(210, 23);
            this.txtcnameaddcheque.TabIndex = 1;
            this.txtcnameaddcheque.TabStop = false;
            // 
            // Add_Cheque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(403, 169);
            this.Controls.Add(this.txtcnameaddcheque);
            this.Controls.Add(this.txtamtaddcheque);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtbnknameaddcheque);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnaddaddcheque);
            this.Controls.Add(this.txtchnoaddcheque);
            this.Controls.Add(this.label16);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Add_Cheque";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Cheque";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Add_Cheque_FormClosing);
            this.Load += new System.EventHandler(this.Add_Cheque_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtamtaddcheque;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbnknameaddcheque;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnaddaddcheque;
        private System.Windows.Forms.TextBox txtchnoaddcheque;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtcnameaddcheque;
    }
}