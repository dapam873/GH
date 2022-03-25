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
using Routine;
using Squirrel; //nnn
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks; //nnn
using System.Windows.Forms;

namespace GH
{
    public partial class GHClass : Form
    {

        public static string[] fichier_deboguer =
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)) + @"\GH_deboguer.html",
                ""
            };

        public static string[] fichier_erreur =
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)) + @"\GH_erreur.html",
                ""
            };
        public static string fichier_balise = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)) + @"\GH_balise.html";
        
        public static string fichier_GEDCOM = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)) + @"\GH_GEDCOM.html";

        public static string dossier_sortie;
        public static bool annuler;
        public static string erreur = "GH0000";
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
        public static string FichierGEDCOMaLire = "";
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
        public class Para
        {
            public static string arriere_plan;
            public static int location_X;
            public static int location_Y;
            public static string dosssier_page;
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
            public static bool enregistrer_balise;
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

        public GHClass()
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
                R.Animation(true);
                Btn_annuler.Visible = true;
                string nom = HTML.Fiche_individu(ID, dossier_sortie, false);
                Page page = new Page("file:///" + dossier_sortie + @"\individus\page.html", nom);
                Page frm = page;
                frm.Tag = this;
                frm.Show(this);
                Btn_annuler.Visible = false;
                R.Animation(false);
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
                R.Animation(true);
                Btn_annuler.Visible = true;
                string nom = HTML.Fiche_famille(ID, dossier_sortie, false);
                Page page = new Page("file:///" + dossier_sortie + @"\familles\page.html", nom);
                Page frm = page;
                frm.Tag = this;
                frm.Show(this);
                Btn_annuler.Visible = false;
                R.Animation(false);
            }
        }
        private void tout_afficher()
        {
            Btn_total.Visible = true;
            Btn_fiche_individu.Visible = true;
            Btn_fiche_famille.Visible = true;
            Gb_info_GEDCOM.Visible = true;
            Cadre.Visible = true;
            LvChoixIndividu.Visible = true;
            LvChoixFamille.Visible = true;
            lpIndividu.Visible = true;
            lbFamilleConjoint.Visible = true;
            RechercheFamilleB.Visible = true;
            RechercheFamilleTB.Visible = true;
            RechercheIndividuB.Visible = true;
            RechercheIndividuTB.Visible = true;
            menuPrincipal.Visible = true;
            Btn_annuler.Visible = false;
            Pb_recherche_individu.Visible = true;
            Pb_recherche_famille.Visible = true;
            Btn_individu_avant.Visible = true;
            Btn_individu_apres.Visible = true;
            Btn_famille_avant.Visible = true;
            Btn_famille_apres.Visible = true;
            Btn_GEDCOM.Visible = true;
            Btn_debug.Visible = true;
            Btn_erreur.Visible = true;
            Btn_balise.Visible = true;
            Pb_attendre.Visible = true;
            Lb_nombre_ligne.Visible = true;
            Btn_annuler.Visible=true;
            Pb_attendre.Visible=true;
            Application.DoEvents();
        }

        public bool Valider_dossier_page()
        {
            Regler_code_erreur();
            while (!Directory.Exists(Para.dosssier_page))
            {
                DialogResult reponse = R.Afficher_message(
                    "S.V.P.Spécifiez dans les paramêtres, le dossier des page.",
                    null,
                    erreur,
                    null,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information
                    );
                if (reponse == DialogResult.OK)
                {
                    if (this.WindowState == FormWindowState.Normal)
                    {
                        Para.location_X = this.Location.X;
                        Para.location_Y = this.Location.Y;
                    }
                    else
                    {
                        Para.location_X = this.RestoreBounds.Location.X;
                        Para.location_Y = this.RestoreBounds.Location.Y;
                    }
                    Voir_paramettre();
                    Valider_dossier_page();
                }
                else
                    return false;
            }
            return true;
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
        private void OuvrirToolStripMenuItem_Click(
            object sender,
            EventArgs e
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name );
            // règer et démarer chronomètre
            Stopwatch chrono;
            chrono = Stopwatch.StartNew();
            string temps = null;
            TimeSpan durer;

            annuler = false;
            Regler_code_erreur();
            if (Valider_dossier_page())
            {
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
                    else
                    {
                        chrono.Stop();
                        return;
                    }
                }
                Effacer_Log();
                dossier_sortie = Para.dosssier_page + @"\" + Path.GetFileNameWithoutExtension(FichierGEDCOMaLire);
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
                        chrono.Stop();
                        return;
                    }
                }
                R.Animation(true);
                Btn_annuler.Visible = true;
                Btn_balise.Visible = false;
                menuPrincipal.Visible = false;
                FormVisible(false);
                Application.DoEvents();
                if (!Lire_fichier_GEDCOM(FichierGEDCOMaLire))
                {
                    Application.DoEvents();
                    Btn_annuler.Visible = false;
                    Gb_info_GEDCOM.Visible = false;
                    Cadre.Visible = false;
                    this.Cursor = Cursors.Default;
                    annuler = false;
                    menuPrincipal.Visible = true;
                    R.Animation(false);
                    chrono.Stop();
                    return;
                }
                Application.DoEvents();
                Para.dossier_GEDCOM = Path.GetDirectoryName(FichierGEDCOMaLire);
                if (Para.dosssier_page != null)
                    CreerDossier();
                Application.DoEvents();
                FichierGEDCOM = Path.GetFileName(FichierGEDCOMaLire);
                this.Text = " " + FichierGEDCOM;
                menuPrincipal.Visible = true;
                FormVisible(true);
                Btn_GEDCOM.Visible = true;
                Btn_annuler.Visible = false;
                annuler = false;
                Application.DoEvents();
                R.Animation(false);

                // avoir temps d.exécution
                chrono.Stop();
                durer = chrono.Elapsed;
                if (durer != null)
                    temps += durer.Hours.ToString() + "h ";
                temps += String.Format("{0:00}m {1:00}s", durer.Minutes, durer.Seconds);
                Tb_temps_execution.Text= temps;
                
                //tout_afficher();
            }
        }
        private string AssemblerPatronymePrenom(
            string patronyme,
            string prenom
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + " patronymme="+ patronyme + " prenom=" + prenom);
            if (prenom == null)
                prenom = "?";
            else if (prenom == "")
                prenom = "?";
            if (patronyme == null)
                patronyme = "?";
            else if (patronyme == "")
                patronyme = "?";
            if (prenom == "?" && patronyme == "?")
            {
                //R..Z("<b>Retourne null");
                return null;
            }
            //R..Z("<b>Retourne " + patronyme + ", " + prenom);
            return patronyme + ", " + prenom;
        }
        private static void Regler_code_erreur([CallerLineNumber] int sourceLineNumber = 0)
        {
            erreur = "GH" + sourceLineNumber;
        }

        private void ChoixLVIndividu_SelectedIndexChanged(object sender, EventArgs e)
        {
            Regler_code_erreur();
            if (LvChoixIndividu.SelectedItems.Count == 0)
                return;
            ListViewItem item = LvChoixIndividu.SelectedItems[0];
            IDCourantIndividu = item.SubItems[0].Text;
        }
        private void CreerDossier()
        {
            Regler_code_erreur();
            if (!DossierHTMLValide())
            {
                return;
            }
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
                        string nomFichier = Path.GetFileName(f);
                        ;
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
                        string nomFichier = Path.GetFileName(f);
                        ;
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
            R.Animation(true);

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
            Para.enregistrer_balise = true;

            Btn_log_del.Visible = true;

            // lire fichier
            FichierGEDCOMaLire = @"D:\Data\Documents\Mega\GH\GEDCOM\GEDCOM_5.5.ged";
            dossier_sortie = Para.dosssier_page + @"\" + Path.GetFileNameWithoutExtension(FichierGEDCOMaLire);

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
            if (File.Exists(fichier_deboguer[1]))
                System.Diagnostics.Process.Start("file:///" + fichier_deboguer);

            R.Animation(false);
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
            if (!Directory.Exists(Para.dosssier_page))
            {
                DialogResult reponse = R.Afficher_message(
                    "S.V.P. Spécifiez dans les paramêtres, Dossier page.",
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
        private bool EffacerLeDossier1(string dossier)
        {
            Regler_code_erreur();
            if (!Directory.Exists(dossier))
                return true;
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
            bool status = EffacerLeDossier1(Para.dosssier_page + @"\" + dossier1 + @"\medias");
            if (status)
                status = EffacerLeDossier1(Para.dosssier_page + @"\" + dossier1 + @"\commun");
            if (status)
                status = EffacerLeDossier1(Para.dosssier_page + @"\" + dossier1 + @"\individus\medias");
            if (status)
                status = EffacerLeDossier1(Para.dosssier_page + @"\" + dossier1 + @"\individus");
            if (status)
                status = EffacerLeDossier1(Para.dosssier_page + @"\" + dossier1 + @"\familles\medias");
            if (status)
                status = EffacerLeDossier1(Para.dosssier_page + @"\" + dossier1 + @"\familles");
            return status;
        }
        private void Effacer_Log()
        {
            Regler_code_erreur();
            try
            {
                string dossier_log = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

                // si fichier_debug existe efface fichiers
                if (File.Exists(fichier_deboguer[0]))
                {
                    File.Delete(fichier_deboguer[0]);
                }
                if (File.Exists(fichier_deboguer[1]))
                {
                    File.Delete(fichier_deboguer[1]);
                }

                // si fichier_erreur existe efface fichiers
                if (File.Exists(fichier_erreur[0]))
                {
                    File.Delete(fichier_erreur[0]);
                }
                if (File.Exists(fichier_erreur[1]))
                {
                    File.Delete(fichier_erreur[1]);
                }

                // si fichier_balise existe efface fichiers
                if (File.Exists(dossier_log + @"\GH_balise.html"))
                {
                    File.Delete(dossier_log + @"\GH_balise.html");
                }
                if (File.Exists(fichier_balise))
                {
                    File.Delete(fichier_balise);
                }

                // si fichier_GEDCOM existe efface fichiers
                if (File.Exists(dossier_log + @"\GH_GEDCOM.html"))
                {
                    File.Delete(dossier_log + @"\GH_GEDCOM.html");
                }
                if (File.Exists(fichier_GEDCOM))
                {
                    File.Delete(fichier_GEDCOM);
                }

                // rendre invisible les bouttons saul Btn_log
                Btn_debug.Visible = false;
                Btn_erreur.Visible = false;
                Btn_balise.Visible = false;
                Btn_GEDCOM.Visible = false;
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
            annuler = true;
            if (this.WindowState == FormWindowState.Normal)
            {
                Para.location_X = this.Location.X;
                Para.location_Y = this.Location.Y;
            }
            else
            {
                Para.location_X = this.RestoreBounds.Location.X;
                Para.location_Y = this.RestoreBounds.Location.Y;
            }
            ParaClass.Ecrire_paramettre();
            Environment.Exit(Environment.ExitCode);
        }

        private void GH_Load(object sender, EventArgs e)
        {
            Regler_code_erreur();
            // lire paramètre
            Para.arriere_plan = "FAD07E";
            Para.voir_ID = true;
            Para.voir_media = true;
            Para.date_longue = true;
            Para.voir_date_changement = true;
            Para.voir_chercheur = true;
            Para.voir_reference = true;
            Para.voir_note = true;
            Para.voir_carte = true;
            Para.voir_info_bulle = true;
            Para.mode_depanage = false;
            Para.enregistrer_balise = true;
            bool position_Bonne = false;
            (int position_X, int position_Y) = Lire_parametre();

            Effacer_Log();

            //Set up the delays for the ToolTip.
            Info_bulle.AutoPopDelay = 5000;
            Info_bulle.InitialDelay = 1000;
            Info_bulle.ReshowDelay = 500;
            //Force the ToolTip text to be displayed whether or not the form is active.
            Info_bulle.ShowAlways = true;
            Info_bulle.IsBalloon = true;
            //Set up the ToolTip text for the Button and Checkbox.

            // rapport
            Info_bulle.SetToolTip(this.Btn_GEDCOM, "Liste du GEDCOM");
            Info_bulle.SetToolTip(this.Btn_debug, "Journal de débogueur");
            Info_bulle.SetToolTip(this.Btn_erreur, "Journal d'erreur");
            Info_bulle.SetToolTip(this.Btn_balise, "Journal de balise");

            // section recherche individu
            Info_bulle.SetToolTip(this.RechercheIndividuTB, "Écrire prénon ou/et patronyme à rechercher");
            Info_bulle.SetToolTip(this.Btn_individu_avant, "Précédent");
            Info_bulle.SetToolTip(this.Btn_individu_apres, "Suivant");

            // section recherche Famille
            Info_bulle.SetToolTip(this.RechercheFamilleTB, "Écrire prénon ou/et patronyme à rechercher");
            Info_bulle.SetToolTip(this.Btn_famille_avant, "Précédent");
            Info_bulle.SetToolTip(this.Btn_famille_apres, "Suivant");

            // fiche
            Info_bulle.SetToolTip(this.Btn_fiche_individu, "Créer la fiche de l'individu sélectionné");
            Info_bulle.SetToolTip(this.Btn_fiche_famille, "Créer la fiche de la famille sélectionnée");
            Info_bulle.SetToolTip(this.Btn_total, "Créer les index et toutes les fiches individus et familles");
            // Log Btn_log_del
            Info_bulle.SetToolTip(this.Btn_log_del, "Effacer les journaux d'événement");

            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\GH"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\GH");
            if (Para.mode_depanage)
                Btn_log_del.Visible = true;
            this.Visible = false;
            Btn_annuler.Visible = false;

            LvChoixIndividu.Items.Clear();
            //organisation des élément graphique de la forme
            Lb_nombre_ligne.Text = null;

            Lb_nom_programe.Text = "";
            lblVersionProgramme.Text = "";
            Lb_fichier_GEDCOM.Text = "";
            lblDateHeure.Text = "";
            lblVersionGEDCOM.Text = "";
            lblCopyright.Text = "";
            lblCharSet.Text = "";
            lblLanguage.Text = "";
            Tb_nombre_individu.Text = "";
            Tb_nombre_famille.Text = "";
            Gb_info_GEDCOM.Visible = false;
            Cadre.Visible = false;
            Btn_individu_avant.Visible = false;
            Btn_individu_apres.Visible = false;
            Btn_famille_avant.Visible = false;
            Btn_famille_apres.Visible = false;
            Pb_recherche_individu.Visible = false;
            Pb_recherche_famille.Visible = false;
            LvChoixIndividu.Visible = false;
            lpIndividu.Visible = false;

            LvChoixFamille.Visible = false;
            lbFamilleConjoint.Visible = false;
            Btn_fiche_individu.Visible = false;
            Btn_fiche_famille.Visible = false;
            Btn_total.Visible = false;

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
            Directe(); // à commenter pour la production *** DEBUG ***
            Application.DoEvents();
            R.Animation(false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FichierGEDCOMaLire"></param>
        /// <returns>
        /// bool true si annuler false non annuler
        /// 
        /// </returns>
        public bool Lire_fichier_GEDCOM(
            string FichierGEDCOMaLire
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + " FichierGEDCOMaLire="+ FichierGEDCOMaLire);
            Regler_code_erreur();
            if (annuler)
                return (false);
            R.Animation(true);
            Tb_nombre_individu.Text = "";
            Tb_nombre_famille.Text = "";
            this.Text = "";
            string FichierGEDCOMaLireTemp = GEDCOMClass.Lire_entete_GEDCOM(FichierGEDCOMaLire);
            Application.DoEvents();
            if (FichierGEDCOMaLireTemp == null)
                return false;
            FichierGEDCOMaLire = FichierGEDCOMaLireTemp;
            GEDCOMClass.HEADER InfoGEDCOM = GEDCOMClass.Avoir_info_GEDCOM();
            lblCharSet.Text = "Code charactère: " + InfoGEDCOM.N1_CHAR;
            //Gb_info_GEDCOM.Visible = true;
            if (!GEDCOMClass.Lire_GEDCOM(FichierGEDCOMaLire))
                return false;
            this.Cursor = Cursors.WaitCursor;
            annuler = GEDCOMClass.Extraire_GEDCOM();
            if (annuler)
                return (false);
            InfoGEDCOM = GEDCOMClass.Avoir_info_GEDCOM();
            Lb_nom_programe.Text = InfoGEDCOM.N1_SOUR + " " + InfoGEDCOM.N2_SOUR_NAME;
            lblVersionProgramme.Text = "Version: " + InfoGEDCOM.N2_SOUR_VERS;
            lblDateHeure.Text = InfoGEDCOM.N1_DATE + " " + InfoGEDCOM.N2_DATE_TIME;
            Lb_fichier_GEDCOM.Text = "Fichier: " + InfoGEDCOM.N1_FILE;
            lblCopyright.Text = InfoGEDCOM.N1_COPR;
            lblVersionGEDCOM.Text = "Version: " + InfoGEDCOM.N2_GEDC_VERS;
            lblCharSet.Text = "Code charactère: " + InfoGEDCOM.N1_CHAR;
            lblLanguage.Text = "Langue: " + InfoGEDCOM.N1_LANG;
            Gb_info_GEDCOM.Visible = true;
            Cadre.Visible = true;
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
                Tb_nombre_individu.Text = String.Format("{0:n0}", f);
                Application.DoEvents();
                if (annuler)
                {
                    Btn_annuler.Visible = false;
                    return false;
                }
                bool Ok;
                string IDIndividu = ListeIDIndividu[f];
                GEDCOM.GEDCOMClass.INDIVIDUAL_RECORD info_individu;
                (Ok, info_individu) = GEDCOMClass.Avoir_info_individu(IDIndividu);
                string nom = null;
                if (Ok)
                {
                    if (info_individu.N1_NAME_liste.Count > 0)
                    {
                        if (info_individu.N1_NAME_liste[0].N1_PERSONAL_NAME_PIECES != null)
                        {
                            nom = AssemblerPatronymePrenom(info_individu.N1_NAME_liste[0].N1_PERSONAL_NAME_PIECES.Nn_SURN, info_individu.N1_NAME_liste[0].N1_PERSONAL_NAME_PIECES.Nn_GIVN);
                        }
                    }
                    if (nom == null)
                    {
                        if (info_individu.N1_NAME_liste.Count > 0)
                            nom = info_individu.N1_NAME_liste[0].N0_NAME;
                    }
                }
                string[] ligne = new string[8];
                ligne[0] = IDIndividu;
                ligne[1] = nom;
                (ligne[2], _) = HTMLClass.Convertir_date(info_individu.Date_naissance, false);
                if (info_individu.Date_naissance != null)
                    ligne[3] = info_individu.Lieu_naissance;
                (ligne[4], _) = HTMLClass.Convertir_date(info_individu.Date_deces, false);
                if (info_individu.Date_deces != null)
                    ligne[5] = info_individu.Lieu_deces;
                ligne[6] = info_individu.Date_naissance;
                ligne[7] = info_individu.Date_deces;
                itmIndividu = new ListViewItem(ligne);
                LvChoixIndividu.Items.Add(itmIndividu);
            }
            Tb_nombre_individu.Text = string.Format("{0:0,0}", LvChoixIndividu.Items.Count);
            GC.Collect();
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
                Tb_nombre_famille.Text = String.Format("{0:n0}", f);
                Application.DoEvents();
                if (annuler)
                {
                    Btn_annuler.Visible = false;
                    return false;
                }
                bool Ok;
                string IDFamille = ListeIDFamille[f];
                GEDCOM.GEDCOMClass.FAM_RECORD info_famille = GEDCOMClass.Avoir_info_famille(IDFamille);
                GEDCOM.GEDCOMClass.INDIVIDUAL_RECORD infoConjoint;
                (Ok, infoConjoint) = GEDCOMClass.Avoir_info_individu(info_famille.N1_HUSB);
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
                        if (nomConjoint == null)
                            nomConjoint = infoConjoint.N1_NAME_liste[0].N0_NAME;
                    }
                }
                string nomConjointe = null;
                GEDCOM.GEDCOMClass.INDIVIDUAL_RECORD infoConjointe;
                (Ok, infoConjointe) = GEDCOMClass.Avoir_info_individu(info_famille.N1_WIFE);
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
                        if (nomConjointe == null)
                            nomConjointe = infoConjointe.N1_NAME_liste[0].N0_NAME;
                    }
                }
                //GEDCOMClass.EVEN_ATTRIBUTE_STRUCTURE Mariage = GEDCOMClass.AvoirEvenementMariage(infoFamille.N1_EVEN_Liste);
                string[] ligne = new string[6];
                ligne[0] = IDFamille;
                ligne[1] = nomConjoint;
                ligne[2] = nomConjointe;
                (ligne[3], _) = HTMLClass.Convertir_date(info_famille.Date_mariage, false);
                if (info_famille.Lieu_mariage != null)
                    ligne[4] = info_famille.Lieu_mariage;
                ligne[5] = info_famille.Date_mariage;
                itmFamille = new ListViewItem(ligne);
                LvChoixFamille.Items.Add(itmFamille);
            }
            Application.DoEvents();
            Tb_nombre_famille.Text = string.Format("{0:0,0}", LvChoixFamille.Items.Count);
            GC.Collect();
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
            GC.Collect();
            //R..Z("<b>Retourne</b> true");
            return true;
        }
        public (int position_X, int position_Y) Lire_parametre()
        {
            Regler_code_erreur();
             string Fichier_parametre = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\GH\GH.ini";
            int position_X = 5;
            int position_Y = 5;
            Regler_code_erreur();
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
                            if (Ligne == "[dossier_page]")
                            {
                                Para.dosssier_page = sr.ReadLine();
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
                                if (a == "1")
                                    Para.voir_ID = true;
                                else
                                    Para.voir_ID = false;
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
                                if (a == "1")
                                    Para.date_longue = true;
                                else
                                    Para.date_longue = false;
                            }
                            if (Ligne == "[voir_date_changement]")
                            {
                                string a = sr.ReadLine();
                                if (a == "1")
                                    Para.voir_date_changement = true;
                                else
                                    Para.voir_date_changement = false;
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
                                if (a == "1")
                                    Para.voir_carte = true;
                                else
                                    Para.voir_carte = false;
                            }
                            if (Ligne == "[voir_info_bulle]")
                            {
                                string a = sr.ReadLine();
                                if (a == "1")
                                    Para.voir_info_bulle = true;
                                else
                                    Para.voir_info_bulle = false;
                                if (Para.voir_info_bulle)
                                    Info_bulle.Active = true;
                                else
                                    Info_bulle.Active = false;
                            }

                            // depanage
                            if (Ligne == "[mode_depanage]")
                            {
                                string a = sr.ReadLine();
                                if (a == "1")
                                {
                                    Para.mode_depanage = true;
                                    Btn_log_del.Visible = true;
                                }
                                else
                                {
                                    Para.mode_depanage = false;
                                    Btn_log_del.Visible = false;
                                }
                            }

                            // enregister balise
                            if (Ligne == "[enregistrer_balise]")
                            {
                                string a = sr.ReadLine();
                                if (a == "1")
                                {
                                    Para.enregistrer_balise = true;
                                    Btn_balise.BackgroundImage = Properties.Resources.Btn_B;
                                    Btn_balise.Enabled = true;
                                }
                                else
                                {
                                    Para.enregistrer_balise = false;
                                    Btn_balise.BackgroundImage = Properties.Resources.Btn_B_gris;
                                    Btn_balise.Enabled = false;

                                }
                            }
                            // confidentiel
                            if (Ligne == "[tout_evenement]")
                            {
                                switch (sr.ReadLine())
                                {
                                    case "0":
                                        Para.tout_evenement = false;
                                        break;
                                    case "1":
                                        Para.tout_evenement = true;
                                        break;
                                }
                            }
                            if (Ligne == "[tout_evenement_date]")
                            {
                                switch (sr.ReadLine())
                                {
                                    case "0":
                                        Para.tout_evenement_date = false;
                                        break;
                                    case "1":
                                        Para.tout_evenement_date = true;
                                        break;
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
                                    case "0":
                                        Para.naissance_date = false;
                                        break;
                                    case "1":
                                        Para.naissance_date = true;
                                        break;
                                }
                            }
                            if (Ligne == "[naissance_ans]")
                                Int32.TryParse(sr.ReadLine(), out Para.naissance_ans);

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
                                    case "0":
                                        Para.union_date = false;
                                        break;
                                    case "1":
                                        Para.union_date = true;
                                        break;
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
                            if (Ligne == "[deces_ans]")
                                Int32.TryParse(sr.ReadLine(), out Para.deces_ans);

                            // inhumation
                            if (Ligne == "[inhumation_date]")
                            {
                                switch (sr.ReadLine())
                                {
                                    case "0":
                                        Para.inhumation_date = false;
                                        break;
                                    case "1":
                                        Para.inhumation_date = true;
                                        break;
                                }
                            }
                            if (Ligne == "[inhumation_ans]")
                                Int32.TryParse(sr.ReadLine(), out Para.inhumation_ans);

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
                                    case "0":
                                        Para.autre_date = false;
                                        break;
                                    case "1":
                                        Para.autre_date = true;
                                        break;
                                }
                            }
                            if (Ligne == "[autre_ans]")
                                Int32.TryParse(sr.ReadLine(), out Para.autre_ans);

                            // citoyen
                            if (Ligne == "[citoyen_date]")
                            {
                                switch (sr.ReadLine())
                                {
                                    case "0":
                                        Para.citoyen_date = false;
                                        break;
                                    case "1":
                                        Para.citoyen_date = true;
                                        break;
                                }
                            }
                            if (Ligne == "[citoyen_ans]")
                                Int32.TryParse(sr.ReadLine(), out Para.citoyen_ans);

                            // testament
                            if (Ligne == "[testament_information]")
                            {
                                switch (sr.ReadLine())
                                {
                                    case "0":
                                        Para.testament_information = false;
                                        break;
                                    case "1":
                                        Para.testament_information = true;
                                        break;
                                }
                            }
                            if (Ligne == "[testament_ans]")
                                Int32.TryParse(sr.ReadLine(),
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
            }
            if (Para.dosssier_page != null)
            {
                fichier_deboguer[1] = Para.dosssier_page + @"\GH_deboguer.html";
                fichier_erreur[1] = Para.dosssier_page + @"\GH_erreur.html";
                fichier_balise = Para.dosssier_page + @"\GH_balise.html";
                fichier_GEDCOM = Para.dosssier_page + @"\GH_GEDCOM.html";
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
                if (ageD > 30)
                    return true;
            }
            if (naissance != "")
            {
                DateTime n = DateTime.ParseExact(naissance, "yyyy-MM-dd", null);
                int ageN = maintenent.Year - n.Year;
                if (ageN > 100)
                    return true;
            }
            if (deces == "" && naissance == "")
                return true;

            return false;
        }
        private void Fichier_total()
        {
            Regler_code_erreur();
            Application.DoEvents();
            Btn_annuler.Visible = true;
            if (!DossierHTMLValide())
            {
                return;
            }
            Form_desactiver(true);
            Array.ForEach(Directory.GetFiles(dossier_sortie + @"\medias"), delegate (string path)
            {
                File.Delete(path);
            });
            Array.ForEach(Directory.GetFiles(dossier_sortie + @"\individus"), delegate (string path)
            {
                File.Delete(path);
            });
            Array.ForEach(Directory.GetFiles(dossier_sortie + @"\individus\medias"), delegate (string path)
            {
                File.Delete(path);
            });
            Array.ForEach(Directory.GetFiles(dossier_sortie + @"\familles"), delegate (string path)
            {
                File.Delete(path);
            });
            Array.ForEach(Directory.GetFiles(dossier_sortie, "*.html"), delegate (string path)
            {
                File.Delete(path);
            });
            if (annuler)
                return;
            List<string> ListeID = new List<string>();
            ListeID.Clear();
            ListeID = GEDCOMClass.Avoir_liste_ID_individu();
            int compteur = 1;
            compteur = 0;
            HTML.Index(FichierGEDCOM, Tb_nombre_individu.Text, Tb_nombre_famille.Text);
            if (annuler)
                return;
            HTML.Index_individu(dossier_sortie);
            if (annuler)
                return;
            HTML.Index_famille_conjoint(dossier_sortie);
            if (annuler)
                return;
            HTML.Index_famille_conjointe(dossier_sortie);
            if (annuler)
                return;
            foreach (string ID in ListeID)
            {
                HTML.Fiche_individu(ID, dossier_sortie, true);
                compteur++;
                if (annuler)
                    return;
            }
            ListeID = GEDCOMClass.Avoir_liste_ID_famille();
            compteur = 1;
            foreach (string ID in ListeID)
            {
                HTML.Fiche_famille(ID, dossier_sortie, true);
                compteur++;
                if (annuler)
                    return;
            }
            try
            {
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
            if (LvChoixFamille.SelectedItems.Count == 0)
                return;
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
                Btn_individu_avant.Enabled = false;
                Btn_individu_apres.Enabled = false;
                RechercheFamilleTB.Enabled = false;
                Btn_famille_avant.Enabled = false;
                Btn_famille_apres.Enabled = false;

                // rapport
                Btn_GEDCOM.Enabled = false;
                Btn_debug.Enabled = false;
                Btn_erreur.Enabled = false;
                Btn_balise.Enabled = false;

                // fiche
                Btn_fiche_individu.Enabled = false;
                Btn_fiche_famille.Enabled = false;
                Btn_total.Enabled = false;
            }
            else
            {
                this.UseWaitCursor = false;
                LvChoixIndividu.Enabled = true;
                LvChoixFamille.Enabled = true;
                menuPrincipal.Enabled = true;

                // Recherche
                RechercheIndividuTB.Enabled = true;
                Btn_individu_avant.Enabled = true;
                Btn_individu_apres.Enabled = true;
                RechercheFamilleTB.Enabled = true;
                Btn_famille_avant.Enabled = true;
                Btn_famille_apres.Enabled = true;

                // rapport
                Btn_GEDCOM.Enabled = true;
                Btn_debug.Enabled = true;
                Btn_erreur.Enabled = true;
                Btn_balise.Enabled = true;

                // fiche
                Btn_fiche_individu.Enabled = true;
                Btn_fiche_famille.Enabled = true;
                Btn_total.Enabled = true;
            }
        }
        private void FormVisible(bool status)
        {
            Regler_code_erreur();
            if (status)
            {

                Btn_total.Visible = true;
                Btn_fiche_individu.Visible = true;
                Btn_fiche_famille.Visible = true;
                Gb_info_GEDCOM.Visible = true;
                Cadre.Visible = true;
                LvChoixIndividu.Visible = true;
                LvChoixFamille.Visible = true;
                lpIndividu.Visible = true;
                lbFamilleConjoint.Visible = true;
                RechercheFamilleB.Visible = true;
                RechercheFamilleTB.Visible = true;
                RechercheIndividuB.Visible = true;
                RechercheIndividuTB.Visible = true;
                menuPrincipal.Visible = true;
                Btn_annuler.Visible = false;
                Pb_recherche_individu.Visible = true;
                Pb_recherche_famille.Visible = true;
                Btn_individu_avant.Visible = true;
                Btn_individu_apres.Visible = true;
                Btn_famille_avant.Visible = true;
                Btn_famille_apres.Visible = true;
            }
            else
            {
                Btn_balise.Visible = false;
                Btn_total.Visible = false;
                Btn_fiche_individu.Visible = false;
                Btn_fiche_famille.Visible = false;
                Gb_info_GEDCOM.Visible = false;
                Cadre.Visible = false;
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
                Btn_individu_avant.Visible = false;
                Btn_individu_apres.Visible = false;
                Btn_famille_avant.Visible = false;
                Btn_famille_apres.Visible = false;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message_1"></param>
        /// <param name="message_2"></param>
        /// <param name="erreur"></param>
        /// <param name="bouton "></param>
        /// <param name="icon"></param>
        /// <returns>reponse</returns>

        private void AideToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Page page = new Page("file:///" + Application.StartupPath + "\\aide\\aide.html", "aide");
            Page frm = page;
            frm.Tag = this;
            frm.Show(this);
            R.Animation(false);
        }
        private void LvChoixIndividu_DoubleClick(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Genere_page_individu(IDCourantIndividu);
        }
        private void OuvrirDossierDesFichesHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            if (!Directory.Exists(Para.dosssier_page))
            {
                R.Afficher_message(
                    "S.V.P. Spécifiez dans les paramêtres, le dossier page."
                    );
                return;
            }
            System.Diagnostics.Process.Start(@Para.dosssier_page);
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
            if (RechercheIndividuTB.Text == "")
            {
                LvChoixIndividu.SelectedItems.Clear();
                return;
            }
            string[] mots = RechercheIndividuTB.Text.ToLower().Split(' ');
            for (int f = 0; f < LvChoixIndividu.Items.Count; f++)
            {
                bool Ok = true;
                foreach (string m in mots)
                {
                    if (LvChoixIndividu.Items[f].SubItems[1].Text.ToLower().IndexOf(m) == -1)
                        Ok = false;
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

        public void Voir_paramettre()
        {
            Regler_code_erreur();
            Form l = new ParaClass
            {
                StartPosition = FormStartPosition.Manual,
                Left = this.Left + 70,  // position de la fenêtte GH
                Top = this.Top + 60     // position de la fenêtte GH
            };
            l.ShowDialog(this);
            if (Para.voir_info_bulle)
                Info_bulle.Active = true;
            else
                Info_bulle.Active = false;
            if (Para.mode_depanage)
                Btn_log_del.Visible = true;
            else
                Btn_log_del.Visible = false;
            Application.DoEvents();
        }

        private void ParamètresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            if (this.WindowState == FormWindowState.Normal)
            {
                Para.location_X = this.Location.X;
                Para.location_Y = this.Location.Y;
            }
            else
            {
                Para.location_X = this.RestoreBounds.Location.X;
                Para.location_Y = this.RestoreBounds.Location.Y;
            }
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

        private void Btn_G_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Btn_GEDCOM.Visible = false;
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
                    ligne.WriteLine("\t\t<title>GEDCOM</title>");
                    ligne.WriteLine("\t\t<style>\n" +
                                "\t\t\th1{color:#00F}\n" +
                                "\t\t\t\t.col0{width:150px;vertical-align:top;}\n" +
                                "\t\t\t\t.col1{width:90px;vertical-align:top;}\n" +
                                "\t\t\t\t.col2{width:50px;vertical-align:top;}\n" +
                                "\t\t\t\t.col3{width:330px;vertical-align:top;}\n" +
                                "\t\t\t\t.col4{vertical-align:top;}\n" +
                                "\t\t</style>");
                    ligne.WriteLine("\t</head>");
                    ligne.WriteLine("\t<body>");
                    ligne.WriteLine("\t\t<h1>GEDCOM</h1>");
                    ligne.WriteLine("\t\t<table style=\"border:2px solid #000;width:100%\">");
                    ligne.WriteLine("\t\t\t<tr>");
                    ligne.WriteLine("\t\t\t\t<tr>");
                    ligne.WriteLine("\t\t\t\t\t<td style=\"width:150px\">Nom</td><td>" + info_HEADER.N2_SOUR_NAME + "</td></tr>");
                    ligne.WriteLine("\t\t\t<tr><td>Version</td><td>" + info_HEADER.N2_SOUR_VERS + "<td></tr>");
                    ligne.WriteLine("\t\t\t<tr><td>Date</td><td>" + info_HEADER.N1_DATE + " " + info_HEADER.N2_DATE_TIME + "<td></tr>");
                    ligne.WriteLine("\t\t\t<tr><td>Copyright</td><td>" + info_HEADER.N1_COPR + "<td></tr>");
                    ligne.WriteLine("\t\t\t<tr><td>Version</td><td>" + info_HEADER.N2_GEDC_VERS + "<td></tr>");
                    ligne.WriteLine("\t\t\t<tr><td>Code charactère</td><td>" + info_HEADER.N1_CHAR + "<td></tr>");
                    ligne.WriteLine("\t\t\t<tr><td>Langue</td><td>" + info_HEADER.N1_LANG + "<td></tr>");
                    ligne.WriteLine("\t\t\t<tr><td>Fichier sur le disque</td><td>" + info_HEADER.Nom_fichier_disque + "<td></tr>");
                    ligne.WriteLine("\t\t\t<tr><td>Page générée le</td><td>" + DateTime.Now + "<td></tr>");
                    System.Version version = Assembly.GetExecutingAssembly().GetName().Version;

                    ligne.WriteLine("\t\t\t<tr><td>Version de GH</td><td>" + version.Major + "." + version.Minor + "." + version.Build + "<td></tr>");
                    ligne.WriteLine("\t\t\t\t<tr><td>Fichier déboquer</td><td>" + fichier_GEDCOM + "<td></tr>");
                    ligne.WriteLine("\t\t</table>");
                    string text = "";
                    int position = 1;
                    ligne.WriteLine("\t\t<br /><br />");
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
                        ligne.WriteLine("\t\t" + position.ToString("D8") + "&nbsp;&nbsp;" + tab + text + "<br />");
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
                    erreur,
                    null,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
            GC.Collect();
            Btn_GEDCOM.Visible = true;
        }

        private void Btn_debug_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Btn_debug.Enabled = false;
            if (File.Exists(fichier_deboguer[0]))
            {
                Page page = new Page("file:///" + fichier_deboguer[0], "Déboguer");
                Page frm = page;
                frm.Tag = this;
                frm.Show(this);
            }
            if (File.Exists(fichier_deboguer[1]))
            {
                Page page = new Page("file:///" + fichier_deboguer[1], "Déboguer");
                Page frm = page;
                frm.Tag = this;
                frm.Show(this);
            }
            GC.Collect();
            Btn_debug.Enabled = true;
        }

        private void Btn_erreur_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Btn_erreur.Enabled = false;
            if (File.Exists(fichier_erreur[0]))
            {
                Page page = new Page("file:///" + fichier_erreur[0], "Erreur du bureau");
                Page frm = page;
                frm.Tag = this;
                frm.Show(this);
            }
            if (File.Exists(fichier_erreur[1]))
            {
                Page page = new Page("file:///" + fichier_erreur[1], "Erreur du bureau du dossier page");
                Page frm = page;
                frm.Tag = this;
                frm.Show(this);
            }
            GC.Collect();
            Btn_erreur.Enabled = true;
        }

        private void Btn_balise_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Btn_balise.Enabled = false;
            if (File.Exists(fichier_balise))
            {
                Page page = new Page("file:///" + fichier_balise, "Balise");
                Page frm = page;
                frm.Tag = this;
                frm.Show(this);
            }
            GC.Collect();
            Btn_balise.Enabled = true;
        }

        private void Btn_individu_avant_Click(object sender, EventArgs e)
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

        private void Btn__individu_apres_Click(object sender, EventArgs e)
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

        private void Btn_famille_avant_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Form_desactiver(true);
            int index = 0;
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
            Form_desactiver(false);
        }

        private void Btn_famille_apres_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Form_desactiver(true);
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
            Form_desactiver(false);
        }

        private void Btn_fiche_individu_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Btn_fiche_individu.Enabled = false;
            Genere_page_individu(IDCourantIndividu);
            GC.Collect();
            Btn_fiche_individu.Enabled = true;
            
        }

        private void Btn_fiche_famille_Click(object sender, EventArgs e)
        {
            Btn_fiche_famille.Enabled = false;
            Regler_code_erreur();
            Genere_page_famille(IDCourantFamilleConjoint);
            GC.Collect();
            Btn_fiche_famille.Enabled = true;
        }

        private void Btn__total_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Btn_total.Enabled = false;
            Btn_annuler.Visible = true;
            R.Animation(true);
            Form_desactiver(true);
            Fichier_total();
            annuler = false;
            Btn_annuler.Visible = false;
            Form_desactiver(false);
            GC.Collect();
            Btn_total.Enabled = true;
            R.Animation(false);
        }

        private void Btn_log_del_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Btn_log_del.Enabled = false;
            Effacer_Log();
            GC.Collect();
            Btn_log_del.Enabled = true;
        }

        private void Btn_annuler_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            annuler = true;
            GC.Collect();
        }

        private void QuitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cadre_Click(object sender, EventArgs e)
        {

        }
    }
    

}
