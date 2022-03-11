/*
 Licence français

     GEDCOM-HTML Ce logiciel permet d'extraire les informations d'un fichier GEDCOM
     sous forme de fichier HTML pour tous les individus et toutes les familles.

     Copyright (C) 2022 Daniel Pambrun

     Ce programme est un logiciel libre : vous pouvez le redistribuer et/ou le modifier
     sous les termes de la licence publique générale GNU telle que publiée par
     la Free Software Foundation, soit la version 3 de la Licence.

     Ce programme est distribué dans l'espoir qu'il sera utile,
     mais SANS AUCUNE GARANTIE ; sans même la garantie implicite de
     QUALITÉ MARCHANDE ou ADAPTATION À UN USAGE PARTICULIER. Voir le
     Licence publique générale GNU pour plus de détails.

     Vous devriez avoir reçu une copie de la licence publique générale GNU
     avec ce programme. Sinon, consultez <https://www.gnu.org/licenses/>.

Licence English
    GEDCOM-HTML This software makes it possible to extract information from a GEDCOM file
    in the form of an HTML file for all individuals and all families.
    
    Copyright (C) 2022 Daniel Pambrun

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

/*
    Pour connaitre la méthode qui a appelé une metode

    R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
    , [CallerLineNumber] int callerLineNumber = 0
*/
using GEDCOM;
using howto_sort_list_columns;
using HTML;
using Squirrel; //nnn
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using Routine;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks; //nnn
using System.Windows.Forms;

namespace GH
{
    public partial class GH : Form
    {
        public static string fichier_deboguer = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)) + @"\GH_deboguer.html";
        public static string fichier_balise = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)) + @"\GH\balise.html";
        public static string fichier_erreur = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)) + @"\GH_erreur.html";
        public static string fichier_GEDCOM = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)) + @"\GH_GEDCOM.html";

        public static string dossier_sortie;
        public static bool annuler;
        public static string erreur;
        public bool Ok_anuler = false;
        public readonly HTMLClass HTML = new HTMLClass();
        public int indexConjoint = 0;
        public int indexConjointe = 0;
        public int indexIndividu = 0;
        public static string FichierGEDCOM = null;
        public List<GEDCOMClass.NOTE_RECORD> liste_Code_HTML_Note = new List<GEDCOMClass.NOTE_RECORD>();
        private ColumnHeader SortingColumnIndividu = null;
        private ColumnHeader SortingColumnFamille = null;

        //Create the ToolTip and associate with the Form container.
        readonly ToolTip Info_bulle = new ToolTip();
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
        public static string FichierGEDCOMaLire = null;
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

        // paramètre
        public static class Para
        {
            public static string arriere_plan;
            public static int location_X;
            public static int location_Y;
            public static string dosssier_HTML;
            public static string dossier_media;
            public static string dossier_GEDCOM;
            public static bool voir_ID;
            public static bool voir_media;
            public static bool date_longue;
            public static bool voir_date_changement;
            public static bool voir_chercheur;
            public static bool voir_reference;
            public static bool voir_note;
            public static bool voir_carte;
            public static bool voir_info_bulle;
            public static bool mode_depanage;
        // confidentiel
            // tout événement
            public static bool tout_evenement;
            public static bool tout_evenement_date;
            public static int tout_evenement_ans;
            // naissance
            public static bool naissance_date;
            public static int naissance_ans;

            // baptème
            public static bool bapteme_date;
            public static int bapteme_ans;

            // union
            public static bool union_date;
            public static int union_ans;
            // décès
            public static bool deces_date;
            public static int deces_ans;
            // inhumation
            public static bool inhumation_date;
            public static int inhumation_ans;


            // ordonnance
            public static bool ordonnance_date;
            public static int ordonnance_ans;

            // autre
            public static bool autre_date;
            public static int autre_ans;
            // testament
            public static bool testament_information;
            public static int testament_ans;
            // citoyen
            public static bool citoyen_date;
            public static int citoyen_ans;
        }
        public static class Priver
        {
            // naissance
            public static bool naissance_date;
            public static int naissance_ans;

            // bapteme
            public static bool bapteme_date;
            public static int bapteme_ans;


            // mariage
            public static bool union_date;
            public static int union_ans;
            
            // décès
            public static bool deces_date;
            public static int deces_ans;
            // inhumation
            public static bool inhumation_date;
            public static int inhumation_ans;
            // décès
            public static bool cause_deces_date;
            public static int cause_deces_ans;

            // ordonnance
            public static bool ordonnance_date;
            public static int ordonnance_ans;

            // autre
            public static bool autre_date;
            public static int autre_ans;

            // testament
            public static bool testament_information;
            public static int testament_ans;
            // citoyen
            public static bool citoyen_date;
            public static int citoyen_ans;
        }
        public GH()
        {
            InitializeComponent();
            var task = Verifier_mise_a_jour();
            //Verifier_mise_a_jour();
            //task.Wait();
            task.ToString();
        }
#pragma warning disable CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone

        private void Genere_page_individu(string ID)
        {
            Regler_code_erreur();
            if (!DossierHTMLValide())
            {
                return;
            }
            if (ID == "")
            {
                int X = Cursor.Position.X;
                int Y = Cursor.Position.Y;
                MessageBox.Show("Faites votre choix dans la liste Individu.", "Choix ?",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                Cursor.Position = new Point(X, Y);
            }
            else
            {
                Animation(true);
                Pb_annuler.Visible = true;
                string nom = HTML.Fiche_individu(ID, dossier_sortie, false);
                Page page = new Page("file:///" + dossier_sortie + @"\individus\page.html", nom);
                Page frm = page;
                frm.Tag = this;
                frm.Show(this);
                Pb_annuler.Visible = false;
                Animation(false);
            }
        }
        private void Genere_page_famille(string ID)
        {
            Regler_code_erreur();
            if (!DossierHTMLValide())
            {
                return;
            }
            if (ID == "")
            {
                int X = Cursor.Position.X;
                int Y = Cursor.Position.Y;
                MessageBox.Show("Faites votre choix dans la liste Individu.", "Choix ?",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                Cursor.Position = new Point(X, Y);
            }
            else
            {
                Animation(true);
                Pb_annuler.Visible = true;
                string nom = HTML.Fiche_famille(ID, dossier_sortie, false);
                Page page = new Page("file:///" + dossier_sortie + @"\familles\page.html", nom);
                Page frm = page;
                frm.Tag = this;
                frm.Show(this);
                Pb_annuler.Visible = false;
                Animation(false);
            }
        }
        private void Tout_visible()
        {
            Regler_code_erreur();
            Pb_individu_avant.Visible = true;
            Pb_individu_apres.Visible = true;
            Pb_famille_avant.Visible = true;
            Pb_famille_apres.Visible = true;

            Pb_fiche_individu.Visible = true;
            Pb_fiche_famille.Visible = true;
            Pb_total.Visible = true;

            Pb_annuler.Visible = true;
            Pb_debug.Visible = true;
            Pb_erreur.Visible = true;
            Pb_balise.Visible = true;
            Pb_GEDCOM.Visible = true;
            Pb_recherche_individu.Visible = true;
            RechercheIndividuTB.Visible = true;
            RechercheIndividuB.Visible = true;

            Pb_recherche_famille.Visible = true;
            RechercheFamilleTB.Visible = true;
            RechercheFamilleB.Visible = true;

            LvChoixIndividu.Visible = true;
            LvChoixFamille.Visible = true;
            Gb_info_GEDCOM.Visible = true;
        }
        private async Task Verifier_mise_a_jour() //nnn
#pragma warning restore CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        {
            using (var manager = new UpdateManager("https://github.com/dapam873/GH/")) // adresse web pour la mise à jour
            //using (var manager = new UpdateManager(@"D:\Data\Documents\Mega\GH\GH\Releases", "GH" )) // dossier pour la mise à jour
            {
                Regler_code_erreur();
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
            Regler_code_erreur();
            Form l = new Bienvenu
            {
                StartPosition = FormStartPosition.Manual,
                Left = 70,
                Top = 60
            };
            l.ShowDialog(this);
        }
        //readonly ToolTip t1 = new ToolTip();
        private void OuvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            while (!Directory.Exists(Para.dosssier_HTML))
            {
                DialogResult reponse = R.Afficher_message(
                    "S.V.P.Spécifiez dans les paramêtres, le dossier HTML.",
                    null,
                    erreur,
                    null,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error
                    );
                if (reponse == System.Windows.Forms.DialogResult.Cancel)
                    return;
                if (reponse == System.Windows.Forms.DialogResult.OK)
                {
                    Voir_paramettre();
                }
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Para.dossier_GEDCOM;
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
            if ( R.IsNotNullOrEmpty(FichierGEDCOMaLire))
            {
                Effacer_Log();
                dossier_sortie = Para.dosssier_HTML + @"\" + Path.GetFileNameWithoutExtension(FichierGEDCOMaLire);
                if (Directory.Exists(dossier_sortie))
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
                    {
                        annuler = true;
                        return;
                    }
                }
            }
            Animation(true);
            if (R.IsNotNullOrEmpty(FichierGEDCOMaLire))
            {
                Animation(true);
                Pb_annuler.Visible = true;
                Pb_balise.Visible = false;
                menuPrincipal.Visible = false;
                FormVisible(false);
                Application.DoEvents();

                if (!Lire_fichier_GEDCOM(FichierGEDCOMaLire))
                {
                    Application.DoEvents();
                    Pb_annuler.Visible = false;
                    Gb_info_GEDCOM.Visible = false;
                    this.Cursor = Cursors.Default;
                    annuler = false;
                    menuPrincipal.Visible = true;
                    Animation(false);
                    return;
                }
                Application.DoEvents();

                Para.dossier_GEDCOM = Path.GetDirectoryName(FichierGEDCOMaLire);
                if (Para.dosssier_HTML != "") CreerDossier();
                Application.DoEvents();
                FichierGEDCOM = Path.GetFileName(FichierGEDCOMaLire);
                this.Text = " " + FichierGEDCOM;
                Pb_annuler.Visible = false;
                annuler = false;
                Application.DoEvents();
            }
            menuPrincipal.Visible = true;
            FormVisible(true);
            Pb_GEDCOM.Visible = true;
            Animation(false);
        }
        private string AssemblerPatronymePrenom(string patronyme, string prenom)
        {
            Regler_code_erreur();
            if (prenom == null && patronyme == null) return null;
            if (prenom == "" && patronyme == "") return null;
            if (prenom == "") prenom = "?";
            if (prenom == null) prenom = "?";
            if (patronyme == "") patronyme = "?";
            if (patronyme == null) patronyme = "?";
            return patronyme + ", " + prenom;
        }
        private static void Regler_code_erreur([CallerLineNumber] int sourceLineNumber = 0)
        {
            erreur = "GH" + sourceLineNumber;
        }
        private void AvoirDossierPageWeb()
        {
            Regler_code_erreur();
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog
            {
                Description = "Ou enregister les fiches HTML"
            };
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Para.dosssier_HTML = folderBrowserDialog1.SelectedPath;
                TextBox dossierHTML = Application.OpenForms["Parametre"].Controls["TbDossierHTML"] as TextBox;
                dossierHTML.Text = Para.dosssier_HTML;
            }
        }
        private void ChoixLVIndividu_SelectedIndexChanged(object sender, EventArgs e)
        {
            Regler_code_erreur();
            if (LvChoixIndividu.SelectedItems.Count == 0) return;
            ListViewItem item = LvChoixIndividu.SelectedItems[0];
            IDCourantIndividu = item.SubItems[0].Text;
        }
        private void CreerDossier()
        {
            if (!DossierHTMLValide())
            {
                return;
            }
            Regler_code_erreur();
            try
            {
                Regler_code_erreur();
                DirectoryInfo di = new DirectoryInfo(@dossier_sortie);
                
                // créé les dossiers
                if (EffacerLesDossier(di))
                {
                    Application.DoEvents();
                    //Thread.Sleep(2000);
                    if (!Directory.Exists(dossier_sortie))
                    {
                        Application.DoEvents();
                        Directory.CreateDirectory(dossier_sortie);
                    }
                    if (!Directory.Exists(dossier_sortie + @"\medias"))
                    {
                        Application.DoEvents();
                        Directory.CreateDirectory(dossier_sortie + @"\medias");
                    }
                    if (!Directory.Exists(dossier_sortie + @"\commun"))
                    {
                        Application.DoEvents();
                        Directory.CreateDirectory(dossier_sortie + @"\commun");
                    }
                    if (!Directory.Exists(dossier_sortie + @"\commun\images"))
                    {
                        Application.DoEvents();
                        Directory.CreateDirectory(dossier_sortie + @"\commun\images");
                    }
                    if (!Directory.Exists(dossier_sortie + @"\familles"))
                    {
                        Application.DoEvents();
                        Directory.CreateDirectory(dossier_sortie + @"\familles");
                    }
                    if (!Directory.Exists(dossier_sortie + @"\familles\medias"))
                    {
                        Application.DoEvents();
                        Directory.CreateDirectory(dossier_sortie + @"\familles\medias");
                    }
                    if (!Directory.Exists(dossier_sortie + @"\individus"))
                    {
                        Application.DoEvents();
                        Directory.CreateDirectory(dossier_sortie + @"\individus");
                    }
                    if (!Directory.Exists(dossier_sortie + @"\individus\medias"))
                    {
                        Application.DoEvents();
                        Directory.CreateDirectory(dossier_sortie + @"\individus\medias");
                    }
                    Application.DoEvents();
                    
                    Application.DoEvents();
                    // copier tous les fichiers commun
                    string souceDossier = @Application.StartupPath + @"\commun\";
                    string destinationDossier = @dossier_sortie + @"\commun\";
                    string[] listeFichier = Directory.GetFiles(souceDossier, "*.*");
                    foreach (string f in listeFichier)
                    {
                        // Supprimez le chemin du nom de fichier.
                        string nomFichier = Path.GetFileName(f); ;
                        // Utilisez la méthode Path.Combine pour ajouter le nom de fichier au chemin.
                        // Remplacera si le fichier de destination existe déjà.
                        File.Copy(Path.Combine(souceDossier, nomFichier), Path.Combine(destinationDossier, nomFichier), true);
                    }
                    Application.DoEvents();
                    souceDossier = @Application.StartupPath + @"\commun\images\";
                    destinationDossier = @dossier_sortie + @"\commun\images\";
                    listeFichier = Directory.GetFiles(souceDossier, "*.*");
                    foreach (string f in listeFichier)
                    {
                        // Supprimez le chemin du nom de fichier.
                        string nomFichier = Path.GetFileName(f); ;
                        // Utilisez la méthode Path.Combine pour ajouter le nom de fichier au chemin.
                        // Remplacera si le fichier de destination existe déjà.
                        File.Copy(Path.Combine(souceDossier, nomFichier), Path.Combine(destinationDossier, nomFichier), true);
                    }
                    Application.DoEvents();
                    // Modifier dapam.css 
                    souceDossier = @Application.StartupPath + @"\commun\";
                    destinationDossier = @dossier_sortie + @"\commun\";
                    string dapam = System.IO.File.ReadAllText(souceDossier + "dapam.css");
                    dapam = dapam.Replace("&&&background-color&&&", Para.arriere_plan);
                    File.WriteAllText(destinationDossier + "dapam.css", dapam);
                    Application.DoEvents();
                }
            }
            catch (Exception msg)
            {
                string message = "Problème pour créer les dossiers.";
                R.Afficher_message(
                    message,
                    msg.Message,
                    erreur,
                    null,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
        }

        private void Directe()
        {
            Regler_code_erreur();
            return;
            /*
            Stopwatch chrono;
            chrono = Stopwatch.StartNew();
            */
            Animation(true);

            // paramètre
            Para.arriere_plan = "F0D0B0";
            Para.voir_ID = true;
            Para.voir_media = true;

            // date 
            Para.voir_date_changement = true;
            Para.date_longue = true;
            Para.tout_evenement = true;
            Para.tout_evenement_date = true;
            Para.tout_evenement_ans = 0;

            Para.voir_chercheur = true;
            Para.voir_reference = true;
            Para.voir_note = true;
            Para.voir_carte = true;
            Para.voir_info_bulle = false;
            Para.mode_depanage = true;
            Pb_log_del.Visible = true;

            // lire fichier
            FichierGEDCOMaLire = @"D:\Data\Documents\Mega\GH\GEDCOM\GEDCOM_5.5.ged";
            dossier_sortie = Para.dosssier_HTML + @"\" + Path.GetFileNameWithoutExtension(FichierGEDCOMaLire);
            
            string souceDossier = @Application.StartupPath + @"\commun\";
            string destinationDossier = @dossier_sortie + @"\commun\";
            // Modifier dapam.css 
            string dapam = System.IO.File.ReadAllText(souceDossier + "dapam.css");
            dapam = dapam.Replace("&&&background-color&&&", Para.arriere_plan);
            File.WriteAllText(destinationDossier + "dapam.css", dapam);
            Lire_fichier_GEDCOM(FichierGEDCOMaLire);
            string[] liste_fichier = new string[] {
                dossier_sortie + @"\index.html",
                dossier_sortie + @"\individus\I01.html",
                dossier_sortie + @"\individus\I02.html",
                dossier_sortie + @"\individus\I03.html",
                dossier_sortie + @"\individus\I04.html",
                dossier_sortie + @"\individus\I05.html",
                dossier_sortie + @"\individus\I06.html",
                dossier_sortie + @"\individus\I07.html",
                dossier_sortie + @"\familles\F01.html",
                dossier_sortie + @"\familles\F02.html"};
            foreach (string fichier in liste_fichier)
            {
                if (File.Exists(fichier))
                    File.Delete(fichier);
            }

            //index
            //HTML.Index("en directe", NombreIndividuTb.Text, NombreFamilleTb.Text);

            //Thread.Sleep(4000);
            
            /*
                Individu
            */
            // individu I01
            //HTML.Fiche_individu("I01", dossier_sortie, true);

            //individu I02
            HTML.Fiche_individu("I02", dossier_sortie, true);

            // individu I03
            //HTML.Fiche_individu("I03", dossier_sortie, true);

            // individu I04
            //HTML.Fiche_individu("I04", dossier_sortie, true);

            // individu I05
            //HTML.Fiche_individu("I05", dossier_sortie, true);

            /*
                Famille
            */

            // Famille F02
           //HTML.Fiche_famille("F02", dossier_sortie, true);

            // Famille F04
            //HTML.Fiche_famille("F04", true, DossierSortie);

            // Famille F06
            //HTML.Fiche_famille("F06", true, DossierSortie);

            foreach (string fichier in liste_fichier)
            {
                if (File.Exists(fichier))
                {
                    string f = fichier + "#groupe_evenement";
                    System.Diagnostics.Process.Start("file:///" + f);
                    Thread.Sleep(2000);
                }
            }

            // balise
            if (File.Exists(fichier_balise))
                Process.Start(fichier_balise);
            
            // debug
            if (File.Exists(fichier_deboguer))
                System.Diagnostics.Process.Start("file:///" + fichier_deboguer);
            
            Animation(false);
            /*
            chrono.Stop();
            TimeSpan durer = TimeSpan.FromMilliseconds(chrono.ElapsedMilliseconds);
            GEDCOMClass.Ecrire_execution_temps("Direct ------------------- ", durer);
            */
            Close();
        }
        private bool DossierHTMLValide([CallerFilePath] string code = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            Regler_code_erreur();
            code = Path.GetFileName(code);
            code = code[0].ToString().ToUpper();
            if (!Directory.Exists(Para.dosssier_HTML))
            {
                DialogResult reponse = R.Afficher_message(
                    "S.V.P. Spécifiez dans les paramêtres, le dossier des fiches HTML.",
                    code + lineNumber + " " + caller,
                    erreur,
                    "Information",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information
                    );
                if (reponse == System.Windows.Forms.DialogResult.Cancel)
                    annuler = true;
                return false;
            }
            return true;
        }
        private static bool EffacerLeDossier1(string dossier)
        {
            Regler_code_erreur();
            if (!Directory.Exists(dossier)) return true;
            try
            {
                string[] listeFichier = Directory.GetFiles(@dossier);
                foreach (string fichier in listeFichier)
                {
                    File.Delete(fichier);
                }
                Directory.Delete(dossier);
                return true;
            }
            catch (Exception msg)
            {
                R.Afficher_message(
                    "Problème pour effacer les dossiers.",
                    msg.Message,
                    erreur,
                    null,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
            return false;
        }
        private bool EffacerLesDossier(DirectoryInfo dossier)
        {
            Regler_code_erreur();
            if (dossier != null)
                if (!dossier.Exists)
                    return true;
            string dossier1 = null;
            if (dossier != null)
                dossier1 = dossier.ToString();
            bool status = EffacerLeDossier1(Para.dosssier_HTML + @"\" + dossier1 + @"\medias");
            if (status) status = EffacerLeDossier1(Para.dosssier_HTML + @"\" + dossier1 + @"\commun");
            if (status) status = EffacerLeDossier1(Para.dosssier_HTML + @"\" + dossier1 + @"\individus\medias");
            if (status) status = EffacerLeDossier1(Para.dosssier_HTML + @"\" + dossier1 + @"\individus");
            if (status) status = EffacerLeDossier1(Para.dosssier_HTML + @"\" + dossier1 + @"\familles\medias");
            if (status) status = EffacerLeDossier1(Para.dosssier_HTML + @"\" + dossier1 + @"\familles");
            return status;
        }
        private void Effacer_Log()
        {
            Regler_code_erreur();
            try
            {
                //string dossier = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\GH";
                // si fichier_debug existe efface fichiers
                if (File.Exists(fichier_deboguer))
                {
                    File.Delete(fichier_deboguer);
                }
                // si fichier_erreur existe efface fichiers
                if (File.Exists(fichier_erreur))
                {
                    File.Delete(fichier_erreur);
                }
                // si fichier_balise existe efface fichiers
                if (File.Exists(fichier_balise))
                {
                    File.Delete(fichier_balise);
                }
                

                // rendre invisible les bouttons
                Pb_debug.Visible = false;
                Pb_erreur.Visible = false;
                Pb_balise.Visible = false;
                Pb_GEDCOM.Visible = false;
            }
            catch (Exception msg)
            {
                if (msg.Message == null)
                {
                    R.Afficher_message(
                        "Problème pour effacer les journaux.",
                        msg.Message,
                        erreur,
                        null,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void GH_FormClosing(object sender, FormClosingEventArgs e)
        {
            Regler_code_erreur();
            //Ecrire_paramettre();
        }

        private void GH_Load(object sender, EventArgs e)
        {
            Regler_code_erreur();
            
            Pb_log_del.Visible= true;
            //Set up the delays for the ToolTip.
            Info_bulle.AutoPopDelay = 5000;
            Info_bulle.InitialDelay = 1000;
            Info_bulle.ReshowDelay = 500;
            //Force the ToolTip text to be displayed whether or not the form is active.
            Info_bulle.ShowAlways = true;
            Info_bulle.IsBalloon = true;
            //Set up the ToolTip text for the Button and Checkbox.

            // rapport
            Info_bulle.SetToolTip(this.Pb_GEDCOM, "Liste du GEDCOM");
            Info_bulle.SetToolTip(this.Pb_debug, "Journal de débogueur");
            Info_bulle.SetToolTip(this.Pb_erreur, "Journal d'erreur");
            Info_bulle.SetToolTip(this.Pb_balise, "Journal de balise");

            // section recherche individu
            Info_bulle.SetToolTip(this.RechercheIndividuTB, "Écrire prénon ou/et patronyme à rechercher");
            Info_bulle.SetToolTip(this.Pb_individu_avant, "Précédent");
            Info_bulle.SetToolTip(this.Pb_individu_apres, "Suivant");

            // section recherche Famille
            Info_bulle.SetToolTip(this.RechercheFamilleTB, "Écrire prénon ou/et patronyme à rechercher");
            Info_bulle.SetToolTip(this.Pb_famille_avant, "Précédent");
            Info_bulle.SetToolTip(this.Pb_famille_apres, "Suivant");

            // fiche
            Info_bulle.SetToolTip(this.Pb_fiche_individu, "Créer la fiche de l'individu sélectionné");
            Info_bulle.SetToolTip(this.Pb_fiche_famille, "Créer la fiche de la famille sélectionnée");
            Info_bulle.SetToolTip(this.Pb_total, "Créer les index et toutes les fiches individus et familles");
            // Log Pb_log_del
            Info_bulle.SetToolTip(this.Pb_log_del, "Effacer les journaux d'événement");

            // tab stop
            RechercheIndividuTB.TabStop = true;
            RechercheIndividuTB.TabIndex = 1;

            Pb_individu_avant.TabStop = true;
            Pb_individu_avant.TabIndex = 2;
            Pb_individu_apres.TabStop = true;
            Pb_individu_apres.TabIndex = 3;
            LvChoixIndividu.TabStop = true;
            LvChoixIndividu.TabIndex = 4;

            RechercheFamilleTB.TabStop = true;
            RechercheFamilleTB.TabIndex = 5;

            Pb_famille_avant.TabStop = true;
            Pb_famille_avant.TabIndex = 6;
            Pb_famille_apres.TabStop = true;
            Pb_famille_apres.TabIndex = 7;
            LvChoixFamille.TabStop = true;
            LvChoixFamille.TabIndex = 8;


            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\GH"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\GH");
            int position_X;
            int position_Y;
            bool position_Bonne = false;
            Regler_code_erreur();
            (position_X, position_Y) = Lire_parametre();
            Effacer_Log();
            this.Visible = false;
            Pb_annuler.Visible = false;

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
            pb_del_1.Image = Properties.Resources.del_off;
            pb_del_2.Image = Properties.Resources.del_off;
            pb_del_3.Image = Properties.Resources.del_off;
            Gb_info_GEDCOM.Visible = false;
            Pb_individu_avant.Visible = false;
            Pb_individu_apres.Visible = false;
            Pb_famille_avant.Visible = false;
            Pb_famille_apres.Visible = false;
            Pb_recherche_individu.Visible = false;
            Pb_recherche_famille.Visible = false;
            LvChoixIndividu.Visible = false;
            lpIndividu.Visible = false;

            LvChoixFamille.Visible = false;
            lbFamilleConjoint.Visible = false;
            Pb_fiche_individu.Visible = false;
            Pb_fiche_famille.Visible = false;
            Pb_total.Visible = false;

            LvChoixIndividu.Columns.Add("ID", 80);
            LvChoixIndividu.Columns.Add("Nom", 200);
            LvChoixIndividu.Columns.Add("Naissance", 80);
            LvChoixIndividu.Columns.Add("Lieu naissance", 268);//275
            LvChoixIndividu.Columns.Add("Décès", 80);
            LvChoixIndividu.Columns.Add("Lieu décès", 268); // 275
            LvChoixIndividu.Columns.Add("N", 0);
            LvChoixIndividu.Columns.Add("D", 0);
            LvChoixIndividu.BackColor = Color.LightSkyBlue;

            LvChoixFamille.View = View.Details;
            LvChoixFamille.GridLines = true;
            LvChoixFamille.FullRowSelect = true;
            LvChoixFamille.Items.Clear();
            LvChoixFamille.Columns.Add("ID", 80);
            LvChoixFamille.Columns.Add("Conjoint", 225);
            LvChoixFamille.Columns.Add("Conjointe", 225);
            LvChoixFamille.Columns.Add("Mariage", 80);
            LvChoixFamille.Columns.Add("Lieu mariage", 366);//380
            LvChoixFamille.Columns.Add("", 0);
            LvChoixFamille.BackColor = Color.LightSkyBlue;


            RechercheIndividuTB.MaximumSize = new Size(280, 20);
            RechercheIndividuTB.MinimumSize = new Size(280, 20);
            RechercheIndividuTB.Size = new Size(280, 20);
            RechercheFamilleTB.MaximumSize = new Size(280, 20);
            RechercheFamilleTB.MinimumSize = new Size(280, 20);
            RechercheFamilleTB.Size = new Size(280, 20);




            PasConfidentiel("", "");
            FormVisible(false);
            foreach (var screen in System.Windows.Forms.Screen.AllScreens)
            {
                if ((position_X >= screen.WorkingArea.Left) && position_X <= (screen.WorkingArea.Width + screen.WorkingArea.Left))
                {
                    if ((position_Y >= screen.WorkingArea.Top) && (position_Y <= screen.WorkingArea.Height - screen.WorkingArea.Top))
                    {
                        position_Bonne = true;
                    }
                }
            }
            this.Left = 100;
            this.Top = 100;
            if (position_Bonne)
            {
                this.Left = Pixel_a_dpi(position_X);
                this.Top = Pixel_a_dpi(position_Y);
            }
            this.Visible = true;
            //Tout_visible();
            Directe(); // à commenter pour la production *** DEBUG ***
            Application.DoEvents();

        }
        private void Ligne_code(
             [CallerLineNumber] int callerLineNumber = 0)
        {
            Regler_code_erreur();
            // pour vérifier ou le code stagne
            Lb_ligne.Text = (callerLineNumber.ToString());
            Application.DoEvents();
        }
        public bool Lire_fichier_GEDCOM(
            string FichierGEDCOMaLire
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + " FichierGEDCOMaLire="+ FichierGEDCOMaLire);
            Regler_code_erreur();
            Animation(true);
            NombreIndividuTb.Text = "";
            NombreFamilleTb.Text = "";
            this.Text = "";
            string FichierGEDCOMaLireTemp = GEDCOMClass.Lire_entete_GEDCOM(FichierGEDCOMaLire);
            Application.DoEvents();
            if (FichierGEDCOMaLireTemp == null) return false;
            FichierGEDCOMaLire = FichierGEDCOMaLireTemp;
            GEDCOMClass.HEADER InfoGEDCOM = GEDCOMClass.Avoir_info_GEDCOM();
            lblCharSet.Text = "Code charactère: " + InfoGEDCOM.N1_CHAR;
            Gb_info_GEDCOM.Visible = true;
            if (!GEDCOMClass.Lire_GEDCOM(FichierGEDCOMaLire)) return false;
            this.Cursor = Cursors.WaitCursor;
            GEDCOMClass.Extraire_GEDCOM();
            if (annuler) return (false);
            InfoGEDCOM = GEDCOMClass.Avoir_info_GEDCOM();
            lblNom.Text = "Nom: " + InfoGEDCOM.N2_SOUR_NAME;
            lblVersionProgramme.Text = "Version: " + InfoGEDCOM.N2_SOUR_VERS;
            lblDateHeure.Text = InfoGEDCOM.N1_DATE + " " + InfoGEDCOM.N2_DATE_TIME;
            lblFichier.Text = "Fichier: " + FichierGEDCOM;
            lblCopyright.Text = InfoGEDCOM.N1_COPR;
            lblVersionGEDCOM.Text = "Version: " + InfoGEDCOM.N2_GEDC_VERS;
            lblCharSet.Text = "Code charactère: " + InfoGEDCOM.N1_CHAR;
            lblLanguage.Text = "Langue: " + InfoGEDCOM.N1_LANG;
            Gb_info_GEDCOM.Visible = true;
            FichierCourant = "";
            Application.DoEvents();
            // créer liste individu
            LvChoixIndividu.View = View.Details;
            LvChoixIndividu.GridLines = true;
            LvChoixIndividu.FullRowSelect = true;
            LvChoixIndividu.Items.Clear();
            List<string> ListeIDIndividu;
            ListeIDIndividu = GEDCOMClass.Avoir_liste_ID_individu();
            ListViewItem itmIndividu;
            //int compteur = 0;
            int nombre = ListeIDIndividu.Count;
            LvChoixIndividu.Items.Clear();
            for (int f = 0; f < nombre; f++)
            {
                if (f % 1000 ==0) Application.DoEvents();
                if (GH.annuler)
                {
                    Pb_annuler.Visible = false;
                    return false;
                }
                bool Ok;
                string IDIndividu = ListeIDIndividu[f];
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
                        if (infoIndividu.N1_NAME_liste.Count >0 )
                            nom = infoIndividu.N1_NAME_liste[0].N0_NAME;
                    }

                }
                // info naissance
                GEDCOMClass.EVEN_ATTRIBUTE_STRUCTURE Naissance;
                (_, Naissance) = GEDCOMClass.Avoir_evenement_naissance(infoIndividu.N1_EVEN_Liste);
                // info Décès
                GEDCOMClass.EVEN_ATTRIBUTE_STRUCTURE Deces;
                (_, Deces) = GEDCOMClass.Avoir_evenement_deces(infoIndividu.N1_EVEN_Liste);
                string[] ligne = new string[8];
                ligne[0] = IDIndividu;
                ligne[1] = nom;

                (ligne[2], _) = HTMLClass.Convertir_date(Naissance.N2_DATE, false);
                if (Naissance.N2_PLAC != null) ligne[3] = Naissance.N2_PLAC.N0_PLAC;
                (ligne[4], _) = HTMLClass.Convertir_date(Deces.N2_DATE, false);
                if (Deces.N2_PLAC != null) ligne[5] = Deces.N2_PLAC.N0_PLAC;
                ligne[6] = Naissance.N2_DATE;
                ligne[7] = Deces.N2_DATE;
                itmIndividu = new ListViewItem(ligne);
                LvChoixIndividu.Items.Add(itmIndividu);
            }
            NombreIndividuTb.Text = string.Format("{0:0,0}", LvChoixIndividu.Items.Count);
            Application.DoEvents();
            // créer liste famille
            LvChoixFamille.View = View.Details;
            LvChoixFamille.GridLines = true;
            LvChoixFamille.FullRowSelect = true;
            LvChoixFamille.Items.Clear();
            List<string> ListeIDFamille;
            ListeIDFamille = GEDCOMClass.Avoir_liste_ID_famille();
            ListViewItem itmFamille;
            nombre = ListeIDFamille.Count;
            for (int f = 0; f < nombre; f++)
            {
                if (f % 1000 == 0) Application.DoEvents();
                if (GH.annuler)
                {
                    Pb_annuler.Visible = false;
                    return false;
                }
                bool Ok;
                string IDFamille = ListeIDFamille[f];
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
                GEDCOMClass.EVEN_ATTRIBUTE_STRUCTURE Mariage = GEDCOMClass.AvoirEvenementMariage(infoFamille.N1_EVEN_Liste);
                string[] ligne = new string[6];
                ligne[0] = IDFamille;
                ligne[1] = nomConjoint;
                ligne[2] = nomConjointe;
                (ligne[3], _) = HTMLClass.Convertir_date(Mariage.N2_DATE, false);
                if (Mariage.N2_PLAC != null) ligne[4] = Mariage.N2_PLAC.N0_PLAC;
                ligne[5] = Mariage.N2_DATE;
                itmFamille = new ListViewItem(ligne);
                LvChoixFamille.Items.Add(itmFamille);
            }

            Application.DoEvents();
            NombreFamilleTb.Text = string.Format("{0:0,0}", LvChoixFamille.Items.Count);
            
            this.Cursor = Cursors.Default;
            System.Windows.Forms.SortOrder sort_order;
            Application.DoEvents();
            sort_order = SortOrder.Ascending;
            Application.DoEvents();
            //sort individu par nom
            LvChoixIndividu.ListViewItemSorter = new ListViewComparer(1, sort_order);
            Application.DoEvents();
            LvChoixIndividu.Sort();
            Application.DoEvents();
            // sort Famille par nom du conjoint
            Application.DoEvents();
            LvChoixFamille.ListViewItemSorter = new ListViewComparer(1, sort_order);
            Application.DoEvents();
            LvChoixFamille.Sort();
            Application.DoEvents();
            
            return true;
        }
        public (int position_X, int position_Y) Lire_parametre()
        {
            Regler_code_erreur();
            // parametre valeur par défaut
            Para.dosssier_HTML = "";
            Para.dossier_media = "";
            Para.dossier_GEDCOM = "";
            Para.arriere_plan = "FFFFFF";
            Para.voir_ID = false;
            Para.voir_media = true;
            Para.date_longue = true;
            Para.voir_date_changement = true;
            Para.voir_chercheur = true;
            Para.voir_reference = true;
            Para.voir_note = true;
            Para.voir_carte = true;
            Para.voir_info_bulle = true;
            Para.mode_depanage = false;
            string Fichier_parametre = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\GH\GH.ini";
            int position_X = 5;
            int position_Y = 5;
            /*bool position_Bonne = false;*/
            // lecture des paramettres
            if (File.Exists(Fichier_parametre))
            {
                string Ligne = "";
                try
                {
                    using (StreamReader sr = File.OpenText(Fichier_parametre))
                    {
                        while ((Ligne = sr.ReadLine()) != null)
                        {
                            if (Ligne == "[location_X]")
                            {
                                position_X = Int32.Parse(sr.ReadLine());
                            }
                            if (Ligne == "[location_Y]")
                            {
                                position_Y = Int32.Parse(sr.ReadLine());
                            }
                            if (Ligne == "[dossier_HTML]")
                            {
                                Para.dosssier_HTML = sr.ReadLine();
                            }
                            if (Ligne == "[dossier_media]")
                            {
                                Para.dossier_media = sr.ReadLine();
                            }
                            if (Ligne == "[dossier_GEDCOM]")
                            {
                                Para.dossier_GEDCOM = sr.ReadLine();
                            }
                            if (Ligne == "[arriere_plan]")
                            {
                                Para.arriere_plan = sr.ReadLine();
                            }
                            if (Ligne == "[voir_ID]")
                            {
                                string a = sr.ReadLine();
                                if (a == "1") Para.voir_ID = true; else Para.voir_ID = false;
                            }
                            if (Ligne == "[voir_media]")
                            {
                                string a = sr.ReadLine();
                                if (a == "1")
                                {
                                    Para.voir_media = true;
                                }
                                else
                                {
                                    Para.voir_media = false;
                                }
                            }
                            if (Ligne == "[date_longue]")
                            {
                                string a = sr.ReadLine();
                                if (a == "1") Para.date_longue = true; else Para.date_longue = false;
                            }
                            if (Ligne == "[voir_date_changement]")
                            {
                                string a = sr.ReadLine();
                                if (a == "1") Para.voir_date_changement = true; else Para.voir_date_changement = false;
                            }
                            if (Ligne == "[voir_chercheur]")
                            {
                                string a = sr.ReadLine();
                                if (a == "1")
                                {
                                    Para.voir_chercheur = true;
                                }
                                else
                                {
                                    Para.voir_chercheur = false;
                                }
                            }
                            if (Ligne == "[voir_reference]")
                            {
                                string a = sr.ReadLine();
                                if (a == "1")
                                {
                                    Para.voir_reference = true;
                                }
                                else
                                {
                                    Para.voir_reference = false;
                                }
                            }
                            if (Ligne == "[voir_note]")
                            {
                                string a = sr.ReadLine();
                                if (a == "1")
                                {
                                    Para.voir_note = true;
                                }
                                else
                                {
                                    Para.voir_note = false;
                                }
                            }
                            if (Ligne == "[voir_carte]")
                            {
                                string a = sr.ReadLine();
                                if (a == "1") Para.voir_carte = true; else Para.voir_carte = false;
                            }
                            if (Ligne == "[voir_info_bulle]")
                            {
                                string a = sr.ReadLine();
                                if (a == "1") Para.voir_info_bulle = true; else Para.voir_info_bulle = false;
                                if (Para.voir_info_bulle)
                                    Info_bulle.Active = true;
                                else
                                    Info_bulle.Active = false;
                            }
                            if (Ligne == "[mode_depanage]")
                            {
                                string a = sr.ReadLine();
                                if (a == "1")
                                {
                                    Para.mode_depanage = true;
                                    //Pb_log_del.Visible = true;
                                }
                                else
                                {
                                    Para.mode_depanage = false;
                                    //Pb_log_del.Visible = false;
                                }
                            }
                            // confidentiel
                            if (Ligne == "[tout_evenement]")
                            {
                                switch (sr.ReadLine())
                                {
                                    case "0": Para.tout_evenement = false; break;
                                    case "1": Para.tout_evenement = true; break;
                                }
                            }
                            if (Ligne == "[tout_evenement_date]")
                            {
                                switch (sr.ReadLine())
                                {
                                    case "0": Para.tout_evenement_date = false; break;
                                    case "1": Para.tout_evenement_date = true; break;
                                }
                            }
                            if (Ligne == "[tout_evenement_ans]")
                            {
                                Int32.TryParse(sr.ReadLine(), out Para.tout_evenement_ans);
                            }

                            // naissance
                            if (Ligne == "[naissance_date]")
                            {
                                switch (sr.ReadLine())
                                {
                                    case "0": Para.naissance_date = false; break;
                                    case "1": Para.naissance_date = true; break;
                                }
                            }
                            if (Ligne == "[naissance_ans]") Int32.TryParse(sr.ReadLine(), out Para.naissance_ans);

                            // baptème
                            if (Ligne == "[bapteme_date]")
                            {
                                switch (sr.ReadLine())
                                {
                                    case "0":
                                        Para.bapteme_date = false;
                                        break;
                                    case "1":
                                        Para.bapteme_date = true;
                                        break;
                                }
                            }
                            if (Ligne == "[bapteme_ans]")
                                Int32.TryParse(sr.ReadLine(), out Para.bapteme_ans);

                            // union
                            if (Ligne == "[union_date]")
                            {
                                switch (sr.ReadLine())
                                {
                                    case "0": Para.union_date = false; break;
                                    case "1": Para.union_date = true; break;
                                }
                            }
                            if (Ligne == "[union_ans]")
                                Int32.TryParse(sr.ReadLine(), out Para.union_ans);

                            // Déces
                            if (Ligne == "[deces_date]")
                            {
                                switch (sr.ReadLine())
                                {
                                    case "0":
                                        Para.deces_date = false;
                                        break;
                                    case "1":
                                        Para.deces_date = true;
                                        break;
                                }
                            }
                            if (Ligne == "[deces_ans]") Int32.TryParse(sr.ReadLine(), out Para.deces_ans);

                            // inhumation
                            if (Ligne == "[inhumation_date]")
                            {
                                switch (sr.ReadLine())
                                {
                                    case "0": Para.inhumation_date = false; break;
                                    case "1": Para.inhumation_date = true; break;
                                }
                            }
                            if (Ligne == "[inhumation_ans]") Int32.TryParse(sr.ReadLine(), out Para.inhumation_ans);

                            // ordonnance
                            if (Ligne == "[ordonnance_date]")
                            {
                                switch (sr.ReadLine())
                                {
                                    case "0":
                                        Para.ordonnance_date = false;
                                        break;
                                    case "1":
                                        Para.ordonnance_date = true;
                                        break;
                                }
                            }
                            if (Ligne == "[ordonnance_ans]")
                                Int32.TryParse(sr.ReadLine(), out Para.ordonnance_ans);

                            // autre
                            if (Ligne == "[autre_date]")
                            {
                                switch (sr.ReadLine())
                                {
                                    case "0": Para.autre_date = false; break;
                                    case "1": Para.autre_date = true; break;
                                }
                            }
                            if (Ligne == "[autre_ans]") Int32.TryParse(sr.ReadLine(), out Para.autre_ans);

                            // citoyen
                            if (Ligne == "[citoyen_date]")
                            {
                                switch (sr.ReadLine())
                                {
                                    case "0": Para.citoyen_date = false; break;
                                    case "1": Para.citoyen_date = true; break;
                                }
                            }
                            if (Ligne == "[citoyen_ans]") Int32.TryParse(sr.ReadLine(), out Para.citoyen_ans);

                            // testament
                            if (Ligne == "[testament_information]")
                            {
                                switch (sr.ReadLine())
                                {
                                    case "0": Para.testament_information = false; break;
                                    case "1": Para.testament_information = true; break;
                                }
                            }
                            if (Ligne == "[testament_ans]") Int32.TryParse(sr.ReadLine(),
                                out Para.testament_ans);
                        }
                    }
                    Application.DoEvents();
                }
                catch (Exception msg)
                {
                    R.Afficher_message(
                        "Ne peut pas lire les paramètres.",
                        msg.Message,
                        erreur,
                        null,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                }
                if (Para.tout_evenement)
                {
                    // naissance
                    Priver.naissance_date = true;
                    Priver.naissance_ans = Para.tout_evenement_ans;
                    
                    // bapteme
                    Priver.bapteme_date = true;
                    Priver.bapteme_ans = Para.tout_evenement_ans;

                    // union
                    Priver.union_date = true;
                    Priver.union_ans = Para.tout_evenement_ans;

                    // deces
                    Priver.deces_date = true;
                    Priver.deces_ans = Para.tout_evenement_ans;
                    // inhumation
                    Priver.inhumation_date = true;
                    Priver.inhumation_ans = Para.tout_evenement_ans;

                    // autre
                    Priver.autre_date = true;
                    Priver.autre_ans = Para.tout_evenement_ans;

                    // citoyen
                    Priver.citoyen_date = true;
                    Priver.citoyen_ans = Para.tout_evenement_ans;
                    // testament
                    Priver.testament_information = true;
                    Priver.testament_ans = Para.tout_evenement_ans;
                }
                else
                {
                    // naissance
                    Priver.naissance_date = Para.naissance_date;
                    Priver.naissance_ans = Para.naissance_ans;

                    // bapteme
                    Priver.bapteme_date = Para.bapteme_date;
                    Priver.bapteme_ans = Para.bapteme_ans;

                    // union
                    Priver.union_date = Para.union_date;
                    Priver.union_ans = Para.union_ans;
                    // deces
                    Priver.deces_date = Para.deces_date;
                    Priver.deces_ans = Para.deces_ans;
                    
                    // inhumation
                    Priver.inhumation_date = Para.inhumation_date;
                    Priver.inhumation_ans = Para.inhumation_ans;

                    // ordonnance
                    Priver.ordonnance_date = Para.ordonnance_date;
                    Priver.ordonnance_ans = Para.ordonnance_ans;

                    // autre
                    Priver.autre_date = Para.autre_date;
                    Priver.autre_ans = Para.autre_ans;
                    // citoyen
                    Priver.citoyen_date = Para.citoyen_date;
                    Priver.citoyen_ans = Para.citoyen_ans;
                    // testament
                    Priver.testament_information = Para.testament_information;
                    Priver.testament_ans = Para.testament_ans;
                }
            }
            if (Para.dosssier_HTML != null)
            {
                fichier_deboguer = Para.dosssier_HTML + @"\GH_deboguer.html";
                fichier_erreur = Para.dosssier_HTML + @"\GH_erreur.html";
                fichier_balise = Para.dosssier_HTML + @"\GH_balise.html";
                fichier_GEDCOM = Para.dosssier_HTML + @"\GH_GEDCOM.html";
            }

            return (position_X, position_Y);
        }
        private bool PasConfidentiel(string naissance, string deces)
        {
            Regler_code_erreur();
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
            Regler_code_erreur();
            Application.DoEvents();
            //Pb_total.Visible = false;
            //Pb_fiche_individu.Visible = false;
            //Pb_voir_fiche_famille.Visible = false;
            Pb_annuler.Visible = true;
            if (!DossierHTMLValide())
            {
                return;
            }
            Form_desactiver(true);
            //Pb_total.Visible = false;
            //Pb_fiche_individu.Visible = false;
            //Pb_voir_fiche_famille.Visible = false;
            Array.ForEach(Directory.GetFiles(dossier_sortie + @"\medias"), delegate (string path) { File.Delete(path); });
            Array.ForEach(Directory.GetFiles(dossier_sortie + @"\individus"), delegate (string path) { File.Delete(path); });
            Array.ForEach(Directory.GetFiles(dossier_sortie + @"\individus\medias"), delegate (string path) { File.Delete(path); });
            Array.ForEach(Directory.GetFiles(dossier_sortie + @"\familles"), delegate (string path) { File.Delete(path); });
            Array.ForEach(Directory.GetFiles(dossier_sortie, "*.html"), delegate (string path) { File.Delete(path); });
            if (annuler) return;
            List<string> ListeID = new List<string>();
            ListeID.Clear();
            ListeID = GEDCOMClass.Avoir_liste_ID_individu();
            int compteur = 1;
            compteur = 0;
            HTML.Index(FichierGEDCOM, NombreIndividuTb.Text, NombreFamilleTb.Text);
            if (annuler) return;
            HTML.Index_individu(dossier_sortie);
            if (annuler) return;
            HTML.Index_famille_conjoint(dossier_sortie);
            if (annuler) return;
            HTML.Index_famille_conjointe(dossier_sortie);
            if (annuler) return;
            foreach (string ID in ListeID)
            {
                HTML.Fiche_individu(ID, dossier_sortie,true);
                compteur++;
                if (annuler) return;
            }
            ListeID = GEDCOMClass.Avoir_liste_ID_famille();
            compteur = 1;
            foreach (string ID in ListeID)
            {
                HTML.Fiche_famille(ID, dossier_sortie, true);
                compteur++;
                if (annuler) return;
            }
            try
            {
                //System.Diagnostics.Process.Start("file:///" + dossier_sortie + @"\index.html");

                Page page = new Page("file:///" + dossier_sortie + @"\index.html", FichierGEDCOM);
                Page frm = page;
                frm.Tag = this;
                frm.Show(this);
            }
            catch (Exception msg)
            {
                R.Afficher_message(
                    "Problème pour générer les fichier",
                    msg.Message,
                    erreur,
                    null,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return;
            }
            Form_desactiver(false);
            //FormVisible(true);
        }
        private void FichierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
        }

        private void LvChoixFamilleConjoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            Regler_code_erreur();
            if (LvChoixFamille.SelectedItems.Count == 0) return;
            ListViewItem item = LvChoixFamille.SelectedItems[0];
            IDCourantFamilleConjoint = item.SubItems[0].Text;
        }

        private void Form_desactiver(bool status)
        {
            Regler_code_erreur();
            if (status)
            {
                this.UseWaitCursor = true;
                LvChoixIndividu.Enabled = false;
                LvChoixFamille.Enabled = false;
                menuPrincipal.Enabled = false;
                
                // Recherche
                RechercheIndividuTB.Enabled = false;
                Pb_individu_avant.Enabled = false;
                Pb_individu_apres.Enabled = false;
                RechercheFamilleTB.Enabled = false;
                Pb_famille_avant.Enabled = false;
                Pb_famille_apres.Enabled = false;

                // rapport
                Pb_GEDCOM.Enabled = false;
                Pb_debug.Enabled = false;
                Pb_erreur.Enabled = false;
                Pb_balise.Enabled = false;

                // fiche
                Pb_fiche_individu.Enabled = false;
                Pb_fiche_famille.Enabled = false;
                Pb_total.Enabled = false;
            }
            else
            {
                this.UseWaitCursor = false;
                LvChoixIndividu.Enabled = true;
                LvChoixFamille.Enabled = true;
                menuPrincipal.Enabled = true;

                // Recherche
                RechercheIndividuTB.Enabled = true;
                Pb_individu_avant.Enabled = true;
                Pb_individu_apres.Enabled = true;
                RechercheFamilleTB.Enabled = true;
                Pb_famille_avant.Enabled = true;
                Pb_famille_apres.Enabled = true;

                // rapport
                Pb_GEDCOM.Enabled = true;
                Pb_debug.Enabled = true;
                Pb_erreur.Enabled = true;
                Pb_balise.Enabled = true;

                // fiche
                Pb_fiche_individu.Enabled = true;
                Pb_fiche_famille.Enabled = true;
                Pb_total.Enabled = true;
            }
        }
        private void FormVisible(bool status)
        {
            Regler_code_erreur();
            if (status)
            {
                
                Pb_total.Visible = true;
                Pb_fiche_individu.Visible = true;
                Pb_fiche_famille.Visible = true;
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
                Pb_annuler.Visible = false;
                Pb_recherche_individu.Visible = true;
                Pb_recherche_famille.Visible = true;
                Pb_individu_avant.Visible = true;
                Pb_individu_apres.Visible = true;
                Pb_famille_avant.Visible = true;
                Pb_famille_apres.Visible = true;
            }
            else
            {
                Pb_balise.Visible = false;
                Pb_total.Visible = false;
                Pb_fiche_individu.Visible = false;
                Pb_fiche_famille.Visible = false;
                Gb_info_GEDCOM.Visible = false;
                LvChoixIndividu.Visible = false;
                LvChoixFamille.Visible = false;
                lpIndividu.Visible = false;
                lbFamilleConjoint.Visible = false;
                RechercheFamilleB.Visible = false;
                RechercheFamilleTB.Visible = false;
                RechercheIndividuB.Visible = false;
                RechercheIndividuTB.Visible = false;
                Pb_recherche_individu.Visible = false;
                Pb_recherche_famille.Visible = false;
                Pb_recherche_individu.Visible = false;
                Pb_recherche_famille.Visible = false;
                Pb_individu_avant.Visible = false;
                Pb_individu_apres.Visible = false;
                Pb_famille_avant.Visible = false;
                Pb_famille_apres.Visible = false;
            }
        }

        private void LblCharSet_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
        }

        private void LvChoixIndividu_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Regler_code_erreur();
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
            Regler_code_erreur();
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
            Regler_code_erreur();
            AvoirDossierPageWeb();
        }
        private void ÀProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Form l = new A_propos
            {
                StartPosition = FormStartPosition.Manual,
                Left = this.Left + 145,
                Top = this.Top + 60
            };
            l.ShowDialog(this);
        }
        private void AideToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Form l = new Aide
            {
                StartPosition = FormStartPosition.Manual,
                Left = this.Left + 145,
                Top = this.Top + 60
            };
            l.Show();

        }
        private void LvChoixIndividu_DoubleClick(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Genere_page_individu(IDCourantIndividu);
        }
        private void OuvrirDossierDesFichesHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            if (!Directory.Exists(Para.dosssier_HTML))
            {
                R.Afficher_message(
                    "S.V.P. Spécifiez dans les paramêtres, le dossier des fiches HTML."
                    );
                return;
            }
            System.Diagnostics.Process.Start(@Para.dosssier_HTML);
        }
        private int Pixel_a_dpi(float pixel)
        {
            Regler_code_erreur();
            float point;
            Graphics g = CreateGraphics();
            point = pixel * g.DpiX / g.DpiX;
            g.Dispose();
            int dpi = Convert.ToInt32(point);
            return dpi;
        }
        private void RechercheIndividuTB_TextChanged(object sender, EventArgs e)
        {
            Regler_code_erreur();
            //Form_desactiver(true);
            if (RechercheIndividuTB.Text == "")
            {
                LvChoixIndividu.SelectedItems.Clear();
                //Form_desactiver(false);
                return;
            }
            string[] mots = RechercheIndividuTB.Text.ToLower().Split(' ');
            //Pb_individu_avant.Visible = true;
            //Pb_individu_apres.Visible = true;
            for (int f = 0; f < LvChoixIndividu.Items.Count; f++)
            {
                bool Ok = true;
                foreach (string m in mots)
                {
                    if (LvChoixIndividu.Items[f].SubItems[1].Text.ToLower().IndexOf(m) == -1) Ok = false;
                }
                if (Ok)
                {
                    LvChoixIndividu.Items[f].Selected = true;
                    LvChoixIndividu.EnsureVisible(f);
                    //Form_desactiver(false);
                    return;
                }
                else
                {
                    LvChoixIndividu.Items[f].Selected = false;
                }
            }
            //Form_desactiver(false);
        }
        private void RechercheConjointeTB_TextChanged(object sender, EventArgs e)
        {
            Regler_code_erreur();
            if (RechercheFamilleTB.Text == "")
            {
                LvChoixFamille.SelectedItems.Clear();
                return;
            }
            string[] mots = RechercheFamilleTB.Text.ToLower().Split(' ');
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

        public void Animation(bool afficher)
        {
            Regler_code_erreur();
            if (afficher)
            {
                Timer_animation.Start();
                Application.DoEvents();
            }
            else
            {
                Timer_animation.Stop();
                pb_del_1.Image = Properties.Resources.del_off;
                pb_del_2.Image = Properties.Resources.del_off;
                pb_del_3.Image = Properties.Resources.del_off;
                Application.DoEvents();
                return;
            }
        }
        private void ApresConjointeB_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
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

        private void AvantConjointeB_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            int index = 0;
            //Pb_famille_avant.Visible = false;
            //ApresFamilleB.Visible = false;
            //if (RechercheFamilleTB.Text == "") return;
            //Pb_famille_avant.Visible = true;
            //ApresFamilleB.Visible = true;
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
        /*private void Ecrire_paramettre()
        {
            Regler_code_erreur();
            try
            {
                string repertoire = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\GH";
                if (!Directory.Exists(repertoire))
                    Directory.CreateDirectory(repertoire);
                string Fichier = repertoire + @"\GH.ini";

                if (File.Exists(Fichier))
                {
                    File.Delete(Fichier);
                }
                using (StreamWriter ligne = File.CreateText(Fichier))
                {
                    if (this.WindowState == FormWindowState.Normal)
                    {
                        ligne.WriteLine("[location_X]");
                        ligne.WriteLine(this.Location.X);
                        ligne.WriteLine("[location_Y]");
                        ligne.WriteLine(this.Location.Y);
                    }
                    else
                    {
                        ligne.WriteLine("[location_X]");
                        ligne.WriteLine(this.RestoreBounds.Location.X);
                        ligne.WriteLine("[location_Y]");
                        ligne.WriteLine(this.RestoreBounds.Location.X);
                    }

                    ligne.WriteLine("[dossier_HTML]");
                    ligne.WriteLine(Para.dosssier_HTML);
                    ligne.WriteLine("[dossier_media]");
                    ligne.WriteLine(Para.dossier_media);
                    ligne.WriteLine("[dossier_GEDCOM]");
                    ligne.WriteLine(GH.Para.dossier_GEDCOM);
                    ligne.WriteLine("[arriere_plan]");
                    ligne.WriteLine(GH.Para.arriere_plan);
                    ligne.WriteLine("[voir_ID]");
                    if (Para.voir_ID) { ligne.WriteLine("1"); } else { ligne.WriteLine("0"); }
                    ligne.WriteLine("[voir_media]");
                    if (Para.voir_media) { ligne.WriteLine("1"); } else { ligne.WriteLine("0"); }
                    ligne.WriteLine("[date_longue]");
                    if (Para.date_longue) { ligne.WriteLine("1"); } else { ligne.WriteLine("0"); }
                    ligne.WriteLine("[voir_date_changement]");
                    if (Para.voir_date_changement) { ligne.WriteLine("1"); } else { ligne.WriteLine("0"); }
                    ligne.WriteLine("[voir_chercheur]");
                    if (Para.voir_chercheur) { ligne.WriteLine("1"); } else { ligne.WriteLine("0"); }
                    ligne.WriteLine("[voir_reference]");
                    if (Para.voir_reference) { ligne.WriteLine("1"); } else { ligne.WriteLine("0"); }
                    ligne.WriteLine("[voir_note]");
                    if (Para.voir_note) { ligne.WriteLine("1"); } else { ligne.WriteLine("0"); }
                    ligne.WriteLine("[voir_carte]");
                    if (Para.voir_carte) { ligne.WriteLine("1"); } else { ligne.WriteLine("0"); }
                    ligne.WriteLine("[voir_info_bulle]");
                    if (Para.voir_info_bulle) { ligne.WriteLine("1"); } else { ligne.WriteLine("0"); }

                    ligne.WriteLine("[mode_depanage]");
                    if (Para.mode_depanage) { ligne.WriteLine("1"); } else { ligne.WriteLine("0"); }

                    //*** Confidentiel] ***

                    ligne.WriteLine("[tout_evenement]");
                    if (Para.tout_evenement) { ligne.WriteLine("1"); } else { ligne.WriteLine("0"); }
                    ligne.WriteLine("[tout_evenement_date]");
                    if (Para.tout_evenement_date) { ligne.WriteLine("1"); } else { ligne.WriteLine("0"); }
                    ligne.WriteLine("[tout_evenement_ans]");
                    ligne.WriteLine(Para.tout_evenement_ans);

                    // naissance
                    ligne.WriteLine("[naissance_date]");
                    if (Para.naissance_date) { ligne.WriteLine("1"); } else { ligne.WriteLine("0"); }
                    ligne.WriteLine("[naissance_ans]");
                    ligne.WriteLine(Para.naissance_ans);

                    // bapteme
                    ligne.WriteLine("[bapteme_date]");
                    if (Para.bapteme_date)
                    {
                        ligne.WriteLine("1");
                    }
                    else
                    {
                        ligne.WriteLine("0");
                    }
                    ligne.WriteLine("[bapteme_ans]");
                    ligne.WriteLine(Para.bapteme_ans);

                    // union
                    ligne.WriteLine("[union_date]");
                    if (Para.union_date) { ligne.WriteLine("1"); } else { ligne.WriteLine("0"); }
                    ligne.WriteLine("[union_ans]");
                    ligne.WriteLine(Para.union_ans);

                    // déces
                    ligne.WriteLine("[deces_date]");
                    if (Para.deces_date) { ligne.WriteLine("1"); } else { ligne.WriteLine("0"); }
                    ligne.WriteLine("[deces_ans]");
                    ligne.WriteLine(Para.deces_ans);

                    // inhumation
                    ligne.WriteLine("[inhumation_date]");
                    if (Para.inhumation_date) { ligne.WriteLine("1"); } else { ligne.WriteLine("0"); }
                    ligne.WriteLine("[inhumation_ans]");
                    ligne.WriteLine(Para.inhumation_ans);

                    // ordonnance
                    ligne.WriteLine("[ordonnance_date]");
                    if (Para.ordonnance_date)
                    {
                        ligne.WriteLine("1");
                    }
                    else
                    {
                        ligne.WriteLine("0");
                    }
                    ligne.WriteLine("[ordonnance_ans]");
                    ligne.WriteLine(Para.ordonnance_ans);

                    // autre
                    ligne.WriteLine("[autre_date]");
                    if (Para.autre_date) { ligne.WriteLine("1"); } else { ligne.WriteLine("0"); }
                    ligne.WriteLine("[autre_ans]");
                    ligne.WriteLine(Para.autre_ans);

                    // citoyen
                    ligne.WriteLine("[citoyen_date]");
                    if (Para.citoyen_date) { ligne.WriteLine("1"); } else { ligne.WriteLine("0"); }
                    ligne.WriteLine("[citoyen_ans]");
                    ligne.WriteLine(Para.citoyen_ans);

                    // testement

                    ligne.WriteLine("[testament]");
                    if (Para.testament_information) { ligne.WriteLine("1"); } else { ligne.WriteLine("0"); }
                    ligne.WriteLine("[testament_information]");
                    if (Para.testament_information) { ligne.WriteLine("1"); } else { ligne.WriteLine("0"); }
                    ligne.WriteLine("[testament_ans]");
                    ligne.WriteLine(Para.testament_ans);
                }
            }
            catch (Exception msg)
            {
                R.Afficher_message("Ne peut pas écrire les paramètres",
                    msg.Message,
                    erreur,
                    null,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
        }
        */
        private void LvChoixFamille_DoubleClick(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Genere_page_famille(IDCourantFamilleConjoint);
        }
        private void BaliseBt_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            if (File.Exists(fichier_balise))
                Process.Start(fichier_balise);
        }
        private void DeboguerTb_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            if (File.Exists(fichier_deboguer))
                Process.Start(fichier_deboguer);
        }
        private void VoirDateDeChangementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
        }
        private void VoirReferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
        }
        private void VoirNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
        }
        private void VoirReferenceToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            Regler_code_erreur();
            if (R.IsNotNullOrEmpty(FichierGEDCOMaLire))
            {

                MessageBox.Show("Les fiches HTML doivent être re-générer pour voir le changement.", "Voir reférences a été changer",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Warning);
            }
        }

        private void Voir_paramettre()
        {
            Regler_code_erreur();
            Form l = new Parametre
            {
                StartPosition = FormStartPosition.Manual,
                Left = this.Left + 70,
                Top = this.Top + 60
            };
            l.ShowDialog(this);
            if (Para.voir_info_bulle)
                Info_bulle.Active = true;
            else
                Info_bulle.Active = false;
            Application.DoEvents();
        }
        private void VoirDateDeChangementToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            Regler_code_erreur();
            if (R.IsNotNullOrEmpty(FichierGEDCOMaLire))
            {

                MessageBox.Show("Les fiches HTML doivent être re-générer pour voir le changement.", "Voir date de changement a été changer",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Warning);
            }
        }

        private void VoirNoteToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            Regler_code_erreur();
            if (R.IsNotNullOrEmpty(FichierGEDCOMaLire))
            {

                MessageBox.Show("Les fiches HTML doivent être re-générer pour voir le changement.", "Voir Note a été changer",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Warning);
            }
        }

        private void ParamètresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Voir_paramettre();
        }

        private void BienvenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Form l = new Bienvenu
            {
                StartPosition = FormStartPosition.Manual,
                Left = this.Left + 145,
                Top = this.Top + 100
            };
            l.ShowDialog(this);
        }

        private void Timer_animation_Tick(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Random rnd = new Random();
            if (rnd.Next(1, 5) == 1) pb_del_1.Image = Properties.Resources.del_bleu;
            if (rnd.Next(1, 5) == 2) pb_del_1.Image = Properties.Resources.del_ambe;
            if (rnd.Next(1, 5) == 3) pb_del_1.Image = Properties.Resources.del_rouge;
            if (rnd.Next(1, 5) == 4) pb_del_1.Image = Properties.Resources.del_vert;
            if (rnd.Next(1, 5) == 1) pb_del_2.Image = Properties.Resources.del_bleu;
            if (rnd.Next(1, 5) == 2) pb_del_2.Image = Properties.Resources.del_ambe;
            if (rnd.Next(1, 5) == 3) pb_del_2.Image = Properties.Resources.del_rouge;
            if (rnd.Next(1, 5) == 4) pb_del_2.Image = Properties.Resources.del_vert;
            if (rnd.Next(1, 5) == 1) pb_del_3.Image = Properties.Resources.del_bleu;
            if (rnd.Next(1, 5) == 2) pb_del_3.Image = Properties.Resources.del_ambe;
            if (rnd.Next(1, 5) == 3) pb_del_3.Image = Properties.Resources.del_rouge;
            if (rnd.Next(1, 5) == 4) pb_del_3.Image = Properties.Resources.del_vert;
            Application.DoEvents();
        }

        private void Pb_GEDCOM_MouseHover(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_GEDCOM.Image = Properties.Resources.G_bleu;
        }

        private void Pb_GEDCOM_MouseLeave(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_GEDCOM.Image = Properties.Resources.G_marine;
        }

        private void Pb_GEDCOM_MouseDown(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_GEDCOM.Image = Properties.Resources.G_gris;
        }

        private void Pb_GEDCOM_MouseUp(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_GEDCOM.Image = Properties.Resources.G_bleu;
        }

        private void Pb_GEDCOM_MouseClick(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            try
            {
                // si fichier_gedcom existe efface fichiers
                if (File.Exists(fichier_GEDCOM))
                {
                    File.Delete(fichier_GEDCOM);
                }
                // info_GEDCOM
                GEDCOMClass.HEADER info_HEADER = GEDCOMClass.Avoir_info_GEDCOM();
                using (StreamWriter ligne = File.AppendText(fichier_GEDCOM))
                {
                    ligne.WriteLine("<!DOCTYPE html>");
                    ligne.WriteLine("<html lang=\"fr\" style=\"background-color:#FFF;\">");
                    ligne.WriteLine("\t<head>");
                    ligne.WriteLine("\t\t<meta charset = 'UTF-8' />");
                    ligne.WriteLine("\t\t\t<title>GEDCOM</title>");
                    ligne.WriteLine(
                                "<style>" +
                                "    h1{color:#00F}\n" +
                                "    .col0{width:150px;vertical-align:top;} " +
                                "    .col1{width:90px;vertical-align:top;} " +
                                "    .col2{width:50px;vertical-align:top;} " +
                                "    .col3{width:330px;vertical-align:top;} " +
                                "    .col4{vertical-align:top;} " +
                                "</style>");
                    ligne.WriteLine("\t</head>");
                    ligne.WriteLine("\t<body>");
                    ligne.WriteLine("<h1>GEDCOM</h1>");
                    ligne.WriteLine("<table style=\"border:2px solid #000;width:100%\">");
                    ligne.WriteLine("\t<tr><td style=\"width:200px\">Nom</td><td>" + info_HEADER.N2_SOUR_NAME + "</td><td></tr>");
                    ligne.WriteLine("\t<tr><td>Version</td><td>" + info_HEADER.N2_SOUR_VERS + "<td></tr>");
                    ligne.WriteLine("\t<tr><td>Date</td><td>" + info_HEADER.N1_DATE + " " + info_HEADER.N2_DATE_TIME + "<td></tr>");
                    ligne.WriteLine("\t<tr><td>Copyright</td><td>" + info_HEADER.N1_COPR + "<td></tr>");
                    ligne.WriteLine("\t<tr><td>Version</td><td>" + info_HEADER.N2_GEDC_VERS + "<td></tr>");
                    ligne.WriteLine("\t<tr><td>Code charactère</td><td>" + info_HEADER.N1_CHAR + "<td></tr>");
                    ligne.WriteLine("\t<tr><td>Langue</td><td>" + info_HEADER.N1_LANG + "<td></tr>");
                    ligne.WriteLine("\t<tr><td>Fichier sur le disque</td><td>" + info_HEADER.Nom_fichier_disque + "<td></tr>");
                    ligne.WriteLine("\t<tr><td>Page générée le</td><td>" + DateTime.Now + "<td></tr>");
                    System.Version version;
                    version = Assembly.GetExecutingAssembly().GetName().Version;
                    ligne.WriteLine("\t<tr><td>Version de GH</td><td>" + version.Major + "." + version.Minor + "." + version.Build + "<td></tr>");
                    ligne.WriteLine("</table>");
                    string text = "";
                    int position = 1;
                    ligne.WriteLine("<br /><br />");
                    System.IO.StreamReader fichier_lecture = new System.IO.StreamReader(FichierGEDCOMaLire);
                    int niveau;
                    do
                    {
                        text = fichier_lecture.ReadLine().TrimStart();
                        char[] espace = { ' ' };
                        string[] section = text.Split(espace);
                        if (text != "")
                        {
                            niveau = Int32.Parse(section[0]);
                        }
                        else
                            niveau = 0;
                        string tab = "";
                        for (int f = 0; f < niveau; f++)
                            tab += "&nbsp;&nbsp;&nbsp;&nbsp;";
                        ligne.WriteLine(position.ToString("D8") + "&nbsp;&nbsp;" + tab + text + "<br />");
                        position++;
                    } while (!fichier_lecture.EndOfStream);
                    fichier_lecture.Close();
                    ligne.WriteLine("\t</body>");
                    ligne.WriteLine("</html>");
                }
                if (File.Exists(fichier_GEDCOM))
                {
                    Page page = new Page("file:///" + fichier_GEDCOM, "GEDCOM");
                    Page frm = page;
                    frm.Tag = this;
                    frm.Show(this);
                }
                
            }
            catch (Exception msg)
            {
                R.Afficher_message(
                    "Ne peut pas afficher le fichier GEDCOM" + fichier_GEDCOM + ".",
                    msg.Message,
                    GH.erreur,
                    null,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
        }

        private void Pb_debug_MouseHover(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_debug.Image = Properties.Resources.D_bleu;
        }

        private void Pb_debug_MouseLeave(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_debug.Image = Properties.Resources.D_marine;
        }

        private void Pb_debug_MouseDown(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_debug.Image = Properties.Resources.D_gris;
        }

        private void Pb_debug_MouseUp(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_debug.Image = Properties.Resources.D_bleu;
        }

        private void Pb_debug_MouseClick(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            if (File.Exists(fichier_deboguer))
            {
                System.Diagnostics.Process.Start("file:///" + fichier_deboguer);
                Page page = new Page("file:///" + fichier_deboguer , "Déboguer");
                Page frm = page;
                frm.Tag = this;
                frm.Show(this);
            }
        }


        private void Pb_erreur_MouseHover(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_erreur.Image = Properties.Resources.E_bleu;
        }

        private void Pb_erreur_MouseLeave(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_erreur.Image = Properties.Resources.E_marine;
        }

        private void Pb_erreur_MouseDown(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_erreur.Image = Properties.Resources.E_gris;
        }

        private void Pb_erreur_MouseUp(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_erreur.Image = Properties.Resources.E_bleu;
        }

        private void Pb_erreur_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            if (File.Exists(fichier_erreur))
                Process.Start(fichier_erreur);
        }

        private void Pb_balise_MouseHover(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_balise.Image = Properties.Resources.B_bleu;
        }

        private void Pb_balise_MouseLeave(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_balise.Image = Properties.Resources.B_marine;
        }

        private void Pb_balise_MouseDown(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_balise.Image = Properties.Resources.B_gris;
        }

        private void Pb_balise_MouseUp(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_balise.Image = Properties.Resources.B_bleu;
        }

        private void Pb_balise_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            if (File.Exists(fichier_balise))
                Process.Start(fichier_balise);
        }

        private void Pb_fiche_individu_MouseHover(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_fiche_individu.Image = Properties.Resources.individu_bleu;
        }

        private void Pb_fiche_individu_MouseLeave(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_fiche_individu.Image = Properties.Resources.individu_marine;
        }

        private void Pb_fiche_individu_MouseDown(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_fiche_individu.Image = Properties.Resources.individu_gris;
        }

        private void Pb_fiche_individu_MouseUp(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_fiche_individu.Image = Properties.Resources.individu_bleu;
        }

        private void Pb_fiche_individu_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Genere_page_individu(IDCourantIndividu);
        }

        private void Pb_fiche_famille_MouseHover(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_fiche_famille.Image = Properties.Resources.famille_bleu;
        }

        private void Pb_fiche_famille_MouseLeave(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_fiche_famille.Image = Properties.Resources.famille_marine;
        }

        private void Pb_fiche_famille_MouseDown(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_fiche_famille.Image = Properties.Resources.famille_gris;
        }

        private void Pb_fiche_famille_MouseUp(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_fiche_famille.Image = Properties.Resources.famille_bleu;
        }

        private void Pb_fiche_famille_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Genere_page_famille(IDCourantFamilleConjoint);
        }

        private void Pb_total_MouseHover(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_total.Image = Properties.Resources.total_bleu;
        }

        private void Pb_total_MouseLeave(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_total.Image = Properties.Resources.total_marine;
        }

        private void Pb_total_MouseDown(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_total.Image = Properties.Resources.total_gris;
        }

        private void Pb_total_MouseUp(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_total.Image = Properties.Resources.total_bleu;
        }

        private void Pb_total_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_annuler.Visible = true;
            Animation(true);
            Form_desactiver(true);
            Fichier_total();
            annuler = false;
            Pb_annuler.Visible = false;
            Form_desactiver(false);
            Animation(false);
        }

        private void Pb_log_del_MouseHover(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_log_del.Image = Properties.Resources.log_bleu;
        }

        private void Pb_log_del_MouseLeave(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_log_del.Image = Properties.Resources.log_marine;
        }

        private void Pb_log_del_MouseDown(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_log_del.Image = Properties.Resources.log_gris;
        }

        private void Pb_log_del_MouseUp(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_log_del.Image = Properties.Resources.log_bleu;
        }

        private void Pb_log_del_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Effacer_Log();
        }

        private void Pb_annuler_MouseHover(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_annuler.Image = Properties.Resources.annuler_bleu;
        }

        private void Pb_annuler_MouseLeave(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_annuler.Image = Properties.Resources.annuler_marine;
        }

        private void Pb_annuler_MouseDown(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_annuler.Image = Properties.Resources.annuler_gris;
        }

        private void Pb_annuler_MouseUp(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_annuler.Image = Properties.Resources.annuler_bleu;
        }

        private void Pb_annuler_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            annuler = true;
        }

        private void Pb_individu_avant_MouseHover(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_individu_avant.Image = Properties.Resources.gauche_bleu;
        }

        private void Pb_individu_avant_MouseLeave(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_individu_avant.Image = Properties.Resources.gauche_marine;
        }

        private void Pb_individu_avant_MouseDown(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_individu_avant.Image = Properties.Resources.gauche_gris;
        }

        private void Pb_individu_avant_MouseUp(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_individu_avant.Image = Properties.Resources.gauche_bleu;
        }

        private void Pb_individu_avant_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Form_desactiver(true);
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
                    if (LvChoixIndividu.Items[f].SubItems[1].Text.ToLower().IndexOf(fe) < 0)
                        trouver = false;
                }
                if (trouver)
                {
                    LvChoixIndividu.Items[f].Selected = true;
                    LvChoixIndividu.EnsureVisible(f);
                    Form_desactiver(false);
                    return;
                }
            }
            for (int f = LvChoixIndividu.Items.Count - 1; f > index; f--)
            {
                bool trouver = true;
                foreach (string fe in s)
                {
                    if (LvChoixIndividu.Items[f].SubItems[1].Text.ToLower().IndexOf(fe) < 0)
                        trouver = false;
                }
                if (trouver)
                {
                    LvChoixIndividu.Items[f].Selected = true;
                    LvChoixIndividu.EnsureVisible(f);
                    Form_desactiver(false);
                    return;
                }
            }
            Form_desactiver(false);
        }

        private void Pb_individu_apres_MouseHover(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_individu_apres.Image = Properties.Resources.droite_bleu;
        }

        private void Pb_individu_apres_MouseLeave(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_individu_apres.Image = Properties.Resources.droite_marine;
        }

        private void Pb_individu_apres_MouseDown(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_individu_apres.Image = Properties.Resources.droite_gris;
        }

        private void Pb_individu_apres_MouseUp(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_individu_apres.Image = Properties.Resources.droite_bleu;
        }

        private void Pb_individu_apres_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Form_desactiver(true);
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
                    if (LvChoixIndividu.Items[f].SubItems[1].Text.ToLower().IndexOf(fe) < 0)
                        trouver = false;
                }
                if (trouver)
                {
                    LvChoixIndividu.Items[f].Selected = true;
                    LvChoixIndividu.EnsureVisible(f);
                    Form_desactiver(false);
                    return;
                }
            }
            for (int f = 0; f < index; f++)
            {
                bool trouver = true;
                foreach (string fe in s)
                {
                    if (LvChoixIndividu.Items[f].SubItems[1].Text.ToLower().IndexOf(fe) < 0)
                        trouver = false;
                }
                if (trouver)
                {
                    LvChoixIndividu.Items[f].Selected = true;
                    LvChoixIndividu.EnsureVisible(f);
                    Form_desactiver(false);
                    return;
                }
            }
            Form_desactiver(false);
        }

        private void Pb_famille_avant_MouseHover(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_famille_avant.Image = Properties.Resources.gauche_bleu;
        }

        private void Pb_famille_avant_MouseLeave(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_famille_avant.Image = Properties.Resources.gauche_marine;
        }

        private void Pb_famille_avant_MouseDown(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_famille_avant.Image = Properties.Resources.gauche_gris;
        }

        private void Pb_famille_avant_MouseUp(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_famille_avant.Image = Properties.Resources.gauche_bleu;
        }

        private void Pb_famille_avant_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            int index = 0;
            //Pb_famille_avant.Visible = false;
            //ApresFamilleB.Visible = false;
            //if (RechercheFamilleTB.Text == "") return;
            //Pb_famille_avant.Visible = true;
            //ApresFamilleB.Visible = true;
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

        private void Pb_famille_apres_MouseHover(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_famille_apres.Image = Properties.Resources.droite_bleu;
        }

        private void Pb_famille_apres_MouseLeave(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Pb_famille_apres.Image = Properties.Resources.droite_marine;
        }

        private void Pb_famille_apres_MouseDown(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_famille_apres.Image = Properties.Resources.droite_gris;
        }

        private void Pb_famille_apres_MouseUp(object sender, MouseEventArgs e)
        {
            Regler_code_erreur();
            Pb_famille_apres.Image = Properties.Resources.droite_bleu;
        }

        private void Pb_famille_apres_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
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

        private void Btn_G_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            try
            {
                // si fichier_gedcom existe efface fichiers
                if (File.Exists(fichier_GEDCOM))
                {
                    File.Delete(fichier_GEDCOM);
                }
                // info_GEDCOM
                GEDCOMClass.HEADER info_HEADER = GEDCOMClass.Avoir_info_GEDCOM();
                using (StreamWriter ligne = File.AppendText(fichier_GEDCOM))
                {
                    ligne.WriteLine("<!DOCTYPE html>");
                    ligne.WriteLine("<html lang=\"fr\" style=\"background-color:#FFF;\">");
                    ligne.WriteLine("\t<head>");
                    ligne.WriteLine("\t\t<meta charset = 'UTF-8' />");
                    ligne.WriteLine("\t\t\t<title>GEDCOM</title>");
                    ligne.WriteLine(
                                "<style>" +
                                "    h1{color:#00F}\n" +
                                "    .col0{width:150px;vertical-align:top;} " +
                                "    .col1{width:90px;vertical-align:top;} " +
                                "    .col2{width:50px;vertical-align:top;} " +
                                "    .col3{width:330px;vertical-align:top;} " +
                                "    .col4{vertical-align:top;} " +
                                "</style>");
                    ligne.WriteLine("\t</head>");
                    ligne.WriteLine("\t<body>");
                    ligne.WriteLine("<h1>GEDCOM</h1>");
                    ligne.WriteLine("<table style=\"border:2px solid #000;width:100%\">");
                    ligne.WriteLine("\t<tr><td style=\"width:200px\">Nom</td><td>" + info_HEADER.N2_SOUR_NAME + "</td><td></tr>");
                    ligne.WriteLine("\t<tr><td>Version</td><td>" + info_HEADER.N2_SOUR_VERS + "<td></tr>");
                    ligne.WriteLine("\t<tr><td>Date</td><td>" + info_HEADER.N1_DATE + " " + info_HEADER.N2_DATE_TIME + "<td></tr>");
                    ligne.WriteLine("\t<tr><td>Copyright</td><td>" + info_HEADER.N1_COPR + "<td></tr>");
                    ligne.WriteLine("\t<tr><td>Version</td><td>" + info_HEADER.N2_GEDC_VERS + "<td></tr>");
                    ligne.WriteLine("\t<tr><td>Code charactère</td><td>" + info_HEADER.N1_CHAR + "<td></tr>");
                    ligne.WriteLine("\t<tr><td>Langue</td><td>" + info_HEADER.N1_LANG + "<td></tr>");
                    ligne.WriteLine("\t<tr><td>Fichier sur le disque</td><td>" + info_HEADER.Nom_fichier_disque + "<td></tr>");
                    ligne.WriteLine("\t<tr><td>Page générée le</td><td>" + DateTime.Now + "<td></tr>");
                    System.Version version;
                    version = Assembly.GetExecutingAssembly().GetName().Version;
                    ligne.WriteLine("\t<tr><td>Version de GH</td><td>" + version.Major + "." + version.Minor + "." + version.Build + "<td></tr>");
                    ligne.WriteLine("</table>");
                    string text = "";
                    int position = 1;
                    ligne.WriteLine("<br /><br />");
                    System.IO.StreamReader fichier_lecture = new System.IO.StreamReader(FichierGEDCOMaLire);
                    int niveau;
                    do
                    {
                        text = fichier_lecture.ReadLine().TrimStart();
                        char[] espace = { ' ' };
                        string[] section = text.Split(espace);
                        if (text != "")
                        {
                            niveau = Int32.Parse(section[0]);
                        }
                        else
                            niveau = 0;
                        string tab = "";
                        for (int f = 0; f < niveau; f++)
                            tab += "&nbsp;&nbsp;&nbsp;&nbsp;";
                        ligne.WriteLine(position.ToString("D8") + "&nbsp;&nbsp;" + tab + text + "<br />");
                        position++;
                    } while (!fichier_lecture.EndOfStream);
                    fichier_lecture.Close();
                    ligne.WriteLine("\t</body>");
                    ligne.WriteLine("</html>");
                }
                if (File.Exists(fichier_GEDCOM))
                {
                    Page page = new Page("file:///" + fichier_GEDCOM, "GEDCOM");
                    Page frm = page;
                    frm.Tag = this;
                    frm.Show(this);
                }

            }
            catch (Exception msg)
            {
                R.Afficher_message(
                    "Ne peut pas afficher le fichier GEDCOM" + fichier_GEDCOM + ".",
                    msg.Message,
                    GH.erreur,
                    null,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
        }

        private void Pb_GEDCOM_Click(object sender, EventArgs e)
        {

        }
    }
    internal class ListViewItemComparer : IComparer
    {
        public readonly int col;
        private ListViewItemComparer()
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
