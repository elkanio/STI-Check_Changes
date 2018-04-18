namespace ProjektSTI
{
    partial class MainForm
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.RefreshButton = new System.Windows.Forms.Button();
            this.UkazatelCasu = new System.Windows.Forms.Label();
            this.Kontrolka = new System.Windows.Forms.PictureBox();
            this.GrafButton = new System.Windows.Forms.Button();
            this.ExportButton = new System.Windows.Forms.Button();
            this.TabulkaCommitu = new System.Windows.Forms.DataGridView();
            this.nazev = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UlozitButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.info = new System.Windows.Forms.Label();
            this.pocetRadku = new System.Windows.Forms.Label();
            this.pocetCommitu = new System.Windows.Forms.Label();
            this.casCommitu = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Kontrolka)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TabulkaCommitu)).BeginInit();
            this.SuspendLayout();
            // 
            // RefreshButton
            // 
            this.RefreshButton.Enabled = false;
            this.RefreshButton.Location = new System.Drawing.Point(414, 432);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(82, 34);
            this.RefreshButton.TabIndex = 0;
            this.RefreshButton.Text = "Aktualizace";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // UkazatelCasu
            // 
            this.UkazatelCasu.AutoSize = true;
            this.UkazatelCasu.BackColor = System.Drawing.Color.Transparent;
            this.UkazatelCasu.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.UkazatelCasu.Location = new System.Drawing.Point(22, 437);
            this.UkazatelCasu.Name = "UkazatelCasu";
            this.UkazatelCasu.Size = new System.Drawing.Size(10, 13);
            this.UkazatelCasu.TabIndex = 4;
            this.UkazatelCasu.Text = ".";
            this.UkazatelCasu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Kontrolka
            // 
            this.Kontrolka.BackColor = System.Drawing.Color.Red;
            this.Kontrolka.Location = new System.Drawing.Point(-4, 477);
            this.Kontrolka.Name = "Kontrolka";
            this.Kontrolka.Size = new System.Drawing.Size(702, 10);
            this.Kontrolka.TabIndex = 7;
            this.Kontrolka.TabStop = false;
            // 
            // GrafButton
            // 
            this.GrafButton.Enabled = false;
            this.GrafButton.Location = new System.Drawing.Point(161, 431);
            this.GrafButton.Name = "GrafButton";
            this.GrafButton.Size = new System.Drawing.Size(75, 34);
            this.GrafButton.TabIndex = 8;
            this.GrafButton.Text = "Vytvoř graf";
            this.GrafButton.UseVisualStyleBackColor = true;
            this.GrafButton.Click += new System.EventHandler(this.GrafButton_Click);
            // 
            // ExportButton
            // 
            this.ExportButton.Enabled = false;
            this.ExportButton.Location = new System.Drawing.Point(323, 432);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(85, 34);
            this.ExportButton.TabIndex = 12;
            this.ExportButton.Text = "Vytvoř excel";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // TabulkaCommitu
            // 
            this.TabulkaCommitu.AllowUserToAddRows = false;
            this.TabulkaCommitu.AllowUserToDeleteRows = false;
            this.TabulkaCommitu.AllowUserToResizeRows = false;
            this.TabulkaCommitu.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.TabulkaCommitu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TabulkaCommitu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nazev,
            this.datum,
            this.sha});
            this.TabulkaCommitu.Location = new System.Drawing.Point(-4, 57);
            this.TabulkaCommitu.MultiSelect = false;
            this.TabulkaCommitu.Name = "TabulkaCommitu";
            this.TabulkaCommitu.ReadOnly = true;
            this.TabulkaCommitu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TabulkaCommitu.Size = new System.Drawing.Size(688, 369);
            this.TabulkaCommitu.TabIndex = 13;
            this.TabulkaCommitu.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TabulkaCommitu_CellContentClick);
            this.TabulkaCommitu.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.TabulkaCommitu.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView1_RowsRemoved);
            this.TabulkaCommitu.SelectionChanged += new System.EventHandler(this.TabulkaCommitu_SelectionChanged);
            // 
            // nazev
            // 
            this.nazev.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nazev.HeaderText = "Cesta k souboru";
            this.nazev.Name = "nazev";
            this.nazev.ReadOnly = true;
            this.nazev.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.nazev.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // datum
            // 
            this.datum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.datum.HeaderText = "Datum posledního commitu";
            this.datum.Name = "datum";
            this.datum.ReadOnly = true;
            this.datum.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.datum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // sha
            // 
            this.sha.HeaderText = "Sha";
            this.sha.Name = "sha";
            this.sha.ReadOnly = true;
            // 
            // UlozitButton
            // 
            this.UlozitButton.Enabled = false;
            this.UlozitButton.Location = new System.Drawing.Point(242, 432);
            this.UlozitButton.Name = "UlozitButton";
            this.UlozitButton.Size = new System.Drawing.Size(75, 34);
            this.UlozitButton.TabIndex = 14;
            this.UlozitButton.Text = "Ulož soubor";
            this.UlozitButton.UseVisualStyleBackColor = true;
            this.UlozitButton.Click += new System.EventHandler(this.UlozitButon_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 15;
            // 
            // info
            // 
            this.info.AutoSize = true;
            this.info.BackColor = System.Drawing.Color.Transparent;
            this.info.CausesValidation = false;
            this.info.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.info.Location = new System.Drawing.Point(337, 28);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(10, 13);
            this.info.TabIndex = 16;
            this.info.Text = ".";
            // 
            // pocetRadku
            // 
            this.pocetRadku.AutoSize = true;
            this.pocetRadku.BackColor = System.Drawing.Color.Transparent;
            this.pocetRadku.CausesValidation = false;
            this.pocetRadku.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pocetRadku.Location = new System.Drawing.Point(12, 41);
            this.pocetRadku.Name = "pocetRadku";
            this.pocetRadku.Size = new System.Drawing.Size(109, 13);
            this.pocetRadku.TabIndex = 17;
            this.pocetRadku.Text = "Celkový počet řádků:";
            // 
            // pocetCommitu
            // 
            this.pocetCommitu.AutoSize = true;
            this.pocetCommitu.BackColor = System.Drawing.Color.Transparent;
            this.pocetCommitu.CausesValidation = false;
            this.pocetCommitu.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pocetCommitu.Location = new System.Drawing.Point(12, 28);
            this.pocetCommitu.Name = "pocetCommitu";
            this.pocetCommitu.Size = new System.Drawing.Size(120, 13);
            this.pocetCommitu.TabIndex = 18;
            this.pocetCommitu.Text = "Celkový počet commitů:";
            // 
            // casCommitu
            // 
            this.casCommitu.AutoSize = true;
            this.casCommitu.BackColor = System.Drawing.Color.Transparent;
            this.casCommitu.CausesValidation = false;
            this.casCommitu.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.casCommitu.Location = new System.Drawing.Point(13, 9);
            this.casCommitu.Name = "casCommitu";
            this.casCommitu.Size = new System.Drawing.Size(90, 13);
            this.casCommitu.TabIndex = 19;
            this.casCommitu.Text = "Poslední refresh: ";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(683, 483);
            this.Controls.Add(this.casCommitu);
            this.Controls.Add(this.pocetCommitu);
            this.Controls.Add(this.pocetRadku);
            this.Controls.Add(this.info);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UlozitButton);
            this.Controls.Add(this.TabulkaCommitu);
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(this.GrafButton);
            this.Controls.Add(this.Kontrolka);
            this.Controls.Add(this.UkazatelCasu);
            this.Controls.Add(this.RefreshButton);
            this.Name = "MainForm";
            this.Text = "Changes Checker";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Kontrolka)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TabulkaCommitu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Label UkazatelCasu;
        private System.Windows.Forms.PictureBox Kontrolka;
        private System.Windows.Forms.Button GrafButton;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.DataGridView TabulkaCommitu;
        private System.Windows.Forms.Button UlozitButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label info;
        private System.Windows.Forms.Label pocetRadku;
        private System.Windows.Forms.Label pocetCommitu;
        private System.Windows.Forms.Label casCommitu;
        private System.Windows.Forms.DataGridViewTextBoxColumn nazev;
        private System.Windows.Forms.DataGridViewTextBoxColumn datum;
        private System.Windows.Forms.DataGridViewTextBoxColumn sha;
    }
}

