namespace ProjektSTI
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.tokenBox = new System.Windows.Forms.TextBox();
            this.uzivatelBox = new System.Windows.Forms.TextBox();
            this.repozitarBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tokenBox
            // 
            this.tokenBox.ForeColor = System.Drawing.Color.LightGray;
            this.tokenBox.Location = new System.Drawing.Point(12, 64);
            this.tokenBox.Name = "tokenBox";
            this.tokenBox.Size = new System.Drawing.Size(285, 20);
            this.tokenBox.TabIndex = 4;
            this.tokenBox.Text = "Github Token";
            // 
            // uzivatelBox
            // 
            this.uzivatelBox.ForeColor = System.Drawing.Color.LightGray;
            this.uzivatelBox.Location = new System.Drawing.Point(12, 12);
            this.uzivatelBox.Name = "uzivatelBox";
            this.uzivatelBox.Size = new System.Drawing.Size(285, 20);
            this.uzivatelBox.TabIndex = 5;
            this.uzivatelBox.Text = "Jméno uživatele/skupiny ";
            // 
            // repozitarBox
            // 
            this.repozitarBox.ForeColor = System.Drawing.Color.LightGray;
            this.repozitarBox.Location = new System.Drawing.Point(12, 38);
            this.repozitarBox.Name = "repozitarBox";
            this.repozitarBox.Size = new System.Drawing.Size(285, 20);
            this.repozitarBox.TabIndex = 6;
            this.repozitarBox.Text = "Soubor pro sledování";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(97, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Nastavit sledování";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(305, 115);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.repozitarBox);
            this.Controls.Add(this.uzivatelBox);
            this.Controls.Add(this.tokenBox);
            this.Name = "LoginForm";
            this.Text = "Nastavení ";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tokenBox;
        private System.Windows.Forms.TextBox uzivatelBox;
        private System.Windows.Forms.TextBox repozitarBox;
        private System.Windows.Forms.Button button1;
    }
}