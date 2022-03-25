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
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.CompilerServices;


namespace GH
{
    public partial class A_propos : Form
    {
        public A_propos()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, System.EventArgs e)
        {
            Regler_code_erreur();
            //this.Location = new Point(Properties.Settings.Default.Form1_location.X + 100, Properties.Settings.Default.Form1_location.Y + 20);
            //VersionLb.Text = "Version " +Application.ProductVersion;
            VersionLb.Text = "Version " + Application.ProductVersion;
        }

        private void PambrunLLb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Regler_code_erreur();
            System.Diagnostics.Process.Start("https://pambrun.net");
        }

        private void Btn_Fermer_Click(object sender, System.EventArgs e)
        {
            Regler_code_erreur();
            this.Close();
        }

        private void VersionLb_Click(object sender, System.EventArgs e)
        {
            Regler_code_erreur();

        }
        private static void Regler_code_erreur([CallerLineNumber] int sourceLineNumber = 0)
        {
            GH.GHClass.erreur = "AP" + sourceLineNumber;
        }

        private void Tb_text_TextChanged(object sender, System.EventArgs e)
        {
            Regler_code_erreur();

        }

        private void Btn_fermer_Click_1(object sender, System.EventArgs e)
        {
            Regler_code_erreur();
            this.Close();
        }
    }
}
