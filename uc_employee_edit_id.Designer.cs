namespace e_commerce_NYC
{
    partial class uc_employee_edit_id
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
            this.delete = new System.Windows.Forms.Button();
            this.t_delete = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // delete
            // 
            this.delete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.delete.Location = new System.Drawing.Point(244, 265);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(313, 88);
            this.delete.TabIndex = 5;
            this.delete.Text = "Edit";
            this.delete.UseVisualStyleBackColor = false;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // t_delete
            // 
            this.t_delete.Location = new System.Drawing.Point(341, 170);
            this.t_delete.Name = "t_delete";
            this.t_delete.Size = new System.Drawing.Size(100, 31);
            this.t_delete.TabIndex = 4;
            this.t_delete.TextChanged += new System.EventHandler(this.t_delete_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(324, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Delete by id";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // uc_employee_edit_id
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.t_delete);
            this.Controls.Add(this.label1);
            this.Name = "uc_employee_edit_id";
            this.Text = "uc_employee_edit_id";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.TextBox t_delete;
        private System.Windows.Forms.Label label1;
    }
}