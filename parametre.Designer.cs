namespace GH
{
    partial class ParaClass
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParaClass));
            this.TbDossierMedia = new System.Windows.Forms.TextBox();
            this.Tb_dossier_page = new System.Windows.Forms.TextBox();
            this.Cb_voir_ID = new System.Windows.Forms.CheckBox();
            this.CbVoirMedia = new System.Windows.Forms.CheckBox();
            this.CbVoirDateChangement = new System.Windows.Forms.CheckBox();
            this.CbDateLongue = new System.Windows.Forms.CheckBox();
            this.CbVoirNote = new System.Windows.Forms.CheckBox();
            this.CbVoirReference = new System.Windows.Forms.CheckBox();
            this.CbVoirChercheur = new System.Windows.Forms.CheckBox();
            this.CbVoirCarte = new System.Windows.Forms.CheckBox();
            this.CbVoirInfoBulle = new System.Windows.Forms.CheckBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.Lb_code_arriere_plan = new System.Windows.Forms.Label();
            this.Lb_couleur_arriere_plan = new System.Windows.Forms.Label();
            this.Lb_Confidentiel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.Tb_tout_evenement_ans = new System.Windows.Forms.TextBox();
            this.Cb_tout_evenement_date = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.Tb_union_ans = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Cb_union_date = new System.Windows.Forms.CheckBox();
            this.Cb_deces_date = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Tb_deces_ans = new System.Windows.Forms.TextBox();
            this.Tb_naissance_ans = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Cb_naissance_date = new System.Windows.Forms.CheckBox();
            this.Cb_inhumation_date = new System.Windows.Forms.CheckBox();
            this.Tb_inhumation_ans = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Cb_autre_date = new System.Windows.Forms.CheckBox();
            this.Cb_citoyen_date = new System.Windows.Forms.CheckBox();
            this.Tb_citoyen_ans = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.Cb_testament_information = new System.Windows.Forms.CheckBox();
            this.Tb_autre_ans = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Tb_testament_ans = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.Cb_ordonnance_date = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Tb_ordonnance_ans = new System.Windows.Forms.TextBox();
            this.Cb_bapteme_date = new System.Windows.Forms.CheckBox();
            this.Tb_bapteme_ans = new System.Windows.Forms.TextBox();
            this.Lb_bapteme_ans = new System.Windows.Forms.Label();
            this.Cb_mode_depanage = new System.Windows.Forms.CheckBox();
            this.menuPrincipal = new System.Windows.Forms.MenuStrip();
            this.AideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Cb_tout_evenement = new System.Windows.Forms.CheckBox();
            this.Cb_certain = new System.Windows.Forms.CheckBox();
            this.Cb_enregistrer_balise = new System.Windows.Forms.CheckBox();
            this.Btn_annuler = new System.Windows.Forms.Button();
            this.Btn_fermer = new System.Windows.Forms.Button();
            this.Btn_couleur_ap = new System.Windows.Forms.Button();
            this.Btn_dossier_media = new System.Windows.Forms.Button();
            this.Btn_dossier_page = new System.Windows.Forms.Button();
            this.Pb_cadenas = new System.Windows.Forms.PictureBox();
            this.menuPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pb_cadenas)).BeginInit();
            this.SuspendLayout();
            // 
            // TbDossierMedia
            // 
            this.TbDossierMedia.BackColor = System.Drawing.Color.White;
            this.TbDossierMedia.Location = new System.Drawing.Point(10, 94);
            this.TbDossierMedia.Name = "TbDossierMedia";
            this.TbDossierMedia.Size = new System.Drawing.Size(226, 20);
            this.TbDossierMedia.TabIndex = 2;
            this.TbDossierMedia.TextChanged += new System.EventHandler(this.TbDossierMedia_TextChanged);
            // 
            // Tb_dossier_page
            // 
            this.Tb_dossier_page.BackColor = System.Drawing.Color.White;
            this.Tb_dossier_page.Location = new System.Drawing.Point(11, 46);
            this.Tb_dossier_page.Name = "Tb_dossier_page";
            this.Tb_dossier_page.Size = new System.Drawing.Size(226, 20);
            this.Tb_dossier_page.TabIndex = 0;
            this.Tb_dossier_page.TextChanged += new System.EventHandler(this.TbDossierHTML_TextChanged);
            // 
            // Cb_voir_ID
            // 
            this.Cb_voir_ID.AutoSize = true;
            this.Cb_voir_ID.Checked = true;
            this.Cb_voir_ID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Cb_voir_ID.Location = new System.Drawing.Point(8, 126);
            this.Cb_voir_ID.Name = "Cb_voir_ID";
            this.Cb_voir_ID.Size = new System.Drawing.Size(112, 17);
            this.Cb_voir_ID.TabIndex = 4;
            this.Cb_voir_ID.Text = "Voir &identifiant [ID]";
            this.Cb_voir_ID.UseVisualStyleBackColor = true;
            this.Cb_voir_ID.CheckedChanged += new System.EventHandler(this.Cb_voir_ID_CheckedChanged);
            // 
            // CbVoirMedia
            // 
            this.CbVoirMedia.AutoSize = true;
            this.CbVoirMedia.Checked = true;
            this.CbVoirMedia.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbVoirMedia.Location = new System.Drawing.Point(8, 142);
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
            this.CbVoirDateChangement.Checked = true;
            this.CbVoirDateChangement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbVoirDateChangement.Location = new System.Drawing.Point(8, 175);
            this.CbVoirDateChangement.Name = "CbVoirDateChangement";
            this.CbVoirDateChangement.Size = new System.Drawing.Size(145, 17);
            this.CbVoirDateChangement.TabIndex = 7;
            this.CbVoirDateChangement.Text = "Voir date de &changement";
            this.CbVoirDateChangement.UseVisualStyleBackColor = true;
            this.CbVoirDateChangement.CheckedChanged += new System.EventHandler(this.CbVoirDateChangement_CheckedChanged);
            // 
            // CbDateLongue
            // 
            this.CbDateLongue.AutoSize = true;
            this.CbDateLongue.Checked = true;
            this.CbDateLongue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbDateLongue.Location = new System.Drawing.Point(8, 158);
            this.CbDateLongue.Name = "CbDateLongue";
            this.CbDateLongue.Size = new System.Drawing.Size(84, 17);
            this.CbDateLongue.TabIndex = 6;
            this.CbDateLongue.Text = "&Date longue";
            this.CbDateLongue.UseVisualStyleBackColor = true;
            this.CbDateLongue.TextChanged += new System.EventHandler(this.CbDateLongue_TextChanged);
            // 
            // CbVoirNote
            // 
            this.CbVoirNote.AutoSize = true;
            this.CbVoirNote.Checked = true;
            this.CbVoirNote.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbVoirNote.Location = new System.Drawing.Point(155, 142);
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
            this.CbVoirReference.Checked = true;
            this.CbVoirReference.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbVoirReference.Location = new System.Drawing.Point(155, 126);
            this.CbVoirReference.Name = "CbVoirReference";
            this.CbVoirReference.Size = new System.Drawing.Size(92, 17);
            this.CbVoirReference.TabIndex = 9;
            this.CbVoirReference.Text = "Voir &reference";
            this.CbVoirReference.UseVisualStyleBackColor = true;
            this.CbVoirReference.CheckedChanged += new System.EventHandler(this.CbVoirReference_CheckedChanged);
            // 
            // CbVoirChercheur
            // 
            this.CbVoirChercheur.AutoSize = true;
            this.CbVoirChercheur.Checked = true;
            this.CbVoirChercheur.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbVoirChercheur.Location = new System.Drawing.Point(8, 191);
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
            this.CbVoirCarte.Checked = true;
            this.CbVoirCarte.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbVoirCarte.Location = new System.Drawing.Point(155, 158);
            this.CbVoirCarte.Name = "CbVoirCarte";
            this.CbVoirCarte.Size = new System.Drawing.Size(139, 17);
            this.CbVoirCarte.TabIndex = 11;
            this.CbVoirCarte.Text = "Voir carte &géographique";
            this.CbVoirCarte.UseVisualStyleBackColor = true;
            this.CbVoirCarte.CheckedChanged += new System.EventHandler(this.CbVoirCarte_CheckedChanged);
            // 
            // CbVoirInfoBulle
            // 
            this.CbVoirInfoBulle.AutoSize = true;
            this.CbVoirInfoBulle.Checked = true;
            this.CbVoirInfoBulle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbVoirInfoBulle.Location = new System.Drawing.Point(155, 175);
            this.CbVoirInfoBulle.Name = "CbVoirInfoBulle";
            this.CbVoirInfoBulle.Size = new System.Drawing.Size(89, 17);
            this.CbVoirInfoBulle.TabIndex = 12;
            this.CbVoirInfoBulle.Text = "Voir info &bulle";
            this.CbVoirInfoBulle.UseVisualStyleBackColor = true;
            // 
            // Lb_code_arriere_plan
            // 
            this.Lb_code_arriere_plan.AutoSize = true;
            this.Lb_code_arriere_plan.Location = new System.Drawing.Point(178, 241);
            this.Lb_code_arriere_plan.Name = "Lb_code_arriere_plan";
            this.Lb_code_arriere_plan.Size = new System.Drawing.Size(46, 13);
            this.Lb_code_arriere_plan.TabIndex = 140;
            this.Lb_code_arriere_plan.Text = "F0D07E";
            this.Lb_code_arriere_plan.TextChanged += new System.EventHandler(this.Lb_code_arriere_plan_TextChanged);
            // 
            // Lb_couleur_arriere_plan
            // 
            this.Lb_couleur_arriere_plan.AutoSize = true;
            this.Lb_couleur_arriere_plan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Lb_couleur_arriere_plan.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lb_couleur_arriere_plan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(208)))), ((int)(((byte)(176)))));
            this.Lb_couleur_arriere_plan.Location = new System.Drawing.Point(137, 226);
            this.Lb_couleur_arriere_plan.Name = "Lb_couleur_arriere_plan";
            this.Lb_couleur_arriere_plan.Size = new System.Drawing.Size(44, 31);
            this.Lb_couleur_arriere_plan.TabIndex = 141;
            this.Lb_couleur_arriere_plan.Text = "▌▌▌";
            // 
            // Lb_Confidentiel
            // 
            this.Lb_Confidentiel.AutoSize = true;
            this.Lb_Confidentiel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lb_Confidentiel.Location = new System.Drawing.Point(385, 63);
            this.Lb_Confidentiel.Name = "Lb_Confidentiel";
            this.Lb_Confidentiel.Size = new System.Drawing.Size(231, 20);
            this.Lb_Confidentiel.TabIndex = 142;
            this.Lb_Confidentiel.Text = "Rendre les informations visibles";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(385, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(142, 29);
            this.label8.TabIndex = 164;
            this.label8.Text = "Confidentiel";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 284);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(184, 26);
            this.label10.TabIndex = 177;
            this.label10.Text = "Notez que les notes peuvent contenir\r\ndes informations confidentielles.";
            // 
            // Tb_tout_evenement_ans
            // 
            this.Tb_tout_evenement_ans.BackColor = System.Drawing.Color.White;
            this.Tb_tout_evenement_ans.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Tb_tout_evenement_ans.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tb_tout_evenement_ans.Location = new System.Drawing.Point(566, 116);
            this.Tb_tout_evenement_ans.Name = "Tb_tout_evenement_ans";
            this.Tb_tout_evenement_ans.Size = new System.Drawing.Size(29, 12);
            this.Tb_tout_evenement_ans.TabIndex = 17;
            this.Tb_tout_evenement_ans.Text = "0";
            this.Tb_tout_evenement_ans.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Tb_tout_evenement_ans.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_tout_evenement_ans_KeyPress);
            // 
            // Cb_tout_evenement_date
            // 
            this.Cb_tout_evenement_date.AutoSize = true;
            this.Cb_tout_evenement_date.Checked = true;
            this.Cb_tout_evenement_date.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Cb_tout_evenement_date.Location = new System.Drawing.Point(350, 116);
            this.Cb_tout_evenement_date.Name = "Cb_tout_evenement_date";
            this.Cb_tout_evenement_date.Size = new System.Drawing.Size(78, 17);
            this.Cb_tout_evenement_date.TabIndex = 16;
            this.Cb_tout_evenement_date.Text = "Date après";
            this.Cb_tout_evenement_date.UseVisualStyleBackColor = true;
            this.Cb_tout_evenement_date.CheckedChanged += new System.EventHandler(this.Cb_tout_evenement_date_CheckedChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(596, 118);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(99, 13);
            this.label15.TabIndex = 193;
            this.label15.Text = "ans de l\'événement";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(4, 323);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(197, 28);
            this.label14.TabIndex = 198;
            this.label14.Text = "Dans le champ des ans, mettez 0 pour afficher les dates sans restriction de temps" +
    ".";
            // 
            // Tb_union_ans
            // 
            this.Tb_union_ans.BackColor = System.Drawing.Color.White;
            this.Tb_union_ans.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Tb_union_ans.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tb_union_ans.Location = new System.Drawing.Point(566, 222);
            this.Tb_union_ans.MaxLength = 4;
            this.Tb_union_ans.Name = "Tb_union_ans";
            this.Tb_union_ans.Size = new System.Drawing.Size(29, 12);
            this.Tb_union_ans.TabIndex = 28;
            this.Tb_union_ans.Text = "0";
            this.Tb_union_ans.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Tb_union_ans.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_union_ans_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(596, 222);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 148;
            this.label2.Text = "ans de l\'événement";
            // 
            // Cb_union_date
            // 
            this.Cb_union_date.AutoSize = true;
            this.Cb_union_date.Checked = true;
            this.Cb_union_date.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Cb_union_date.Location = new System.Drawing.Point(350, 222);
            this.Cb_union_date.Name = "Cb_union_date";
            this.Cb_union_date.Size = new System.Drawing.Size(203, 17);
            this.Cb_union_date.TabIndex = 27;
            this.Cb_union_date.Text = "Date relier à l\'union d\'un couple après";
            this.Cb_union_date.UseVisualStyleBackColor = true;
            this.Cb_union_date.CheckedChanged += new System.EventHandler(this.Cb_mariage_CheckedChanged);
            // 
            // Cb_deces_date
            // 
            this.Cb_deces_date.AutoSize = true;
            this.Cb_deces_date.Checked = true;
            this.Cb_deces_date.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Cb_deces_date.Location = new System.Drawing.Point(350, 238);
            this.Cb_deces_date.Name = "Cb_deces_date";
            this.Cb_deces_date.Size = new System.Drawing.Size(159, 17);
            this.Cb_deces_date.TabIndex = 29;
            this.Cb_deces_date.Text = "Date relier à un décès après";
            this.Cb_deces_date.UseVisualStyleBackColor = true;
            this.Cb_deces_date.CheckedChanged += new System.EventHandler(this.Cb_deces_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(596, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "ans de l\'événement";
            // 
            // Tb_deces_ans
            // 
            this.Tb_deces_ans.BackColor = System.Drawing.Color.White;
            this.Tb_deces_ans.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Tb_deces_ans.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tb_deces_ans.Location = new System.Drawing.Point(566, 238);
            this.Tb_deces_ans.MaxLength = 4;
            this.Tb_deces_ans.Name = "Tb_deces_ans";
            this.Tb_deces_ans.Size = new System.Drawing.Size(29, 12);
            this.Tb_deces_ans.TabIndex = 30;
            this.Tb_deces_ans.Text = "0";
            this.Tb_deces_ans.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Tb_deces_ans.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_deces_ans_KeyPress);
            // 
            // Tb_naissance_ans
            // 
            this.Tb_naissance_ans.BackColor = System.Drawing.Color.White;
            this.Tb_naissance_ans.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Tb_naissance_ans.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tb_naissance_ans.Location = new System.Drawing.Point(566, 157);
            this.Tb_naissance_ans.MaxLength = 4;
            this.Tb_naissance_ans.Name = "Tb_naissance_ans";
            this.Tb_naissance_ans.Size = new System.Drawing.Size(29, 12);
            this.Tb_naissance_ans.TabIndex = 20;
            this.Tb_naissance_ans.Text = "0";
            this.Tb_naissance_ans.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Tb_naissance_ans.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_naissance_ans_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(596, 238);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 151;
            this.label3.Text = "ans de l\'événement";
            // 
            // Cb_naissance_date
            // 
            this.Cb_naissance_date.AccessibleDescription = "ertertret ert";
            this.Cb_naissance_date.AutoSize = true;
            this.Cb_naissance_date.Checked = true;
            this.Cb_naissance_date.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Cb_naissance_date.Location = new System.Drawing.Point(350, 157);
            this.Cb_naissance_date.Name = "Cb_naissance_date";
            this.Cb_naissance_date.Size = new System.Drawing.Size(174, 17);
            this.Cb_naissance_date.TabIndex = 19;
            this.Cb_naissance_date.Text = "Date relier à la naissance après";
            this.Cb_naissance_date.UseVisualStyleBackColor = true;
            this.Cb_naissance_date.CheckedChanged += new System.EventHandler(this.Cb_naissance_CheckedChanged);
            // 
            // Cb_inhumation_date
            // 
            this.Cb_inhumation_date.AutoSize = true;
            this.Cb_inhumation_date.Checked = true;
            this.Cb_inhumation_date.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Cb_inhumation_date.Location = new System.Drawing.Point(350, 254);
            this.Cb_inhumation_date.Name = "Cb_inhumation_date";
            this.Cb_inhumation_date.Size = new System.Drawing.Size(187, 17);
            this.Cb_inhumation_date.TabIndex = 31;
            this.Cb_inhumation_date.Text = "Date relier à une inhumation après";
            this.Cb_inhumation_date.UseVisualStyleBackColor = true;
            this.Cb_inhumation_date.CheckedChanged += new System.EventHandler(this.Cb_inhumation_CheckedChanged);
            // 
            // Tb_inhumation_ans
            // 
            this.Tb_inhumation_ans.BackColor = System.Drawing.Color.White;
            this.Tb_inhumation_ans.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Tb_inhumation_ans.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tb_inhumation_ans.Location = new System.Drawing.Point(566, 254);
            this.Tb_inhumation_ans.MaxLength = 4;
            this.Tb_inhumation_ans.Name = "Tb_inhumation_ans";
            this.Tb_inhumation_ans.Size = new System.Drawing.Size(29, 12);
            this.Tb_inhumation_ans.TabIndex = 32;
            this.Tb_inhumation_ans.Text = "0";
            this.Tb_inhumation_ans.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Tb_inhumation_ans.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_inhumation_ans_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(596, 254);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 13);
            this.label7.TabIndex = 163;
            this.label7.Text = "ans de l\'événement";
            // 
            // Cb_autre_date
            // 
            this.Cb_autre_date.AutoSize = true;
            this.Cb_autre_date.Checked = true;
            this.Cb_autre_date.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Cb_autre_date.Location = new System.Drawing.Point(350, 271);
            this.Cb_autre_date.Name = "Cb_autre_date";
            this.Cb_autre_date.Size = new System.Drawing.Size(233, 17);
            this.Cb_autre_date.TabIndex = 33;
            this.Cb_autre_date.Text = "Date relier à d\'autres événements et attibuts";
            this.Cb_autre_date.UseVisualStyleBackColor = true;
            this.Cb_autre_date.CheckedChanged += new System.EventHandler(this.Cb_religion_CheckedChanged);
            // 
            // Cb_citoyen_date
            // 
            this.Cb_citoyen_date.AutoSize = true;
            this.Cb_citoyen_date.Checked = true;
            this.Cb_citoyen_date.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Cb_citoyen_date.Location = new System.Drawing.Point(350, 189);
            this.Cb_citoyen_date.Name = "Cb_citoyen_date";
            this.Cb_citoyen_date.Size = new System.Drawing.Size(181, 17);
            this.Cb_citoyen_date.TabIndex = 23;
            this.Cb_citoyen_date.Text = "Date relier à la citoyenneté après";
            this.Cb_citoyen_date.UseVisualStyleBackColor = true;
            this.Cb_citoyen_date.CheckedChanged += new System.EventHandler(this.Cb_citoyen_CheckedChanged);
            // 
            // Tb_citoyen_ans
            // 
            this.Tb_citoyen_ans.BackColor = System.Drawing.Color.White;
            this.Tb_citoyen_ans.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Tb_citoyen_ans.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tb_citoyen_ans.Location = new System.Drawing.Point(566, 189);
            this.Tb_citoyen_ans.MaxLength = 4;
            this.Tb_citoyen_ans.Name = "Tb_citoyen_ans";
            this.Tb_citoyen_ans.Size = new System.Drawing.Size(29, 12);
            this.Tb_citoyen_ans.TabIndex = 24;
            this.Tb_citoyen_ans.Text = "0";
            this.Tb_citoyen_ans.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Tb_citoyen_ans.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_citoyen_ans_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(596, 189);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(99, 13);
            this.label13.TabIndex = 173;
            this.label13.Text = "ans de l\'événement";
            // 
            // Cb_testament_information
            // 
            this.Cb_testament_information.AutoSize = true;
            this.Cb_testament_information.Checked = true;
            this.Cb_testament_information.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Cb_testament_information.Location = new System.Drawing.Point(350, 287);
            this.Cb_testament_information.Name = "Cb_testament_information";
            this.Cb_testament_information.Size = new System.Drawing.Size(167, 17);
            this.Cb_testament_information.TabIndex = 35;
            this.Cb_testament_information.Text = "Information relier au testament";
            this.Cb_testament_information.UseVisualStyleBackColor = true;
            this.Cb_testament_information.CheckedChanged += new System.EventHandler(this.Cb_testament_CheckedChanged);
            // 
            // Tb_autre_ans
            // 
            this.Tb_autre_ans.BackColor = System.Drawing.Color.White;
            this.Tb_autre_ans.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Tb_autre_ans.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tb_autre_ans.Location = new System.Drawing.Point(566, 271);
            this.Tb_autre_ans.MaxLength = 4;
            this.Tb_autre_ans.Name = "Tb_autre_ans";
            this.Tb_autre_ans.Size = new System.Drawing.Size(29, 12);
            this.Tb_autre_ans.TabIndex = 34;
            this.Tb_autre_ans.Text = "0";
            this.Tb_autre_ans.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Tb_autre_ans.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_autre_ans_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(596, 271);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 188;
            this.label5.Text = "ans de l\'événement";
            // 
            // Tb_testament_ans
            // 
            this.Tb_testament_ans.BackColor = System.Drawing.Color.White;
            this.Tb_testament_ans.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Tb_testament_ans.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tb_testament_ans.Location = new System.Drawing.Point(566, 287);
            this.Tb_testament_ans.MaxLength = 4;
            this.Tb_testament_ans.Name = "Tb_testament_ans";
            this.Tb_testament_ans.Size = new System.Drawing.Size(29, 12);
            this.Tb_testament_ans.TabIndex = 36;
            this.Tb_testament_ans.Text = "0";
            this.Tb_testament_ans.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Tb_testament_ans.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_testament_ans_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(596, 287);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(99, 13);
            this.label12.TabIndex = 190;
            this.label12.Text = "ans de l\'événement";
            // 
            // Cb_ordonnance_date
            // 
            this.Cb_ordonnance_date.AutoSize = true;
            this.Cb_ordonnance_date.Checked = true;
            this.Cb_ordonnance_date.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Cb_ordonnance_date.Location = new System.Drawing.Point(350, 206);
            this.Cb_ordonnance_date.Name = "Cb_ordonnance_date";
            this.Cb_ordonnance_date.Size = new System.Drawing.Size(187, 17);
            this.Cb_ordonnance_date.TabIndex = 25;
            this.Cb_ordonnance_date.Text = "Date relier à un ordonnance (SDJ)";
            this.Cb_ordonnance_date.UseVisualStyleBackColor = true;
            this.Cb_ordonnance_date.CheckedChanged += new System.EventHandler(this.Cb_ordonnance_date_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(596, 206);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 13);
            this.label9.TabIndex = 208;
            this.label9.Text = "ans de l\'événement";
            // 
            // Tb_ordonnance_ans
            // 
            this.Tb_ordonnance_ans.BackColor = System.Drawing.Color.White;
            this.Tb_ordonnance_ans.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Tb_ordonnance_ans.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tb_ordonnance_ans.Location = new System.Drawing.Point(566, 206);
            this.Tb_ordonnance_ans.MaxLength = 4;
            this.Tb_ordonnance_ans.Name = "Tb_ordonnance_ans";
            this.Tb_ordonnance_ans.Size = new System.Drawing.Size(29, 12);
            this.Tb_ordonnance_ans.TabIndex = 26;
            this.Tb_ordonnance_ans.Text = "0";
            this.Tb_ordonnance_ans.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Tb_ordonnance_ans.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_ordonnance_ans_KeyPress);
            // 
            // Cb_bapteme_date
            // 
            this.Cb_bapteme_date.AccessibleDescription = "ertertret ert";
            this.Cb_bapteme_date.AutoSize = true;
            this.Cb_bapteme_date.Checked = true;
            this.Cb_bapteme_date.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Cb_bapteme_date.Location = new System.Drawing.Point(350, 173);
            this.Cb_bapteme_date.Name = "Cb_bapteme_date";
            this.Cb_bapteme_date.Size = new System.Drawing.Size(171, 17);
            this.Cb_bapteme_date.TabIndex = 21;
            this.Cb_bapteme_date.Text = "Date relier à un bapteme après";
            this.Cb_bapteme_date.UseVisualStyleBackColor = true;
            this.Cb_bapteme_date.CheckedChanged += new System.EventHandler(this.Cb_bapteme_date_CheckedChanged);
            // 
            // Tb_bapteme_ans
            // 
            this.Tb_bapteme_ans.BackColor = System.Drawing.Color.White;
            this.Tb_bapteme_ans.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Tb_bapteme_ans.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tb_bapteme_ans.Location = new System.Drawing.Point(566, 173);
            this.Tb_bapteme_ans.MaxLength = 4;
            this.Tb_bapteme_ans.Name = "Tb_bapteme_ans";
            this.Tb_bapteme_ans.Size = new System.Drawing.Size(29, 12);
            this.Tb_bapteme_ans.TabIndex = 22;
            this.Tb_bapteme_ans.Text = "0";
            this.Tb_bapteme_ans.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Tb_bapteme_ans.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_bapteme_ans_KeyPress);
            // 
            // Lb_bapteme_ans
            // 
            this.Lb_bapteme_ans.AutoSize = true;
            this.Lb_bapteme_ans.Location = new System.Drawing.Point(597, 173);
            this.Lb_bapteme_ans.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.Lb_bapteme_ans.Name = "Lb_bapteme_ans";
            this.Lb_bapteme_ans.Size = new System.Drawing.Size(99, 13);
            this.Lb_bapteme_ans.TabIndex = 202;
            this.Lb_bapteme_ans.Text = "ans de l\'événement";
            // 
            // Cb_mode_depanage
            // 
            this.Cb_mode_depanage.AutoSize = true;
            this.Cb_mode_depanage.Checked = true;
            this.Cb_mode_depanage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Cb_mode_depanage.Location = new System.Drawing.Point(155, 191);
            this.Cb_mode_depanage.Name = "Cb_mode_depanage";
            this.Cb_mode_depanage.Size = new System.Drawing.Size(104, 17);
            this.Cb_mode_depanage.TabIndex = 13;
            this.Cb_mode_depanage.Text = "&Mode dépanage";
            this.Cb_mode_depanage.UseVisualStyleBackColor = true;
            this.Cb_mode_depanage.CheckedChanged += new System.EventHandler(this.Cb_mode_depanage_CheckedChanged);
            // 
            // menuPrincipal
            // 
            this.menuPrincipal.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AideToolStripMenuItem});
            this.menuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuPrincipal.Name = "menuPrincipal";
            this.menuPrincipal.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuPrincipal.Size = new System.Drawing.Size(704, 24);
            this.menuPrincipal.TabIndex = 207;
            this.menuPrincipal.Text = "menuStrip1";
            // 
            // AideToolStripMenuItem
            // 
            this.AideToolStripMenuItem.Name = "AideToolStripMenuItem";
            this.AideToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.AideToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.AideToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.AideToolStripMenuItem.Text = "&Aide";
            this.AideToolStripMenuItem.Click += new System.EventHandler(this.AideToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 208;
            this.label4.Text = "Dossier page";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 209;
            this.label6.Text = "Dossier média";
            // 
            // Cb_tout_evenement
            // 
            this.Cb_tout_evenement.AutoSize = true;
            this.Cb_tout_evenement.Checked = true;
            this.Cb_tout_evenement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Cb_tout_evenement.Location = new System.Drawing.Point(326, 100);
            this.Cb_tout_evenement.Name = "Cb_tout_evenement";
            this.Cb_tout_evenement.Size = new System.Drawing.Size(179, 17);
            this.Cb_tout_evenement.TabIndex = 15;
            this.Cb_tout_evenement.Text = "Tous les événements et attributs";
            this.Cb_tout_evenement.UseVisualStyleBackColor = true;
            this.Cb_tout_evenement.CheckedChanged += new System.EventHandler(this.Cb_tout_evenement_CheckedChanged);
            // 
            // Cb_certain
            // 
            this.Cb_certain.AutoSize = true;
            this.Cb_certain.Location = new System.Drawing.Point(326, 141);
            this.Cb_certain.Name = "Cb_certain";
            this.Cb_certain.Size = new System.Drawing.Size(177, 17);
            this.Cb_certain.TabIndex = 18;
            this.Cb_certain.Text = "Certains événements et attributs";
            this.Cb_certain.UseVisualStyleBackColor = true;
            this.Cb_certain.CheckedChanged += new System.EventHandler(this.Cb_certain_CheckedChanged);
            // 
            // Cb_enregistrer_balise
            // 
            this.Cb_enregistrer_balise.AutoSize = true;
            this.Cb_enregistrer_balise.Checked = true;
            this.Cb_enregistrer_balise.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Cb_enregistrer_balise.Location = new System.Drawing.Point(155, 208);
            this.Cb_enregistrer_balise.Name = "Cb_enregistrer_balise";
            this.Cb_enregistrer_balise.Size = new System.Drawing.Size(106, 17);
            this.Cb_enregistrer_balise.TabIndex = 211;
            this.Cb_enregistrer_balise.Text = "&Enregistrer balise";
            this.Cb_enregistrer_balise.UseVisualStyleBackColor = true;
            this.Cb_enregistrer_balise.CheckedChanged += new System.EventHandler(this.Cb_enregistrer_balise_CheckedChanged);
            // 
            // Btn_annuler
            // 
            this.Btn_annuler.AutoSize = true;
            this.Btn_annuler.BackgroundImage = global::GH.Properties.Resources.btn_annuler;
            this.Btn_annuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_annuler.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_annuler.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_annuler.FlatAppearance.BorderSize = 0;
            this.Btn_annuler.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Btn_annuler.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Magenta;
            this.Btn_annuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_annuler.Location = new System.Drawing.Point(599, 312);
            this.Btn_annuler.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_annuler.Name = "Btn_annuler";
            this.Btn_annuler.Size = new System.Drawing.Size(38, 41);
            this.Btn_annuler.TabIndex = 210;
            this.Btn_annuler.UseVisualStyleBackColor = true;
            this.Btn_annuler.Click += new System.EventHandler(this.Btn_annuler_Click);
            // 
            // Btn_fermer
            // 
            this.Btn_fermer.AutoSize = true;
            this.Btn_fermer.BackgroundImage = global::GH.Properties.Resources.Btn_ok;
            this.Btn_fermer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_fermer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_fermer.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_fermer.FlatAppearance.BorderSize = 0;
            this.Btn_fermer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Btn_fermer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Magenta;
            this.Btn_fermer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_fermer.Location = new System.Drawing.Point(651, 312);
            this.Btn_fermer.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_fermer.Name = "Btn_fermer";
            this.Btn_fermer.Size = new System.Drawing.Size(38, 41);
            this.Btn_fermer.TabIndex = 37;
            this.Btn_fermer.UseVisualStyleBackColor = true;
            this.Btn_fermer.Click += new System.EventHandler(this.Btn_fermer_Click_1);
            // 
            // Btn_couleur_ap
            // 
            this.Btn_couleur_ap.AutoSize = true;
            this.Btn_couleur_ap.BackgroundImage = global::GH.Properties.Resources.btn_arriere_plan;
            this.Btn_couleur_ap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_couleur_ap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_couleur_ap.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_couleur_ap.FlatAppearance.BorderSize = 0;
            this.Btn_couleur_ap.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Btn_couleur_ap.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Magenta;
            this.Btn_couleur_ap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_couleur_ap.Location = new System.Drawing.Point(7, 226);
            this.Btn_couleur_ap.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_couleur_ap.Name = "Btn_couleur_ap";
            this.Btn_couleur_ap.Size = new System.Drawing.Size(120, 41);
            this.Btn_couleur_ap.TabIndex = 14;
            this.Btn_couleur_ap.UseVisualStyleBackColor = true;
            this.Btn_couleur_ap.Click += new System.EventHandler(this.Btn_couleur_ap_Click);
            // 
            // Btn_dossier_media
            // 
            this.Btn_dossier_media.AutoSize = true;
            this.Btn_dossier_media.BackgroundImage = global::GH.Properties.Resources.Btn_dossier;
            this.Btn_dossier_media.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_dossier_media.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_dossier_media.FlatAppearance.BorderSize = 0;
            this.Btn_dossier_media.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Btn_dossier_media.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Magenta;
            this.Btn_dossier_media.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_dossier_media.Location = new System.Drawing.Point(242, 84);
            this.Btn_dossier_media.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_dossier_media.Name = "Btn_dossier_media";
            this.Btn_dossier_media.Size = new System.Drawing.Size(52, 41);
            this.Btn_dossier_media.TabIndex = 3;
            this.Btn_dossier_media.UseVisualStyleBackColor = true;
            this.Btn_dossier_media.Click += new System.EventHandler(this.Btn_dossier_media_Click);
            // 
            // Btn_dossier_page
            // 
            this.Btn_dossier_page.AutoSize = true;
            this.Btn_dossier_page.BackgroundImage = global::GH.Properties.Resources.Btn_dossier;
            this.Btn_dossier_page.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_dossier_page.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_dossier_page.FlatAppearance.BorderSize = 0;
            this.Btn_dossier_page.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Btn_dossier_page.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Magenta;
            this.Btn_dossier_page.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_dossier_page.Location = new System.Drawing.Point(241, 35);
            this.Btn_dossier_page.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_dossier_page.Name = "Btn_dossier_page";
            this.Btn_dossier_page.Size = new System.Drawing.Size(52, 41);
            this.Btn_dossier_page.TabIndex = 1;
            this.Btn_dossier_page.UseVisualStyleBackColor = true;
            this.Btn_dossier_page.Click += new System.EventHandler(this.Btn_dossier_page_Click);
            // 
            // Pb_cadenas
            // 
            this.Pb_cadenas.Image = global::GH.Properties.Resources.priver_50X64;
            this.Pb_cadenas.Location = new System.Drawing.Point(333, 32);
            this.Pb_cadenas.Name = "Pb_cadenas";
            this.Pb_cadenas.Size = new System.Drawing.Size(50, 64);
            this.Pb_cadenas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Pb_cadenas.TabIndex = 184;
            this.Pb_cadenas.TabStop = false;
            // 
            // ParaClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(704, 365);
            this.Controls.Add(this.Cb_enregistrer_balise);
            this.Controls.Add(this.Btn_annuler);
            this.Controls.Add(this.Cb_certain);
            this.Controls.Add(this.Cb_tout_evenement);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.Tb_tout_evenement_ans);
            this.Controls.Add(this.Btn_fermer);
            this.Controls.Add(this.Btn_couleur_ap);
            this.Controls.Add(this.Cb_tout_evenement_date);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.Cb_ordonnance_date);
            this.Controls.Add(this.Tb_ordonnance_ans);
            this.Controls.Add(this.Btn_dossier_media);
            this.Controls.Add(this.Tb_bapteme_ans);
            this.Controls.Add(this.Btn_dossier_page);
            this.Controls.Add(this.Lb_bapteme_ans);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Tb_testament_ans);
            this.Controls.Add(this.Cb_bapteme_date);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Tb_autre_ans);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.Tb_citoyen_ans);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Tb_inhumation_ans);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Pb_cadenas);
            this.Controls.Add(this.Tb_naissance_ans);
            this.Controls.Add(this.Cb_testament_information);
            this.Controls.Add(this.Tb_deces_ans);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Lb_Confidentiel);
            this.Controls.Add(this.Tb_union_ans);
            this.Controls.Add(this.Cb_citoyen_date);
            this.Controls.Add(this.Lb_couleur_arriere_plan);
            this.Controls.Add(this.Cb_autre_date);
            this.Controls.Add(this.Lb_code_arriere_plan);
            this.Controls.Add(this.CbVoirInfoBulle);
            this.Controls.Add(this.CbVoirCarte);
            this.Controls.Add(this.Cb_inhumation_date);
            this.Controls.Add(this.CbVoirChercheur);
            this.Controls.Add(this.Cb_naissance_date);
            this.Controls.Add(this.CbVoirReference);
            this.Controls.Add(this.CbVoirNote);
            this.Controls.Add(this.CbDateLongue);
            this.Controls.Add(this.CbVoirDateChangement);
            this.Controls.Add(this.Cb_deces_date);
            this.Controls.Add(this.CbVoirMedia);
            this.Controls.Add(this.Cb_union_date);
            this.Controls.Add(this.Cb_voir_ID);
            this.Controls.Add(this.Tb_dossier_page);
            this.Controls.Add(this.TbDossierMedia);
            this.Controls.Add(this.menuPrincipal);
            this.Controls.Add(this.Cb_mode_depanage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuPrincipal;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParaClass";
            this.Text = "Paramètres";
            this.Load += new System.EventHandler(this.Parametre_Load);
            this.menuPrincipal.ResumeLayout(false);
            this.menuPrincipal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pb_cadenas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TbDossierMedia;
        private System.Windows.Forms.TextBox Tb_dossier_page;
        private System.Windows.Forms.CheckBox Cb_voir_ID;
        private System.Windows.Forms.CheckBox CbVoirMedia;
        private System.Windows.Forms.CheckBox CbVoirDateChangement;
        private System.Windows.Forms.CheckBox CbDateLongue;
        private System.Windows.Forms.CheckBox CbVoirNote;
        private System.Windows.Forms.CheckBox CbVoirReference;
        private System.Windows.Forms.CheckBox CbVoirChercheur;
        private System.Windows.Forms.CheckBox CbVoirCarte;
        private System.Windows.Forms.CheckBox CbVoirInfoBulle;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label Lb_code_arriere_plan;
        private System.Windows.Forms.Label Lb_couleur_arriere_plan;
        private System.Windows.Forms.Label Lb_Confidentiel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox Pb_cadenas;
        private System.Windows.Forms.TextBox Tb_tout_evenement_ans;
        private System.Windows.Forms.CheckBox Cb_tout_evenement_date;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox Tb_union_ans;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox Cb_union_date;
        private System.Windows.Forms.CheckBox Cb_deces_date;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Tb_deces_ans;
        private System.Windows.Forms.TextBox Tb_naissance_ans;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox Cb_naissance_date;
        private System.Windows.Forms.CheckBox Cb_inhumation_date;
        private System.Windows.Forms.TextBox Tb_inhumation_ans;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox Cb_autre_date;
        private System.Windows.Forms.CheckBox Cb_citoyen_date;
        private System.Windows.Forms.TextBox Tb_citoyen_ans;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox Cb_testament_information;
        private System.Windows.Forms.TextBox Tb_autre_ans;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Tb_testament_ans;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox Cb_mode_depanage;
        private System.Windows.Forms.MenuStrip menuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem AideToolStripMenuItem;
        private System.Windows.Forms.CheckBox Cb_bapteme_date;
        private System.Windows.Forms.TextBox Tb_bapteme_ans;
        private System.Windows.Forms.Label Lb_bapteme_ans;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox Cb_ordonnance_date;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox Tb_ordonnance_ans;
        private System.Windows.Forms.Button Btn_dossier_page;
        private System.Windows.Forms.Button Btn_dossier_media;
        private System.Windows.Forms.Button Btn_fermer;
        private System.Windows.Forms.Button Btn_couleur_ap;
        private System.Windows.Forms.CheckBox Cb_tout_evenement;
        private System.Windows.Forms.CheckBox Cb_certain;
        private System.Windows.Forms.Button Btn_annuler;
        private System.Windows.Forms.CheckBox Cb_enregistrer_balise;
    }
}