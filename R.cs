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
using GEDCOM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Routine
{
    internal class R
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message_1"></param>
        /// <param name="message_2"></param>
        /// <param name="erreur"></param>
        /// <param name="bouton "></param>
        /// <param name="icon"></param>
        /// <returns>reponse</returns>
        public static DialogResult Afficher_message(
            string message_1,
            string message_2 = null,
            string erreur = null,
            string caption = null,
            MessageBoxButtons bouton = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.Information
            )
        {
            DialogResult reponse;
            if (erreur != null)
            {
                string message_log = String.Format("Erreur {0,-7 } {1} {2,-15 }{3}", erreur, message_1, message_2, "");
                Erreur_log(message_log);
            }
            string message;
            if (erreur != null)
            {
                caption = "Erreur " + erreur;
            }
            else if (message_2 == null && erreur == null && bouton == MessageBoxButtons.OK && icon == MessageBoxIcon.Information)
            {
                caption = "Information";
                icon = MessageBoxIcon.Information;
                bouton = MessageBoxButtons.OK;
            }

            message = message_1;
            if (message_2 != null)
                message += "\n\r\n\r" + message_2;

            reponse = MessageBox.Show(
                message,
                caption,
                bouton,
                icon);
            return reponse;
        }
        public static string Avoir_vignette(string FORM)
        {
            FORM = Path.GetExtension(FORM.ToLower());
            string s = "vignette.svg";
            if (!R.IsNullOrEmpty(FORM))
            {
                FORM = FORM.ToLower();
                switch (FORM)
                {
                    case "jpg":
                    case ".jpg":
                        s = "jpg.svg";
                        break;
                    case "jepg":
                    case ".jepg":
                        s = "jpeg.svg";
                        break;
                    case "gif":
                    case ".gif":
                        s = "gif.svg";
                        break;
                    case "svg":
                    case ".svg":
                        s = "svg.svg";
                        break;
                    case "png":
                    case ".png":
                        s = "png.svg";
                        break;
                    case "tif":
                    case ".tif":
                        s = "tif.svg";
                        break;
                    case "tiff":
                    case ".tiff":
                        s = "tiff.svg";
                        break;
                    case "pcx":
                    case ".pcx":
                        s = "pcx.svg";
                        break;
                    case "pic":
                    case ".pic":
                        s = "pic.svg";
                        break;
                    case "pict":
                    case ".pict":
                        s = "pict.svg";
                        break;
                    case "pct":
                    case ".pct":
                        s = "pct.svg";
                        break;
                    case "mac":
                    case ".mac":
                        s = "pct.svg";
                        break;
                    case "aif":
                    case ".aif":
                        s = "aif.svg";
                        break;
                    case "psd":
                    case ".psd":
                        s = "psd.svg";
                        break;
                    case "tga":
                    case ".tga":
                        s = "tga.svg";
                        break;
                }
            }
            return s;
        }

        public static bool IsNotNullOrEmpty(string s
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + " à <b>" + Avoir_nom_method() + "</b>");
            // verifie si la string n'est par null ou vide
            // retourne true si pas vide ou vide ou null
            if (s == null || s == "")
            {
                return false;
            }
            return true;
        }
        public static void Erreur_log(
            string message = "")
        {
            Regler_code_erreur();

            System.Windows.Forms.Button Btn_erreur = Application.OpenForms["GHClass"].Controls["Btn_erreur"] as System.Windows.Forms.Button;
            Btn_erreur.Visible = true;
            Application.DoEvents();
            try
            {
                if (!File.Exists(GH.GHClass.fichier_erreur))
                {
                    using (StreamWriter ligne = File.AppendText(GH.GHClass.fichier_erreur))
                    {
                        ligne.WriteLine(
                            "<!DOCTYPE html>\n" +
                            "    <html lang=\"fr\" style=\"background-color:#FFF;\">\n" +
                            "        <head>\n" +
                            "           <title>Erreur</title>" +
                            "           <style>\n" +
                            "               h1{color:#00F}\n" +
                            "               .col0{width:150px;vertical-align:top;}\n " +
                            "               .col1{width:90px;vertical-align:top;}\n " +
                            "               .col2{width:50px;vertical-align:top;}\n " +
                            "               .col3{width:330px;vertical-align:top;}\n " +
                            "               .col4{vertical-align:top;}\n " +
                            "               .navbar {\n" +
                            "                   overflow: hidden;\n" +
                            "                   position: fixed;\n" +
                            "                   top: 0;\n" +
                            "                   background-color: #FFF;\n" +
                            "                   width: 100%;\n" +
                            "               }\n" +
                            "           </style>\n" +
                            "        </head>");
                        ligne.WriteLine("<div class=\"navbar\">");
                        ligne.WriteLine("<h1>Erreur</h1>");
                        ligne.WriteLine("<table style=\"border:2px solid #000;width:100%\">");
                        ligne.WriteLine("\t<tr><td style=\"width:150px\">Nom</td><td>" + GEDCOMClass.info_HEADER.N2_SOUR_NAME + "</td><td></tr>");
                        ligne.WriteLine("\t<tr><td>Version</td><td>" + GEDCOMClass.info_HEADER.N2_SOUR_VERS + "<td></tr>");
                        ligne.WriteLine("\t<tr><td>Date</td><td>" + GEDCOMClass.info_HEADER.N1_DATE + " " + GEDCOMClass.info_HEADER.N2_DATE_TIME + "<td></tr>");
                        ligne.WriteLine("\t<tr><td>Copyright</td><td>" + GEDCOMClass.info_HEADER.N1_COPR + "<td></tr>");
                        ligne.WriteLine("\t<tr><td>Version</td><td>" + GEDCOMClass.info_HEADER.N2_GEDC_VERS + "<td></tr>");
                        ligne.WriteLine("\t<tr><td>Code charactère</td><td>" + GEDCOMClass.info_HEADER.N1_CHAR + "<td></tr>");
                        ligne.WriteLine("\t<tr><td>Langue</td><td>" + GEDCOMClass.info_HEADER.N1_LANG + "<td></tr>");
                        ligne.WriteLine("\t<tr><td>Fichier sur le disque</td><td>" + GEDCOMClass.info_HEADER.Nom_fichier_disque + "<td></tr>");
                        System.Version version;
                        try
                        {
                            version = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                        }
                        catch
                        {
                            GC.Collect();
                            version = Assembly.GetExecutingAssembly().GetName().Version;
                        }
                        ligne.WriteLine("\t<tr><td>Version de GH</td><td>" + version.Major + "." + version.Minor + "." + version.Build + "<td></tr>");
                        ligne.WriteLine("</table>");
                        ligne.WriteLine(
                            "<table>" +
                            "<tr>" +
                            "<td class=\"col0\">" +
                            "Date heure" +
                            "</td>" +
                            "<td>" +
                            "Message" +
                            "</td>" +
                            "</table>" +
                            "<hr>");
                        ligne.WriteLine("</div>");
                        ligne.WriteLine("<div style=\"margin-top: 325px;\">\n");
                        ligne.WriteLine("</div>\n");

                    }
                }
                using (StreamWriter ligne = File.AppendText(GH.GHClass.fichier_erreur))
                {

                    string s = String.Format(
                        "<table>" +
                        "<tr>" +
                        "<td class=\"col0\">" +
                        "{0}" +
                        "</td>" +
                        "<td>" +
                        "{1}" +
                        "</td>" +
                        "</tr>" +
                        "</table>" +
                        "<hr>",
                        DateTime.Now, message);
                    ligne.WriteLine(s);
                }
            }
            catch (Exception msg)
            {
                GC.Collect();
                Afficher_message(
                    message,
                    msg.Message,
                    GH.GHClass.erreur,
                    null,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
        }
        public static bool IsNotNullOrEmpty(List<string> liste
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + " à <b>" + Avoir_nom_method() + "</b>");
            if (liste == null)
                return false;
            foreach (string a in liste)
            {
                if (!String.IsNullOrWhiteSpace(a))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsNotNullOrEmpty<T>(
            List<T> list
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + " à <b>" + Avoir_nom_method() + "</b>");
            // verifie si une liste est null ou vide
            // retourne true si n'est pas vide ou null
            if (list == null)
                return false;
            if (list.Count == 0)
                return false;
            return list.Any();
        }

        public static bool IsNullOrEmpty(string s)
        {
            // verifie si s est null ou vide
            // retourne true si vide ou null
            if (s == null)
            {
                return true;
            }
            return !s.Any();
        }
        public static bool IsNullOrEmpty(List<string> liste)
        {
            // verifie si une liste de s est null ou vide
            // retourne true si vide ou null
            if (liste == null)
            {
                return true;
            }
            return !liste.Any();
        }

        public static bool IsNullOrEmpty<T>(List<T> list)
        {
            // verifie si une liste est null ou vide
            // retourne true si vide ou null
            if (list == null)
            {
                return true;
            }
            return !list.Any();
        }
        private static void Regler_code_erreur([CallerLineNumber] int sourceLineNumber = 0)
        {
            GH.GHClass.erreur = "R" + sourceLineNumber;
        }

        public static void Z(
            object message = null,
            [CallerFilePath] string code = "",
            [CallerLineNumber] int ligneCode = 0,
            [CallerMemberName] string fonction = null
            )
        {
            Regler_code_erreur();
            if (!GH.GHClass.Para.mode_depanage)
                return;
            System.Windows.Forms.Button Btn_debug = Application.OpenForms["GHClass"].Controls["Btn_debug"] as System.Windows.Forms.Button;
            code = Path.GetFileName(code);
            try
            {
                if (!File.Exists(GH.GHClass.fichier_deboguer))
                {
                    Z_debut();
                    Btn_debug.Visible = true;
                }
                using (StreamWriter ligne = File.AppendText(GH.GHClass.fichier_deboguer))
                {
                    string s = String.Format(
                        "<table>" +
                        "<tr>" +
                        "<td class=\"col0\">" +
                        "{0}" +
                        "</td>" +
                        "<td class=\"col1\">" +
                        "{1}" +
                        "</td>" +
                        "<td class=\"col2\">" +
                        "{2}" +
                        "</td>" +
                        "<td class=\"col3\">" +
                        "{3}" +
                        "</td>" +
                        "<td>" +
                        "{4}" +
                        "</td>" +
                        "</tr>" +
                        "</table>" +
                        "<hr>",
                        DateTime.Now, code, ligneCode, fonction, message);
                    ligne.WriteLine(s);
                }
            }
            catch (Exception msg)
            {
                GC.Collect();
                Afficher_message(
                    "Deboguer Actif dans GEDCOM.\r\n\r\n" + code + " " + ligneCode + " " + fonction + "-> " + message,
                    msg.Message,
                    GH.GHClass.erreur,
                    null,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
        }
        public static void Z_liste_string(
            List<string> info = null,
            [CallerFilePath] string code = "",
            [CallerLineNumber] int ligneCode = 0,
            [CallerMemberName] string fonction = null
            )
        {
            string texte = "Liste de string ";
            code = Path.GetFileName(code);
            System.Windows.Forms.Button Btn_debug = Application.OpenForms["GHClass"].Controls["Btn_debug"] as System.Windows.Forms.Button;
            if (info == null)
            {
                texte += "est null";
            }
            else
            {
                if (!GH.GHClass.Para.mode_depanage)
                    return;

                code = Path.GetFileName(code);
                foreach (string a in info)
                    texte += " ZLS-- " + a;
            }

            try
            {
                if (!File.Exists(GH.GHClass.fichier_deboguer))
                {
                    Z_debut();
                    Btn_debug.Visible = true;
                }
                using (StreamWriter ligne = File.AppendText(GH.GHClass.fichier_deboguer))
                {
                    string s = String.Format(
                        "<table>" +
                        "<tr>" +
                        "<td class=\"col0\">" +
                        "{0}" +
                        "</td>" +
                        "<td class=\"col1\">" +
                        "{1}" +
                        "</td>" +
                        "<td class=\"col2\">" +
                        "{2}" +
                        "</td>" +
                        "<td class=\"col3\">" +
                        "{3}" +
                        "</td>" +
                        "<td>" +
                        "{4}" +
                        "</td>" +
                        "</tr>" +
                        "</table>" +
                        "<hr>",
                        DateTime.Now, code, ligneCode, fonction, texte);
                    ligne.WriteLine(s);
                }
            }
            catch (Exception msg)
            {
                GC.Collect();
                Afficher_message(
                    "Deboguer Actif dans GEDCOM." + code + " " + ligneCode + " " + fonction,
                    msg.Message,
                    GH.GHClass.erreur,
                    null,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static void Z_debut()
        {
            Regler_code_erreur();
            return;
            try
            {
                if (!File.Exists(GH.GHClass.fichier_deboguer))
                {
                    using (StreamWriter ligne = File.AppendText(GH.GHClass.fichier_deboguer))
                    {
                        ligne.WriteLine(
                            "<!DOCTYPE html>\n" +
                            "<html lang=\"fr\" style=\"background-color:#FFF;\">\n" +
                            "    <head>\n" +
                            "       <title>Déboguer</title>\n" +
                            "       <style>\n" +
                            "           h1{color:#00F}\n" +
                            "           .col0{width:150px;vertical-align:top;}\n " +
                            "           .col1{width:90px;vertical-align:top;}\n " +
                            "           .col2{width:50px;vertical-align:top;}\n " +
                            "           .col3{width:330px;vertical-align:top;}\n " +
                            "           .col4{vertical-align:top;}\n " +
                            "           .navbar {\n" +
                            "               overflow: hidden;\n" +
                            "               position: fixed;\n" +
                            "               top: 0;\n" +
                            "               background-color: #FFF;\n" +
                            "               width: 100%;\n" +
                            "           }\n" +
                            "       </style>\n" +
                            "    </head>");
                        ligne.WriteLine("<div class=\"navbar\">");
                        ligne.WriteLine("<h1><img style=\"height:64px\" src=commun/D.svg\" alt=\"\" /> Deboguer</h1>");
                        ligne.WriteLine("<table style=\"border:2px solid #000;width:100%\">");
                        ligne.WriteLine("\t<tr><td style=\"width:150px\">Nom</td><td>" + GEDCOMClass.info_HEADER.N2_SOUR_NAME + "</td><td></tr>");
                        ligne.WriteLine("\t<tr><td>Version</td><td>" + GEDCOMClass.info_HEADER.N2_SOUR_VERS + "<td></tr>");
                        ligne.WriteLine("\t<tr><td>Date</td><td>" + GEDCOMClass.info_HEADER.N1_DATE + " " + GEDCOMClass.info_HEADER.N2_DATE_TIME + "<td></tr>");
                        ligne.WriteLine("\t<tr><td>Copyright</td><td>" + GEDCOMClass.info_HEADER.N1_COPR + "<td></tr>");
                        ligne.WriteLine("\t<tr><td>Version</td><td>" + GEDCOMClass.info_HEADER.N2_GEDC_VERS + "<td></tr>");
                        ligne.WriteLine("\t<tr><td>Code charactère</td><td>" + GEDCOMClass.info_HEADER.N1_CHAR + "<td></tr>");
                        ligne.WriteLine("\t<tr><td>Langue</td><td>" + GEDCOMClass.info_HEADER.N1_LANG + "<td></tr>");
                        ligne.WriteLine("\t<tr><td>Fichier sur le disque</td><td>" + GEDCOMClass.info_HEADER.Nom_fichier_disque + "<td></tr>");
                        System.Version version;
                        GC.Collect();
                        version = Assembly.GetExecutingAssembly().GetName().Version;
                        ligne.WriteLine("\t<tr><td>Version de GH</td><td>" + version.Major + "." + version.Minor + "." + version.Build + "<td></tr>");
                        ligne.WriteLine("</table>");
                        ligne.WriteLine(
                        "<table>" +
                        "<tr>" +
                        "<td class=\"col0\">" +
                        "Date heure" +
                        "</td>" +
                        "<td class=\"col1\">" +
                        "Code" +
                        "</td>" +
                        "<td class=\"col2\">" +
                        "Ligne" +
                        "</td>" +
                        "<td class=\"col3\">" +
                        "Routine" +
                        "</td>" +
                        "<td>" +
                        "Message" +
                        "</td>" +
                        "</table>" +
                        "<hr>");
                        ligne.WriteLine("</div>");
                        ligne.WriteLine("<div style=\"margin-top: 325px;\">\n");
                        ligne.WriteLine("</div>\n");
                    }
                }
            }
            catch (Exception msg)
            {
                GC.Collect();
                Afficher_message(
                    "Déboguer Actif dans GEDCOM.\r\n\r\n" + " " + "Ne peut écrire l'entête de Déboquer",
                    msg.Message,
                    GH.GHClass.erreur,
                    null,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }

    }
}
