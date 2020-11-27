namespace GH
{
    partial class Parametre
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Parametre));
            this.TbDossierMedia = new System.Windows.Forms.TextBox();
            this.TbDossierHTML = new System.Windows.Forms.TextBox();
            this.lbDossierHTML = new System.Windows.Forms.Label();
            this.lbDossierMedia = new System.Windows.Forms.Label();
            this.CbVoirID = new System.Windows.Forms.CheckBox();
            this.CbVoirPhotoPrincipal = new System.Windows.Forms.CheckBox();
            this.CbVoirMedia = new System.Windows.Forms.CheckBox();
            this.CbVoirDateChangement = new System.Windows.Forms.CheckBox();
            this.CbBalise = new System.Windows.Forms.CheckBox();
            this.CbDateLongue = new System.Windows.Forms.CheckBox();
            this.CbVoirNote = new System.Windows.Forms.CheckBox();
            this.CbVoirReference = new System.Windows.Forms.CheckBox();
            this.CbDeboguer = new System.Windows.Forms.CheckBox();
            this.CbVoirChercheur = new System.Windows.Forms.CheckBox();
            this.CbVoirCarte = new System.Windows.Forms.CheckBox();
            this.Btn_Fermer = new System.Windows.Forms.Button();
            this.Btn_dossierHTML = new System.Windows.Forms.Button();
            this.Btn_dossierMedia = new System.Windows.Forms.Button();
            this.CbToolTip = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // TbDossierMedia
            // 
            this.TbDossierMedia.BackColor = System.Drawing.Color.White;
            this.TbDossierMedia.Location = new System.Drawing.Point(89, 67);
            this.TbDossierMedia.Name = "TbDossierMedia";
            this.TbDossierMedia.Size = new System.Drawing.Size(257, 20);
            this.TbDossierMedia.TabIndex = 2;
            this.TbDossierMedia.TextChanged += new System.EventHandler(this.TbDossierMedia_TextChanged);
            // 
            // TbDossierHTML
            // 
            this.TbDossierHTML.BackColor = System.Drawing.Color.White;
            this.TbDossierHTML.Location = new System.Drawing.Point(89, 29);
            this.TbDossierHTML.Name = "TbDossierHTML";
            this.TbDossierHTML.Size = new System.Drawing.Size(257, 20);
            this.TbDossierHTML.TabIndex = 1;
            this.TbDossierHTML.TextChanged += new System.EventHandler(this.TbDossierHTML_TextChanged);
            // 
            // lbDossierHTML
            // 
            this.lbDossierHTML.AutoSize = true;
            this.lbDossierHTML.Location = new System.Drawing.Point(12, 29);
            this.lbDossierHTML.Name = "lbDossierHTML";
            this.lbDossierHTML.Size = new System.Drawing.Size(75, 13);
            this.lbDossierHTML.TabIndex = 109;
            this.lbDossierHTML.Text = "Dossier HTML";
            // 
            // lbDossierMedia
            // 
            this.lbDossierMedia.AutoSize = true;
            this.lbDossierMedia.Location = new System.Drawing.Point(12, 67);
            this.lbDossierMedia.Name = "lbDossierMedia";
            this.lbDossierMedia.Size = new System.Drawing.Size(73, 13);
            this.lbDossierMedia.TabIndex = 110;
            this.lbDossierMedia.Text = "Dossier média";
            // 
            // CbVoirID
            // 
            this.CbVoirID.AutoSize = true;
            this.CbVoirID.Location = new System.Drawing.Point(24, 103);
            this.CbVoirID.Name = "CbVoirID";
            this.CbVoirID.Size = new System.Drawing.Size(58, 17);
            this.CbVoirID.TabIndex = 3;
            this.CbVoirID.Text = "Voir &ID";
            this.CbVoirID.UseVisualStyleBackColor = true;
            this.CbVoirID.CheckedChanged += new System.EventHandler(this.CbVoirID_CheckedChanged);
            // 
            // CbVoirPhotoPrincipal
            // 
            this.CbVoirPhotoPrincipal.AutoSize = true;
            this.CbVoirPhotoPrincipal.Location = new System.Drawing.Point(24, 126);
            this.CbVoirPhotoPrincipal.Name = "CbVoirPhotoPrincipal";
            this.CbVoirPhotoPrincipal.Size = new System.Drawing.Size(116, 17);
            this.CbVoirPhotoPrincipal.TabIndex = 4;
            this.CbVoirPhotoPrincipal.Text = "Voir &photo principal";
            this.CbVoirPhotoPrincipal.UseVisualStyleBackColor = true;
            this.CbVoirPhotoPrincipal.CheckedChanged += new System.EventHandler(this.CbVoirPphotoPrincipal_CheckedChanged);
            // 
            // CbVoirMedia
            // 
            this.CbVoirMedia.AutoSize = true;
            this.CbVoirMedia.Location = new System.Drawing.Point(24, 149);
            this.CbVoirMedia.Name = "CbVoirMedia";
            this.CbVoirMedia.Size = new System.Drawing.Size(75, 17);
            this.CbVoirMedia.TabIndex = 5;
            this.CbVoirMedia.Text = "Voir &média";
            this.CbVoirMedia.UseVisualStyleBackColor = true;
            this.CbVoirMedia.CheckedChanged += new System.EventHandler(this.CbVoirMedia_CheckedChanged);
            // 
            // CbVoirDateChangement
            // 
            this.CbVoirDateChangement.AutoSize = true;
            this.CbVoirDateChangement.Location = new System.Drawing.Point(24, 195);
            this.CbVoirDateChangement.Name = "CbVoirDateChangement";
            this.CbVoirDateChangement.Size = new System.Drawing.Size(145, 17);
            this.CbVoirDateChangement.TabIndex = 7;
            this.CbVoirDateChangement.Text = "Voir date de &changement";
            this.CbVoirDateChangement.UseVisualStyleBackColor = true;
            this.CbVoirDateChangement.CheckedChanged += new System.EventHandler(this.CbVoirDateChangement_CheckedChanged);
            // 
            // CbBalise
            // 
            this.CbBalise.AutoSize = true;
            this.CbBalise.Location = new System.Drawing.Point(193, 172);
            this.CbBalise.Name = "CbBalise";
            this.CbBalise.Size = new System.Drawing.Size(54, 17);
            this.CbBalise.TabIndex = 12;
            this.CbBalise.Text = "&Balise";
            this.CbBalise.UseVisualStyleBackColor = true;
            this.CbBalise.CheckedChanged += new System.EventHandler(this.CbBalise_CheckedChanged);
            // 
            // CbDateLongue
            // 
            this.CbDateLongue.AutoSize = true;
            this.CbDateLongue.Location = new System.Drawing.Point(24, 172);
            this.CbDateLongue.Name = "CbDateLongue";
            this.CbDateLongue.Size = new System.Drawing.Size(84, 17);
            this.CbDateLongue.TabIndex = 6;
            this.CbDateLongue.Text = "&Date longue";
            this.CbDateLongue.UseVisualStyleBackColor = true;
            this.CbDateLongue.CheckedChanged += new System.EventHandler(this.CbDateLongue_CheckedChanged);
            // 
            // CbVoirNote
            // 
            this.CbVoirNote.AutoSize = true;
            this.CbVoirNote.Location = new System.Drawing.Point(193, 126);
            this.CbVoirNote.Name = "CbVoirNote";
            this.CbVoirNote.Size = new System.Drawing.Size(68, 17);
            this.CbVoirNote.TabIndex = 10;
            this.CbVoirNote.Text = "Voir &note";
            this.CbVoirNote.UseVisualStyleBackColor = true;
            this.CbVoirNote.CheckedChanged += new System.EventHandler(this.CbVoirNote_CheckedChanged);
            // 
            // CbVoirReference
            // 
            this.CbVoirReference.AutoSize = true;
            this.CbVoirReference.Location = new System.Drawing.Point(193, 103);
            this.CbVoirReference.Name = "CbVoirReference";
            this.CbVoirReference.Size = new System.Drawing.Size(92, 17);
            this.CbVoirReference.TabIndex = 9;
            this.CbVoirReference.Text = "Voir &reference";
            this.CbVoirReference.UseVisualStyleBackColor = true;
            this.CbVoirReference.CheckedChanged += new System.EventHandler(this.CbVoirReference_CheckedChanged);
            // 
            // CbDeboguer
            // 
            this.CbDeboguer.AutoSize = true;
            this.CbDeboguer.Location = new System.Drawing.Point(193, 198);
            this.CbDeboguer.Name = "CbDeboguer";
            this.CbDeboguer.Size = new System.Drawing.Size(73, 17);
            this.CbDeboguer.TabIndex = 13;
            this.CbDeboguer.Text = "D&éboguer";
            this.CbDeboguer.UseVisualStyleBackColor = true;
            this.CbDeboguer.CheckedChanged += new System.EventHandler(this.CbDeboguer_CheckedChanged);
            // 
            // CbVoirChercheur
            // 
            this.CbVoirChercheur.AutoSize = true;
            this.CbVoirChercheur.Location = new System.Drawing.Point(24, 219);
            this.CbVoirChercheur.Name = "CbVoirChercheur";
            this.CbVoirChercheur.Size = new System.Drawing.Size(95, 17);
            this.CbVoirChercheur.TabIndex = 8;
            this.CbVoirChercheur.Text = "Voir c&hercheur";
            this.CbVoirChercheur.UseVisualStyleBackColor = true;
            this.CbVoirChercheur.CheckedChanged += new System.EventHandler(this.CbVoirChercheur_CheckedChanged);
            // 
            // CbVoirCarte
            // 
            this.CbVoirCarte.AutoSize = true;
            this.CbVoirCarte.Location = new System.Drawing.Point(193, 149);
            this.CbVoirCarte.Name = "CbVoirCarte";
            this.CbVoirCarte.Size = new System.Drawing.Size(139, 17);
            this.CbVoirCarte.TabIndex = 11;
            this.CbVoirCarte.Text = "Voir carte &géographique";
            this.CbVoirCarte.UseVisualStyleBackColor = true;
            this.CbVoirCarte.CheckedChanged += new System.EventHandler(this.CbVoirCarte_CheckedChanged);
            // 
            // Btn_Fermer
            // 
            this.Btn_Fermer.BackColor = System.Drawing.Color.Blue;
            this.Btn_Fermer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Fermer.FlatAppearance.BorderSize = 0;
            this.Btn_Fermer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Btn_Fermer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Fermer.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Bold);
            this.Btn_Fermer.ForeColor = System.Drawing.Color.White;
            this.Btn_Fermer.Location = new System.Drawing.Point(317, 202);
            this.Btn_Fermer.Name = "Btn_Fermer";
            this.Btn_Fermer.Size = new System.Drawing.Size(75, 35);
            this.Btn_Fermer.TabIndex = 136;
            this.Btn_Fermer.Text = "Fermer";
            this.Btn_Fermer.UseVisualStyleBackColor = false;
            this.Btn_Fermer.Click += new System.EventHandler(this.Btn_Fermer_Click);
            // 
            // Btn_dossierHTML
            // 
            this.Btn_dossierHTML.BackColor = System.Drawing.Color.Blue;
            this.Btn_dossierHTML.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_dossierHTML.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.Btn_dossierHTML.FlatAppearance.BorderSize = 0;
            this.Btn_dossierHTML.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.Btn_dossierHTML.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Btn_dossierHTML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_dossierHTML.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Bold);
            this.Btn_dossierHTML.ForeColor = System.Drawing.Color.White;
            this.Btn_dossierHTML.Image = global::GH.Properties.Resources.dossier_20;
            this.Btn_dossierHTML.Location = new System.Drawing.Point(352, 25);
            this.Btn_dossierHTML.Name = "Btn_dossierHTML";
            this.Btn_dossierHTML.Size = new System.Drawing.Size(39, 30);
            this.Btn_dossierHTML.TabIndex = 137;
            this.Btn_dossierHTML.UseVisualStyleBackColor = false;
            this.Btn_dossierHTML.Click += new System.EventHandler(this.Btn_dossierHTML_Click);
            this.Btn_dossierHTML.MouseHover += new System.EventHandler(this.Btn_dossierHTML_MouseHover);
            // 
            // Btn_dossierMedia
            // 
            this.Btn_dossierMedia.BackColor = System.Drawing.Color.Blue;
            this.Btn_dossierMedia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_dossierMedia.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.Btn_dossierMedia.FlatAppearance.BorderSize = 0;
            this.Btn_dossierMedia.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.Btn_dossierMedia.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Btn_dossierMedia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_dossierMedia.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Bold);
            this.Btn_dossierMedia.ForeColor = System.Drawing.Color.White;
            this.Btn_dossierMedia.Image = global::GH.Properties.Resources.dossier_20;
            this.Btn_dossierMedia.Location = new System.Drawing.Point(352, 61);
            this.Btn_dossierMedia.Name = "Btn_dossierMedia";
            this.Btn_dossierMedia.Size = new System.Drawing.Size(39, 30);
            this.Btn_dossierMedia.TabIndex = 138;
            this.Btn_dossierMedia.UseVisualStyleBackColor = false;
            this.Btn_dossierMedia.Click += new System.EventHandler(this.Btn_dossierMedia_Click);
            this.Btn_dossierMedia.MouseHover += new System.EventHandler(this.Btn_dossierMedia_MouseHover);
            // 
            // CbToolTip
            // 
            this.CbToolTip.AutoSize = true;
            this.CbToolTip.Location = new System.Drawing.Point(193, 219);
            this.CbToolTip.Name = "CbToolTip";
            this.CbToolTip.Size = new System.Drawing.Size(69, 17);
            this.CbToolTip.TabIndex = 139;
            this.CbToolTip.Text = "Inf&o-bulle";
            this.CbToolTip.UseVisualStyleBackColor = true;
            this.CbToolTip.CheckedChanged += new System.EventHandler(this.CbToolTip_CheckedChanged);
            // 
            // Parametre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(397, 244);
            this.Controls.Add(this.CbToolTip);
            this.Controls.Add(this.Btn_dossierMedia);
            this.Controls.Add(this.Btn_dossierHTML);
            this.Controls.Add(this.Btn_Fermer);
            this.Controls.Add(this.CbVoirCarte);
            this.Controls.Add(this.CbVoirChercheur);
            this.Controls.Add(this.CbDeboguer);
            this.Controls.Add(this.CbVoirReference);
            this.Controls.Add(this.CbVoirNote);
            this.Controls.Add(this.CbDateLongue);
            this.Controls.Add(this.CbBalise);
            this.Controls.Add(this.CbVoirDateChangement);
            this.Controls.Add(this.CbVoirMedia);
            this.Controls.Add(this.CbVoirPhotoPrincipal);
            this.Controls.Add(this.CbVoirID);
            this.Controls.Add(this.lbDossierMedia);
            this.Controls.Add(this.lbDossierHTML);
            this.Controls.Add(this.TbDossierHTML);
            this.Controls.Add(this.TbDossierMedia);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Parametre";
            this.Text = "Paramètres";
            this.Load += new System.EventHandler(this.Parametre_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TbDossierMedia;
        private System.Windows.Forms.TextBox TbDossierHTML;
        private System.Windows.Forms.Label lbDossierHTML;
        private System.Windows.Forms.Label lbDossierMedia;
        private System.Windows.Forms.CheckBox CbVoirID;
        private System.Windows.Forms.CheckBox CbVoirPhotoPrincipal;
        private System.Windows.Forms.CheckBox CbVoirMedia;
        private System.Windows.Forms.CheckBox CbVoirDateChangement;
        private System.Windows.Forms.CheckBox CbBalise;
        private System.Windows.Forms.CheckBox CbDateLongue;
        private System.Windows.Forms.CheckBox CbVoirNote;
        private System.Windows.Forms.CheckBox CbVoirReference;
        private System.Windows.Forms.CheckBox CbDeboguer;
        private System.Windows.Forms.CheckBox CbVoirChercheur;
        private System.Windows.Forms.CheckBox CbVoirCarte;
        private System.Windows.Forms.Button Btn_Fermer;
        private System.Windows.Forms.Button Btn_dossierHTML;
        private System.Windows.Forms.Button Btn_dossierMedia;
        private System.Windows.Forms.CheckBox CbToolTip;
    }
}