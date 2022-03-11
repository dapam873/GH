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


using Routine;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace GH
{
    public partial class Parametre : Form
    {
        public Parametre()
        {
            InitializeComponent();
        }

        bool recharger_HTML = false;

        private void TbDossierHTML_TextChanged(object sender, EventArgs e)
        {
            if (Tb_dossier_page.Text != "")
            {
                if (!Directory.Exists(Tb_dossier_page.Text))
                {
                    Tb_dossier_page.BackColor = Color.Red;
                    return;
                }
            }
            Tb_dossier_page.BackColor = Color.White;
            GHClass.Para.dosssier_page = Tb_dossier_page.Text;
            recharger_HTML = true;
        }

        private void TbDossierMedia_TextChanged(object sender, EventArgs e)
        {
            if (TbDossierMedia.Text != "")
            {
                if (!Directory.Exists(TbDossierMedia.Text))
                {
                    TbDossierMedia.BackColor = Color.Red;
                    return;
                }
            }
            TbDossierMedia.BackColor = Color.White;
            GHClass.Para.dossier_media = TbDossierMedia.Text;
            recharger_HTML = true;
        }

        private void PbDossierHTML_Click(object sender, EventArgs e)
        {
            AvoirDossierPageWeb();
        }
        private void AvoirDossierPageWeb()
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog
            {
                Description = "Où enregister les pages?"
            };
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                GHClass.Para.dosssier_page = folderBrowserDialog1.SelectedPath;
                Tb_dossier_page.Text = GHClass.Para.dosssier_page;
                Btn_dossier_page.Focus();
            }
        }

        private void PbDossierMedia_Click(object sender, EventArgs e)
        {
            AvoirDossierMedias();
        }
        private void AvoirDossierMedias()
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog
            {
                Description = "Où est le dossier de vos médias?"
            };
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                GHClass.Para.dossier_media = folderBrowserDialog1.SelectedPath;
                TbDossierMedia.Text = GHClass.Para.dossier_media;
                Btn_dossier_page.Focus();
            }
        }
        private void Parametre_Load(object sender, EventArgs e)
        {
            Tb_dossier_page.Text = GHClass.Para.dosssier_page;
            TbDossierMedia.Text = GHClass.Para.dossier_media;
            Lb_code_arriere_plan.Text = GHClass.Para.arriere_plan;
            int rouge = int.Parse(GHClass.Para.arriere_plan.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int vert = int.Parse(GHClass.Para.arriere_plan.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int bleu = int.Parse(GHClass.Para.arriere_plan.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            Lb_couleur_arriere_plan.ForeColor = Color.FromArgb(rouge, vert, bleu);

            if (GHClass.Para.voir_ID)
                Cb_voir_ID.Checked = true;
            else
                Cb_voir_ID.Checked = false;
            if (GHClass.Para.voir_media)
                CbVoirMedia.Checked = true;
            else
                CbVoirMedia.Checked = false;
            if (GHClass.Para.voir_date_changement)
                CbVoirDateChangement.Checked = true;
            else
                CbVoirDateChangement.Checked = false;
            if (GHClass.Para.voir_reference)
                CbVoirReference.Checked = true;
            else
                CbVoirReference.Checked = false;
            if (GHClass.Para.voir_chercheur)
                CbVoirChercheur.Checked = true;
            else
                CbVoirChercheur.Checked = false;
            if (GHClass.Para.voir_carte)
                CbVoirCarte.Checked = true;
            else
                CbVoirCarte.Checked = false;
            if (GHClass.Para.voir_note)
                CbVoirNote.Checked = true;
            else
                CbVoirNote.Checked = false;
            if (GHClass.Para.voir_info_bulle)
                CbVoirInfoBulle.Checked = true;
            else
                CbVoirInfoBulle.Checked = false;
            if (GHClass.Para.date_longue)
                CbDateLongue.Checked = true;
            else
                CbDateLongue.Checked = false;
            if (GHClass.Para.mode_depanage)
                Cb_mode_depanage.Checked = true;
            else
                Cb_mode_depanage.Checked = false;

            // confidentiel
            if (GHClass.Para.tout_evenement)
                Cb_tout_evenement.Checked = true;
            else
                Cb_certain.Checked = true;
            if (GHClass.Para.tout_evenement_date)
                Cb_tout_evenement_date.Checked = true;
            else
                Cb_tout_evenement_date.Checked = false;
            Tb_tout_evenement_ans.Text = GHClass.Para.tout_evenement_ans.ToString();

            // naissance
            if (GHClass.Para.naissance_date)
                Cb_naissance_date.Checked = true;
            else
                Cb_naissance_date.Checked = false;
            Tb_naissance_ans.Text = GHClass.Para.naissance_ans.ToString();

            // bapteme
            if (GHClass.Para.bapteme_date)
                Cb_bapteme_date.Checked = true;
            else
                Cb_bapteme_date.Checked = false;
            Tb_bapteme_ans.Text = GHClass.Para.bapteme_ans.ToString();

            // union
            if (GHClass.Para.union_date)
                Cb_union_date.Checked = true;
            else
                Cb_union_date.Checked = false;
            Tb_union_ans.Text = GHClass.Para.union_ans.ToString();

            // décès
            if (GHClass.Para.deces_date)
                Cb_deces_date.Checked = true;
            else
                Cb_deces_date.Checked = false;
            Tb_deces_ans.Text = GHClass.Para.deces_ans.ToString();

            // inhumation
            if (GHClass.Para.inhumation_date)
                Cb_inhumation_date.Checked = true;
            else
                Cb_inhumation_date.Checked = false;
            Tb_inhumation_ans.Text = GHClass.Para.inhumation_ans.ToString();

            // ordonnance
            if (GHClass.Para.ordonnance_date)
                Cb_ordonnance_date.Checked = true;
            else
                Cb_ordonnance_date.Checked = false;
            Tb_ordonnance_ans.Text = GHClass.Para.ordonnance_ans.ToString();

            // autre
            if (GHClass.Para.autre_date)
                Cb_autre_date.Checked = true;
            else
                Cb_autre_date.Checked = false;
            Tb_autre_ans.Text = GHClass.Para.autre_ans.ToString();

            if (GHClass.Para.testament_information)
                Cb_testament_information.Checked = true;
            else
                Cb_testament_information.Checked = false;
            Tb_testament_ans.Text = GHClass.Para.testament_ans.ToString();

            // citoyen
            if (GHClass.Para.citoyen_date)
                Cb_citoyen_date.Checked = true;
            else
                Cb_citoyen_date.Checked = false;
            Tb_citoyen_ans.Text = GHClass.Para.citoyen_ans.ToString();

            recharger_HTML = false;
            /*
            if (Rb_tout_evenement.Checked)
            {
                P_tout_evenement.Enabled = true;
                P_certain_evenement.Enabled = false;
            }
            else
            {
                P_tout_evenement.Enabled = false;
                P_certain_evenement.Enabled = true;
            }
            */
        }
        private int Convertir_texte_int(string s)
        {
            Int32.TryParse(s, out int n);
            return (n);

        }

        private void Btn_Fermer_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            GHClass.Para.dosssier_page = Tb_dossier_page.Text;
            GHClass.Para.dossier_media = TbDossierMedia.Text;
            if (Cb_voir_ID.Checked)
                GHClass.Para.voir_ID = true;
            else
                GHClass.Para.voir_ID = false;
            if (CbVoirMedia.Checked)
                GHClass.Para.voir_media = true;
            else
                GHClass.Para.voir_media = false;
            if (CbDateLongue.Checked)
                GHClass.Para.date_longue = true;
            else
                GHClass.Para.date_longue = false;
            if (CbVoirDateChangement.Checked)
                GHClass.Para.voir_date_changement = true;
            else
                GHClass.Para.voir_date_changement = false;
            if (CbVoirChercheur.Checked)
                GHClass.Para.voir_chercheur = true;
            else
                GHClass.Para.voir_chercheur = false;
            if (CbVoirReference.Checked)
                GHClass.Para.voir_reference = true;
            else
                GHClass.Para.voir_reference = false;
            if (CbVoirNote.Checked)
                GHClass.Para.voir_note = true;
            else
                GHClass.Para.voir_note = false;
            if (CbVoirCarte.Checked)
                GHClass.Para.voir_carte = true;
            else
                GHClass.Para.voir_carte = false;
            if (CbVoirInfoBulle.Checked)
                GHClass.Para.voir_info_bulle = true;
            else
                GHClass.Para.voir_info_bulle = false;
            //if (Cb_mode_depanage.Checked) GHClass.Para.mode_depanage = true; else GHClass.Para.mode_depanage = false;

            // confidentiel
            if (Cb_tout_evenement.Checked)
                GHClass.Para.tout_evenement = true;
            else
                GHClass.Para.tout_evenement = false;
            if (Cb_tout_evenement_date.Checked)
                GHClass.Para.tout_evenement_date = true;
            else
                GHClass.Para.tout_evenement_date = false;
            GHClass.Para.tout_evenement_ans = Convertir_texte_int(Tb_tout_evenement_ans.Text);

            // naissance
            if (Cb_naissance_date.Checked)
                GHClass.Para.naissance_date = true;
            else
                GHClass.Para.naissance_date = false;
            GHClass.Para.naissance_ans = Convertir_texte_int(Tb_naissance_ans.Text);

            // bapteme
            if (Cb_bapteme_date.Checked)
                GHClass.Para.bapteme_date = true;
            else
                GHClass.Para.bapteme_date = false;
            GHClass.Para.bapteme_ans = Convertir_texte_int(Tb_bapteme_ans.Text);

            // union
            if (Cb_union_date.Checked)
                GHClass.Para.union_date = true;
            else
                GHClass.Para.union_date = false;
            GHClass.Para.union_ans = Convertir_texte_int(Tb_union_ans.Text);

            // décès
            if (Cb_deces_date.Checked)
                GHClass.Para.deces_date = true;
            else
                GHClass.Para.deces_date = false;
            GHClass.Para.deces_ans = Convertir_texte_int(Tb_deces_ans.Text);

            // inhumation
            if (Cb_inhumation_date.Checked)
                GHClass.Para.inhumation_date = true;
            else
                GHClass.Para.inhumation_date = false;
            GHClass.Para.inhumation_ans = Convertir_texte_int(Tb_inhumation_ans.Text);

            // ordonnance_date
            if (Cb_ordonnance_date.Checked)
                GHClass.Para.ordonnance_date = true;
            else
                GHClass.Para.ordonnance_date = false;
            GHClass.Para.ordonnance_ans = Convertir_texte_int(Tb_ordonnance_ans.Text);

            // autre
            if (Cb_autre_date.Checked)
                GHClass.Para.autre_date = true;
            else
                GHClass.Para.autre_date = false;
            GHClass.Para.autre_ans = Convertir_texte_int(Tb_autre_ans.Text);

            // citoyen
            if (Cb_citoyen_date.Checked)
                GHClass.Para.citoyen_date = true;
            else
                GHClass.Para.citoyen_date = false;
            GHClass.Para.citoyen_ans = Convertir_texte_int(Tb_citoyen_ans.Text);

            // testament
            if (Cb_testament_information.Checked)
                GHClass.Para.testament_information = true;
            else
                GHClass.Para.testament_information = false;
            GHClass.Para.testament_ans = Convertir_texte_int(Tb_testament_ans.Text);

            // si modifier affiche message
            if (recharger_HTML && GHClass.FichierGEDCOMaLire != null)
            {
                R.Afficher_message(
                    "Pour prendre effet, on doit refaire les fiches HTML.",
                    "",
                    null,
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            this.Close();
        }

        private void CbVoirMedia_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void CbVoirChercheur_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void CbVoirReference_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void CbVoirNote_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private static void Regler_code_erreur([CallerLineNumber] int sourceLineNumber = 0)
        {
            GHClass.erreur = "PA" + sourceLineNumber;
        }

        private void Cb_voir_ID_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void CbDateLongue_TextChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void CbVoirDateChangement_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void CbVoirCarte_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void Lb_code_arriere_plan_TextChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void Cb_naissance_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void Cb_bapteme_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void Cb_mariage_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void Cb_separation_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void Cb_divorce_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void Cb_deces_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void Cb_inhumation_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void Cb_medicale_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void Cb_citoyen_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void Cb_religion_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void Cb_testament_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }


        private void Rb_certain_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
            /*if (Rb_certain.Checked)
            {
                P_certain_evenement.Enabled = true;
                P_tout_evenement.Enabled = false;
            }
            else
            {
                P_certain_evenement.Enabled = false;
                P_tout_evenement.Enabled = true;
            }
            */
        }

        private void Cb_tout_evenement_date_CheckedChanged(object sender, EventArgs e)
        {
            if (R.IsNotNullOrEmpty(GHClass.FichierGEDCOM))
                recharger_HTML = true;
        }


        private void Cb_citoyen_date_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void Cb_religion_date_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void CbVoirOrdonnances_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void Rb_tout_evenement_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;



            /*
            if (Rb_tout_evenement.Checked)
            {
                P_tout_evenement.Enabled = true;
                P_certain_evenement.Enabled = false;
            }
            else
            {
                P_tout_evenement.Enabled = false;
                P_certain_evenement.Enabled = true;
            }
            */
        }

        private void Cb_mode_depanage_CheckedChanged(object sender, EventArgs e)
        {
            if (Cb_mode_depanage.Checked)
            {
                GHClass.Para.mode_depanage = true;
            }
            else
            {
                GHClass.Para.mode_depanage = false;
            }
        }

        private void AideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Regler_code_erreur();
            Page page = new Page("file:///" + Application.StartupPath + "\\aide\\aide.html", "aide");
            Page frm = page;
            frm.Tag = this;
            frm.Show(this);
        }

        private void Cb_bapteme_date_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void Cb_ordonnance_date_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
        }

        private void Tb_naissance_ans_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void Tb_bapteme_ans_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void Tb_citoyen_ans_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void Tb_ordonnance_ans_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void Tb_union_ans_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void Tb_deces_ans_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void Tb_inhumation_ans_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void Tb_autre_ans_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void Tb_testament_ans_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void Tb_tout_evenement_ans_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void Btn_dossier_page_Click(object sender, EventArgs e)
        {
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            AvoirDossierPageWeb();
            Cursor.Position = new Point(X, Y);
        }

        private void Btn_dossier_media_Click(object sender, EventArgs e)
        {
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            AvoirDossierMedias();
            Cursor.Position = new Point(X, Y);
        }

        private void Btn_fermer_Click_1(object sender, EventArgs e)
        {
            while (!Directory.Exists(Tb_dossier_page.Text))
            {
                DialogResult reponse = R.Afficher_message(
                    "Le dossier Page HTML n'existe pas, SVP corrigez.",
                    null,
                    null,
                    "Dossier Page HTML ?",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error
                    );
                if (reponse == System.Windows.Forms.DialogResult.Cancel)
                {
                    this.Close();
                    return;
                }
            }
            GHClass.Para.dosssier_page = Tb_dossier_page.Text;
            GHClass.Para.dossier_media = TbDossierMedia.Text;
            if (Cb_voir_ID.Checked)
                GHClass.Para.voir_ID = true;
            else
                GHClass.Para.voir_ID = false;
            if (CbVoirMedia.Checked)
                GHClass.Para.voir_media = true;
            else
                GHClass.Para.voir_media = false;
            if (CbDateLongue.Checked)
                GHClass.Para.date_longue = true;
            else
                GHClass.Para.date_longue = false;
            if (CbVoirDateChangement.Checked)
                GHClass.Para.voir_date_changement = true;
            else
                GHClass.Para.voir_date_changement = false;
            if (CbVoirChercheur.Checked)
                GHClass.Para.voir_chercheur = true;
            else
                GHClass.Para.voir_chercheur = false;
            if (CbVoirReference.Checked)
                GHClass.Para.voir_reference = true;
            else
                GHClass.Para.voir_reference = false;
            if (CbVoirNote.Checked)
                GHClass.Para.voir_note = true;
            else
                GHClass.Para.voir_note = false;
            if (CbVoirCarte.Checked)
                GHClass.Para.voir_carte = true;
            else
                GHClass.Para.voir_carte = false;
            // Voir info bulle
            if (CbVoirInfoBulle.Checked)
                GHClass.Para.voir_info_bulle = true;
            else
                GHClass.Para.voir_info_bulle = false;
            if (Cb_mode_depanage.Checked)
                GHClass.Para.mode_depanage = true;
            else
                GHClass.Para.mode_depanage = false;

            // confidentiel
            if (Cb_tout_evenement.Checked)
                GHClass.Para.tout_evenement = true;
            else
                GHClass.Para.tout_evenement = false;
            if (Cb_tout_evenement_date.Checked)
                GHClass.Para.tout_evenement_date = true;
            else
                GHClass.Para.tout_evenement_date = false;
            GHClass.Para.tout_evenement_ans = Convertir_texte_int(Tb_tout_evenement_ans.Text);

            // naissance
            if (Cb_naissance_date.Checked)
                GHClass.Para.naissance_date = true;
            else
                GHClass.Para.naissance_date = false;
            GHClass.Para.naissance_ans = Convertir_texte_int(Tb_naissance_ans.Text);

            // bapteme
            if (Cb_bapteme_date.Checked)
                GHClass.Para.bapteme_date = true;
            else
                GHClass.Para.bapteme_date = false;
            GHClass.Para.bapteme_ans = Convertir_texte_int(Tb_bapteme_ans.Text);

            // union
            if (Cb_union_date.Checked)
                GHClass.Para.union_date = true;
            else
                GHClass.Para.union_date = false;
            GHClass.Para.union_ans = Convertir_texte_int(Tb_union_ans.Text);

            // décès
            if (Cb_deces_date.Checked)
                GHClass.Para.deces_date = true;
            else
                GHClass.Para.deces_date = false;
            GHClass.Para.deces_ans = Convertir_texte_int(Tb_deces_ans.Text);

            // inhumation
            if (Cb_inhumation_date.Checked)
                GHClass.Para.inhumation_date = true;
            else
                GHClass.Para.inhumation_date = false;
            GHClass.Para.inhumation_ans = Convertir_texte_int(Tb_inhumation_ans.Text);

            // ordonnance
            if (Cb_ordonnance_date.Checked)
            {
                GHClass.Para.ordonnance_date = true;
            }
            else
            {
                GHClass.Para.ordonnance_date = false;
            }
            GHClass.Para.ordonnance_ans = Convertir_texte_int(Tb_ordonnance_ans.Text);

            // autre
            if (Cb_autre_date.Checked)
                GHClass.Para.autre_date = true;
            else
                GHClass.Para.autre_date = false;
            if (Cb_autre_date.Checked)
                GHClass.Para.autre_date = true;
            else
                GHClass.Para.autre_date = false;
            GHClass.Para.autre_ans = Convertir_texte_int(Tb_autre_ans.Text);
            // citoyen
            if (Cb_citoyen_date.Checked)
                GHClass.Para.citoyen_date = true;
            else
                GHClass.Para.citoyen_date = false;
            GHClass.Para.citoyen_ans = Convertir_texte_int(Tb_citoyen_ans.Text);
            // testament
            if (Cb_testament_information.Checked)
                GHClass.Para.testament_information = true;
            else
                GHClass.Para.testament_information = false;
            GHClass.Para.testament_ans = Convertir_texte_int(Tb_testament_ans.Text);

            // si modifier affiche message
            if (recharger_HTML && GHClass.FichierGEDCOMaLire != null)
            {
                R.Afficher_message(
                    "Pour prendre effet, on doit refaire les fiches HTML.",
                    null,
                    null,
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
            }
            this.Close();
        }

        private void Btn_couleur_ap_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog
            {
                AllowFullOpen = false,
                AnyColor = true,
                SolidColorOnly = false,
                Color = Color.Red
            };

            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                Lb_code_arriere_plan.Text = colorDlg.Color.ToArgb().ToString("x8").Substring(2, 6).ToUpper();
                GHClass.Para.arriere_plan = Lb_code_arriere_plan.Text;
                Lb_couleur_arriere_plan.ForeColor = colorDlg.Color;
            }
            recharger_HTML = true;
        }

        private void Cb_tout_evenement_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
            if (Cb_tout_evenement.Checked)
                Cb_certain.Checked = false;
            else
                Cb_certain.Checked = true;
        }

        private void Cb_certain_CheckedChanged(object sender, EventArgs e)
        {
            recharger_HTML = true;
            if (Cb_certain.Checked)
                Cb_tout_evenement.Checked = false;
            else
                Cb_tout_evenement.Checked = true;
        }

        private void Btn_annuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
