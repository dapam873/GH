using GEDCOM;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GH
{
    public partial class Parametre : Form
    {
        public Parametre()
        {
            InitializeComponent();
        }
        ToolTip t1 = new ToolTip();
        private void CbVoirID_CheckedChanged(object sender, EventArgs e)
        {
            if (CbVoirID.Checked)
            {
                Properties.Settings.Default.VoirID = true;
                CbVoirID.Checked = true;
            }
            else
            {
                Properties.Settings.Default.VoirID = false;
                CbVoirID.Checked = false;
            }
        }
        private void CbVoirPphotoPrincipal_CheckedChanged(object sender, EventArgs e)
        {
            if (CbVoirPhotoPrincipal.Checked)
            {
                Properties.Settings.Default.photo_principal = true;
                CbVoirPhotoPrincipal.Checked = true;
            }
            else
            {
                Properties.Settings.Default.photo_principal = false;
                CbVoirPhotoPrincipal.Checked = false;
            }
        }

        private void CbVoirMedia_CheckedChanged(object sender, EventArgs e)
        {
            if (CbVoirMedia.Checked)
            {
                Properties.Settings.Default.voir_media = true;
                CbVoirMedia.Checked = true;
            }
            else
            {
                Properties.Settings.Default.voir_media = false;
                CbVoirMedia.Checked = false;
            }
        }

        private void CbVoirDateChangement_CheckedChanged(object sender, EventArgs e)
        {
            if (CbVoirDateChangement.Checked)
            {
                Properties.Settings.Default.voir_date_changement = true;
                CbVoirDateChangement.Checked = true;
            }
            else
            {
                Properties.Settings.Default.voir_date_changement = false;
                CbVoirDateChangement.Checked = false;
            }
        }

        private void CbDateLongue_CheckedChanged(object sender, EventArgs e)
        {
            if (CbDateLongue.Checked)
            {
                Properties.Settings.Default.date_longue = true;
                CbDateLongue.Checked = true;
            }
            else
            {
                Properties.Settings.Default.date_longue = false;
                CbDateLongue.Checked = false;
            }
        }

        private void CbVoirReference_CheckedChanged(object sender, EventArgs e)
        {
            if (CbVoirReference.Checked)
            {
                Properties.Settings.Default.voir_reference = true;
                CbVoirReference.Checked = true;
            }
            else
            {
                Properties.Settings.Default.voir_reference = false;
                CbVoirReference.Checked = false;
            }
        }

        private void CbVoirNote_CheckedChanged(object sender, EventArgs e)
        {
            if (CbVoirNote.Checked)
            {
                Properties.Settings.Default.voir_note = true;
                CbVoirNote.Checked = true;
            }
            else
            {
                Properties.Settings.Default.voir_note = false;
                CbVoirNote.Checked = false;
            }
        }

        private void CbBalise_CheckedChanged(object sender, EventArgs e)
        {
            if (CbBalise.Checked)
            {
                Properties.Settings.Default.balise = true;
                CbBalise.Checked = true;
            }
            else
            {
                Properties.Settings.Default.balise = false;
                CbBalise.Checked = false;
            }
        }

        private void CbDeboguer_CheckedChanged(object sender, EventArgs e)
        {
            if (CbDeboguer.Checked)
            {
                Properties.Settings.Default.deboguer = true;
                CbDeboguer.Checked = true;
            }
            else
            {
                Properties.Settings.Default.deboguer = false;
                CbDeboguer.Checked = false;
            }
        }

        private void TbDossierHTML_TextChanged(object sender, EventArgs e)
        {
            if (TbDossierHTML.Text != "")
            {
                if (!Directory.Exists(TbDossierHTML.Text))
                {
                    TbDossierHTML.BackColor = Color.Red;
                    return;
                }
            }
            TbDossierHTML.BackColor = Color.White;
            Properties.Settings.Default.DossierHTML = TbDossierHTML.Text;
        }

        private void TbDossierMedia_TextChanged(object sender, EventArgs e)
        {
            if(TbDossierMedia.Text != "")
               {
                if (!Directory.Exists(TbDossierMedia.Text))
                {
                    TbDossierMedia.BackColor = Color.Red;
                    return;
                }
            }
            TbDossierMedia.BackColor = Color.White;
            Properties.Settings.Default.DossierMedia = TbDossierMedia.Text;
        }

        private void PbDossierHTML_Click(object sender, EventArgs e)
        {
            AvoirDossierPageWeb();
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
                TbDossierHTML.Text = Properties.Settings.Default.DossierHTML;
                Btn_dossierHTML.Focus();
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
                Description = "Où est le dossier de vos médias"
            };
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.DossierMedia = folderBrowserDialog1.SelectedPath;
                TbDossierMedia.Text = Properties.Settings.Default.DossierMedia;
                Btn_dossierHTML.Focus();
            }
        }

        private void Parametre_Load(object sender, EventArgs e)
        {
            TbDossierHTML.Text = Properties.Settings.Default.DossierHTML;

            TbDossierMedia.Text = Properties.Settings.Default.DossierMedia;
            if (Properties.Settings.Default.VoirID) CbVoirID.Checked = true; else CbVoirID.Checked = false;

            if (Properties.Settings.Default.photo_principal) CbVoirPhotoPrincipal.Checked = true; else CbVoirPhotoPrincipal.Checked = false;

            if (Properties.Settings.Default.voir_media) CbVoirMedia.Checked = true; else CbVoirMedia.Checked = false;

            if (Properties.Settings.Default.voir_date_changement) CbVoirDateChangement.Checked = true; else CbVoirDateChangement.Checked = false;

            if (Properties.Settings.Default.voir_reference) CbVoirReference.Checked = true; else CbVoirReference.Checked = false;
            if (Properties.Settings.Default.voir_chercheur) CbVoirChercheur.Checked = true; else CbVoirChercheur.Checked = false;
            if (Properties.Settings.Default.voir_carte) CbVoirCarte.Checked = true; else CbVoirCarte.Checked = false;

            if (Properties.Settings.Default.voir_note) CbVoirNote.Checked = true; else CbVoirNote.Checked = false;

            if (Properties.Settings.Default.date_longue) CbDateLongue.Checked = true; else CbDateLongue.Checked = false;

            if (Properties.Settings.Default.balise) CbBalise.Checked = true; else CbBalise.Checked = false;

            if (Properties.Settings.Default.deboguer) CbDeboguer.Checked = true; else CbDeboguer.Checked = false;

        }
        private void CbVoirChercheur_CheckedChanged(object sender, EventArgs e)
        {
            if (CbVoirChercheur.Checked)
            {
                Properties.Settings.Default.voir_chercheur = true;
                CbVoirChercheur.Checked = true;
            }
            else
            {
                Properties.Settings.Default.voir_chercheur = false;
                CbVoirChercheur.Checked = false;
            }
        }

        private void CbVoirCarte_CheckedChanged(object sender, EventArgs e)
        {
            if (CbVoirCarte.Checked)
            {
                Properties.Settings.Default.voir_carte = true;
                CbVoirCarte.Checked = true;
            }
            else
            {
                Properties.Settings.Default.voir_carte = false;
                CbVoirCarte.Checked = false;
            }
        }

        private void Btn_Fermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_dossierHTML_Click(object sender, EventArgs e)
        {
            AvoirDossierPageWeb();
        }

        private void Btn_dossierMedia_Click(object sender, EventArgs e)
        {
            AvoirDossierMedias();
        }

        private void Btn_dossierHTML_MouseHover(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.voir_ToolTip)
                t1.Show("Choisir le dossier HTML", Btn_dossierHTML);
        }

        private void Btn_dossierMedia_MouseHover(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.voir_ToolTip)
                t1.Show("Choisir le dossier média", Btn_dossierHTML);
        }

        private void CbToolTip_CheckedChanged(object sender, EventArgs e)
        {
            if (CbToolTip.Checked)
            {
                Properties.Settings.Default.voir_ToolTip = true;
                CbToolTip.Checked = true;
            }
            else
            {
                Properties.Settings.Default.voir_ToolTip = false;
                CbToolTip.Checked = false;
            }
        }
    }
}
