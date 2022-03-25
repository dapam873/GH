namespace GH
{
    partial class GHClass
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GHClass));
            this.menuPrincipal = new System.Windows.Forms.MenuStrip();
            this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OuvrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OuvrirDossierDesFichesHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QuitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ParamètresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AideToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.BienvenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AProposToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblVersionProgramme = new System.Windows.Forms.Label();
            this.Lb_fichier_GEDCOM = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblVersionGEDCOM = new System.Windows.Forms.Label();
            this.lblCharSet = new System.Windows.Forms.Label();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.lblDateHeure = new System.Windows.Forms.Label();
            this.Lb_nom_programe = new System.Windows.Forms.Label();
            this.lbFichierGenererPar = new System.Windows.Forms.Label();
            this.lbFamilleConjoint = new System.Windows.Forms.Label();
            this.lpIndividu = new System.Windows.Forms.Label();
            this.Gb_info_GEDCOM = new System.Windows.Forms.GroupBox();
            this.Tb_temps_execution = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Tb_nombre_famille = new System.Windows.Forms.TextBox();
            this.Tb_nombre_individu = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.RechercheIndividuTB = new System.Windows.Forms.TextBox();
            this.RechercheFamilleTB = new System.Windows.Forms.TextBox();
            this.LvChoixIndividu = new System.Windows.Forms.ListView();
            this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
            this.LvChoixFamille = new System.Windows.Forms.ListView();
            this.Lb_nombre_ligne = new System.Windows.Forms.Label();
            this.Pb_attendre = new System.Windows.Forms.PictureBox();
            this.Btn_annuler = new System.Windows.Forms.Button();
            this.Btn_log_del = new System.Windows.Forms.Button();
            this.Btn_total = new System.Windows.Forms.Button();
            this.Btn_fiche_famille = new System.Windows.Forms.Button();
            this.Btn_fiche_individu = new System.Windows.Forms.Button();
            this.Btn_famille_apres = new System.Windows.Forms.Button();
            this.Btn_famille_avant = new System.Windows.Forms.Button();
            this.Btn_individu_apres = new System.Windows.Forms.Button();
            this.Btn_individu_avant = new System.Windows.Forms.Button();
            this.Btn_balise = new System.Windows.Forms.Button();
            this.Btn_erreur = new System.Windows.Forms.Button();
            this.Btn_debug = new System.Windows.Forms.Button();
            this.Btn_GEDCOM = new System.Windows.Forms.Button();
            this.RechercheFamilleB = new System.Windows.Forms.Button();
            this.RechercheIndividuB = new System.Windows.Forms.Button();
            this.Pb_recherche_individu = new System.Windows.Forms.PictureBox();
            this.Pb_recherche_famille = new System.Windows.Forms.PictureBox();
            this.Cadre = new System.Windows.Forms.Button();
            this.menuPrincipal.SuspendLayout();
            this.Gb_info_GEDCOM.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pb_attendre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pb_recherche_individu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pb_recherche_famille)).BeginInit();
            this.SuspendLayout();
            // 
            // menuPrincipal
            // 
            this.menuPrincipal.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem,
            this.ParamètresToolStripMenuItem,
            this.AideToolStripMenuItem});
            this.menuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuPrincipal.Name = "menuPrincipal";
            this.menuPrincipal.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuPrincipal.Size = new System.Drawing.Size(1234, 24);
            this.menuPrincipal.TabIndex = 0;
            this.menuPrincipal.Text = "dasdsad";
            // 
            // fichierToolStripMenuItem
            // 
            this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OuvrirToolStripMenuItem,
            this.OuvrirDossierDesFichesHTMLToolStripMenuItem,
            this.QuitterToolStripMenuItem});
            this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            this.fichierToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.fichierToolStripMenuItem.Text = "&Fichier";
            this.fichierToolStripMenuItem.Click += new System.EventHandler(this.FichierToolStripMenuItem_Click);
            // 
            // OuvrirToolStripMenuItem
            // 
            this.OuvrirToolStripMenuItem.Name = "OuvrirToolStripMenuItem";
            this.OuvrirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OuvrirToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.OuvrirToolStripMenuItem.Text = "&Ouvrir";
            this.OuvrirToolStripMenuItem.Click += new System.EventHandler(this.OuvrirToolStripMenuItem_Click);
            // 
            // OuvrirDossierDesFichesHTMLToolStripMenuItem
            // 
            this.OuvrirDossierDesFichesHTMLToolStripMenuItem.Name = "OuvrirDossierDesFichesHTMLToolStripMenuItem";
            this.OuvrirDossierDesFichesHTMLToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.H)));
            this.OuvrirDossierDesFichesHTMLToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.OuvrirDossierDesFichesHTMLToolStripMenuItem.Text = "Ouvrir le &dossier des fiches HTML";
            this.OuvrirDossierDesFichesHTMLToolStripMenuItem.Click += new System.EventHandler(this.OuvrirDossierDesFichesHTMLToolStripMenuItem_Click);
            // 
            // QuitterToolStripMenuItem
            // 
            this.QuitterToolStripMenuItem.Name = "QuitterToolStripMenuItem";
            this.QuitterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.QuitterToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.QuitterToolStripMenuItem.Text = "&Quitter";
            this.QuitterToolStripMenuItem.Click += new System.EventHandler(this.QuitterToolStripMenuItem_Click);
            // 
            // ParamètresToolStripMenuItem
            // 
            this.ParamètresToolStripMenuItem.Checked = true;
            this.ParamètresToolStripMenuItem.CheckOnClick = true;
            this.ParamètresToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ParamètresToolStripMenuItem.Name = "ParamètresToolStripMenuItem";
            this.ParamètresToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.ParamètresToolStripMenuItem.Text = "&Paramètres";
            this.ParamètresToolStripMenuItem.Click += new System.EventHandler(this.ParamètresToolStripMenuItem_Click);
            // 
            // AideToolStripMenuItem
            // 
            this.AideToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AideToolStripMenuItem1,
            this.BienvenuToolStripMenuItem,
            this.AProposToolStripMenuItem});
            this.AideToolStripMenuItem.Name = "AideToolStripMenuItem";
            this.AideToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.AideToolStripMenuItem.Text = "&Aide";
            // 
            // AideToolStripMenuItem1
            // 
            this.AideToolStripMenuItem1.Name = "AideToolStripMenuItem1";
            this.AideToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.AideToolStripMenuItem1.Size = new System.Drawing.Size(123, 22);
            this.AideToolStripMenuItem1.Text = "&Aide";
            this.AideToolStripMenuItem1.Click += new System.EventHandler(this.AideToolStripMenuItem1_Click);
            // 
            // BienvenuToolStripMenuItem
            // 
            this.BienvenuToolStripMenuItem.Name = "BienvenuToolStripMenuItem";
            this.BienvenuToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.BienvenuToolStripMenuItem.Text = "Bienvenu";
            this.BienvenuToolStripMenuItem.Click += new System.EventHandler(this.BienvenuToolStripMenuItem_Click);
            // 
            // AProposToolStripMenuItem
            // 
            this.AProposToolStripMenuItem.Name = "AProposToolStripMenuItem";
            this.AProposToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.AProposToolStripMenuItem.Text = "À &propos";
            this.AProposToolStripMenuItem.Click += new System.EventHandler(this.ÀProposToolStripMenuItem_Click);
            // 
            // lblVersionProgramme
            // 
            this.lblVersionProgramme.AutoSize = true;
            this.lblVersionProgramme.Location = new System.Drawing.Point(4, 52);
            this.lblVersionProgramme.Name = "lblVersionProgramme";
            this.lblVersionProgramme.Size = new System.Drawing.Size(105, 13);
            this.lblVersionProgramme.TabIndex = 81;
            this.lblVersionProgramme.Text = "lblVersionProgramme";
            // 
            // Lb_fichier_GEDCOM
            // 
            this.Lb_fichier_GEDCOM.AutoSize = true;
            this.Lb_fichier_GEDCOM.Location = new System.Drawing.Point(4, 66);
            this.Lb_fichier_GEDCOM.Name = "Lb_fichier_GEDCOM";
            this.Lb_fichier_GEDCOM.Size = new System.Drawing.Size(48, 13);
            this.Lb_fichier_GEDCOM.TabIndex = 82;
            this.Lb_fichier_GEDCOM.Text = "lblFichier";
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Location = new System.Drawing.Point(4, 108);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(61, 13);
            this.lblCopyright.TabIndex = 83;
            this.lblCopyright.Text = "lblCopyright";
            // 
            // lblVersionGEDCOM
            // 
            this.lblVersionGEDCOM.AutoSize = true;
            this.lblVersionGEDCOM.Location = new System.Drawing.Point(4, 94);
            this.lblVersionGEDCOM.Name = "lblVersionGEDCOM";
            this.lblVersionGEDCOM.Size = new System.Drawing.Size(99, 13);
            this.lblVersionGEDCOM.TabIndex = 85;
            this.lblVersionGEDCOM.Text = "lblVersionGEDCOM";
            // 
            // lblCharSet
            // 
            this.lblCharSet.AutoSize = true;
            this.lblCharSet.Location = new System.Drawing.Point(4, 122);
            this.lblCharSet.Name = "lblCharSet";
            this.lblCharSet.Size = new System.Drawing.Size(55, 13);
            this.lblCharSet.TabIndex = 86;
            this.lblCharSet.Text = "lblCharSet";
            this.lblCharSet.Click += new System.EventHandler(this.LblCharSet_Click);
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(4, 136);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(65, 13);
            this.lblLanguage.TabIndex = 87;
            this.lblLanguage.Text = "lblLanguage";
            // 
            // lblDateHeure
            // 
            this.lblDateHeure.AutoSize = true;
            this.lblDateHeure.Location = new System.Drawing.Point(4, 80);
            this.lblDateHeure.Name = "lblDateHeure";
            this.lblDateHeure.Size = new System.Drawing.Size(69, 13);
            this.lblDateHeure.TabIndex = 90;
            this.lblDateHeure.Text = "lblDateHeure";
            // 
            // Lb_nom_programe
            // 
            this.Lb_nom_programe.AutoSize = true;
            this.Lb_nom_programe.Location = new System.Drawing.Point(19, 38);
            this.Lb_nom_programe.Name = "Lb_nom_programe";
            this.Lb_nom_programe.Size = new System.Drawing.Size(39, 13);
            this.Lb_nom_programe.TabIndex = 91;
            this.Lb_nom_programe.Text = "lblNom";
            // 
            // lbFichierGenererPar
            // 
            this.lbFichierGenererPar.AutoSize = true;
            this.lbFichierGenererPar.Location = new System.Drawing.Point(4, 24);
            this.lbFichierGenererPar.Name = "lbFichierGenererPar";
            this.lbFichierGenererPar.Size = new System.Drawing.Size(95, 13);
            this.lbFichierGenererPar.TabIndex = 92;
            this.lbFichierGenererPar.Text = "Fichier générer par";
            // 
            // lbFamilleConjoint
            // 
            this.lbFamilleConjoint.AutoSize = true;
            this.lbFamilleConjoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFamilleConjoint.Location = new System.Drawing.Point(14, 355);
            this.lbFamilleConjoint.Name = "lbFamilleConjoint";
            this.lbFamilleConjoint.Size = new System.Drawing.Size(66, 20);
            this.lbFamilleConjoint.TabIndex = 93;
            this.lbFamilleConjoint.Text = "Famille";
            // 
            // lpIndividu
            // 
            this.lpIndividu.AutoSize = true;
            this.lpIndividu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lpIndividu.Location = new System.Drawing.Point(15, 65);
            this.lpIndividu.Name = "lpIndividu";
            this.lpIndividu.Size = new System.Drawing.Size(71, 20);
            this.lpIndividu.TabIndex = 96;
            this.lpIndividu.Text = "Individu";
            // 
            // Gb_info_GEDCOM
            // 
            this.Gb_info_GEDCOM.BackColor = System.Drawing.Color.Transparent;
            this.Gb_info_GEDCOM.Controls.Add(this.Tb_temps_execution);
            this.Gb_info_GEDCOM.Controls.Add(this.lblVersionGEDCOM);
            this.Gb_info_GEDCOM.Controls.Add(this.label4);
            this.Gb_info_GEDCOM.Controls.Add(this.Tb_nombre_famille);
            this.Gb_info_GEDCOM.Controls.Add(this.Tb_nombre_individu);
            this.Gb_info_GEDCOM.Controls.Add(this.label3);
            this.Gb_info_GEDCOM.Controls.Add(this.label2);
            this.Gb_info_GEDCOM.Controls.Add(this.label1);
            this.Gb_info_GEDCOM.Controls.Add(this.Lb_nom_programe);
            this.Gb_info_GEDCOM.Controls.Add(this.lblDateHeure);
            this.Gb_info_GEDCOM.Controls.Add(this.lblVersionProgramme);
            this.Gb_info_GEDCOM.Controls.Add(this.lbFichierGenererPar);
            this.Gb_info_GEDCOM.Controls.Add(this.lblCopyright);
            this.Gb_info_GEDCOM.Controls.Add(this.lblCharSet);
            this.Gb_info_GEDCOM.Controls.Add(this.Lb_fichier_GEDCOM);
            this.Gb_info_GEDCOM.Controls.Add(this.lblLanguage);
            this.Gb_info_GEDCOM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Gb_info_GEDCOM.ForeColor = System.Drawing.Color.Black;
            this.Gb_info_GEDCOM.Location = new System.Drawing.Point(1026, 41);
            this.Gb_info_GEDCOM.Margin = new System.Windows.Forms.Padding(10);
            this.Gb_info_GEDCOM.Name = "Gb_info_GEDCOM";
            this.Gb_info_GEDCOM.Size = new System.Drawing.Size(190, 194);
            this.Gb_info_GEDCOM.TabIndex = 104;
            this.Gb_info_GEDCOM.TabStop = false;
            // 
            // Tb_temps_execution
            // 
            this.Tb_temps_execution.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Tb_temps_execution.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Tb_temps_execution.Location = new System.Drawing.Point(104, 178);
            this.Tb_temps_execution.Name = "Tb_temps_execution";
            this.Tb_temps_execution.Size = new System.Drawing.Size(58, 13);
            this.Tb_temps_execution.TabIndex = 182;
            this.Tb_temps_execution.TabStop = false;
            this.Tb_temps_execution.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 181;
            this.label4.Text = "Temps d\'exécution";
            // 
            // Tb_nombre_famille
            // 
            this.Tb_nombre_famille.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Tb_nombre_famille.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Tb_nombre_famille.Location = new System.Drawing.Point(105, 164);
            this.Tb_nombre_famille.Name = "Tb_nombre_famille";
            this.Tb_nombre_famille.Size = new System.Drawing.Size(58, 13);
            this.Tb_nombre_famille.TabIndex = 123;
            this.Tb_nombre_famille.TabStop = false;
            this.Tb_nombre_famille.Text = "1 000 000";
            this.Tb_nombre_famille.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Tb_nombre_individu
            // 
            this.Tb_nombre_individu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Tb_nombre_individu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Tb_nombre_individu.Location = new System.Drawing.Point(105, 150);
            this.Tb_nombre_individu.Name = "Tb_nombre_individu";
            this.Tb_nombre_individu.Size = new System.Drawing.Size(58, 13);
            this.Tb_nombre_individu.TabIndex = 122;
            this.Tb_nombre_individu.TabStop = false;
            this.Tb_nombre_individu.Text = "1 000 000";
            this.Tb_nombre_individu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 97;
            this.label3.Text = "Nombre de famille";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 96;
            this.label2.Text = "Nombre d\'individu";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 13);
            this.label1.TabIndex = 93;
            this.label1.Text = "Information du fichier GEDCOM";
            // 
            // RechercheIndividuTB
            // 
            this.RechercheIndividuTB.BackColor = System.Drawing.Color.White;
            this.RechercheIndividuTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RechercheIndividuTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RechercheIndividuTB.Location = new System.Drawing.Point(630, 45);
            this.RechercheIndividuTB.MaximumSize = new System.Drawing.Size(300, 26);
            this.RechercheIndividuTB.MinimumSize = new System.Drawing.Size(300, 26);
            this.RechercheIndividuTB.Name = "RechercheIndividuTB";
            this.RechercheIndividuTB.Size = new System.Drawing.Size(300, 22);
            this.RechercheIndividuTB.TabIndex = 1;
            this.RechercheIndividuTB.TextChanged += new System.EventHandler(this.RechercheIndividuTB_TextChanged);
            // 
            // RechercheFamilleTB
            // 
            this.RechercheFamilleTB.BackColor = System.Drawing.Color.White;
            this.RechercheFamilleTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RechercheFamilleTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RechercheFamilleTB.Location = new System.Drawing.Point(630, 334);
            this.RechercheFamilleTB.MaximumSize = new System.Drawing.Size(300, 26);
            this.RechercheFamilleTB.MinimumSize = new System.Drawing.Size(300, 26);
            this.RechercheFamilleTB.Name = "RechercheFamilleTB";
            this.RechercheFamilleTB.Size = new System.Drawing.Size(300, 22);
            this.RechercheFamilleTB.TabIndex = 4;
            this.RechercheFamilleTB.TextChanged += new System.EventHandler(this.RechercheConjointeTB_TextChanged);
            // 
            // LvChoixIndividu
            // 
            this.LvChoixIndividu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.LvChoixIndividu.Cursor = System.Windows.Forms.Cursors.Default;
            this.LvChoixIndividu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LvChoixIndividu.GridLines = true;
            this.LvChoixIndividu.HideSelection = false;
            this.LvChoixIndividu.LabelWrap = false;
            this.LvChoixIndividu.Location = new System.Drawing.Point(13, 88);
            this.LvChoixIndividu.MultiSelect = false;
            this.LvChoixIndividu.Name = "LvChoixIndividu";
            this.LvChoixIndividu.Size = new System.Drawing.Size(998, 210);
            this.LvChoixIndividu.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.LvChoixIndividu.TabIndex = 4;
            this.LvChoixIndividu.UseCompatibleStateImageBehavior = false;
            this.LvChoixIndividu.Visible = false;
            this.LvChoixIndividu.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LvChoixIndividu_ColumnClick);
            this.LvChoixIndividu.SelectedIndexChanged += new System.EventHandler(this.ChoixLVIndividu_SelectedIndexChanged);
            this.LvChoixIndividu.DoubleClick += new System.EventHandler(this.LvChoixIndividu_DoubleClick);
            // 
            // LvChoixFamille
            // 
            this.LvChoixFamille.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.LvChoixFamille.Cursor = System.Windows.Forms.Cursors.Default;
            this.LvChoixFamille.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LvChoixFamille.HideSelection = false;
            this.LvChoixFamille.Location = new System.Drawing.Point(12, 378);
            this.LvChoixFamille.Margin = new System.Windows.Forms.Padding(0);
            this.LvChoixFamille.MultiSelect = false;
            this.LvChoixFamille.Name = "LvChoixFamille";
            this.LvChoixFamille.Size = new System.Drawing.Size(998, 210);
            this.LvChoixFamille.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.LvChoixFamille.TabIndex = 8;
            this.LvChoixFamille.UseCompatibleStateImageBehavior = false;
            this.LvChoixFamille.Visible = false;
            this.LvChoixFamille.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LvChoixFamilleConjoint_ColumnClick);
            this.LvChoixFamille.SelectedIndexChanged += new System.EventHandler(this.LvChoixFamilleConjoint_SelectedIndexChanged);
            this.LvChoixFamille.DoubleClick += new System.EventHandler(this.LvChoixFamille_DoubleClick);
            // 
            // Lb_nombre_ligne
            // 
            this.Lb_nombre_ligne.AutoSize = true;
            this.Lb_nombre_ligne.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lb_nombre_ligne.Location = new System.Drawing.Point(1024, 562);
            this.Lb_nombre_ligne.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Lb_nombre_ligne.MaximumSize = new System.Drawing.Size(150, 40);
            this.Lb_nombre_ligne.MinimumSize = new System.Drawing.Size(150, 40);
            this.Lb_nombre_ligne.Name = "Lb_nombre_ligne";
            this.Lb_nombre_ligne.Size = new System.Drawing.Size(150, 40);
            this.Lb_nombre_ligne.TabIndex = 162;
            this.Lb_nombre_ligne.Text = "123 456 789";
            this.Lb_nombre_ligne.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Pb_attendre
            // 
            this.Pb_attendre.ErrorImage = null;
            this.Pb_attendre.Image = global::GH.Properties.Resources.spe340;
            this.Pb_attendre.InitialImage = null;
            this.Pb_attendre.Location = new System.Drawing.Point(1019, 525);
            this.Pb_attendre.Margin = new System.Windows.Forms.Padding(0);
            this.Pb_attendre.Name = "Pb_attendre";
            this.Pb_attendre.Size = new System.Drawing.Size(197, 22);
            this.Pb_attendre.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pb_attendre.TabIndex = 178;
            this.Pb_attendre.TabStop = false;
            this.Pb_attendre.Visible = false;
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
            this.Btn_annuler.Location = new System.Drawing.Point(1177, 552);
            this.Btn_annuler.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_annuler.Name = "Btn_annuler";
            this.Btn_annuler.Size = new System.Drawing.Size(38, 41);
            this.Btn_annuler.TabIndex = 177;
            this.Btn_annuler.UseVisualStyleBackColor = true;
            this.Btn_annuler.Click += new System.EventHandler(this.Btn_annuler_Click);
            // 
            // Btn_log_del
            // 
            this.Btn_log_del.AutoSize = true;
            this.Btn_log_del.BackgroundImage = global::GH.Properties.Resources.btn_log;
            this.Btn_log_del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_log_del.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_log_del.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_log_del.FlatAppearance.BorderSize = 0;
            this.Btn_log_del.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Btn_log_del.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Magenta;
            this.Btn_log_del.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_log_del.Location = new System.Drawing.Point(1019, 454);
            this.Btn_log_del.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_log_del.Name = "Btn_log_del";
            this.Btn_log_del.Size = new System.Drawing.Size(75, 65);
            this.Btn_log_del.TabIndex = 176;
            this.Btn_log_del.UseVisualStyleBackColor = true;
            this.Btn_log_del.Click += new System.EventHandler(this.Btn_log_del_Click);
            // 
            // Btn_total
            // 
            this.Btn_total.AutoSize = true;
            this.Btn_total.BackgroundImage = global::GH.Properties.Resources.Btn_total;
            this.Btn_total.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_total.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_total.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_total.FlatAppearance.BorderSize = 0;
            this.Btn_total.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Btn_total.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Magenta;
            this.Btn_total.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_total.Location = new System.Drawing.Point(1019, 367);
            this.Btn_total.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_total.Name = "Btn_total";
            this.Btn_total.Size = new System.Drawing.Size(206, 81);
            this.Btn_total.TabIndex = 175;
            this.Btn_total.UseVisualStyleBackColor = true;
            this.Btn_total.Click += new System.EventHandler(this.Btn__total_Click);
            // 
            // Btn_fiche_famille
            // 
            this.Btn_fiche_famille.AutoSize = true;
            this.Btn_fiche_famille.BackgroundImage = global::GH.Properties.Resources.Btn_famille;
            this.Btn_fiche_famille.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_fiche_famille.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_fiche_famille.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_fiche_famille.FlatAppearance.BorderSize = 0;
            this.Btn_fiche_famille.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Btn_fiche_famille.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Magenta;
            this.Btn_fiche_famille.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_fiche_famille.Location = new System.Drawing.Point(1157, 305);
            this.Btn_fiche_famille.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_fiche_famille.Name = "Btn_fiche_famille";
            this.Btn_fiche_famille.Size = new System.Drawing.Size(68, 57);
            this.Btn_fiche_famille.TabIndex = 174;
            this.Btn_fiche_famille.UseVisualStyleBackColor = true;
            this.Btn_fiche_famille.Click += new System.EventHandler(this.Btn_fiche_famille_Click);
            // 
            // Btn_fiche_individu
            // 
            this.Btn_fiche_individu.AutoSize = true;
            this.Btn_fiche_individu.BackgroundImage = global::GH.Properties.Resources.Btn_individu;
            this.Btn_fiche_individu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_fiche_individu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_fiche_individu.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_fiche_individu.FlatAppearance.BorderSize = 0;
            this.Btn_fiche_individu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Btn_fiche_individu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Magenta;
            this.Btn_fiche_individu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_fiche_individu.Location = new System.Drawing.Point(1019, 304);
            this.Btn_fiche_individu.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_fiche_individu.Name = "Btn_fiche_individu";
            this.Btn_fiche_individu.Size = new System.Drawing.Size(68, 57);
            this.Btn_fiche_individu.TabIndex = 173;
            this.Btn_fiche_individu.UseVisualStyleBackColor = true;
            this.Btn_fiche_individu.Click += new System.EventHandler(this.Btn_fiche_individu_Click);
            // 
            // Btn_famille_apres
            // 
            this.Btn_famille_apres.AutoSize = true;
            this.Btn_famille_apres.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Btn_famille_apres.BackgroundImage = global::GH.Properties.Resources.Btn_droite;
            this.Btn_famille_apres.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_famille_apres.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_famille_apres.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_famille_apres.FlatAppearance.BorderSize = 0;
            this.Btn_famille_apres.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Btn_famille_apres.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Magenta;
            this.Btn_famille_apres.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_famille_apres.Location = new System.Drawing.Point(973, 329);
            this.Btn_famille_apres.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_famille_apres.Name = "Btn_famille_apres";
            this.Btn_famille_apres.Size = new System.Drawing.Size(26, 28);
            this.Btn_famille_apres.TabIndex = 172;
            this.Btn_famille_apres.UseVisualStyleBackColor = false;
            this.Btn_famille_apres.Click += new System.EventHandler(this.Btn_famille_apres_Click);
            // 
            // Btn_famille_avant
            // 
            this.Btn_famille_avant.AutoSize = true;
            this.Btn_famille_avant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Btn_famille_avant.BackgroundImage = global::GH.Properties.Resources.Btn_gauche;
            this.Btn_famille_avant.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_famille_avant.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_famille_avant.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_famille_avant.FlatAppearance.BorderSize = 0;
            this.Btn_famille_avant.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Btn_famille_avant.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Magenta;
            this.Btn_famille_avant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_famille_avant.Location = new System.Drawing.Point(940, 329);
            this.Btn_famille_avant.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_famille_avant.Name = "Btn_famille_avant";
            this.Btn_famille_avant.Size = new System.Drawing.Size(26, 28);
            this.Btn_famille_avant.TabIndex = 171;
            this.Btn_famille_avant.UseVisualStyleBackColor = false;
            this.Btn_famille_avant.Click += new System.EventHandler(this.Btn_famille_avant_Click);
            // 
            // Btn_individu_apres
            // 
            this.Btn_individu_apres.AutoSize = true;
            this.Btn_individu_apres.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Btn_individu_apres.BackgroundImage = global::GH.Properties.Resources.Btn_droite;
            this.Btn_individu_apres.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_individu_apres.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_individu_apres.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_individu_apres.FlatAppearance.BorderSize = 0;
            this.Btn_individu_apres.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Btn_individu_apres.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Magenta;
            this.Btn_individu_apres.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_individu_apres.Location = new System.Drawing.Point(973, 39);
            this.Btn_individu_apres.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_individu_apres.Name = "Btn_individu_apres";
            this.Btn_individu_apres.Size = new System.Drawing.Size(26, 28);
            this.Btn_individu_apres.TabIndex = 170;
            this.Btn_individu_apres.UseVisualStyleBackColor = false;
            this.Btn_individu_apres.Click += new System.EventHandler(this.Btn__individu_apres_Click);
            // 
            // Btn_individu_avant
            // 
            this.Btn_individu_avant.AutoSize = true;
            this.Btn_individu_avant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Btn_individu_avant.BackgroundImage = global::GH.Properties.Resources.Btn_gauche;
            this.Btn_individu_avant.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_individu_avant.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_individu_avant.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_individu_avant.FlatAppearance.BorderSize = 0;
            this.Btn_individu_avant.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Btn_individu_avant.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Magenta;
            this.Btn_individu_avant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_individu_avant.Location = new System.Drawing.Point(940, 39);
            this.Btn_individu_avant.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_individu_avant.Name = "Btn_individu_avant";
            this.Btn_individu_avant.Size = new System.Drawing.Size(26, 28);
            this.Btn_individu_avant.TabIndex = 169;
            this.Btn_individu_avant.UseVisualStyleBackColor = false;
            this.Btn_individu_avant.Click += new System.EventHandler(this.Btn_individu_avant_Click);
            // 
            // Btn_balise
            // 
            this.Btn_balise.AutoSize = true;
            this.Btn_balise.BackgroundImage = global::GH.Properties.Resources.Btn_B;
            this.Btn_balise.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_balise.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_balise.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_balise.FlatAppearance.BorderSize = 0;
            this.Btn_balise.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Btn_balise.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Magenta;
            this.Btn_balise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_balise.Location = new System.Drawing.Point(1177, 245);
            this.Btn_balise.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_balise.Name = "Btn_balise";
            this.Btn_balise.Size = new System.Drawing.Size(48, 52);
            this.Btn_balise.TabIndex = 168;
            this.Btn_balise.UseVisualStyleBackColor = true;
            this.Btn_balise.Click += new System.EventHandler(this.Btn_balise_Click);
            // 
            // Btn_erreur
            // 
            this.Btn_erreur.AutoSize = true;
            this.Btn_erreur.BackgroundImage = global::GH.Properties.Resources.Btn_E;
            this.Btn_erreur.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_erreur.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_erreur.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_erreur.FlatAppearance.BorderSize = 0;
            this.Btn_erreur.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Btn_erreur.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Magenta;
            this.Btn_erreur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_erreur.Location = new System.Drawing.Point(1126, 245);
            this.Btn_erreur.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_erreur.Name = "Btn_erreur";
            this.Btn_erreur.Size = new System.Drawing.Size(48, 52);
            this.Btn_erreur.TabIndex = 167;
            this.Btn_erreur.UseVisualStyleBackColor = true;
            this.Btn_erreur.Click += new System.EventHandler(this.Btn_erreur_Click);
            // 
            // Btn_debug
            // 
            this.Btn_debug.AutoSize = true;
            this.Btn_debug.BackgroundImage = global::GH.Properties.Resources.btn_D;
            this.Btn_debug.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_debug.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_debug.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_debug.FlatAppearance.BorderSize = 0;
            this.Btn_debug.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Btn_debug.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Magenta;
            this.Btn_debug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_debug.Location = new System.Drawing.Point(1072, 245);
            this.Btn_debug.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_debug.Name = "Btn_debug";
            this.Btn_debug.Size = new System.Drawing.Size(48, 52);
            this.Btn_debug.TabIndex = 166;
            this.Btn_debug.UseVisualStyleBackColor = true;
            this.Btn_debug.Click += new System.EventHandler(this.Btn_debug_Click);
            // 
            // Btn_GEDCOM
            // 
            this.Btn_GEDCOM.AutoSize = true;
            this.Btn_GEDCOM.BackgroundImage = global::GH.Properties.Resources.btn_G;
            this.Btn_GEDCOM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_GEDCOM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_GEDCOM.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_GEDCOM.FlatAppearance.BorderSize = 0;
            this.Btn_GEDCOM.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Btn_GEDCOM.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Magenta;
            this.Btn_GEDCOM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_GEDCOM.Location = new System.Drawing.Point(1019, 245);
            this.Btn_GEDCOM.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_GEDCOM.Name = "Btn_GEDCOM";
            this.Btn_GEDCOM.Size = new System.Drawing.Size(48, 52);
            this.Btn_GEDCOM.TabIndex = 165;
            this.Btn_GEDCOM.UseVisualStyleBackColor = true;
            this.Btn_GEDCOM.Click += new System.EventHandler(this.Btn_G_Click);
            // 
            // RechercheFamilleB
            // 
            this.RechercheFamilleB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RechercheFamilleB.BackgroundImage = global::GH.Properties.Resources.loupe20;
            this.RechercheFamilleB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.RechercheFamilleB.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RechercheFamilleB.FlatAppearance.BorderSize = 0;
            this.RechercheFamilleB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RechercheFamilleB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RechercheFamilleB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RechercheFamilleB.ForeColor = System.Drawing.Color.Transparent;
            this.RechercheFamilleB.Location = new System.Drawing.Point(597, 328);
            this.RechercheFamilleB.Name = "RechercheFamilleB";
            this.RechercheFamilleB.Size = new System.Drawing.Size(30, 30);
            this.RechercheFamilleB.TabIndex = 112;
            this.RechercheFamilleB.TabStop = false;
            this.RechercheFamilleB.UseVisualStyleBackColor = false;
            // 
            // RechercheIndividuB
            // 
            this.RechercheIndividuB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RechercheIndividuB.BackgroundImage = global::GH.Properties.Resources.loupe20;
            this.RechercheIndividuB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.RechercheIndividuB.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RechercheIndividuB.FlatAppearance.BorderSize = 0;
            this.RechercheIndividuB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RechercheIndividuB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RechercheIndividuB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RechercheIndividuB.ForeColor = System.Drawing.Color.Transparent;
            this.RechercheIndividuB.Location = new System.Drawing.Point(597, 38);
            this.RechercheIndividuB.Name = "RechercheIndividuB";
            this.RechercheIndividuB.Size = new System.Drawing.Size(30, 30);
            this.RechercheIndividuB.TabIndex = 107;
            this.RechercheIndividuB.TabStop = false;
            this.RechercheIndividuB.Text = " ";
            this.RechercheIndividuB.UseVisualStyleBackColor = false;
            // 
            // Pb_recherche_individu
            // 
            this.Pb_recherche_individu.Image = global::GH.Properties.Resources.recherche;
            this.Pb_recherche_individu.Location = new System.Drawing.Point(590, 30);
            this.Pb_recherche_individu.Margin = new System.Windows.Forms.Padding(2);
            this.Pb_recherche_individu.Name = "Pb_recherche_individu";
            this.Pb_recherche_individu.Size = new System.Drawing.Size(420, 49);
            this.Pb_recherche_individu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pb_recherche_individu.TabIndex = 163;
            this.Pb_recherche_individu.TabStop = false;
            // 
            // Pb_recherche_famille
            // 
            this.Pb_recherche_famille.Image = global::GH.Properties.Resources.recherche;
            this.Pb_recherche_famille.Location = new System.Drawing.Point(590, 319);
            this.Pb_recherche_famille.Margin = new System.Windows.Forms.Padding(2);
            this.Pb_recherche_famille.Name = "Pb_recherche_famille";
            this.Pb_recherche_famille.Size = new System.Drawing.Size(420, 49);
            this.Pb_recherche_famille.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pb_recherche_famille.TabIndex = 164;
            this.Pb_recherche_famille.TabStop = false;
            // 
            // Cadre
            // 
            this.Cadre.BackColor = System.Drawing.Color.Transparent;
            this.Cadre.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Cadre.FlatAppearance.BorderSize = 3;
            this.Cadre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cadre.Location = new System.Drawing.Point(1019, 33);
            this.Cadre.Name = "Cadre";
            this.Cadre.Size = new System.Drawing.Size(206, 206);
            this.Cadre.TabIndex = 180;
            this.Cadre.UseVisualStyleBackColor = false;
            this.Cadre.Click += new System.EventHandler(this.cadre_Click);
            // 
            // GHClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1234, 606);
            this.Controls.Add(this.Gb_info_GEDCOM);
            this.Controls.Add(this.Cadre);
            this.Controls.Add(this.Pb_attendre);
            this.Controls.Add(this.Btn_annuler);
            this.Controls.Add(this.Btn_log_del);
            this.Controls.Add(this.Btn_total);
            this.Controls.Add(this.Btn_fiche_famille);
            this.Controls.Add(this.Btn_fiche_individu);
            this.Controls.Add(this.Btn_famille_apres);
            this.Controls.Add(this.Btn_famille_avant);
            this.Controls.Add(this.Btn_individu_apres);
            this.Controls.Add(this.Btn_individu_avant);
            this.Controls.Add(this.Btn_balise);
            this.Controls.Add(this.Btn_erreur);
            this.Controls.Add(this.Btn_debug);
            this.Controls.Add(this.Btn_GEDCOM);
            this.Controls.Add(this.Lb_nombre_ligne);
            this.Controls.Add(this.RechercheFamilleB);
            this.Controls.Add(this.RechercheFamilleTB);
            this.Controls.Add(this.RechercheIndividuB);
            this.Controls.Add(this.RechercheIndividuTB);
            this.Controls.Add(this.lpIndividu);
            this.Controls.Add(this.lbFamilleConjoint);
            this.Controls.Add(this.LvChoixIndividu);
            this.Controls.Add(this.menuPrincipal);
            this.Controls.Add(this.LvChoixFamille);
            this.Controls.Add(this.Pb_recherche_individu);
            this.Controls.Add(this.Pb_recherche_famille);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuPrincipal;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1250, 645);
            this.MinimumSize = new System.Drawing.Size(1250, 645);
            this.Name = "GHClass";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GH_FormClosing);
            this.Load += new System.EventHandler(this.GH_Load);
            this.menuPrincipal.ResumeLayout(false);
            this.menuPrincipal.PerformLayout();
            this.Gb_info_GEDCOM.ResumeLayout(false);
            this.Gb_info_GEDCOM.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pb_attendre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pb_recherche_individu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pb_recherche_famille)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OuvrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem QuitterToolStripMenuItem;
        private System.Windows.Forms.Label lblVersionProgramme;
        private System.Windows.Forms.Label Lb_fichier_GEDCOM;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblVersionGEDCOM;
        private System.Windows.Forms.Label lblCharSet;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.Label lblDateHeure;
        private System.Windows.Forms.Label Lb_nom_programe;
        private System.Windows.Forms.Label lbFichierGenererPar;
        private System.Windows.Forms.Label lbFamilleConjoint;
        private System.Windows.Forms.Label lpIndividu;
        private System.Windows.Forms.GroupBox Gb_info_GEDCOM;
        private System.Windows.Forms.ToolStripMenuItem ParamètresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AideToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem AProposToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem OuvrirDossierDesFichesHTMLToolStripMenuItem;
        private System.Windows.Forms.TextBox RechercheIndividuTB;
        private System.Windows.Forms.TextBox RechercheFamilleTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Tb_nombre_individu;
        private System.Windows.Forms.TextBox Tb_nombre_famille;
        public System.Windows.Forms.ListView LvChoixIndividu;
        private System.DirectoryServices.DirectoryEntry directoryEntry1;
        private System.Windows.Forms.ToolStripMenuItem BienvenuToolStripMenuItem;
        public System.Windows.Forms.ListView LvChoixFamille;
        private System.Windows.Forms.Button RechercheFamilleB;
        private System.Windows.Forms.Button RechercheIndividuB;
        private System.Windows.Forms.Label Lb_nombre_ligne;
        private System.Windows.Forms.PictureBox Pb_recherche_individu;
        private System.Windows.Forms.PictureBox Pb_recherche_famille;
        private System.Windows.Forms.Button Btn_GEDCOM;
        private System.Windows.Forms.Button Btn_debug;
        private System.Windows.Forms.Button Btn_erreur;
        private System.Windows.Forms.Button Btn_balise;
        private System.Windows.Forms.Button Btn_individu_avant;
        private System.Windows.Forms.Button Btn_individu_apres;
        private System.Windows.Forms.Button Btn_famille_apres;
        private System.Windows.Forms.Button Btn_famille_avant;
        private System.Windows.Forms.Button Btn_fiche_individu;
        private System.Windows.Forms.Button Btn_fiche_famille;
        private System.Windows.Forms.Button Btn_total;
        private System.Windows.Forms.Button Btn_log_del;
        private System.Windows.Forms.Button Btn_annuler;
        private System.Windows.Forms.PictureBox Pb_attendre;
        private System.Windows.Forms.Button Cadre;
        private System.Windows.Forms.TextBox Tb_temps_execution;
        private System.Windows.Forms.Label label4;
    }
}

