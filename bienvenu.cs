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
using System;
using System.Windows.Forms;

namespace GH
{
    public partial class Bienvenu : Form
    {
        public Bienvenu()
        {
            InitializeComponent();
        }
        private void Btn_Fermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Tb_3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Tb_text_TextChanged(object sender, EventArgs e)
        {

        }

        private void Bienvenu_Load(object sender, EventArgs e)
        {
        }

        private void Pb_fermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Ll_licence_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("GPLV3.txt");
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Btn_fermer_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
