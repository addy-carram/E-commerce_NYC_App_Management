namespace e_commerce_NYC
{
    partial class Form3
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
            this.components = new System.ComponentModel.Container();
            this.component_label1 = new e_commerce_NYC.Models.Component_label(this.components);
            this.userControl11 = new e_commerce_NYC.UserControl1();
            this.userControl12 = new e_commerce_NYC.UserControl1();
            this.userControl13 = new e_commerce_NYC.UserControl1();
            this.userControl14 = new e_commerce_NYC.UserControl1();
            this.SuspendLayout();
            // 
            // userControl11
            // 
            this.userControl11.Location = new System.Drawing.Point(547, 217);
            this.userControl11.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.userControl11.Name = "userControl11";
            this.userControl11.Size = new System.Drawing.Size(496, 258);
            this.userControl11.TabIndex = 0;
            // 
            // userControl12
            // 
            this.userControl12.Location = new System.Drawing.Point(1078, 217);
            this.userControl12.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.userControl12.Name = "userControl12";
            this.userControl12.Size = new System.Drawing.Size(496, 258);
            this.userControl12.TabIndex = 1;
            // 
            // userControl13
            // 
            this.userControl13.Location = new System.Drawing.Point(224, 133);
            this.userControl13.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.userControl13.Name = "userControl13";
            this.userControl13.Size = new System.Drawing.Size(496, 258);
            this.userControl13.TabIndex = 2;
            // 
            // userControl14
            // 
            this.userControl14.Location = new System.Drawing.Point(346, 427);
            this.userControl14.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.userControl14.Name = "userControl14";
            this.userControl14.Size = new System.Drawing.Size(496, 258);
            this.userControl14.TabIndex = 3;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1850, 807);
            this.Controls.Add(this.userControl14);
            this.Controls.Add(this.userControl13);
            this.Controls.Add(this.userControl12);
            this.Controls.Add(this.userControl11);
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);

        }

        #endregion

        private Models.Component_label component_label1;
        private UserControl1 userControl11;
        private UserControl1 userControl12;
        private UserControl1 userControl13;
        private UserControl1 userControl14;
    }
}