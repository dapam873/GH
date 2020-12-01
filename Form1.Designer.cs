namespace GH
{
    partial class GH
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GH));
            this.menuPrincipal = new System.Windows.Forms.MenuStrip();
            this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ouvrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ouvrirDossierDesFichesHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ParamètresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aideToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.àProposToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LvChoixFamille = new System.Windows.Forms.ListView();
            this.lblVersionProgramme = new System.Windows.Forms.Label();
            this.lblFichier = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblVersionGEDCOM = new System.Windows.Forms.Label();
            this.lblCharSet = new System.Windows.Forms.Label();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.lblDateHeure = new System.Windows.Forms.Label();
            this.lblNom = new System.Windows.Forms.Label();
            this.lbFichierGenererPar = new System.Windows.Forms.Label();
            this.lbFamilleConjoint = new System.Windows.Forms.Label();
            this.lpIndividu = new System.Windows.Forms.Label();
            this.Gb_info_GEDCOM = new System.Windows.Forms.GroupBox();
            this.NombreFamilleTb = new System.Windows.Forms.TextBox();
            this.NombreIndividuTb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_voir_fiche_individu = new System.Windows.Forms.Button();
            this.Tb_Status = new System.Windows.Forms.TextBox();
            this.RechercheIndividuTB = new System.Windows.Forms.TextBox();
            this.RechercheFamilleTB = new System.Windows.Forms.TextBox();
            this.LvChoixIndividu = new System.Windows.Forms.ListView();
            this.AvantFamilleB = new System.Windows.Forms.Button();
            this.ApresFamilleB = new System.Windows.Forms.Button();
            this.RechercheFamilleB = new System.Windows.Forms.Button();
            this.ApresIndividuB = new System.Windows.Forms.Button();
            this.AvantIndividuB = new System.Windows.Forms.Button();
            this.RechercheIndividuB = new System.Windows.Forms.Button();
            this.Lb_HTML_1 = new System.Windows.Forms.Label();
            this.Lb_HTML_2 = new System.Windows.Forms.Label();
            this.Lb_HTML_3 = new System.Windows.Forms.Label();
            this.Lb_HTML_4 = new System.Windows.Forms.Label();
            this.Lb_animation = new System.Windows.Forms.Label();
            this.Btn_total = new System.Windows.Forms.Button();
            this.Btn_annuler = new System.Windows.Forms.Button();
            this.Btn_annuler_HTML = new System.Windows.Forms.Button();
            this.Btn_voir_fiche_famille = new System.Windows.Forms.Button();
            this.Btn_deboguer = new System.Windows.Forms.Button();
            this.Btn_balise = new System.Windows.Forms.Button();
            this.Btn_erreur = new System.Windows.Forms.Button();
            this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
            this.Btn_cadre_individu = new System.Windows.Forms.Button();
            this.Btn_cadre_famille = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuPrincipal.SuspendLayout();
            this.Gb_info_GEDCOM.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuPrincipal
            // 
            this.menuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem,
            this.ParamètresToolStripMenuItem,
            this.aideToolStripMenuItem});
            this.menuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuPrincipal.Name = "menuPrincipal";
            this.menuPrincipal.Size = new System.Drawing.Size(1235, 24);
            this.menuPrincipal.TabIndex = 0;
            this.menuPrincipal.Text = "dasdsad";
            // 
            // fichierToolStripMenuItem
            // 
            this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ouvrirToolStripMenuItem,
            this.ouvrirDossierDesFichesHTMLToolStripMenuItem,
            this.quitterToolStripMenuItem});
            this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            this.fichierToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.fichierToolStripMenuItem.Text = "&Fichier";
            this.fichierToolStripMenuItem.Click += new System.EventHandler(this.FichierToolStripMenuItem_Click);
            // 
            // ouvrirToolStripMenuItem
            // 
            this.ouvrirToolStripMenuItem.Name = "ouvrirToolStripMenuItem";
            this.ouvrirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.ouvrirToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.ouvrirToolStripMenuItem.Text = "&Ouvrir";
            this.ouvrirToolStripMenuItem.Click += new System.EventHandler(this.OuvrirToolStripMenuItem_Click);
            // 
            // ouvrirDossierDesFichesHTMLToolStripMenuItem
            // 
            this.ouvrirDossierDesFichesHTMLToolStripMenuItem.Name = "ouvrirDossierDesFichesHTMLToolStripMenuItem";
            this.ouvrirDossierDesFichesHTMLToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.H)));
            this.ouvrirDossierDesFichesHTMLToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.ouvrirDossierDesFichesHTMLToolStripMenuItem.Text = "Ouvrir le &dossier des fiches HTML";
            this.ouvrirDossierDesFichesHTMLToolStripMenuItem.Click += new System.EventHandler(this.OuvrirDossierDesFichesHTMLToolStripMenuItem_Click);
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.quitterToolStripMenuItem.Text = "&Quitter";
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
            // aideToolStripMenuItem
            // 
            this.aideToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aideToolStripMenuItem1,
            this.àProposToolStripMenuItem});
            this.aideToolStripMenuItem.Name = "aideToolStripMenuItem";
            this.aideToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.aideToolStripMenuItem.Text = "&Aide";
            // 
            // aideToolStripMenuItem1
            // 
            this.aideToolStripMenuItem1.Name = "aideToolStripMenuItem1";
            this.aideToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.aideToolStripMenuItem1.Text = "&Aide";
            this.aideToolStripMenuItem1.Click += new System.EventHandler(this.AideToolStripMenuItem1_Click);
            // 
            // àProposToolStripMenuItem
            // 
            this.àProposToolStripMenuItem.Name = "àProposToolStripMenuItem";
            this.àProposToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.àProposToolStripMenuItem.Text = "À &propos";
            this.àProposToolStripMenuItem.Click += new System.EventHandler(this.ÀProposToolStripMenuItem_Click);
            // 
            // LvChoixFamille
            // 
            this.LvChoixFamille.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.LvChoixFamille.Cursor = System.Windows.Forms.Cursors.Default;
            this.LvChoixFamille.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LvChoixFamille.HideSelection = false;
            this.LvChoixFamille.Location = new System.Drawing.Point(12, 291);
            this.LvChoixFamille.MultiSelect = false;
            this.LvChoixFamille.Name = "LvChoixFamille";
            this.LvChoixFamille.Size = new System.Drawing.Size(1011, 160);
            this.LvChoixFamille.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.LvChoixFamille.TabIndex = 8;
            this.LvChoixFamille.UseCompatibleStateImageBehavior = false;
            this.LvChoixFamille.Visible = false;
            this.LvChoixFamille.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LvChoixFamilleConjoint_ColumnClick);
            this.LvChoixFamille.SelectedIndexChanged += new System.EventHandler(this.LvChoixFamilleConjoint_SelectedIndexChanged);
            this.LvChoixFamille.DoubleClick += new System.EventHandler(this.LvChoixFamille_DoubleClick);
            // 
            // lblVersionProgramme
            // 
            this.lblVersionProgramme.AutoSize = true;
            this.lblVersionProgramme.Location = new System.Drawing.Point(4, 61);
            this.lblVersionProgramme.Name = "lblVersionProgramme";
            this.lblVersionProgramme.Size = new System.Drawing.Size(105, 13);
            this.lblVersionProgramme.TabIndex = 81;
            this.lblVersionProgramme.Text = "lblVersionProgramme";
            // 
            // lblFichier
            // 
            this.lblFichier.AutoSize = true;
            this.lblFichier.Location = new System.Drawing.Point(4, 83);
            this.lblFichier.Name = "lblFichier";
            this.lblFichier.Size = new System.Drawing.Size(48, 13);
            this.lblFichier.TabIndex = 82;
            this.lblFichier.Text = "lblFichier";
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Location = new System.Drawing.Point(4, 117);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(61, 13);
            this.lblCopyright.TabIndex = 83;
            this.lblCopyright.Text = "lblCopyright";
            // 
            // lblVersionGEDCOM
            // 
            this.lblVersionGEDCOM.AutoSize = true;
            this.lblVersionGEDCOM.Location = new System.Drawing.Point(4, 106);
            this.lblVersionGEDCOM.Name = "lblVersionGEDCOM";
            this.lblVersionGEDCOM.Size = new System.Drawing.Size(99, 13);
            this.lblVersionGEDCOM.TabIndex = 85;
            this.lblVersionGEDCOM.Text = "lblVersionGEDCOM";
            // 
            // lblCharSet
            // 
            this.lblCharSet.AutoSize = true;
            this.lblCharSet.Location = new System.Drawing.Point(4, 136);
            this.lblCharSet.Name = "lblCharSet";
            this.lblCharSet.Size = new System.Drawing.Size(55, 13);
            this.lblCharSet.TabIndex = 86;
            this.lblCharSet.Text = "lblCharSet";
            this.lblCharSet.Click += new System.EventHandler(this.LblCharSet_Click);
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(4, 149);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(65, 13);
            this.lblLanguage.TabIndex = 87;
            this.lblLanguage.Text = "lblLanguage";
            // 
            // lblDateHeure
            // 
            this.lblDateHeure.AutoSize = true;
            this.lblDateHeure.Location = new System.Drawing.Point(4, 94);
            this.lblDateHeure.Name = "lblDateHeure";
            this.lblDateHeure.Size = new System.Drawing.Size(69, 13);
            this.lblDateHeure.TabIndex = 90;
            this.lblDateHeure.Text = "lblDateHeure";
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(19, 46);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(39, 13);
            this.lblNom.TabIndex = 91;
            this.lblNom.Text = "lblNom";
            // 
            // lbFichierGenererPar
            // 
            this.lbFichierGenererPar.AutoSize = true;
            this.lbFichierGenererPar.Location = new System.Drawing.Point(4, 33);
            this.lbFichierGenererPar.Name = "lbFichierGenererPar";
            this.lbFichierGenererPar.Size = new System.Drawing.Size(95, 13);
            this.lbFichierGenererPar.TabIndex = 92;
            this.lbFichierGenererPar.Text = "Fichier générer par";
            // 
            // lbFamilleConjoint
            // 
            this.lbFamilleConjoint.AutoSize = true;
            this.lbFamilleConjoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFamilleConjoint.Location = new System.Drawing.Point(13, 269);
            this.lbFamilleConjoint.Name = "lbFamilleConjoint";
            this.lbFamilleConjoint.Size = new System.Drawing.Size(66, 20);
            this.lbFamilleConjoint.TabIndex = 93;
            this.lbFamilleConjoint.Text = "Famille";
            // 
            // lpIndividu
            // 
            this.lpIndividu.AutoSize = true;
            this.lpIndividu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lpIndividu.Location = new System.Drawing.Point(13, 49);
            this.lpIndividu.Name = "lpIndividu";
            this.lpIndividu.Size = new System.Drawing.Size(71, 20);
            this.lpIndividu.TabIndex = 96;
            this.lpIndividu.Text = "Individu";
            // 
            // Gb_info_GEDCOM
            // 
            this.Gb_info_GEDCOM.BackColor = System.Drawing.Color.Transparent;
            this.Gb_info_GEDCOM.Controls.Add(this.NombreFamilleTb);
            this.Gb_info_GEDCOM.Controls.Add(this.NombreIndividuTb);
            this.Gb_info_GEDCOM.Controls.Add(this.label3);
            this.Gb_info_GEDCOM.Controls.Add(this.label2);
            this.Gb_info_GEDCOM.Controls.Add(this.label1);
            this.Gb_info_GEDCOM.Controls.Add(this.lblNom);
            this.Gb_info_GEDCOM.Controls.Add(this.lblDateHeure);
            this.Gb_info_GEDCOM.Controls.Add(this.lblVersionProgramme);
            this.Gb_info_GEDCOM.Controls.Add(this.lbFichierGenererPar);
            this.Gb_info_GEDCOM.Controls.Add(this.lblCopyright);
            this.Gb_info_GEDCOM.Controls.Add(this.lblCharSet);
            this.Gb_info_GEDCOM.Controls.Add(this.lblVersionGEDCOM);
            this.Gb_info_GEDCOM.Controls.Add(this.lblFichier);
            this.Gb_info_GEDCOM.Controls.Add(this.lblLanguage);
            this.Gb_info_GEDCOM.ForeColor = System.Drawing.Color.Black;
            this.Gb_info_GEDCOM.Location = new System.Drawing.Point(1038, 27);
            this.Gb_info_GEDCOM.Margin = new System.Windows.Forms.Padding(10);
            this.Gb_info_GEDCOM.Name = "Gb_info_GEDCOM";
            this.Gb_info_GEDCOM.Size = new System.Drawing.Size(191, 204);
            this.Gb_info_GEDCOM.TabIndex = 104;
            this.Gb_info_GEDCOM.TabStop = false;
            // 
            // NombreFamilleTb
            // 
            this.NombreFamilleTb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.NombreFamilleTb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NombreFamilleTb.Location = new System.Drawing.Point(105, 179);
            this.NombreFamilleTb.Name = "NombreFamilleTb";
            this.NombreFamilleTb.Size = new System.Drawing.Size(58, 13);
            this.NombreFamilleTb.TabIndex = 123;
            this.NombreFamilleTb.TabStop = false;
            this.NombreFamilleTb.Text = "1 000 000";
            this.NombreFamilleTb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // NombreIndividuTb
            // 
            this.NombreIndividuTb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.NombreIndividuTb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NombreIndividuTb.Location = new System.Drawing.Point(105, 166);
            this.NombreIndividuTb.Name = "NombreIndividuTb";
            this.NombreIndividuTb.Size = new System.Drawing.Size(58, 13);
            this.NombreIndividuTb.TabIndex = 122;
            this.NombreIndividuTb.TabStop = false;
            this.NombreIndividuTb.Text = "1 000 000";
            this.NombreIndividuTb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 97;
            this.label3.Text = "Nombre de famille";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 166);
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
            // Btn_voir_fiche_individu
            // 
            this.Btn_voir_fiche_individu.BackColor = System.Drawing.Color.Blue;
            this.Btn_voir_fiche_individu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_voir_fiche_individu.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.Btn_voir_fiche_individu.FlatAppearance.BorderSize = 0;
            this.Btn_voir_fiche_individu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.Btn_voir_fiche_individu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Btn_voir_fiche_individu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_voir_fiche_individu.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Bold);
            this.Btn_voir_fiche_individu.ForeColor = System.Drawing.Color.White;
            this.Btn_voir_fiche_individu.Image = ((System.Drawing.Image)(resources.GetObject("Btn_voir_fiche_individu.Image")));
            this.Btn_voir_fiche_individu.Location = new System.Drawing.Point(1046, 305);
            this.Btn_voir_fiche_individu.Name = "Btn_voir_fiche_individu";
            this.Btn_voir_fiche_individu.Size = new System.Drawing.Size(58, 44);
            this.Btn_voir_fiche_individu.TabIndex = 124;
            this.Btn_voir_fiche_individu.Tag = "";
            this.Btn_voir_fiche_individu.UseVisualStyleBackColor = false;
            this.Btn_voir_fiche_individu.Click += new System.EventHandler(this.Btn_voir_fiche_Click);
            this.Btn_voir_fiche_individu.MouseHover += new System.EventHandler(this.Btn_voir_fiche_individu_MouseHover);
            // 
            // Tb_Status
            // 
            this.Tb_Status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.Tb_Status.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Tb_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tb_Status.Location = new System.Drawing.Point(19, 144);
            this.Tb_Status.Name = "Tb_Status";
            this.Tb_Status.Size = new System.Drawing.Size(997, 15);
            this.Tb_Status.TabIndex = 68;
            this.Tb_Status.Text = "Lecture fichier GEDCOM";
            this.Tb_Status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // RechercheIndividuTB
            // 
            this.RechercheIndividuTB.BackColor = System.Drawing.Color.White;
            this.RechercheIndividuTB.Location = new System.Drawing.Point(693, 37);
            this.RechercheIndividuTB.Name = "RechercheIndividuTB";
            this.RechercheIndividuTB.Size = new System.Drawing.Size(257, 20);
            this.RechercheIndividuTB.TabIndex = 1;
            this.RechercheIndividuTB.TextChanged += new System.EventHandler(this.RechercheIndividuTB_TextChanged);
            // 
            // RechercheFamilleTB
            // 
            this.RechercheFamilleTB.BackColor = System.Drawing.Color.White;
            this.RechercheFamilleTB.Location = new System.Drawing.Point(693, 254);
            this.RechercheFamilleTB.Name = "RechercheFamilleTB";
            this.RechercheFamilleTB.Size = new System.Drawing.Size(257, 20);
            this.RechercheFamilleTB.TabIndex = 5;
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
            this.LvChoixIndividu.Location = new System.Drawing.Point(12, 71);
            this.LvChoixIndividu.MultiSelect = false;
            this.LvChoixIndividu.Name = "LvChoixIndividu";
            this.LvChoixIndividu.Size = new System.Drawing.Size(1011, 160);
            this.LvChoixIndividu.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.LvChoixIndividu.TabIndex = 4;
            this.LvChoixIndividu.UseCompatibleStateImageBehavior = false;
            this.LvChoixIndividu.Visible = false;
            this.LvChoixIndividu.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LvChoixIndividu_ColumnClick);
            this.LvChoixIndividu.SelectedIndexChanged += new System.EventHandler(this.ChoixLVIndividu_SelectedIndexChanged);
            this.LvChoixIndividu.DoubleClick += new System.EventHandler(this.LvChoixIndividu_DoubleClick);
            // 
            // AvantFamilleB
            // 
            this.AvantFamilleB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AvantFamilleB.BackgroundImage = global::GH.Properties.Resources.flecheGauche_22_20;
            this.AvantFamilleB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.AvantFamilleB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AvantFamilleB.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.AvantFamilleB.FlatAppearance.BorderSize = 0;
            this.AvantFamilleB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AvantFamilleB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AvantFamilleB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AvantFamilleB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AvantFamilleB.ForeColor = System.Drawing.Color.Transparent;
            this.AvantFamilleB.Location = new System.Drawing.Point(957, 253);
            this.AvantFamilleB.Name = "AvantFamilleB";
            this.AvantFamilleB.Size = new System.Drawing.Size(22, 20);
            this.AvantFamilleB.TabIndex = 6;
            this.AvantFamilleB.UseVisualStyleBackColor = false;
            this.AvantFamilleB.Click += new System.EventHandler(this.AvantConjointeB_Click);
            // 
            // ApresFamilleB
            // 
            this.ApresFamilleB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ApresFamilleB.BackgroundImage = global::GH.Properties.Resources.flecheDroite_22_20;
            this.ApresFamilleB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ApresFamilleB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ApresFamilleB.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.ApresFamilleB.FlatAppearance.BorderSize = 0;
            this.ApresFamilleB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ApresFamilleB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ApresFamilleB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ApresFamilleB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApresFamilleB.ForeColor = System.Drawing.Color.Transparent;
            this.ApresFamilleB.Location = new System.Drawing.Point(992, 253);
            this.ApresFamilleB.Name = "ApresFamilleB";
            this.ApresFamilleB.Size = new System.Drawing.Size(22, 20);
            this.ApresFamilleB.TabIndex = 7;
            this.ApresFamilleB.UseVisualStyleBackColor = false;
            this.ApresFamilleB.Click += new System.EventHandler(this.ApresConjointeB_Click);
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
            this.RechercheFamilleB.Location = new System.Drawing.Point(662, 250);
            this.RechercheFamilleB.Name = "RechercheFamilleB";
            this.RechercheFamilleB.Size = new System.Drawing.Size(30, 30);
            this.RechercheFamilleB.TabIndex = 112;
            this.RechercheFamilleB.TabStop = false;
            this.RechercheFamilleB.UseVisualStyleBackColor = false;
            // 
            // ApresIndividuB
            // 
            this.ApresIndividuB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ApresIndividuB.BackgroundImage = global::GH.Properties.Resources.flecheDroite_22_20;
            this.ApresIndividuB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ApresIndividuB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ApresIndividuB.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.ApresIndividuB.FlatAppearance.BorderSize = 0;
            this.ApresIndividuB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ApresIndividuB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ApresIndividuB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ApresIndividuB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApresIndividuB.ForeColor = System.Drawing.Color.Transparent;
            this.ApresIndividuB.Location = new System.Drawing.Point(992, 37);
            this.ApresIndividuB.Name = "ApresIndividuB";
            this.ApresIndividuB.Size = new System.Drawing.Size(22, 20);
            this.ApresIndividuB.TabIndex = 3;
            this.ApresIndividuB.UseVisualStyleBackColor = false;
            this.ApresIndividuB.Click += new System.EventHandler(this.ApresIndividuB_Click);
            // 
            // AvantIndividuB
            // 
            this.AvantIndividuB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AvantIndividuB.BackgroundImage = global::GH.Properties.Resources.flecheGauche_22_20;
            this.AvantIndividuB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.AvantIndividuB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AvantIndividuB.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.AvantIndividuB.FlatAppearance.BorderSize = 0;
            this.AvantIndividuB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AvantIndividuB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AvantIndividuB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AvantIndividuB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AvantIndividuB.ForeColor = System.Drawing.Color.Transparent;
            this.AvantIndividuB.Location = new System.Drawing.Point(957, 37);
            this.AvantIndividuB.Name = "AvantIndividuB";
            this.AvantIndividuB.Size = new System.Drawing.Size(22, 20);
            this.AvantIndividuB.TabIndex = 2;
            this.AvantIndividuB.UseVisualStyleBackColor = false;
            this.AvantIndividuB.Click += new System.EventHandler(this.AvantIndividuB_Click);
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
            this.RechercheIndividuB.Location = new System.Drawing.Point(661, 32);
            this.RechercheIndividuB.Name = "RechercheIndividuB";
            this.RechercheIndividuB.Size = new System.Drawing.Size(30, 30);
            this.RechercheIndividuB.TabIndex = 107;
            this.RechercheIndividuB.TabStop = false;
            this.RechercheIndividuB.Text = " ";
            this.RechercheIndividuB.UseVisualStyleBackColor = false;
            // 
            // Lb_HTML_1
            // 
            this.Lb_HTML_1.AutoSize = true;
            this.Lb_HTML_1.Location = new System.Drawing.Point(1043, 275);
            this.Lb_HTML_1.MaximumSize = new System.Drawing.Size(180, 13);
            this.Lb_HTML_1.MinimumSize = new System.Drawing.Size(180, 13);
            this.Lb_HTML_1.Name = "Lb_HTML_1";
            this.Lb_HTML_1.Size = new System.Drawing.Size(180, 13);
            this.Lb_HTML_1.TabIndex = 128;
            this.Lb_HTML_1.Text = "HTML 1";
            this.Lb_HTML_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lb_HTML_2
            // 
            this.Lb_HTML_2.AutoSize = true;
            this.Lb_HTML_2.Location = new System.Drawing.Point(1043, 294);
            this.Lb_HTML_2.MaximumSize = new System.Drawing.Size(180, 13);
            this.Lb_HTML_2.MinimumSize = new System.Drawing.Size(180, 13);
            this.Lb_HTML_2.Name = "Lb_HTML_2";
            this.Lb_HTML_2.Size = new System.Drawing.Size(180, 13);
            this.Lb_HTML_2.TabIndex = 129;
            this.Lb_HTML_2.Text = "HTML 2";
            this.Lb_HTML_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lb_HTML_3
            // 
            this.Lb_HTML_3.AutoSize = true;
            this.Lb_HTML_3.Location = new System.Drawing.Point(1043, 313);
            this.Lb_HTML_3.MaximumSize = new System.Drawing.Size(180, 13);
            this.Lb_HTML_3.MinimumSize = new System.Drawing.Size(180, 13);
            this.Lb_HTML_3.Name = "Lb_HTML_3";
            this.Lb_HTML_3.Size = new System.Drawing.Size(180, 13);
            this.Lb_HTML_3.TabIndex = 130;
            this.Lb_HTML_3.Text = "HTML 3";
            this.Lb_HTML_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lb_HTML_4
            // 
            this.Lb_HTML_4.AutoSize = true;
            this.Lb_HTML_4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lb_HTML_4.ForeColor = System.Drawing.Color.Blue;
            this.Lb_HTML_4.Location = new System.Drawing.Point(1043, 332);
            this.Lb_HTML_4.MaximumSize = new System.Drawing.Size(180, 30);
            this.Lb_HTML_4.MinimumSize = new System.Drawing.Size(180, 30);
            this.Lb_HTML_4.Name = "Lb_HTML_4";
            this.Lb_HTML_4.Size = new System.Drawing.Size(180, 30);
            this.Lb_HTML_4.TabIndex = 131;
            this.Lb_HTML_4.Text = "HTML 4";
            this.Lb_HTML_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lb_animation
            // 
            this.Lb_animation.AutoSize = true;
            this.Lb_animation.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lb_animation.ForeColor = System.Drawing.Color.Blue;
            this.Lb_animation.Location = new System.Drawing.Point(433, 157);
            this.Lb_animation.Margin = new System.Windows.Forms.Padding(0);
            this.Lb_animation.MaximumSize = new System.Drawing.Size(180, 30);
            this.Lb_animation.MinimumSize = new System.Drawing.Size(180, 30);
            this.Lb_animation.Name = "Lb_animation";
            this.Lb_animation.Size = new System.Drawing.Size(180, 30);
            this.Lb_animation.TabIndex = 132;
            this.Lb_animation.Text = "▀▄▀▄▀▄";
            this.Lb_animation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Btn_total
            // 
            this.Btn_total.BackColor = System.Drawing.Color.Blue;
            this.Btn_total.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_total.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.Btn_total.FlatAppearance.BorderSize = 0;
            this.Btn_total.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.Btn_total.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Btn_total.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_total.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Bold);
            this.Btn_total.ForeColor = System.Drawing.Color.White;
            this.Btn_total.Image = global::GH.Properties.Resources.total;
            this.Btn_total.Location = new System.Drawing.Point(1046, 361);
            this.Btn_total.Name = "Btn_total";
            this.Btn_total.Size = new System.Drawing.Size(170, 88);
            this.Btn_total.TabIndex = 134;
            this.Btn_total.UseVisualStyleBackColor = false;
            this.Btn_total.Click += new System.EventHandler(this.Btn_HTML_Click);
            this.Btn_total.MouseHover += new System.EventHandler(this.Btn_total_MouseHover);
            // 
            // Btn_annuler
            // 
            this.Btn_annuler.BackColor = System.Drawing.Color.Transparent;
            this.Btn_annuler.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_annuler.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.Btn_annuler.FlatAppearance.BorderSize = 0;
            this.Btn_annuler.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Btn_annuler.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Btn_annuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_annuler.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold);
            this.Btn_annuler.ForeColor = System.Drawing.Color.White;
            this.Btn_annuler.Image = global::GH.Properties.Resources.arret;
            this.Btn_annuler.Location = new System.Drawing.Point(481, 190);
            this.Btn_annuler.Name = "Btn_annuler";
            this.Btn_annuler.Size = new System.Drawing.Size(70, 70);
            this.Btn_annuler.TabIndex = 135;
            this.Btn_annuler.Text = " &Arrêt";
            this.Btn_annuler.UseVisualStyleBackColor = false;
            this.Btn_annuler.Click += new System.EventHandler(this.Btn_annuler_Click);
            this.Btn_annuler.MouseHover += new System.EventHandler(this.Btn_annuler_MouseHover);
            // 
            // Btn_annuler_HTML
            // 
            this.Btn_annuler_HTML.BackColor = System.Drawing.Color.Transparent;
            this.Btn_annuler_HTML.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_annuler_HTML.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.Btn_annuler_HTML.FlatAppearance.BorderSize = 0;
            this.Btn_annuler_HTML.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Btn_annuler_HTML.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Btn_annuler_HTML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_annuler_HTML.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold);
            this.Btn_annuler_HTML.ForeColor = System.Drawing.Color.White;
            this.Btn_annuler_HTML.Image = global::GH.Properties.Resources.arret;
            this.Btn_annuler_HTML.Location = new System.Drawing.Point(1100, 378);
            this.Btn_annuler_HTML.Name = "Btn_annuler_HTML";
            this.Btn_annuler_HTML.Size = new System.Drawing.Size(70, 70);
            this.Btn_annuler_HTML.TabIndex = 136;
            this.Btn_annuler_HTML.Text = " &Arrêt";
            this.Btn_annuler_HTML.UseVisualStyleBackColor = false;
            this.Btn_annuler_HTML.Click += new System.EventHandler(this.Btn_anuler_HTML_Click);
            // 
            // Btn_voir_fiche_famille
            // 
            this.Btn_voir_fiche_famille.BackColor = System.Drawing.Color.Blue;
            this.Btn_voir_fiche_famille.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_voir_fiche_famille.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.Btn_voir_fiche_famille.FlatAppearance.BorderSize = 0;
            this.Btn_voir_fiche_famille.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.Btn_voir_fiche_famille.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Btn_voir_fiche_famille.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_voir_fiche_famille.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Bold);
            this.Btn_voir_fiche_famille.ForeColor = System.Drawing.Color.White;
            this.Btn_voir_fiche_famille.Image = global::GH.Properties.Resources.ficheFamilleNoir;
            this.Btn_voir_fiche_famille.Location = new System.Drawing.Point(1113, 305);
            this.Btn_voir_fiche_famille.Name = "Btn_voir_fiche_famille";
            this.Btn_voir_fiche_famille.Size = new System.Drawing.Size(58, 44);
            this.Btn_voir_fiche_famille.TabIndex = 137;
            this.Btn_voir_fiche_famille.UseVisualStyleBackColor = false;
            this.Btn_voir_fiche_famille.Click += new System.EventHandler(this.Btn_voir_fiche_famille_Click);
            this.Btn_voir_fiche_famille.MouseHover += new System.EventHandler(this.Btn_voir_fiche_famille_MouseHover);
            // 
            // Btn_deboguer
            // 
            this.Btn_deboguer.BackColor = System.Drawing.Color.Blue;
            this.Btn_deboguer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_deboguer.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.Btn_deboguer.FlatAppearance.BorderSize = 0;
            this.Btn_deboguer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.Btn_deboguer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Btn_deboguer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_deboguer.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Bold);
            this.Btn_deboguer.ForeColor = System.Drawing.Color.White;
            this.Btn_deboguer.Image = global::GH.Properties.Resources.deboguer;
            this.Btn_deboguer.Location = new System.Drawing.Point(1109, 224);
            this.Btn_deboguer.Name = "Btn_deboguer";
            this.Btn_deboguer.Size = new System.Drawing.Size(44, 48);
            this.Btn_deboguer.TabIndex = 138;
            this.Btn_deboguer.UseVisualStyleBackColor = false;
            this.Btn_deboguer.Click += new System.EventHandler(this.Btn_deboguer_Click);
            this.Btn_deboguer.MouseHover += new System.EventHandler(this.Btn_deboguer_MouseHover);
            // 
            // Btn_balise
            // 
            this.Btn_balise.BackColor = System.Drawing.Color.Blue;
            this.Btn_balise.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_balise.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.Btn_balise.FlatAppearance.BorderSize = 0;
            this.Btn_balise.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.Btn_balise.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Btn_balise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_balise.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Bold);
            this.Btn_balise.ForeColor = System.Drawing.Color.White;
            this.Btn_balise.Image = ((System.Drawing.Image)(resources.GetObject("Btn_balise.Image")));
            this.Btn_balise.Location = new System.Drawing.Point(1046, 224);
            this.Btn_balise.Name = "Btn_balise";
            this.Btn_balise.Size = new System.Drawing.Size(44, 48);
            this.Btn_balise.TabIndex = 139;
            this.Btn_balise.UseVisualStyleBackColor = false;
            this.Btn_balise.Click += new System.EventHandler(this.Btn_balise_Click);
            this.Btn_balise.MouseHover += new System.EventHandler(this.Btn_balise_MouseHover);
            // 
            // Btn_erreur
            // 
            this.Btn_erreur.BackColor = System.Drawing.Color.Blue;
            this.Btn_erreur.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_erreur.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.Btn_erreur.FlatAppearance.BorderSize = 0;
            this.Btn_erreur.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.Btn_erreur.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Btn_erreur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_erreur.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Bold);
            this.Btn_erreur.ForeColor = System.Drawing.Color.White;
            this.Btn_erreur.Image = global::GH.Properties.Resources.erreur;
            this.Btn_erreur.Location = new System.Drawing.Point(1170, 224);
            this.Btn_erreur.Name = "Btn_erreur";
            this.Btn_erreur.Size = new System.Drawing.Size(44, 48);
            this.Btn_erreur.TabIndex = 140;
            this.Btn_erreur.UseVisualStyleBackColor = false;
            this.Btn_erreur.Click += new System.EventHandler(this.Btn_erreur_Click);
            this.Btn_erreur.MouseHover += new System.EventHandler(this.Btn_erreur_MouseHover);
            // 
            // Btn_cadre_individu
            // 
            this.Btn_cadre_individu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Btn_cadre_individu.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.Btn_cadre_individu.FlatAppearance.BorderSize = 3;
            this.Btn_cadre_individu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_cadre_individu.Location = new System.Drawing.Point(655, 29);
            this.Btn_cadre_individu.Name = "Btn_cadre_individu";
            this.Btn_cadre_individu.Size = new System.Drawing.Size(368, 37);
            this.Btn_cadre_individu.TabIndex = 142;
            this.Btn_cadre_individu.UseVisualStyleBackColor = false;
            // 
            // Btn_cadre_famille
            // 
            this.Btn_cadre_famille.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Btn_cadre_famille.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.Btn_cadre_famille.FlatAppearance.BorderSize = 3;
            this.Btn_cadre_famille.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_cadre_famille.Location = new System.Drawing.Point(655, 245);
            this.Btn_cadre_famille.Name = "Btn_cadre_famille";
            this.Btn_cadre_famille.Size = new System.Drawing.Size(368, 37);
            this.Btn_cadre_famille.TabIndex = 143;
            this.Btn_cadre_famille.UseVisualStyleBackColor = false;
            // 
            // GH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1235, 461);
            this.Controls.Add(this.Btn_erreur);
            this.Controls.Add(this.Btn_balise);
            this.Controls.Add(this.Btn_deboguer);
            this.Controls.Add(this.Btn_voir_fiche_famille);
            this.Controls.Add(this.Btn_annuler_HTML);
            this.Controls.Add(this.Btn_annuler);
            this.Controls.Add(this.Btn_total);
            this.Controls.Add(this.Btn_voir_fiche_individu);
            this.Controls.Add(this.Lb_animation);
            this.Controls.Add(this.AvantFamilleB);
            this.Controls.Add(this.ApresFamilleB);
            this.Controls.Add(this.RechercheFamilleB);
            this.Controls.Add(this.RechercheFamilleTB);
            this.Controls.Add(this.AvantIndividuB);
            this.Controls.Add(this.ApresIndividuB);
            this.Controls.Add(this.RechercheIndividuB);
            this.Controls.Add(this.RechercheIndividuTB);
            this.Controls.Add(this.Tb_Status);
            this.Controls.Add(this.Gb_info_GEDCOM);
            this.Controls.Add(this.lpIndividu);
            this.Controls.Add(this.lbFamilleConjoint);
            this.Controls.Add(this.LvChoixIndividu);
            this.Controls.Add(this.menuPrincipal);
            this.Controls.Add(this.LvChoixFamille);
            this.Controls.Add(this.Lb_HTML_1);
            this.Controls.Add(this.Lb_HTML_2);
            this.Controls.Add(this.Lb_HTML_3);
            this.Controls.Add(this.Lb_HTML_4);
            this.Controls.Add(this.Btn_cadre_individu);
            this.Controls.Add(this.Btn_cadre_famille);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuPrincipal;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1251, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1251, 500);
            this.Name = "GH";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GH_FormClosing);
            this.Load += new System.EventHandler(this.GH_Load);
            this.menuPrincipal.ResumeLayout(false);
            this.menuPrincipal.PerformLayout();
            this.Gb_info_GEDCOM.ResumeLayout(false);
            this.Gb_info_GEDCOM.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ouvrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.Label lblVersionProgramme;
        private System.Windows.Forms.Label lblFichier;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblVersionGEDCOM;
        private System.Windows.Forms.Label lblCharSet;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.Label lblDateHeure;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.Label lbFichierGenererPar;
        private System.Windows.Forms.Label lbFamilleConjoint;
        private System.Windows.Forms.Label lpIndividu;
        private System.Windows.Forms.GroupBox Gb_info_GEDCOM;
        private System.Windows.Forms.TextBox Tb_Status;
        private System.Windows.Forms.ToolStripMenuItem ParamètresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aideToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem àProposToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem ouvrirDossierDesFichesHTMLToolStripMenuItem;
        private System.Windows.Forms.Button AvantIndividuB;
        private System.Windows.Forms.Button ApresIndividuB;
        private System.Windows.Forms.TextBox RechercheIndividuTB;
        private System.Windows.Forms.Button AvantFamilleB;
        private System.Windows.Forms.Button ApresFamilleB;
        private System.Windows.Forms.Button RechercheFamilleB;
        private System.Windows.Forms.TextBox RechercheFamilleTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NombreIndividuTb;
        private System.Windows.Forms.TextBox NombreFamilleTb;
        public System.Windows.Forms.ListView LvChoixFamille;
        public System.Windows.Forms.ListView LvChoixIndividu;
        private System.Windows.Forms.Button RechercheIndividuB;
        private System.Windows.Forms.Label Lb_HTML_1;
        private System.Windows.Forms.Label Lb_HTML_2;
        private System.Windows.Forms.Label Lb_HTML_3;
        private System.Windows.Forms.Label Lb_HTML_4;
        private System.Windows.Forms.Label Lb_animation;
        private System.Windows.Forms.Button Btn_voir_fiche_individu;
        private System.Windows.Forms.Button Btn_total;
        private System.Windows.Forms.Button Btn_annuler;
        private System.Windows.Forms.Button Btn_annuler_HTML;
        private System.Windows.Forms.Button Btn_voir_fiche_famille;
        private System.Windows.Forms.Button Btn_deboguer;
        private System.Windows.Forms.Button Btn_balise;
        private System.Windows.Forms.Button Btn_erreur;
        private System.DirectoryServices.DirectoryEntry directoryEntry1;
        private System.Windows.Forms.Button Btn_cadre_individu;
        private System.Windows.Forms.Button Btn_cadre_famille;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

