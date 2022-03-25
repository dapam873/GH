
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

using Microsoft.Web.WebView2.Core;
using Routine;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace GH
{
    public partial class Page : Form
    {
        public Page()
        {
        }
        public Page(string lien, string nom)
        {
            InitializeComponent();
            this.Text = nom;
            lb_lien.Text = lien;
            InitializeBrowser();

        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
        private async void InitializeBrowser()
        {
            Regler_code_erreur();
            string nom_fichier = null;
            try
            {
                Application.DoEvents();
                int largeur = this.Width;
                int hauteur = this.Height;
                Wv_page.Width = largeur - 18;
                Wv_page.Height = hauteur - 39;
                nom_fichier = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\GH";
                {
                    var env = await CoreWebView2Environment.CreateAsync(null, nom_fichier);
                    await Wv_page.EnsureCoreWebView2Async(env);
                }

                if (Path.GetFileName(lb_lien.Text) == "aide.html")
                {
                    this.Width = 780;
                    this.Height = 500;
                    this.MinimumSize = new System.Drawing.Size(780, 500);
                    this.MaximumSize = new System.Drawing.Size(780, 2500);

                    nom_fichier = "file:///" + Application.StartupPath + "\\aide\\aide.html";
                    Wv_page.Source = new Uri(nom_fichier);
                }
                else
                {
                    Wv_page.Source = new Uri(lb_lien.Text);
                }
                Wv_page.Visible = true;
            }
            catch (Exception msg)
            {
                R.Afficher_message("Ne peut pas écrire le fichier" + nom_fichier + ".", msg.Message, GH.GHClass.erreur);
            }
        }

        private void Page_SizeChanged(object sender, EventArgs e)
        {
            Regler_code_erreur();
            int largeur = this.Width;
            int hauteur = this.Height;
            Wv_page.Width = largeur - 18;
            Wv_page.Height = hauteur - 40;
        }

        private static void Regler_code_erreur([CallerLineNumber] int sourceLineNumber = 0)
        {
            GH.GHClass.erreur = "PA" + sourceLineNumber;
        }
    }
}
