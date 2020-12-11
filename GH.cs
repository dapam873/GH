// https://github.com/carloscds/HtmlToPDFCore/


using GEDCOM;
using howto_sort_list_columns;
using HTML;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO; 
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks; //nnn
using System.Windows.Forms;
using Squirrel; //nnn
using System.Reflection; // temporaire à retirer après debug update

namespace GH
{
    public partial class GH : Form
    {
        public static bool annuler;
        public bool Ok_anuler = false;
        public readonly HTMLClass HTML = new HTMLClass();
        public int indexConjoint = 0;
        public int indexConjointe = 0;
        public int indexIndividu = 0;
        public string FichierGEDCOM = "";
        public string DossierSortie = "";
        public static System.Timers.Timer aTimer = new System.Timers.Timer();
        public List<GEDCOMClass.NOTE_RECORD> liste_Code_HTML_Note = new List<GEDCOMClass.NOTE_RECORD>();
        private ColumnHeader SortingColumnIndividu = null;
        private ColumnHeader SortingColumnFamille = null;
        /// <summary>
        /// constante de la liste
        /// </summary>
        /// <summary>
        /// position de la colonne SOSA dans le tableau grille
        /// </summary>
        public const int SOSA = 0;
        /// <summary>
        /// position de la colonne PAGE dans le tableau grille
        /// </summary>
        public const int PAGE = 1;
        /// <summary>
        /// position de la colonne GENERATION dans le tableau grille
        /// </summary>
        public const int GENERATION = 2;
        /// <summary>
        /// position de la colonne PATRONYME dans le tableau grille
        /// </summary>
        public const int PATRONYME = 3;
        /// <summary>
        /// position de la colonne PRENOM dans le tableau grille
        /// </summary>
        public const int PRENOM = 4;
        /// <summary>
        /// position de la colonne NOMTRI dans le tableau grille
        /// </summary>
        public const int NOMTRI = 5;
        /// <summary>
        /// position de la colonne NELE dans le tableau grille
        /// </summary>
        public const int NELE = 6;
        /// <summary>
        /// position de la colonne NELIEU dans le tableau grille
        /// </summary>
        public const int NELIEU = 7;
        /// <summary>
        /// position de la colonne DELE dans le tableau grille
        /// </summary>
        public const int DELE = 8;
        /// <summary>
        /// position de la colonne DELIEU dans le tableau grille
        /// </summary>
        public const int DELIEU = 9;
        /// <summary>
        /// position de la colonne MALE dans le tableau grille
        /// </summary>
        public const int MALE = 10;
        /// <summary>
        /// position de la colonne MALIEU dans le tableau grille
        /// </summary>
        public const int MALIEU = 11;
        /// <summary>
        /// position de la colonne IDg dans le tableau grille
        /// </summary>
        public const int IDg = 12;
        /// <summary>
        /// position de la colonne IDFAMILLEENFANT dans le tableau grille
        /// </summary>
        public const int IDFAMILLEENFANT = 13;
        /// <summary>
        /// position de la colonne IDFAMILLEPARENT dans le tableau grille
        /// </summary>
        public const int NOTE1 = 14;
        /// <summary>
        /// position de la colonne Note2 dans le tableau grille
        /// </summary>
        public const int NOTE2 = 15;
        /// <summary>
        /// Nom du programme
        /// </summary>
        // ****************************************************************
        /// <summary>
        /// Nom du fichier courant
        /// </summary>
        public string FichierCourant = "";
        /// <summary>
        /// nom du fichier GEDCOM
        /// </summary>
        public string FichierGEDCOMaLire = "";
        /// <summary>
        /// IDCourantFamilleConjoint
        /// à utiliser avec le boutton Navigateur
        /// </summary>
        public string IDCourantFamilleConjoint = "";
        /// <summary>
        /// IDCourantFamilleConjointe
        /// à utiliser avec le boutton Navigateur
        /// </summary>
        public string IDCourantFamilleConjointe = "";
        /// <summary>
        /// IDCourantIndividu
        /// à utiliser avec le boutton Navigateur
        /// </summary>
        public string IDCourantIndividu = "";
        public string[,] liste = new string[512, 17];
        /// <summary>
        /// Nom du programme
        /// </summary>
        /// 
        public const string NomPrograme = "GH";
        /// <summary>
        /// numero du SOSA courant
        /// </summary>
        public int sosaCourant = 0;
        public static Form _Form1;
        public GH()
        {
            InitializeComponent();
            var task = Verifier_mise_a_jour();
            //Verifier_mise_a_jour();
            //task.Wait();
            task.ToString();
        }
#pragma warning disable CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        private async Task Verifier_mise_a_jour() //nnn
#pragma warning restore CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        {
            using (var manager = new UpdateManager("https://github.com/dapam873/GH")) // adresse web pour la mise à jour
            //using (var manager = new UpdateManager(@"D:\Data\Mega\GH\GH\Releases", "GH" )) // dossier pour la mise à jour
            { 
                //await manager.UpdateApp();
                //Assembly assembly = Assembly.GetExecutingAssembly();
                
                SquirrelAwareApp.HandleEvents(
                    onInitialInstall: v => { manager.CreateShortcutForThisExe(); manager.CreateUninstallerRegistryEntry(); },
                    onAppUpdate: v => { manager.CreateShortcutForThisExe(); manager.CreateUninstallerRegistryEntry(); },
                    onAppUninstall: v => { manager.RemoveShortcutForThisExe(); manager.RemoveUninstallerRegistryEntry(); },
                    onFirstRun: () => Voir_Bienvenu()
                );
            }
        }
        private void Voir_Bienvenu()
        {
            Form l = new Bienvenu
            {
                StartPosition = FormStartPosition.Manual,
                Left = 70,
                Top = 60
            };
            l.ShowDialog(this);
        }
        readonly ToolTip t1 = new ToolTip();
        private void OuvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Properties.Settings.Default.DossierHTML))
            {
                MessageBox.Show("S.V.P. Spécifiez dans les paramêtres, le dossier des fiches HTML.", "Erreur OTSI0213 Problème ?",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Warning);
                return;
            }
            FichierGEDCOMaLire = "";
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Properties.Settings.Default.DossierGEDCOM;
                openFileDialog.Filter = "GEDCOM (*.ged)|*.ged|All files (*.*)|*.*";
                openFileDialog.Title = "Lire le fichier";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FichierGEDCOMaLire = openFileDialog.FileName;
                }

            }
            // Si dialog retourne autre chose que ""
            if (FichierGEDCOMaLire != "")
            {
                DossierSortie = Properties.Settings.Default.DossierHTML + @"\" + Path.GetFileNameWithoutExtension(FichierGEDCOMaLire);

                if (Directory.Exists(DossierSortie))
                {
                    DialogResult reponse;
                    string message = "Le dossier des rapports " + Path.GetFileNameWithoutExtension(FichierGEDCOMaLire) +
                        " existe. Il sera effacé.";
                    reponse = MessageBox.Show(
                        message,
                        "Attention",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning
                        );
                    if (reponse == System.Windows.Forms.DialogResult.Cancel)
                        GH.annuler = true;
                }
            }
            if (GH.annuler) return;
            if (FichierGEDCOMaLire != "")
            {
                Btn_annuler.Visible = true;
                Btn_balise.Visible = false;
                menuPrincipal.Visible = false;
                FormVisible(false);
                Tb_Status.Text = "Un instant svp";
                Tb_Status.Visible = true;
                Application.DoEvents();
                if (!LireFichierGEDCOM(FichierGEDCOMaLire))
                {
                    Btn_annuler.Visible = false;
                    Lb_animation.Visible = false;
                    Tb_Status.Visible = false;
                    Gb_info_GEDCOM.Visible = false;
                    this.Cursor = Cursors.Default;
                    annuler = false;
                    menuPrincipal.Visible = true;
                    return;
                }
                Properties.Settings.Default.DossierGEDCOM = Path.GetDirectoryName(FichierGEDCOMaLire);
               
                if (Properties.Settings.Default.DossierHTML != "") CreerDossier();
                FichierGEDCOM = Path.GetFileName(FichierGEDCOMaLire);
                this.Text = " " + FichierGEDCOM;
                Tb_Status.Visible = false;
                Btn_annuler.Visible = false;
                annuler = false;
            }
            menuPrincipal.Visible = true;
        }
        //*************************** Function
        public string AssemblerPatronymePrenom(string patronyme, string prenom)
        {
            if (prenom == null && patronyme == null) return null;
            if (prenom == "" && patronyme == "") return null;
            if (prenom == "") prenom = "?";
            if (prenom == null) prenom = "?";
            if (patronyme == "") patronyme = "?";
            if (patronyme == null) patronyme = "?";
            return patronyme + ", " + prenom;
        }
        private void AvoirDossierMedias()
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog
            {
                Description = "Où est le dossier de vos médias"
            };
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.DossierMedia = folderBrowserDialog1.SelectedPath;
                TextBox dossierMedia = Application.OpenForms["Parametre"].Controls["TbDossierHTML"] as TextBox;
                dossierMedia.Text = Properties.Settings.Default.DossierMedia;
            }
        }
        private static string Avoir_code_erreur([CallerLineNumber] int sourceLineNumber = 0)
        {
            return "F" + sourceLineNumber;
        }
        private void AvoirDossierPageWeb()
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog
            {
                Description = "Ou enregister les fiches HTML"
            };
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.DossierHTML = folderBrowserDialog1.SelectedPath;
                TextBox dossierHTML = Application.OpenForms["Parametre"].Controls["TbDossierHTML"] as TextBox;
                dossierHTML.Text = Properties.Settings.Default.DossierHTML;
            }
        }
        private void ChoixLVIndividu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LvChoixIndividu.SelectedItems.Count == 0) return;
            ListViewItem item = LvChoixIndividu.SelectedItems[0];
            IDCourantIndividu = item.SubItems[0].Text;
        }
        private void CreerDossier()
        {
            Tb_Status.Text = "Création des dossiers";

            if (!DossierHTMLValide())
            {
                return;
            }
            string erreur = Avoir_code_erreur();
            try
            {
                erreur = Avoir_code_erreur();
                DirectoryInfo di = new DirectoryInfo(@DossierSortie);
                if (EffacerLesDossier(di))
                {
                    erreur = Avoir_code_erreur();
                    Thread.Sleep(2000);
                    Tb_Status.Text = "Création des dossiers";
                    if (!Directory.Exists(DossierSortie)) Directory.CreateDirectory(DossierSortie);
                    if (!Directory.Exists(DossierSortie + @"\commun")) Directory.CreateDirectory(DossierSortie + @"\commun");
                    erreur = Avoir_code_erreur();
                    if (!Directory.Exists(DossierSortie + @"\commun\images")) Directory.CreateDirectory(DossierSortie + @"\commun\images");
                    erreur = Avoir_code_erreur();
                    if (!Directory.Exists(DossierSortie + @"\familles")) Directory.CreateDirectory(DossierSortie + @"\familles");
                    erreur = Avoir_code_erreur();
                    if (!Directory.Exists(DossierSortie + @"\familles\medias")) Directory.CreateDirectory(DossierSortie + @"\familles\medias");
                    erreur = Avoir_code_erreur();
                    if (!Directory.Exists(DossierSortie + @"\individus")) Directory.CreateDirectory(DossierSortie + @"\individus");
                    erreur = Avoir_code_erreur();
                    if (!Directory.Exists(DossierSortie + @"\individus\medias")) Directory.CreateDirectory(DossierSortie + @"\individus\medias");
                    erreur = Avoir_code_erreur();
                    erreur = Avoir_code_erreur();
                    // copier tous les fichiers commun
                    string souceDossier = @Application.StartupPath + @"\commun\";
                    string destinationDossier = @DossierSortie + @"\commun\";
                    string[] listeFichier = Directory.GetFiles(souceDossier, "*.*");
                    foreach (string f in listeFichier)
                    {
                        erreur = Avoir_code_erreur();
                        // Supprimez le chemin du nom de fichier.
                        string nomFichier = Path.GetFileName(f); ;
                        // Utilisez la méthode Path.Combine pour ajouter le nom de fichier au chemin.
                        // Remplacera si le fichier de destination existe déjà.
                        File.Copy(Path.Combine(souceDossier, nomFichier), Path.Combine(destinationDossier, nomFichier), true);
                    }
                    erreur = Avoir_code_erreur();
                    souceDossier = @Application.StartupPath + @"\commun\images\";
                    destinationDossier = @DossierSortie + @"\commun\images\";
                    listeFichier = Directory.GetFiles(souceDossier, "*.*");
                    erreur = Avoir_code_erreur();
                    foreach (string f in listeFichier)
                    {
                        erreur = Avoir_code_erreur();
                        // Supprimez le chemin du nom de fichier.
                        string nomFichier = Path.GetFileName(f); ;
                        // Utilisez la méthode Path.Combine pour ajouter le nom de fichier au chemin.
                        // Remplacera si le fichier de destination existe déjà.
                        File.Copy(Path.Combine(souceDossier, nomFichier), Path.Combine(destinationDossier, nomFichier), true);
                    }
                }
            }
            catch (Exception msg)
            {
                string message = "Problème pour créer les dossirs.";
                GEDCOMClass.Voir_message(message, msg.Message, erreur);
            }
        }
        private bool DossierHTMLValide([CallerFilePath] string code = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            DialogResult reponse;
            code = Path.GetFileName(code);
            code = code[0].ToString().ToUpper();
            if (!Directory.Exists(Properties.Settings.Default.DossierHTML))
            {
                reponse = MessageBox.Show("S.V.P. Spécifiez dans les paramêtres, le dossier des fiches HTML.", "Erreur " + code + lineNumber + " " + caller + " Problème ?",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Warning);
                if (reponse == System.Windows.Forms.DialogResult.Cancel)
                    GH.annuler = true;
                return false;
            }
            return true;
        }
        public static bool EffacerLeDossier1(string dossier)
        {
            if (!Directory.Exists(dossier)) return true;
            string erreur = Avoir_code_erreur();
            try
            {
                erreur = Avoir_code_erreur();
                string[] listeFichier = Directory.GetFiles(@dossier);
                erreur = Avoir_code_erreur();
                foreach (string fichier in listeFichier)
                {
                    erreur = Avoir_code_erreur();
                    File.Delete(fichier);
                    while (File.Exists(fichier))
                    {
                        erreur = Avoir_code_erreur();
                    }
                }
                erreur = Avoir_code_erreur();
                Directory.Delete(dossier);
                while (Directory.Exists(dossier))
                {
                    erreur = Avoir_code_erreur();
                }

                erreur = Avoir_code_erreur();
                return true;
            }
            catch (Exception msg)
            {
                string message = "Problème pour effacer les dossiers.";
                GEDCOMClass.Voir_message(message, msg.Message, erreur);
            }
            return false;
        }
        public bool EffacerLesDossier(DirectoryInfo dossier)
        {
            if (dossier != null)
                if (!dossier.Exists)
                    return true;
            string dossier1 = null;
            if (dossier != null)
                dossier1 = dossier.ToString();
            Tb_Status.Text = "Effacement des anciens dossiers";
            bool status = EffacerLeDossier1(Properties.Settings.Default.DossierHTML + @"\" + dossier1 + @"\commun");
            if (status) status = EffacerLeDossier1(Properties.Settings.Default.DossierHTML + @"\" + dossier1 + @"\individus\medias");
            if (status) status = EffacerLeDossier1(Properties.Settings.Default.DossierHTML + @"\" + dossier1 + @"\individus");
            if (status) status = EffacerLeDossier1(Properties.Settings.Default.DossierHTML + @"\" + dossier1 + @"\familles\medias");
            if (status) status = EffacerLeDossier1(Properties.Settings.Default.DossierHTML + @"\" + dossier1 + @"\familles");
            Tb_Status.Text = "";
            return status;
        }
        public void Effacer_Log()
        {
            try
            {
                //string dossier = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\GH";
                // si deboguer.txt existe efface fichiers
                if (File.Exists(Properties.Settings.Default.DossierHTML + @"\deboguer.txt"))
                {
                    File.Delete(Properties.Settings.Default.DossierHTML + @"\deboguer.txt");
                }
                // si erreur.txt existe efface fichiers
                if (File.Exists(Properties.Settings.Default.DossierHTML + @"\erreur.txt"))
                {
                    File.Delete(Properties.Settings.Default.DossierHTML + @"\erreur.txt");
                }
                // si balise.txt existe efface fichiers
                if (File.Exists(Properties.Settings.Default.DossierHTML + @"\balise.txt"))
                {
                    File.Delete(Properties.Settings.Default.DossierHTML + @"\balise.txt");
                }
            }
            catch (Exception msg)
            {
                if (msg.Message == null)
                {
                }
            }
        }
        private void GH_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Form1_State = this.WindowState;
            if (this.WindowState == FormWindowState.Normal)
            {
                // save location and size if the state is normal
                Properties.Settings.Default.Form1_location = this.Location;
            }
            else
            {
                // save the RestoreBounds if the form is minimized or maximized!
                Properties.Settings.Default.Form1_location = this.RestoreBounds.Location;
            }
            // don't forget to save the settings
            Properties.Settings.Default.Save();
        }
        private void GH_Load(object sender, EventArgs e)
        {
            //system.Version version;
            /*try
            {
                version = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            catch
            {
                version = Assembly.GetExecutingAssembly().GetName().Version;
            }*/
            this.Visible = false;
            this.WindowState = Properties.Settings.Default.Form1_State;
            this.Location = Properties.Settings.Default.Form1_location;
            Btn_annuler.Visible = false;
            Btn_deboguer.Visible = false;
            Btn_erreur.Visible = false;
            Btn_balise.Visible = false;
            LvChoixIndividu.Items.Clear();
            //organisation des élément graphique de la forme
            lblNom.Text = "";
            lblVersionProgramme.Text = "";
            lblFichier.Text = "";
            lblDateHeure.Text = "";
            lblVersionGEDCOM.Text = "";
            lblCopyright.Text = "";
            lblCharSet.Text = "";
            lblLanguage.Text = "";
            NombreIndividuTb.Text = "";
            NombreFamilleTb.Text = "";
            Lb_HTML_1.Text = "";
            Lb_HTML_2.Text = "";
            Lb_HTML_3.Text = "";
            Lb_HTML_4.Text = "";
            Lb_animation.Text = "";
            Btn_annuler.Visible = false;
            Btn_annuler_HTML.Visible = false;
            Gb_info_GEDCOM.Visible = false;
            Tb_Status.Visible = false;
            Tb_Status.Visible = false;
            AvantIndividuB.Visible = false;
            ApresIndividuB.Visible = false;
            AvantFamilleB.Visible = false;
            ApresFamilleB.Visible = false;
            Btn_cadre_individu.Visible = false;
            Btn_cadre_famille.Visible = false;
            LvChoixIndividu.Visible = false;
            lpIndividu.Visible = false;

            LvChoixFamille.Visible = false;
            lbFamilleConjoint.Visible = false;
            Btn_voir_fiche_individu.Visible = false;
            Btn_voir_fiche_famille.Visible = false;
            Btn_total.Visible = false;

            LvChoixIndividu.Columns.Add("ID", 80);
            LvChoixIndividu.Columns.Add("Nom", 200);
            LvChoixIndividu.Columns.Add("Naissance", 80);
            LvChoixIndividu.Columns.Add("Lieu naissance", 275);
            LvChoixIndividu.Columns.Add("Décès", 80);
            LvChoixIndividu.Columns.Add("Lieu décès", 275);
            LvChoixIndividu.BackColor = Color.LightSkyBlue;

            LvChoixFamille.View = View.Details;
            LvChoixFamille.GridLines = true;
            LvChoixFamille.FullRowSelect = true;
            LvChoixFamille.Items.Clear();
            LvChoixFamille.Columns.Add("ID", 80);
            LvChoixFamille.Columns.Add("Conjoint", 225);
            LvChoixFamille.Columns.Add("Conjointe", 225);
            LvChoixFamille.Columns.Add("Mariage", 80);
            LvChoixFamille.Columns.Add("Lieu mariage", 380);
            LvChoixFamille.BackColor = Color.LightSkyBlue;
            Effacer_Log();
            PasConfidentiel("", "");
            this.Visible = true;
            FormVisible(false);
        }
        private bool LireFichierGEDCOM(string FichierGEDCOMaLire)
        {
            NombreIndividuTb.Text = "";
            NombreFamilleTb.Text = "";
            Tb_Status.Visible = true;
            Tb_Status.Text = "Lecture du fichier";
            string FichierGEDCOMaLireTemp = GEDCOMClass.LireEnteteGEDCOM(FichierGEDCOMaLire);
            if (FichierGEDCOMaLireTemp == null) return false;
            FichierGEDCOMaLire = FichierGEDCOMaLireTemp;
            GEDCOMClass.HEADER InfoGEDCOM = GEDCOMClass.AvoirInfoGEDCOM();
            lblCharSet.Text = "Code charactère: " + InfoGEDCOM.N1_CHAR;
            Gb_info_GEDCOM.Visible = true;
            if (!GEDCOMClass.LireGEDCOM(FichierGEDCOMaLire)) return false;
            this.Cursor = Cursors.WaitCursor;
            GEDCOMClass.Extraire_GEDCOM();
            if (annuler) return (false);
            Tb_Status.Text = "";
            InfoGEDCOM = GEDCOMClass.AvoirInfoGEDCOM();
            lblNom.Text = "Nom: " + InfoGEDCOM.N2_SOUR_NAME;
            lblVersionProgramme.Text = "Version: " + InfoGEDCOM.N2_SOUR_VERS;
            lblDateHeure.Text = InfoGEDCOM.N1_DATE + " " + InfoGEDCOM.N2_DATE_TIME;
            lblFichier.Text = "Fichier: " + FichierGEDCOM;
            lblCopyright.Text = InfoGEDCOM.N1_COPR;
            lblVersionGEDCOM.Text = "Version: " + InfoGEDCOM.N2_GEDC_VERS;
            lblCharSet.Text = "Code charactère: " + InfoGEDCOM.N1_CHAR;
            lblLanguage.Text = "Langue: " + InfoGEDCOM.N1_LANG;
            Application.DoEvents();
            Gb_info_GEDCOM.Visible = true;
            FichierCourant = "";
            // créer liste individu
            LvChoixIndividu.View = View.Details;
            LvChoixIndividu.GridLines = true;
            LvChoixIndividu.FullRowSelect = true;
            LvChoixIndividu.Items.Clear();
            List<string> ListeIDIndividu;
            ListeIDIndividu = GEDCOMClass.AvoirListeIDIndividu();
            ListViewItem itmIndividu;
            //int compteur = 0;
            int nombre = ListeIDIndividu.Count;
            LvChoixIndividu.Items.Clear();
            for (int f = 0; f < nombre; f++)
            {
                if (GH.annuler) return false;
                bool Ok;
                string IDIndividu = ListeIDIndividu[f];
                Tb_Status.Text = "Création de la liste Individu ID  " + IDIndividu;
                Animation(true);
                GEDCOM.GEDCOMClass.INDIVIDUAL_RECORD infoIndividu;
                (Ok, infoIndividu) = GEDCOMClass.Avoir_info_individu(IDIndividu);
                string nom = null;
                if (Ok)
                {
                    if (infoIndividu.N1_NAME_liste.Count > 0)
                    {
                        if (infoIndividu.N1_NAME_liste[0].N1_PERSONAL_NAME_PIECES != null)
                        {
                            nom = AssemblerPatronymePrenom(infoIndividu.N1_NAME_liste[0].N1_PERSONAL_NAME_PIECES.Nn_SURN,
                                infoIndividu.N1_NAME_liste[0].N1_PERSONAL_NAME_PIECES.Nn_GIVN);
                        }
                    }
                    if (nom == null)
                    {
                        nom = infoIndividu.N1_NAME_liste[0].N0_NAME;
                    }
                    
                }
                // info naissance
                GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE Naissance;
                (_, Naissance) = GEDCOMClass.AvoirEvenementNaissance(infoIndividu.N1_EVENT_Liste);
                // info Décès
                GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE Deces;
                (_, Deces) = GEDCOMClass.AvoirEvenementDeces(infoIndividu.N1_EVENT_Liste);
                string[] ligne = new string[6];
                ligne[0] = IDIndividu;
                ligne[1] = nom;
                ligne[2] = Naissance.N2_DATE;
                if (Naissance.N2_PLAC != null) ligne[3] = Naissance.N2_PLAC.N0_PLAC;
                ligne[4] = Deces.N2_DATE;
                if (Deces.N2_PLAC != null) ligne[5] = Deces.N2_PLAC.N0_PLAC;
                itmIndividu = new ListViewItem(ligne);
                Animation(true);
                LvChoixIndividu.Items.Add(itmIndividu);
            }
            Tb_Status.Text = "Un instant SVP. Mise à jour de la liste Individu";
            Application.DoEvents();
            NombreIndividuTb.Text = string.Format("{0:0,0}", LvChoixIndividu.Items.Count);
            // créer liste famille
            LvChoixFamille.View = View.Details;
            LvChoixFamille.GridLines = true;
            LvChoixFamille.FullRowSelect = true;
            LvChoixFamille.Items.Clear();
            List<string> ListeIDFamille;
            ListeIDFamille = GEDCOMClass.AvoirListeIDFamille();
            ListViewItem itmFamille;
            nombre = ListeIDFamille.Count;
            for (int f = 0; f < nombre; f++)
            {
                if (GH.annuler) return false;
                bool Ok;
                string IDFamille = ListeIDFamille[f];
                Tb_Status.Text = "Création de la liste Famille ID  " + IDFamille;
                GEDCOM.GEDCOMClass.FAM_RECORD infoFamille = GEDCOMClass.Avoir_info_famille(IDFamille);
                GEDCOM.GEDCOMClass.INDIVIDUAL_RECORD infoConjoint;
                (Ok, infoConjoint) = GEDCOMClass.Avoir_info_individu(infoFamille.N1_HUSB);
                string nomConjoint = null;
                if (Ok)
                {
                    if (infoConjoint.N1_NAME_liste.Count > 0)
                    {
                        if (infoConjoint.N1_NAME_liste[0].N1_PERSONAL_NAME_PIECES != null)
                        {
                            nomConjoint = AssemblerPatronymePrenom(
                                infoConjoint.N1_NAME_liste[0].N1_PERSONAL_NAME_PIECES.Nn_SURN,
                                infoConjoint.N1_NAME_liste[0].N1_PERSONAL_NAME_PIECES.Nn_GIVN);
                        }
                        if (nomConjoint == null) nomConjoint = infoConjoint.N1_NAME_liste[0].N0_NAME;
                    }
                }
                string nomConjointe = null;
                GEDCOM.GEDCOMClass.INDIVIDUAL_RECORD infoConjointe;

                (Ok, infoConjointe) = GEDCOMClass.Avoir_info_individu(infoFamille.N1_WIFE);
                if (Ok)
                {
                    if (infoConjointe.N1_NAME_liste.Count > 0)
                    {
                        if (infoConjointe.N1_NAME_liste[0].N1_PERSONAL_NAME_PIECES != null)
                        {
                            nomConjointe = AssemblerPatronymePrenom(
                                infoConjointe.N1_NAME_liste[0].N1_PERSONAL_NAME_PIECES.Nn_SURN,
                                infoConjointe.N1_NAME_liste[0].N1_PERSONAL_NAME_PIECES.Nn_GIVN);
                        }
                        if (nomConjointe == null) nomConjointe = infoConjointe.N1_NAME_liste[0].N0_NAME;
                    }
                }
                GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE Mariage = GEDCOMClass.AvoirEvenementMariage(infoFamille.N1_EVENT_Liste);
                string[] ligne = new string[5];
                ligne[0] = IDFamille;
                ligne[1] = nomConjoint;
                ligne[2] = nomConjointe;
                ligne[3] = Mariage.N2_DATE;
                if (Mariage.N2_PLAC != null) ligne[4] = Mariage.N2_PLAC.N0_PLAC;
                itmFamille = new ListViewItem(ligne);
                LvChoixFamille.Items.Add(itmFamille);
            }
            Tb_Status.Text = "Un instant SVP. Mise à jour de la liste Famille";
            Application.DoEvents();
            NombreFamilleTb.Text = string.Format("{0:0,0}", LvChoixFamille.Items.Count);
            Tb_Status.Text = "Un instant SVP";
            Application.DoEvents();
            this.Cursor = Cursors.Default;
            // bouton
            Btn_voir_fiche_individu.Visible = true;
            Btn_voir_fiche_famille.Visible = true;
            Btn_total.Visible = true;
            Animation(true);
            System.Windows.Forms.SortOrder sort_order;
            Animation(true);
            sort_order = SortOrder.Ascending;
            Animation(true);
            //sort individu par nom
            LvChoixIndividu.ListViewItemSorter = new ListViewComparer(1, sort_order);
            LvChoixIndividu.Sort();
            // sort Famille par nom du conjoint
            LvChoixFamille.ListViewItemSorter = new ListViewComparer(1, sort_order);
            LvChoixFamille.Sort();
            LvChoixIndividu.Visible = true;
            lpIndividu.Visible = true;
            lbFamilleConjoint.Visible = true;
            LvChoixFamille.Visible = true;
            FormVisible(true);
            Animation(false);
            return true;
        }
        private bool PasConfidentiel(string naissance, string deces)
        {
            DateTime maintenent = DateTime.Now;
            if (deces != "")
            {
                DateTime d = DateTime.ParseExact(deces, "yyyy-MM-dd", null);
                int ageD = maintenent.Year - d.Year;
                if (ageD > 30) return true;
            }
            if (naissance != "")
            {
                DateTime n = DateTime.ParseExact(naissance, "yyyy-MM-dd", null);
                int ageN = maintenent.Year - n.Year;
                if (ageN > 100) return true;
            }
            if (deces == "" && naissance == "") return true;

            return false;
        }
        private void Fichier_total()
        {
            Btn_total.Visible = false;
            Btn_voir_fiche_individu.Visible = false;
            Btn_voir_fiche_famille.Visible = false;
            Btn_annuler_HTML.Visible = true;
            Application.DoEvents();
            if (!DossierHTMLValide())
            {
                return;
            }
            Form_desactiver(true);
            Btn_total.Visible = false;
            Btn_voir_fiche_individu.Visible = false;
            Btn_voir_fiche_famille.Visible = false;
            Application.DoEvents();
            Array.ForEach(Directory.GetFiles(DossierSortie + @"\individus"), delegate (string path) { File.Delete(path); });
            Array.ForEach(Directory.GetFiles(DossierSortie + @"\individus\medias"), delegate (string path) { File.Delete(path); });
            Array.ForEach(Directory.GetFiles(DossierSortie + @"\familles"), delegate (string path) { File.Delete(path); });
            Array.ForEach(Directory.GetFiles(DossierSortie, "*.html"), delegate (string path) { File.Delete(path); });
            if (annuler) return;
            List<string> ListeID = new List<string>();
            ListeID.Clear();
            ListeID = GEDCOMClass.AvoirListeIDIndividu();
            int compteur = 1;
            compteur = 0;
            HTML.Index(FichierGEDCOM, NombreIndividuTb.Text, NombreFamilleTb.Text, DossierSortie);
            if (annuler) return;
            HTML.Index_individu(DossierSortie);
            if (annuler) return;
            HTML.Index_famille_conjoint(DossierSortie);
            if (annuler) return;
            HTML.Index_famille_conjointe(DossierSortie);
            if (annuler) return;
            foreach (string ID in ListeID)
            {
                Message_HTML("Génération de la ", "fiche individu", "ID " + ID);
                HTML.Individu(ID, true, DossierSortie);
                Application.DoEvents();
                compteur++;
                if (annuler) return;
            }
            ListeID = GEDCOMClass.AvoirListeIDFamille();
            compteur = 1;
            foreach (string ID in ListeID)
            {
                Message_HTML("Génération de la ", "fiche famille", "ID " + ID);
                HTML.Famille(ID, true, DossierSortie);
                Application.DoEvents();
                compteur++;
                if (annuler) return;
            }
            Message_HTML("Un instant SVP");

            try
            {
                System.Diagnostics.Process.Start("file:///" + DossierSortie + @"\index.html");
            }
            catch (Exception msg)
            {
                MessageBox.Show("Générez les fiches HTML avant de voir la page d'acceuil.\r\n\r\n", "Page d'acceuil ?",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);
                if (msg.Message == null) { }
                if (annuler) return;
            }
            //FormVisible(true);
        }
        private void FichierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            Btn_balise.Visible = true;
            Btn_deboguer.Visible = true;
            Btn_erreur.Visible = true;
            */
        }
        private void LvChoixFamilleConjoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LvChoixFamille.SelectedItems.Count == 0)
                return;
            ListViewItem item = LvChoixFamille.SelectedItems[0];
            IDCourantFamilleConjoint = item.SubItems[0].Text;
        }
        private void MessageErreur(string message, string entette = "", [CallerLineNumber] int lineNumber = 0
            )
        {
            if (entette == "") entette = "Erreur G" + lineNumber;
            MessageBox.Show(message, entette);
        }
        public void Message_HTML(string ligne1 = "", string ligne2 = "", string ligne3 = "")
        {
            if (ligne1 == "" && ligne2 == "" && ligne3 == "")
            {
                Lb_HTML_1.Visible = false;
                Lb_HTML_2.Visible = false;
                Lb_HTML_3.Visible = false;
                Lb_HTML_4.Visible = false;
            }
            else
            {
                Lb_HTML_1.Visible = true;
                Lb_HTML_2.Visible = true;
                Lb_HTML_3.Visible = true;
                Lb_HTML_4.Visible = true;
                Lb_HTML_1.Text = ligne1;
                Lb_HTML_2.Text = ligne2;
                Lb_HTML_3.Text = ligne3;
                Random rnd = new Random();
                string ligne4 = "";
                for (int f = 0; f < 7; f++)
                {
                    if (rnd.Next(0, 2) == 0) ligne4 += "▀"; else ligne4 += "▄";
                }
                Lb_HTML_4.Text = ligne4.Substring(0, 5);
            }
            Application.DoEvents();
        }
        public void Form_desactiver(bool status)
        {
            if (status)
            {
                this.UseWaitCursor = true;
                LvChoixIndividu.Enabled = false;
                LvChoixFamille.Enabled = false;
                Btn_voir_fiche_individu.Enabled = false;
                Btn_voir_fiche_famille.Enabled = false;

            } else
            {
                this.UseWaitCursor = false;
                LvChoixIndividu.Enabled = true;
                LvChoixFamille.Enabled = true;
                Btn_voir_fiche_individu.Enabled = true;
                Btn_voir_fiche_famille.Enabled = true;
            }
            Application.DoEvents();
        }
        private void FormVisible(bool status)
        {
            if (status)
            {
                Btn_annuler.Visible = false;
                Lb_animation.Visible = false;
                Btn_total.Visible = true;
                Btn_voir_fiche_individu.Visible = true;
                Btn_voir_fiche_famille.Visible = true;
                Gb_info_GEDCOM.Visible = true;
                LvChoixIndividu.Visible = true;
                LvChoixFamille.Visible = true;
                lpIndividu.Visible = true;
                lbFamilleConjoint.Visible = true;
                RechercheFamilleB.Visible = true;
                RechercheFamilleTB.Visible = true;
                RechercheIndividuB.Visible = true;
                RechercheIndividuTB.Visible = true;
                menuPrincipal.Visible = true;
                Tb_Status.Visible = false;
                Btn_annuler.Visible = false;
                Btn_cadre_individu.Visible = true;
                Btn_cadre_famille.Visible = true;
            }
            else
            {
                Btn_balise.Visible = false;
                Btn_total.Visible = false;
                Btn_voir_fiche_individu.Visible = false;
                Btn_voir_fiche_famille.Visible = false;
                Gb_info_GEDCOM.Visible = false;
                LvChoixIndividu.Visible = false;
                LvChoixFamille.Visible = false;
                lpIndividu.Visible = false;
                lbFamilleConjoint.Visible = false;
                RechercheFamilleB.Visible = false;
                RechercheFamilleTB.Visible = false;
                RechercheIndividuB.Visible = false;
                RechercheIndividuTB.Visible = false;
                Btn_cadre_individu.Visible = false;
                Btn_cadre_famille.Visible = false;
                Btn_cadre_individu.Visible = false;
                Btn_cadre_famille.Visible = false;
            }
            Application.DoEvents();
        }
        private void LblCharSet_Click(object sender, EventArgs e)
        {

        }
        private void LvChoixIndividu_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Obtenez la nouvelle colonne de tri.
            ColumnHeader nouvelleColonneIndividu = LvChoixIndividu.Columns[e.Column];

            // Déterminez le nouvel ordre de tri.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumnIndividu == null)
            {
                // Nouvelle colonne. Trier assendant
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // Voir si c'est la même colonne.
                if (nouvelleColonneIndividu == SortingColumnIndividu)
                {
                    // Même colonne. Changer l'ordre de tri.
                    if (SortingColumnIndividu.Text.StartsWith("▼ "))
                    {
                        sort_order = SortOrder.Descending;
                    }
                    else
                    {
                        sort_order = SortOrder.Ascending;
                    }
                }
                else
                {
                    // Nouvelle colonne. Trier par ordre croissant.
                    sort_order = SortOrder.Ascending;
                }
                // Supprimez l'ancien indicateur de tri.
                SortingColumnIndividu.Text = SortingColumnIndividu.Text.Substring(2);
            }

            // Affiche le nouvel ordre de tri.
            SortingColumnIndividu = nouvelleColonneIndividu;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumnIndividu.Text = "▼ " + SortingColumnIndividu.Text;
            }
            else
            {
                SortingColumnIndividu.Text = "▲ " + SortingColumnIndividu.Text;
            }

            // Créez un comparateur.
            LvChoixIndividu.ListViewItemSorter = new ListViewComparer(e.Column, sort_order);

            // Sort.
            LvChoixIndividu.Sort();
        }
        private void LvChoixFamilleConjoint_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //SSS v
            // Obtenez la nouvelle colonne de tri.
            ColumnHeader nouvelleColonneFamille = LvChoixFamille.Columns[e.Column];

            // Déterminez le nouvel ordre de tri.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumnFamille == null)
            {
                // Nouvelle colonne. Trier p
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // Voir si c'est la même colonne.
                if (nouvelleColonneFamille == SortingColumnFamille)
                {
                    // Même colonne. Changer l'ordre de tri.
                    if (SortingColumnFamille.Text.StartsWith("▼ "))
                    {
                        sort_order = SortOrder.Descending;
                    }
                    else
                    {
                        sort_order = SortOrder.Ascending;
                    }
                }
                else
                {
                    // Nouvelle colonne. Trier par ordre croissant.
                    sort_order = SortOrder.Ascending;
                }

                // Supprimez l'ancien indicateur de tri.
                SortingColumnFamille.Text = SortingColumnFamille.Text.Substring(2);
            }
            // Affiche le nouvel ordre de tri.
            SortingColumnFamille = nouvelleColonneFamille;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumnFamille.Text = "▼ " + SortingColumnFamille.Text;
            }
            else
            {
                SortingColumnFamille.Text = "▲ " + SortingColumnFamille.Text;
            }
            // Créez un comparateur.
            LvChoixFamille.ListViewItemSorter = new ListViewComparer(e.Column, sort_order);
            // Sort.
            LvChoixFamille.Sort();
        }
        private void DossierPagesWebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AvoirDossierPageWeb();
        }
        private void ÀProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form l = new A_propos
            {
                StartPosition = FormStartPosition.Manual,
                Left = this.Left + 145,
                Top = this.Top + 60
            };
            l.ShowDialog(this);
        }
        private void DossierPageMédiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AvoirDossierMedias();
        }
        private void VoirIDToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (FichierGEDCOMaLire != "")
            {

                MessageBox.Show("Les fiches HTML doivent être re-générer pour voir le changement.", "Voir ID a été changer",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Warning);
            }
        }
        private void PhotoPrincipalToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (FichierGEDCOMaLire != "")
            {
                MessageBox.Show("Les fiches HTML doivent être re-générer pour voir le changement.", "Photo pricipale a été changer",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Warning);
            }
        }
        private void VoirMédiaToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (FichierGEDCOMaLire != "")
            {
                MessageBox.Show("Les fiches HTML doivent être re-générer pour voir le changement.", "Voir média a été changer",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Warning);
            }
        }
        private void DateLongueToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (FichierGEDCOMaLire != "")
            {
                MessageBox.Show("Les fiches HTML doivent être re-générer pour voir le changement.", "Voir date longue a été changer",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Warning);
            }
        }
        
        private void AideToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("file:///" + Application.StartupPath + @"\aide\aide.html");
            }
            catch (Exception msg)
            {
                MessageBox.Show("L'aide n'est pas disponible.\r\n\r\n", "Aide ?",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);
                if (msg.Message == null) { }
            }
        }
        private void LvChoixIndividu_DoubleClick(object sender, EventArgs e)
        {
            if (!DossierHTMLValide())
            {
                return;
            }
            Form_desactiver(true);
            if (LvChoixIndividu.SelectedItems.Count == 0) return;
            ListViewItem item = LvChoixIndividu.SelectedItems[0];
            IDCourantIndividu = item.SubItems[0].Text;
            HTML.Individu(IDCourantIndividu, false, /*IDCourantIndividu*/ DossierSortie);
            try
            {
                System.Diagnostics.Process.Start("file:///" + DossierSortie + @"\individus\page.html");
                Form_desactiver(false);
            }
            catch (Exception msg)
            {
                Form_desactiver(false);
                string message = "Erreur dans la création du fichiers " + DossierSortie + @"\individus\page.html.";
                GEDCOMClass.Voir_message(message, msg.Message, Avoir_code_erreur());
            }
        }
        private void OuvrirDossierDesFichesHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Properties.Settings.Default.DossierHTML))
            {
                MessageErreur("S.V.P. Spécifiez dans les paramêtres, le dossier des fiches HTML.");
                return;
            }
            System.Diagnostics.Process.Start(@Properties.Settings.Default.DossierHTML);
        }
        private void RechercheIndividuTB_TextChanged(object sender, EventArgs e)
        {
            if (RechercheIndividuTB.Text == "")
            {
                LvChoixIndividu.SelectedItems.Clear();
                AvantIndividuB.Visible = true;
                ApresIndividuB.Visible = true;

                return;
            }
            string[] mots = RechercheIndividuTB.Text.ToLower().Split(' ');
            AvantIndividuB.Visible = true;
            ApresIndividuB.Visible = true;
            for (int f = 0; f < LvChoixIndividu.Items.Count; f++)
            {
                bool Ok = true;
                foreach(string m in mots)
                {
                    if (LvChoixIndividu.Items[f].SubItems[1].Text.ToLower().IndexOf(m) == -1) Ok = false;
                }
                if (Ok)
                {
                    LvChoixIndividu.Items[f].Selected = true;
                    LvChoixIndividu.EnsureVisible(f);
                    return;
                }
                else
                {
                    LvChoixIndividu.Items[f].Selected = false;
                }
            }
        }
        private void RechercheConjointeTB_TextChanged(object sender, EventArgs e)
        {
            if (RechercheFamilleTB.Text == "")
            {
                LvChoixFamille.SelectedItems.Clear();
                AvantFamilleB.Visible = true;
                ApresFamilleB.Visible = true;
                return;
            }
            string[] mots = RechercheFamilleTB.Text.ToLower().Split(' ');
            AvantFamilleB.Visible = true;
            ApresFamilleB.Visible = true;
            for (int f = 0; f < LvChoixFamille.Items.Count; f++)
            {
                bool Ok = true;
                foreach (string m in mots)
                {
                    if (
                        (LvChoixFamille.Items[f].SubItems[1].Text.ToLower().IndexOf(m) == -1) &&
                        (LvChoixFamille.Items[f].SubItems[2].Text.ToLower().IndexOf(m) == -1)
                        )
                        Ok = false;
                }
                if (Ok)
                {
                    LvChoixFamille.Items[f].Selected = true;
                    LvChoixFamille.EnsureVisible(f);
                    return;
                }
                else
                {
                    LvChoixFamille.Items[f].Selected = false;
                }
            }
        }
        private void ApresIndividuB_Click(object sender, EventArgs e)
        {
            int index = 0;
            string[] s = RechercheIndividuTB.Text.ToLower().Split(' ');
            if (LvChoixIndividu.SelectedItems.Count > 0)
            {
                index = LvChoixIndividu.SelectedIndices[0];
            }
            for (int f = index + 1; f < LvChoixIndividu.Items.Count; f++)
            {
                bool trouver = true;
                foreach (string fe in s)
                {
                    if (LvChoixIndividu.Items[f].SubItems[1].Text.ToLower().IndexOf(fe) < 0) trouver = false;
                }
                if (trouver)
                {
                    LvChoixIndividu.Items[f].Selected = true;
                    LvChoixIndividu.EnsureVisible(f);
                    return;
                }
            }
            for (int f = 0; f < index; f++)
            {
                bool trouver = true;
                foreach (string fe in s)
                {
                    if (LvChoixIndividu.Items[f].SubItems[1].Text.ToLower().IndexOf(fe) < 0) trouver = false;
                }
                if (trouver)
                {
                    LvChoixIndividu.Items[f].Selected = true;
                    LvChoixIndividu.EnsureVisible(f);
                    return;
                }
            }
        }
        private void Animation(bool actif)
        {
            if (!actif)
            {
                Lb_animation.Visible = false;
            }
            else
            {
                Lb_animation.Visible = true;
                Random rnd = new Random();
                string ligne = "";
                for (int f = 0; f < 7; f++)
                {
                    if (rnd.Next(0, 2) == 0) ligne += "▀"; else ligne += "▄";
                }
                Lb_animation.Text = ligne.Substring(0, 5);
            }
            Application.DoEvents();
        }
        private void ApresConjointeB_Click(object sender, EventArgs e)
        {
            int index = 0;
            string[] s = RechercheFamilleTB.Text.ToLower().Split(' ');
            if (LvChoixFamille.SelectedItems.Count > 0)
            {
                index = LvChoixFamille.SelectedIndices[0];
            }
            for (int f = index + 1; f < LvChoixFamille.Items.Count; f++)
            {
                bool trouver = true;
                foreach (string fe in s)
                {
                    if (
                        (LvChoixFamille.Items[f].SubItems[1].Text.ToLower().IndexOf(fe) < 0) &&
                        (LvChoixFamille.Items[f].SubItems[2].Text.ToLower().IndexOf(fe) < 0)
                        ) 
                    trouver = false;
                }
                if (trouver)
                {
                    LvChoixFamille.Items[f].Selected = true;
                    LvChoixFamille.EnsureVisible(f);
                    return;
                }
            }
            for (int f = 0; f < index; f++)
            {
                bool trouver = true;
                foreach (string fe in s)
                {
                    if (
                        (LvChoixFamille.Items[f].SubItems[1].Text.ToLower().IndexOf(fe) < 0) &&
                        (LvChoixFamille.Items[f].SubItems[2].Text.ToLower().IndexOf(fe) < 0)
                        )
                        trouver = false;
                }
                if (trouver)
                {
                    LvChoixFamille.Items[f].Selected = true;
                    LvChoixFamille.EnsureVisible(f);
                    return;
                }
            }
        }
        private void AvantIndividuB_Click(object sender, EventArgs e)
        {
            int index = 0;
            string[] s = RechercheIndividuTB.Text.ToLower().Split(' ');
            if (LvChoixIndividu.SelectedItems.Count > 0)
            {
                index = LvChoixIndividu.SelectedIndices[0];
            }
            for (int f = index - 1; f > -1; f--)
            {
                bool trouver = true;
                foreach (string fe in s)
                {
                    if (LvChoixIndividu.Items[f].SubItems[1].Text.ToLower().IndexOf(fe) < 0) trouver = false;
                }
                if (trouver)
                {
                    LvChoixIndividu.Items[f].Selected = true;
                    LvChoixIndividu.EnsureVisible(f);
                    return;
                }
            }
            for (int f = LvChoixIndividu.Items.Count - 1; f > index; f--)
            {
                bool trouver = true;
                foreach (string fe in s)
                {
                    if (LvChoixIndividu.Items[f].SubItems[1].Text.ToLower().IndexOf(fe) < 0) trouver = false;
                }
                if (trouver)
                {
                    LvChoixIndividu.Items[f].Selected = true;
                    LvChoixIndividu.EnsureVisible(f);
                    return;
                }
            }
        }
        private void AvantConjointeB_Click(object sender, EventArgs e)
        {
            int index = 0;
            AvantFamilleB.Visible = false;
            ApresFamilleB.Visible = false;
            if (RechercheFamilleTB.Text == "") return;
            AvantFamilleB.Visible = true;
            ApresFamilleB.Visible = true;
            string[] s = RechercheFamilleTB.Text.ToLower().Split(' ');
            if (LvChoixFamille.SelectedItems.Count > 0)
            {
                index = LvChoixFamille.SelectedIndices[0];
            }
            for (int f = index - 1; f > -1; f--)
            {
                bool trouver = true;
                foreach (string fe in s)
                {
                    if (
                        (LvChoixFamille.Items[f].SubItems[1].Text.ToLower().IndexOf(fe) < 0) &&
                        (LvChoixFamille.Items[f].SubItems[2].Text.ToLower().IndexOf(fe) < 0)
                    )
                        
                        trouver = false;
                }
                if (trouver)
                {
                    LvChoixFamille.Items[f].Selected = true;
                    LvChoixFamille.EnsureVisible(f);
                    return;
                }
            }
            for (int f = LvChoixFamille.Items.Count - 1; f > index; f--)
            {
                bool trouver = true;
                foreach (string fe in s)
                {
                    if (
                        (LvChoixFamille.Items[f].SubItems[1].Text.ToLower().IndexOf(fe) < 0) &&
                        (LvChoixFamille.Items[f].SubItems[2].Text.ToLower().IndexOf(fe) < 0)
                        )
                        trouver = false;
                }
                if (trouver)
                {
                    LvChoixFamille.Items[f].Selected = true;
                    LvChoixFamille.EnsureVisible(f);
                    return;
                }
            }
        }
        private void FermerProgramme_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void LvChoixFamille_DoubleClick(object sender, EventArgs e)
        {
            if (!DossierHTMLValide())
            {
                return;
            }
            Form_desactiver(true);
            if (LvChoixFamille.SelectedItems.Count == 0)
                return;
            ListViewItem item = LvChoixFamille.SelectedItems[0];
            IDCourantFamilleConjoint = item.SubItems[0].Text;
            HTML.Famille(IDCourantFamilleConjoint, false, DossierSortie);
            System.Diagnostics.Process.Start("file:///" + DossierSortie + @"\familles\page.html");
            Form_desactiver(false);
        }
        private void BaliseBt_Click(object sender, EventArgs e)
        {
            if (File.Exists(Properties.Settings.Default.DossierHTML + @"\balise.txt"))
                Process.Start(Properties.Settings.Default.DossierHTML + @"\balise.txt");
        }
        private void DeboguerTb_Click(object sender, EventArgs e)
        {
            if (File.Exists(Properties.Settings.Default.DossierHTML + @"\deboguer.txt"))
                Process.Start(Properties.Settings.Default.DossierHTML + @"\deboguer.txt");
        }
        private void VoirDateDeChangementToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void VoirReferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void VoirNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void VoirReferenceToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (FichierGEDCOMaLire != "")
            {

                MessageBox.Show("Les fiches HTML doivent être re-générer pour voir le changement.", "Voir reférences a été changer",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Warning);
            }
        }
        private void VoirDateDeChangementToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (FichierGEDCOMaLire != "")
            {

                MessageBox.Show("Les fiches HTML doivent être re-générer pour voir le changement.", "Voir date de changement a été changer",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Warning);
            }
        }

        private void VoirNoteToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (FichierGEDCOMaLire != "")
            {

                MessageBox.Show("Les fiches HTML doivent être re-générer pour voir le changement.", "Voir Note a été changer",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Warning);
            }
        }

        private void ParamètresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form l = new Parametre
            {
                StartPosition = FormStartPosition.Manual,
                Left = this.Left + 70,
                Top = this.Top + 60
            };
            l.ShowDialog(this);
        }
        private void Bt_annuler_Click(object sender, EventArgs e)
        {
            annuler = true;
        }
        private void Btn_voir_fiche_Click(object sender, EventArgs e)
        {
            if (!DossierHTMLValide())
            {
                return;
            }
            {
                if (IDCourantIndividu == "")
                {
                    MessageBox.Show("Faites votre choix dans la liste Individu.", "Choix ?",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Warning);
                }
                else
                {
                    Btn_total.Visible = false;
                    Btn_voir_fiche_individu.Visible = false;
                    Btn_voir_fiche_famille.Visible = false;
                    Form_desactiver(true);
                    Application.DoEvents();
                    HTML.Individu(IDCourantIndividu, false, DossierSortie);
                    System.Diagnostics.Process.Start("file:///" + DossierSortie + @"\individus\page.html");
                    Form_desactiver(false);
                    Btn_total.Visible = true;
                    Btn_voir_fiche_individu.Visible = true;
                    Btn_voir_fiche_famille.Visible = true;
                    Application.DoEvents();
                }
            }
        }
        private void Btn_HTML_Click(object sender, EventArgs e)
        {
            Fichier_total();
            Form_desactiver(false);
            Btn_total.Visible = true;
            Btn_voir_fiche_individu.Visible = true;
            Btn_voir_fiche_famille.Visible = true;
            Lb_HTML_1.Text = "";
            Lb_HTML_2.Text = "";
            Lb_HTML_3.Text = "";
            Lb_HTML_4.Text = "";
            annuler = false;
            Btn_annuler_HTML.Visible = false;
        }
        private void Btn_annuler_Click(object sender, EventArgs e)
        {
            annuler = true;
        }
        private void Btn_anuler_HTML_Click(object sender, EventArgs e)
        {
            annuler = true;
            if (Properties.Settings.Default.voir_ToolTip)
                t1.Show("Arrêter l'opération", Btn_annuler_HTML);
        }
        private void Btn_balise_Click(object sender, EventArgs e)
        {
            if (File.Exists(Properties.Settings.Default.DossierHTML + @"\balise.txt"))
                Process.Start(Properties.Settings.Default.DossierHTML + @"\balise.txt");
        }
        private void Btn_deboguer_Click(object sender, EventArgs e)
        {
            if (File.Exists(Properties.Settings.Default.DossierHTML + @"\deboguer.txt"))
                Process.Start(Properties.Settings.Default.DossierHTML + @"\deboguer.txt");
        }
        private void Btn_voir_fiche_famille_Click(object sender, EventArgs e)
        {
            if (!DossierHTMLValide())
            {
                return;
            }
            
            if (IDCourantFamilleConjoint == "")
            {
                MessageBox.Show("Faites votre choix dans la liste Famille.", "Choix ?",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            else
            {
                Btn_total.Visible = false;
                Btn_voir_fiche_individu.Visible = false;
                Btn_voir_fiche_famille.Visible = false;
                Form_desactiver(true);
                Application.DoEvents();
                HTML.Famille(IDCourantFamilleConjoint, false, DossierSortie);
                System.Diagnostics.Process.Start("file:///" + DossierSortie + @"\familles\page.html");
                Form_desactiver(false);
                Btn_total.Visible = true;
                Btn_voir_fiche_individu.Visible = true;
                Btn_voir_fiche_famille.Visible = true;
                Application.DoEvents();
            }
        }
        private void Btn_erreur_Click(object sender, EventArgs e)
        {
            if (File.Exists(Properties.Settings.Default.DossierHTML + @"\erreur.txt"))
                Process.Start(Properties.Settings.Default.DossierHTML + @"\erreur.txt");
        }

        private void Btn_voir_fiche_individu_MouseHover(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.voir_ToolTip)
                t1.Show("Voir l'individu sélectionné", Btn_voir_fiche_individu);
        }

        private void Btn_voir_fiche_famille_MouseHover(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.voir_ToolTip)
                t1.Show("Voir la famille sélectionné", Btn_voir_fiche_famille);
        }

        private void Btn_total_MouseHover(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.voir_ToolTip)
                t1.Show("Voir tous les individus et les familles avec index", Btn_total);
        }

        private void Btn_balise_MouseHover(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.voir_ToolTip)
                t1.Show("Voir le journal des balises non reconnue", Btn_balise);
        }

        private void Btn_deboguer_MouseHover(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.voir_ToolTip)
                t1.Show("Voir le journal de deboque", Btn_deboguer);
        }

        private void Btn_erreur_MouseHover(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.voir_ToolTip)
                t1.Show("Voir le journal des erreurs", Btn_erreur);
        }

        private void Btn_annuler_MouseHover(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.voir_ToolTip)
                t1.Show("Arrêter l'opération", Btn_annuler);
        }

        private void BienvenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form l = new Bienvenu
            {
                StartPosition = FormStartPosition.Manual,
                Left = this.Left + 145,
                Top = this.Top + 100
            };
            l.ShowDialog(this);
        }
    }
    internal class ListViewItemComparer : IComparer
    {
        private readonly int col;
        public ListViewItemComparer()
        {
            col = 0;
        }
        public ListViewItemComparer(int column)
        {
            col = column;
        }
        public int Compare(object x, object y)
        {
            return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
        }
    }
}
