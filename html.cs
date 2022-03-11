/* Licence français


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

/* Pour connaitre la méthode qui a appelé une metode
, [CallerLineNumber] int callerLineNumber = 0
R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
Regler_code_erreur();
*/

// texte += Convertir_texte_html(stexte, false, tab + X);
// 0.04f, // retrait
// charactères spéciaux ֍ &nbsp;
using GEDCOM;
using GH;
using Routine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


/* ordre des paramètres de méthode

        liste_ID
        sous_dossier,
        dossier_sortie,
        menu
        numero_carte,
        numero_source,
        nombre_source,
        List<ID_numero>             // liste_SUBMITTER_ID_numero,
        List<MULTIMEDIA_ID_numero>  // liste_MULTIMEDIA_ID_numero,
        List<ID_numero>             // liste_citation_ID_numero,
        List<ID_numero>             // liste_source_ID_numero,
        List<ID_numero>             // liste_repo_ID_numero,
        List<ID_numero>             // liste_note_STRUCTURE_ID_numero,
        List<ID_numero>             // liste_NOTE_RECORD_ID_numero,
        modifier,
        retrait
        tab
 */
namespace HTML
{
    public class HTMLClass
    {
        public const string BLINK0 = "<span class=\"blink1\">" + "֍" + "</span>"; // ֍ trouve pas info
        // liste de reference ID_numero
        private class ID_numero
        {
            public string ID;
            public int numero; // numero assigné 

        }

        // liste de reference ID_numero MULTIMEDIA seulement
        private class MULTIMEDIA_ID_numero
        {
            public string ID_LINK; // ID de link
            public string ID_RECORD; // ID de record inclu dans le link
        }
        private static string Avoir_texte_association(
            List<GEDCOMClass.ASSOCIATION_STRUCTURE> associalion_liste,
            bool menu,
            List<ID_numero> liste_SUBMITTER_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab
            )
        {
            if (associalion_liste == null)
                return null;
            string texte = null;
            string espace = Tabulation(tab);
            foreach (GEDCOMClass.ASSOCIATION_STRUCTURE info_ASSO in associalion_liste)
            {
                // si c'est un individu.
                if (GEDCOMClass.Si_individu(info_ASSO.N0_ASSO))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += "\t\t\t\t\t\tAssociation\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    if (menu)
                    {
                        texte += espace + "\t\t\t<a class=\"ficheIndividu\"  href=\"" + info_ASSO.N0_ASSO + ".html\"></a>\n";
                    }
                    else
                    {
                        texte += espace + "\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                    }
                    texte += espace + "\t\t\t" + GEDCOMClass.Avoir_premier_nom_individu(info_ASSO.N0_ASSO);
                    if (GH.GHClass.Para.voir_ID == true)
                    {
                        texte += "[" + info_ASSO.N0_ASSO + "]";
                    }
                    texte += "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }

                // ASSO
                else if (GEDCOMClass.Si_chercheur(info_ASSO.N0_ASSO))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tAssociation\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    List<string> liste_ID = new List<string>
                        {
                            info_ASSO.N0_ASSO
                        };
                    texte += Avoir_texte_lien_chercheur(liste_ID, liste_SUBMITTER_ID_numero, 6);
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                if (info_ASSO.N1_TYPE != null)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\t\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\tType: " + info_ASSO.N1_TYPE + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                if (info_ASSO.N1_RELA != null)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\t\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "Relation: ";
                    texte += Convertir_texte_html(info_ASSO.N1_RELA, false, tab + 3);
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // citation association
                if (info_ASSO.N1_SOUR_citation_liste_ID.Count > 0)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\t&nbsp;\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    List<string> listeLien = new List<string>();
                    texte += Avoir_texte_lien_citation(
                        info_ASSO.N1_SOUR_citation_liste_ID,
                        liste_citation_ID_numero,
                        0, 5);
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // source association
                if (info_ASSO.N1_SOUR_source_liste_ID.Count > 0)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\t&nbsp;\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    List<string> listeLien = new List<string>();
                    texte += Avoir_texte_lien_source(
                        info_ASSO.N1_SOUR_source_liste_ID,
                        liste_source_ID_numero,
                        0, 5);
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }

                // association note
                if ((info_ASSO.N1_NOTE_STRUCTURE_liste_ID.Count > 0))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\t&nbsp;\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += Avoir_texte_NOTE_STRUCTURE(
                        info_ASSO.N1_NOTE_STRUCTURE_liste_ID,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        0, // retrait
                        tab + 5);
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
            }
            return texte;
        }
        public static string Avoir_texte_age_evenenemt(
            string age
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + "<br> age =" + age);
            Regler_code_erreur();
            string texte = null;
            /*
            V5.3 p.
                A number that indicates the age in years, months, and/or days. Any labels must come after
                their corresponding number, for example; 4 yr 8 mo 10 da. The year is required, and
                listed first, even if it is 0 (zero).

            V5.4 p.34 V5.5 p.37 V5.5.1 p.42
                [ < | > | <NULL>]
                [ YYy MMm DDDd | YYy | MMm | DDDd |
                YYy MMm | YYy DDDd | MMm DDDd |
                CHILD | INFANT | STILLBORN ]
                ]
                Where:
                > = greater than indicated age
                < = less than indicated age
                y = a label indicating years
                m = a label indicating months
                d = a label indicating days
                YY = number of full years
                MM = number of months
                DDD = number of days
                CHILD = age < 8 years
                INFANT = age < 1 year
                STILLBORN = died just prior, at, or near birth, 0 years 
            
            V5.5.1 p.77
                [ NULL
                | < + space
                | > + space ]
                ]
                [ YYY + y + space + MM + m + space + DDD + d
                | YYY + y
                | MM + m
                | DDD + d
                | YYY + y + space + MM + m
                | YYY+ y + space + DDD + d
                | MM + m + space + DDD + d
                | CHILD
                | INFANT
                | STILLBORN
                ]
                where:
                > = greater than indicated age
                < = less than indicated age
                y = a label indicating years
                m = a label indicating months
                d = a label indicating days
                YYY = number of full years, at most three digits
                MM = number of months, at most 11, at most two digits
                DDD = number of days, at most 365, at most three digits
                CHILD = age < 8 years
                INFANT = age < 1 year
                STILLBORN = died just prior, at, or near birth, 0 years
                space = U+0020, the Space character
                Notice that, in the above syntax, the uppercase letters represents a number, while the
                lowercase letters are to be included literally, as part of the line value.
                A number that indicates the age in years, months, and days that the principal was at the time
                of the associated event. Any labels must come after their corresponding number, for
                example; 4y 8m 10d.
                The line value should be normalised; it should for example not specify 2y 13m, but 3y 1m
                instead. Number of days is allowed to be 365 because of leap years.
                The YYY, MM and DDD values must not be zero; if a value equals zero, that part is left off.
                The values may not contain leading zeroes either.
                The values CHILD, INFANT, STILLBORN have been deprecated because they break the
                mold, are technically superflous, and not likely to be understood as precise as they are
                defined here. They continue to be allowed for compatability with GEDCOM 5.5.1 only.
                A notable shortcoming of the AGE_AT_EVENT syntax it that it does not support hours or
                minutes. Recording that a child died minutes or hours after birth is not supported.
             */
            if (R.IsNullOrEmpty(age))
            {
                return null;
            }
            age = age.ToLower();
            if (age == "child")
            {
                //R..Z("<b>Retourne</b> moins de 8 ans");
                return "moins de 8 ans";
            }
            if (age == "infant")
            {
                //R..Z("<b>Retourne</b> moins de 1 ans");
                return "moins de 1 ans";
            }
            if (age == "stillborn")
            {
                //R..Z("<b>Retourne</b> Décédé juste avant, ou après la naissance, 0 ans");
                return "Décédé juste avant, ou après la naissance, 0 ans";
            }

            age = age.Replace("mo", " mois ");
            age = age.Replace(">", "de plus de ");
            age = age.Replace("<", "de moins de ");
            age = age.Replace("yr", " ans ");
            age = age.Replace("da", " jours ");
            string[] section = age.Split(' ');
            if (section.Count() == 1)
            {
                if (int.TryParse(age, out int a))
                {
                    //R..Z("<b>Retourne</b> " + a.ToString() + " ans\n");
                    return a.ToString() + " ans";
                }
            }
            for (int f = 0; f < section.Count(); f++)
            {
                if (section[f] == "y")
                    section[f] = "ans";
                if (section[f] == "m")
                    section[f] = "mois";
                if (section[f] == "d")
                    section[f] = "jours";
                // si la setion a chiffre et charactère
                if (Regex.IsMatch(section[f], "[a-z]") && Regex.IsMatch(section[f], "[0-9]"))
                {
                    section[f] = section[f].Replace("y", " ans ");
                    section[f] = section[f].Replace("m", " mois ");
                    section[f] = section[f].Replace("d", " jours ");
                }
            }
            // si un jour seulement retirer le s
            if (section[section.Count() - 1] == "jours")
            {
                int.TryParse(section[section.Count() - 2], out int jour);
                if (jour == 1)
                    section[section.Count()] = "jour";
            }
            for (int f = 0; f < section.Count(); f++)
            {
                texte += section[f] + " ";
            }
            //R..Z("<b>Retourne</b> " + texte);
            return texte;
        }

        private static (
            string,          // texte
            List<MULTIMEDIA_ID_numero>, // 
            List<ID_numero>, // liste_citation_ID_numer
            List<ID_numero>, // liste_source_ID_numero
            List<ID_numero>, // liste_repo_ID_numer
            List<ID_numero>, // liste_note_STRUCTURE_ID_numero,
            List<ID_numero>  // liste_NOTE_RECORD_ID_numer
            ) Avoir_texte_MULTIMEDIA(
            List<string> liste_ID,
            string sous_dossier,
            string dossier_sortie,
            List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            bool marge,
            int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + "<br>marge="+ marge.ToString()+ "<br>tab="+ tab.ToString());
            //string z = "liste_ID ";foreach (string ZZ in liste_ID) z += "<br>&nbsp;&nbsp;&nbsp;&nbsp;" + ZZ;R..Z(z);
            if (!GH.GHClass.Para.voir_media || liste_ID == null || liste_MULTIMEDIA_ID_numero == null)
                return (
                    null,
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero
                    );
            string espace = Tabulation(tab);
            string origine = @"../";
            if (sous_dossier == "")
                origine = "";
            string texte = null;
            foreach (string ID in liste_ID)
            {
                GEDCOMClass.MULTIMEDIA_LINK info_LINK = GEDCOMClass.Avoir_info_MULTIMEDIA_LINK(ID);
                Application.DoEvents();
                // MULTIMEDIA_LINK
                if (info_LINK != null)
                    if (!info_LINK.ID_seul)
                    {
                        if (marge)
                        {
                            texte += espace + "<table class=\"multimedia_m\">\n";
                        }
                        else
                        {

                            texte += espace + "<table class=\"multimedia\">\n";
                        }
                        texte += espace + "\t<tr>\n";
                        texte += espace + "\t\t<td width=160px>\n";
                        if (info_LINK.FILE != null)
                        {
                            string fichier = CopierObjet(info_LINK.FILE, sous_dossier, dossier_sortie);
                            if (fichier == null)
                            {
                                fichier = "media_manquant.svg";
                            }
                            string extention = Path.GetExtension(info_LINK.FILE.ToLower());
                            if (extention != null)
                            {
                                // image jpg jpeg gif bmp svg png
                                if (extention == ".jpg" ||
                                    extention == ".jpeg" ||
                                    extention == ".gif" ||
                                    extention == ".bmp" ||
                                    extention == ".svg" ||
                                    extention == ".png"
                                )
                                {
                                    if (fichier == "media_manquant.svg")
                                    {
                                        texte += espace + "\t\t\t<img class=\"mini2\" src=\"" + origine +
                                                "commun\\media_manquant.svg\" alt=\"\" />\n";
                                    }
                                    else if (sous_dossier == "commun")
                                    {
                                        texte += espace + "\t\t\t<img class=\"mini\" src=\"" + origine +
                                                @"commun/" + fichier + "\" alt=\"\" />\n";
                                    }
                                    else
                                    {
                                        texte += espace + "\t\t\t<img class=\"mini\" src=\"" + @"medias/" +
                                                fichier + "\" alt=\"\" />\n";
                                    }

                                }
                                // Image tif, pic et pcx doivent être télécharger.
                                else if (
                                        extention == ".tif" ||
                                        extention == ".tiff" ||
                                        extention == ".pic" ||
                                        extention == ".pcx" ||
                                        extention == ".pic" ||  // Macintosh
                                        extention == ".pict" || // Macintosh
                                        extention == ".pct" ||  // Macintosh
                                        extention == ".mac" ||  // Macintosh
                                        extention == ".aif" ||  // Macintosh
                                        extention == ".psd" ||  // photoshop
                                        extention == ".tga"
                                        )
                                {
                                    string vignette = R.Avoir_vignette(fichier);
                                    if (fichier != "" && sous_dossier != "commun" && sous_dossier != "")
                                    {
                                        texte += espace + "\t\t\t<a href=\"medias/" + fichier +
                                                "\" target=\"_BLANK\">\n";
                                        texte += espace + "\t\t\t\t<img class=\"mini_150\" src=\"" + origine +
                                                "commun/" + vignette + "\" alt=\"\" />\n";
                                        texte += espace + "\t\t\t</a>\n";
                                    }
                                    else if ((sous_dossier == "commun" || sous_dossier == "") && fichier != "")
                                    {
                                        texte += espace + "\t\t\t<a href=\"" + origine + "medias/" + fichier +
                                                "\" >\n";
                                        texte += espace + "\t\t\t\t<img class=\"mini_150\" src=\"" + origine +
                                                "commun/" + vignette + "\" alt=\"\" />\n";
                                        texte += espace + "\t\t\t</a>\n";
                                    }
                                }
                                // audio audio
                                else if (
                                    extention == ".aac" ||
                                    extention == ".flac" ||
                                    extention == ".wav" ||
                                    extention == ".mp3"
                                    )
                                {
                                    if (fichier == "media_manquant.svg")
                                    {
                                        texte += espace + "\t\t\t<img class=\"mini2\" src=\"" + origine +
                                                "commun\\media_manquant.svg\" alt=\"\" />\n";
                                    }
                                    else
                                    {

                                        texte += espace + "\t\t\t<a href=\"medias/" + fichier +
                                            "\" target=\"_BLANK\">\n";
                                        texte += espace + "\t\t\t\t<img class=\"mini_150\" src=\"" + origine +
                                                "commun/audio.svg\" alt=\"\" />\n";
                                        texte += espace + "\t\t\t</a>\n";
                                    }
                                }
                                // video audio
                                else if (
                                    extention == ".mov" ||
                                    extention == ".avi" ||
                                    extention == ".mkv" ||
                                    extention == ".mpg"
                                    )
                                {
                                    if (fichier == "media_manquant.svg")
                                    {
                                        texte += espace + "\t\t\t<img class=\"mini2\" src=\"" + origine +
                                                "commun\\media_manquant.svg\" alt=\"\" />\n";
                                    }
                                    else
                                    {

                                        texte += espace + "\t\t\t<a href=\"medias/" + fichier +
                                            "\" target=\"_BLANK\">\n";
                                        texte += espace + "\t\t\t\t<img class=\"mini_150\" src=\"" +
                                                origine + "commun/video.svg\" alt=\"\" />\n";
                                        texte += espace + "\t\t\t</a>\n";
                                    }
                                }
                                // pdf
                                else if (extention == ".pdf")
                                {
                                    if (fichier == "media_manquant.svg")
                                    {
                                        texte += espace + "\t\t\t<img class=\"mini2\" src=\"" + origine +
                                                "commun\\media_manquant.svg\" alt=\"\" />\n";
                                    }
                                    else
                                    {
                                        texte += espace + "\t\t\t<a href=\"" + origine + "medias/" + fichier +
                                                "\" target=\"_BLANK\">\n";
                                        texte += espace + "\t\t\t\t<img class=\"mini_150\" src=\"" + origine +
                                                "commun/pdf.svg\" alt=\"\" />\n";
                                        texte += espace + "\t\t\t</a>\n";
                                    }
                                }
                                // blob
                                else if (extention == ".blob")
                                {
                                    texte += espace + "\t\t\t<img class=\"mini2\" src=\"" + origine +
                                            "commun/blob.svg\" alt=\"\" />\n";
                                }
                                // tout les autres avec extention
                                else
                                {
                                    if (fichier == "media_manquant.svg")
                                    {
                                        texte += espace + "\t\t\t<img class=\"mini2\" src=\"" + origine +
                                                "commun\\media_manquant.svg\" alt=\"\" />\n";
                                    }
                                    else
                                    {
                                        texte += espace + "\t\t\t<a class=\"media\" href=\"" + "medias/" + fichier + "\" target=\"_BLANK\">\n";
                                        texte += espace + "\t\t\t\t<img class=\"mini_150\" src=\"" + origine +
                                                "commun/doc.svg\" alt=\"\" />\n";
                                        texte += espace + "\t\t\t</a>\n";
                                    }
                                }
                            }
                        }
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t<td width=700px>\n";
                        // ID
                        if (GH.GHClass.Para.voir_ID && GH.GHClass.Para.mode_depanage)
                        {
                            texte += espace + "\t<div style=\"text-align:left\">\n";
                            texte += espace + "\t\t[" + info_LINK.ID_LINK + "]\n";
                            texte += espace + "\t</div>\n";
                        }
                        // titre
                        if (info_LINK.TITL != null)
                        {
                            texte += espace + "\t<div style=\"text-align:left\">\n";
                            texte += Convertir_texte_html(info_LINK.TITL, false, tab + 2);
                            texte += espace + "\t</div>\n";
                        }
                        // fichier
                        if (info_LINK.FILE != null)
                        {

                            texte += espace + "\t<div class=\"tableau_ligne\">\n";
                            texte += espace + "\t\t<div>\n";
                            texte += espace + "\t\tFichier:";
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                            texte += espace + info_LINK.FILE + "\n";
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t</div>\n";
                        }

                        // FORM
                        if (info_LINK.FORM != null)
                        {
                            texte += espace + "\t<div style=\"text-align:left\">\n";
                            texte += espace + "\t\tExtention: " + info_LINK.FORM + "\n";
                            texte += espace + "\t</div>\n";
                        }

                        // MEDI
                        if (info_LINK.MEDI != null)
                        {
                            texte += espace + "\t<div style=\"text-align:left\">\n";
                            texte += espace + "\t\tType de média: " + Convertir_mot_anglais(info_LINK.MEDI) + "\n";
                            texte += espace + "\t</div>\n";
                        }

                        // LINK note
                        texte += Avoir_texte_NOTE_STRUCTURE(
                            info_LINK.NOTE_STRUCTURE_liste_ID,
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero,
                            0, // retrait
                            tab + 2);


                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                        texte += espace + "</table>\n";
                    }

                // MULTIMEDIA_RECORD
                if (info_LINK != null)
                {
                    GEDCOMClass.MULTIMEDIA_RECORD info_RECORD = GEDCOMClass.Avoir_info_MULTIMEDIA_RECORD(info_LINK.ID_RECORD);
                    if (info_RECORD != null)
                    {
                        if (marge)
                        {
                            texte += espace + "<table class=\"multimedia_m\">\n";
                        }
                        else
                        {
                            texte += espace + "<table class=\"multimedia\">\n";
                        }
                        texte += espace + "\t<tr>\n";
                        texte += espace + "\t\t<td width=160px>\n";
                        if (info_RECORD.FILE != null)
                        {
                            string fichier = CopierObjet(
                                info_RECORD.FILE, sous_dossier, dossier_sortie);
                            if (info_RECORD.FILE == "enregistrement_manquant.svg")
                            {
                                fichier = "enregistrement_manquant.svg";
                            }
                            if (fichier == null)
                            {
                                fichier = "media_manquant.svg";
                            }
                            string extention = Path.GetExtension(info_RECORD.FILE.ToLower());
                            if (extention != null)
                            {
                                // image jpg jpeg gif bmp svg png
                                if (extention == ".jpg" ||
                                    extention == ".jpeg" ||
                                    extention == ".gif" ||
                                    extention == ".bmp" ||
                                    extention == ".svg" ||
                                    extention == ".png"
                                )
                                {
                                    if (fichier == "media_manquant.svg")
                                    {
                                        texte += espace + "\t\t\t<img class=\"mini2\" src=\"" +
                                            origine + "commun\\media_manquant.svg\" alt=\"\" />\n";
                                    }
                                    else if (fichier == "enregistrement_manquant.svg")
                                    {
                                        texte += espace + "\t\t\t<img class=\"mini\" src=\"" +
                                            origine + @"commun/" + fichier + "\" alt=\"\" />\n";
                                    }
                                    else if (sous_dossier == "commun")
                                    {
                                        texte += espace + "\t\t\t<img class=\"mini\" src=\"" +
                                            origine + @"commun/" + fichier + "\" alt=\"\" />\n";
                                    }
                                    else
                                    {
                                        texte += espace + "\t\t\t<img class=\"mini\" src=\"" + @"medias/" +
                                            fichier + "\" alt=\"\" />\n";
                                    }
                                }
                                // Image tif, pic et pcx doivent être télécharger.
                                else if (
                                        extention == ".tif" ||
                                        extention == ".tiff" ||
                                        extention == ".pic" ||
                                        extention == ".pcx" ||
                                        extention == ".pic" ||  // Macintosh
                                        extention == ".pict" || // Macintosh
                                        extention == ".pct" ||  // Macintosh
                                        extention == ".mac" ||  // Macintosh
                                        extention == ".aif" ||  // Macintosh
                                        extention == ".psd" ||  // photoshop
                                        extention == ".tga"
                                        )
                                {
                                    string vignette = R.Avoir_vignette(fichier);
                                    if (fichier != "" && sous_dossier != "commun" && sous_dossier != "")
                                    {
                                        texte += espace + "\t<a href=\"medias/" + fichier + "\" target=\"_BLANK\">\n";
                                        texte += espace + "\t\t<img class=\"mini_150\" src=\"" + origine + "commun/" + vignette + "\" alt=\"\" />\n";
                                        texte += espace + "\t</a>\n";
                                    }
                                    else if ((
                                        sous_dossier == "commun" ||
                                        sous_dossier == "") &&
                                        fichier != "")
                                    {
                                        texte += espace + "\t<a href=\"" + origine + "medias/" +
                                            fichier + "\" >\n";
                                        texte += espace + "\t\t<img class=\"mini_150\" src=\"" +
                                            origine + "commun/" + vignette + "\" alt=\"\" />\n";
                                        texte += espace + "\t</a>\n";
                                    }
                                }
                                // audio audio
                                else if (
                                    extention == ".aac" ||
                                    extention == ".flac" ||
                                    extention == ".wav" ||
                                    extention == ".mp3"
                                    )
                                {
                                    texte += espace + "\t<a href=\"medias/" + fichier +
                                        "\" target=\"_BLANK\">\n";
                                    texte += espace + "\t\t<img class=\"mini_150\" src=\"" +
                                        origine + "commun/audio.svg\" alt=\"\" />\n";
                                    texte += espace + "\t</a>\n";
                                }
                                // video audio
                                else if (
                                    extention == ".mov" ||
                                    extention == ".avi" ||
                                    extention == ".mkv" ||
                                    extention == ".mpg"
                                    )
                                {
                                    texte += espace + "\t\t\t<a href=\"medias/" + fichier +
                                        "\" target=\"_BLANK\">\n";
                                    texte += espace + "\t\t\t\t<img class=\"mini_150\" src=\"" +
                                        origine + "commun/video.svg\" alt=\"\" />\n";
                                    texte += espace + "\t\t\t</a>\n";
                                }
                                // pdf
                                else if (extention == ".pdf")
                                {
                                    if (fichier == "media_manquant.svg")
                                    {
                                        texte += espace + "\t\t\t<img class=\"mini2\" src=\"" +
                                            origine + "commun\\media_manquant.svg\" alt=\"\" />\n";
                                    }
                                    else
                                    {
                                        texte += espace + "\t\t\t<a href=\"" + origine + "medias/" +
                                            fichier + "\" target=\"_BLANK\">\n";
                                        texte += espace + "\t\t\t\t<img class=\"mini_150\" src=\"" +
                                            origine + "commun/pdf.svg\" alt=\"\" />\n";
                                        texte += espace + "\t\t\t</a>\n";
                                    }
                                }
                                // blob
                                else if (extention == ".blob")
                                {
                                    texte += espace + "\t\t\t<img class=\"mini2\" src=\"" + origine +
                                        "commun/blob.svg\" alt=\"\" />\n";
                                }
                                // tout les autres avec extention
                                else
                                {
                                    texte += espace + "\t\t\t<a class=\"media\" href=\"" + "medias/" +
                                        fichier + "\" target=\"_BLANK\">\n";
                                    texte += espace + "\t\t\t\t<img class=\"mini_150\" src=\"" + origine +
                                        "commun/doc.svg\" alt=\"\" />\n";
                                    texte += espace + "\t\t\t</a>\n";
                                }
                            }
                        }
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t<td width=700px>\n";
                        // ID
                        if (GH.GHClass.Para.voir_ID)
                        {
                            texte += espace + "\t<div style=\"\">\n";
                            texte += espace + "\t\t[" + info_RECORD.ID_RECORD + "]\n";
                            texte += espace + "\t</div>\n";
                        }
                        // titre
                        if (info_RECORD.TITL != null)
                        {
                            texte += espace + "\t<div style=\"text-align:left\">\n";
                            texte += Convertir_texte_html(info_RECORD.TITL, false, tab + 2);
                            texte += espace + "\t</div>\n";
                        }
                        // fichier
                        if (
                            info_RECORD.FILE != null &&
                            info_RECORD.FILE != "enregistrement_manquant.svg")
                        {
                            texte += espace + "\t<div class=\"tableau_ligne\">\n";
                            texte += espace + "\t\t<div>\n";
                            texte += espace + "\t\tFichier:";
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                            texte += espace + info_RECORD.FILE + "\n";
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t</div>\n";
                        }

                        // FORM
                        if (info_RECORD.FORM != null)
                        {
                            texte += espace + "\t<div style=\"text-align:left\">\n";
                            texte += espace + "\t\tExtention: " + info_RECORD.FORM + "\n";
                            texte += espace + "\t</div>\n";
                            // type
                            if (info_RECORD.FORM != null)
                            {
                                texte += espace + "\t<div style=\"text-align:left\">\n";
                                texte += espace + "\t\tType de média: " + info_RECORD.FORM_TYPE + "\n";
                                texte += espace + "\t</div>\n";
                            }
                        }

                        // RECORD REFM
                        if (info_RECORD.REFN_liste != null)
                        {
                            foreach (GEDCOMClass.USER_REFERENCE_NUMBER info_REFN in info_RECORD.REFN_liste)
                            {
                                texte += espace + "\t<div colspan=2 style=\"text-align:left\">\n";
                                texte += espace + "\t\tREFN: " + info_REFN.N0_REFN + " Type: " + info_REFN.N1_TYPE + "\n";
                                texte += espace + "\t</div>\n";
                            }
                        }

                        // RECORD RIN
                        if (info_RECORD.RIN != null)
                        {
                            texte += espace + "\t<div colspan=2 style=\"text-align:left\">\n";
                            texte += espace + "\t\tRIN: " + info_RECORD.RIN + "\n";
                            texte += espace + "\t</div>\n";
                        }

                        // RECORD DATE Heridis
                        (string date, _) = Convertir_date(info_RECORD.Herisis_DATE, GH.GHClass.Para.date_longue);
                        if (date != null)
                        {
                            texte += espace + "\t<div style=\"text-align:left\">\n";
                            texte += espace + "\t\t" + date + "\n";
                            texte += espace + "\t</div>\n";
                        }

                        // citation
                        if (info_RECORD.SOUR_citation_liste_ID != null)
                        {
                            texte += espace + "\t<div style=\"text-align:left\">\n";
                            texte += Avoir_texte_lien_citation(
                                info_RECORD.SOUR_citation_liste_ID,
                                liste_citation_ID_numero,
                                0,
                                tab + 2);
                            texte += espace + "\t</div>\n";
                        }

                        // record
                        if (info_RECORD.SOUR_source_liste_ID != null)
                        {
                            texte += espace + "\t<div style=\"text-align:left\">\n";
                            texte += Avoir_texte_lien_source(
                                info_RECORD.SOUR_source_liste_ID,
                                liste_source_ID_numero,
                                0,
                                tab + 2);
                            texte += espace + "\t</div>\n";
                        }
                        // NOTE record
                        if (info_RECORD.NOTE_STRUCTURE_liste_ID != null)
                        {
                            texte += Avoir_texte_NOTE_STRUCTURE(
                                info_RECORD.NOTE_STRUCTURE_liste_ID,
                                liste_citation_ID_numero,
                                liste_source_ID_numero,
                                liste_note_STRUCTURE_ID_numero,
                                liste_NOTE_RECORD_ID_numero,
                                0, // retrait
                                tab + 1);
                        }
                        texte += Avoir_texte_date_Changement(
                            info_RECORD.CHAN,
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero,
                            false,
                            tab + 1);
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                        texte += espace + "</table>\n";
                    }
                    else if (info_LINK.ID_RECORD != null)
                    {
                        if (marge)
                        {
                            texte += espace + "<table class=\"multimedia_m\">\n";
                        }
                        else
                        {

                            texte += espace + "<table class=\"multimedia\">\n";
                        }
                        texte += espace + "\t<tr>\n";
                        texte += espace + "\t\t<td>\n";
                        texte += espace + "\t\t\t<img class=\"mini2\" src=\"" +
                                            origine + "commun\\enregistrement_manquant.svg\" alt =\"\" \\>\n";

                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t<td width=700px>\n";
                        // ID
                        if (GH.GHClass.Para.voir_ID)
                        {

                            texte += espace + "\t<div style=\"\">\n";
                            texte += espace + "\t\t[" + info_LINK.ID_RECORD + "]\n";
                            texte += espace + "\t</div>\n";
                        }
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                        texte += espace + "</table>\n";

                    }
                }
            }
            GC.Collect();
            return (
                texte,
                liste_MULTIMEDIA_ID_numero,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_repo_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero
                );
        }

        private static (
            string, // teste,
            List<ID_numero>, // liste_citation_ID_numero,
            List<ID_numero>, // liste_note_STRUCTURE_ID_numero,
            List<ID_numero> // liste_NOTE_RECORD_ID_numero,
            ) Avoir_texte_auteur(
            List<GEDCOMClass.ORIG_record> ORIG_liste,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab = 0)
        {
            string espace = Tabulation(tab);
            if (R.IsNullOrEmpty(ORIG_liste))
                return
                (
                    null,
                    liste_citation_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero
                );
            string texte = null;
            foreach (GEDCOMClass.ORIG_record info_ORIG in ORIG_liste)
            {
                if (R.IsNotNullOrEmpty(info_ORIG.N1_NAME))
                    texte += espace + " Ouvrage fait par  " + info_ORIG.N1_NAME;
                if (R.IsNotNullOrEmpty(info_ORIG.N1_TYPE))
                    texte += ", " + Traduire_mot_anglais(info_ORIG.N1_TYPE, true) + "\n";
                if (R.IsNotNullOrEmpty(info_ORIG.N1_NOTE_STRUCTURE_liste_ID))
                {
                    texte +=
                        Avoir_texte_NOTE_STRUCTURE(
                        info_ORIG.N1_NOTE_STRUCTURE_liste_ID,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        0, // retrait
                        tab);
                }
            }
            GC.Collect();
            return (
                texte,
                liste_citation_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero
                );
        }

        private static string Avoir_texte_chercheur(
            List<ID_numero> liste,
            string sous_dossier,
            string dossier_sortie,
            List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab
            //            , [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            if (R.IsNullOrEmpty(liste))
                return null;
            string espace = Tabulation(tab);
            string texte = null;
            string temp_string;
            foreach (ID_numero item in liste)
            {
                GEDCOMClass.SUBMITTER_RECORD info_chercheur = GEDCOMClass.Avoir_info_SUBMITTER_RECORD(item.ID);
                if (info_chercheur == null)
                    return null;
                string nom = GEDCOMClass.Avoir_premier_nom_chercheur(item.ID);
                texte += espace + "\t<a id=\"RefSubmitter" + item.numero.ToString() + "\"></a>\n";
                string lien_chercheur = Avoir_nom_lien_chercheur(nom, item.ID);
                texte += espace + "<a id=\"" + lien_chercheur + "\"></a>\n";
                // separation
                texte += Separation(5, null, "e00", null, tab);
                texte += espace + "<div class=\"tableau\" style=\"min-height:220px\">\n";
                // titre du tableau
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<table class=\"tableau_tete\">\n";
                texte += espace + "\t\t\t<tr>\n";
                texte += espace + "\t\t\t\t<td style=\"width:50px\"><span class=\"tableau_citation\">" + item.numero.ToString() + "</span></td>\n";
                texte += espace + "\t\t\t\t<td style=\"width:*\">\n";
                texte += espace + "\t\t\t\t\t<strong style=\"font-size:150%\">\n";
                texte += Convertir_texte_html(nom, false, tab + 6);
                texte += espace + "\t\t\t\t\t</strong>\n";
                if (GH.GHClass.Para.voir_ID)
                {
                    texte += espace + "\t\t\t\t\t<span class=\"tableau_ID\">[" + item.ID + "]</span>\n";
                }
                texte += espace + "\t\t\t\t</td>\n";
                texte += espace + "\t\t\t\t<td style=\"width:150px\">\n";
                texte += espace + "\t\t\t\t</td>\n";
                texte += espace + "\t\t\t</tr>\n";
                texte += espace + "\t\t</table>\n";
                texte += espace + "\t</div>\n";
                texte += Avoir_texte_NAME
                    (
                        info_chercheur.N1_NAME_liste,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        tab);
                // adresse
                string texte_adresse = Avoir_texte_adresse(

                info_chercheur.N1_SITE,
                info_chercheur.N1_ADDR_liste,
                info_chercheur.N1_PHON_liste,
                info_chercheur.N1_FAX_liste,
                info_chercheur.N1_EMAIL_liste,
                info_chercheur.N1_WWW_liste,
                sous_dossier,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero,
                tab + 3);
                if (R.IsNotNullOrEmpty(texte_adresse))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tAdresse\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\"> \n";
                    texte += texte_adresse;
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // langue
                if (info_chercheur.N1_LANG != "")
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tLangue\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t" + Traduire_language(info_chercheur.N1_LANG) + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // RFN numéro de fichier d'enregistrement permanent
                if (R.IsNotNullOrEmpty(info_chercheur.N1_RFN))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tRFN\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t" + info_chercheur.N1_RFN + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // RIN id d'enregistrement automatisé
                if (R.IsNotNullOrEmpty(info_chercheur.N1_RIN))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tRIN\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t" + info_chercheur.N1_RIN + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }

                //  MULTIMEDIA_LINK
                temp_string = null;
                (
                    temp_string,
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero
                    ) =
                Avoir_texte_MULTIMEDIA(
                    info_chercheur.MULTIMEDIA_LINK_liste_ID,
                    sous_dossier,
                    dossier_sortie,
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    true, // +marge
                    tab + 1
                    );
                texte += temp_string;

                // note du submiteur
                if (R.IsNotNullOrEmpty(info_chercheur.N1_NOTE_STRUCTURE_liste_ID))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += Avoir_texte_NOTE_STRUCTURE
                        (info_chercheur.N1_NOTE_STRUCTURE_liste_ID,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        0, // retrait
                        tab + 1);
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // si changement de date
                if (GH.GHClass.Para.voir_date_changement && info_chercheur.N1_CHAN != null)
                {
                    GEDCOMClass.CHANGE_DATE N1_CHAN = info_chercheur.N1_CHAN;
                    if (N1_CHAN.N1_CHAN_DATE != "")
                    {
                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                        texte += Avoir_texte_date_Changement(
                                N1_CHAN,
                                liste_citation_ID_numero,
                                liste_source_ID_numero,
                                liste_note_STRUCTURE_ID_numero,
                                liste_NOTE_RECORD_ID_numero,
                                false,
                                tab + 3);
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t</div>\n";
                    }
                }
                // fin tableau
                texte += espace + "</div>\n";
            }
            GC.Collect();
            return texte;
        }

        private (string, bool) Groupe_archive(
            List<string> liste_ID,
            string sous_dossier,
            string dossier_sortie,
            List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab
            )
        {
            if (liste_ID == null)
                return (null, false);
            string origine = @"../";
            if (sous_dossier == "")
                origine = "";
            string texte = null;
            string espace = Tabulation(tab);
            if (GH.GHClass.Para.voir_media)
            {
                string texte_MULTIMEDIA;
                (
                    texte_MULTIMEDIA,
                    _,
                    _,
                    _,
                    _,
                    _,
                    _
                ) = Avoir_texte_MULTIMEDIA(
                    liste_ID,
                    sous_dossier,
                    dossier_sortie,
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    false,
                    tab + 1);
                if (texte_MULTIMEDIA != null)
                {
                    texte += espace + "<a id=\"groupe_archive\"></a>\n";
                    texte += espace + "<!--**************************\n";
                    texte += espace + "*  groupe Archive            *\n";
                    texte += espace + "***************************-->\n";
                    // titre du groupe archive
                    texte += Titre_groupe(origine + @"commun/archive.svg", "Archive", null, tab);
                    // Tableau
                    texte += Separation(5, null, "000", null, tab);
                    texte += texte_MULTIMEDIA;
                }
            }
            return (texte, true);
        }

        private (string, int, int, bool) Groupe_attribut(
            List<GEDCOMClass.EVEN_ATTRIBUTE_STRUCTURE> liste_attribut,
            string sous_dossier,
            string dossier_sortie,
            bool menu,
            int numero_carte,
            int numero_source,
            List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            if (R.IsNullOrEmpty(liste_attribut))
                return (
                    null,
                    numero_carte,
                    numero_source,
                    false);
            string origine = @"../";
            if (sous_dossier == "")
                origine = "";
            string texte = null;
            string espace = Tabulation(tab);
            texte += "<a id=\"groupe_attribut\"></a>\n";
            texte += espace + "<!--**************************\n";
            texte += espace + "*  groupe Attribut           *\n";
            texte += espace + "***************************-->\n";
            string titre = "Attribut";
            if (R.IsNotNullOrEmpty(liste_attribut))
                titre += "s";
            // titre du groupe
            texte += Titre_groupe(origine + @"commun/attribut.svg", titre, null, tab);
            Liste_par_date par_date = new Liste_par_date();
            liste_attribut.Sort(par_date);

            // index des événements
            if (liste_attribut.Count > 1)
            {
                texte += Separation(5, null, "000", null, tab);
                texte += espace + "<div class=\"tableau evenement-index \" >\n";
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_tete\">\n";
                texte += espace + "\t\t\t<strong style=\"font-size:150%\">\n";
                texte += espace + "\t\t\t\tIndex\n";
                texte += espace + "\t\t\t</strong>\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
                texte += espace + "\t<table class=\"index_evenement\">\n";
                foreach (GEDCOMClass.EVEN_ATTRIBUTE_STRUCTURE info in liste_attribut)
                {
                    string lien_evenement = Avoir_nom_lien_evenement(info.N1_EVEN, info.N2_DATE);
                    texte += espace + "\t\t<tr>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t<button>\n";
                    texte += espace + "\t\t\t\t\t<a href=\"#" + lien_evenement + "\">\n";
                    texte += espace + "\t\t\t\t\t\t<img style=\"height:25px\" src=\"../commun/go_evenement.svg\" alt=\"\" />\n";
                    texte += espace + "\t\t\t\t\t</a>\n";
                    texte += espace + "\t\t\t\t</button>\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    string date = Avoir_texte_date_PP(
                        info.N1_EVEN,
                        info.N2_DATE
                        );
                    texte += espace + "\t\t\t\t\t" + date + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + Convertir_EVEN_en_texte(info.N1_EVEN) + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t</tr>\n";
                }
                texte += espace + "\t</table>\n";
                texte += espace + "</div>";
            }
            // FIN index des événements
            foreach (GEDCOMClass.EVEN_ATTRIBUTE_STRUCTURE info in liste_attribut)
            {
                string temp = null;
                string lien_evenement = Avoir_nom_lien_evenement(info.N1_EVEN, info.N2_DATE);
                texte += espace + "<a id=\"" + lien_evenement + "\"></a>\n";
                (temp, numero_carte, numero_source) =
                    Avoir_texte_evenement(
                        info,
                        null,
                        null,
                        null,
                        null,
                        sous_dossier,
                        dossier_sortie,
                        menu,
                        numero_carte,
                        numero_source,
                        liste_MULTIMEDIA_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        tab);
                texte += temp;
            }
            GC.Collect();
            return (
                texte,
                numero_carte,
                numero_source,
                true);
        }

        /*        public string AssemblerPatronymePrenom(string patronyme, string prenom)
                {
                    if (String.IsNullOrEmpty(prenom) && String.IsNullOrEmpty(patronyme)) return null;
                    if (String.IsNullOrEmpty(prenom)) prenom = "?";
                    if (String.IsNullOrEmpty(patronyme)) patronyme = "?";
                    return patronyme + ", " + prenom;
                }
        */
        private static string Avoir_texte_individual_53(
            GEDCOMClass.INDIVIDUAL_53 info,
            string sous_dossier,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + " à <b>" + Avoir_nom_method() + "</b>");

            string texte = null;
            string espace = Tabulation(tab);
            texte += espace + "<div class=\"tableau\" style=\"font-size:75%\">\n";
            // titre du tableau
            texte += espace + "\t<div class=\"tableau_ligne\">\n";
            texte += espace + "\t\t<div class=\"tableau_tete\">\n";
            texte += espace + "\t\t\tIndividu\n";
            texte += espace + "\t\t</div>\n";
            texte += espace + "\t</div>\n";
            // liste de nom
            texte += Avoir_texte_NAME_53(
                info.N0_NAME_liste,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero,
                tab + 1);
            // Titre
            if (R.IsNotNullOrEmpty(info.N0_TITL))
            {
                texte += espace + "\t<div class=\"tableau_ligne\" style=\"margin-right:2px\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tTitre\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info.N0_TITL + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // Sex
            if (R.IsNotNullOrEmpty(info.N0_SEX))
            {
                texte += espace + "\t<div class=\"tableau_ligne\" style=\"margin-right:2px\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tSex (biologique)\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + Convertir_SEX(info.N0_SEX) + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";

            }
            // address
            if (R.IsNotNullOrEmpty(info.N0_ADDR_liste))
            {
                string texte_adresse = Avoir_texte_adresse(//c
                   null,
                   info.N0_ADDR_liste,
                   null,
                   null,
                   null,
                   null,
                   sous_dossier,
                   null,
                   null,
                   null,
                   liste_NOTE_RECORD_ID_numero,
                   tab);
                if (R.IsNotNullOrEmpty(texte_adresse))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tAdresse\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += texte_adresse;
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
            }
            // RELI
            if (R.IsNotNullOrEmpty(info.N0_RELI))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tAppartenance religieuse\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info.N0_RELI + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // RELI Appartenance religieuse
            if (R.IsNotNullOrEmpty(info.N0_NAMR))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tReligion \n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info.N0_NAMR + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";

                // Nom de la religion
                if (R.IsNotNullOrEmpty(info.N1_NAMR_RELI))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\t&nbsp;\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\tAffiliation: " + info.N1_NAMR_RELI + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
            }
            // EDUC
            if (R.IsNotNullOrEmpty(info.N0_EDUC))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tÉducation\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info.N0_EDUC + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // OCCU
            if (R.IsNotNullOrEmpty(info.N0_OCCU))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tOccupation\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info.N0_OCCU + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // SSN SOCIAL_SECURITY_NUMBER
            if (R.IsNotNullOrEmpty(info.N0_SSN))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tNuméro de sécurité sociale\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info.N0_SSN + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // IDNO
            if (R.IsNotNullOrEmpty(info.N0_IDNO))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tNuméro national d'identité\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info.N0_IDNO + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
                // type
                if (R.IsNotNullOrEmpty(info.N1_IDNO_TYPE))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\t&nbsp;\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\tType: " + info.N1_IDNO_TYPE + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
            }
            // PROP
            if (R.IsNotNullOrEmpty(info.N0_PROP))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tPossession\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info.N0_PROP + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // DSCR
            if (R.IsNotNullOrEmpty(info.N0_PROP))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tDescription\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info.N0_DSCR + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // SIGN
            if (R.IsNotNullOrEmpty(info.N0_SIGN))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tSignature\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info.N0_SIGN + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // NMR
            if (R.IsNotNullOrEmpty(info.N0_NMR))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tNombre de mariage\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info.N0_NMR + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // NCHI
            if (R.IsNotNullOrEmpty(info.N0_NCHI))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tNombre d'enfant\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info.N0_NCHI + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // NATI
            if (R.IsNotNullOrEmpty(info.N0_NATI))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tNationalité\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info.N0_NATI + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // CAST
            if (R.IsNotNullOrEmpty(info.N0_CAST))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tNom de caste\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info.N0_CAST + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // Évènement
            GEDCOMClass.EVEN_STRUCTURE_53 NaissanceIndividu;
            (_, NaissanceIndividu) = GEDCOMClass.AvoirEvenementNaissance_53(info.N0_EVEN_liste);
            string dateNaissance = NaissanceIndividu.N1_DATE;
            foreach (GEDCOMClass.EVEN_STRUCTURE_53 info_evenement in info.N0_EVEN_liste)
            {
                string temp = null;
                (temp, _, _) = Avoir_texte_evenement_53(
                info_evenement,
                dateNaissance,
                null,
                null,
                sous_dossier,
                null,
                0,
                0,
                null,
                liste_citation_ID_numero,
                null,
                null,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero,
                tab
                );
                texte += temp;
            }
            texte += espace + "<p style=\"font-size:20%\"></p>";
            texte += espace + "</div>\n";// fin tableau
            GC.Collect();
            return texte;
        }
        private static string Avoir_texte_lien_chercheur(List<string> liste_chercheur_ID, List<ID_numero> liste_chercheur_ID_numero, int tab)
        {
            if (!GH.GHClass.Para.voir_reference)
                return "";
            Application.DoEvents();
            if (liste_chercheur_ID.Count() == 0)
                return "";
            string espace = Tabulation(tab);
            string texte = null;
            texte += espace + "<span>\n";
            if (R.IsNotNullOrEmpty(liste_chercheur_ID_numero))
            {
                int compteur_chercheur;
                if (liste_chercheur_ID.Count == 1)
                {
                    texte += espace + "\tVoir chercheur ";
                }
                else
                {
                    texte += espace + "\tVoir chercheurs ";
                }
                foreach (string ID in liste_chercheur_ID)
                {
                    compteur_chercheur = Avoir_numero_reference(ID, liste_chercheur_ID_numero);
                    texte += "<a class=\"chercheur\" href=\"#RefSubmitter" + compteur_chercheur.ToString() + "\">" + compteur_chercheur.ToString() + "</a>, ";
                }
                texte = texte.TrimEnd(' ', ',') + "\n";
            }
            texte += espace + "</span>\n";
            return texte;
        }

        private static (
            string,
            List<ID_numero>,
            List<ID_numero>,
            List<ID_numero>,
            List<ID_numero>
            ) Avoir_texte_immigration(
            GEDCOMClass.IMMI_record info_IMMI,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab = 0)
        {
            if (info_IMMI == null)
                return (
                    null,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero
                    );
            string texte = null;
            string espace = Tabulation(tab);
            // Tableau 0px
            texte += espace + "<div class=\"tableau tableau_0px\">\n";
            texte += espace + "\t<div class=\"tableau_ligne\">\n";
            texte += espace + "\t\t<div class=\"tableau_colW\">\n";
            if (R.IsNotNullOrEmpty(info_IMMI.N1_NAME))
                texte += espace + "\t\t\tLe départ s'est fait sur le " + info_IMMI.N1_NAME + "\n";
            texte += espace + "\t\t\ten date du ";
            string date = "_ _ _ _ _ _";
            if (info_IMMI.N3_PORT_DPRT_DATE != null)
                (date, _) = Convertir_date(info_IMMI.N3_PORT_DPRT_DATE, GH.GHClass.Para.date_longue);
            texte += date + " à ";
            string lieu = "_ _ _ _ _ _ _ _ _ _ _ _ _";

            if (info_IMMI.N3_PORT_DPRT_PLAC != null)
                lieu = info_IMMI.N3_PORT_DPRT_PLAC;
            texte += lieu + ".\n";

            texte += espace + "\t\t\tArrivé le ";
            date = "_ _ _ _ _ _";
            if (info_IMMI.N3_PORT_ARVL_DATE != null)
                (date, _) = Convertir_date(info_IMMI.N3_PORT_ARVL_DATE, GH.GHClass.Para.date_longue);
            texte += date + " à ";
            lieu = "_ _ _ _ _ _ _ _ _ _ _ _ _";
            if (info_IMMI.N3_PORT_ARVL_PLAC != null)
                lieu = info_IMMI.N3_PORT_ARVL_PLAC;
            texte += lieu + ".\n";
            texte += espace + "\t\t</div>\n";
            texte += espace + "\t</div>\n";
            // texte
            if (info_IMMI.N1_TEXT_liste != null)
            {

                for (int f = 0; f < info_IMMI.N1_TEXT_liste.Count; f++)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += Convertir_texte_html(info_IMMI.N1_TEXT_liste[f].N0_TEXT, false, tab + 3);
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                    if (R.IsNotNullOrEmpty(info_IMMI.N1_TEXT_liste[f].N1_NOTE_STRUCTURE_liste_ID))
                    {
                        texte +=
                        Avoir_texte_NOTE_STRUCTURE(
                        info_IMMI.N1_TEXT_liste[f].N1_NOTE_STRUCTURE_liste_ID,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        .25f, // retrait
                        tab);
                    }
                }
            }
            // note
            texte += Avoir_texte_NOTE_STRUCTURE(
                info_IMMI.N1_NOTE_STRUCTURE_liste_ID,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero,
                0, // retrait
                tab);
            // fin tableau
            texte += espace + "</div>\n";
            GC.Collect();
            return (
                texte,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero
                );
        }

        private static string Avoir_texte_lien_citation(
            List<string> liste_citation_ID,
            List<ID_numero> liste_citation_ID_numero,
            //List<ID_numero> liste_source_ID_numero,
            float retrait,
            int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {

            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            string texte = null;
            if (!GH.GHClass.Para.voir_reference)
                return texte;
            if (R.IsNullOrEmpty(liste_citation_ID))
                return texte;
            string espace = Tabulation(tab);
            texte += espace + "<div style=\"font-Size:medium;padding-left:" + retrait.ToString().Replace(",", ".") + "in;\">\n";
            int compteur_citation;
            if (liste_citation_ID.Count > 1)
            {
                texte += espace + "\t" + "Voir citations ";
            }
            else
            {
                texte += espace + "\tVoir citation ";
            }
            foreach (string ID in liste_citation_ID)
            {
                compteur_citation = Avoir_numero_reference(ID, liste_citation_ID_numero);
                //Ra.Z("compteur_citation=" + compteur_citation);
                if (compteur_citation == 0)
                    texte += BLINK0 + "\n";
                else
                    texte += "<a class=\"citation\" href=\"#citation-" + compteur_citation.ToString() + "\">" + compteur_citation.ToString() + "</a>, ";
            }
            texte = texte.TrimEnd(' ', ',') + "\n";
            texte += espace + "</div>\n";
            return texte;
        }

        private static (string, int, int)
            Avoir_texte_evenement
            (
            GEDCOMClass.EVEN_ATTRIBUTE_STRUCTURE info_evenement,
            string date_individu_naissance,
            string date_individu_deces,
            string date_conjoint_naissance,
            string date_conjointe_naissance,
            string sous_dossier,
            string dossier_sortie,
            bool menu,
            int numero_carte,
            int numero_source,
            List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber+"<br>even=" + info_evenement.N1_EVEN);
            Regler_code_erreur();
            string texte_bloque = null;
            string texte_titre = null;
            string texte;
            string texte_date;
            string espace = Tabulation(tab);
            string age_HUSB;
            string age_WIFE;
            Application.DoEvents();
            if (R.IsNotNullOrEmpty(info_evenement.N1_EVEN_texte))
            {
                if (info_evenement.N1_EVEN_texte.ToUpper() == "Y")
                {
                    texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte_bloque += espace + "\t\t\t" + "L'évènement a eu lieu, mais nous avons pas toute les informations.\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t</div>\n";
                }
            }
            {
                // Texte de EVEN
                if (R.IsNotNullOrEmpty(info_evenement.N1_EVEN_texte))
                {
                    if (info_evenement.N1_EVEN_texte != "Y")
                    {
                        texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                        texte_bloque += Convertir_texte_html(info_evenement.N1_EVEN_texte, false, tab + 3);
                        texte_bloque += espace + "\t\t</div>\n";
                        texte_bloque += espace + "\t</div>\n";
                    }
                }
                // Age individu
                if (info_evenement.N1_EVEN != "BIRT" && (info_evenement.N2_AGE != null) || date_individu_naissance != null)
                {
                    texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte_bloque += espace + "\t\t\tÂge\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";

                    if (R.IsNotNullOrEmpty(info_evenement.N2_AGE))
                    {
                        texte_bloque += espace + Avoir_texte_age_evenenemt(info_evenement.N2_AGE);
                    }
                    else
                    {
                        string age = Calculer_age(date_individu_naissance, info_evenement.N2_DATE);
                        if (R.IsNotNullOrEmpty(age))
                        {
                            texte_bloque += espace + age;
                        }

                    }
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t</div>\n";
                }
                // age conjoint
                if (R.IsNotNullOrEmpty(info_evenement.N3_HUSB_AGE))
                {
                    texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte_bloque += espace + "\t\t\tÂge conjoint\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                    if (R.IsNotNullOrEmpty(info_evenement.N3_HUSB_AGE))
                    {
                        age_HUSB = Avoir_texte_age_evenenemt(info_evenement.N3_HUSB_AGE);
                    }
                    else
                    {
                        age_HUSB = Calculer_age(date_conjoint_naissance, info_evenement.N2_DATE);
                    }
                    texte_bloque += espace + age_HUSB;
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t</div>\n";
                }
                // age conjointe
                if (R.IsNotNullOrEmpty(info_evenement.N3_WIFE_AGE))
                {
                    texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte_bloque += espace + "\t\t\tÂge conjointe\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                    if (R.IsNotNullOrEmpty(info_evenement.N3_WIFE_AGE))
                    {
                        age_WIFE = Avoir_texte_age_evenenemt(info_evenement.N3_WIFE_AGE);
                    }
                    else
                    {
                        age_WIFE = Calculer_age(date_conjoint_naissance, info_evenement.N2_DATE);
                    }
                    texte_bloque += espace + age_WIFE;
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t</div>\n";
                }
                // TYPE
                if (R.IsNotNullOrEmpty(info_evenement.N2_TYPE_liste))
                {
                    texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte_bloque += espace + "\t\t\tType\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                    int numero_type = 0;
                    int nombre_item = info_evenement.N2_TYPE_liste.Count;
                    foreach (string type in info_evenement.N2_TYPE_liste)
                    {
                        numero_type++;
                        texte_bloque += Convertir_texte_html(Traduire_relation_conjoint(type), false, tab + 3);
                        if (numero_type < nombre_item)
                            texte_bloque += "\t\t\t<br />";
                    }
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t</div>\n";
                }
                // statut marital
                if (R.IsNotNullOrEmpty(info_evenement.N2_MSTAT))
                {
                    texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte_bloque += espace + "\t\t\tStatut marital\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte_bloque += espace + "\t\t\t" + Convertir_statut_marital(info_evenement.N2_MSTAT) + "\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t</div>\n";
                }
                // _FNA Heridis
                if (R.IsNotNullOrEmpty(info_evenement.N2__FNA))
                {
                    texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte_bloque += espace + "\t\t\tToujours en recherche\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte_bloque += espace + "\t\t\t" + Traduire_yes_no(info_evenement.N2__FNA) + "\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t</div>\n";
                }
                // si temple 
                if (R.IsNotNullOrEmpty(info_evenement.N2_TEMP))
                {
                    texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte_bloque += espace + "\t\t\tTemple\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte_bloque += espace + "\t\t\t" + info_evenement.N2_TEMP + "\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t</div>\n";
                }
                // si Place lieu
                if ((info_evenement.N2_PLAC != null) && (info_evenement.N1_EVEN != "RESI"))
                {
                    texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte_bloque += espace + "\t\t\tLieu\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                    string a;
                    (
                        a,
                        numero_carte,
                        _
                    ) = Avoir_texte_PLAC(
                        info_evenement.N2_PLAC,
                        sous_dossier,
                        numero_carte,
                        numero_source,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        tab + 3);
                    texte_bloque += a;
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t</div>\n";
                }
                // adresse
                string texte_adresse = Avoir_texte_adresse(
                    info_evenement.N2_SITE,
                    info_evenement.N2_ADDR_liste,
                    info_evenement.N2_PHON_liste,
                    info_evenement.N2_FAX_liste,
                    info_evenement.N2_EMAIL_liste, info_evenement.N2_WWW_liste,
                    sous_dossier,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    tab);
                if (R.IsNotNullOrEmpty(texte_adresse))
                {
                    texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte_bloque += espace + "\t\t\tAdresse\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte_bloque += texte_adresse;
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t</div>\n";
                }
                // QUAY
                if (R.IsNotNullOrEmpty(info_evenement.N2_QUAY))
                {
                    texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte_bloque += espace + "\t\t\tCrédibilité\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte_bloque += espace + "\t\t\t\t" + Traduire_QUAY(info_evenement.N2_QUAY) + "\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t</div>\n";
                }
                // si religion
                if (R.IsNotNullOrEmpty(info_evenement.N2_RELI))
                {
                    texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte_bloque += espace + "\t\t\tReligion \n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte_bloque += Convertir_texte_html(info_evenement.N2_RELI, false, tab + 3) + "\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t</div>\n";
                }
                // si cause
                if (R.IsNotNullOrEmpty(info_evenement.N2_CAUS))
                {
                    texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte_bloque += espace + "\t\t\tCause de l'évènement \n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte_bloque += Convertir_texte_html(info_evenement.N2_CAUS, false, tab + 3);
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t</div>\n";
                }
                // Relation avec une famille
                if (R.IsNotNullOrEmpty(info_evenement.N2_FAMC))
                {
                    string ID_pere_adoption = GEDCOMClass.Avoir_famille_conjoint_ID(info_evenement.N2_FAMC);
                    string nom_pere_adoption = GEDCOMClass.AvoirPrenomPatronymeIndividu(ID_pere_adoption);
                    string ID_mere_adoption = GEDCOMClass.Avoir_famille_conjointe_ID(info_evenement.N2_FAMC);
                    string nom_mere_adoption = GEDCOMClass.AvoirPrenomPatronymeIndividu(ID_mere_adoption);
                    string texte_ID_pere = "";
                    string texte_ID_mere = "";
                    if (GH.GHClass.Para.voir_ID == true)
                    {
                        if (ID_pere_adoption != null && nom_pere_adoption != null)
                        {
                            texte_ID_pere = " [" + ID_pere_adoption + "]";
                        }
                        if (ID_mere_adoption != null && nom_mere_adoption != null)
                        {
                            texte_ID_mere = " [" + ID_mere_adoption + "]";
                        }
                    }
                    // texte pour le père
                    string texte_pere = null;
                    if (ID_pere_adoption != null)
                    {
                        if (menu)
                        {
                            texte_pere += "<a class=\"ficheIndividu\"  href=\"" + ID_pere_adoption + ".html\"></a>";
                        }
                        else
                        {
                            texte_pere += "<a class=\"ficheIndividuGris\"></a>";
                        }
                    }
                    if (R.IsNotNullOrEmpty(nom_pere_adoption))
                        texte_pere += " " + nom_pere_adoption;
                    if (R.IsNotNullOrEmpty(texte_ID_pere))
                        texte_pere += " " + texte_ID_pere;
                    // texte pour le mère
                    string texte_mere = null;
                    if (ID_mere_adoption != null)
                    {
                        if (menu)
                        {
                            texte_mere += "<a class=\"ficheIndividu\"  href=\"" + ID_mere_adoption + ".html\"></a>";
                        }
                        else
                        {
                            texte_mere += "<a class=\"ficheIndividuGris\"></a>";
                        }
                    }
                    if (R.IsNotNullOrEmpty(nom_mere_adoption))
                        texte_mere += " " + nom_mere_adoption;
                    if (R.IsNotNullOrEmpty(texte_ID_mere))
                        texte_mere += " " + texte_ID_mere;
                    texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                    if (info_evenement.N1_EVEN == "ADOP")
                    {
                        texte_bloque += espace + "\t\t\tAdopter par \n";
                        if (R.IsNotNullOrEmpty(info_evenement.N2_FAMC_ADOP))
                        {
                            if (info_evenement.N2_FAMC_ADOP.ToUpper() == "BOTH")
                            {
                                texte_bloque += espace + "\t\t\t" + texte_pere + " et " + texte_mere + "\n";
                            }
                            else if (info_evenement.N2_FAMC_ADOP.ToUpper() == "HUSB")
                            {
                                texte_bloque += espace + "\t\t\t" + texte_pere + "\n";
                            }
                            else if (info_evenement.N2_FAMC_ADOP.ToUpper() == "WIFE")
                            {
                                texte_bloque += espace + "\t\t\t" + texte_mere + "\n";
                            }
                        }
                    }
                    else
                    {
                        if (R.IsNotNullOrEmpty(info_evenement.N2_FAMC))
                        {
                            texte_bloque += espace + "\t\t\t\tEn relation avec la\n";
                            if (menu)
                            {
                                texte_bloque += espace + "\t\t\t\t<a class=\"ficheFamille\"  href=\"../familles/" +
                                    info_evenement.N2_FAMC + ".html\"></a> \n";
                            }
                            else
                            {
                                texte_bloque += espace + "\t\t\t\t<a class=\"ficheFamilleGris\"></a> \n";
                            }
                        }
                        else
                        {
                            texte_bloque += espace + "\t\t\t\t<a class=\"ficheFamilleGris\"></a> \n";
                        }
                        texte_bloque += espace + "\t\t\tfamille de \n";

                        if (R.IsNotNullOrEmpty(texte_pere) && R.IsNullOrEmpty(texte_mere))
                            texte_bloque += espace + "\t\t\t" + texte_pere;
                        else if (R.IsNotNullOrEmpty(texte_mere) && R.IsNullOrEmpty(texte_pere))
                            texte_bloque += espace + "\t\t\t" + texte_mere;
                        else
                            texte_bloque += espace + "\t\t\t" + texte_pere + " et " + texte_mere;
                    }
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t</div>\n";
                }
                // si a description
                if (R.IsNotNullOrEmpty(info_evenement.description))
                {
                    texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_col1d\">\n";
                    texte_bloque += espace + "\t\t\tDescription \n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte_bloque += espace + "\t\t\t" + info_evenement.description + "\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t</div>\n";
                }
                // si a agence
                if (R.IsNotNullOrEmpty(info_evenement.N2_AGNC))
                {
                    texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte_bloque += espace + "\t\t\tAgence\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte_bloque += Convertir_texte_html(info_evenement.N2_AGNC, false, tab + 3);
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t</div>\n";
                }
                // si Ancestrologie
                // _ANCES_ORDRE
                if (R.IsNotNullOrEmpty(info_evenement.N2__ANCES_ORDRE))
                {
                    texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte_bloque += espace + "\t\t\tOrdre\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte_bloque += espace + "\t\t\t" + info_evenement.N2__ANCES_ORDRE + " (" + GEDCOMClass.info_HEADER.N2_SOUR_NAME + ")\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t</div>\n";
                }
                // _ANCES_XINSEE
                if (R.IsNotNullOrEmpty(info_evenement.N2__ANCES_XINSEE))
                {
                    texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte_bloque += espace + "\t\t\tCode géographique\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte_bloque += espace + "\t\t\t" + info_evenement.N2__ANCES_XINSEE + " (" + GEDCOMClass.info_HEADER.N2_SOUR_NAME + ")\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t</div>\n";
                }
                // texte
                if (R.IsNotNullOrEmpty(info_evenement.N2_TEXT_liste))
                {
                    foreach (GEDCOMClass.TEXT_STRUCTURE info_texte in info_evenement.N2_TEXT_liste)
                    {
                        if (R.IsNotNullOrEmpty(info_texte.N0_TEXT))
                        {
                            texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                            texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                            texte_bloque += espace + "\t\t\tInformation\n";
                            texte_bloque += espace + "\t\t</div>\n";
                            texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                            texte_bloque += Convertir_texte_html(info_texte.N0_TEXT, false, tab + 3);
                            texte_bloque += espace + "\t\t</div>\n";
                            texte_bloque += espace + "\t</div>\n";
                        }
                        if (R.IsNotNullOrEmpty(info_texte.N1_NOTE_STRUCTURE_liste_ID))
                        {
                            texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                            texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                            texte_bloque += espace + "\t\t\t&nbsp;\n";
                            texte_bloque += espace + "\t\t</div>\n";
                            texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                            texte_bloque +=
                            Avoir_texte_NOTE_STRUCTURE(
                                info_texte.N1_NOTE_STRUCTURE_liste_ID,
                                liste_citation_ID_numero,
                                liste_source_ID_numero,
                                liste_note_STRUCTURE_ID_numero,
                                liste_NOTE_RECORD_ID_numero,
                                .25f, // retrait
                                tab);
                            texte_bloque += espace + "\t\t</div>\n";
                            texte_bloque += espace + "\t</div>\n";
                        }
                    }
                }
                //  MULTIMEDIA_LINK
                string temp_string;
                (
                    temp_string,
                    _,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    _,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero
                    ) =
                Avoir_texte_MULTIMEDIA(
                    info_evenement.MULTIMEDIA_LINK_liste_ID,
                    sous_dossier,
                    dossier_sortie,
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    true, // +marge
                    tab + 1
                    );
                texte_bloque += temp_string;

                // citation
                texte_bloque += Avoir_texte_lien_citation(
                        info_evenement.N2_SOUR_citation_liste_ID,
                        liste_citation_ID_numero,
                        0,
                        tab + 1);
                // source
                texte_bloque += Avoir_texte_lien_source(
                        info_evenement.N2_SOUR_source_liste_ID,
                        liste_source_ID_numero,
                        0,
                        tab + 1);
                // si note
                texte_bloque += Avoir_texte_NOTE_STRUCTURE(
                    info_evenement.N2_NOTE_STRUCTURE_liste_ID,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    0.04f, // retrait
                    tab + 1);
                // date changemnent
                if (info_evenement.N2_CHAN != null)
                {
                    if (GH.GHClass.Para.voir_date_changement && info_evenement.N2_CHAN != null)
                    {
                        texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                        texte_bloque += Avoir_texte_date_Changement(
                            info_evenement.N2_CHAN,
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero,
                            false,
                            tab + 3
                            );
                        texte_bloque += espace + "\t\t</div>\n";
                        texte_bloque += espace + "\t</div>\n";
                    }
                }
            }
            // titre de tableau
            texte_titre += espace + "\t<div class=\"tableau_ligne\">\n";
            if (String.IsNullOrEmpty(texte_bloque))
            {
                texte_titre += espace + "\t\t<div class=\"tableau_tete_bloque\">\n";
            }
            else
            {
                texte_titre += espace + "\t\t<div class=\"tableau_tete\">\n";
            }
            if (R.IsNotNullOrEmpty(info_evenement.N2_RESN))
            {
                texte_titre += espace + "\t\t\t<div class=\"tableau_colW blink2\">\n";
                texte_titre += espace + "\t\t\t\tRestriction\n";
                texte_titre += espace + "\t\t\t\t" + Traduire_RESN(info_evenement.N2_RESN) + "\n";
                texte_titre += espace + "\t\t\t</div>\n";
            }

            // Date EVENT 
            texte_titre += espace + "\t\t\t<strong style=\"font-size:150%\">\n";
            texte_date = Avoir_texte_date_PP(info_evenement.N1_EVEN, info_evenement.N2_DATE);
            if (texte_date != null)
                texte_titre += espace + texte_date + "\n";
            texte_titre += Convertir_EVEN_en_texte(info_evenement.N1_EVEN) + "</strong>\n";
            texte_titre += espace + "\t\t</div>\n";
            texte_titre += espace + "\t</div>\n";
            // Tableau
            texte = Separation(5, null, "000", null, tab);
            texte += espace + "<div class=\"tableau\">\n";
            texte += texte_titre + texte_bloque;
            // fin tableau
            texte += espace + "</div>\n";
            GC.Collect();
            return (
                texte,
                numero_carte,
                numero_source
                );
        }


        private static (string, int, int)
            Avoir_texte_evenement_53(
            GEDCOMClass.EVEN_STRUCTURE_53 info_evenement,
            string date_individu_naissance,
            string date_conjoint_naissance,
            string date_conjointe_naissance,
            string sous_dossier,
            string dossier_sortie,
            int numero_carte,
            int numero_source,
            List<MULTIMEDIA_ID_numero> liste_multimedia_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            Application.DoEvents();
            string texte_bloque = null;
            string texte_titre = null;
            string texte = null;
            string texte_date;
            string espace = Tabulation(tab);
            // avoir date
            texte_date = "\t\t\t" + Avoir_texte_date_PP(
            info_evenement.N0_EVEN,
            info_evenement.N1_DATE
            ) + "\n";
            // Description
            if (R.IsNotNullOrEmpty(info_evenement.N1_TYPE))
            {
                texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte_bloque += espace + "\t\t\tDescription\n";
                texte_bloque += espace + "\t\t</div>\n";
                texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte_bloque += espace + "\t\t\t" + info_evenement.N1_TYPE + "\n";
                texte_bloque += espace + "\t\t</div>\n";
                texte_bloque += espace + "\t</div>\n";
            }
            // statut marital
            if (R.IsNotNullOrEmpty(info_evenement.N1_MSTAT))
            {
                texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte_bloque += espace + "\t\t\tStatut marital\n";
                texte_bloque += espace + "\t\t</div>\n";
                texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte_bloque += espace + "\t\t\t" + Convertir_statut_marital(info_evenement.N1_MSTAT) + "\n";
                texte_bloque += espace + "\t\t</div>\n";
                texte_bloque += espace + "\t</div>\n";
            }
            // si type 
            if (R.IsNotNullOrEmpty(info_evenement.N1_TYPE) && info_evenement.N0_EVEN != "FACT")
            {
                texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte_bloque += espace + "\t\t\tType\n";
                texte_bloque += espace + "\t\t</div>\n";
                texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte_bloque += espace + "\t\t\t" + info_evenement.N1_TYPE + "\n";
                texte_bloque += espace + "\t\t</div>\n";
                texte_bloque += espace + "\t</div>\n";
            }
            // si Place lieu
            if ((info_evenement.N1_PLAC != null) && (info_evenement.N0_EVEN != "RESI"))
            {
                texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte_bloque += espace + "\t\t\tLieu\n";
                texte_bloque += espace + "\t\t</div>\n";
                texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                string a;
                (
                    a,
                    numero_carte,
                    numero_source
                ) = Avoir_texte_PLAC(
                    info_evenement.N1_PLAC,
                    sous_dossier,
                    numero_carte,
                    numero_source,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    tab + 3);
                texte_bloque += a;
                texte_bloque += espace + "\t\t</div>\n";
                texte_bloque += espace + "\t</div>\n";
            }
            // si religion
            if (R.IsNotNullOrEmpty(info_evenement.N1_RELI))
            {
                texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte_bloque += espace + "\t\t\tReligion \n";
                texte_bloque += espace + "\t\t</div>\n";
                texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte_bloque += espace + "\t\t\t" + info_evenement.N1_RELI + "\n";
                texte_bloque += espace + "\t\t</div>\n";
                texte_bloque += espace + "\t</div>\n";
            }
            // si cause
            if (R.IsNotNullOrEmpty(info_evenement.N1_CAUS))
            {
                texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte_bloque += espace + "\t\t\tCause de l'évènement \n";
                texte_bloque += espace + "\t\t</div>\n";
                texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte_bloque += espace + "\t\t\t" + info_evenement.N1_CAUS + "\n";
                texte_bloque += espace + "\t\t</div>\n";
                texte_bloque += espace + "\t</div>\n";
            }
            // si a agence
            if (R.IsNotNullOrEmpty(info_evenement.N1_AGNC))
            {
                texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte_bloque += espace + "\t\t\tAgence\n";
                texte_bloque += espace + "\t\t</div>\n";
                texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte_bloque += espace + "\t\t\t" + info_evenement.N1_AGNC + "\n";
                texte_bloque += espace + "\t\t</div>\n";
                texte_bloque += espace + "\t</div>\n";
            }
            // texte
            if (R.IsNotNullOrEmpty(info_evenement.N1_TEXT_liste))
            {
                foreach (GEDCOMClass.TEXT_STRUCTURE info_texte in info_evenement.N1_TEXT_liste)
                {
                    if (R.IsNotNullOrEmpty(info_texte.N0_TEXT))
                    {
                        texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                        texte_bloque += espace + "\t\t\tInformation\n";
                        texte_bloque += espace + "\t\t</div>\n";
                        texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                        texte_bloque += Convertir_texte_html(info_texte.N0_TEXT, false, tab + 3);
                        texte_bloque += espace + "\t\t</div>\n";
                        texte_bloque += espace + "\t</div>\n";
                    }
                    texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte_bloque += espace + "\t\t\t\n";
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte_bloque += Avoir_texte_NOTE_STRUCTURE(
                        info_texte.N1_NOTE_STRUCTURE_liste_ID,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        3, // retrait
                        tab);

                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t</div>\n";
                }
            }
            // citation
            texte_bloque += Avoir_texte_lien_citation(
                info_evenement.N1_SOUR_citation_liste_ID,
                liste_citation_ID_numero,
                0,
                tab + 1
                ) + "\n";

            // source
            texte_bloque += Avoir_texte_lien_source(
                    info_evenement.N1_SOUR_source_liste_ID,
                    liste_source_ID_numero,
                    0,
                    tab + 1);
            // note
            texte_bloque += Avoir_texte_NOTE_STRUCTURE(
                info_evenement.N1_NOTE_STRUCTURE_liste_ID,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero,
                0, // retrait
                tab + 1);

            // date changemnent
            if (info_evenement.N1_CHAN != null)
            {
                if (GH.GHClass.Para.voir_date_changement && info_evenement.N1_CHAN != null)
                {
                    texte_bloque += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte_bloque += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte_bloque += Avoir_texte_date_Changement(
                        info_evenement.N1_CHAN,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        false,
                        tab + 3
                        );
                    texte_bloque += espace + "\t\t</div>\n";
                    texte_bloque += espace + "\t</div>\n";
                }
            }
            // titre du tableau
            texte_titre += espace + "\t<div class=\"tableau_ligne\">\n";
            if (String.IsNullOrEmpty(texte_bloque))
            {
                texte_titre += espace + "\t\t<div class=\"tableau_tete_bloque\">\n";
            }
            else
            {
                texte_titre += espace + "\t\t<div class=\"tableau_tete\">\n";
            }
            // DATE EVEN
            string temp = Convertir_EVEN_en_texte(info_evenement.N0_EVEN);
            texte_titre += espace + "\t\t\t<strong>\n";
            if (texte_date != null)
                texte_titre += espace + texte_date + "\n";
            texte_titre += espace + "\t\t\t\t" + temp + "\n";
            texte_titre += espace + "\t\t\t" + "</strong>\n";


            // si age
            if (info_evenement.N0_EVEN != "BIRT")
            {
                if (R.IsNotNullOrEmpty(info_evenement.N1_AGE))
                {
                    texte_titre += espace + "\t\tà l'âge de " + info_evenement.N1_AGE + " ans";
                }
                else
                {
                    string age = Calculer_age(date_individu_naissance, info_evenement.N1_DATE);
                    if (R.IsNotNullOrEmpty(age))
                        texte_titre += espace + "\t\t\tà l'âge de " + age;
                }
            }
            texte_titre += espace + "\t\t</div>\n";
            texte_titre += espace + "\t</div>\n";

            // Tableau
            texte += Separation(5, null, "000", null, tab + 1);
            texte += espace + "<div class=\"tableau\" style=\" margin-left:5px;width:calc(100% - 13px)\"> <!--tableau-->\n";
            texte += texte_titre + texte_bloque;
            // fin tableau
            texte += espace + "</div><!--tableau FIN-->\n";
            GC.Collect();
            return (texte, numero_carte, numero_source);
        }

        private static string Calculer_age(
            string naissance,
            string evenement
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + " Naissance > " + naissance + " Evenement > " + evenement);
            string r = null;
            (string date_naissance, _) = Convertir_date(naissance, false);
            (string date_evenement, _) = Convertir_date(evenement, false);
            try
            {
                if (String.IsNullOrEmpty(date_naissance) || String.IsNullOrEmpty(date_evenement))
                {
                    return null;
                }
                else if (date_naissance.Length != 10 || date_evenement.Length != 10)
                {
                    return null;
                }
                else
                {
                    // vérifie si alphabet dans Naissance et Décès
                    int c = date_naissance.Length;
                    int w = 0;
                    while (w < c)
                    {
                        if ((date_naissance[w] >= 'a' && date_naissance[w] <= 'z') || (date_naissance[w] >= 'A' && date_naissance[w] <= 'Z'))
                        {
                            return null;
                        }
                        w++;
                    }
                    c = date_evenement.Length;
                    w = 0;
                    while (w < c)
                    {
                        if ((date_evenement[w] >= 'a' && date_evenement[w] <= 'z') || (date_evenement[w] >= 'A' && date_evenement[w] <= 'Z'))
                        {
                            return null;
                        }
                        w++;
                    }
                }
                DateTime nDate = Convert.ToDateTime(date_naissance);
                DateTime dDate = Convert.ToDateTime(date_evenement);
                TimeSpan difference = dDate.Subtract(nDate);
                DateTime age = DateTime.MinValue + difference;
                string annee = (age.Year - 1).ToString();
                string mois = (age.Month - 1).ToString();
                string jour = (age.Day - 1).ToString();
                if (annee == "1")
                {
                    r = annee + " an";
                }
                if (annee == "0")
                {
                    r = null;
                }
                else if (R.IsNotNullOrEmpty(annee))
                {
                    r = annee + " ans";
                }

                if (mois != "0")
                {
                    r = r + " " + mois + " mois";
                }
                if (jour == "1")
                {
                    r = r + " " + jour + " jour,";
                }
                else if (jour != "0")
                {
                    r = r + " " + jour + " jours";
                }
                return r;
            }
            catch
            {
                GC.Collect();
                return null;
            }
        }

        private static (
            List<MULTIMEDIA_ID_numero>,  // liste_MULTIMEDIA_ID_numero,
            List<ID_numero>, // liste_source_ID_numero,
            List<ID_numero>, // liste_repo_ID_numero,
            List<ID_numero>, // liste_note_STRUCTURE_ID_numero,
            List<ID_numero>, // liste_NOTE_RECORD_ID_numero,
            bool             // modfifier
            ) Genere_liste_reference_date(
            GEDCOMClass.CHANGE_DATE N1_CHAN,
            List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero)

        {
            if (!GH.GHClass.Para.voir_date_changement || N1_CHAN == null)
                return (
                    liste_MULTIMEDIA_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    false
                    );
            bool modifier;
            bool boucler = false;
            (liste_note_STRUCTURE_ID_numero, modifier) =
                Verifier_liste(N1_CHAN.N1_CHAN_NOTE_STRUCTURE_ID_liste, liste_note_STRUCTURE_ID_numero);
            if (modifier)
                boucler = true;
            // avoir note de Date de changement dans source depot note
            if (GH.GHClass.Para.voir_reference)
            {
                // source
                foreach (ID_numero ID_numero in liste_source_ID_numero)
                {
                    GEDCOMClass.SOURCE_RECORD info_source = GEDCOMClass.Avoir_info_source(ID_numero.ID);
                    if (info_source != null)
                        if (info_source.N1_CHAN != null)
                        {
                            (liste_note_STRUCTURE_ID_numero, modifier) =
                                Verifier_liste(info_source.N1_CHAN.N1_CHAN_NOTE_STRUCTURE_ID_liste, liste_note_STRUCTURE_ID_numero);
                            if (modifier)
                                boucler = true;
                        }
                }
                // depot
                foreach (ID_numero ID_numero in liste_repo_ID_numero)
                {
                    GEDCOMClass.REPOSITORY_RECORD info_repo = GEDCOMClass.Avoir_info_repo(ID_numero.ID);
                    if (info_repo != null)
                    {
                        if (GH.GHClass.Para.voir_date_changement && info_repo.N1_CHAN_liste != null)
                            foreach (GEDCOMClass.CHANGE_DATE info_date in info_repo.N1_CHAN_liste)
                            {
                                if (GH.GHClass.Para.voir_note)
                                {
                                    (liste_note_STRUCTURE_ID_numero, modifier) =
                                        Verifier_liste(info_date.N1_CHAN_NOTE_STRUCTURE_ID_liste, liste_note_STRUCTURE_ID_numero);
                                    if (modifier)
                                        boucler = true;
                                }
                            }
                    }
                }
            }
            return (
                liste_MULTIMEDIA_ID_numero,
                liste_source_ID_numero,
                liste_repo_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero,
                boucler);
        }

        private static (
            List<ID_numero>,
            List<ID_numero>,
            List<ID_numero>,
            List<ID_numero>,
            List<ID_numero>,
            bool)
            Genere_liste_reference_depot(
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            bool modifier;
            bool boucler = false;
            if (!GH.GHClass.Para.voir_reference)
                return (
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    false);
            foreach (ID_numero repo in liste_repo_ID_numero)
            {
                GEDCOM.GEDCOMClass.REPOSITORY_RECORD info_repo = GEDCOMClass.Avoir_info_repo(repo.ID);
                if (info_repo != null)
                {
                    // note d'adresse
                    if (GH.GHClass.Para.voir_note)
                        foreach (GEDCOMClass.ADDRESS_STRUCTURE info_adresse in info_repo.N1_ADDR_liste)
                        {
                            (liste_note_STRUCTURE_ID_numero, modifier) = Verifier_liste
                                (
                                    info_adresse.N1_NOTE_STRUCTURE_liste_ID,
                                    liste_note_STRUCTURE_ID_numero
                                );
                            if (modifier)
                                boucler = true;
                        }
                    // Note
                    if (GH.GHClass.Para.voir_note && R.IsNotNullOrEmpty(info_repo.N1_NOTE_STRUCTURE_liste_ID))
                    {
                        (liste_note_STRUCTURE_ID_numero, modifier) =
                           Verifier_liste(info_repo.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                    }
                    // CHAN
                    if (GH.GHClass.Para.voir_date_changement && R.IsNotNullOrEmpty(info_repo.N1_CHAN_liste))
                    {
                        foreach (GEDCOMClass.CHANGE_DATE info_date in info_repo.N1_CHAN_liste)
                        {
                            if (GH.GHClass.Para.voir_note && R.IsNotNullOrEmpty(info_date.N1_CHAN_NOTE_STRUCTURE_ID_liste))
                            {
                                (liste_note_STRUCTURE_ID_numero, modifier) =
                                    Verifier_liste(info_date.N1_CHAN_NOTE_STRUCTURE_ID_liste, liste_note_STRUCTURE_ID_numero);
                            }
                        }
                    }
                }
            }
            return (
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_repo_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero,
                boucler);
        }

        private static (
            List<MULTIMEDIA_ID_numero>,  // liste_MULTIMEDIA_ID_numero,
            List<ID_numero>, // liste_citation_ID_numero
            List<ID_numero>, // liste_source_ID_numero
            List<ID_numero>, // liste_repo_ID_numero
            List<ID_numero>, // liste_note_STRUCTURE_ID_numero,
            List<ID_numero> // liste_NOTE_RECORD_ID_numero
            )
            Genere_liste_reference_nom(
            List<GEDCOM.GEDCOMClass.PERSONAL_NAME_STRUCTURE> info_nom_liste,
            List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + " à <b>" + Avoir_nom_method() + "</b>");
            if (info_nom_liste == null)
                return (
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero
                    );
            foreach (GEDCOM.GEDCOMClass.PERSONAL_NAME_STRUCTURE info_nom in info_nom_liste)
            {
                // citation et source sur le nom
                if (GH.GHClass.Para.voir_reference)
                {
                    if (info_nom.N1_PERSONAL_NAME_PIECES != null)
                    {
                        (liste_citation_ID_numero, _) = Verifier_liste(
                            info_nom.N1_PERSONAL_NAME_PIECES.Nn_SOUR_citation_liste_ID,
                            liste_citation_ID_numero);


                        (liste_source_ID_numero, _) = Verifier_liste(
                           info_nom.N1_PERSONAL_NAME_PIECES.Nn_SOUR_source_liste_ID,
                           liste_source_ID_numero);
                    }
                }
                // note sur le nom
                if (GH.GHClass.Para.voir_note)
                    if (info_nom.N1_PERSONAL_NAME_PIECES != null)
                        (liste_note_STRUCTURE_ID_numero, _) = Verifier_liste(
                            info_nom.N1_PERSONAL_NAME_PIECES.Nn_NOTE_STRUCTURE_liste_ID,
                            liste_note_STRUCTURE_ID_numero);
                // citatation de FONE
                if (GH.GHClass.Para.voir_reference)
                    if (info_nom.N1_FONE_name_pieces != null)
                    {
                        (liste_citation_ID_numero, _) = Verifier_liste(info_nom.N1_FONE_name_pieces.Nn_SOUR_citation_liste_ID,
                        liste_citation_ID_numero);
                    }
                // note sur le FONE
                if (GH.GHClass.Para.voir_note)
                    if (info_nom.N1_FONE_name_pieces != null)
                        (liste_note_STRUCTURE_ID_numero, _) = Verifier_liste(info_nom.N1_FONE_name_pieces.Nn_NOTE_STRUCTURE_liste_ID,
                            liste_note_STRUCTURE_ID_numero);
                // citatation de ROMN
                if (GH.GHClass.Para.voir_reference)
                    if (info_nom.N1_ROMN_name_pieces != null)
                        (liste_citation_ID_numero, _) = Verifier_liste(info_nom.N1_ROMN_name_pieces.Nn_SOUR_citation_liste_ID,
                            liste_citation_ID_numero);
                // note sur le ROMN
                if (GH.GHClass.Para.voir_note)
                    if (info_nom.N1_ROMN_name_pieces != null)
                        (liste_note_STRUCTURE_ID_numero, _) = Verifier_liste(info_nom.N1_ROMN_name_pieces.Nn_NOTE_STRUCTURE_liste_ID,
                            liste_note_STRUCTURE_ID_numero);
            }
            return (
                liste_MULTIMEDIA_ID_numero,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_repo_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero);

        }

        private static (
        List<ID_numero>, // liste_citation_ID_numero
        List<ID_numero>, // liste_source_ID_numero
        List<ID_numero>, // liste_repo_ID_numero
        List<ID_numero>, // liste_note_STRUCTURE_ID_numero,
        List<ID_numero>, // liste_NOTE_RECORD_ID_numero
        bool             // modifier
        )
        Genere_liste_reference_NOTE_RECORD(
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            if (GH.GHClass.Para.voir_note && R.IsNullOrEmpty(liste_NOTE_RECORD_ID_numero))
                return (
                   liste_citation_ID_numero,
                   liste_source_ID_numero,
                   liste_repo_ID_numero,
                   liste_note_STRUCTURE_ID_numero,
                   liste_NOTE_RECORD_ID_numero,
                   false);
            bool modifier;
            bool boucler = false;
            for (int f = 0; f < liste_NOTE_RECORD_ID_numero.Count; f++)
            {
                GEDCOMClass.NOTE_RECORD info_NOTE_RECORD = GEDCOMClass.Avoir_Info_Note(liste_NOTE_RECORD_ID_numero[f].ID);
                if (info_NOTE_RECORD != null)
                {
                    (liste_note_STRUCTURE_ID_numero, modifier) =
                        Verifier_liste(info_NOTE_RECORD.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                    if (modifier)
                        boucler = true;
                    (liste_citation_ID_numero, modifier) =
                        Verifier_liste(info_NOTE_RECORD.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                    if (modifier)
                        boucler = true;
                    if (modifier)
                        boucler = true;
                    (liste_source_ID_numero, modifier) =
                        Verifier_liste(info_NOTE_RECORD.N1_SOUR_source_liste_ID, liste_source_ID_numero);
                    if (modifier)
                        boucler = true;
                    // CHAN
                    if (GH.GHClass.Para.voir_date_changement && R.IsNotNullOrEmpty(info_NOTE_RECORD.N1_CHAN_liste))
                    {
                        foreach (GEDCOMClass.CHANGE_DATE info_date in info_NOTE_RECORD.N1_CHAN_liste)
                        {
                            if (R.IsNotNullOrEmpty(info_date.N1_CHAN_NOTE_STRUCTURE_ID_liste))
                            {
                                (liste_note_STRUCTURE_ID_numero, modifier) =
                                    Verifier_liste(info_date.N1_CHAN_NOTE_STRUCTURE_ID_liste, liste_note_STRUCTURE_ID_numero);
                            }
                        }
                    }
                }
            }
            return (
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    boucler);
        }

        private static (
            List<ID_numero>, // liste_citation_ID_numero,
            List<ID_numero>, // liste_source_ID_numero,
            List<ID_numero>, // liste_repo_ID_numero,
            List<ID_numero>, // liste_note_STRUCTURE_ID_numero,
            List<ID_numero>, // liste_NOTE_RECORD_ID_numero,
            bool             // modifier,
            )
        Genere_liste_reference_NOTE_STRUCTURE(
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            if (!GH.GHClass.Para.voir_note && R.IsNullOrEmpty(liste_note_STRUCTURE_ID_numero))
            {
                return (
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    false);
            }
            bool modifier = false;
            bool boucler = false;
            for (int f = 0; f < liste_note_STRUCTURE_ID_numero.Count; f++)
            {
                GEDCOMClass.NOTE_STRUCTURE info_note_STRUCTURE = GEDCOMClass.Avoir_Info_NOTE_STRUCTURE(liste_note_STRUCTURE_ID_numero[f].ID);
                if (info_note_STRUCTURE != null)
                {
                    // note record
                    (liste_NOTE_RECORD_ID_numero, modifier) = Verifier_liste
                        (
                            info_note_STRUCTURE.N0_ID_RECORD,
                             liste_NOTE_RECORD_ID_numero
                        );
                    if (modifier)
                        boucler = true;

                    // note de la note
                    (liste_note_STRUCTURE_ID_numero, modifier) = Verifier_liste
                        (
                            info_note_STRUCTURE.N1_NOTE_STRUCTURE_liste_ID,
                            liste_note_STRUCTURE_ID_numero
                        );
                    if (modifier)
                        boucler = true;

                    // citation et souce de la note
                    if (GH.GHClass.Para.voir_reference)
                    {
                        // citation de la note
                        (liste_citation_ID_numero, modifier) = Verifier_liste
                            (
                                info_note_STRUCTURE.N1_SOUR_citation_liste_ID,
                                liste_citation_ID_numero
                            );
                        if (modifier)
                            boucler = true;
                        // source de la note
                        (liste_source_ID_numero, modifier) = Verifier_liste
                            (
                                info_note_STRUCTURE.N1_SOUR_source_liste_ID,
                                liste_source_ID_numero
                            );
                        if (modifier)
                            boucler = true;

                    }
                }
                if (modifier)
                    boucler = true;
            }
            //foreach (Retourne ID_numero a in liste_NOTE_RECORD_ID_numero) R..Z("Genere_liste_reference_NOTE_STRUCTURE retoune liste_NOTE_RECORD_ID_numero " + a.numero + " " + a.ID);
            return (
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    boucler);
        }

        private static (
            List<MULTIMEDIA_ID_numero>,
            List<ID_numero>,
            List<ID_numero>,
            List<ID_numero>,
            List<ID_numero>,
            List<ID_numero>,
            bool)
        Genere_liste_reference_source(
            List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero
        //, [CallerLineNumber] int callerLineNumber = 0
        )
        {
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + " à <b>" + Avoir_nom_method() + "</b>");
            if (!GH.GHClass.Para.voir_reference)
                return (
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    false);
            bool modifier;
            bool boucler = false;

            for (int f = 0; f < liste_source_ID_numero.Count; f++)
            {
                // Si c'est une source
                if (GEDCOMClass.Si_info_source(liste_source_ID_numero[f].ID))
                {
                    GEDCOM.GEDCOMClass.SOURCE_RECORD info_source = GEDCOMClass.Avoir_info_source(liste_source_ID_numero[f].ID);
                    if (info_source == null)
                        return (
                            liste_MULTIMEDIA_ID_numero,
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_repo_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero,
                            boucler);
                    // SOUR ou EVEN de la source
                    foreach (string ID in info_source.N1_SOUR_EVEN_liste_ID)
                    {
                        (liste_source_ID_numero, modifier) = Verifier_liste(ID, liste_source_ID_numero);
                        if (modifier)
                            boucler = true;
                    }
                    // 
                    // Note de la souce
                    if (GH.GHClass.Para.voir_note)
                    {
                        (liste_note_STRUCTURE_ID_numero, modifier) =
                            Verifier_liste(info_source.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                        if (modifier)
                            boucler = true;
                    }
                    // REFS de la source
                    if (R.IsNotNullOrEmpty(info_source.N1_REFS_liste_ID))
                    {
                        (liste_source_ID_numero, modifier) = Verifier_liste(info_source.N1_REFS_liste_ID, liste_source_ID_numero);
                        if (modifier)
                            boucler = true;
                    }
                    // note de DATA
                    (liste_note_STRUCTURE_ID_numero, modifier) =
                            Verifier_liste(info_source.N2_DATA_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                    if (modifier)
                        boucler = true;
                    // media de la source
                    (liste_MULTIMEDIA_ID_numero, _) = Verifier_liste_MULTIMEDIA(info_source.MULTIMEDIA_LINK_liste_ID, liste_MULTIMEDIA_ID_numero);

                    // dépôt référence  de la source
                    if (R.IsNotNullOrEmpty(info_source.N1_REPO_liste))
                    {
                        foreach (GEDCOMClass.SOURCE_REPOSITORY_CITATION info_REPO in info_source.N1_REPO_liste)
                        {
                            // ID
                            if (R.IsNotNullOrEmpty(info_REPO.N0_ID))
                            {
                                List<string> liste_ID = new List<string>
                                {
                                    info_REPO.N0_ID
                                };
                                (liste_repo_ID_numero, modifier) = Verifier_liste(liste_ID, liste_repo_ID_numero);
                                if (modifier)
                                    boucler = true;
                            }
                            // note
                            if (GH.GHClass.Para.voir_note)
                            {
                                liste_note_STRUCTURE_ID_numero =
                                    Valider_liste_reference_note(info_REPO.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                            }
                        }
                    }
                    // CHAN  de la source
                    if (GH.GHClass.Para.voir_date_changement && info_source.N1_CHAN != null)
                    {
                        liste_note_STRUCTURE_ID_numero =
                            Valider_liste_reference_note(info_source.N1_CHAN.N1_CHAN_NOTE_STRUCTURE_ID_liste,
                                liste_note_STRUCTURE_ID_numero);
                    }
                    // texte de la source
                    if (R.IsNotNullOrEmpty(info_source.N1_TEXT_liste))
                    {
                        foreach (GEDCOMClass.TEXT_STRUCTURE info_texte in info_source.N1_TEXT_liste)
                        {
                            (liste_note_STRUCTURE_ID_numero, modifier) =
                                Verifier_liste(info_texte.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                            if (modifier)
                                boucler = true;
                        }
                    }

                }
                // Si c'est un event V5.3
                if (GEDCOMClass.Si_info_event(liste_source_ID_numero[f].ID))
                {
                    GEDCOMClass.EVEN_RECORD_53 info_event = GEDCOMClass.Avoir_info_event_53(liste_source_ID_numero[f].ID);
                    if (info_event == null)
                        return (
                            liste_MULTIMEDIA_ID_numero,
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_repo_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero,
                            boucler);
                    // Lieu PLAC
                    if (info_event.N2_EVEN_PLAC != null)
                    {
                        (liste_citation_ID_numero, modifier) = Verifier_liste(info_event.N2_EVEN_PLAC.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                        if (modifier)
                            boucler = true;
                    }
                    // Source de l'évènement
                    (liste_source_ID_numero, modifier) = Verifier_liste(info_event.N2_EVEN_SOUR_source_liste_ID, liste_source_ID_numero);
                    if (modifier)
                        boucler = true;
                    // Citation évènement
                    (liste_citation_ID_numero, modifier) = Verifier_liste(info_event.N2_EVEN_SOUR_citation_liste_ID, liste_citation_ID_numero);
                    if (modifier)
                        boucler = true;
                    // Note de l'évènement
                    if (GH.GHClass.Para.voir_note)
                    {
                        (liste_note_STRUCTURE_ID_numero, modifier) =
                            Verifier_liste(info_event.N2_EVEN_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                        if (modifier)
                            boucler = true;
                    }
                    // individu role de l'évènement
                    if (R.IsNotNullOrEmpty(info_event.N2_EVEN_ROLE))
                    {
                        if (info_event.N3_EVEN_ROLE_INDIVIDUAL != null)
                        {
                            // NAME
                            if (info_event.N3_EVEN_ROLE_INDIVIDUAL.N0_NAME_liste != null)
                            {
                                foreach (GEDCOMClass.NAME_STRUCTURE_53 info_NAME in info_event.N3_EVEN_ROLE_INDIVIDUAL.N0_NAME_liste)
                                {
                                    //citation
                                    (liste_citation_ID_numero, modifier) = Verifier_liste(
                                        info_NAME.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                                    if (modifier)
                                        boucler = true;
                                }
                            }
                            // evenement
                            if (R.IsNotNullOrEmpty(info_event.N3_EVEN_ROLE_INDIVIDUAL.N0_EVEN_liste))
                            {
                                if (info_event.N3_EVEN_ROLE_INDIVIDUAL.N0_NAME_liste != null)
                                {
                                    foreach (GEDCOMClass.NAME_STRUCTURE_53 info_NAME in info_event.N3_EVEN_ROLE_INDIVIDUAL.N0_NAME_liste)
                                    {
                                        //citation
                                        (liste_citation_ID_numero, modifier) = Verifier_liste(
                                            info_NAME.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                                        if (modifier)
                                            boucler = true;
                                    }
                                }
                                foreach (GEDCOMClass.EVEN_STRUCTURE_53 info_role_event in info_event.N3_EVEN_ROLE_INDIVIDUAL.N0_EVEN_liste)
                                {
                                    //citation
                                    (liste_citation_ID_numero, modifier) = Verifier_liste(
                                        info_role_event.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                                    if (modifier)
                                        boucler = true;
                                    // source
                                    (liste_source_ID_numero, modifier) = Verifier_liste(
                                        info_role_event.N1_SOUR_source_liste_ID, liste_source_ID_numero);
                                    if (modifier)
                                        boucler = true;
                                    // note
                                    if (GH.GHClass.Para.voir_note)
                                    {
                                        (liste_NOTE_RECORD_ID_numero, modifier) = Verifier_liste(
                                            info_role_event.N1_NOTE_STRUCTURE_liste_ID, liste_NOTE_RECORD_ID_numero);
                                        if (modifier)
                                            boucler = true;
                                    }
                                    // PLAC
                                    if (info_role_event.N1_PLAC != null)
                                    {
                                        (liste_citation_ID_numero, modifier) = Verifier_liste(
                                            info_role_event.N1_PLAC.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                                        if (modifier)
                                            boucler = true;
                                        (liste_source_ID_numero, modifier) = Verifier_liste(
                                         info_role_event.N1_PLAC.N1_SOUR_source_liste_ID, liste_source_ID_numero);
                                        if (modifier)
                                            boucler = true;
                                    }
                                }
                            }
                        }
                    }
                    // individu relation
                    if (info_event.N4_EVEN_ROLE_RELATIONSHIP_INDIVIDUAL != null)
                    {
                        // NAME
                        if (info_event.N4_EVEN_ROLE_RELATIONSHIP_INDIVIDUAL.N0_NAME_liste != null)
                        {
                            foreach (GEDCOMClass.NAME_STRUCTURE_53 info_NAME in info_event.N4_EVEN_ROLE_RELATIONSHIP_INDIVIDUAL.N0_NAME_liste)
                            {
                                //citation
                                (liste_citation_ID_numero, modifier) = Verifier_liste(
                                    info_NAME.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                                if (modifier)
                                    boucler = true;
                            }
                        }
                        // evenement
                        if (R.IsNotNullOrEmpty(info_event.N4_EVEN_ROLE_RELATIONSHIP_INDIVIDUAL.N0_EVEN_liste))
                        {
                            foreach (GEDCOMClass.EVEN_STRUCTURE_53 info_rela_even in info_event.N4_EVEN_ROLE_RELATIONSHIP_INDIVIDUAL.N0_EVEN_liste)
                            {
                                // Name

                                //citation
                                (liste_citation_ID_numero, modifier) = Verifier_liste(
                                    info_rela_even.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                                if (modifier)
                                    boucler = true;
                                // source
                                (liste_source_ID_numero, modifier) = Verifier_liste(
                                    info_rela_even.N1_SOUR_source_liste_ID, liste_source_ID_numero);
                                if (modifier)
                                    boucler = true;
                                // note
                                if (GH.GHClass.Para.voir_note)
                                {
                                    (liste_NOTE_RECORD_ID_numero, modifier) = Verifier_liste(
                                    info_rela_even.N1_NOTE_STRUCTURE_liste_ID, liste_NOTE_RECORD_ID_numero);
                                    if (modifier)
                                        boucler = true;
                                }
                                // PLAC
                                if (info_rela_even.N1_PLAC != null)
                                {
                                    (liste_citation_ID_numero, modifier) = Verifier_liste(
                                        info_rela_even.N1_PLAC.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                                    if (modifier)
                                        boucler = true;
                                    (liste_source_ID_numero, modifier) = Verifier_liste(
                                     info_rela_even.N1_PLAC.N1_SOUR_source_liste_ID, liste_source_ID_numero);
                                    if (modifier)
                                        boucler = true;
                                }
                            }
                        }
                    }
                }
            }
            return (
                liste_MULTIMEDIA_ID_numero,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_repo_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero,
                boucler);
        }
        private static (
            List<ID_numero>, // liste_SUBMITTER_ID_numero,
            List<MULTIMEDIA_ID_numero>,
            List<ID_numero>, // liste_citation_ID_numero,
            List<ID_numero>, // liste_source_ID_numero,
            List<ID_numero>, // liste_repo_ID_numero,
            List<ID_numero>, // liste_note_STRUCTURE_ID_numero,
            List<ID_numero>, // liste_NOTE_RECORD_ID_numero,
            bool // modifier
        ) Genere_liste_reference_tous(
            List<ID_numero> liste_SUBMITTER_ID_numero,
            List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + " à <b>" + Avoir_nom_method() + "</b>");
            bool boucler;
            bool modifier;
            do
            {
                boucler = false;
                // Groupe Chercheur
                if (GH.GHClass.Para.voir_chercheur)
                {
                    (
                        liste_SUBMITTER_ID_numero,
                        liste_MULTIMEDIA_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        modifier) =
                    Genere_liste_reference_chercheur(
                            liste_SUBMITTER_ID_numero,
                            liste_MULTIMEDIA_ID_numero,
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_repo_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero);
                    if (modifier)
                        boucler = true;
                }
                if (GH.GHClass.Para.voir_reference)
                {
                    // Groupe Citation
                    (
                        liste_MULTIMEDIA_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        modifier) =
                    Genere_liste_reference_citation(
                        liste_MULTIMEDIA_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero);
                    if (modifier)
                        boucler = true;
                    // GROUPE SOURCE
                    (
                        liste_MULTIMEDIA_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        modifier) =
                    Genere_liste_reference_source(
                        liste_MULTIMEDIA_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero);
                    if (modifier)
                        boucler = true;
                    foreach (ID_numero a in liste_source_ID_numero)
                        // Groupe Dépôt
                        (

                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_repo_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero,
                            modifier) =
                        Genere_liste_reference_depot(
                                liste_citation_ID_numero,
                                liste_source_ID_numero,
                                liste_repo_ID_numero,
                                liste_note_STRUCTURE_ID_numero,
                                liste_NOTE_RECORD_ID_numero
                                );
                    if (modifier)
                        boucler = true;
                }
                if (GH.GHClass.Para.voir_note)
                {
                    // note structure
                    (
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        modifier
                    ) =
                    Genere_liste_reference_NOTE_STRUCTURE(
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero
                        );
                    if (modifier)
                        boucler = true;
                    // Groupe note
                    (
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        modifier) =
                    Genere_liste_reference_NOTE_RECORD(
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_repo_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero
                            );
                    if (modifier)
                        boucler = true;
                }

                // media
                (
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    modifier
                    ) =
                Genere_liste_reference_MULTIMEDIA(
                        liste_MULTIMEDIA_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero
                        );
                if (modifier)
                    boucler = true;

            } while (boucler != false);
            GC.Collect();
            return (
                liste_SUBMITTER_ID_numero,
                liste_MULTIMEDIA_ID_numero,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_repo_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero,
                modifier
                );
        }

        private static (
            List<MULTIMEDIA_ID_numero>, // liste de numéro_ID_numero
            List<ID_numero>, // liste_citation_ID_numero
            List<ID_numero>, // liste_source_ID_numero
            List<ID_numero>, // liste_note_STRUCTURE_ID_numero
            List<ID_numero>, // liste_NOTE_RECORD_ID_numero
            bool             // modifier
        ) Genere_liste_reference_MULTIMEDIA
            (
                List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero,
                List<ID_numero> liste_citation_ID_numero,
                List<ID_numero> liste_source_ID_numero,
                List<ID_numero> liste_note_STRUCTURE_ID_numero,
                List<ID_numero> liste_NOTE_RECORD_ID_numero
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);

            Application.DoEvents();
            bool boucler = false;
            bool modifier;
            if (!GH.GHClass.Para.voir_media || liste_MULTIMEDIA_ID_numero == null)
                return (
liste_MULTIMEDIA_ID_numero,
liste_citation_ID_numero,
liste_source_ID_numero,
liste_note_STRUCTURE_ID_numero,
liste_NOTE_RECORD_ID_numero,
false
);

            foreach (MULTIMEDIA_ID_numero info_MULTIMEDIA_ID_numero in liste_MULTIMEDIA_ID_numero)
            {
                if (info_MULTIMEDIA_ID_numero.ID_LINK != null)
                {
                    // avoir l'info du LINK
                    GEDCOMClass.MULTIMEDIA_LINK info_LINK = GEDCOMClass.Avoir_info_MULTIMEDIA_LINK(info_MULTIMEDIA_ID_numero.ID_LINK);

                    // note
                    if (GH.GHClass.Para.voir_note)
                    {
                        (liste_note_STRUCTURE_ID_numero, modifier) =
                            Verifier_liste(info_LINK.NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                        if (modifier)
                            boucler = true;
                    }

                    // citation
                    if (GH.GHClass.Para.voir_reference)
                    {
                        (liste_citation_ID_numero, modifier) =
                            Verifier_liste(info_LINK.SOUR_citation_liste_ID, liste_citation_ID_numero);
                        if (modifier)
                            boucler = true;
                    }

                }
                if (info_MULTIMEDIA_ID_numero.ID_RECORD != null)
                {
                    // avoir l'info du LINK
                    GEDCOMClass.MULTIMEDIA_RECORD info_record = GEDCOMClass.Avoir_info_MULTIMEDIA_RECORD(info_MULTIMEDIA_ID_numero.ID_RECORD);

                    // note
                    if (GH.GHClass.Para.voir_note && info_record != null)
                    {
                        (liste_note_STRUCTURE_ID_numero, modifier) =
                            Verifier_liste(info_record.NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                        if (modifier)
                        {
                            boucler = true;
                        }
                    }

                    // citation
                    if (GH.GHClass.Para.voir_reference && info_record != null)
                    {
                        (liste_citation_ID_numero, modifier) =
                            Verifier_liste(info_record.SOUR_citation_liste_ID, liste_citation_ID_numero);
                        if (modifier)
                            boucler = true;
                        (liste_source_ID_numero, modifier) =
                            Verifier_liste(info_record.SOUR_source_liste_ID, liste_source_ID_numero);
                        if (modifier)
                            boucler = true;
                    }
                }
            }
            GC.Collect();
            return (
                liste_MULTIMEDIA_ID_numero,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero,
                boucler);
        }

        private static (
            List<ID_numero>, // liste_SUBMITTER_ID_numero
            List<MULTIMEDIA_ID_numero>, // liste_MULTIMEDIA_ID_numero
            List<ID_numero>, // liste_citation_ID_numero
            List<ID_numero>, // liste_source_ID_numero
            List<ID_numero>, // liste_repo_ID_numero
            List<ID_numero>, // liste_note_STRUCTURE_ID_numero,
            List<ID_numero>, // liste_NOTE_RECORD_ID_numero
            bool             // modifier
            )
            Genere_liste_reference_chercheur(
            List<ID_numero> liste_SUBMITTER_ID_numero,
            List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            bool boucler = false;
            bool modifier;
            if (!GH.GHClass.Para.voir_media)
                return (
liste_SUBMITTER_ID_numero,
liste_MULTIMEDIA_ID_numero,
liste_citation_ID_numero,
liste_source_ID_numero,
liste_repo_ID_numero,
liste_note_STRUCTURE_ID_numero,
liste_NOTE_RECORD_ID_numero,
false
);
            if (R.IsNotNullOrEmpty(liste_SUBMITTER_ID_numero) && liste_SUBMITTER_ID_numero.Any())
            {
                foreach (ID_numero item in liste_SUBMITTER_ID_numero)
                {
                    GEDCOMClass.SUBMITTER_RECORD info_chercheur = GEDCOMClass.Avoir_info_SUBMITTER_RECORD(item.ID);
                    if (info_chercheur != null)
                    {
                        // note et source sur le nom
                        (
                        liste_MULTIMEDIA_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero
                        ) =
                        Genere_liste_reference_nom(
                            info_chercheur.N1_NAME_liste,
                            liste_MULTIMEDIA_ID_numero,
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_repo_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero
                            );
                        Verifier_liste_MULTIMEDIA(
                            info_chercheur.MULTIMEDIA_LINK_liste_ID,
                            liste_MULTIMEDIA_ID_numero
                            );
                        // note chercheur
                        // note de ADDR
                        foreach (GEDCOMClass.ADDRESS_STRUCTURE info_adresse in info_chercheur.N1_ADDR_liste)
                        {
                            if (GH.GHClass.Para.voir_note)
                            {
                                (liste_note_STRUCTURE_ID_numero, modifier) = Verifier_liste
                                    (
                                        info_adresse.N1_NOTE_STRUCTURE_liste_ID,
                                        liste_note_STRUCTURE_ID_numero
                                    );
                                if (modifier)
                                    boucler = true;
                            }
                        }
                        // note de CHAM
                        if (GH.GHClass.Para.voir_date_changement && info_chercheur.N1_CHAN != null)
                        {
                            (liste_note_STRUCTURE_ID_numero, modifier) = Verifier_liste
                                (
                                    info_chercheur.N1_CHAN.N1_CHAN_NOTE_STRUCTURE_ID_liste,
                                    liste_note_STRUCTURE_ID_numero
                                );
                            if (modifier)
                                boucler = true;
                        }
                    }
                }
            }
            return (
                liste_SUBMITTER_ID_numero,
                liste_MULTIMEDIA_ID_numero,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_repo_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero,
                boucler
                );
        }

        private static (
            List<MULTIMEDIA_ID_numero>, //
            List<ID_numero>, // liste_citation_ID_numero
            List<ID_numero>, // liste_source_ID_numero
            List<ID_numero>, // liste_repo_ID_numero
            List<ID_numero>, // liste_note_STRUCTURE_ID_numero,
            List<ID_numero>, // liste_NOTE_RECORD_ID_numero
            bool             // modifier
            )
            Genere_liste_reference_citation(
            List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {

            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + " à <b>" + Avoir_nom_method() + "</b>");
            bool modifier = false;
            bool boucler = false;
            if (!GH.GHClass.Para.voir_reference || liste_citation_ID_numero.Count == 0)
                return (
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    modifier);

            for (int f = 0; f < liste_citation_ID_numero.Count; f++)
            {
                GEDCOM.GEDCOMClass.SOURCE_CITATION info_citation = GEDCOMClass.Avoir_info_citation(liste_citation_ID_numero[f].ID);
                if (info_citation != null)
                {
                    if (GH.GHClass.Para.voir_note)
                    {
                        // DATA    
                        if (R.IsNotNullOrEmpty(info_citation.N2_DATA_TEXT_liste) && info_citation.N2_DATA_TEXT_liste.Any())
                        {
                            foreach (GEDCOMClass.TEXT_STRUCTURE info_texte in info_citation.N2_DATA_TEXT_liste)
                            {
                                // note de texte
                                if (GH.GHClass.Para.voir_note)
                                {
                                    (liste_note_STRUCTURE_ID_numero, modifier) =
                                        Verifier_liste(info_texte.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                                    if (modifier)
                                        boucler = true;
                                }
                            }
                        }
                        // note de citation
                        (liste_note_STRUCTURE_ID_numero, modifier) =
                            Verifier_liste(info_citation.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                        if (modifier)
                            boucler = true;
                        // recencement
                        if (R.IsNotNullOrEmpty(info_citation.N1_CENS_liste))
                        {
                            foreach (GEDCOMClass.CENS_record info_CENS in info_citation.N1_CENS_liste)
                            {
                                (liste_note_STRUCTURE_ID_numero, modifier) =
                                    Verifier_liste(info_CENS.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                                if (modifier)
                                    boucler = true;
                            }
                        }
                        // auteur
                        if (R.IsNotNullOrEmpty(info_citation.N1_ORIG_liste))
                        {
                            foreach (GEDCOMClass.ORIG_record info_ORIG in info_citation.N1_ORIG_liste)
                            {
                                foreach (string ID in info_ORIG.N1_NOTE_STRUCTURE_liste_ID)
                                {
                                    GEDCOMClass.NOTE_STRUCTURE info_note_STRUCTURE = GEDCOMClass.Avoir_Info_NOTE_STRUCTURE(ID);
                                    (liste_note_STRUCTURE_ID_numero, modifier) =
                                        Verifier_liste(info_note_STRUCTURE.N0_ID_RECORD, liste_note_STRUCTURE_ID_numero);
                                    if (modifier)
                                        boucler = true;
                                }
                                if (modifier)
                                    boucler = true;
                            }
                        }
                        // immigration
                        if (info_citation.N1_IMMI_record != null)
                        {
                            if (R.IsNotNullOrEmpty(info_citation.N1_IMMI_record.N1_NOTE_STRUCTURE_liste_ID))
                            {
                                (liste_note_STRUCTURE_ID_numero, modifier) =
                                    Verifier_liste(info_citation.N1_IMMI_record.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                                if (modifier)
                                    boucler = true;
                            }
                            if (modifier)
                                boucler = true;
                            // texte
                            if (R.IsNotNullOrEmpty(info_citation.N1_IMMI_record.N1_TEXT_liste))
                            {
                                foreach (GEDCOMClass.TEXT_STRUCTURE info_texte in info_citation.N1_IMMI_record.N1_TEXT_liste)
                                {
                                    (liste_note_STRUCTURE_ID_numero, modifier) =
                                        Verifier_liste(info_texte.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                                    if (modifier)
                                        boucler = true;
                                }
                            }
                        }
                        // texte
                        if (R.IsNotNullOrEmpty(info_citation.N1_TEXT_liste))
                        {
                            foreach (GEDCOMClass.TEXT_STRUCTURE info_texte in info_citation.N1_TEXT_liste)
                            {
                                //foreach (string ID in info_texte.N1_NOTE_STRUCTURE_liste_ID)
                                //{
                                //GEDCOMClass.NOTE_STRUCTURE info_note_STRUCTURE = GEDCOMClass.Avoir_Info_NOTE_STRUCTURE(ID);
                                (liste_note_STRUCTURE_ID_numero, modifier) =
                                    Verifier_liste(info_texte.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                                if (modifier)
                                    boucler = true;
                                //}
                                if (modifier)
                                    boucler = true;
                            }
                        }
                    }
                    // source
                    if (info_citation.N0_ID_SOUR != null)
                    {
                        (liste_source_ID_numero, modifier) = Verifier_liste(info_citation.N0_ID_SOUR, liste_source_ID_numero);
                        if (modifier)
                            boucler = true;
                    }
                    // TITL
                    if (R.IsNotNullOrEmpty(info_citation.N1_TITL_liste))
                    {
                        foreach (string info_TITL in info_citation.N1_TITL_liste)
                        {
                            string ID = GEDCOMClass.Extraire_ID(info_TITL);
                            if (R.IsNotNullOrEmpty(ID))
                            {
                                (liste_source_ID_numero, modifier) = Verifier_liste(ID, liste_source_ID_numero);
                                if (modifier)
                                    boucler = true;
                            }
                        }
                    }
                    // Source ID de la citation(première ligne)
                    (liste_source_ID_numero, modifier) = Verifier_liste(info_citation.N0_ID_SOUR, liste_source_ID_numero);
                    if (modifier)
                        boucler = true;
                    // liste source dans la citation
                    (liste_source_ID_numero, modifier) = Verifier_liste(info_citation.N1_SOUR_liste_ID, liste_source_ID_numero);
                    if (modifier)
                        boucler = true;
                    // REFS
                    if (R.IsNotNullOrEmpty(info_citation.N1_REFS_liste_ID))
                    {
                        (liste_source_ID_numero, modifier) = Verifier_liste(info_citation.N1_REFS_liste_ID, liste_source_ID_numero);
                    }
                    if (modifier)
                        boucler = true;

                    // repo
                    foreach (GEDCOMClass.SOURCE_REPOSITORY_CITATION item in info_citation.N1_REPO_liste)
                    {
                        (liste_repo_ID_numero, modifier) = Verifier_liste(item.N0_ID, liste_repo_ID_numero);
                        if (modifier)
                            boucler = true;
                    }
                    // media
                    (liste_MULTIMEDIA_ID_numero, _) = Verifier_liste_MULTIMEDIA(info_citation.MULTIMEDIA_LINK_liste_ID, liste_MULTIMEDIA_ID_numero);
                }
            }
            return (
                liste_MULTIMEDIA_ID_numero,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_repo_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero,
                boucler);
        }

        private static string Traduire_language(string langue)
        {
            if (langue == null)
                return null;
            langue = langue.ToLower();
            switch (langue)
            {
                case "afrikaans":
                    langue = "Afrikaans";
                    break;
                case "albanian":
                    langue = "Albanais";
                    break;
                case "anglo-saxon":
                    langue = "Anglo-Saxon";
                    break;
                case "catalan":
                    langue = "Catalan";
                    break;
                case "catalan spn":
                    langue = "Catalan Spn";
                    break;
                case "czech":
                    langue = "Tchèque";
                    break;
                case "danish":
                    langue = "Danois";
                    break;
                case "dutch":
                    langue = "Néerlandais";
                    break;
                case "anglais":
                    langue = "Anglais";
                    break;
                case "english":
                    langue = "Anglais";
                    break;
                case "esperanto":
                    langue = "Espéranto";
                    break;
                case "estonian":
                    langue = "Estonien";
                    break;
                case "faroese":
                    langue = "Féroïen";
                    break;
                case "finnish":
                    langue = "Finlandais";
                    break;
                case "français":
                    langue = "Français";
                    break;
                case "french":
                    langue = "Français";
                    break;
                case "german":
                    langue = "Allemand";
                    break;
                case "hawaiian":
                    langue = "Hawaïen";
                    break;
                case "hungarian":
                    langue = "Hongrois";
                    break;
                case "icelandic":
                    langue = "Islandais";
                    break;
                case "indonesian":
                    langue = "Indonésien";
                    break;
                case "italian":
                    langue = "Italien";
                    break;
                case "latvian":
                    langue = "Letton";
                    break;
                case "lithuanian":
                    langue = "Lituanien";
                    break;
                case "navaho":
                    langue = "Navaho";
                    break;
                case "norwegian":
                    langue = "Norvégien";
                    break;
                case "polish":
                    langue = "Polonais";
                    break;
                case "portuguese":
                    langue = "Portugais";
                    break;
                case "romanian":
                    langue = "Roumain";
                    break;
                case "serbo croa":
                    langue = "Serbo Croa";
                    break;
                case "slovak":
                    langue = "Slovaque";
                    break;
                case "slovene":
                    langue = "Slovène";
                    break;
                case "spanish":
                    langue = "Espagnol";
                    break;
                case "swedish":
                    langue = "Suédois";
                    break;
                case "turkish":
                    langue = "Turc";
                    break;
                case "wendic":
                    langue = "Wendic";
                    break;
                // other languages not supported until unicode	// Autres Langues Non Prises En Charge Jusqu'à Unicode
                case "amharic":
                    langue = "Amharique";
                    break;
                case "arabic":
                    langue = "Arabe";
                    break;
                case "armenian":
                    langue = "Arménien";
                    break;
                case "assamese":
                    langue = "Assamais";
                    break;
                case "belorusian":
                    langue = "Bélarus";
                    break;
                case "bengali":
                    langue = "Bengali";
                    break;
                case "braj":
                    langue = "Braj";
                    break;
                case "bulgarian":
                    langue = "Bulgare";
                    break;
                case "burmese":
                    langue = "Birman";
                    break;
                case "cantonese":
                    langue = "Cantonais";
                    break;
                case "church-slavic":
                    langue = "Église-Slave";
                    break;
                case "dogri":
                    langue = "Dogri";
                    break;
                case "georgian":
                    langue = "Géorgien";
                    break;
                case "greek":
                    langue = "Grec";
                    break;
                case "gujarati":
                    langue = "Gujarati";
                    break;
                case "hebrew":
                    langue = "Hébreu";
                    break;
                case "hindi":
                    langue = "Hindi";
                    break;
                case "japanese":
                    langue = "Japonais";
                    break;
                case "kannada":
                    langue = "Kannada";
                    break;
                case "khmer":
                    langue = "Khmer";
                    break;
                case "konkani":
                    langue = "Konkani";
                    break;
                case "korean":
                    langue = "Coréen";
                    break;
                case "lahnda":
                    langue = "Lahnda";
                    break;
                case "lao":
                    langue = "Laotien";
                    break;
                case "macedonian":
                    langue = "Macédonie";
                    break;
                case "maithili":
                    langue = "Maithili";
                    break;
                case "malayalam":
                    langue = "Malayalam";
                    break;
                case "mandrin":
                    langue = "Mandrin";
                    break;
                case "manipuri":
                    langue = "Manipuri";
                    break;
                case "marathi":
                    langue = "Marathi";
                    break;
                case "mewari":
                    langue = "Mewari";
                    break;
                case "nepali":
                    langue = "Népalais";
                    break;
                case "oriya":
                    langue = "Oriya";
                    break;
                case "pahari":
                    langue = "Pahari";
                    break;
                case "pali":
                    langue = "Pali";
                    break;
                case "panjabi":
                    langue = "Panjabi";
                    break;
                case "persian":
                    langue = "Persan";
                    break;
                case "prakrit":
                    langue = "Prakrit";
                    break;
                case "pusto":
                    langue = "Pusto";
                    break;
                case "rajasthani":
                    langue = "Rajasthan";
                    break;
                case "russian":
                    langue = "Russe";
                    break;
                case "sanskrit":
                    langue = "Sanskrit";
                    break;
                case "serb":
                    langue = "Serbe";
                    break;
                case "tagalog":
                    langue = "Tagalog";
                    break;
                case "tamil":
                    langue = "Tamil";
                    break;
                case "telugu":
                    langue = "Telugu";
                    break;
                case "thai":
                    langue = "Thaïlandais";
                    break;
                case "tibetan":
                    langue = "Tibétain";
                    break;
                case "ukrainian":
                    langue = "Ukrainien";
                    break;
                case "urdu":
                    langue = "Ourdou";
                    break;
                case "vietnamese":
                    langue = "Vietnamien";
                    break;
                case "yiddish":
                    langue = "Yiddish";
                    break;
            }
            return langue;
        }
        private static string Convertir_MEDIA_TYPE(string s
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            string mot = s.ToUpper();
            switch (mot)
            {
                case "CARD":
                    return "Fiche";
                case "AUDIO":
                    return "Audio";
                case "BOOK":
                    return "Livre";
                case "ELECTRONIC":
                    return "Électronique";
                case "FICHE":
                    return "Fiche";
                case "FILM":
                    return "Film";
                case "MAGAZINE":
                    return "Magazine";
                case "MANUSCRIPT":
                    return "Manucrit";
                case "MAP":
                    return "Carte géographique";
                case "NEWSPAPER":
                    return "Journal";
                case "PHOTO":
                    return "Photographie";
                case "TOMBSTONE":
                    return "Pierre tombale";
                case "VIDEO":
                    return "Vidéo";
                case "UNPUBLISHED":
                    return "Non publié";
            }
            return s;
        }

        private static string Traduire_mot_anglais(string texte, bool majuscule)
        {
            if (R.IsNullOrEmpty(texte))
                return null;

            string[,] liste = new string[,] {
                { "AUTHOR",         "Auteur",       "auteur"},
                { "COMPILER",       "Compilateur",  "compilateur"},
                { "TRANSCRIBER",    "Transcriveur", "transcriveur"},
                { "ABSTRACTOR",     "Astracteur",   "astracteur"},
                { "EDITOR",         "Éditeur",      "éditeur"},
                { "INFORMANT",      "Informateur",  "informateur"},
                { "INTERVIEWER",    "Intervieweur", "intervieweur"},
                { "GOVERNMENT",     "Gouvernement", "gouvernement"},
                { "BUSINESS",       "Affaire",      "affaire"},
                { "ORGANIZATION",   "Organisation", "organisation"},

                // évènement
                { "ADOP",           "Adoption",     "adoption"},
                { "ANUL",           "Annulation de mariage","annulation de mariage"},
                { "BAPL",           "Baptême SDJ",  "baptême SDJ"},
                { "BAPM",           "Baptême",      "baptême"},
                { "BARM",           "Bar Mitzvah",  "Bar Mitzvah"},
                { "BASM",           "Bat Mitsva",   "Bat Mitsva"},
                { "BIRT",           "Naissance",    "naissance"},
                { "BLES",           "Bénédiction",  "bénédiction"},
                { "BURI",           "Inhumation",   "Inhumation"},
                { "BUYR",           "Acheteur",     "acheteur"},
                { "CAST",           "Caste",        "caste"},
                { "CENS",           "Recensement",  "recensement"},
                { "CHIL",           "Enfant",       "Enfant"},
                { "CHR",            "Baptême christianisme","baptême"},
                { "CHRA",           "Baptême adulte","baptême adulte"},
                { "CONF",           "Confirmation", "confirmation"},
                { "CONL",           "Confirmation SDJ","confirmation SDJ"},
                { "CREM",           "Crémation",    "crémation"},
                { "DEAT",           "Décès",        "décès" },
                { "DIV" ,           "Divorce",      "Divorce"},
                { "DIVF",           "Demande de divorce par épouse","Demande de divorce par épouse"},
                { "DSCR",           "Description physique","Description physique"},
                { "EDUC",           "Éducation ",   "éducation"},
                { "EMIG",           "Immigration",  "immigration"},
                { "ENDL",           "Dotation SDJ.","dotation"},
                { "ENGA",           "Fiançailles",  "Fiançailles"},
                { "EVEN",           "Évènement ",   "évènement"},
                { "FACT",           "Fait",         "fait"},
                { "FATH",           "Père",         "père"},
                { "FCOM",           "Première communion","première communion"},
                { "GODP",           "Parrain/Marainne","parrain/marainne"},
                { "GRAD",           "Graduation",   "graduation"},
                { "HDOH",           "Chef de maison","Chef de maison"},
                { "HDOG",           "HDOG",         "HDOG"},
                { "HEIR",           "Héritier",     "héritier"},
                { "HFAT",           "Père du conjoint","père du conjoint"},
                { "HMOT",           "Mère du conjoint","mère du conjoint"},
                { "HUSB",           "Conjoint",     "conjoint"},
                { "IDNO",           "Numéro ID nationnal","numéro ID nationnal"},
                { "IMMI",           "Immigration",  "immigration"},
                { "INDI",           "Individu",     "individu"},
                { "INDO",           "Numéro d'identification","numéro d'identification"},
                { "INFT",           "Informateur",  "informateur"},
                { "LEGA",           "Légataire",    "légataire"},
                { "MARB",           "Publication des bans","publication des bans"},
                { "MARC",           "Contrat de mariage","contrat de mariage"},
                { "MARL",           "Licence de mariage","licence de mariage"},
                { "MARR",           "Mariage",      "mariage"},
                { "MARS",           "Contrat de mariage","contrat de mariage"},
                { "MEMBER",         "Membre",       "membre"},
                { "MILI",           "Militaire",    "militaire"},
                { "MOTH",           "Mère",         "mère"},
                { "NATI",           "Nationalité ", "nationalité"},
                { "NATU",           "Naturalization","naturalization"},
                { "NCHI",           "Nombre d'enfant","nombre d'enfant"},
                { "NMR",            "Nombre de mariage","nombre de mariage"},
                { "OCCU",           "Profession",   "profession"},
                { "OFFI",           "Officiel",     "officiel"},
                { "ORDN",           "Ordination","ordination"},
                { "PARE",           "PARE",         "PARE"},
                { "PHUS",           "Conjoint précédent","conjoint précédent"},
                { "PROB",           "Homologation testament","Homologation testament"},
                { "PROP",           "Propriété",    "propriété"},
                { "PWIF",           "Conjointe précédeant","conjointe précédeant"},
                { "RECO",           "Enregistreur", "enregistreur"},
                { "REL",            "Relatif",      "relatif"},
                { "RELI",           "Religion",     "religion"},
                { "RESI",           "Résidence",    "résidence"},
                { "RETI",           "Retraite",     "retraite"},
                { "ROLE",           "Rôle",         "rôle"},
                { "SELR",           "Vendeur",      "vendeur"},
                { "SLGC",           "Scellement SDJ","scellement SDJ"},
                { "SPOU",           "Conjointe",    "conjointe"},
                { "SSN",            "Numéro sécurité sociale","numéro sécurité sociale"},
                { "TITL",           "Titre",        "titre"},
                { "TXPY",           "Taxe payer",   "taxe payer"},
                { "WAC",            "Ordonnance SDJ","ordonnance SDJ"},
                { "WFAT",           "WFTA",         "WFTA"},
                { "WIFE",           "Conjointe",    "conjointe"},
                { "WILL",           "Testament",    "testament"},
                { "WITN",           "Témoin",       "témoin"},
                { "WMOT",           "Père de la conjointe","père de la conjointe"},
                { "_ELEC",          "Élection",     "élection"},
                { "_MDCL",          "Information médicale","information médicale"},
                { "_MILT",          "Service militaire","Service militaire"}, // GRAMPS

                // media
                { "CARD",           "Fiche",        "fiche"},
                { "AUDIO",          "Audio",        "audio"},
                { "BOOK",           "Livre",        "livre"},
                { "ELECTRONIC",     "Électronique", "électronique"},
                { "FICHE",          "Fiche",        "fiche"},
                { "FILM",           "Film",         "film" },
                { "MAGAZINE",       "Magazine",     "magazine"},
                { "MANUSCRIPT",     "Manucrit",     "manucrit" },
                { "MAP",            "Carte géographique", "carte géographique"},
                { "NEWSPAPER",      "Journal",      "journal" },
                { "PHOTO",          "Photographie", "photographie" },
                { "TOMBSTONE",      "Pierre tombale","Pierre tombale" },
                { "VIDEO",          "Vidéo",        "Vidéo" },
                { "UNPUBLISHED",    "Non publié",   "Non publié" }
        };
            //int a = liste.Count();
            int ranger = liste.GetLength(0);
            for (int f = 0; f < ranger; f++)
            {
                if (majuscule)
                    texte = Regex.Replace(texte, liste[f, 0], liste[f, 1], RegexOptions.IgnoreCase);
                else
                    texte = Regex.Replace(texte, liste[f, 0], liste[f, 2], RegexOptions.IgnoreCase);
            }
            return texte;


        }


        private static string Traduire_QUAY(string s)
        {
            switch (s)
            {
                case "0":
                    return "Des preuves ou des données non fiables ont été estimées.";
                case "1":
                    return "Preuve directe ou primaire avec une question de fiabilité ou potentiel de biais, par exemple une autobiographie).";
                case "2":
                    return "Preuve secondaire.";
                case "3":
                    return "Preuve directe et primaire utilisée, ou par dominance de la preuve.";
            }
            return s;
        }
        private static string Traduire_relation_conjoint(string s)
        {
            switch (s.ToLower())
            {
                case "civil":
                    return "Mariage civil";
                case "cohabitation":
                    return " Cohabitation";
                case "common law":
                    return "Mariage de fait en union libre";
                case "extraconjugal_relation":
                    return "Relation extraconjugale";
                case "living apart together":
                    return "Vivre séparément ensemble";
                case "living together":
                    return "Living together";
                case "marriage":
                    return "Marriage";
                case "not married":
                    return "Pas marié";
                case "partnership":
                    return "Partenariat";
                case "registered partnership":
                    return "Partenariat enregistré";
                case "religious":
                    return " Mariage religieux";
                case "separated":
                    return "Séparé";
                case "unknown":
                    return "Inconnue";
            }
            return s;
        }
        private static string Convertir_TAG_texte(string s)
        {
            switch (s.ToUpper())
            {
                case "BUYR":
                    return "Acheteur";
                case "BROT":
                    return "Frère";
                case "CHIL":
                    return "Enfant";
                case "FATH":
                    return "Père";
                case "GODP":
                    return "Parrain/maraine";
                case "HDOH":
                    return "Chef de ménage";
                case "HDOG":
                    return "HDOG";
                case "HEIR":
                    return "A hérité ou a le droit d'hériter d'une succession";
                case "HFAT":
                    return "Agissant en tant que père du mari pour l'évènement";
                case "HUSB":
                    return "Conjoint";
                case "HMOT":
                    return "Agissant en tant que mère du mari pour l'évènement";
                case "INFT":
                    return "Rapporté des faits concernant l'évènement";
                case "LEGA":
                    return "Légale";
                case "MEMBER":
                    return "Membre";
                case "MOTH":
                    return "Mère";
                case "OFFICIATOR":
                    return "Officiel";
                case "PARE":
                    return "PARE";
                case "PHUS":
                    return "Conjoint précédent"; // An individual in the role of the principal's previous husband for a cited event.
                case "PWIF":
                    return "Conjointe précédent"; // An individual in the role of the principal's previous wife for a cited event.
                case "RECO":
                    return "Chargée d'enregistrer des informations sur un évènement, un lieu ou une personne.";
                case "REL":
                    return "REL";
                case "ROLE":
                    return "Rôle";
                case "SIBL":
                    return "Enfant"; // A male or female child of a common parent.
                case "SIST":
                    return "Soeur";
                case "SELR":
                    return "Vendeur";
                case "SPOU":
                    return "Conjointe";
                case "TXPY":
                    return "A fait l'objet d'une cotisation";
                case "WFAT":
                    return "Agissant en tant que père de la femme pour l'évènement.";
                case "WIFE":
                    return "Conjointe";
                case "WITN":
                    return "Témoin";
                case "WMOT":
                    return "Agissant en tant que mère de la femme pour l'évènement";
            }
            return s;
        }
        private static string Traduire_RESN(string s)
        {
            switch (s.ToLower())
            {
                case "confidential":
                    return "confidentiel";
                case "locked":
                    return "verrouillé";
                case "privacy":
                    return "privé";
            }
            return s;
        }
        private static string Traduire_SOURCE_FIDELITY_CODE(string s)
        {
            switch (s.ToUpper())
            {
                case "ORIGINAL":
                    return "Original";
                case "PHOTOCOPY":
                    return "Photocopie";
                case "TRANSCRIPT":
                    return "transcription";
                case "EXTRACT":
                    return "Extraction";
            }
            return s;
        }
        private static string Traduire_yes_no(string s)
        {
            s = s.ToUpper();
            switch (s)
            {
                case "Y":
                case "YES":
                    return "Oui";
                case "N":
                case "NO":
                    return "Non";
            }
            return s;
        }
        private static string ToUpperFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
        private static List<ID_numero> Valider_liste_reference_note(
                List<string> note_liste_ID,
                List<ID_numero> liste_NOTE_RECORD_ID_numero)
        {
            if (R.IsNullOrEmpty(note_liste_ID))
                return liste_NOTE_RECORD_ID_numero;
            if (GH.GHClass.Para.voir_note)
            {
                (liste_NOTE_RECORD_ID_numero, _) = Verifier_liste(note_liste_ID, liste_NOTE_RECORD_ID_numero);
            }
            return liste_NOTE_RECORD_ID_numero;
        }
        private static string Avoir_nom_lien_chercheur(string nom, string ID)
        {
            string texte;
            if (R.IsNullOrEmpty(nom))
                nom = "";
            if (R.IsNullOrEmpty(ID))
                ID = "";
            texte = "chercheur_" + nom.Replace(" ", "") + "_" + ID.Replace(" ", "");
            return texte;
        }
        private static string Avoir_nom_lien_evenement(
            string even,
            string date
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + "<br> even=" + even + "<br> date=" + date);
            string texte = null;
            if (even == null)
                even = "";
            if (date == null)
                date = "";
            texte += "evenement_" + even.Replace(" ", "") + "_" + date.Replace(" ", "");
            //R..Z("retoune " + texte);
            return texte;
        }

        private static string Avoir_texte_lien_NOTE_RECORD
            (
                string ID,
                List<ID_numero> liste_NOTE_RECORD_ID_numero,
                float retrait,
                int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la à ligne " + callerLineNumber + "<br>ID=" + ID);
            if (!GH.GHClass.Para.voir_note)
                return null;
            if (ID == null)
                return null;
            string espace = Tabulation(tab);
            string texte = null;
            string code = "";
            texte += espace + "<div style=\"padding-left:" + retrait.ToString().Replace(",", ".") +
                "in;\"" + code + ">\n";
            texte += espace + "\tVoir note\n";
            int numero;

            numero = Avoir_numero_reference(ID, liste_NOTE_RECORD_ID_numero);
            if (numero == 0)
            {
                texte = espace + "\tVoir note\n" + BLINK0;
                return texte;
            }
            texte += espace + "\t<a class=\"note\" href=\"#note-" + numero + "\">\n";
            texte += espace + "\t\t" + numero + "\n";
            texte += espace + "\t</a>\n";
            texte += espace + "</div>\n";
            return texte;
        }

        private static string Avoir_texte_NOTE_STRUCTURE(
                List<string> liste_ID,
                List<ID_numero> liste_citation_ID_numero,
                List<ID_numero> liste_source_ID_numero,
                List<ID_numero> liste_note_STRUCTURE_ID_numero,
                List<ID_numero> liste_NOTE_RECORD_ID_numero,
                float retrait,
                int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + " retrait=" + retrait);
            if (!GH.GHClass.Para.voir_note)
                return null;
            if (R.IsNullOrEmpty(liste_ID))
                return null;
            string espace = Tabulation(tab);
            string texte = null;
            foreach (string ID in liste_ID)
            {
                texte += espace + "<div style=\"margin-left:" +
                retrait.ToString().Replace(",", ".") + "in;border-left:2px solid #000;padding-left:2px;margin-bottom:2px\">\n";
                GEDCOMClass.NOTE_STRUCTURE info_note = GEDCOMClass.Avoir_Info_NOTE_STRUCTURE(ID);
                // texte
                if (R.IsNotNullOrEmpty(info_note.N0_texte))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\" style=\"width:50px;padding:0px\">\n";
                    texte += espace + "\t\t\t" + "Note:" + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += Convertir_texte_html(info_note.N0_texte, true, tab + 3) + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // note lien
                float temp = 0f;
                if (R.IsNotNullOrEmpty(info_note.N0_texte))
                    temp = .25f;
                texte += Avoir_texte_lien_NOTE_RECORD(info_note.N0_ID_RECORD, liste_NOTE_RECORD_ID_numero, temp, tab + 1) + "\n";
                // Note texte
                if (R.IsNotNullOrEmpty(info_note.N1_NOTE_STRUCTURE_liste_ID))
                {
                    if (info_note.N1_NOTE_STRUCTURE_liste_ID.Count > 0)
                    {
                        texte += Avoir_texte_NOTE_STRUCTURE(
                            info_note.N1_NOTE_STRUCTURE_liste_ID,
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero,
                            .25f, // retrait
                            tab);
                    }
                }
                // citation de la note
                if (R.IsNotNullOrEmpty(info_note.N1_SOUR_citation_liste_ID))
                {
                    texte += Avoir_texte_lien_citation(
                    info_note.N1_SOUR_citation_liste_ID,
                    liste_citation_ID_numero,
                    .25f,
                    tab + 1
                    );
                }
                // source de la note
                texte += Avoir_texte_lien_source(
                    info_note.N1_SOUR_source_liste_ID,
                    liste_source_ID_numero,
                    .25f,
                    tab + 1);
                texte += espace + "</div>\n";
            }
            GC.Collect();
            return texte;
        }

        public static string Avoir_nom_method()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }
        private static int Avoir_numero_reference(string ID, List<ID_numero> liste)
        {
            if (R.IsNullOrEmpty(liste))
                return 0;
            int numero = 0;
            foreach (ID_numero a in liste)
            {
                if (a.ID == ID)
                    return a.numero;
            }
            return numero;
        }


        private static string
            Avoir_texte_adresse(
            string SITE,
            List<GEDCOMClass.ADDRESS_STRUCTURE> info_adresse_liste,
            List<string> telephone_liste,
            List<string> telecopieur_liste,
            List<string> courriel_liste,
            List<string> www_liste,
            string sous_dossier,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + " à <b>" + Avoir_nom_method() + "</b>");
            string origine = @"../";
            if (R.IsNullOrEmpty(sous_dossier))
                origine = "";
            string espace = Tabulation(tab);
            string texte = null;
            string dossier_icone = origine + @"commun/";
            string bloc = null;
            if (R.IsNotNullOrEmpty(SITE))
            {
                bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                bloc += espace + "\t\t\t" + Convertir_texte_html(SITE, false, tab + 4) + "\n";
                bloc += espace + "\t\t</div>\n";
                bloc += espace + "\t</div>\n";
            }
            if (R.IsNotNullOrEmpty(info_adresse_liste))
            {
                foreach (GEDCOMClass.ADDRESS_STRUCTURE info_adresse in info_adresse_liste)
                {
                    // ADDR
                    if (R.IsNotNullOrEmpty(info_adresse.N0_ADDR))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += Convertir_texte_html(info_adresse.N0_ADDR, false, tab + 3);
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }

                    // ADR1
                    if (R.IsNotNullOrEmpty(info_adresse.N1_ADR1))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += Convertir_texte_html(info_adresse.N1_ADR1, false, tab + 3);
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }

                    // ADR2
                    if (R.IsNotNullOrEmpty(info_adresse.N1_ADR2))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += Convertir_texte_html(info_adresse.N1_ADR2, false, tab + 3);
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    if (R.IsNotNullOrEmpty(info_adresse.N1_ADR3))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += Convertir_texte_html(info_adresse.N1_ADR3, false, tab + 3);
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    if (R.IsNotNullOrEmpty(info_adresse.N1_CITY))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += Convertir_texte_html(info_adresse.N1_CITY, false, tab + 3);
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    if (R.IsNotNullOrEmpty(info_adresse.N1_STAE))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += Convertir_texte_html(info_adresse.N1_STAE, false, tab + 3);
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    if (R.IsNotNullOrEmpty(info_adresse.N1_CTRY))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += Convertir_texte_html(info_adresse.N1_CTRY, false, tab + 3);
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    if (R.IsNotNullOrEmpty(info_adresse.N1_POST))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += Convertir_texte_html(info_adresse.N1_POST, false, tab + 3);
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    if (R.IsNotNullOrEmpty(info_adresse.N1_PHON_liste))
                    {
                        foreach (string telephone in info_adresse.N1_PHON_liste)
                        {
                            bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                            bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                            bloc += espace + "\t\t\t" + "<img style=\"vertical-align:middle;height:18px\" src=" +
                                dossier_icone + "telephone.svg />\n";
                            bloc += Convertir_texte_html(telephone, false, tab + 3);
                            bloc += espace + "\t\t</div>\n";
                            bloc += espace + "\t</div>\n";
                        }
                    }
                    if (R.IsNotNullOrEmpty(info_adresse.N1_NOTE_STRUCTURE_liste_ID))
                    {
                        bloc += Avoir_texte_NOTE_STRUCTURE(
                            info_adresse.N1_NOTE_STRUCTURE_liste_ID,
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero,
                            .25f, // retrait
                            tab);
                    }
                }
            }

            // téléphone
            if (R.IsNotNullOrEmpty(telephone_liste))
            {
                foreach (string telephone in telephone_liste)
                {
                    bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                    bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                    bloc += espace + "\t\t\t<img style=\"vertical-align:middle;height:18px\" src=" + dossier_icone + "telephone.svg />\n";
                    bloc += Convertir_texte_html(telephone, false, tab + 3);
                    bloc += espace + "\t\t</div>\n";
                    bloc += espace + "\t</div>\n";
                }
            }

            // télécopieur
            if (R.IsNotNullOrEmpty(telecopieur_liste))
            {
                foreach (string telecopieur in telecopieur_liste)
                {
                    bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                    bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                    bloc += espace + "\t\t\t<img style=\"vertical-align:middle;height:18px\" src=" + dossier_icone + "telecopieur.svg />\n";
                    bloc += Convertir_texte_html(telecopieur, false, tab + 3);
                    bloc += espace + "\t\t</div>\n";
                    bloc += espace + "\t</div>\n";
                }
            }
            //  courriel
            if (R.IsNotNullOrEmpty(courriel_liste))
            {
                foreach (string courriel in courriel_liste)
                {
                    bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                    bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                    bloc += espace + "\t\t\t<img style=\"vertical-align:middle;height:18px\" src=" + dossier_icone + "courriel.svg />\n";
                    bloc += Convertir_texte_html(courriel, false, tab + 3);
                    bloc += espace + "\t\t</div>\n";
                    bloc += espace + "\t</div>\n";
                }
            }
            // www
            if (R.IsNotNullOrEmpty(www_liste))
            {
                foreach (string www in www_liste)
                {
                    bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                    bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                    bloc += espace + "\t\t\t<img style=\"vertical-align:middle;height:18px\" src=" + dossier_icone + "www.svg />\n ";
                    bloc += Convertir_texte_html(www, false, tab + 3);
                    bloc += espace + "\t\t</div>\n";
                    bloc += espace + "\t</div>\n";
                }
            }
            if (R.IsNotNullOrEmpty(bloc))
            {
                // Tableau
                texte += espace + "<div class=\"tableau tableau_0px\">\n";
                texte += bloc;
                // fin tableau
                texte += espace + "</div>\n";
            }
            GC.Collect();
            return texte;
        }

        private static string Avoir_texte_menu_hamburger(
            string origine,
            bool groupe_archive,
            bool groupe_attribut,
            bool groupe_chercheur,
            bool groupe_citation,
            bool groupe_conjoint,
            bool groupe_depot,
            bool groupe_enfant,
            bool groupe_evenement,
            bool groupe_information,
            bool groupe_GEDCOM,
            bool groupe_logiciel,
            bool groupe_media,
            bool groupe_note,
            bool groupe_ordonnance,
            bool groupe_parent,
            bool groupe_source
            )
        {
            string texte = null;
            texte += "\t\t\t\t<div class=\"hamburger\">\n";
            texte += "\t\t\t\t\t<button class=\"boutonHamburger\"></button>\n";
            texte += "\t\t\t\t\t<div class=\"hamburger-contenu\">\n";



            texte += "\t\t\t\t\t\t<table style=\"margin: 0 auto;width:100px\">\n";

            texte += "\t\t\t\t\t\t\t<tr>\n";

            texte += "\t\t\t\t\t\t\t\t<td>&nbsp;\n";
            texte += "\t\t\t\t\t\t\t\t</td>\n";

            texte += "\t\t\t\t\t\t\t\t<td>\n";
            texte += "\t\t\t\t\t\t\t\t\t<button style=\"margin-top:2px;\"><a href=\"#haut_page\"><img style=\"height:16px\" src=\"" + origine + "commun/haut_page.svg\" alt=\"\" /></a></button>\n";
            texte += "\t\t\t\t\t\t\t\t</td>\n";

            texte += "\t\t\t\t\t\t\t\t<td>&nbsp;\n";
            texte += "\t\t\t\t\t\t\t\t</td>\n";

            texte += "\t\t\t\t\t\t\t</tr>\n";



            texte += "\t\t\t\t\t\t\t<tr>\n";

            texte += "\t\t\t\t\t\t\t\t<td>\n";
            texte += "\t\t\t\t\t\t\t\t\t<button style=\"margin-top:2px;\" onclick=\"history.back()\"><img style=\"height:16px\" src=\"" + origine + "commun/page_precedent.svg\" alt=\"\" /></button>\n";
            texte += "\t\t\t\t\t\t\t\t</td>\n";

            texte += "\t\t\t\t\t\t\t\t<td>&nbsp;\n";
            texte += "\t\t\t\t\t\t\t\t</td>\n";

            texte += "\t\t\t\t\t\t\t\t<td>\n";
            texte += "\t\t\t\t\t\t\t\t\t<button style=\"margin-top:2px;\" onclick=\"history.forward()\"><img style=\"height:16px\" src=\"" + origine + "commun/page_suivant.svg\" alt=\"\" /></button>\n";
            texte += "\t\t\t\t\t\t\t\t</td>\n";
            texte += "\t\t\t\t\t\t\t</tr>\n";

            texte += "\t\t\t\t\t\t\t<tr>\n";

            texte += "\t\t\t\t\t\t\t\t<td>&nbsp;\n";
            texte += "\t\t\t\t\t\t\t\t</td>\n";

            texte += "\t\t\t\t\t\t\t\t<td>\n";
            texte += "\t\t\t\t\t\t\t\t\t<button style=\"margin-top:2px;margin-bottom:5px;\"><a href=\"#bas_page\"><img style=\"height:16px\" src=\"" + origine + "commun/bas_page.svg\" alt=\"\" /></a></button>\n";
            texte += "\t\t\t\t\t\t\t\t</td>\n";

            texte += "\t\t\t\t\t\t\t\t<td>&nbsp;\n";
            texte += "\t\t\t\t\t\t\t\t</td>\n";

            texte += "\t\t\t\t\t\t\t</tr>\n";
            texte += "\t\t\t\t\t\t</table>\n";


            if (groupe_archive)
            {
                texte += "\t\t\t\t\t\t<a class=\"hamburger-poussoir\" href=\"#groupe_archive\">\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-image\"><img class=\"hamburger-img\" src=\"" + origine + "commun/archive.svg\" /></div>\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-titre\">&nbsp;Archive</div>\n";
                texte += "\t\t\t\t\t\t</a>\n";
            }
            if (groupe_attribut)
            {
                texte += "\t\t\t\t\t\t<a class=\"hamburger-poussoir\" href=\"#groupe_attribut\">\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-image\"><img class=\"hamburger-img\" src=\"" + origine + "commun/attribut.svg\" /></div>\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-titre\">&nbsp;Attribut</div>\n";
                texte += "\t\t\t\t\t\t</a>\n";
            }
            if (groupe_chercheur)
            {
                texte += "\t\t\t\t\t\t<a class=\"hamburger-poussoir\" href=\"#groupe_chercheur\">\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-image\"><img class=\"hamburger-img\" src=\"" + origine + "commun/tete.svg\" /></div>\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-titre\">&nbsp;Chercheur</div>\n";
                texte += "\t\t\t\t\t\t</a>\n";
            }
            if (groupe_citation)
            {
                texte += "\t\t\t\t\t\t<a class=\"hamburger-poussoir\" href=\"#groupe_citation\">\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-image\"><img class=\"hamburger-img\" src=\"" + origine + "commun/citation.svg\" /></div>\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-titre\">&nbsp;Citation</div>\n";
                texte += "\t\t\t\t\t\t</a>\n";
            }
            if (groupe_conjoint)
            {
                texte += "\t\t\t\t\t\t<a class=\"hamburger-poussoir\" href=\"#groupe_conjoint\">\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-image\"><img class=\"hamburger-img\" src=\"" + origine + "commun/conjoint.svg\" /></div>\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-titre\">&nbsp;Conjoint</div>\n";
                texte += "\t\t\t\t\t\t</a>\n";
            }
            if (groupe_depot)
            {
                texte += "\t\t\t\t\t\t<a class=\"hamburger-poussoir\" href=\"#groupe_depot\">\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-image\"><img class=\"hamburger-img\" src=\"" + origine + "commun/depot.svg\" /></div>\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-titre\">&nbsp;Dépôt</div>\n";
                texte += "\t\t\t\t\t\t</a>\n";
            }
            if (groupe_enfant)
            {
                texte += "\t\t\t\t\t\t<a class=\"hamburger-poussoir\" href=\"#groupe_Enfant\">\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-image\"><img class=\"hamburger-img\" src=\"" + origine + "commun/enfant.svg\" /></div>\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-titre\">&nbsp;Enfant</div>\n";
                texte += "\t\t\t\t\t\t</a>\n";
            }
            if (groupe_evenement)
            {
                texte += "\t\t\t\t\t\t<a class=\"hamburger-poussoir\" href=\"#groupe_evenement\">\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-image\"><img class=\"hamburger-img\" src=\"" + origine + "commun/agenda.svg\" /></div>\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-titre\">&nbsp;Évènement</div>\n";
                texte += "\t\t\t\t\t\t</a>\n";
            }
            if (groupe_information)
            {
                texte += "\t\t\t\t\t\t<a class=\"hamburger-poussoir\" href=\"#groupe_information\">\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-image\"><img class=\"hamburger-img\" src=\"" + origine + "commun/envoi.svg\" /></div>\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-titre\">&nbsp;Information</div>\n";
                texte += "\t\t\t\t\t\t</a>\n";
            }
            if (groupe_GEDCOM)
            {
                texte += "\t\t\t\t\t\t<a class=\"hamburger-poussoir\" href=\"#groupe_GEDCOM\">\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-image\"><img class=\"hamburger-img\" src=\"" + origine + "commun/GEDCOM.svg\" /></div>\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-titre\">&nbsp;GEDCOM</div\n";
                texte += "\t\t\t\t\t\t</a>\n";
            }
            if (groupe_logiciel)
            {
                texte += "\t\t\t\t\t\t<a class=\"hamburger-poussoir\" href=\"#groupe_logiciel\">\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-image\"><img class=\"hamburger-img\" src=\"" + origine + "commun/logiciel.svg\" /></div>\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-titre\">&nbsp;Logiciel</div>\n";
                texte += "\t\t\t\t\t\t</a>\n";
            }
            if (groupe_note)
            {
                texte += "\t\t\t\t\t\t<a class=\"hamburger-poussoir\" href=\"#groupe_note\">\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-image\"><img class=\"hamburger-img\" src=\"" + origine + "commun/note.svg\" /></div>\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-titre\">&nbsp;Note</div>\n";
                texte += "\t\t\t\t\t\t</a>\n";
            }
            if (groupe_ordonnance)
            {
                texte += "\t\t\t\t\t\t<a class=\"hamburger-poussoir\" href=\"#groupe_ordonnance\">\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-image\"><img class=\"hamburger-img\" src=\"" + origine + "commun/LDS.png\" /></div>\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-titre\">&nbsp;Ordonnance</div>\n";
                texte += "\t\t\t\t\t\t</a>\n";
            }
            if (groupe_parent)
            {
                texte += "\t\t\t\t\t\t<a class=\"hamburger-poussoir\" href=\"#groupe_parent\">\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-image\"><img class=\"hamburger-img\" src=\"" + origine + "commun/parent.svg\" /></div>\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-titre\">&nbsp;Parent</div>\n";
                texte += "\t\t\t\t\t\t</a>\n";
            }
            if (groupe_source)
            {
                texte += "\t\t\t\t\t\t<a class=\"hamburger-poussoir\" href=\"#groupe_source\">\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-image\"><img class=\"hamburger-img\"src=\"" + origine + "commun/source.svg\" /></div>\n";
                texte += "\t\t\t\t\t\t\t<div class=\"hamburger-titre\">&nbsp;Source</div>\n";
                texte += "\t\t\t\t\t\t</a>\n";
            }
            texte += "\t\t\t\t\t</div>\n";
            texte += "\t\t\t\t</div>\n";
            return texte;
        }
        private static string Avoir_texte_NAME(
            List<GEDCOMClass.PERSONAL_NAME_STRUCTURE> liste_info_nom,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            if (R.IsNullOrEmpty(liste_info_nom))
                return null;
            string espace = Tabulation(tab);
            string texte = null;

            foreach (GEDCOMClass.PERSONAL_NAME_STRUCTURE infoNom in liste_info_nom)
            {
                string nom1 = null;
                string nom2 = null;
                if (R.IsNotNullOrEmpty(infoNom.N0_NAME))
                    nom1 = infoNom.N0_NAME;
                if (infoNom.N1_PERSONAL_NAME_PIECES != null)
                {
                    if (
                        R.IsNotNullOrEmpty(infoNom.N1_PERSONAL_NAME_PIECES.Nn_SURN) ||
                        R.IsNotNullOrEmpty(infoNom.N1_PERSONAL_NAME_PIECES.Nn_GIVN)
                        )
                    {
                        string titre = infoNom.N1_PERSONAL_NAME_PIECES.Nn_NPFX;
                        if (R.IsNotNullOrEmpty(titre))
                            titre += " ";
                        string prefix = infoNom.N1_PERSONAL_NAME_PIECES.Nn_SPFX;
                        if (R.IsNotNullOrEmpty(prefix))
                            prefix += " ";
                        string suffix = infoNom.N1_PERSONAL_NAME_PIECES.Nn_NSFX;
                        if (R.IsNotNullOrEmpty(suffix))
                            suffix = ", " + suffix;
                        nom2 = titre + prefix + infoNom.N1_PERSONAL_NAME_PIECES.Nn_SURN + ", " + infoNom.N1_PERSONAL_NAME_PIECES.Nn_GIVN + suffix;
                    }
                }
                // Tableau
                texte += espace + "<div class=\"tableau tableau_0px\">\n";
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                // type
                if (R.IsNotNullOrEmpty(infoNom.N1_TYPE))
                {
                    texte += espace + "\t\t<strong>" + infoNom.N1_TYPE + "</strong>\n";
                }
                if (R.IsNotNullOrEmpty(nom1) && R.IsNullOrEmpty(nom2))
                {
                    texte += espace + "\t\t<strong>" + nom1 + "</strong>\n";
                }
                if (R.IsNotNullOrEmpty(nom1) && R.IsNotNullOrEmpty(nom2))
                {
                    texte += espace + "\t\t<strong>" + nom1 + "</strong><br/>\n";
                    texte += espace + "\t\t" + nom2 + "\n";
                }
                if (R.IsNullOrEmpty(nom1) && R.IsNotNullOrEmpty(nom2))
                {
                    texte += espace + "\t\t<strong>" + nom2 + "</strong>\n";
                }
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
                // si allia BROSKEEP
                if (R.IsNotNullOrEmpty(infoNom.N1_ALIA_liste))
                {
                    foreach (string alia in infoNom.N1_ALIA_liste)
                    {
                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                        texte += espace + "\t\t\tAlias\n";
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                        texte += espace + "\t\t\t" + alia + "\n";
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t</div>\n";
                    }
                }
                if (infoNom.N1_PERSONAL_NAME_PIECES != null)
                {
                    // surnom
                    if (infoNom.N1_PERSONAL_NAME_PIECES.Nn_NICK != null)
                    {
                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                        texte += espace + "\t\t&nbsp;\n";
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                        texte += espace + "\t\tSurnom " + infoNom.N1_PERSONAL_NAME_PIECES.Nn_NICK + "\n";
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t</div>\n";
                    }
                    // citation
                    if (R.IsNotNullOrEmpty(infoNom.N1_PERSONAL_NAME_PIECES.Nn_SOUR_citation_liste_ID))
                    {
                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                        texte += Avoir_texte_lien_citation(
                            infoNom.N1_PERSONAL_NAME_PIECES.Nn_SOUR_citation_liste_ID,
                                liste_citation_ID_numero,
                                .25f,
                                tab + 3);
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t</div>\n";
                    }
                    // source
                    if (R.IsNotNullOrEmpty(infoNom.N1_PERSONAL_NAME_PIECES.Nn_SOUR_source_liste_ID))
                    {
                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                        texte += Avoir_texte_lien_source(
                            infoNom.N1_PERSONAL_NAME_PIECES.Nn_SOUR_source_liste_ID,
                                liste_source_ID_numero,
                                .25f,
                                tab + 3);
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t</div>\n";
                    }
                    // note

                    if (R.IsNotNullOrEmpty(liste_note_STRUCTURE_ID_numero))
                    {
                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                        texte +=
                            Avoir_texte_NOTE_STRUCTURE(
                                infoNom.N1_PERSONAL_NAME_PIECES.Nn_NOTE_STRUCTURE_liste_ID,
                                liste_citation_ID_numero,
                                liste_source_ID_numero,
                                liste_note_STRUCTURE_ID_numero,
                                liste_NOTE_RECORD_ID_numero,
                                .25f, // retrait
                                tab + 3
                                );
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t</div>\n";
                    }

                }

                // FONE
                string nom3 = null;
                string nom4 = null;
                string prefixFONE = null;
                string suffixFONE = null;
                string surnomFONE = null;
                string titreFONE = null;
                if (infoNom.N1_FONE_name_pieces != null)
                {
                    prefixFONE = infoNom.N1_FONE_name_pieces.Nn_SPFX;
                    if (R.IsNotNullOrEmpty(prefixFONE))
                        prefixFONE += " ";
                    suffixFONE = infoNom.N1_FONE_name_pieces.Nn_NSFX;
                    if (R.IsNotNullOrEmpty(suffixFONE))
                        suffixFONE = ", " + suffixFONE;
                    surnomFONE = infoNom.N1_FONE_name_pieces.Nn_NICK;
                    titreFONE = infoNom.N1_FONE_name_pieces.Nn_NPFX;
                    if (R.IsNotNullOrEmpty(titreFONE))
                        titreFONE += " ";
                    if (R.IsNotNullOrEmpty(
                        infoNom.N1_FONE_name_pieces.Nn_SURN) ||
                        R.IsNotNullOrEmpty(infoNom.N1_FONE_name_pieces.Nn_GIVN))
                    {
                        nom4 = titreFONE + prefixFONE + infoNom.N1_FONE_name_pieces.Nn_SURN + ", " +
                            infoNom.N1_FONE_name_pieces.Nn_GIVN + suffixFONE;
                    }
                }
                nom3 = infoNom.N1_FONE;
                if (R.IsNotNullOrEmpty(nom3) || R.IsNotNullOrEmpty(nom4))
                {
                    if (R.IsNotNullOrEmpty(infoNom.N1_FONE))
                    {
                        texte += espace + "<div>\n";
                        texte += espace + "\t<span class=\"detail_col1 texteR\" >\n";
                        texte += espace + "\t\tNom phonétiser&nbsp;&nbsp;\n";
                        texte += espace + "\t</span>\n";
                        if (R.IsNotNullOrEmpty(nom3) && R.IsNullOrEmpty(nom4))
                        {
                            texte += espace + "\t<span class=\"detail_col2\">\n";
                            texte += espace + "\t\t<strong>" + nom3 + "</strong>\n";
                            texte += espace + "\t</span>\n";
                        }
                        if (R.IsNotNullOrEmpty(nom3) && R.IsNotNullOrEmpty(nom4))
                        {
                            texte += espace + "\t<span class=\"detail_col2\">\n";
                            texte += espace + "\t\t<strong>" + nom3 + "</strong><br/>\n";
                            texte += espace + "\t\t" + nom4 + "\n";
                            texte += espace + "\t</span>\n";
                        }
                        if (R.IsNullOrEmpty(nom3) && R.IsNotNullOrEmpty(nom4))
                        {
                            texte += espace + "\t<span class=\"detail_col2\">\n";
                            texte += espace + "\t\t<strong>" + nom4 + "</strong>\n";
                            texte += espace + "\t</span>\n";
                        }

                        texte += espace + "</div>\n";
                    }
                    if (R.IsNotNullOrEmpty(infoNom.N2_FONE_TYPE))
                    {
                        texte += espace + "<div>\n";
                        texte += espace + "\t<span class=\"detail_col1 texteR\" >\n";
                        texte += espace + "\t\tType&nbsp;&nbsp;\n";
                        texte += espace + "\t</span>\n";
                        texte += espace + "\t<span class=\"detail_col2\">\n";
                        texte += Convertir_texte_html(infoNom.N2_FONE_TYPE, false, tab + 2);
                        texte += espace + "\t</span>\n";
                        texte += espace + "</div>\n";
                        texte += espace + "<div>\n";
                    }
                    texte += espace + "</div>\n";
                    if (infoNom.N1_FONE_name_pieces != null)
                    {
                        // surnom FONE
                        if (infoNom.N1_FONE_name_pieces.Nn_NICK != null)
                        {
                            texte += espace + "<div>\n";
                            texte += espace + "\t<span class=\"detail_col1 texteR\">\n";
                            texte += espace + "\t\tSurnom&nbsp;&nbsp;\n";
                            texte += espace + "\t</span>\n";
                            texte += espace + "\t<span class=\"detail_col2\">\n";
                            texte += espace + "\t\t" + infoNom.N1_FONE_name_pieces.Nn_NICK + "\n";
                            texte += espace + "\t</span>\n";
                            texte += espace + "</div>\n";
                        }
                        // SOUR FONE
                        if (R.IsNotNullOrEmpty(infoNom.N1_FONE_name_pieces.Nn_SOUR_citation_liste_ID))
                        {
                            texte += espace + "<div>\n";
                            texte += espace + "\t<span class=\"detail_col1\">\n";
                            texte += espace + "\t\t\t&nbsp;\n";
                            texte += espace + "\t</span>\n";
                            texte += espace + "\t<span class=\"detail_col2\">\n";
                            texte += Avoir_texte_lien_citation(
                                infoNom.N1_FONE_name_pieces.Nn_SOUR_citation_liste_ID,
                                liste_citation_ID_numero,
                                0,
                                tab + 2);
                            texte += espace + "\t</span>\n";
                            texte += espace + "</div>\n";
                        }
                        // source
                        if (R.IsNotNullOrEmpty(infoNom.N1_FONE_name_pieces.Nn_SOUR_source_liste_ID))
                        {
                            texte += espace + "<div>\n";
                            texte += espace + "\t<span class=\"detail_col1\">\n";
                            texte += espace + "\t\t\t&nbsp;\n";
                            texte += espace + "\t</span>\n";
                            texte += espace + "\t<span class=\"detail_col2\">\n";
                            texte += Avoir_texte_lien_source(
                                    infoNom.N1_FONE_name_pieces.Nn_SOUR_source_liste_ID,
                                    liste_source_ID_numero,
                                    0,
                                    tab + 3);
                            texte += espace + "\t</span>\n";
                            texte += espace + "</div>\n";
                        }
                        // note FONE
                        if (R.IsNotNullOrEmpty(infoNom.N1_FONE_name_pieces.Nn_NOTE_STRUCTURE_liste_ID))
                        {
                            texte += espace + "<div>\n";
                            texte += espace + "\t<span class=\"detail_col1\">\n";
                            texte += espace + "\t\t&nbsp;\n";
                            texte += espace + "\t</span>\n";
                            texte += espace + "\t<span class=\"detail_col2\">\n";
                            texte +=
                                Avoir_texte_NOTE_STRUCTURE(
                                infoNom.N1_FONE_name_pieces.Nn_NOTE_STRUCTURE_liste_ID,
                                liste_citation_ID_numero,
                                liste_source_ID_numero,
                                liste_note_STRUCTURE_ID_numero,
                                liste_NOTE_RECORD_ID_numero,
                                0, // retrait
                                tab);
                            texte += espace + "\t</span>\n";
                            texte += espace + "</div>\n";
                        }
                    }
                }
                // ROMN
                string nom5 = null;
                string nom6 = null;
                string prefixROMN = null;
                string suffixROMN = null;
                string surnomROMN = null;
                string titreROMN = null;
                if (infoNom.N1_ROMN_name_pieces != null)
                {
                    prefixROMN = infoNom.N1_ROMN_name_pieces.Nn_SPFX;
                    if (R.IsNotNullOrEmpty(prefixROMN))
                        prefixROMN += " ";
                    suffixROMN = infoNom.N1_ROMN_name_pieces.Nn_NSFX;
                    if (R.IsNotNullOrEmpty(suffixROMN))
                        suffixROMN = ", " + suffixROMN;
                    surnomROMN = infoNom.N1_ROMN_name_pieces.Nn_NICK;
                    titreROMN = infoNom.N1_ROMN_name_pieces.Nn_NPFX;
                    if (R.IsNotNullOrEmpty(titreROMN))
                        titreROMN += " ";
                    if (R.IsNotNullOrEmpty(infoNom.N1_ROMN_name_pieces.Nn_SURN) || R.IsNotNullOrEmpty(infoNom.N1_ROMN_name_pieces.Nn_GIVN))
                    {
                        nom6 = titreROMN + prefixROMN + infoNom.N1_ROMN_name_pieces.Nn_SURN + ", " +
                            infoNom.N1_ROMN_name_pieces.Nn_GIVN + suffixROMN;
                    }
                }
                nom5 = infoNom.N1_ROMN;
                if (R.IsNotNullOrEmpty(nom5) || R.IsNotNullOrEmpty(nom6))
                {
                    if (R.IsNotNullOrEmpty(infoNom.N1_ROMN))
                    {
                        texte += espace + "<div>\n";
                        texte += espace + "\t<span class=\"detail_col1 texteR\" >\n";
                        texte += espace + "\tNom romanisation&nbsp;&nbsp;\n";
                        texte += espace + "\t</span>\n";
                        if (R.IsNotNullOrEmpty(nom5) && R.IsNullOrEmpty(nom6))
                        {
                            texte += espace + "t\t<span class=\"detail_col2\">\n";
                            texte += espace + "\t\t<strong>" + nom5 + "</strong>\n";
                            texte += espace + "\t</span>\n";
                        }
                        if (R.IsNotNullOrEmpty(nom5) && R.IsNotNullOrEmpty(nom6))
                        {
                            texte += espace + "\t<span class=\"detail_col2\">\n";
                            texte += espace + "\t\t<strong>" + nom5 + "</strong><br/>\n";
                            texte += espace + "\t\t" + nom6 + "\n";
                            texte += espace + "\t</span>\n";
                        }
                        if (R.IsNullOrEmpty(nom5) && R.IsNotNullOrEmpty(nom6))
                        {
                            texte += espace + "\t<span class=\"detail_col2\">\n";
                            texte += espace + "\t\t<strong>" + nom6 + "</strong>\n";
                            texte += espace + "\t</span>\n";
                        }
                        texte += espace + "</div>\n";
                        //texte += espace + "<div>\n";
                    }
                    if (R.IsNotNullOrEmpty(infoNom.N2_ROMN_TYPE))
                        texte += espace + "<div>\n";
                    texte += espace + "\t<span class=\"detail_col1 texteR\" >\n";
                    texte += espace + "\tType&nbsp;&nbsp;\n";
                    texte += espace + "\t</span>\n";
                    texte += espace + "\t<span class=\"detail_col2\">\n";
                    texte += Convertir_texte_html(infoNom.N2_ROMN_TYPE, false, tab + 2);
                    texte += espace + "\t</span>\n";
                    texte += espace + "</div>\n";
                    texte += espace + "<div>\n";
                    texte += espace + "</div>\n";
                    if (infoNom.N1_ROMN_name_pieces != null)
                    {
                        // surnom ROMN
                        if (R.IsNotNullOrEmpty(infoNom.N1_ROMN_name_pieces.Nn_NICK))
                        {
                            texte += espace + "<div>\n";
                            texte += espace + "\t<span class=\"detail_col1 texteR\">\n";
                            texte += espace + "\tSurnom&nbsp;&nbsp;\n";
                            texte += espace + "\t</span>\n";
                            texte += espace + "\t<span class=\"detail_col2\">\n";
                            texte += espace + "\t\t" + infoNom.N1_ROMN_name_pieces.Nn_NICK + "\n";
                            texte += espace + "\t</span>\n";
                            texte += espace + "</div>\n";
                        }
                        // CITATION ROMN
                        if (R.IsNotNullOrEmpty(infoNom.N1_ROMN_name_pieces.Nn_SOUR_citation_liste_ID))
                        {
                            texte += espace + "<div>\n";
                            texte += espace + "\t<span class=\"detail_col1\">\n";
                            texte += espace + "\t\t&nbsp;\n";
                            texte += espace + "\t</span>\n";
                            texte += espace + "\t<span class=\"detail_col2\">\n";
                            texte += Avoir_texte_lien_citation(
                                infoNom.N1_ROMN_name_pieces.Nn_SOUR_citation_liste_ID,
                                liste_citation_ID_numero,
                                0f,
                                tab + 2);
                            texte += espace + "\t</span>\n";
                            texte += espace + "</div>\n";
                        }
                        // source
                        if (R.IsNotNullOrEmpty(infoNom.N1_ROMN_name_pieces.Nn_SOUR_source_liste_ID))
                        {
                            texte += espace + "<div>\n";
                            texte += espace + "\t<span class=\"detail_col1\">\n";
                            texte += espace + "\t\t&nbsp;\n";
                            texte += espace + "\t</span>\n";
                            texte += espace + "\t<span class=\"detail_col2\">\n";
                            texte += Avoir_texte_lien_source(
                                    infoNom.N1_ROMN_name_pieces.Nn_SOUR_source_liste_ID,
                                    liste_source_ID_numero,
                                    0,
                                    tab + 3);
                            texte += espace + "\t</span>\n";
                            texte += espace + "</div>\n";
                        }
                        // NOTE ROMN
                        if (R.IsNotNullOrEmpty(infoNom.N1_ROMN_name_pieces.Nn_NOTE_STRUCTURE_liste_ID))
                        {
                            texte += espace + "<div>\n";
                            texte += espace + "\t<span class=\"detail_col1\">\n";
                            texte += espace + "\t\t\t&nbsp;\n";
                            texte += espace + "\t</span>\n";
                            texte += espace + "\t<span class=\"detail_col2\">\n";
                            texte +=
                                Avoir_texte_NOTE_STRUCTURE(
                                infoNom.N1_ROMN_name_pieces.Nn_NOTE_STRUCTURE_liste_ID,
                                liste_citation_ID_numero,
                                liste_source_ID_numero,
                                liste_note_STRUCTURE_ID_numero,
                                liste_NOTE_RECORD_ID_numero,
                                0, // retrait
                                tab);
                            texte += espace + "</div>\n";
                        }
                    }
                }
                // fin tableau
                texte += espace + "</div>\n";
            }
            GC.Collect();
            return texte;
        }
        private static string Avoir_texte_NAME_53(
            List<GEDCOMClass.NAME_STRUCTURE_53> liste_info_nom,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab
            //,[CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            if (R.IsNullOrEmpty(liste_info_nom))
                return null;
            string espace = Tabulation(tab);
            string texte = null;
            foreach (GEDCOMClass.NAME_STRUCTURE_53 info_nom in liste_info_nom)
            {
                // Tableau
                texte += espace + "<div class=\"tableau tableau_0px\">\n";
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                if (R.IsNotNullOrEmpty(info_nom.N1_TYPE))
                {
                    texte += espace + "\t\t\t<strong>" + info_nom.N1_TYPE + "</strong>\n";
                }
                texte += espace + "\t\t\t<strong>" + info_nom.N0_NAME + "</strong>\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";

                texte += Avoir_texte_lien_citation(
                    info_nom.N1_SOUR_citation_liste_ID,
                    liste_citation_ID_numero,
                    .25f,
                    tab + 1);
                // source
                texte += Avoir_texte_lien_source(
                        info_nom.N1_SOUR_source_liste_ID,
                        liste_source_ID_numero,
                        0,
                        tab + 1);
                // note
                texte +=
                    Avoir_texte_NOTE_STRUCTURE(
                    info_nom.N1_NOTE_STRUCTURE_liste_ID,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    .25f,
                    tab + 1);
                // fin tableau
                texte += espace + "</div>\n";
            }
            GC.Collect();
            return texte;
        }

        /*
                private static string Avoir_texte_ligne_perdu(List<GEDCOMClass.Ligne_perdue> liste, int tab)
                {
                    if (R.IsNullOrEmpty(liste))
                        return null;
                    string espace = Tabulation(tab);
                    string texte = null;

                    texte += espace + "<div class=\"ligne_perdue\">\n";
                    if (liste.Count > 1)
                        texte += espace + "\t<p>Lignes du fichier GEDCOM qui n'ont pas été interpréter</p>\n";
                    else
                        texte += espace + "\t<p>Ligne du fichier GEDCOM qui n'a pas été interpréter</p>\n";
                    texte += espace + "\t<table class=\"ligne_perdue\">\n";
                    texte += espace + "\t<tr><td class=\"ligne_perdue\" style=\"width: 70px;\">Ligne</td>" +
                        "<td class=\"ligne_perdue\">" +
                        "Texte</td></tr>\n";
                    foreach (GEDCOMClass.Ligne_perdue ligne_perdue in liste)
                        texte += espace + "\t<tr><td class=\"ligne_perdue\">" +
                            ligne_perdue.ligne +
                            "</td><td class=\"ligne_perdue\">" +
                            ligne_perdue.texte +
                            "</td></tr>\n";
                    texte += espace + "\t</table>\n";
                    texte += espace + "</div>\n";
                    GC.Collect();
                    return texte;
                }
        */

        private static string Avoir_texte_NOTE_RECORD(
            string sous_dossier,
            string dossier_sortie,
            bool menu,
            int numero_carte,
            int numero_source,
            int nombre_source,
            List<ID_numero> liste_SUBMITTER_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab)
        {
            string texte = null;
            Application.DoEvents();
            string espace = Tabulation(tab);
            for (int f = 0; f < liste_NOTE_RECORD_ID_numero.Count; f++)
            {
                string bloc = null;
                GEDCOMClass.NOTE_RECORD info_NOTE_RECORD =
                    GEDCOMClass.Avoir_Info_Note(liste_NOTE_RECORD_ID_numero[f].ID);
                if (info_NOTE_RECORD != null)
                {
                    // N0_texte
                    if (R.IsNotNullOrEmpty(info_NOTE_RECORD.N0_texte))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += Convertir_texte_html(info_NOTE_RECORD.N0_texte, false, tab + 3);
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    // REFN
                    if (R.IsNotNullOrEmpty(info_NOTE_RECORD.N1_REFN_liste))
                    {
                        foreach (GEDCOMClass.USER_REFERENCE_NUMBER info in info_NOTE_RECORD.N1_REFN_liste)
                        {
                            bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                            bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                            bloc += espace + "\t\t\tREFN\n";
                            bloc += espace + "\t\t</div>\n";
                            bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                            bloc += espace + "\t\t\t\t" + info.N0_REFN + " type " + info.N1_TYPE + "\n";
                            bloc += espace + "\t\t</div>\n";
                            bloc += espace + "\t</div>\n";
                        }
                    }
                    //  RIN
                    if (R.IsNotNullOrEmpty(info_NOTE_RECORD.N1_RIN))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                        bloc += espace + "\t\t\tRIN\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += espace + "\t\t\t\t" + info_NOTE_RECORD.N1_RIN + "\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    // note de la note
                    if (R.IsNotNullOrEmpty(info_NOTE_RECORD.N1_NOTE_STRUCTURE_liste_ID))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += Avoir_texte_NOTE_STRUCTURE(
                            info_NOTE_RECORD.N1_NOTE_STRUCTURE_liste_ID,
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero,
                            0, // retrait
                            4) + "\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    // citation
                    bloc += Avoir_texte_lien_citation( //c
                    info_NOTE_RECORD.N1_SOUR_citation_liste_ID,
                    liste_citation_ID_numero,
                    0.04f,
                    tab + 3);
                    // source
                    bloc += Avoir_texte_lien_source(info_NOTE_RECORD.N1_SOUR_source_liste_ID, liste_source_ID_numero, 0.04f, tab + 3);
                    // date de changement
                    if (
                            GH.GHClass.Para.voir_date_changement &&
                            (info_NOTE_RECORD.N1_CHAN_liste != null) &&
                            info_NOTE_RECORD.N1_CHAN_liste.Any()
                        )
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        string texte_date = null;
                        foreach (GEDCOMClass.CHANGE_DATE info_date in info_NOTE_RECORD.N1_CHAN_liste)
                        {
                            texte_date += Avoir_texte_date_Changement(
                                info_date,
                                liste_citation_ID_numero,
                                liste_source_ID_numero,
                                liste_note_STRUCTURE_ID_numero,
                                liste_NOTE_RECORD_ID_numero,
                                false,
                                tab + 3
                                ) + System.Environment.NewLine;
                        }
                        texte_date = Convertir_texte_html(texte_date, false, 0);
                        if (R.IsNotNullOrEmpty(texte_date))
                            bloc += texte_date.TrimEnd('\r', '\n');
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    if (R.IsNotNullOrEmpty(bloc))
                    {
                        texte += espace + "\t<a id=\"note-" + liste_NOTE_RECORD_ID_numero[f].numero.ToString() + "\"></a>\n";
                        // Tableau
                        texte += Separation(5, null, "000", null, tab);
                        texte += espace + "<div class=\"tableau\">\n";
                        // titre du tableau
                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte += espace + "\t\t<div class=\"tableau_tete\">\n";
                        texte += espace + "\t\t\t<span class=\"tableau_note\">" + liste_NOTE_RECORD_ID_numero[f].numero.ToString() + "</span>\n";
                        if (GH.GHClass.Para.voir_ID)
                        {
                            texte += espace + "\t\t\t<span class=\"tableau_ID\">[" + liste_NOTE_RECORD_ID_numero[f].ID + "]</span>\n";
                        }
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t</div>\n";
                        texte += bloc;
                        // fin tableau
                        texte += espace + "</div>\n";
                    }
                }
            }
            GC.Collect();
            return texte;
        }

        private static (string, int, int) Avoir_texte_PLAC(
            GEDCOMClass.PLACE_STRUCTURE info_place,
            string sous_dossier,
            int numero_carte,
            int nombre_source,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + " à <b>" + Avoir_nom_method() + "</b>");
            if (info_place == null)
                return (null, numero_carte, nombre_source);
            string texte = null;
            string espace = Tabulation(tab);
            // Tableau 0px
            texte += espace + "<div class=\"tableau tableau_0px\">\n";
            // nom de la place
            if (info_place.N0_PLAC != null)
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += Convertir_texte_html(info_place.N0_PLAC, false, tab + 2);
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // CEME
            if (R.IsNotNullOrEmpty(info_place.N1_CEME))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info_place.N1_CEME + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
                if (R.IsNotNullOrEmpty(info_place.N2_CEME_PLOT))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t" + info_place.N2_CEME_PLOT + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
            }
            // FORM
            if (R.IsNotNullOrEmpty(info_place.N1_FORM))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info_place.N1_FORM + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // si FONE
            if (R.IsNotNullOrEmpty(info_place.N1_FONE))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\tPhonétisation " + info_place.N1_FONE + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";

                if (info_place.N2_FONE_TYPE != "")
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t&nbsp;&nbsp;&nbsp;&nbsp;" + " Type " + Convertir_texte_html(info_place.N2_FONE_TYPE, false, 0) + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
            }
            // si ROMN
            if (R.IsNotNullOrEmpty(info_place.N1_ROMN))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\tRomanisation " + info_place.N1_ROMN + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                if (info_place.N2_ROMN_TYPE != "")
                {
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t&nbsp;&nbsp;&nbsp;&nbsp;" + " Type " + Convertir_texte_html(info_place.N2_ROMN_TYPE, false, 0) + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
            }
            // si map
            if (R.IsNotNullOrEmpty(info_place.N2_MAP_LATI) || R.IsNotNullOrEmpty(info_place.N2_MAP_LONG))
            {
                numero_carte++;
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\tLatitude " + info_place.N2_MAP_LATI + " Longitude " + info_place.N2_MAP_LONG + "\n";
                texte += Avoir_texte_carte(numero_carte, info_place.N2_MAP_LATI, info_place.N2_MAP_LONG, info_place.N0_PLAC, tab + 3);
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // adresse
            string texte_adresse = Avoir_texte_adresse(
                info_place.N1_SITE,
                info_place.N1_ADDR_liste,
                info_place.N1_PHON_liste,
                info_place.N1_FAX_liste,
                info_place.N1_EMAIL_liste,
                info_place.N1_WWW_liste,
                sous_dossier,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero,
                tab + 1);
            if (R.IsNotNullOrEmpty(texte_adresse))
            {
                texte += texte_adresse;
            }
            // citation
            texte += Avoir_texte_lien_citation(
                info_place.N1_SOUR_citation_liste_ID,
                liste_citation_ID_numero,
                .25f,
                tab + 1);
            // source
            texte += Avoir_texte_lien_source(
                info_place.N1_SOUR_source_liste_ID,
                liste_source_ID_numero,
                .25f,
                tab + 1);
            // note
            texte +=
                Avoir_texte_NOTE_STRUCTURE(
                info_place.N1_NOTE_STRUCTURE_liste_ID,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero,
                0, // retrait
                tab + 1);
            // fin tableau
            texte += espace + "</div>\n";
            GC.Collect();
            return (texte, numero_carte, nombre_source);
        }

        private static string Avoir_texte_lien_repo(
            string ID,
            List<ID_numero> liste_repo_ID_numero,
            int tab)
        {
            if (!GH.GHClass.Para.voir_reference)
                return (null);
            if (R.IsNullOrEmpty(ID))
                return null;
            string espace = Tabulation(tab);
            string texte = espace + "<span>\n";
            texte += espace + "\tVoir dépôt \n";
            string texte_lien = null;
            int numero;
            numero = Avoir_numero_reference(ID, liste_repo_ID_numero);
            if (numero == 0)
                return (" Voir dépôt " + BLINK0);
            texte_lien += "<a class=\"depot\" href=\"#depot-" + numero + "\">" + numero + "</a>" + ", ";

            texte_lien = texte_lien.TrimEnd(' ', ',');
            texte += espace + "\t" + texte_lien + "\n";
            texte += espace + "</span>\n";
            return (texte);

        }

        private static string Avoir_texte_lien_source
            (
                string ID,
                List<ID_numero> liste_ID_numero,
                float retrait,
                int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + " à <b>" + Avoir_nom_method() + "</b>");
            // utiliser pour source et REFS
            if (!GH.GHClass.Para.voir_reference)
                return null;
            if (R.IsNullOrEmpty(ID))
                return null;
            string espace = Tabulation(tab);
            string texte = null;
            texte += espace + "<div style=\"font-Size:medium;padding-left:" + retrait.ToString().Replace(",", ".") + "in;\">\n";
            int position;
            texte += espace + "\tVoir source ";
            position = Avoir_numero_reference(ID, liste_ID_numero);
            if (position == 0)
            {
                texte += "Voir source " + BLINK0;
            }
            else
            {
                texte += "<a class=\"source\" href=\"#source-" + (position).ToString() + "\">" + (position).ToString() + "</a>, ";
                texte = texte.TrimEnd(' ', ',') + "\n";
            }
            texte += espace + "</div>\n";
            return texte;
        }

        private static string Avoir_texte_lien_source
            (
                //int nombre_source,
                List<string> liste_ID,
                List<ID_numero> liste_ID_numero,
                float retrait,
                int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + " à <b>" + Avoir_nom_method() + "</b>");
            //string a = "liste ID |";for (int f = 0; f < liste_ID.Count();f++) a += " <br>[" + f.ToString() + "] " + liste_ID[f];R..Z(a);
            // utiliser pour source et REFS
            if (!GH.GHClass.Para.voir_reference)
                return null;
            if (R.IsNullOrEmpty(liste_ID))
                return null;
            string espace = Tabulation(tab);
            string texte = null;
            texte += espace + "<div style=\"font-Size:medium;padding-left:" + retrait.ToString().Replace(",", ".") + "in;\">\n";
            int position;
            if (liste_ID.Count == 1)
            {
                texte += espace + "\tVoir source ";
            }
            else
            {
                texte += espace + "\tVoir sources ";
            }
            foreach (string ID in liste_ID)
            {
                position = Avoir_numero_reference(ID, liste_ID_numero);
                if (position == 0)
                {
                    texte += BLINK0;
                }
                else
                {
                    texte += "<a class=\"source\" href=\"#source-" + (position).ToString() + "\">" + (position).ToString() + "</a>, ";
                }
            }
            texte = texte.TrimEnd(' ', ',') + "\n";
            texte += espace + "</div>\n";
            return texte;
        }
        private string Texte_bouton_index_conjoint()
        {
            ListView lvChoixFamille = Application.OpenForms["GHClass"].Controls["lvChoixFamille"] as ListView;
            bool[] alphabette = new bool[27];
            for (int f = 0; f < 27; f++)
            {
                alphabette[f] = false;
            }
            string texte = null;
            // détermine si lettre est utiliser
            for (int f = 0; f < lvChoixFamille.Items.Count; f++)
            {
                if (lvChoixFamille.Items[f].SubItems[1].Text != "")
                {
                    char l = lvChoixFamille.Items[f].SubItems[1].Text[0];
                    int v = (int)l - 65;
                    if (v > -1 && v < 26)
                    {
                        alphabette[v] = true;
                    }
                    else
                    {
                        alphabette[26] = true;
                    }
                }
                else
                {
                    alphabette[26] = true;
                }
            }
            texte += "\t\t\t<div style=\"width:700px;border-radius:10px;background-color:#6464FF;margin-left:auto;margin-right:auto;padding:5px;\">\n";
            for (int f = 0; f < 26; f++)
            {
                if (alphabette[f])
                {
                    texte += "\t\t\t\t<a class=\"index\" href=\"m" + (char)(f + 65) + ".html\" >\n";
                    texte += "\t\t\t\t\t" + (char)(f + 65) + "\n";
                    texte += "\t\t\t\t</a>\n";
                }
                else
                {
                    texte += "\t\t\t\t<div class=\" index\">\n";
                    texte += "\t\t\t\t\t" + (char)(f + 65) + "\n";
                    texte += "\t\t\t\t</div>\n";
                }
            }
            if (alphabette[26])
            {
                texte += "\t\t\t\t<a class=\"index\" href=\"mDiver.html\" >\n";
                texte += "\t\t\t\t\t&\n";
                texte += "\t\t\t\t</a>\n";
            }
            else
            {
                texte += "\t\t\t\t<div class=\" index\">\n";
                texte += "\t\t\t\t\t&\n";
                texte += "\t\t\t\t\t</div>\n";
            }
            texte += "\t\t\t</div>\n";
            return texte;
        }
        private static void Regler_code_erreur([CallerLineNumber] int sourceLineNumber = 0)
        {
            GH.GHClass.erreur = "HT" + sourceLineNumber;
        }
        private static string Avoir_texte_publication(
            GEDCOMClass.PUBL_record info_PUBL,
            string sous_dossier,
            int tab = 0)
        {
            if (info_PUBL == null)
                return null;
            string espace = Tabulation(tab);
            string texte = null;
            // Tableau 0px
            texte += espace + "<div class=\"tableau tableau_0px\">\n";
            if (R.IsNotNullOrEmpty(info_PUBL.N1_PUBL))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += Convertir_texte_html(info_PUBL.N1_PUBL, false, tab + 3);
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }

            // NAME
            if (R.IsNotNullOrEmpty(info_PUBL.N1_NAME))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info_PUBL.N1_NAME + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }

            // TYPE
            if (R.IsNotNullOrEmpty(info_PUBL.N1_TYPE))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\tFormat: " + Convertir_mot_anglais(info_PUBL.N1_TYPE) + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // volume
            if (R.IsNotNullOrEmpty(info_PUBL.N1_SERS))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\tVolume: \n";
                texte += espace + "\t\t\t" + info_PUBL.N1_SERS + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }

            // numero
            if (R.IsNotNullOrEmpty(info_PUBL.N1_ISSU))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\tNumero: \n";
                texte += espace + "\t\t\t" + info_PUBL.N1_ISSU + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }

            // ISBN
            if (R.IsNotNullOrEmpty(info_PUBL.N1_LCCN))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\tISBN :\n";
                texte += espace + "\t\t\t" + info_PUBL.N1_LCCN + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }

            // ÉDITEUR
            if (R.IsNotNullOrEmpty(info_PUBL.N1_PUBR))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\tÉditeur: \n";
                texte += espace + "\t\t\t" + info_PUBL.N1_PUBR + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }

            // date
            if (R.IsNotNullOrEmpty(info_PUBL.N1_DATE))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\tDate de publication: ";
                (string date, _) = Convertir_date(info_PUBL.N1_DATE, GH.GHClass.Para.date_longue);
                texte += date + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }

            // EDTM
            if (R.IsNotNullOrEmpty(info_PUBL.N1_EDTN))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\tDescription: \n";
                texte += espace + "\t\t\t" + info_PUBL.N1_EDTN + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }

            // addr
            if (R.IsNotNullOrEmpty(info_PUBL.N1_ADDR_liste))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += Avoir_texte_adresse(//c
                    info_PUBL.N1_SITE,
                    info_PUBL.N1_ADDR_liste,
                    info_PUBL.N1_PHON_liste,
                    info_PUBL.N1_FAX_liste,
                    info_PUBL.N1_EMAIL_liste,
                    info_PUBL.N1_WWW_liste,
                    sous_dossier,
                    null,
                    null,
                    null,
                    null,
                    tab + 3);
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // fin tableau
            texte += espace + "</div>\n";
            GC.Collect();
            return texte;
        }

        private static (
            string,
            List<ID_numero>,
            List<ID_numero>,
            List<ID_numero>
            ) Avoir_texte_recencement(
            List<GEDCOMClass.CENS_record> CENS_liste,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab)
        {
            if (R.IsNullOrEmpty(CENS_liste))
                return (
                    null,
                    liste_citation_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero);
            string espace = Tabulation(tab);
            string texte = null;
            foreach (GEDCOMClass.CENS_record info_CENS in CENS_liste)
            {
                // Tableau
                texte += espace + "<div class=\"tableau tableau_0px\">\n";
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                if (R.IsNotNullOrEmpty(info_CENS.N1_DATE))
                {
                    (string date, _) = Convertir_date(info_CENS.N1_DATE, GH.GHClass.Para.date_longue);
                    texte += espace + "\t\t\tRecencement fait le " + date + ".\n";
                }
                if (R.IsNotNullOrEmpty(info_CENS.N1_LINE))
                    texte += espace + "\t\t\tL'information est sur la ligne " + info_CENS.N1_LINE + " du document.\n";
                if (R.IsNotNullOrEmpty(info_CENS.N1_DWEL))
                    texte += espace + "\t\t\tLe numéro de l'habitation est le " + info_CENS.N1_DWEL + ".\n";
                if (R.IsNotNullOrEmpty(info_CENS.N1_FAMN))
                    texte += espace + "\t\t\tLe numéro de famille est " + info_CENS.N1_FAMN + ".\n";
                if (R.IsNotNullOrEmpty(info_CENS.N1_NOTE_STRUCTURE_liste_ID))
                {
                    texte +=
                        Avoir_texte_NOTE_STRUCTURE(
                        info_CENS.N1_NOTE_STRUCTURE_liste_ID,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        0, // retrait
                        tab);
                }
                // fin tableau
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
                texte += espace + "</div>\n";
            }
            GC.Collect();
            return (
                texte,
                liste_citation_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero
                );
        }
        private static string Avoir_texte_REPO_record(
            string sous_dossier,
            string dossier_sortie,
            int nombre_REPO,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            string espace = Tabulation(tab);
            string texte = null;
            string bloc = null;
            foreach (ID_numero item_REPO in liste_repo_ID_numero)
            {
                GEDCOMClass.REPOSITORY_RECORD info_REPO = GEDCOMClass.Avoir_info_repo(item_REPO.ID);
                if (info_REPO == null)
                    return null;
                texte += espace + "<a id=\"depot-" + (nombre_REPO + item_REPO.numero).ToString() + "\"></a>\n";
                if (info_REPO != null)
                {
                    // Nom
                    if (R.IsNotNullOrEmpty(info_REPO.N1_NAME))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                        bloc += espace + "\t\t\tNom du dépôt\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += Convertir_texte_html(info_REPO.N1_NAME, false, tab + 3);
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    // Contact
                    if (R.IsNotNullOrEmpty(info_REPO.N1_CNTC))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                        bloc += espace + "\t\t\tContact\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += espace + "\t\t\t" + info_REPO.N1_CNTC + "\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    // addr
                    if (R.IsNotNullOrEmpty(info_REPO.N1_ADDR_liste))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                        bloc += espace + "\t\t\tAdresse\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += Avoir_texte_adresse//c
                            (
                                info_REPO.N1_SITE,
                                info_REPO.N1_ADDR_liste,
                                info_REPO.N1_PHON_liste,
                                info_REPO.N1_FAX_liste,
                                info_REPO.N1_EMAIL_liste,
                                info_REPO.N1_WWW_liste,
                                sous_dossier,
                                null,
                                null,
                                null,
                                liste_NOTE_RECORD_ID_numero,
                                tab + 3
                            );
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    // MEDI
                    if (R.IsNotNullOrEmpty(info_REPO.N1_MEDI))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                        bloc += espace + "\t\t\tFormat\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += espace + "\t\t\t" + Convertir_MEDIA_TYPE(info_REPO.N1_MEDI) + "\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    // CALN le titre
                    if (R.IsNotNullOrEmpty(info_REPO.N1_CALN))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += espace + "\t\t\tIdentification utilisé pour classer et récupérer des éléments des fonds d'un dépôt.\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    // CALN
                    if (R.IsNotNullOrEmpty(info_REPO.N1_CALN))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                        bloc += espace + "\t\t\t&nbsp;\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += espace + "\t\t\tNuméro d'identification: " + info_REPO.N1_CALN + "\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    // CALN_ITEM
                    if (R.IsNotNullOrEmpty(info_REPO.N2_CALN_ITEM))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                        bloc += espace + "\t\t\t&nbsp;\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += espace + "\t\t\tIdentification de l'article du film: " + info_REPO.N2_CALN_ITEM + "\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    // CALN_SHEE
                    if (R.IsNotNullOrEmpty(info_REPO.N2_CALN_SHEE))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                        bloc += espace + "\t\t\t&nbsp;\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += espace + "\t\t\tNuméro de feuille: " + info_REPO.N2_CALN_SHEE + "\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    // CALN_PAGE
                    if (R.IsNotNullOrEmpty(info_REPO.N2_CALN_PAGE))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                        bloc += espace + "\t\t\t&nbsp;\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += espace + "\t\t\tNuméro de page: " + info_REPO.N2_CALN_PAGE + "\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    // REFN
                    if (R.IsNotNullOrEmpty(info_REPO.N1_REFN_liste))
                    {
                        foreach (GEDCOMClass.USER_REFERENCE_NUMBER info in info_REPO.N1_REFN_liste)
                        {
                            bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                            bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                            bloc += espace + "\t\t\tREFN\n";
                            bloc += espace + "\t\t</div>\n";
                            bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                            bloc += "\t\t\t\t\t\t" + info.N0_REFN;
                            if (R.IsNotNullOrEmpty(info.N1_TYPE))
                                bloc += " Type " + info.N1_TYPE + "\n";
                            else
                                bloc += "\n";
                            bloc += espace + "\t\t</div>\n";
                            bloc += espace + "\t</div>\n";
                        }
                    }
                    // RIN
                    if (R.IsNotNullOrEmpty(info_REPO.N1_RIN))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                        bloc += espace + "\t\t\t\t\tRIN\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += espace + "\t\t\t\t" + info_REPO.N1_RIN + "\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    // NOTE
                    if (R.IsNotNullOrEmpty(info_REPO.N1_NOTE_STRUCTURE_liste_ID))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc +=
                            Avoir_texte_NOTE_STRUCTURE(
                            info_REPO.N1_NOTE_STRUCTURE_liste_ID,
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero,
                            0, // retrait
                            4);
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    // CHAN
                    if (GH.GHClass.Para.voir_date_changement && R.IsNotNullOrEmpty(info_REPO.N1_CHAN_liste))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        string texte_date = null;
                        foreach (GEDCOMClass.CHANGE_DATE info_date in info_REPO.N1_CHAN_liste)
                        {
                            texte_date += Avoir_texte_date_Changement(
                                info_date,
                                liste_citation_ID_numero,
                                liste_source_ID_numero,
                                liste_note_STRUCTURE_ID_numero,
                                liste_NOTE_RECORD_ID_numero,
                                false,
                                tab + 3) + System.Environment.NewLine;
                        }
                        texte_date = Convertir_texte_html(texte_date, false, 0);
                        if (R.IsNotNullOrEmpty(texte_date))
                            bloc += texte_date.TrimEnd('\r', '\n');
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                }
                // Tableau
                texte += Separation(5, null, "000", null, tab);
                if (R.IsNullOrEmpty(bloc))
                {
                    texte += espace + "<div class=\"tableau\">\n";
                }
                else
                {
                    texte += espace + "<div class=\"tableau\">  \n";
                }
                // titre du tableau
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_tete\">\n";
                texte += espace + "\t\t\t<span class=\"tableau_depot\">" +
                    (item_REPO.numero + nombre_REPO).ToString() + "</span>\n";
                if (GH.GHClass.Para.voir_ID)
                {
                    texte += espace + "\t\t\t<span class=\"tableau_ID\">[" + item_REPO.ID + "]</span>\n";
                }
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
                texte += bloc;
                texte += espace + "</div>\n";
            }
            GC.Collect();
            return texte;
        }

        private static string
            Avoir_texte_source
            (
                //List<ID_numero> liste_ID_numero,
                string sous_dossier,
                string dossier_sortie,
                bool menu,
                int numero_carte,
                int numero_source,
                int nombre_source,
                List<ID_numero> liste_SUBMITTER_ID_numero,
                List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero,
                List<ID_numero> liste_citation_ID_numero,
                List<ID_numero> liste_source_ID_numero,
                List<ID_numero> liste_repo_ID_numero,
                List<ID_numero> liste_note_STRUCTURE_ID_numero,
                List<ID_numero> liste_NOTE_RECORD_ID_numero,
                int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {

            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            //string z = "liste_source_ID_numero ";foreach (ID_numero b in liste_source_ID_numero){z += "<br> " + b.numero + " " + b.ID;}R..Z(z);

            if (liste_source_ID_numero == null)
                return null;
            string espace = Tabulation(tab);
            string texte = null;
            int compteurMedia = 0;
            Application.DoEvents();
            foreach (ID_numero item_source in liste_source_ID_numero)
            {
                compteurMedia++;
                // si la référence est une souce
                if (GEDCOMClass.Si_info_source(item_source.ID))
                {
                    GEDCOM.GEDCOMClass.SOURCE_RECORD info_source = GEDCOMClass.Avoir_info_source(item_source.ID);
                    {
                        if (info_source != null)
                        {
                            texte += espace + "<a id=\"source-" + (nombre_source + item_source.numero).ToString() + "\"></a>\n";
                            // Tableau
                            texte += Separation(5, null, "000", null, tab);
                            texte += espace + "<div class=\"tableau\">\n";
                            // titre du tableau
                            texte += espace + "\t<div class=\"tableau_ligne\">\n";
                            texte += espace + "\t\t<div class=\"tableau_tete\">\n";
                            texte += espace + "\t\t\t<span class=\"tableau_source\">" +
                                (item_source.numero + nombre_source).ToString() + "</span>\n";
                            if (GH.GHClass.Para.voir_ID)
                            {
                                texte += espace + "\t\t\t<span class=\"tableau_ID\">[" + item_source.ID + "]</span>\n";
                            }
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t</div>\n";
                            // N0_texte
                            if (R.IsNotNullOrEmpty(info_source.N0_texte))
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += Convertir_texte_html(info_source.N0_texte, false, tab + 3) + "\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // titre
                            if (R.IsNotNullOrEmpty(info_source.N1_TITL_liste))
                            {
                                foreach (string TITL in info_source.N1_TITL_liste)
                                {
                                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                    texte += espace + "\t\tTitre";
                                    texte += espace + "\t\t</div>\n";
                                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                    string TITL_id = Extraire_ID(TITL);
                                    if (R.IsNotNullOrEmpty(TITL_id))
                                        texte += Avoir_texte_lien_source(TITL_id, liste_source_ID_numero, 0, 3);
                                    else
                                        texte += espace + "\t\t\t\t" + Convertir_texte_html(TITL, false, 0) + "\n";
                                    texte += espace + "\t\t</div>\n";
                                    texte += espace + "\t</div>\n";
                                }
                            }
                            // TEXT
                            if (R.IsNotNullOrEmpty(info_source.N1_TEXT_liste))
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte += espace + "\t\t\t\tTexte de la source\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                for (int f = 0; f < info_source.N1_TEXT_liste.Count; f++)
                                {
                                    texte += Convertir_texte_html(info_source.N1_TEXT_liste[f].N0_TEXT, false, tab + 4);

                                    texte += Avoir_texte_NOTE_STRUCTURE(
                                        info_source.N1_TEXT_liste[f].N1_NOTE_STRUCTURE_liste_ID,
                                        liste_citation_ID_numero,
                                        liste_source_ID_numero,
                                        liste_note_STRUCTURE_ID_numero,
                                        liste_NOTE_RECORD_ID_numero,
                                        0, // retrait
                                        tab + 4);

                                    if (f > 0 && f < info_source.N1_TEXT_liste.Count)
                                        texte += espace + "\t\t\t\t\n";
                                }
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // page
                            if (R.IsNotNullOrEmpty(info_source.N1_PAGE))
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte += espace + "\t\tPage";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += espace + "\t\t\t\t" + info_source.N1_PAGE + "\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // CLASS
                            if (R.IsNotNullOrEmpty(info_source.N1_CLAS))
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte += espace + "\t\t\t\tCode de classification";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += espace + "\t\t\t\t" + Convertir_mot_anglais(info_source.N1_CLAS);
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // EVEN
                            if (R.IsNotNullOrEmpty(info_source.N1_EVEN))
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte += espace + "\t\t\t\tType d'évènement";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += espace + "\t\t\t\t" + Convertir_EVEN_en_texte(info_source.N1_EVEN);
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // PERIode
                            if (R.IsNotNullOrEmpty(info_source.N1_PERI))
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte += espace + "\t\t\t\tPériode\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                (string date, _) = Convertir_date(info_source.N1_PERI, GH.GHClass.Para.date_longue);
                                texte += espace + "\t\t\t\t" + date + "\n";
                                ;
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // data
                            {
                                string bloc = null;
                                // data événemnt
                                if (R.IsNotNullOrEmpty(info_source.N2_DATA_EVEN))
                                {
                                    bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                                    bloc += espace + "\t\t<div class=\"tableau_col1d\">\n";
                                    bloc += espace + "\t\t\tÉvènement:\n";
                                    bloc += espace + "\t\t</div>\n";
                                    bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                                    bloc += espace + "\t\t\t" +
                                        Traduire_mot_anglais(info_source.N2_DATA_EVEN, true) + ".\n";
                                    bloc += espace + "\t\t</div>\n";
                                    bloc += espace + "\t</div>\n";
                                }
                                // Date période
                                if (R.IsNotNullOrEmpty(info_source.N3_DATA_EVEN_DATE))
                                {
                                    bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                                    bloc += espace + "\t\t<div class=\"tableau_col1d\">\n";
                                    bloc += espace + "\t\t\t\tDate période: \n";
                                    bloc += espace + "\t\t</div>\n";
                                    bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                                    (string date_texte, _) = Convertir_date(info_source.N3_DATA_EVEN_DATE, GH.GHClass.Para.date_longue);
                                    bloc += espace + "\t\t\t\t" + date_texte + "\n";
                                    bloc += espace + "\t\t</div>\n";
                                    bloc += espace + "\t</div>\n";
                                }
                                // Lieu de juridiction
                                if (R.IsNotNullOrEmpty(info_source.N3_DATA_EVEN_PLAC))
                                {
                                    bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                                    bloc += espace + "\t\t<div class=\"tableau_col1d\">\n";
                                    bloc += espace + "\t\t\tLieu de juridiction: \n";
                                    bloc += espace + "\t\t</div>\n";
                                    bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                                    bloc += Convertir_texte_html(info_source.N3_DATA_EVEN_PLAC, false, tab + 3);
                                    bloc += espace + "\t\t</div>\n";
                                    bloc += espace + "\t</div>\n";
                                }
                                // Agence
                                if (R.IsNotNullOrEmpty(info_source.N2_DATA_AGNC))
                                {
                                    bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                                    bloc += espace + "\t\t<div class=\"tableau_col1d\">\n";
                                    bloc += espace + "\t\t\tAgence:\n";
                                    bloc += espace + "\t\t</div>\n";
                                    bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                                    bloc += Convertir_texte_html(info_source.N2_DATA_AGNC, false, tab + 3) + "\n";
                                    bloc += espace + "\t\t</div>\n";
                                    bloc += espace + "\t</div>\n";
                                }
                                // Note
                                if (R.IsNotNullOrEmpty(info_source.N2_DATA_NOTE_STRUCTURE_liste_ID))
                                {
                                    bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                                    bloc += espace + "\t\t<div class=\"tableau_col1d\">\n";
                                    bloc += espace + "\t\t</div>\n";
                                    bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                                    bloc +=
                                        Avoir_texte_NOTE_STRUCTURE(
                                        info_source.N2_DATA_NOTE_STRUCTURE_liste_ID,
                                        liste_citation_ID_numero,
                                        liste_source_ID_numero,
                                        liste_note_STRUCTURE_ID_numero,
                                        liste_NOTE_RECORD_ID_numero,
                                        0, // retrait
                                        tab + 3);
                                    bloc += espace + "\t\t</div>\n";
                                    bloc += espace + "\t</div>\n";
                                }
                                if (R.IsNotNullOrEmpty(bloc))
                                {
                                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                    texte += espace + "\t\t\tData\n";
                                    texte += espace + "\t\t</div>\n";
                                    texte += espace + "\t</div>\n";
                                    texte += bloc;
                                }
                            }
                            // QUAY
                            if (R.IsNotNullOrEmpty(info_source.N1_QUAY))
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte += espace + "\t\t\tCrédibilité: \n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += espace + "\t\t\t" + Traduire_QUAY(info_source.N1_QUAY) + "\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // DATE
                            if (R.IsNotNullOrEmpty(info_source.N1_DATE))
                            {
                                string date = null;
                                (date, _) = Convertir_date(info_source.N1_DATE, GH.GHClass.Para.date_longue);
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte += espace + "\t\t\t\tDate\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += espace + "\t\t\t\t" + date + "\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // recencement
                            if (R.IsNotNullOrEmpty(info_source.N1_CENS_liste))
                            {
                                // Recencement DATE
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte += espace + "\t\t\t\tRecencement\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                string texte_CENS;
                                (
                                    texte_CENS,
                                    liste_citation_ID_numero,
                                    liste_note_STRUCTURE_ID_numero,
                                    liste_NOTE_RECORD_ID_numero
                                ) = Avoir_texte_recencement(
                                    info_source.N1_CENS_liste,
                                    liste_citation_ID_numero,
                                    liste_source_ID_numero,
                                    liste_note_STRUCTURE_ID_numero,
                                    liste_NOTE_RECORD_ID_numero,
                                    tab + 3);
                                texte += texte_CENS;
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // ORIG
                            if (R.IsNotNullOrEmpty(info_source.N1_ORIG_liste))
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte += espace + "\t\t\t\tAuteur\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                string texte_ORIG;
                                (
                                    texte_ORIG,
                                    liste_citation_ID_numero,
                                    liste_note_STRUCTURE_ID_numero,
                                    liste_NOTE_RECORD_ID_numero
                                ) = Avoir_texte_auteur(
                                    info_source.N1_ORIG_liste,
                                    liste_citation_ID_numero,
                                    liste_source_ID_numero,
                                    liste_note_STRUCTURE_ID_numero,
                                    liste_NOTE_RECORD_ID_numero,
                                    tab + 3);
                                //  texte += espace + "\t\t\t\t" + info_citation.N1_ORIG_record.N1_NAME + "\n";
                                texte += texte_ORIG;
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // Publication
                            if (info_source.N1_PUBL_record != null)
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte += espace + "\t\t\t\tÉdition\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += Avoir_texte_publication(info_source.N1_PUBL_record, sous_dossier, tab + 4);
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // Immigration
                            if (info_source.N1_IMMI_record != null)
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte += espace + "\t\t\t\tImmigration\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                string texte_IMMI = null;
                                (
                                    texte_IMMI,
                                    liste_citation_ID_numero,
                                    liste_source_ID_numero,
                                    liste_note_STRUCTURE_ID_numero,
                                    liste_NOTE_RECORD_ID_numero) =
                                    Avoir_texte_immigration(
                                        info_source.N1_IMMI_record,
                                        liste_citation_ID_numero,
                                        liste_source_ID_numero,
                                        liste_note_STRUCTURE_ID_numero,
                                        liste_NOTE_RECORD_ID_numero,
                                        tab + 4);
                                texte += texte_IMMI;
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // AUTH The name of the individual who created or compiled information,
                            if (R.IsNotNullOrEmpty(info_source.N1_AUTH))
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte += espace + "\t\t\t\tCreateur ou compilateur\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += Convertir_texte_html(info_source.N1_AUTH, false, tab + 4);
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // CPLR The name of the person that compiled writings of others.
                            if (R.IsNotNullOrEmpty(info_source.N1_CPLR))
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += espace + "\t\t\t\tCompilateur des écrits des autres: ";
                                texte += espace + "\t\t\t\t" + info_source.N1_CPLR;
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // EDTR The name of a person who edited information.
                            if (R.IsNotNullOrEmpty(info_source.N1_EDTR))
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += espace + "\t\t\t\tEditeur de l'information: ";
                                texte += espace + "\t\t\t\t" + info_source.N1_EDTR;
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // ABBR
                            if (R.IsNotNullOrEmpty(info_source.N1_ABBR))
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte += espace + "\t\t\t\tSaisi des données:\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += Convertir_texte_html(info_source.N1_ABBR, false, tab + 4);
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // TYPE HERIDIS
                            if (R.IsNotNullOrEmpty(info_source.N1_TYPE))
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte += espace + "\t\t\t\tTYPE (Heridis):\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += espace + "\t\t\t\t" + info_source.N1_TYPE + "\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // PUBL
                            if (R.IsNotNullOrEmpty(info_source.N1_PUBL))
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte += espace + "\t\t\t\tFait sur la publication:\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += espace + "\t\t\t\t" + info_source.N1_PUBL + "\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // REFN
                            if (R.IsNotNullOrEmpty(info_source.N1_REFN_liste))
                            {
                                foreach (GEDCOMClass.USER_REFERENCE_NUMBER info in info_source.N1_REFN_liste)
                                {
                                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                    texte += espace + "\t\t\tREFN\n";
                                    texte += espace + "\t\t</div>\n";
                                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                    texte += espace + "\t\t\t" + info.N0_REFN + " Type " + info.N1_TYPE + "\n";
                                    texte += espace + "\t\t</div>\n";
                                    texte += espace + "\t</div>\n";
                                }
                            }
                            // RIN
                            if (R.IsNotNullOrEmpty(info_source.N1_RIN))
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte += espace + "\t\t\t\t\tRIN\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += espace + "\t\t\t\t" + info_source.N1_RIN + "\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // STAT
                            if (R.IsNotNullOrEmpty(info_source.N1_STAT))
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte += espace + "\t\t\t\tÉtat de la recherche\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += espace + "\t\t\t\t" + Convertir_mot_anglais(info_source.N1_STAT) + "\n";
                                if (R.IsNotNullOrEmpty(info_source.N2_STAT_DATE))
                                {
                                    string date = null;
                                    (date, _) = Convertir_date(info_source.N2_STAT_DATE, GH.GHClass.Para.date_longue);
                                    texte += espace + "\t\t\ten date du " + date + "\n";

                                }
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // Code de fidélité
                            if (R.IsNotNullOrEmpty(info_source.N1_FIDE))
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte += espace + "\t\t\tCode de fidélité\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += espace + "\t\t\t" + Traduire_SOURCE_FIDELITY_CODE(info_source.N1_FIDE) + "\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            //  MULTIMEDIA_LINK
                            string temp_string = null;
                            (
                                temp_string,
                                liste_MULTIMEDIA_ID_numero,
                                liste_citation_ID_numero,
                                liste_source_ID_numero,
                                liste_repo_ID_numero,
                                liste_note_STRUCTURE_ID_numero,
                                liste_NOTE_RECORD_ID_numero
                                ) =
                            Avoir_texte_MULTIMEDIA(
                                info_source.MULTIMEDIA_LINK_liste_ID,
                                sous_dossier,
                                dossier_sortie,
                                liste_MULTIMEDIA_ID_numero,
                                liste_citation_ID_numero,
                                liste_source_ID_numero,
                                liste_repo_ID_numero,
                                liste_note_STRUCTURE_ID_numero,
                                liste_NOTE_RECORD_ID_numero,
                                true, // + marge
                                tab + 1
                                );
                            texte += temp_string;

                            // référence liste REFS
                            if (R.IsNotNullOrEmpty(info_source.N1_REFS_liste_ID))
                            {
                                string phrase = "Source non examinée";
                                if (info_source.N1_REFS_liste_ID.Count > 1)
                                    phrase = "Sources non examinées";
                                nombre_source = liste_source_ID_numero.Count;
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte += espace + "\t\t\t" + phrase + "<sup>1</sup>\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += Avoir_texte_lien_source(info_source.N1_REFS_liste_ID, liste_source_ID_numero, 0, tab + 4);
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // source ou EVEN de la source
                            if (info_source.N1_SOUR_EVEN_liste_ID != null)
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += Avoir_texte_lien_source(info_source.N1_SOUR_EVEN_liste_ID,
                                    liste_source_ID_numero, 0, tab + 4);
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // Dépot 
                            if (R.IsNotNullOrEmpty(info_source.N1_REPO_liste))
                            {
                                foreach (GEDCOMClass.SOURCE_REPOSITORY_CITATION info_repo in info_source.N1_REPO_liste)
                                {
                                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                    texte += espace + "\t\t\tDépôt";
                                    texte += espace + "\t\t</div>\n";
                                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                    //texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                    //texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                    string texte_repo = Avoir_texte_REPO_citation(
                                        info_repo,
                                        sous_dossier,
                                        liste_citation_ID_numero,
                                        liste_source_ID_numero,
                                        liste_repo_ID_numero,
                                        liste_note_STRUCTURE_ID_numero,
                                        liste_NOTE_RECORD_ID_numero,
                                        tab + 3);
                                    texte += texte_repo;
                                    //texte += espace + "\t\t</div>\n";
                                    //texte += espace + "\t</div>\n";
                                    texte += espace + "\t\t</div>\n";
                                    texte += espace + "\t</div>\n";

                                }
                            }
                            // NOTE
                            if (R.IsNotNullOrEmpty(info_source.N1_NOTE_STRUCTURE_liste_ID))
                            {

                                texte +=
                                Avoir_texte_NOTE_STRUCTURE(
                                info_source.N1_NOTE_STRUCTURE_liste_ID,
                                liste_citation_ID_numero,
                                liste_source_ID_numero,
                                liste_note_STRUCTURE_ID_numero,
                                liste_NOTE_RECORD_ID_numero,
                                0.04f, // retrait
                                tab + 3);

                            }
                            // date de changement
                            if (GH.GHClass.Para.voir_date_changement && info_source.N1_CHAN != null)
                            {
                                if (R.IsNotNullOrEmpty(info_source.N1_CHAN.N1_CHAN_DATE))
                                {
                                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                    texte += Avoir_texte_date_Changement(
                                        info_source.N1_CHAN,
                                        liste_citation_ID_numero,
                                        liste_source_ID_numero,
                                        liste_note_STRUCTURE_ID_numero,
                                        liste_NOTE_RECORD_ID_numero,
                                        false,
                                        tab + 3);
                                    texte += espace + "\t\t</div>\n";
                                    texte += espace + "\t</div>\n";
                                }
                            }
                            // ligne perdue
                            // fin tableau
                            texte += espace + "</div>\n";
                        }
                    }
                }
                // si la référence est un évènement V5.3
                if (GEDCOMClass.Si_info_event(item_source.ID))
                {
                    GEDCOM.GEDCOMClass.EVEN_RECORD_53 info_event = GEDCOMClass.Avoir_info_event_53(item_source.ID);
                    if (info_event != null)
                    {
                        texte += espace + "<a id=\"source-" + (item_source.numero).ToString() + "\"></a>\n";
                        // Tableau
                        texte += Separation(5, null, "000", null, tab);
                        texte += espace + "<div class=\"tableau\">\n";
                        // titre du tableau
                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte += espace + "\t\t<div class=\"tableau_tete\">\n";
                        texte += espace + "\t\t\t<span class=\"tableau_citation\">" +
                            (item_source.numero).ToString() + "</span>\n";
                        if (GH.GHClass.Para.voir_ID)
                        {
                            texte += espace + "\t\t\t<span class=\"tableau_ID\">[" + item_source.ID + "]</span>\n";
                        }
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t</div>\n";
                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                        string evenement = Convertir_EVEN_en_texte(info_event.N1_EVEN);
                        texte += espace + "\t\t\t<b>" + evenement + " est l'évènement en référence</b>\n";
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t</div>\n";
                        if (R.IsNotNullOrEmpty(info_event.N2_EVEN_TYPE))
                        {
                            texte += espace + "\t<div class=\"tableau_ligne\">\n";
                            texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                            texte += espace + "\t\t\t" + info_event.N2_EVEN_TYPE + "\n";
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t</div>\n";
                        }
                        // Date 
                        if (R.IsNotNullOrEmpty(info_event.N2_EVEN_DATE))
                        {
                            texte += espace + "\t<div class=\"tableau_ligne\">\n";
                            texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                            texte += espace + "\t\t\t\tDate\n";
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                            texte += espace + "\t\t\t\t" + info_event.N2_EVEN_DATE + "\n";
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t</div>\n";
                        }
                        // Place lieu
                        if (info_event.N2_EVEN_PLAC != null)
                        {
                            texte += espace + "\t<div class=\"tableau_ligne\">\n";
                            texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                            texte += espace + "\t\t\tLieu\n";
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                            string temp;
                            (
                                temp,
                                numero_carte,
                                numero_source
                            ) = Avoir_texte_PLAC(
                                info_event.N2_EVEN_PLAC,
                                sous_dossier,
                                numero_carte,
                                numero_source,
                                liste_citation_ID_numero,
                                liste_source_ID_numero,
                                liste_note_STRUCTURE_ID_numero,
                                liste_NOTE_RECORD_ID_numero,
                                tab + 3);
                            texte += temp;
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t</div>\n";
                        }
                        // PERI
                        if (R.IsNotNullOrEmpty(info_event.N2_EVEN_PERI))
                        {
                            texte += espace + "\t<div class=\"tableau_ligne\">\n";
                            texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                            texte += espace + "\t\t\tPériode\n";
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                            string date;
                            (date, _) = Convertir_date(info_event.N2_EVEN_PERI, GH.GHClass.Para.date_longue);
                            texte += espace + "\t\t\t" + date + "\n";
                            ;
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t</div>\n";
                        }
                        // RELI
                        if (R.IsNotNullOrEmpty(info_event.N2_EVEN_RELI))
                        {
                            texte += espace + "\t<div class=\"tableau_ligne\">\n";
                            texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                            texte += espace + "\t\t\tAffiliation\n";
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                            texte += espace + "\t\t\t" + info_event.N2_EVEN_RELI + "\n";
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t</div>\n";
                        }

                        // TEXT
                        if (R.IsNotNullOrEmpty(info_event.N2_EVEN_TEXT_liste))
                        {
                            bool premier = true;
                            foreach (GEDCOMClass.TEXT_STRUCTURE info_texte in info_event.N2_EVEN_TEXT_liste)
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                                if (premier)
                                {
                                    texte += espace + "\t\t\tTexte";
                                    premier = false;
                                }
                                else
                                {
                                    texte += espace + "\t\t\t&nbsp;";
                                }
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += espace + Convertir_texte_html(info_texte.N0_TEXT, false, tab + 2);
                                texte +=
                                    Avoir_texte_NOTE_STRUCTURE(
                                    info_texte.N1_NOTE_STRUCTURE_liste_ID,
                                    liste_citation_ID_numero,
                                    liste_source_ID_numero,
                                    liste_note_STRUCTURE_ID_numero,
                                    liste_NOTE_RECORD_ID_numero,
                                    0, // retrait
                                    4);
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                        }
                        // ROLE
                        if (R.IsNotNullOrEmpty(info_event.N2_EVEN_ROLE))
                        {
                            texte += espace + "\t<div class=\"tableau_ligne\">\n";
                            texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                            texte += espace + "\t\t\tRôle\n";
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                            texte += espace + "\t\t\t" + Convertir_ROLE_TAG(info_event.N2_EVEN_ROLE) + "\n";
                            // ROLE TYPE
                            if (R.IsNotNullOrEmpty(info_event.N3_EVEN_ROLE_TYPE))
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += espace + "\t\t\tType: " + info_event.N3_EVEN_ROLE_TYPE + "\n";
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";

                            }
                            // ROLE INDIVIDUAL
                            if (info_event.N3_EVEN_ROLE_INDIVIDUAL != null)
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\" style=\"padding-right:8px;\">\n";
                                texte += espace + Avoir_texte_individual_53(
                                    info_event.N3_EVEN_ROLE_INDIVIDUAL,
                                    sous_dossier,
                                    liste_citation_ID_numero,
                                    liste_source_ID_numero,
                                    liste_note_STRUCTURE_ID_numero,
                                    liste_NOTE_RECORD_ID_numero,
                                    tab + 3);
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            // ROLE ASSO
                            if (R.IsNotNullOrEmpty(info_event.N3_EVEN_ROLE_ASSO_liste))
                            {
                                foreach (GEDCOMClass.ASSOCIATION_STRUCTURE info_ASSO in info_event.N3_EVEN_ROLE_ASSO_liste)
                                {
                                    // si c'est un individu.
                                    if (GEDCOMClass.Si_individu(info_ASSO.N0_ASSO))
                                    {
                                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                        texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                        texte += espace + "\t\tAssociation:\n";
                                        if (menu)
                                        {
                                            texte += espace + "\t\t\t<a class=\"ficheIndividu\"" + info_ASSO.N0_ASSO + ".html\"></a>\n";
                                        }
                                        else
                                        {
                                            texte += espace + "\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                                        }
                                        texte += espace + "\t\t\t" + GEDCOMClass.Avoir_premier_nom_individu(info_ASSO.N0_ASSO);
                                        if (GH.GHClass.Para.voir_ID == true)
                                        {
                                            texte += "[" + info_ASSO.N0_ASSO + "]\n";
                                        }
                                        texte += espace + "\t\t</div>\n";
                                        texte += espace + "\t</div>\n";
                                        if (R.IsNotNullOrEmpty(info_ASSO.N1_TYPE))
                                        {
                                            string type = info_ASSO.N1_TYPE;
                                            switch (info_ASSO.N1_TYPE.ToUpper())
                                            {
                                                case "FAM":
                                                    type = "Famille";
                                                    break;
                                                case "INDI":
                                                    type = "Individu";
                                                    break;
                                            }
                                            texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                            texte += espace + "\t\t<div class=\"tableau_colW\" style=\"padding-left:.25in\">\n";
                                            texte += espace + "\t\t\tType: " + type + "\n";
                                            texte += espace + "\t\t</div>\n";
                                            texte += espace + "\t</div>\n";
                                        }
                                    }
                                    // si c'est un chercheur
                                    if (GEDCOMClass.Si_chercheur(info_ASSO.N0_ASSO))
                                    {
                                        List<string> liste_ID = new List<string>
                                        {
                                            info_ASSO.N0_ASSO
                                        };
                                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                        texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                        texte += Avoir_texte_lien_chercheur(liste_ID, liste_SUBMITTER_ID_numero, tab + 3);
                                        texte += espace + "\t\t</div>\n";
                                        texte += espace + "\t</div>\n";
                                        if (R.IsNotNullOrEmpty(info_ASSO.N1_TYPE))
                                        {
                                            texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                            texte += espace + "\t\t<div class=\"tableau_colW\" style=\"padding-left:.25in\">\n";
                                            texte += espace + "\t\t\tType: " + info_ASSO.N1_TYPE + "\n";
                                            texte += espace + "\t\t</div>\n";
                                            texte += espace + "\t</div>\n";
                                        }
                                    }
                                }
                            }
                            // ROLE RELATIONSHIP_ROLE_TAG
                            if (R.IsNotNullOrEmpty(info_event.N3_EVEN_ROLE_RELATIONSHIP_tag))
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += espace + "\t\t\tRelation: " +
                                    Convertir_TAG_texte(info_event.N3_EVEN_ROLE_RELATIONSHIP_tag) + "\n";
                                if (R.IsNotNullOrEmpty(info_event.N3_EVEN_ROLE_RELATIONSHIP_ID))
                                {
                                    if (menu)
                                    {
                                        texte += espace + "\t\t\t<a class=\"ficheIndividu\"" + info_event.N3_EVEN_ROLE_RELATIONSHIP_ID + ".html\"></a>\n";
                                    }
                                    else
                                    {
                                        texte += espace + "\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                                    }
                                    texte += espace + "\t\t\t" + GEDCOMClass.Avoir_premier_nom_individu(info_event.N3_EVEN_ROLE_RELATIONSHIP_ID);
                                    if (GH.GHClass.Para.voir_ID == true)
                                    {
                                        texte += "[" + info_event.N3_EVEN_ROLE_RELATIONSHIP_ID + "]\n";
                                    }
                                    if (R.IsNotNullOrEmpty(info_event.N4_EVEN_ROLE_RELATIONSHIP_TYPE))
                                    {
                                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                        texte += espace + "\t\t<div class=\"tableau_colW\" style=\"padding-left:.25in\">\n";
                                        texte += espace + "\t\t\tType: " + info_event.N4_EVEN_ROLE_RELATIONSHIP_TYPE + "\n";
                                        texte += espace + "\t\t</div>\n";
                                        texte += espace + "\t</div>\n";
                                    }
                                    // ROLE RELATIONSHIP_ROLE_TAGINDIVIDUAL
                                    if (info_event.N4_EVEN_ROLE_RELATIONSHIP_INDIVIDUAL != null)
                                    {
                                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                        texte += espace + "\t\t<div class=\"tableau_colW\" style=\"padding-left:.25in;padding-right:8px;\">\n";
                                        texte += espace + Avoir_texte_individual_53(//c
                                            info_event.N4_EVEN_ROLE_RELATIONSHIP_INDIVIDUAL,
                                            sous_dossier,
                                            liste_citation_ID_numero,
                                            liste_source_ID_numero,
                                            liste_note_STRUCTURE_ID_numero,
                                            liste_NOTE_RECORD_ID_numero,
                                            tab + 3);
                                        texte += espace + "\t\t</div>\n";
                                        texte += espace + "\t</div>\n";
                                    }
                                }

                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t</div>\n";
                        }
                        //  MULTIMEDIA_LINK
                        string temp_string = null;
                        (
                            temp_string,
                            liste_MULTIMEDIA_ID_numero,
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_repo_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero
                            ) =
                        Avoir_texte_MULTIMEDIA(
                            info_event.MULTIMEDIA_LINK_liste_ID,
                            sous_dossier,
                            dossier_sortie,
                            liste_MULTIMEDIA_ID_numero,
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_repo_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero,
                            true, // +marge
                            tab + 1
                            );
                        texte += temp_string;
                        // citation
                        texte += Avoir_texte_lien_citation(
                            info_event.N2_EVEN_SOUR_citation_liste_ID,
                            liste_citation_ID_numero,
                            .04f,
                            tab + 1);
                        // source
                        texte += Avoir_texte_lien_source(
                            info_event.N2_EVEN_SOUR_source_liste_ID,
                            liste_source_ID_numero, .04f,
                            tab + 1);
                        // note
                        texte +=
                            Avoir_texte_NOTE_STRUCTURE(
                            info_event.N2_EVEN_NOTE_STRUCTURE_liste_ID,
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero,
                            .04f, // retrait
                            tab + 1);
                        // date de changement
                        if (GH.GHClass.Para.voir_date_changement && info_event.N1_CHAN != null)
                        {
                            if (R.IsNotNullOrEmpty(info_event.N1_CHAN.N1_CHAN_DATE))
                            {
                                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte += Avoir_texte_date_Changement(
                                    info_event.N1_CHAN,
                                    liste_citation_ID_numero,
                                    liste_source_ID_numero,
                                    liste_note_STRUCTURE_ID_numero,
                                    liste_NOTE_RECORD_ID_numero,
                                    false,
                                    tab + 3);
                                texte += espace + "\t\t</div>\n";
                                texte += espace + "\t</div>\n";
                            }
                        }
                        // fin tableau
                        texte += espace + "</div>\n";
                    }
                }
            }
            GC.Collect();
            return texte;
        }
        private static string Avoir_texte_REPO_citation(
            GEDCOMClass.SOURCE_REPOSITORY_CITATION info_REPO,
            string sous_dossier,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            if (info_REPO == null)
                return null;
            string texte = null;
            string espace = Tabulation(tab);
            // Tableau 0px
            texte += espace + "<div class=\"tableau tableau_0px\">\n";
            //NAME
            if (info_REPO.N1_NAME != null)
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info_REPO.N1_NAME + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // Contact
            if (R.IsNotNullOrEmpty(info_REPO.N1_CNTC))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\tContact: " + info_REPO.N1_CNTC + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // addr
            if (R.IsNotNullOrEmpty(info_REPO.N1_ADDR_liste))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += Avoir_texte_adresse( //c
                    info_REPO.N1_SITE,
                    info_REPO.N1_ADDR_liste,
                    info_REPO.N1_PHON_liste,
                    info_REPO.N1_FAX_liste,
                    info_REPO.N1_EMAIL_liste,
                    info_REPO.N1_WWW_liste,
                    sous_dossier,
                    null,
                    null,
                    null,
                    liste_NOTE_RECORD_ID_numero,
                    tab + 3);
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // REPO
            // REPO MEDI
            if (R.IsNotNullOrEmpty(info_REPO.N1_MEDI))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\tFormat: " + Convertir_texte_html(Traduire_mot_anglais(info_REPO.N1_MEDI, true), false, 0) + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // REPO SOURCE_CALL_NUMBER>
            if (R.IsNotNullOrEmpty(info_REPO.N1_CALN))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\tNuméro de la source: " + info_REPO.N1_CALN + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
                // REPO CALN MEDI
                if (R.IsNotNullOrEmpty(info_REPO.N2_CALN_MEDI))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t<div style=\"padding-left:.25in\">\n";
                    texte += espace + "\t\t\t\tMédia: " +
                        Convertir_texte_html(Traduire_mot_anglais(info_REPO.N2_CALN_MEDI, true), false, 0) + "\n";
                    texte += espace + "\t\t\t</div>\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // REPO CALN ITEM
                if (R.IsNotNullOrEmpty(info_REPO.N2_CALN_ITEM))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t<span style=\"padding:.25in\">Item: " + info_REPO.N2_CALN_ITEM + "</span>\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // CALN SHEE
                if (R.IsNotNullOrEmpty(info_REPO.N2_CALN_SHEE))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t<span style=\"padding:.25in\">Numéro de feuille: " + info_REPO.N2_CALN_SHEE + "</span>\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // CALN PAGE
                if (R.IsNotNullOrEmpty(info_REPO.N2_CALN_PAGE))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t<span style=\"padding:.25in\">Numéro de page: " +
                        info_REPO.N2_CALN_PAGE + "</span>\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // REFN
                if (R.IsNotNullOrEmpty(info_REPO.N1_REFN))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\tREFN: " + info_REPO.N1_REFN + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                if (R.IsNotNullOrEmpty(info_REPO.N1_NOTE_STRUCTURE_liste_ID))
                {
                    texte +=
                        Avoir_texte_NOTE_STRUCTURE(
                        info_REPO.N1_NOTE_STRUCTURE_liste_ID,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        0, // retrait
                        tab + 1);
                }
            }
            texte += Avoir_texte_lien_repo(info_REPO.N0_ID, liste_repo_ID_numero, tab + 1);
            texte += espace + "</div>\n";
            GC.Collect();
            return texte;
        }
        private string Texte_bouton_index_conjointe()
        {
            bool[] alphabette = new bool[27];
            ListView lvChoixFamille = Application.OpenForms["GHClass"].Controls["lvChoixFamille"] as ListView;
            for (int f = 0; f < 27; f++)
            {
                alphabette[f] = false;
            }
            string texte = null;
            // détermine si lettre est utiliser
            for (int f = 0; f < lvChoixFamille.Items.Count; f++)
            {
                if (lvChoixFamille.Items[f].SubItems[2].Text != "")
                {
                    char l = lvChoixFamille.Items[f].SubItems[2].Text[0];
                    int v = (int)l - 65;
                    if (v > -1 && v < 26)
                    {
                        alphabette[v] = true;
                    }
                    else
                    {
                        alphabette[26] = true;
                    }
                }
                else
                {
                    alphabette[26] = true;
                }
            }
            texte += "\t\t\t<div style=\"width:700px;border-radius:10px;background-color:#6464FF;margin-left:auto;margin-right:auto;padding:5px;\">\n";
            for (int f = 0; f < 26; f++)
            {
                if (alphabette[f])
                {
                    texte += "\t\t\t\t<a class=\"index\" href=\"f" + (char)(f + 65) + ".html\" >\n";
                    texte += "\t\t\t\t\t" + (char)(f + 65) + "\n";
                    texte += "\t\t\t\t</a>\n";
                }
                else
                {
                    texte += "\t\t\t\t<div class=\" index\">\n";
                    texte += "\t\t\t\t\t" + (char)(f + 65) + "\n";
                    texte += "\t\t\t\t</div>\n";
                }
            }
            if (alphabette[26])
            {
                texte += "\t\t\t\t<a class=\"index\" href=\"fDiver.html\" >\n";
                texte += "\t\t\t\t\t&\n";
                texte += "\t\t\t\t</a>\n";
            }
            else
            {
                texte += "\t\t\t\t<div class=\" index\">\n";
                texte += "\t\t\t\t\t&\n";
                texte += "\t\t\t\t</div>\n";
            }
            texte += "\t\t\t</div>\n";
            GC.Collect();
            return texte;
        }
        private string Bas_Page()
        {
            string texte = null;
            texte += "\t\t<a id=\"bas_page\"></a>\n";
            texte += "\t\t\t<footer>\n";
            texte += Separation(15, null, "000", null, 4);
            DateTime laDate = DateTime.Now;
            texte += "\t\t\t\t<span style=\"padding:5px;\">Généré par GEDCOM-Html V" + Application.ProductVersion + ", le " + laDate.ToString("dd MMMMM yyyy") + "</span>\n";
            texte += Separation(15, null, "000", null, 4);
            texte += "\t\t\t</footer>\n";
            texte += "\t\t</div><!-- fin de page-->\n";
            texte += "\t</body>\n";
            texte += "</html>\n";
            return texte;
        }
        private static string Avoir_texte_carte(
            int Numero_carte,
            string LATI,
            string LONG,
            string info,
            int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            if (!GH.GHClass.Para.voir_carte)
                return "";
            string espace = Tabulation(tab);
            string texte = espace + "<div id=\"map" + Numero_carte.ToString() + "\" class=\"carte\"></div>\n";
            texte += espace + "<script>\n";
            texte += espace + "\t// Initialiser Leaflet\n";
            texte += espace + "\tvar map" + Numero_carte.ToString() +
                " = L.map('map" + Numero_carte.ToString() + "').setView({lon: " + LONG + ", lat: " + LATI + "}, 7);\n";
            texte += espace + "\t// ajouter titre OpenStreetMap\n";
            texte += espace + "\tL.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 25, attribution: '&copy;<a href=\"https://openstreetmap.org/copyright\">Contribution OpenStreetMap</a>'}).addTo(map" + Numero_carte.ToString() + ");\n";
            texte += espace + "\t// Montrer échelle en bas à gauche\n";
            texte += espace + "\tL.control.scale().addTo(map" + Numero_carte.ToString() + ");\n";
            texte += espace + "\t// montrer le lieu sur la carte\n";
            texte += espace + "\tL.marker({lon: " + LONG + ", lat: " + LATI + "}).addTo(map" + Numero_carte.ToString() + ");\n";
            texte += espace + "</script>\n";
            GC.Collect();
            return texte;
        }

        private static int Comparer_annee(string a1, string a2)
        {
            int an1 = 99999999;
            int an2 = 99999999;
            if (a1.All(char.IsDigit))
                an1 = int.Parse(a1);
            if (a2.All(char.IsDigit))
                an2 = int.Parse(a2);
            return Math.Min(an1, an2);
        }
        public static string Convertir_Age_GEDCOM(string age)
        {
            if (age.ToUpper() == "CHILD")
                return "enfant";
            if (age.ToUpper() == "INFANT")
                return "bébé";
            if (age.ToUpper() == "STILLBORN")
                return "mort né";
            char[] espace = { ' ' };
            string[] section = age.Split(espace);
            int nombreSection = section.Length;
            for (int f = 0; f < nombreSection; f++)
            {
                if (section[f].ToLower().Contains(">"))
                    section[f] = "après ";
                if (section[f].ToLower().Contains("<"))
                    section[f] = "avant ";
                if (section[f].ToLower().Contains("y"))
                {
                    section[f] = section[f].Remove(section[f].Length - 1, 1);
                    if (int.Parse(section[f]) == 1)
                    {
                        section[f] += " an";
                    }
                    else
                    {
                        section[f] += " ans";
                    }
                }
                if (section[f].ToLower().Contains("m"))
                {
                    section[f] = section[f].Remove(section[f].Length - 1, 1);
                    section[f] += " mois";
                }
                if (section[f].ToLower().Contains("d"))
                {
                    section[f] = section[f].Remove(section[f].Length - 1, 1);
                    if (int.Parse(section[f]) == 1)
                    {
                        section[f] += " jour";
                    }
                    else
                    {
                        section[f] += " jours";
                    }
                }
            }
            if (nombreSection == 1)
                age = section[0];
            if (nombreSection == 2)
                age = section[0] + " " + section[1];
            if (nombreSection == 3)
                age = section[0] + " " + section[1] + " " + section[2];
            if (nombreSection == 4)
                age = section[0] + " " + section[1] + " " + section[2] + " " + section[3];
            if (age.All(char.IsDigit))
            {
                if (int.Parse(section[0]) == 1)
                    age += " an";
                else
                    age += " ans";
            }
            return age;
        }

        private static string Convertir_texte_html(
            string s,
            bool decalage,
            int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            string espace = Tabulation(tab);
            if (R.IsNullOrEmpty(s))
                return null;
            char carriage_return = (char)0x0D;
            char newline = (char)0x0A;
            string crnl = carriage_return.ToString() + newline.ToString();
            string texte = null;
            s = s.Replace(crnl, "<br />\n" + espace + "\t");
            s = Remplacer_espace_html(s);
            texte += espace + "" + s + "\n";
            return texte;
        }

        public static (
            string, // texte de la date
            int // année de la date
            )
            Convertir_date(string date, bool date_long)
        {
            if (R.IsNullOrEmpty(date))
            {
                return (null, 0);
            }
            int an = 99999999;
            string retourne = date;
            date = date.ToUpper();
            date = Retiter_espace_extrat(date);
            char[] s = { ' ' };
            string[] d = date.Split(s);
            int l = d.Length; // nombre item dans la date ex. CAL 31 DEC 1997 l=4
            /*
                ABT  ABT. = About, meaning the date is not exact.
                AFT AFT.= Event happened after the given date.
                AND and         et
                BEF BEF. = Event happened before the given date.
                BET = Event happened sometime between date 1 AND date 2
                CA  Circa       Environ        Utiliser par  Gedcom Publisher
                CAL = Calculated mathematically, for example, from an event date and age.
                EST = Estimated based on an algorithm using some other event date.
                FROM = Indicates the beginning of a happening or state.
                INT Intepreted  Interprèter
                OR  or          ou             Utiliser par  Gedcom Publisher
                SAY Say         Dit            Utiliser par  Gedcom Publisher
                TO = Indicates the ending of a happening or state
             */
            if (l == 1)
            {
                if (date.All(char.IsDigit))
                    an = int.Parse(date);
                retourne = date;
            }
            else if (l == 2)
            {
                // 0        1       2       3       4       5       6       7  
                // ABT      1852
                if (d[0] == "ABT" || d[0] == "ABT.")
                {
                    if (d[1].All(char.IsDigit))
                        an = int.Parse(d[1]);
                    retourne = "Autour de " + d[1];
                }
                // 0        1       2       3       4       5       6       7  
                // AFT      1852
                else if (d[0] == "AFT" || d[0] == "AFT.")
                {
                    if (d[1].All(char.IsDigit))
                        an = int.Parse(d[1]);
                    retourne = "Autour de " + d[1];
                }
                // 0        1       2       3       4       5       6       7  
                // BEF      1852
                else if (d[0] == "BEF" || d[0] == "BEF.")
                {
                    if (d[1].All(char.IsDigit))
                        an = int.Parse(d[1]);
                    retourne = "Avant " + d[1];
                }
                // 0        1       2       3       4       5       6       7  
                // EST      1852
                else if (d[0] == "EST" || d[0] == "EST.")
                {
                    if (d[1].All(char.IsDigit))
                        an = int.Parse(d[1]);
                    retourne = "Estimé " + d[1];
                }
                // FROM 1852
                else if (d[0] == "FROM")
                {
                    if (d[1].All(char.IsDigit))
                        an = int.Parse(d[1]);
                    retourne = "Depuis " + d[1];
                }
                // 0        1       2       3       4       5       6       7  
                // INT      1852
                else if (d[0] == "INT")
                {
                    if (d[1].All(char.IsDigit))
                        an = int.Parse(d[1]);
                    retourne = "Interprèter " + d[1];
                }
                // 0        1       2       3       4       5       6       7
                // CA      1852
                else if (d[0] == "CA")
                {
                    if (d[1].All(char.IsDigit))
                        an = int.Parse(d[1]);
                    retourne = "Environ " + d[1];
                }
                // 0        1       2       3       4       5       6       7
                // SAY      1852
                else if (d[0] == "SAY")
                {
                    if (d[1].All(char.IsDigit))
                        an = int.Parse(d[1]);
                    retourne = "Dit " + d[1];
                }
                // 0        1       2       3       4       5       6       7
                // CAL      1852
                else if (d[0] == "CAL" || d[0] == "CAL.")
                {
                    if (d[1].All(char.IsDigit))
                        an = int.Parse(d[1]);
                    retourne = "Calculé " + d[1];
                }
                // 0        1       2       3       4       5       6       7
                // TO       1852
                else if (d[0] == "TO")
                {
                    if (d[1].All(char.IsDigit))
                        an = int.Parse(d[1]);
                    retourne = "Jusqu'a " + d[1];
                }
                // 0        1       2       3       4       5       6       7
                // DEC      1852
                // 0        1       2       3       4       5       6       7
                else
                {
                    if (d[1].All(char.IsDigit))
                        an = int.Parse(d[1]);
                    if (date_long)
                        retourne = Convertir_mois_texte(d[0]) + " " + d[1];
                    else
                        retourne = d[1] + "-" + Convertir_mois_chiffre(d[0]);
                }
            }
            else if (l == 3)
            {
                // 0        1       2       3       4       5       6       7  
                // ABT      DEC     1852
                if (d[0] == "ABT" || d[0] == "ABT.")
                {
                    if (d[2].All(char.IsDigit))
                        an = int.Parse(d[2]);
                    if (date_long)
                        retourne = "Autour de " + Convertir_mois_texte(d[1]) + " " + d[2];
                    else
                        retourne = "Autour de " + d[2] + "-" + Convertir_mois_chiffre(d[1]);
                }
                // 0        1       2       3       4       5       6       7
                // AFT      DEC     1852
                else if (d[0] == "AFT" || d[0] == "AFT.")
                {
                    if (d[2].All(char.IsDigit))
                        an = int.Parse(d[2]);
                    if (date_long)
                        retourne = "Après " + Convertir_mois_texte(d[1]) + " " + d[2];
                    else
                        retourne = "Après " + d[2] + "-" + Convertir_mois_chiffre(d[1]);
                }
                // 0        1       2       3       4       5       6       7
                // BEF      DEC     1852
                else if (d[0] == "BEF" || d[0] == "BEF.")
                {
                    if (d[2].All(char.IsDigit))
                        an = int.Parse(d[2]);
                    if (date_long)
                        retourne = "Avant " + Convertir_mois_texte(d[1]) + " " + d[2];
                    else
                        retourne = "Avant " + d[2] + "-" + Convertir_mois_chiffre(d[1]);
                }
                // 0        1       2       3       4       5       6       7
                // BET      DEC     1852
                else if (d[0] == "BET" || d[0] == "BET.")
                {
                    if (d[2].All(char.IsDigit))
                        an = int.Parse(d[2]);
                    if (date_long)
                        retourne = "Entre " + Convertir_mois_texte(d[1]) + " " + d[2];
                    else
                        retourne = "Entre " + d[2] + "-" + Convertir_mois_chiffre(d[1]);
                }
                // 0        1       2       3       4       5       6       7
                // CA      DEC     1852
                else if (d[0] == "CA")
                {
                    if (d[2].All(char.IsDigit))
                        an = int.Parse(d[2]);
                    if (date_long)
                        retourne = "Environ " + Convertir_mois_texte(d[1]) + " " + d[2];
                    else
                        retourne = "Environ " + d[2] + "-" + Convertir_mois_chiffre(d[1]);
                }
                // 0        1       2       3       4       5       6       7
                // CAL      DEC     1852
                else if (d[0] == "CAL" || d[0] == "CAL")
                {
                    if (d[2].All(char.IsDigit))
                        an = int.Parse(d[2]);
                    if (date_long)
                        retourne = "Calculé " + Convertir_mois_texte(d[1]) + " " + d[2];
                    else
                        retourne = "Calculé " + d[2] + " " + Convertir_mois_chiffre(d[1]);
                }
                // 0        1       2       3       4       5       6       7
                // EST      DEC     1852
                else if (d[0] == "EST" || d[0] == "EST.")
                {
                    if (d[2].All(char.IsDigit))
                        an = int.Parse(d[2]);
                    if (date_long)
                        retourne = "Estimé " + Convertir_mois_texte(d[1]) + " " + d[2];
                    else
                        retourne = "Estimé " + d[2] + "-" + Convertir_mois_chiffre(d[1]);
                }
                // 0        1       2       3       4       5       6       7
                // FROM      DEC     1852
                else if (d[0] == "FROM")
                {
                    if (d[2].All(char.IsDigit))
                        an = int.Parse(d[2]);
                    if (date_long)
                        retourne = "À partir de " + Convertir_mois_texte(d[1]) + " " + d[2];
                    else
                        retourne = "À partir de " + d[2] + "-" + Convertir_mois_chiffre(d[1]);
                }
                // 0        1       2       3       4       5       6       7
                // INT      DEC     1852
                else if (d[0] == "INT")
                {
                    if (d[2].All(char.IsDigit))
                        an = int.Parse(d[2]);
                    if (date_long)
                        retourne = "Interprèter " + Convertir_mois_texte(d[1]) + " " + d[2];
                    else
                        retourne = "Interprèter  " + d[2] + "-" + Convertir_mois_chiffre(d[1]);
                }
                // 0        1       2       3       4       5       6       7
                // 1853     OR      1853
                else if (d[1] == "OR")
                {
                    an = Comparer_annee(d[0], d[2]);
                    retourne = d[0] + " ou " + d[2];
                }
                // 1853     TO      1853
                else if (d[1] == "TO")
                {
                    an = Comparer_annee(d[0], d[2]);
                    retourne = d[0] + " à " + d[2];
                }
                // 0        1       2       3       4       5       6       7
                // SAY      DEC     1852
                else if (d[0] == "SAY")
                {
                    if (d[2].All(char.IsDigit))
                        an = int.Parse(d[2]);
                    if (date_long)
                        retourne = "Dit " + Convertir_mois_texte(d[1]) + " " + d[2];
                    else
                        retourne = "Dit " + d[2] + "-" + Convertir_mois_chiffre(d[1]);
                }
                // 0        1       2       3       4       5       6       7
                // TO       DEC     1852
                else if (d[0] == "TO")
                {
                    if (d[2].All(char.IsDigit))
                        an = int.Parse(d[2]);
                    if (date_long)
                        retourne = "Jusqu'a " + Convertir_mois_texte(d[1]) + " " + d[2];
                    else
                        retourne = "Jusqu'a " + d[2] + "-" + Convertir_mois_chiffre(d[1]);
                }
                // 0        1       2       3       4       5       6       7
                // 24       DEC     1852
                else
                {
                    if (d[2].All(char.IsDigit))
                        an = int.Parse(d[2]);
                    if (date_long)
                        retourne = d[0] + " " + Convertir_mois_texte(d[1]) + " " + d[2];
                    else
                    {
                        string a = d[0];
                        retourne = d[2] + "-" + Convertir_mois_chiffre(d[1]) + "-" + Convertir_jour_2chiffre(d[0]);
                    }
                }
            }
            else if (l == 4)
            {
                // 0        1       2       3       4       5       6       7  
                // ABT      24      DEC     1852
                if (d[0] == "ABT" || d[0] == "ABT.")
                {
                    if (d[3].All(char.IsDigit))
                        an = int.Parse(d[3]);
                    if (date_long)
                        retourne = "Autour de " + d[1] + " " + Convertir_mois_texte(d[2]) + " " + d[3];
                    else
                        retourne = "Autour de " + d[3] + " " + Convertir_mois_chiffre(d[2]) + "-" + d[1];
                }
                // 0        1       2       3       4       5       6       7  
                // AFT      24      DEC     1852
                else if (d[0] == "AFT" || d[0] == "AFT.")
                {
                    if (d[3].All(char.IsDigit))
                        an = int.Parse(d[3]);
                    if (date_long)
                        retourne = "Après " + d[1] + " " + Convertir_mois_texte(d[2]) + " " + d[3];
                    else
                        retourne = "Après " + d[3] + "-" + Convertir_mois_chiffre(d[2]) + "-" + Convertir_jour_2chiffre(d[1]);
                }
                // 0        1       2       3       4       5       6       7
                // BEF      24       DEC    1852
                else if (d[0] == "BEF" || d[0] == "BEF.")
                {
                    if (d[3].All(char.IsDigit))
                        an = int.Parse(d[3]);
                    if (date_long)
                        retourne = "Avant " + d[1] + " " + Convertir_mois_texte(d[2]) + " " + d[3];
                    else
                        retourne = "Avant " + d[3] + "-" + Convertir_mois_chiffre(d[2]) + "-" + Convertir_jour_2chiffre(d[1]);
                }
                // 0        1       2       3       4       5       6       7
                // BET     1852    AND     1853
                else if ((d[0] == "BET" || d[0] == "BET.") && d[2] == "AND")
                {
                    an = Comparer_annee(d[1], d[3]);
                    retourne = "Entre " + d[1] + " et " + d[3];
                }
                // 0        1       2       3       4       5       6       7
                // CA      24      DEC     1852
                else if (d[0] == "CA")
                {
                    if (d[3].All(char.IsDigit))
                        an = int.Parse(d[3]);
                    if (date_long)
                        retourne = "Environ " + d[1] + " " + Convertir_mois_texte(d[2]) + " " + d[3];
                    else
                        retourne = "Environ " + d[3] + "-" + Convertir_mois_chiffre(d[2]) + "-" + Convertir_jour_2chiffre(d[1]);
                }
                // 0        1       2       3       4       5       6       7
                // CAL     24      DEC     1852
                else if (d[0] == "CAL" || d[0] == "CAL.")
                {
                    if (d[3].All(char.IsDigit))
                        an = int.Parse(d[3]);
                    if (date_long)
                        retourne = "Calculé " + d[1] + " " + Convertir_mois_texte(d[2]) + " " + d[3];
                    else
                        retourne = "Calculé " + d[3] + "-" + Convertir_mois_chiffre(d[2]) + "-" + Convertir_jour_2chiffre(d[1]);
                }

                // 0        1       2       3       4       5       6       7
                // EST      24      DEC     1852
                else if (d[0] == "EST" || d[0] == "EST.")
                {
                    if (d[3].All(char.IsDigit))
                        an = int.Parse(d[3]);
                    if (date_long)
                        retourne = "Estimé " + d[1] + " " + Convertir_mois_texte(d[2]) + " " + d[3];
                    else
                        retourne = "Estimé " + d[3] + "-" + Convertir_mois_chiffre(d[2]) + "-" + Convertir_jour_2chiffre(d[1]);
                }
                // 0        1       2       3       4       5       6       7
                // FROM     1852    TO      1853
                else if (d[0] == "FROM" && d[2] == "TO")
                {
                    an = Comparer_annee(d[1], d[3]);
                    retourne = "De " + d[1] + " à " + d[3];
                }
                // 0        1       2       3       4       5       6       7
                // FROM     01      JAN     1852
                else if (d[0] == "FROM")
                {
                    if (d[3].All(char.IsDigit))
                        an = int.Parse(d[3]);
                    if (date_long)
                        retourne = "Depuis " + d[1] + " " + Convertir_mois_texte(d[2]) + " " + d[3];
                    else
                        retourne = "Depuis " + d[3] + "-" + Convertir_mois_chiffre(d[2] + "-" + Convertir_jour_2chiffre(d[1]));
                }
                // 0        1       2       3       4       5       6       7
                // INT      24      DEC     1852
                else if (d[0] == "INT")
                {
                    if (d[3].All(char.IsDigit))
                        an = int.Parse(d[3]);
                    if (date_long)
                        retourne = "Interprèter " + d[1] + " " + Convertir_mois_texte(d[2]) + " " + d[3];
                    else
                        retourne = "Interprèter " + d[3] + "-" + Convertir_mois_chiffre(d[2]) + "-" + "-" + Convertir_jour_2chiffre(d[1]);
                }
                // 0        1       2       3       4       5       6       7
                // JAN      1853    OR      1854
                else if (d[2] == "OR")
                {
                    an = Comparer_annee(d[1], d[3]);
                    if (date_long)
                        retourne = Convertir_mois_texte(d[0]) + " " + d[1] + " ou " + d[3];
                    else
                        retourne = Convertir_mois_chiffre(d[1]) + " " + d[0] + " ou " + d[3];
                }
                // 0        1       2       3       4       5       6       7
                // 1853     OR      JAN     1853
                else if (d[1] == "OR")
                {
                    an = Comparer_annee(d[0], d[3]);
                    if (date_long)
                        retourne = d[0] + " ou " + Convertir_mois_texte(d[2]) + " " + d[3];
                    else
                        retourne = d[0] + " ou " + d[3] + "-" + Convertir_mois_chiffre(d[2]);
                }
                // 0        1       2       3       4       5       6       7
                // SAY      24      DEC     1852
                else if (d[0] == "SAY")
                {
                    if (d[3].All(char.IsDigit))
                        an = int.Parse(d[3]);
                    if (date_long)
                        retourne = "Dit " + d[1] + " " + Convertir_mois_texte(d[2]) + " " + d[3];
                    else
                        retourne = "Dit " + d[3] + "-" + Convertir_mois_chiffre(d[2]) + "-" + Convertir_jour_2chiffre(d[1]);
                }
                // 0        1       2       3       4       5       6       7
                // TO       24      DEC     1852
                else if (d[0] == "TO")
                {
                    if (d[3].All(char.IsDigit))
                        an = int.Parse(d[3]);
                    if (date_long)
                        retourne = "Jusqu'au " + d[1] + " " + Convertir_mois_texte(d[2]) + " " + d[3];
                    else
                        retourne = "Jusqu'au " + d[3] + "-" + Convertir_mois_chiffre(d[2]) + "-" + Convertir_jour_2chiffre(d[1]);
                }
                else
                    GEDCOMClass.DEBUG("Problème pour convertir la date " + date); // ###DEBUG###
            }
            else if (l == 5)
            {
                // 0        1       2       3       4       5       6       7  
                // BET     1852    AND     DEC     1853
                if ((d[0] == "BET" || d[0] == "BET.") && d[2] == "AND")
                {
                    an = Comparer_annee(d[1], d[4]);
                    if (date_long)
                        retourne = "Entre " + d[1] + " et " + Convertir_mois_texte(d[3] + " " + d[4]);
                    else
                        retourne = "Entre " + d[1] + " et " + d[4] + "-" + Convertir_mois_chiffre(d[3]);
                }
                // 0        1       2       3       4       5       6       7
                // BET     DEC     1852    AND     1853 
                else if ((d[0] == "BET" || d[0] == "BET.") && d[3] == "AND")
                {
                    an = Comparer_annee(d[2], d[4]);
                    if (date_long)
                        retourne = "Entre " + Convertir_mois_texte(d[1]) + " " + d[2] + " et " + d[4];
                    else
                        retourne = "Entre " + d[2] + "_" + Convertir_mois_chiffre(d[1]) + " et " + d[4];
                    ;
                }
                // 0        1       2       3       4       5       6       7
                // FROM     1852    TO      DEC     1853
                else if (d[0] == "FROM" && d[2] == "TO")
                {
                    an = Comparer_annee(d[1], d[3]);
                    if (date_long)
                        retourne = "De " + d[1] + " à " + Convertir_mois_texte(d[3]) + " " + d[4];
                    else
                        retourne = "De " + d[1] + " à " + d[4] + "-" + Convertir_mois_chiffre(d[3]);
                }
                // 0        1       2       3       4       5       6       7
                // FROM     DEC     1852    TO      1853
                else if (d[0] == "FROM" && d[3] == "TO")
                {
                    an = Comparer_annee(d[2], d[4]);
                    if (date_long)
                        retourne = "De " + Convertir_mois_texte(d[1]) + " " + d[2] + " à " + d[4];
                    else
                        retourne = "De " + d[2] + "-" + Convertir_mois_chiffre(d[1]) + " à " + d[4];
                }
                // 0        1       2       3       4       5       6       7
                // 1853     OR      1       JAN     1953 
                else if (d[1] == "OR")
                {
                    an = Comparer_annee(d[0], d[4]);
                    if (date_long)
                        retourne = d[0] + " ou " + d[2] + " " + Convertir_mois_texte(d[3]) + " " + d[4];
                    else
                        retourne = d[0] + " ou " + d[4] + "-" + Convertir_mois_chiffre(d[3]) + "-" + Convertir_jour_2chiffre(d[2]);
                }
                // 0        1       2       3       4       5       6       7
                // JAN      1853    OR      JAN     1854
                else if (d[2] == "OR")
                {
                    an = Comparer_annee(d[1], d[4]);
                    if (date_long)
                        retourne = Convertir_mois_texte(d[0]) + " " + d[1] + " ou " + Convertir_mois_texte(d[3]) + " " + d[4];
                    else
                        retourne = d[1] + "-" + Convertir_mois_chiffre(d[0]) + " ou " + d[4] + "-" + Convertir_mois_chiffre(d[3]);
                }
                // 0        1       2       3       4       5       6       7
                // 1        JAN     1853    OR      1854    
                else if (d[3] == "OR")
                {
                    an = Comparer_annee(d[2], d[4]);
                    if (date_long)
                        retourne = d[0] + " " + Convertir_mois_texte(d[1]) + " " + d[2] + " ou " + d[4];
                    else
                        retourne = d[2] + "-" + Convertir_mois_chiffre(d[1]) + "-" + Convertir_jour_2chiffre(d[0]) + " ou " + d[4];
                }
                else
                    GEDCOMClass.DEBUG("Problème pour convertir la date " + date); // ###DEBUG###
            }
            else if (l == 6)
            {
                // 0        1       2       3       4       5       6       7  
                // BET     1852    AND     24      DEC     1853
                if ((d[0] == "BET" || d[0] == "BET.") && d[2] == "AND")
                {
                    an = Comparer_annee(d[1], d[5]);
                    if (date_long)
                        retourne = "Entre " + d[1] + " et " + d[3] + " " + Convertir_mois_texte(d[4]) + " " + d[5];
                    else
                        retourne = "Entre " + d[1] + " et " + d[5] + "-" + Convertir_mois_chiffre(d[4]) + "-" + Convertir_jour_2chiffre(d[3]);
                }
                // 0        1       2       3       4       5       6       7
                // BET     DEC     1852    AND     DEC     1853
                else if ((d[0] == "BET" || d[0] == "BET.") && d[3] == "AND")
                {
                    an = Comparer_annee(d[2], d[5]);
                    if (date_long)
                        retourne = Convertir_mois_texte(d[1]) + " " + d[2] + " et " + Convertir_mois_texte(d[4]) + " " + " " + d[5];
                    else
                        retourne = "Entre " + d[2] + "-" + Convertir_mois_chiffre(d[1]) + " et " + d[5] + "-" + Convertir_mois_chiffre(d[4]);
                }
                // 0        1       2       3       4       5       6       7  
                // BET      24     DEC     1852    AND     1853
                else if ((d[0] == "BET" || d[0] == "BET.") && d[4] == "AND")
                {
                    an = Comparer_annee(d[3], d[5]);
                    if (date_long)
                        retourne = "Entre " + d[1] + " " + Convertir_mois_texte(d[2]) + " " + d[3] + " et " + d[5];
                    else
                        retourne = "Entre " + d[3] + "-" + Convertir_mois_chiffre(d[2]) + "-" + Convertir_jour_2chiffre(d[1]) + " et " + d[5];
                }
                // 0        1       2       3       4       5       6       7
                // FROM     24      DEC     1852    TO      1853
                else if (d[0] == "FROM" && d[4] == "TO")
                {
                    an = Comparer_annee(d[3], d[5]);
                    if (date_long)
                        retourne = "Du " + d[1] + " " + Convertir_mois_texte(d[2]) + " " + " " + d[3] + " à " + d[5];
                    else
                        retourne = "Du " + d[3] + "-" + Convertir_mois_chiffre(d[2]) + "-" + Convertir_jour_2chiffre(d[2]) + " à " + d[5];
                }
                // 0        1       2       3       4       5       6       7  
                // FROM     DEC     1852    TO      DEC     1853
                else if (d[0] == "FROM" && d[3] == "TO")
                {
                    an = Comparer_annee(d[2], d[5]);
                    if (date_long)
                        retourne = "De " + Convertir_mois_texte(d[1]) + " " + d[2] + " à " + Convertir_mois_texte(d[4]) + " " + d[5];
                    else
                        retourne = "De " + d[2] + "-" + Convertir_mois_chiffre(d[1]) + " à " + d[5] + "-" + Convertir_mois_chiffre(d[4]);
                }
                // 0        1       2       3       4       5       6       7  
                // FROM     24      DEC     1852    TO      1853    
                else if (d[0] == "FROM" && d[4] == "TO")
                {
                    an = Comparer_annee(d[3], d[5]);
                    if (date_long)
                        retourne = "Du " + Convertir_mois_texte(d[1]) + " " + d[2] + " " + d[3] + " à " + d[5];
                    else
                        retourne = "Du " + d[3] + "-" + Convertir_mois_chiffre(d[2]) + "-" + Convertir_jour_2chiffre(d[1]) + " à " + d[5];
                }
                // 0        1       2       3       4       5       6       7  
                // JAN      1853   OR       1       JAN     1853
                else if (d[0] == "FROM" && d[4] == "TO")
                {
                    an = Comparer_annee(d[1], d[5]);
                    if (date_long)
                        retourne = Convertir_mois_texte(d[0]) + " " + d[1] + " ou " + d[3] + " " + Convertir_mois_texte(d[4]) + " " + d[5];
                    else
                        retourne = d[1] + "-" + Convertir_mois_chiffre(d[1]) + " ou " + d[5] + "-" + Convertir_mois_chiffre(d[4]) + "-" + Convertir_jour_2chiffre(d[3]);
                }
                // 0        1       2       3       4       5       6       7
                // JAN      1853    OR      1       JAN     1854
                else if (d[2] == "OR")
                {
                    an = Comparer_annee(d[1], d[5]);
                    if (date_long)
                        retourne = Convertir_mois_texte(d[0]) + " " + d[1] + " ou " + d[1] + " " + Convertir_mois_texte(d[4]) + " " + d[5];
                    else
                        retourne = d[1] + "-" + Convertir_mois_chiffre(d[0]) + " ou " + d[5] + "-" + Convertir_mois_chiffre(d[4]) + "-" + Convertir_jour_2chiffre(d[3]);
                }
                // 0        1       2       3       4       5       6       7
                // 1        JAN     1853    OR      JAN     1854
                else if (d[3] == "OR")
                {
                    an = Comparer_annee(d[2], d[5]);
                    if (date_long)
                        retourne = d[0] + Convertir_mois_texte(d[1]) + d[2] + " ou " + Convertir_mois_texte(d[4]) + " " + d[5];
                    else
                        retourne = d[2] + "-" + Convertir_mois_chiffre(d[1]) + "-" + Convertir_jour_2chiffre(d[0]) + " ou " + d[5] + "-" + Convertir_mois_chiffre(d[4]);
                }
                else
                    GEDCOMClass.DEBUG("Problème pour convertir la date " + date); // ###DEBUG###
            }
            else if (l == 7)
            {
                // 0        1       2       3       4       5       6       7   
                // BET      DEC     1852    AND     24      DEC     1853
                if ((d[0] == "BET" || d[0] == "BET.") && d[3] == "AND")
                {
                    an = Comparer_annee(d[2], d[6]);
                    if (date_long)
                        retourne = "Entre " + Convertir_mois_texte(d[1]) + d[2] + " et " + d[4] + " " + Convertir_mois_texte(d[5]) + " " + d[6];
                    else
                        retourne = "Entre " + d[2] + "-" + Convertir_mois_chiffre(d[1]) + " et " + d[6] + "-" + Convertir_mois_chiffre(d[5]) + "-" + Convertir_jour_2chiffre(d[4]);
                }
                // 0        1       2       3       4       5       6       7
                // BET      24     DEC     1852    AND      DEC     1853
                else if ((d[0] == "BET" || d[0] == "BET.") && d[4] == "AND")
                {
                    an = Comparer_annee(d[3], d[6]);
                    if (date_long)
                        retourne = "Entre " + d[1] + " " + Convertir_mois_texte(d[2]) + " " + d[3] + " et " + Convertir_mois_texte(d[5]) + " " + d[6];
                    else
                        retourne = "Entre " + d[3] + "-" + Convertir_mois_chiffre(d[2]) + "-" + Convertir_jour_2chiffre(d[1]) + " et " + d[6] + "-" + Convertir_mois_chiffre(d[5]);
                }
                // 0        1       2       3       4       5       6       7   
                // FROM     DEC     1852    TO      24      DEC     1853
                else if (d[0] == "FROM" && d[3] == "TO")
                {
                    an = Comparer_annee(d[2], d[6]);
                    if (date_long)
                        retourne = "De " + Convertir_mois_texte(d[1]) + " " + d[2] + " à " + d[4] + " " + Convertir_mois_texte(d[5]) + " " + d[6];
                    else
                        retourne = "De " + d[2] + "-" + Convertir_mois_chiffre(d[1]) + "-" + d[2] + " à " + d[6] + "-" + Convertir_mois_chiffre(d[5]) + "-" + Convertir_jour_2chiffre(d[4]);
                }
                // 0        1       2       3       4       5       6       7
                // FROM     24      DEC     1852    TO      DEC     1853    
                else if (d[0] == "FROM" && d[4] == "TO")
                {
                    an = Comparer_annee(d[3], d[6]);
                    if (date_long)
                        retourne = "Du " + d[1] + Convertir_mois_texte(d[2]) + " " + d[3] + " à " + Convertir_mois_texte(d[5]) + " " + d[6];
                    else
                        retourne = "Du " + d[3] + "-" + Convertir_mois_chiffre(d[2]) + "-" + Convertir_jour_2chiffre(d[1]) + " à " + d[6] + "-" + Convertir_mois_chiffre(d[5]);
                }
                // 0        1       2       3       4       5       6       7
                // FROM     DEC     1852    TO      24      DEC     1853    
                else if (d[0] == "FROM" && d[4] == "TO")
                {
                    an = Comparer_annee(d[2], d[6]);
                    if (date_long)
                        retourne = "De " + Convertir_mois_texte(d[1]) + " " + d[2] + " au " + d[4] + Convertir_mois_texte(d[5]) + " " + d[6];
                    else
                        retourne = "De " + d[2] + "-" + Convertir_mois_chiffre(d[1] + " au " + d[6] + "-" + Convertir_date_GEDCOM_chiffre(d[5]) + " " + Convertir_jour_2chiffre(d[4]));
                }
                // 0        1       2       3       4       5       6       7   
                // 31       DEC     1852    TO      24      DEC     1853    
                else if (d[3] == "TO")
                {
                    an = Comparer_annee(d[2], d[6]);
                    if (date_long)
                        retourne = "Du " + d[0] + " " + Convertir_mois_texte(d[1]) + " " + d[2] + " au " + d[4] + " " + Convertir_mois_texte(d[5]) + " " + d[6];
                    else
                        retourne = "Du " + d[2] + "- " + Convertir_mois_chiffre(d[1]) + "-" + Convertir_jour_2chiffre(d[0]) + " au " + d[6] + "-" + Convertir_mois_chiffre(d[5]) + "-" + Convertir_jour_2chiffre(d[4]);
                }
                // 0        1       2       3       4       5       6       7
                // 1        JAN     1853    OR      1       JAN     1854
                else if (d[3] == "OR")
                {
                    an = Comparer_annee(d[2], d[6]);
                    if (date_long)
                        retourne = d[0] + " " + Convertir_mois_texte(d[1]) + " " + d[2] + " ou " + d[4] + " " + Convertir_mois_texte(d[5]) + " " + d[6];
                    else
                        retourne = d[2] + "-" + Convertir_mois_chiffre(d[1]) + "-" + Convertir_jour_2chiffre(d[0]) + " ou " + d[6] + "-" + Convertir_mois_chiffre(d[5]) + "-" + Convertir_jour_2chiffre(d[4]);
                }
                else
                    GEDCOMClass.DEBUG("Problème pour convertir la date " + date); // ###DEBUG###
            }
            else if (l == 8)
            {
                // 0        1       2       3       4       5       6       7   
                // BET      24     DEC     1852    AND     24      DEC     1853
                if ((d[0] == "BET" || d[0] == "BET.") && d[4] == "AND")
                {
                    an = Comparer_annee(d[3], d[7]);
                    if (date_long)
                        retourne = "Entre " + d[1] + " " + Convertir_mois_texte(d[2]) + d[3] + " et " + d[5] + " " + Convertir_mois_texte(d[6]) + " " + d[7];
                    else
                        retourne = "Entre " + d[3] + "-" + Convertir_mois_chiffre(d[2]) + "-" + Convertir_jour_2chiffre(d[1]) + " et " + d[7] + "-" + Convertir_mois_chiffre(d[6]) + "-" + Convertir_jour_2chiffre(d[5]);
                }
                // 0        1       2       3       4       5       6       7
                // FROM     24      DEC     1852    TO      24      DEC     1853    
                else if (d[0] == "FROM" && d[4] == "TO")
                {
                    an = Comparer_annee(d[3], d[7]);
                    if (date_long)
                        retourne = "Du " + d[1] + " " + Convertir_mois_texte(d[2]) + " " + d[3] + " au " + d[5] + " " + Convertir_mois_texte(d[6]) + " " + d[7];
                    else
                        retourne = "Du " + d[3] + "-" + Convertir_mois_chiffre(d[2]) + "-" + Convertir_jour_2chiffre(d[1]) + " au " + d[7] + "-" + Convertir_mois_chiffre(d[6]) + "-" + Convertir_jour_2chiffre(d[5]);
                }
                else
                    GEDCOMClass.DEBUG("Problème pour convertir la date " + date); // ###DEBUG###
            }
            else
                GEDCOMClass.DEBUG("Problème pour convertir la date " + date); // ###DEBUG###
            //R..Z("retourne " + retourne);
            return (retourne, an);
        }

        private static string Convertir_date_GEDCOM_chiffre(params string[] date)
        {
            //      0       1       2
            // l=1  1958
            // l=2  JAN     1958
            // l=3  1       jan     1958
            // si date  = "" ou null retourner ""
            if (date == null)
                return null;
            char zero = '0';
            int l = date.Length;
            if (l == 1)
            {
                return date[0];
            }
            if (l == 2)
            {
                return date[1] + "-" + Convertir_mois_chiffre(date[0]);
            }
            if (l == 3)
            {
                return date[2] + "-" + Convertir_mois_chiffre(date[1]) + "-" + date[0].PadLeft(2, zero);
            }
            return null;
        }

        private static string Convertir_filiation(string s)
        {
            switch (s)
            {
                case "LEGITIMATE_CHILD":
                    return "Enfant légitime";
                case "ADULTEROUS_CHILD":
                    return "Enfant adultérin";
                case "RECOGNIZED_CHILD":
                    return "Enfant reconnu";
                case "NATURAL_CHILD":
                    return "Enfant naturel";
                case "CHILD_FOUND":
                    return "Enfant trouvé";
                case "ADOPTED_CHILD":
                    return "Enfant adopté";
                case "STILLBORN_CHILD":
                    return "Mort - Né";
                case "RELATIONSHIP_UNKNOW":
                    return "Non Connue";
            }
            return s;
        }

        private static string Convertir_statut_marital(string s)
        {
            switch (s)
            {
                case "D":
                    return "Célibataire mais légalement divorcé au moment de l'évènement.";
                case "M":
                    return "Marié au moment de l'évènement.";
                case "S":
                    return "Célibataire, jamais marié au moment de l'évènement.";
                case "W":
                    return "Célibataire en raison du décès d'un conjoint.";
            }
            return s;
        }
        private static string Convertir_ROLE_TAG(string s)
        {
            switch (s)
            {
                case "BUYR":
                    return "Acheteur";
                case "CHIL":
                    return "Enfant";
                case "FATH":
                    return "Père";
                case "GODP":
                    return "GODP";
                case "HDOH":
                    return "";
                case "HDOG":
                    return "HDOG";
                case "HEIR":
                    return "A hérité ou a le droit d'hériter d'une succession";
                case "HFAT":
                    return "Agissant en tant que père du mari pour l'évènement";
                case "HMOT":
                    return "Agissant en tant que mère du mari pour l'évènement";
                case "HUSB":
                    return "Conjoint";
                case "INDI":
                    return "Individu";
                case "INFT":
                    return "Rapporté des faits concernant l'évènement";
                case "LEGA":
                    return "Légale";
                case "MEMBER":
                    return "Membre";
                case "MOTH":
                    return "Mère";
                case "OFFI":
                    return "Officiel";
                case "PARE":
                    return "PARE";
                case "PHUS":
                    return "Conjoint précédent";
                case "PWIF":
                    return "Conjointe précéden";
                case "RECO":
                    return "Enregistreur";
                case "REL":
                    return "Relatif";
                case "ROLE":
                    return "Rôle";
                case "SELR":
                    return "Vendeur";
                case "TXPY":
                    return "A fait l'objet d'une cotisation";
                case "WFAT":
                    return "Agissant en tant que père de la femme pour l'évènement.";
                case "WIFE":
                    return "Conjointe";
                case "WITN":
                    return "Témoin";
                case "WMOT":
                    return "";

            }
            return s;
        }
        private static string Convertir_SEX(string s)
        {
            switch (s)
            {
                case "M":
                    return "Masculin";
                case "F":
                    return "Féminin";
                case "X":
                    return "Intersexe";
                case "U":
                    return "Inconnue";
                case "N":
                    return "Pas enregistré";
            }
            return s;
        }
        private static string Convertir_mot_anglais(string s)
        {
            if (R.IsNullOrEmpty(s))
                return null;
            switch (s.ToLower())
            {
                case "audio":
                    s = "Audio";
                    break;
                case "active":
                    s = "Active";
                    break;
                case "book":
                    s = "Livre";
                    break;
                case "card":
                    s = "Fiche";
                    break;
                case "census":
                    s = "Recensement";
                    break;
                case "challenged":
                    s = "Lier cet enfant à cette famille est suspect, mais le lien n’a été ni prouvé ni réfuté.";
                    break;
                case "chil":
                    s = "Enfant";
                    break;
                case "court":
                    s = "Dossier d'un tribunal pénal ou civil";
                    break;
                case "church":
                    s = "Record d'église";
                    break;
                case "disproven":
                    s = "Certains prétendent que cet enfant appartient à cette famille, mais le lien a été réfutée.";
                    break;
                case "electronic":
                    s = "Numérique";
                    break;
                case "extract":
                    s = "Extrait";
                    break;
                case "fath":
                    s = "Père";
                    break;
                case "fiche":
                    s = "Fiche";
                    break;
                case "film":
                    s = "Film";
                    break;
                case "found":
                    s = "Trouvé";
                    break;
                case "history":
                    s = "Compte rendu historique publié";
                    break;
                case "husb":
                    s = "Conjoint";
                    break;
                case "interview":
                    s = "Entrevue";
                    break;
                case "journal":
                    s = "Dossier ou un journal personnel";
                    break;
                case "land":
                    s = "Enregistrement des propriétés foncières ou des transactions";
                    break;
                case "letter":
                    s = "Lettre ou autre communication écrite";
                    break;
                case "magazine":
                    s = "Magazine";
                    break;
                case "manuscript":
                    s = "Manuscrit";
                    break;
                case "map":
                    s = "Carte géographique";
                    break;
                case "military":
                    s = "Record militaire";
                    break;
                case "moth":
                    s = "Mère";
                    break;
                case "newspaper":
                    s = "Journal";
                    break;
                case "no":
                    s = "Nom";
                    break;
                case "ordered":
                    s = "Commandé";
                    break;
                case "original":
                    s = "Original";
                    break;
                case "periodical":
                    s = "Périodique";
                    break;
                case "personal":
                    s = "Source à partir de la mémoire d'une personne.";
                    break;
                case "photo":
                    s = "Photographie";
                    break;
                case "photocopy":
                    s = "Photocopie";
                    break;
                case "planned":
                    s = "Prévu";
                    break;
                case "proved":
                    s = "Prouvé";
                    break;
                case "recited":
                    s = "Généalogie récitée";
                    break;
                case "spou":
                    s = "Conjoint(e)";
                    break;
                case "tradition":
                    s = "Source par le bouche-à-oreille";
                    break;
                case "transcript":
                    s = "Transcript";
                    break;
                case "tombstone":
                    s = "Pierre tombale";
                    break;
                case "video":
                    s = "Vidéo";
                    break;
                case "vital":
                    s = "Acte vital créé par une agence gouvernementale";
                    break;
                case "wife":
                    s = "Conjointe";
                    break;
            }
            return s;
        }
        private static string Convertir_jour_2chiffre(string j)
        {
            if (j.Count() == 1)
            {
                j = "0" + j;
            }
            return j;
        }
        private static string Convertir_mois_chiffre(string mois)
        {
            string m = mois.ToUpper();
            switch (m)
            {
                case "JAN":
                case "JANUARY":
                    m = "01";
                    break;
                case "FEB":
                case "FEBRUARY":
                    m = "02";
                    break;
                case "MAR":
                case "MARS":
                    m = "03";
                    break;
                case "APR":
                case "APRIL":
                    m = "04";
                    break;
                case "MAY":
                    m = "05";
                    break;
                case "JUN":
                case "JUNE":
                    m = "06";
                    break;
                case "JUL":
                case "JULY":
                    m = "07";
                    break;
                case "AUG":
                case "AUGUST":
                    m = "08";
                    break;
                case "SEP":
                case "SEPTEMBER":
                    m = "09";
                    break;
                case "OCT":
                case "OCTOBER":
                    m = "10";
                    break;
                case "NOV":
                case "NOVEMBER":
                    m = "11";
                    break;
                case "DEC":
                case "DECEMBER":
                    m = "12";
                    break;
            }
            return m;
        }
        private static string Convertir_mois_texte(string mois)
        {
            string m = mois.ToUpper();
            switch (m)
            {
                case "JAN":
                case "JANUARY":
                    m = "janvier";
                    break;
                case "FEB":
                case "FEBRUARY":
                    m = "février";
                    break;
                case "MAR":
                case "MARS":
                    m = "mars";
                    break;
                case "APR":
                case "APRIL":
                    m = "avril";
                    break;
                case "MAY":
                    m = "mai";
                    break;
                case "JUN":
                case "JUNE":
                    m = "juin";
                    break;
                case "JUL":
                case "JULY":
                    m = "Juillet";
                    break;
                case "AUG":
                case "AUGUST":
                    m = "août";
                    break;
                case "SEP":
                case "SEPTEMBER":
                    m = "septembre";
                    break;
                case "OCT":
                case "OCTOBER":
                    m = "octobre";
                    break;
                case "NOV":
                case "NOVEMBER":
                    m = "novembre";
                    break;
                case "DEC":
                case "DECEMBER":
                    m = "décembre";
                    break;
            }
            return m;
        }
        private static string Convertir_EVEN_en_texte(
            string action,
            string ligne = null
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {

            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + "<br> Action=" + action + "<br> ligne="+ ligne);
            if (R.IsNullOrEmpty(action))
                return null;
            char carriage_return = (char)0x0D;
            char newline = (char)0x0A;
            string crnl = carriage_return.ToString() + newline.ToString();
            string temp = action.Replace(crnl, " ");
            string[] section = temp.Split(' ');
            section[0] = section[0].ToUpper();
            switch (section[0])
            {
                case "ADOP":
                    return action.Replace("ADOP", "Adoption");
                case "ANUL":
                    return action.Replace("ANUL", "Annulation de mariage");
                case "BAPL":
                    return action.Replace("BAPL", "Baptême huit ans ou plus SDJ");
                case "BAPM":
                    return action.Replace("BAPM", "Baptême");
                case "BARM":
                    return action.Replace("BARM", "Bar Mitzvah");
                case "BASM":
                    return action.Replace("BASM", "Bat Mitsva");
                case "BIRT":
                    return action.Replace("BIRT", "Naissance");
                case "BLES":
                    return action.Replace("BLES", "Bénédiction");
                case "BURI":
                    return action.Replace("BURI", "Inhumation");
                case "BUYR":
                    return action.Replace("BUYR", "Acheteur");
                case "CAST":
                    return action.Replace("CAST", "Caste ") + ligne;
                case "CENS":
                    return action.Replace("CENS", "Recensement");
                case "CHIL":
                    return action.Replace("CHIL", "Enfant");
                case "CHR":
                    return action.Replace("CHR", "Baptême christianisme");
                case "CHRA":
                    return action.Replace("CHRA", "Baptême adulte");
                case "CONF":
                    return action.Replace("CONF", "Confirmation");
                case "CONL":
                    return action.Replace("CONL", "Confirmation LDS");
                case "CREM":
                    return action.Replace("CREM", "Crémation");
                case "DEAT":
                    return action.Replace("DEAT", "Décès");
                case "DIV":
                    return action.Replace("DIV", "Divorce");
                case "DIVF":
                    return action.Replace("DIVF", "Demande de divorce par épouse");
                case "DSCR":
                    return action.Replace("DSCR", "Description physique");
                case "EDUC":
                    return action.Replace("EDUC", "Éducation ") + ligne;
                case "EMIG":
                    return action.Replace("EMIG", "Immigration");
                case "ENDL":
                    return action.Replace("ENDL", "Dotation");
                case "ENGA":
                    return action.Replace("ENGA", "Fiançailles");
                case "EVEN":
                    return action.Replace("EVEN", "Évènement ") + ligne;
                case "FACT":
                    return action.Replace("FACT", "Fait ") + ligne;
                case "FATH":
                    return action.Replace("FATH", "Père");
                case "FCOM":
                    return action.Replace("FCOM", "Première communion");
                case "GODP":
                    return action.Replace("GODP", "Parrain/Marainne");
                case "GRAD":
                    return action.Replace("GRAD", "Graduation");
                case "HDOH":
                    return action.Replace("HDOH", "Chef de maison");
                case "HDOG":
                    return action.Replace("HDOG", "HDOG");
                case "HEIR":
                    return action.Replace("HEIR", "Héritier");
                case "HFAT":
                    return action.Replace("HFAT", "Père du conjoint");
                case "HMOT":
                    return action.Replace("HMOT", "Mère du conjoint");
                case "HUSB":
                    return action.Replace("HUSB", "Conjoint");
                case "IDNO":
                    return action.Replace("IDNO", "Numéro ID nationnal ") + ligne;
                case "IMMI":
                    return action.Replace("IMMI", "Immigration");
                case "INDI":
                    return action.Replace("INDI", "Individu");
                case "INDO":
                    return action.Replace("INDO", "Numéro d'identification ") + ligne;
                case "INFT":
                    return action.Replace("INFT", "Informateur");
                case "LEGA":
                    return action.Replace("LEGA", "Légataire");
                case "MARB":
                    return action.Replace("MARB", "Publication des bans");
                case "MARC":
                    return action.Replace("MARC", "contrat de mariage");
                case "MARL":
                    return action.Replace("MARL", "Licence de mariage");
                case "MARR":
                    return action.Replace("MARR", "Mariage");
                case "MARS":
                    return action.Replace("MARS", "Contrat de mariage");
                case "MEMBER":
                    return action.Replace("MEMBER", "Membre");
                case "MILI":
                    return action.Replace("MILI", "Militaire");
                case "MOTH":
                    return action.Replace("MOTH", "Mère");
                case "NATI":
                    return action.Replace("NATI", "Nationalité ") + ligne;
                case "NATU":
                    return action.Replace("NATU", "Naturalization");
                case "NCHI":
                    return action.Replace("NCHI", "Nombre d'enfant ") + ligne;
                case "NMR":
                    return action.Replace("NMR", "Nombre de mariage ") + ligne;
                case "OCCU":
                    return action.Replace("OCCU", "Profession ") + ligne;
                case "OFFI":
                    return action.Replace("OFFI", "Officiel");
                case "ORDN":
                    return action.Replace("ORDN", "Ordination");
                case "PARE":
                    return action.Replace("PARE", "PARE");
                case "PHUS":
                    return action.Replace("PHUS", "Conjoint précédent");
                case "PROB":
                    return action.Replace("PROB", "Homologation d'un testament");
                case "PROP":
                    return action.Replace("PROP", "Propriété ") + ligne;
                case "PWIF":
                    return action.Replace("PWIF", "Conjointe précédeant");
                case "RECO":
                    return action.Replace("RECO", "Enregistreur");
                case "REL":
                    return action.Replace("REL", "Relatif");
                case "RELI":
                    return action.Replace("RELI", "Religion ") + ligne;
                case "RESI":
                    return action.Replace("RESI", "Résidence");
                case "RETI":
                    return action.Replace("RETI", "Retraite");
                case "ROLE":
                    return action.Replace("ROLE", "Rôle");
                case "SELR":
                    return action.Replace("SELR", "Vendeur");
                case "SLGC":
                    return action.Replace("SLGC", "Scellement d'un enfant à ses parents SDJ.");
                case "SPOU":
                    return action.Replace("SPOU", "Conjointe");
                case "SSN":
                    return action.Replace("SSN", "Numéro sécurité sociale ") + ligne;
                case "TITL":
                    return action.Replace("TITL", "Titre ") + ligne;
                case "TXPY":
                    return action.Replace("TXPY", "Taxe payer ") + ligne;
                case "WAC":
                    return action.Replace("WAC", "Évènement d'ordonnance SDJ.");
                case "WFAT":
                    return action.Replace("WFAT", "WFTA");
                case "WIFE":
                    return action.Replace("WIFE", "Conjointe");
                case "WILL":
                    return action.Replace("WILL", "Testament");
                case "WITN":
                    return action.Replace("WITN", "Témoin");
                case "WMOT":
                    return action.Replace("WMOT", "Père de la conjointe");
                case "_ELEC":
                    return action.Replace("_ELEC", "Élection");
                case "_MDCL":
                    return action.Replace("_MDCL", "Information médicale");
                case "_MILT":
                    return action.Replace("_MILT", "Service militaire"); // GRAMPS
            }
            //R..Z("retourne action null");
            return action;
        }
        private static string Convertir_STAT(string stat)
        {
            stat = stat.ToUpper();
            switch (stat)
            {
                case "BIC":
                    return "Cette personne est née dans l'alliance, ce qui signifie qu'elle reçoit automatiquement la bénédiction du scellement «enfant à parent».";
                case "CANCELED":
                    return "Annuler et cosidérer invalide";
                case "CHILD":
                    return "Décédé avant l'âge de huit ans, le baptême n'est pas requis.";//BAPL CONL ENDL
                case "COMPLETED":
                    return "Terminer mais la date n'est pas connue.";//BAPL CONL ENDL
                case "DNS":
                    return "Cette ordonnance n'est pas autorisée ou n'est pas soumis au temple.";
                case "DNS/CAN":
                    return "Cette ordonnance n'est pas autorisée, le scellement précédent a été annulé.";
                case "DONE":
                    return "Cette ordonnance est terminée mais la date n'est pas connue.";
                case "EXCLUDED":
                    return "Le patron a exclu que cette ordonnance soit effacée dans cette soumission.";//BAPL CONL ENDL SLGC
                case "INFANT":
                    return "Décédé avant moins d'un an, baptême ou dotation non requis."; // ENDL
                case "PRE-1970":
                    return "L'ordonnance est probablement terminée, une autre ordonnance pour cette personne a été convertie à partir des registres du temple des travaux achevés avant 1970, donc cette ordonnance est supposé complet jusqu'à la conversion de tous les enregistrements."; //BAPL CONL ENDL SLGC
                case "STILLBORN":
                    return "Mort-né, baptême non requis.";//BAPL CONL ENDL SLGC
                case "SUBMITTED":
                    return "Les données pour la demande d'ordonnance étaient insuffisantes."; // ENDL SLGC
                case "UNCLEARED":
                    return "Data for clearing ordinance request was insufficient.";//BAPL CONL ENDLSLGC

            }
            return stat;
        }

        private static string CopierObjet(
            string fichier,
            string sous_dossier,
            string dossier_sortie
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + "<br />fichier=[" + fichier + "] <br />sous_dossier=[" + sous_dossier + "]<br />dossier_sortie=[" + dossier_sortie + "]");
            {
                Regler_code_erreur();
                if (fichier == null)
                {
                    return null;
                }
                string fichierSource = Path.GetFileName(fichier);
                string fichierDestination = fichierSource.Replace(" ", "_");
                string source = null;
                string RepertoireDestination = dossier_sortie + @"\" + sous_dossier + @"/medias/";
                if (sous_dossier == "commun")
                    RepertoireDestination = dossier_sortie + @"/commun/";
                if (sous_dossier == "")
                    RepertoireDestination = dossier_sortie + @"/medias/";
                if (!Directory.Exists(RepertoireDestination))
                {
                    R.Afficher_message("Le dossier de destination\r\n\r\n«" + RepertoireDestination + "»\r\n\r\nn'existe pas, corrigez dans vos paramètres.", "", GHClass.erreur);
                    return null;
                }
                if (File.Exists(fichier) && R.IsNotNullOrEmpty(fichier))
                {
                    source = fichier;
                }
                else
                {
                    if (GHClass.Para.dossier_media != "")
                    {
                        source = GHClass.Para.dossier_media + "\\" + fichierSource;
                    }
                }
                if (!File.Exists(RepertoireDestination + fichierDestination))
                {
                    if (File.Exists(source) && fichierSource != null)
                    {
                        File.Copy(source, RepertoireDestination + fichierDestination, true);
                        return fichierDestination;
                    }
                }
                else
                {
                    return fichierDestination;
                }
            }
            return null;
        }
        private static string Avoir_texte_citation(
            string sous_dossier,
            string dossier_sortie,
            List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            //string a = "liste_citation_ID_numero ";foreach (ID_numero b in liste_citation_ID_numero) a += "<br>&nbsp;&nbsp;&nbsp;&nbsp;" + b.numero + " " + b.ID; R..Z(a);
            Application.DoEvents();
            string texte = null;
            string espace = Tabulation(tab);
            /*if (GH.GHClass.DEBUG)
            {
                texte += espace + "<div class=\"tableau\" style=\"background-color:#f55;font-Size:150%;margin-top:3px\">\n";
                texte += espace + "\tNode dépanage, montre un maximun de 3 citations.\n";
                texte += espace + "</div>\n";
            }
            */
            foreach (ID_numero item_citation in liste_citation_ID_numero)
            {
                /*if (GH.GHClass.DEBUG)
                {
                    if (item_citation.numero > 3) return texte;
                }
                */
                Regler_code_erreur();
                // avoir l'info de la citation
                GEDCOMClass.SOURCE_CITATION info_citation = GEDCOMClass.Avoir_info_citation(item_citation.ID);
                // pointer groupe citation
                texte += espace + "<a id=\"citation-" + item_citation.numero.ToString() + "\"></a>\n";
                if (info_citation == null)
                    return null;
                //{
                // Tableau
                texte += Separation(5, null, "000", null, tab);
                texte += espace + "<div class=\"tableau\">\n";
                // titre du tableau
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_tete\">\n";
                texte += espace + "\t\t\t<span class=\"tableau_citation\">" + item_citation.numero.ToString() + "</span>\n";
                if (GH.GHClass.Para.voir_ID)
                {
                    texte += espace + "\t\t\t<span class=\"tableau_ID\">[" + item_citation.ID + "]</span>\n";
                }
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
                // N0_text
                if (R.IsNotNullOrEmpty(info_citation.N0_texte))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += Convertir_texte_html(info_citation.N0_texte, false, tab + 3);
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // N0_Titre
                if (R.IsNotNullOrEmpty(info_citation.N0_Titre))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += Convertir_texte_html(info_citation.N0_Titre, false, tab + 3);
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // Titre v5.3
                if (R.IsNotNullOrEmpty(info_citation.N1_TITL_liste))
                {
                    foreach (string info_TITL in info_citation.N1_TITL_liste)
                    {
                        string ID = GEDCOMClass.Extraire_ID(info_TITL);
                        if (R.IsNotNullOrEmpty(ID))
                        {
                            texte += espace + "\t<div class=\"tableau_ligne\">\n";
                            texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                            texte += espace + "\t\t\tTitre" + "\n";
                            ;
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                            texte += Avoir_texte_lien_source(ID, liste_source_ID_numero, 0, tab + 3);
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t</div>\n";
                        }
                        else
                        {
                            texte += espace + "\t<div class=\"tableau_ligne\">\n";
                            texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                            texte += espace + "\t\t\tTitre" + "\n";
                            ;
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                            texte += espace + "\t\t\t" + info_TITL + "\n";
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t</div>\n";
                        }
                    }
                }
                // page
                if (R.IsNotNullOrEmpty(info_citation.N1_PAGE))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tPage" + "\n";
                    ;
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += Convertir_texte_html(info_citation.N1_PAGE, false, tab + 3);
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // classement
                if (R.IsNotNullOrEmpty(info_citation.N1_CLAS))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tClassement\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t\t" + Convertir_mot_anglais(info_citation.N1_CLAS) + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // PERIode
                if (R.IsNotNullOrEmpty(info_citation.N1_PERI))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tPériode\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    string date;
                    (date, _) = Convertir_date(info_citation.N1_PERI, GH.GHClass.Para.date_longue);
                    texte += espace + "\t\t\t" + date + "\n";
                    ;
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // DATE
                if (R.IsNotNullOrEmpty(info_citation.N1_DATE))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tDate d'entrée\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    (string date, _) = Convertir_date(info_citation.N1_DATE, GH.GHClass.Para.date_longue);
                    texte += espace + "\t\t\t" + date + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // recencement
                if (R.IsNotNullOrEmpty(info_citation.N1_CENS_liste))
                {
                    // Recencement DATE
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tRecencement\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    string texte_CENS;
                    (
                        texte_CENS,
                        liste_citation_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero
                    ) = Avoir_texte_recencement(
                        info_citation.N1_CENS_liste,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        tab + 3);
                    texte += texte_CENS;
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // ORIG
                if (R.IsNotNullOrEmpty(info_citation.N1_ORIG_liste))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tAuteur\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    string texte_ORIG;
                    (texte_ORIG,
                        liste_citation_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero) =
                        Avoir_texte_auteur(
                            info_citation.N1_ORIG_liste,
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero,
                            tab + 3);
                    texte += texte_ORIG;
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // Publication
                if (info_citation.N1_PUBL_record != null)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tÉdition\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += Avoir_texte_publication(info_citation.N1_PUBL_record, sous_dossier, tab + 3);
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                //Immigration
                if (info_citation.N1_IMMI_record != null)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tImmigration\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    string texte_IMMI = null;
                    (
                        texte_IMMI,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero
                    ) = Avoir_texte_immigration(
                        info_citation.N1_IMMI_record,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        tab + 3);
                    texte += texte_IMMI;
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // texte
                if (R.IsNotNullOrEmpty(info_citation.N1_TEXT_liste))
                {
                    bool premier = true;
                    foreach (GEDCOMClass.TEXT_STRUCTURE info_texte in info_citation.N1_TEXT_liste)
                    {
                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                        if (premier)
                        {
                            texte += espace + "\t\t\tTexte";
                            premier = false;
                        }
                        else
                        {
                            texte += espace + "\t\t\t&nbsp;";
                        }
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                        texte += espace + Convertir_texte_html(info_texte.N0_TEXT, false, tab + 2);
                        texte +=
                            Avoir_texte_NOTE_STRUCTURE(
                            info_texte.N1_NOTE_STRUCTURE_liste_ID,
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero,
                            0, // retrait
                            4);
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t</div>\n";
                    }
                }
                // Evenement
                if (R.IsNotNullOrEmpty(info_citation.N1_EVEN))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\t\tÉvènement\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    string action = Convertir_EVEN_en_texte(info_citation.N1_EVEN);
                    texte += Convertir_texte_html(action, false, tab + 4);
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                    if (R.IsNotNullOrEmpty(info_citation.N2_EVEN_ROLE))
                    {
                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte += espace + "\t\t<div class=\"tableau_col1d\">\n";
                        texte += espace + "\t\t\tRôle:\n";
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                        texte += Convertir_texte_html(Convertir_ROLE_TAG(info_citation.N2_EVEN_ROLE), false, tab + 3);
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t</div>\n";
                    }
                }
                // DATA
                if (R.IsNotNullOrEmpty(info_citation.N2_DATA_DATE))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tData ";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    (string date, _) = Convertir_date(info_citation.N2_DATA_DATE, GH.GHClass.Para.date_longue);
                    texte += date;
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                    if (info_citation.N2_DATA_TEXT_liste != null)
                    {
                        foreach (GEDCOMClass.TEXT_STRUCTURE info_TEXT in info_citation.N2_DATA_TEXT_liste)
                        {
                            texte += espace + "\t<div class=\"tableau_ligne\">\n";
                            texte += espace + "\t\t<div class=\"tableau_col1d\">\n";
                            texte += espace + "\t\t\tTexte: \n";
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                            texte += espace + "\t\t\t" + Convertir_texte_html(info_TEXT.N0_TEXT, false, 0) + "\n";
                            if (info_TEXT.N1_NOTE_STRUCTURE_liste_ID != null)
                            {
                                texte += Avoir_texte_NOTE_STRUCTURE(
                                    info_TEXT.N1_NOTE_STRUCTURE_liste_ID,
                                    liste_citation_ID_numero,
                                    liste_source_ID_numero,
                                    liste_note_STRUCTURE_ID_numero,
                                    liste_NOTE_RECORD_ID_numero,
                                    0, // retrait
                                    0);
                            }
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t</div>\n";
                        }
                    }
                }
                // QUAY
                if (R.IsNotNullOrEmpty(info_citation.N1_QUAY))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tCrédibilité\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t" + Traduire_QUAY(info_citation.N1_QUAY) + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // _QUAL Heridis
                // _QUAL__SOUR Heridis
                if (R.IsNotNullOrEmpty(info_citation.N2__QUAL__SOUR))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\t\tQualité de la source (Heridis)\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t" + info_citation.N2__QUAL__SOUR + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // _QUAL__INFO Heridis
                if (R.IsNotNullOrEmpty(info_citation.N2__QUAL__INFO))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tQualité de l'information (Heridis)\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t" + info_citation.N2__QUAL__INFO + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // _QUAL__EVID Heridis
                if (R.IsNotNullOrEmpty(info_citation.N2__QUAL__EVID))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tQualité de la preuve (Heridis)\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t\t" + info_citation.N2__QUAL__EVID + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // STAT
                if (R.IsNotNullOrEmpty(info_citation.N1_STAT))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tÉtat de la recherche\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t\t" + Convertir_mot_anglais(info_citation.N1_STAT) + "\n";
                    if (R.IsNotNullOrEmpty(info_citation.N2_STAT_DATE))
                    {
                        (string date, _) = Convertir_date(info_citation.N2_STAT_DATE, GH.GHClass.Para.date_longue);
                        texte += espace + "\t\t\ten date du " + date + "\n";
                    }
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // FIDE code de fidélité
                if (R.IsNotNullOrEmpty(info_citation.N1_FIDE))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tCode de fidélité\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t" + Traduire_SOURCE_FIDELITY_CODE(info_citation.N1_FIDE) + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                string temp_string = null;
                (
                    temp_string,
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero
                    ) =
                Avoir_texte_MULTIMEDIA(
                    info_citation.MULTIMEDIA_LINK_liste_ID,
                    sous_dossier,
                    dossier_sortie,
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    true, // +marge
                    tab + 1
                    );
                texte += temp_string;

                // REFS de la source avec EVEN
                if (R.IsNotNullOrEmpty(info_citation.N1_REFS_liste_EVEN))
                {
                    foreach (string s in info_citation.N1_REFS_liste_EVEN)
                    {
                        string phrase = "Source non examinée";
                        if (info_citation.N1_REFS_liste_EVEN.Count > 1)
                            phrase = "Sources non examinées";
                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                        texte += espace + "\t\t\t" + phrase + "<sup>1</sup>";
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                        texte += s + " ";
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t</div>\n";
                    }
                }
                // REFS référence
                if (R.IsNotNullOrEmpty(info_citation.N1_REFS_liste_ID))
                {
                    string phrase = "Source non examinée";
                    if (info_citation.N1_REFS_liste_ID.Count > 1)
                        phrase = "Sources non examinées";
                    int nombre_source = liste_source_ID_numero.Count;
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\t" + phrase + "<sup>1</sup>\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += Avoir_texte_lien_source(info_citation.N1_REFS_liste_ID, liste_source_ID_numero, 0, tab + 3);
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // source de la source avec EVEN
                if (R.IsNotNullOrEmpty(info_citation.N1_SOUR_liste_EVEN))
                {
                    if (R.IsNotNullOrEmpty(info_citation.N1_SOUR_liste_EVEN))
                    {
                        foreach (string s in info_citation.N1_SOUR_liste_EVEN)
                        {
                            texte += espace + "\t<div class=\"tableau_ligne\">\n";
                            texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                            texte += espace + "\t\t\tSource ";
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                            texte += s + " ";
                            texte += espace + "\t\t</div>\n";
                            texte += espace + "\t</div>\n";
                        }
                    }
                }
                // source de la source avec ID
                texte += Avoir_texte_lien_source(info_citation.N0_ID_SOUR, liste_source_ID_numero, 0.04f, tab + 1);
                // source de la source avec ID multiple
                texte += Avoir_texte_lien_source(info_citation.N1_SOUR_liste_ID, liste_source_ID_numero, 0.04f, tab + 3);
                // Dépo 
                if (R.IsNotNullOrEmpty(info_citation.N1_REPO_liste))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tDépôt";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    foreach (GEDCOMClass.SOURCE_REPOSITORY_CITATION info_repo in info_citation.N1_REPO_liste)
                    {
                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                        string texte_repo = Avoir_texte_REPO_citation(
                            info_repo,
                            sous_dossier,
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_repo_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero,
                            tab + 3);
                        texte += texte_repo;

                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t</div>\n";
                    }
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // note
                texte +=
                Avoir_texte_NOTE_STRUCTURE(
                    info_citation.N1_NOTE_STRUCTURE_liste_ID,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    .04f, // retrait
                    tab + 3);
                if (R.IsNotNullOrEmpty(info_citation.N1_REFS_liste_ID))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\" style=\"font-Size: 11px;\">\n";
                    texte += espace + "\t\t\t<hr style=\"width:150px;padding-left:5px;margin: 0px;\" />\n";
                    texte += espace + "\t\t\t1 Une source qui a été référencée par la source citée " +
                        "mais qui n'a pas été examinée par l'auteur de la communication.\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // fin tableau
                texte += espace + "</div>\n";
                //}
            }
            GC.Collect();
            return (texte);
        }

        private static string Avoir_texte_date_PP(
            string even,
            string date
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + "<br>even= " + even + "<br>date= " + date);
            bool even_date;
            int even_ans;
            // tout événemtent
            if (GH.GHClass.Para.tout_evenement)
            {
                even_date = GH.GHClass.Para.tout_evenement_date;
                even_ans = GH.GHClass.Para.tout_evenement_ans;
            }

            // si naissance
            else if (Si_naissance(even))
            {
                even_date = GH.GHClass.Para.naissance_date;
                even_ans = GH.GHClass.Para.naissance_ans;
            }

            // si décéder
            else if (Si_deceder(even))
            {
                even_date = GH.GHClass.Para.deces_date;
                even_ans = GH.GHClass.Para.deces_ans;
            }

            // si union
            else if (Si_union(even))
            {
                even_date = GH.GHClass.Para.union_date;
                even_ans = GH.GHClass.Para.union_ans;
            }

            // si baptème
            else if (Si_bapteme(even))
            {
                even_date = GH.GHClass.Para.bapteme_date;
                even_ans = GH.GHClass.Para.bapteme_ans;
            }

            // si inhumer
            else if (Si_inhumer(even))
            {
                even_date = GH.GHClass.Para.inhumation_date;
                even_ans = GH.GHClass.Para.inhumation_ans;
            }

            // si citoyen
            else if (Si_citoyen(even))
            {
                even_date = GH.GHClass.Para.citoyen_date;
                even_ans = GH.GHClass.Para.citoyen_ans;
            }

            // si ordonnance
            else if (Si_ordonnance(even))
            {
                even_date = GH.GHClass.Para.ordonnance_date;
                even_ans = GH.GHClass.Para.ordonnance_ans;
            }

            // si relier à testament affiche toujours date 
            else if (even == "WILL" || even == "PROB")
            {
                even_date = true;
                even_ans = 0;
            }

            // si autre
            else
            {
                even_date = GH.GHClass.Para.autre_date;
                even_ans = GH.GHClass.Para.autre_ans;
            }

            // année courante
            DateTime maintenant = DateTime.Now;
            int annee_courante = maintenant.Year;
            int an;
            (date, an) = Convertir_date(date, GH.GHClass.Para.date_longue);
            if (even_date && annee_courante >= even_ans + an)
            {
                // si premier charactère est une lettre, mettre en majuscule.
                date = ToUpperFirst(date);
                return date;
            }
            GC.Collect();
            return "<span class=\"priver\"></span>";
        }
        private static string Avoir_texte_date_Changement(
        GEDCOMClass.CHANGE_DATE info_date,
        List<ID_numero> liste_citation_ID_numero,
        List<ID_numero> liste_source_ID_numero,
        List<ID_numero> liste_note_STRUCTURE_ID_numero,
        List<ID_numero> liste_NOTE_RECORD_ID_numero,
        bool multi_ligne,
        int tab
        //, [CallerLineNumber] int callerLineNumber = 0
        )
        {
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            if (GH.GHClass.Para.voir_date_changement == false || info_date == null)
                return null;
            string texte = null;
            string espace = Tabulation(tab);
            texte += espace + "<span class=\"dateChangement\">\n";
            texte += espace + "\tDernier changement,\n";
            if (multi_ligne)
                texte += espace + "\t<br/>&emsp;\n";
            (string date, _) = Convertir_date(info_date.N1_CHAN_DATE, GH.GHClass.Para.date_longue);
            texte += espace + "\t" + date + " à " + info_date.N2_CHAN_DATE_TIME + "\n";
            texte += espace + "</span><br />\n";
            if (R.IsNotNullOrEmpty(info_date.N1_CHAN_NOTE_STRUCTURE_ID_liste))
            {
                texte += espace + "<div class=\"tableau_ligne\">\n";
                texte += espace + "\t<div style=\"width:.25in\">\n";
                texte += espace + "\t\t&nbsp;\n";
                texte += espace + "\t</div>\n";
                texte += espace + "\t<div class=\"tableau_colW\">\n";
                texte += Avoir_texte_NOTE_STRUCTURE(
                    info_date.N1_CHAN_NOTE_STRUCTURE_ID_liste,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    0, // retrait
                    tab + 2);
                texte += espace + "\t</div>\n";
                texte += espace + "</div>\n";
            }
            GC.Collect();
            return texte;
        }
        /*
                private static string Avoir_texte_date_priver(
                    string date_evenement,
                    bool para_evenement_date,
                    int para_evenement_an,
                    string even = null)
                {
                    if (GH.GHClass.Para.tout_evenement)
                    {
                        para_evenement_date = GH.GHClass.Para.tout_evenement_date;
                        para_evenement_an = GH.GHClass.Para.tout_evenement_ans;
                    }
                    if (R.IsNullOrEmpty(date_evenement))
                        return "<span class=\"inconnu\"></span>" + "\n";
                    // année courante
                    DateTime maintenant = DateTime.Now;
                    int annee_courante = maintenant.Year;
                    string date;
                    int an;
                    (date, an) = Convertir_date(date_evenement, GH.GHClass.Para.date_longue);
                    if (para_evenement_date && annee_courante >= para_evenement_an + an)
                        return date;
                    GC.Collect();
                    return "<span class=\"priver\"></span>" + "\n";
                }
                */

        private (string, bool) Groupe_depot(
            string sous_dossier,
            string dossier_sortie,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab = 0)
        {
            string origine = @"../";
            if (sous_dossier == "")
                origine = "";
            if (R.IsNullOrEmpty(liste_repo_ID_numero))
                return (null, false);
            string espace = Tabulation(tab);
            string texte = null;
            int nombre_repo = 0;
            string bloc_1 = null;
            if (R.IsNotNullOrEmpty(liste_repo_ID_numero))
            {
                bloc_1 += Avoir_texte_REPO_record(
                    sous_dossier,
                    dossier_sortie,
                    nombre_repo,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    tab);
            }
            if (bloc_1.Length > 0)
            {
                texte += espace + "<a id=\"groupe_depot\"></a>\n";
                texte += espace + "<!--**************************\n";
                texte += espace + "*  groupe Dépôt              *\n";
                texte += espace + "***************************-->\n";
                // titre du groupe
                string titre = "Dépôt";
                if (R.IsNotNullOrEmpty(liste_repo_ID_numero))
                    titre += "s";
                texte += Titre_groupe(origine + @"commun/depot.svg", titre, null, tab);
                texte += bloc_1;
                GC.Collect();
                return (texte, true);
            }
            GC.Collect();
            return (null, true);
        }

        private (string, int, int, bool) Groupe_evenement(
            List<GEDCOMClass.EVEN_ATTRIBUTE_STRUCTURE> liste,
            string date_naissance_individu,
            string date_deces_individu,
            string date_naissace_conjoint,
            string date_naissace_conjointe,
            string sous_dossier,
            string dossier_sortie,
            bool menu,
            int numero_carte,
            int numero_source,
            List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab)
        {
            if (R.IsNullOrEmpty(liste))
                return ("",
                    numero_carte,
                    numero_source,
                    false);
            // classé événement
            Liste_par_date par_date = new Liste_par_date();
            liste.Sort(par_date);

            string origine = @"../";
            if (sous_dossier == "")
                origine = "";
            string espace = Tabulation(tab);
            string texte = null;
            texte += espace + "<a id=\"groupe_evenement\"></a>\n";
            texte += espace + "<!--**************************\n";
            texte += espace + "*  groupe Évènement          *\n";
            texte += espace + "***************************-->\n";
            string titre = "Évènement";
            if (R.IsNotNullOrEmpty(liste))
                titre += "s";
            texte += Titre_groupe(origine + @"commun/agenda.svg", titre, null, tab);
            // index des événements
            if (liste.Count > 1)
            {
                texte += Separation(5, null, "000", null, tab);
                texte += espace + "<div class=\"tableau evenement-index \" >\n";
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_tete\">\n";
                texte += espace + "\t\t\t<strong style=\"font-size:150%\">\n";
                texte += espace + "\t\t\t\tIndex\n";
                texte += espace + "\t\t\t</strong>\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
                texte += espace + "\t<table class=\"index_evenement\">\n";
                foreach (GEDCOMClass.EVEN_ATTRIBUTE_STRUCTURE info in liste)
                {

                    if (Si_testament_ou_autre(info.N1_EVEN, info.N2_DATE, date_naissance_individu, date_deces_individu))
                    {

                        string lien_evenement = Avoir_nom_lien_evenement(info.N1_EVEN, info.N2_DATE);

                        // debut tableau
                        texte += espace + "\t\t<tr>\n";
                        texte += espace + "\t\t\t<td>\n";

                        // bouton lien
                        texte += espace + "\t\t\t\t<button>\n";
                        texte += espace + "\t\t\t\t\t<a href=\"#" + lien_evenement + "\">\n";
                        texte += espace + "\t\t\t\t\t\t<img style=\"height:25px\" src=\"../commun/go_evenement.svg\" alt=\"\" />\n";
                        texte += espace + "\t\t\t\t\t</a>\n";
                        texte += espace + "\t\t\t\t</button>\n";
                        texte += espace + "\t\t\t</td>\n";
                        texte += espace + "\t\t\t<td>\n";

                        // Date

                        string texte_date = Avoir_texte_date_PP(info.N1_EVEN, info.N2_DATE);
                        texte += espace + "\t\t\t\t\t" + texte_date + "\n";
                        texte += espace + "\t\t\t</td>\n";
                        texte += espace + "\t\t\t<td>\n";
                        texte += espace + "\t\t\t\t" + Convertir_EVEN_en_texte(info.N1_EVEN) + "\n";

                        // fermer tableau
                        texte += espace + "\t\t\t</td>\n";
                        texte += espace + "\t\t</tr>\n";
                    }
                }
                texte += espace + "\t</table>\n";
                texte += espace + "</div>";
            }
            // FIN index des événements
            foreach (GEDCOMClass.EVEN_ATTRIBUTE_STRUCTURE info in liste)
            {
                if (Si_testament_ou_autre(info.N1_EVEN, info.N2_DATE, date_naissance_individu, date_deces_individu))
                {
                    string temp = null;
                    string lien_evenement = Avoir_nom_lien_evenement(info.N1_EVEN, info.N2_DATE);
                    texte += espace + "<a id=\"" + lien_evenement + "\"></a>\n";
                    (temp, numero_carte, numero_source) = Avoir_texte_evenement(
                            info,
                            date_naissance_individu,
                            date_deces_individu,
                            date_naissace_conjoint,
                            date_naissace_conjointe,
                            sous_dossier,
                            dossier_sortie,
                            menu,
                            numero_carte,
                            numero_source,
                            liste_MULTIMEDIA_ID_numero,
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_repo_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero,
                            tab + 1);
                    texte += temp;
                }
            }
            GC.Collect();
            return (
                texte,
                numero_carte,
                numero_source,
                true);
        }
        private (string, int, int, bool) Groupe_ordonnance(
            List<GEDCOMClass.LDS_INDIVIDUAL_ORDINANCE> liste,
            string dateNaissance,
            int numero_carte,
            int numero_source,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            Regler_code_erreur();
            if (R.IsNullOrEmpty(liste))
                return (null, numero_carte, numero_source, false);
            if (!GH.GHClass.Para.tout_evenement)
            {
                //if (!GH.GHClass.Para.religion) return (null, numero_carte, numero_source, false);
            }
            string origine = @"../";
            string texte = null;
            string espace = Tabulation(tab);
            texte += espace + "<a id=\"groupe_ordonnance\"></a>\n";
            texte += espace + "<!--**************************\n";
            texte += espace + "*  groupe Ordonnance         *\n";
            texte += espace + "***************************-->\n";
            string titre = "Ordonnance";
            if (liste.Count > 1)
                titre += "s";
            // titre du groupe
            texte += Titre_groupe(origine + @"commun/LDS.png", titre, null, tab);
            Ordonnance_liste_par_date par_date = new Ordonnance_liste_par_date();
            liste.Sort(par_date);
            // index des événements
            if (liste.Count > 1)
            {
                texte += Separation(5, null, "000", null, tab);
                texte += espace + "<div class=\"tableau evenement-index \" >\n";
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_tete\">\n";
                texte += espace + "\t\t\t<strong style=\"font-size:150%\">\n";
                texte += espace + "\t\t\t\tIndex\n";
                texte += espace + "\t\t\t</strong>\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
                texte += espace + "\t<table class=\"index_evenement\">\n";
                foreach (GEDCOMClass.LDS_INDIVIDUAL_ORDINANCE info_LDS in liste)
                {
                    string lien_evenement = Avoir_nom_lien_evenement(info_LDS.N0_EVEN, info_LDS.N1_DATE);
                    texte += espace + "\t\t<tr>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t<button>\n";
                    texte += espace + "\t\t\t\t\t<a href=\"#" + lien_evenement + "\">\n";
                    texte += espace + "\t\t\t\t\t\t<img style=\"height:25px\" src=\"../commun/go_evenement.svg\" alt=\"\" />\n";
                    texte += espace + "\t\t\t\t\t</a>\n";
                    texte += espace + "\t\t\t\t</button>\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    string date = Avoir_texte_date_PP(info_LDS.N0_EVEN, info_LDS.N1_DATE);
                    texte += espace + "\t\t\t\t\t" + date + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + Convertir_EVEN_en_texte(info_LDS.N0_EVEN) + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t</tr>\n";
                }
                texte += espace + "\t</table>\n";
                texte += espace + "</div>";
            }
            // FIN index des événements

            foreach (GEDCOMClass.LDS_INDIVIDUAL_ORDINANCE info_LDS in liste)
            {
                Application.DoEvents();
                string lien_evenement = Avoir_nom_lien_evenement(info_LDS.N0_EVEN, info_LDS.N1_DATE);
                texte += espace + "<a id=\"" + lien_evenement + "\"></a>\n";
                texte += Separation(5, null, "000", null, tab);
                // Tableau
                texte += espace + "<div class=\"tableau\">\n";
                if (R.IsNotNullOrEmpty(info_LDS.N0_EVEN))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_tete\">\n";
                    string texte_date = Avoir_texte_date_PP(info_LDS.N0_EVEN, info_LDS.N1_DATE);
                    string even = info_LDS.N0_EVEN;
                    switch (info_LDS.N0_EVEN)
                    {
                        case "BAPL":
                            even = "Baptême";
                            break;
                        case "CONL":
                            even = "Confirmation";
                            break;
                        case "ENDL":
                            even = "Dotation";
                            break;
                        case "SLGC":
                            even = "Scellement";
                            break;
                        case "WAC":
                            even = "Évènement";
                            break;
                    }
                    texte += "\t\t\t<strong style=\"font-size:150%\">";
                    if (texte_date != null)
                        texte += espace + "\t\t\t\t" + texte_date + "\n";
                    texte += espace + "\t\t\t\t" + even + "\n";
                    texte += espace + "\t\t\t</strong>";
                    string description = "";
                    switch (info_LDS.N0_EVEN)
                    {
                        case "BAPL":
                            description = " célébré à l'âge de huit ans ou plus par l'Église SDJ";
                            break;
                        case "CONL":
                            description = " SDJ";
                            break;
                        case "ENDL":
                            description = " a été accomplie par la prêtrise autorité dans un temple SDJ.";
                            break;
                        case "SLGC":
                            description = " d'un enfant à ses parents lors d'une cérémonie au temple SDJ.";
                            break;
                        case "WAC":
                            description = " Ordonnance du Temple LDS.";
                            break;
                    }
                    string age = Calculer_age(dateNaissance, info_LDS.N1_DATE);
                    texte += espace + "\t\t\t\t" + description + "\n";
                    if (R.IsNotNullOrEmpty(age))
                        texte += espace + "\t\t\t à l'âge de " + age + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                if (R.IsNotNullOrEmpty(info_LDS.N1_TYPE))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tType\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t" + Convertir_STAT(info_LDS.N1_TYPE) + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                if (R.IsNotNullOrEmpty(info_LDS.N1_TEMP))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tTemple\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t" + info_LDS.N1_TEMP + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                if (R.IsNotNullOrEmpty(info_LDS.N1_PLAC))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tLieu\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t" + info_LDS.N1_PLAC + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                if (R.IsNotNullOrEmpty(info_LDS.N1_STAT))
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tStatus\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t" + info_LDS.N1_STAT + "\n";
                    if (R.IsNotNullOrEmpty(info_LDS.N2_STAT_DATE))
                    {
                        (string date2, _) = Convertir_date(info_LDS.N2_STAT_DATE, GH.GHClass.Para.date_longue);
                        texte += espace + "\t\t\t En date du " + date2 + "\n";
                    }
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // citation 
                texte += Avoir_texte_lien_citation(
                        info_LDS.N1_SOUR_citation_liste_ID,
                        liste_citation_ID_numero,
                        .04f, // retrait
                        tab + 1);
                // source 
                texte += Avoir_texte_lien_source(
                        info_LDS.N1_SOUR_source_liste_ID,
                        liste_source_ID_numero,
                        .04f, // retrait
                        tab + 1);
                // note ordonnance
                texte += Avoir_texte_NOTE_STRUCTURE(
                    info_LDS.N1_NOTE_STRUCTURE_liste_ID,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    .04f, // retrait
                    tab + 1);
                // fin tableau
                texte += espace + "</div>\n";
            }
            GC.Collect();
            return (
                texte,
                numero_carte,
                numero_source,
                true);
        }

        public static string Extraire_ID(string s)
        {
            int p1 = s.IndexOf("@") + 2;
            if (p1 > 9)
                return ""; // si ID n'est pas au début retour ""
            int p2 = s.IndexOf("@", s.IndexOf('@') + 1);
            if ((p1 >= 0) && (p2 >= 0))
            {
                return s.Substring(p1 - 1, p2 - p1 + 1);
            }
            else
            {
                return null;
            }
        }
        public string Fiche_famille(
            string ID_famille,
            string dossier_sortie,
            bool menu
            //            , [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            Regler_code_erreur();
            Application.DoEvents();
            bool modifier;
            if (R.IsNullOrEmpty(ID_famille))
                return null;
            string sous_dossier = "familles";
            int nombre_source = 0;
            string origine = @"../";
            List<ID_numero> liste_SUBMITTER_ID_numero = new List<ID_numero>();

            List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero = new List<MULTIMEDIA_ID_numero>();
            MULTIMEDIA_ID_numero a = new MULTIMEDIA_ID_numero();
            liste_MULTIMEDIA_ID_numero.Add(a);
            List<ID_numero> liste_note_STRUCTURE_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_NOTE_RECORD_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_citation_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_source_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_repo_ID_numero = new List<ID_numero>();
            GEDCOMClass.FAM_RECORD info_famille = GEDCOMClass.Avoir_info_famille(ID_famille);
            //Généré référence
            {
                // info famille
                if (info_famille == null)
                    return null;
                // citation souce famille
                if (GH.GHClass.Para.voir_reference)
                {
                    (liste_citation_ID_numero, _) = Verifier_liste(info_famille.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                    (liste_source_ID_numero, _) = Verifier_liste(info_famille.N1_SOUR_source_liste_ID, liste_source_ID_numero);
                }
                // note famille
                if (GH.GHClass.Para.voir_note)
                    (liste_note_STRUCTURE_ID_numero, _) = Verifier_liste(info_famille.N1_NOTE_STRUCTURE_liste_ID,
                        liste_note_STRUCTURE_ID_numero);
                // note de SLGS
                if (GH.GHClass.Para.voir_note)
                    (liste_note_STRUCTURE_ID_numero, _) = Verifier_liste(info_famille.N1_SLGS.N1_NOTE_STRUCTURE_liste_ID,
                        liste_note_STRUCTURE_ID_numero);
                // citation source SLGS
                if (GH.GHClass.Para.voir_reference)
                {
                    (liste_citation_ID_numero, _) = Verifier_liste(info_famille.N1_SLGS.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                    (liste_source_ID_numero, _) = Verifier_liste(info_famille.N1_SLGS.N1_SOUR_source_liste_ID, liste_source_ID_numero);
                }
                // chercheur
                if (GH.GHClass.Para.voir_chercheur)
                    (liste_SUBMITTER_ID_numero, _) = Verifier_liste(info_famille.N1_SUBM_liste_ID, liste_SUBMITTER_ID_numero);
                // Groupe média
                if (GH.GHClass.Para.voir_media)
                {
                    if (info_famille.MULTIMEDIA_LINK_liste_ID != null)
                    {
                        (liste_MULTIMEDIA_ID_numero, _) = Verifier_liste_MULTIMEDIA(info_famille.MULTIMEDIA_LINK_liste_ID, liste_MULTIMEDIA_ID_numero);
                    }
                }
                // groupe evenement
                if (R.IsNotNullOrEmpty(info_famille.N1_EVEN_Liste))
                {
                    foreach (GEDCOMClass.EVEN_ATTRIBUTE_STRUCTURE info_evenement in info_famille.N1_EVEN_Liste)
                    {
                        // citation evenement
                        if (GH.GHClass.Para.voir_reference)
                            (liste_citation_ID_numero, modifier) = Verifier_liste(info_evenement.N2_SOUR_citation_liste_ID, liste_citation_ID_numero);
                        // note PLAC
                        // if PLAC n'est pas null
                        if (info_evenement.N2_PLAC != null)
                        {
                            // note de PLAC
                            if (GH.GHClass.Para.voir_note)
                                (liste_note_STRUCTURE_ID_numero, modifier) =
                                    Verifier_liste(info_evenement.N2_PLAC.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                            // citation PLAC
                            if (GH.GHClass.Para.voir_reference)
                                (liste_citation_ID_numero, modifier) = Verifier_liste(info_evenement.N2_PLAC.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                        }
                        // note de l'adresse
                        // if adresse n'est pas null
                        if (R.IsNotNullOrEmpty(info_evenement.N2_ADDR_liste))
                            if (GH.GHClass.Para.voir_note)
                                foreach (GEDCOMClass.ADDRESS_STRUCTURE info_adresse in info_evenement.N2_ADDR_liste)
                                {
                                    if (R.IsNotNullOrEmpty(info_adresse.N1_NOTE_STRUCTURE_liste_ID))
                                    {
                                        (liste_note_STRUCTURE_ID_numero, modifier) =
                                            Verifier_liste(info_adresse.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                                    }
                                }
                        // note évènement
                        if (GH.GHClass.Para.voir_reference)
                            (liste_note_STRUCTURE_ID_numero, modifier) = Verifier_liste(info_evenement.N2_NOTE_STRUCTURE_liste_ID,
                                liste_note_STRUCTURE_ID_numero);
                        // note de TEXT
                        if (R.IsNotNullOrEmpty(info_evenement.N2_TEXT_liste))
                        {
                            foreach (GEDCOMClass.TEXT_STRUCTURE info_texte in info_evenement.N2_TEXT_liste)
                            {
                                if (R.IsNotNullOrEmpty(info_texte.N1_NOTE_STRUCTURE_liste_ID))
                                {
                                    liste_NOTE_RECORD_ID_numero =
                                        Valider_liste_reference_note(info_texte.N1_NOTE_STRUCTURE_liste_ID, liste_NOTE_RECORD_ID_numero);
                                }
                            }
                        }
                        // media
                        (liste_MULTIMEDIA_ID_numero, _) = Verifier_liste_MULTIMEDIA(info_evenement.MULTIMEDIA_LINK_liste_ID, liste_MULTIMEDIA_ID_numero);

                        // CHAN
                        (
                            liste_MULTIMEDIA_ID_numero,
                            liste_source_ID_numero,
                            liste_repo_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero,
                            _
                        ) =
                        Genere_liste_reference_date
                            (
                            info_evenement.N2_CHAN,
                            liste_MULTIMEDIA_ID_numero,
                            liste_source_ID_numero,
                            liste_repo_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero
                            );
                    }
                }
                // groupe attribut
                if (R.IsNotNullOrEmpty(info_famille.N1_ATTRIBUTE_liste))
                {
                    foreach (GEDCOMClass.EVEN_ATTRIBUTE_STRUCTURE info_attribut in info_famille.N1_ATTRIBUTE_liste)
                    {
                        // citation evenement
                        if (GH.GHClass.Para.voir_reference)
                            (liste_citation_ID_numero, modifier) = Verifier_liste(info_attribut.N2_SOUR_citation_liste_ID, liste_citation_ID_numero);
                        // note PLAC
                        // if PLAC n'est pas null
                        if (info_attribut.N2_PLAC != null)
                        {
                            // note de PLAC
                            if (GH.GHClass.Para.voir_note)
                                (liste_note_STRUCTURE_ID_numero, modifier) =
                                    Verifier_liste(info_attribut.N2_PLAC.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                            // citation PLAC
                            if (GH.GHClass.Para.voir_reference)
                                (liste_citation_ID_numero, modifier) = Verifier_liste(info_attribut.N2_PLAC.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                        }
                        // note de l'adresse
                        // if adresse n'est pas null
                        if (R.IsNotNullOrEmpty(info_attribut.N2_ADDR_liste))
                            if (GH.GHClass.Para.voir_note)
                                foreach (GEDCOMClass.ADDRESS_STRUCTURE info_adresse in info_attribut.N2_ADDR_liste)
                                {
                                    if (R.IsNotNullOrEmpty(info_adresse.N1_NOTE_STRUCTURE_liste_ID))
                                    {
                                        (liste_note_STRUCTURE_ID_numero, modifier) =
                                            Verifier_liste(info_adresse.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                                    }
                                }
                        // note évènement
                        if (GH.GHClass.Para.voir_reference)
                            (liste_note_STRUCTURE_ID_numero, modifier) =
                                Verifier_liste(info_attribut.N2_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                        // note de TEXT
                        if (R.IsNotNullOrEmpty(info_attribut.N2_TEXT_liste))
                        {
                            foreach (GEDCOMClass.TEXT_STRUCTURE info_texte in info_attribut.N2_TEXT_liste)
                            {
                                if (R.IsNotNullOrEmpty(info_texte.N1_NOTE_STRUCTURE_liste_ID))
                                {
                                    liste_NOTE_RECORD_ID_numero =
                                        Valider_liste_reference_note(info_texte.N1_NOTE_STRUCTURE_liste_ID, liste_NOTE_RECORD_ID_numero);
                                }
                            }
                        }
                        // media
                        (liste_MULTIMEDIA_ID_numero, _) = Verifier_liste_MULTIMEDIA(info_famille.MULTIMEDIA_LINK_liste_ID, liste_MULTIMEDIA_ID_numero);

                        // CHAN
                        (
                            liste_MULTIMEDIA_ID_numero,
                            liste_source_ID_numero,
                            liste_repo_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero,
                            _
                        ) =
                        Genere_liste_reference_date
                            (
                            info_attribut.N2_CHAN,
                            liste_MULTIMEDIA_ID_numero,
                            liste_source_ID_numero,
                            liste_repo_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero
                            );
                    }
                }
                // CHAN
                (
                    liste_MULTIMEDIA_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    _
                ) =
                Genere_liste_reference_date
                    (
                    info_famille.N1_CHAN,
                    liste_MULTIMEDIA_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero
                    );

                // générer tout
                (
                    liste_SUBMITTER_ID_numero,
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    _
                ) =
                Genere_liste_reference_tous
                (
                    liste_SUBMITTER_ID_numero,
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero
                );
            }
            string espace = Tabulation(3);
            bool groupe_archive;
            bool groupe_enfant = false;
            bool groupe_media = false;
            bool groupe_evenement;
            bool groupe_attribut;
            bool groupe_chercheur;
            bool groupe_citation = false;
            bool groupe_source = false;
            bool groupe_note = false;
            bool groupe_depot = false;
            int numero_carte = 0;
            int numero_source = 0;
            int tab = 3;
            // dossier
            string nomFichierFamille = @dossier_sortie + "/familles/" + ID_famille + ".html";
            if (!menu)
                nomFichierFamille = @dossier_sortie + "/familles/page.html";
            if (File.Exists(nomFichierFamille))
            {
                File.Delete(nomFichierFamille);
            }
            // mariage
            // conjoint
            bool Ok;
            (
                string nom_conjoint,
                string naissance_date_conjoint,
                _, // string naissance_lieu_conjoint,
                string deces_date_conjoint,
                _ // string deces_lieu_conjoint
            ) = GEDCOMClass.Avoir_nom_naissance_deces(info_famille.N1_HUSB);
            // conjointe
            (
                string nom_conjointe,
                string naissance_date_conjointe,
                _, // string naissance_lieu_conjointe,
                string deces_date_conjointe,
                _ // string deces_lieu_conjointe
            ) = GEDCOMClass.Avoir_nom_naissance_deces(info_famille.N1_WIFE);
            // @Parent
            string texte = null;
            string bloc = null;
            string IDConjointTexte = null;
            string IDConjointeTexte = null;
            if (GH.GHClass.Para.voir_ID == true)
            {
                if (info_famille.N1_HUSB != "")
                    IDConjointTexte = " [" + info_famille.N1_HUSB + "]";
                if (info_famille.N1_WIFE != "")
                    IDConjointeTexte = " [" + info_famille.N1_WIFE + "]";
            }

            string titre_page;
            if (nom_conjoint != null)
                titre_page = nom_conjoint + " et ";
            else
                titre_page = "Conjoint inconnu et ";
            if (nom_conjointe != null)
                titre_page += nom_conjointe;
            else
                titre_page += "conjointe inconnue";
            texte += Haut_Page("../", menu, titre_page);
            // restriction
            if (R.IsNotNullOrEmpty(info_famille.N1_RESN))
            {
                texte += "\t\t\t<div class=\"blink3\">\n";
                texte += "\t\t\t\tRestriction\n";
                texte += "\t\t\t\t" + Traduire_RESN(info_famille.N1_RESN) + "\n";
                texte += "\t\t\t</div>\n";
            }
            texte += espace + "<a id=\"groupe_famille\"></a>\n";
            texte += espace + "<!--**************************\n";
            texte += espace + "*  groupe Famille            *\n";
            texte += espace + "***************************-->\n";
            texte += Titre_groupe(origine + @"commun/familleConjoint.svg", "Famille", ID_famille, tab);
            // nom parents
            texte += Separation(5, null, "000", null, tab);
            texte += espace + "<table class=\"tableau\">\n";
            texte += espace + "\t<thead>\n";
            texte += espace + "\t\t<tr>\n";
            texte += espace + "\t\t\t<th class=\"cellule2LMB\" style=\"width:25px\">&nbsp;</th>\n";
            texte += espace + "\t\t\t<th class=\"cellule2LMB\">Nom</th>\n";
            texte += espace + "\t\t\t<th class=\"cellule2LMB date\">Naissance</th>\n";
            texte += espace + "\t\t\t<th class=\"cellule2LMB date\">Décès</th>\n";
            texte += espace + "\t\t</tr>\n";
            texte += espace + "\t</thead>\n";
            // conjoint
            texte += espace + "\t<tr>\n";
            texte += espace + "\t\t<td class=\"cellule2LTF\">\n";
            texte += espace + "\t\t\tPère\n";
            texte += espace + "\t\t</td>\n";
            texte += espace + "\t\t<td class=\"cellule2LTF\">\n";
            if (info_famille.N1_HUSB != "")
            {
                if (menu)
                {
                    texte += espace + "\t\t\t<a class=\"ficheIndividu\" href=\"../individus/" + info_famille.N1_HUSB + ".html\"></a>\n";
                }
                else
                {
                    texte += espace + "\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                }
            }
            else
            {
                texte += espace + "\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
            }
            texte += espace + "\t\t\t" + nom_conjoint + IDConjointTexte + "\n";
            texte += espace + "\t\t</td>\n";
            texte += espace + "\t\t<td class=\"cellule2LTF date\">\n";

            texte += espace + "\t\t\t" + Avoir_texte_date_PP(
                "BIRT",
                naissance_date_conjoint
                ) + "\n";
            texte += espace + "\t\t</td>\n";
            texte += espace + "\t\t<td class=\"cellule2LTF\" date>\n";
            texte += espace + "\t\t\t" + Avoir_texte_date_PP(
                "DEAT",
                deces_date_conjoint
                ) + "\n";

            texte += espace + "\t\t</td>\n";
            texte += espace + "\t</tr>\n";
            // conjointe
            texte += espace + "\t<tr>\n";
            texte += espace + "\t\t<td class=\"cellule2LTF\">\n";
            texte += espace + "\t\t\tMère\n";

            texte += espace + "\t\t</td>\n";
            texte += espace + "\t\t<td class=\"cellule2LTF\">\n";
            if (R.IsNotNullOrEmpty(info_famille.N1_WIFE))
            {
                if (menu)
                {
                    texte += espace + "\t\t\t<a class=\"ficheIndividu\"  href=\"" + info_famille.N1_WIFE + ".html\"></a>\n";
                }
                else
                {
                    texte += espace + "\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                }
            }
            else
            {
                texte += espace + "\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
            }
            texte += espace + "\t\t\t" + nom_conjointe + IDConjointeTexte + "\n";
            // age 

            // Ordonnance
            if (GH.GHClass.Para.ordonnance_date)
            {
                if (R.IsNotNullOrEmpty(info_famille.N1_SLGS.N1_DATE))
                {
                    bloc += espace + "\t\t\t<div class=\"tableau_ligne\">\n";
                    bloc += espace + "\t\t\t\t<div class=\"tableau_colW\">\n";
                    bloc += espace + "\t\t\t\t\tDate:  ";
                    bloc += espace + "\t\t\t" + Avoir_texte_date_PP(
                        "SLGS",
                        info_famille.N1_SLGS.N1_DATE
                        ) + "\n";
                    bloc += espace + "\t\t\t\t</div>\n";
                    bloc += espace + "\t\t\t</div>\n";
                }
                if (R.IsNotNullOrEmpty(info_famille.N1_SLGS.N1_TYPE)) // V5.3
                {
                    bloc += espace + "\t\t\t<div class=\"tableau_ligne\">\n";
                    bloc += espace + "\t\t\t\t<div class=\"tableau_colW\">\n";
                    bloc += espace + "\t\t\t\t\tStatus: " + Convertir_STAT(info_famille.N1_SLGS.N1_TYPE) + "\n";
                    bloc += espace + "\t\t\t\t</div>\n";
                    bloc += espace + "\t\t\t</div>\n";
                }
                if (R.IsNotNullOrEmpty(info_famille.N1_SLGS.N1_TEMP))
                {
                    bloc += espace + "\t\t\t<div class=\"tableau_ligne\">\n";
                    bloc += espace + "\t\t\t\t<div class=\"tableau_colW\">\n";
                    bloc += espace + "\t\t\t\t\tTemple: " + info_famille.N1_SLGS.N1_TEMP + "\n";
                    bloc += espace + "\t\t\t\t</div>\n";
                    bloc += espace + "\t\t\t</div>\n";
                }
                if (R.IsNotNullOrEmpty(info_famille.N1_SLGS.N1_PLAC))
                {
                    bloc += espace + "\t\t\t<div class=\"tableau_ligne\">\n";
                    bloc += espace + "\t\t\t\t<div class=\"tableau_colW\">\n";
                    bloc += espace + "\t\t\t\t\tLieu: " + info_famille.N1_SLGS.N1_PLAC + "\n";
                    bloc += espace + "\t\t\t\t</div>\n";
                    bloc += espace + "\t\t\t</div>\n";
                }
                if (R.IsNotNullOrEmpty(info_famille.N1_SLGS.N1_STAT))
                {
                    bloc += espace + "\t\t\t<div class=\"tableau_ligne\">\n";
                    bloc += espace + "\t\t\t\t<div class=\"tableau_colW\">\n";
                    bloc += espace + "\t\t\t\t\tStatus: " + Convertir_STAT(info_famille.N1_SLGS.N1_STAT) + "\n";
                    bloc += espace + "\t\t\t\t</div>\n";
                    bloc += espace + "\t\t\t</div>\n";
                }
                if (R.IsNotNullOrEmpty(info_famille.N1_SLGS.N2_STAT_DATE))
                {
                    string date_SLGS;
                    (date_SLGS, _) = Convertir_date(info_famille.N1_SLGS.N2_STAT_DATE, GH.GHClass.Para.date_longue);
                    bloc += espace + "\t\t\t<div class=\"tableau_ligne\">\n";
                    bloc += espace + "\t\t\t\t<div class=\"tableau_colW\">\n";
                    bloc += espace + "\t\t\t\t\t&emsp; En date du " + date_SLGS + "\n";
                    bloc += espace + "\t\t\t\t</div>\n";
                    bloc += espace + "\t\t\t</div>\n";
                }
                bloc += Avoir_texte_lien_citation(
                    info_famille.N1_SLGS.N1_SOUR_citation_liste_ID,
                    liste_citation_ID_numero,
                    0,
                    tab + 3);
                bloc += Avoir_texte_lien_source(
                    info_famille.N1_SLGS.N1_SOUR_source_liste_ID,
                    liste_source_ID_numero,
                    0,
                    tab + 1);
                bloc += Avoir_texte_NOTE_STRUCTURE(
                    info_famille.N1_SLGS.N1_NOTE_STRUCTURE_liste_ID,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    0.04f, // retrait
                    tab + 1);
                if (R.IsNotNullOrEmpty(bloc))
                {
                    // Tableau
                    texte += espace + "\t\t<div class=\"tableau\">\n";
                    // titre de tableau
                    texte += espace + "\t\t\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t\t\t<div class=\"tableau_tete\">\n";
                    texte += espace + "\t\t\t\t\tOrdonnance\n";
                    texte += espace + "\t\t\t\t</div>\n";
                    texte += espace + "\t\t\t</div>\n";
                    texte += bloc;
                    // fin tableau
                    texte += espace + "\t\t</div>\n";
                }
            }
            // date naissance conjointe
            texte += espace + "\t\t<td class=\"cellule2LTF date\">\n";
            texte += espace + "\t\t\t" + Avoir_texte_date_PP(
                "BIRT",
                naissance_date_conjointe
                ) + "\n";
            texte += espace + "\t\t</td>\n";
            texte += espace + "\t\t<td class=\"cellule2LTF date\">\n";
            texte += espace + "\t\t\t" + Avoir_texte_date_PP(
                "DEAT",
                deces_date_conjointe
                ) + "\n";
            texte += espace + "\t\t</td>\n";
            texte += espace + "\t</tr>\n";
            texte += espace + "</table>\n";
            // bloc de texte
            bloc = null;
            // Association V5.5
            if (R.IsNotNullOrEmpty(info_famille.N1_ASSO_liste))
            {
                foreach (GEDCOMClass.ASSOCIATION_STRUCTURE info_ASSO in info_famille.N1_ASSO_liste)
                {
                    // si c'est un individu.
                    if (GEDCOMClass.Si_individu(info_ASSO.N0_ASSO))
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                        bloc += espace + "\t\t\tAssociation\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        if (menu)
                        {
                            bloc += espace + "\t\t\t<a class=\"ficheIndividu\"  href=\"" + info_ASSO.N0_ASSO + ".html\"></a>\n";
                        }
                        else
                        {
                            bloc += espace + "\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                        }
                        bloc += espace + "\t\t\t" + GEDCOMClass.Avoir_premier_nom_individu(info_ASSO.N0_ASSO);
                        if (GH.GHClass.Para.voir_ID == true)
                        {
                            bloc += "[" + info_ASSO.N0_ASSO + "]\n";
                        }
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                        if (R.IsNotNullOrEmpty(info_ASSO.N1_TYPE))
                        {
                            bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                            bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                            bloc += espace + "\t\t\t&nbsp";
                            bloc += espace + "\t\t</div>\n";
                            bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                            bloc += espace + "\t\t\t&emsp;Type: " + info_ASSO.N1_TYPE + "\n";
                            bloc += espace + "\t\t</div>\n";
                            bloc += espace + "\t</div>\n";
                        }
                    }
                    // si c'est un chercheur
                    if (GEDCOMClass.Si_chercheur(info_ASSO.N0_ASSO))
                    {
                        List<string> liste_ID = new List<string>
                        {
                            info_ASSO.N0_ASSO
                        };
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += Avoir_texte_lien_chercheur(liste_ID, liste_SUBMITTER_ID_numero, tab + 3);
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                        if (R.IsNotNullOrEmpty(info_ASSO.N1_TYPE))
                        {
                            bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                            bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                            bloc += espace + "\t\t</div>\n";
                            bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                            bloc += espace + "\t\t\tType: " + info_ASSO.N1_TYPE + "\n";
                            bloc += espace + "\t\t</div>\n";
                            bloc += espace + "\t</div>\n";
                        }
                    }
                }
            }
            // TYPU pour Ancestrologie
            if (R.IsNotNullOrEmpty(info_famille.N1_TYPU))
            {
                bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                bloc += espace + "\t\t\tType d'union (Ancestrologie) " + info_famille.N1_TYPU + "\n";
                bloc += espace + "\t\t</div>\n";
                bloc += espace + "\t</div>\n";
            }
            // _UST pour Heridis
            if (R.IsNotNullOrEmpty(info_famille.N1__UST))
            {
                bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                bloc += espace + "\t\t\tType d'union (Heridis) " +
                    Traduire_relation_conjoint(info_famille.N1__UST) + "\n";
                bloc += espace + "\t\t</div>\n";
                bloc += espace + "\t</div>\n";
            }
            //nombre d'enfant
            if (R.IsNotNullOrEmpty(info_famille.N1_NCHI))
            {
                bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                bloc += espace + "\t\t\tNombre d'enfant " + info_famille.N1_NCHI + "\n";
                bloc += espace + "\t\t</div>\n";
                bloc += espace + "\t</div>\n";
            }
            // REFN
            if (R.IsNotNullOrEmpty(info_famille.N1_REFN_liste))
            {
                foreach (GEDCOMClass.USER_REFERENCE_NUMBER info in info_famille.N1_REFN_liste)
                {
                    bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                    bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                    bloc += espace + "\t\t\tREFN\n";
                    bloc += espace + "\t\t</div>\n";
                    bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                    bloc += espace + "\t\t\t" + info.N0_REFN + " Type " + info.N1_TYPE + "\n";
                    bloc += espace + "\t\t</div>\n";
                    bloc += espace + "\t</div>\n";
                }
            }
            // RIN
            if (R.IsNotNullOrEmpty(info_famille.N1_RIN))
            {
                bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                bloc += espace + "\t\t\tRIN\n";
                bloc += espace + "\t\t</div>\n";
                bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                bloc += espace + "\t\t\t\t\t\t " + info_famille.N1_RIN + "\n";
                bloc += espace + "\t\t</div>\n";
                bloc += espace + "\t</div>\n";
            }
            // chercheur
            if ((R.IsNotNullOrEmpty(info_famille.N1_SUBM_liste_ID)) && GH.GHClass.Para.voir_chercheur)
            {
                bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                bloc += espace + Avoir_texte_lien_chercheur(info_famille.N1_SUBM_liste_ID, liste_SUBMITTER_ID_numero, 6);
                bloc += espace + "\t\t</div>\n";
                bloc += espace + "\t</div>\n";
            }
            // CITATION ********************************************************************************
            bloc += Avoir_texte_lien_citation(
                info_famille.N1_SOUR_citation_liste_ID,
                liste_citation_ID_numero,
                .04f,
                tab + 1);
            // SOURCE **********************************************************************************
            bloc += Avoir_texte_lien_source(
                info_famille.N1_SOUR_source_liste_ID,
                liste_source_ID_numero,
                0.04f,
                tab + 1);
            // note ************************************************************************************
            bloc += Avoir_texte_NOTE_STRUCTURE(
                info_famille.N1_NOTE_STRUCTURE_liste_ID,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero,
                0.04f, // retrait
                tab + 1);
            // date changement *********************************************************************************************
            bloc += Avoir_texte_date_Changement(
                info_famille.N1_CHAN,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero,
                false,
                tab + 1);
            if (R.IsNotNullOrEmpty(bloc))
            {
                texte += Separation(3, null, "000", null, 5);
                // Tableau
                texte += espace + "<div class=\"tableau\">\n";
                texte += bloc;
                // fin tableau
                texte += espace + "</div>\n";
            }
            // @enfant *****************************************************************************************************
            List<string> listIDEnfant = GEDCOMClass.AvoirListIDEnfant(ID_famille);
            if (listIDEnfant.Count() > 0)
            {
                texte += espace + "<a id=\"groupe_enfant\"></a>\n";
                texte += espace + "<!--**************************\n";
                texte += espace + "*  groupe Enfant             *\n";
                texte += espace + "***************************-->\n";
                groupe_enfant = true;
                // titre du groupe
                string titre_enfant = "Enfant";
                if (listIDEnfant.Count() > 1)
                    titre_enfant += "s";
                // titre du groupe
                texte += Titre_groupe(origine + @"commun/enfant.svg", titre_enfant, null, tab);
                texte += Separation(5, null, "000", null, tab);
                texte += espace + "<table class=\"tableau\">\n";
                texte += espace + "\t<thead>\n";
                texte += espace + "\t\t<tr>\n";
                texte += espace + "\t\t\t<th class=\"listeEnfant\">Non</th>\n";
                texte += espace + "\t\t\t<th class=\"listeEnfant date\">Naisannce</th>\n";
                texte += espace + "\t\t\t<th class=\"listeEnfant date\">Décès</th>\n";
                texte += espace + "\t\t</tr>\n";
                texte += espace + "\t</thead>\n";
                string txtIDEfant;
                foreach (string ID_enfant in listIDEnfant)
                {
                    if (R.IsNotNullOrEmpty(ID_enfant))
                    {
                        if (GH.GHClass.Para.voir_ID)
                        {
                            txtIDEfant = " [" + ID_enfant + "]";
                        }
                        else
                        {
                            txtIDEfant = null;
                        }

                        (
                            string nom_enfant,
                            string naissance_date_enfant,
                            string naissance_lieu_enfant,
                            string deces_date_enfant,
                            string deces_lieu_enfant
                        ) = GEDCOMClass.Avoir_nom_naissance_deces(ID_enfant);
                        texte += espace + "\t<tr class=\"listeEnfant\">\n";
                        texte += espace + "\t\t<td class=\"cellule2LTF\">\n";
                        if (R.IsNotNullOrEmpty(ID_enfant))
                        {
                            if (menu)
                            {
                                texte += espace + "\t\t\t<a class=\"ficheIndividu\"  href=\"../individus/" +
                                    ID_enfant + ".html\"></a>\n";
                            }
                            else
                            {
                                texte += espace + "\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                            }
                        }
                        texte += espace + "\t\t\t" + nom_enfant + txtIDEfant + "\n";
                        string adoptionID = GEDCOMClass.Avoir_IDAdoption(ID_enfant);
                        if (R.IsNotNullOrEmpty(adoptionID))
                        {
                            GEDCOM.GEDCOMClass.INDIVIDUAL_RECORD info_individuEnfant;
                            (Ok, info_individuEnfant) = GEDCOMClass.Avoir_info_individu(ID_enfant);
                            string pereIDAdoption = GEDCOMClass.Avoir_famille_conjoint_ID(adoptionID);
                            string nomPereAdoption = GEDCOMClass.AvoirPrenomPatronymeIndividu(pereIDAdoption);
                            string mereIDAdoption = GEDCOMClass.Avoir_famille_conjointe_ID(adoptionID);
                            string nomMereAdoption = GEDCOMClass.AvoirPrenomPatronymeIndividu(mereIDAdoption);
                            texte += espace + "\t\t\t< br />Famille de " + nomPereAdoption + " et " + nomMereAdoption + "\n";
                        }
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"cellule2LTF\">\n";
                        texte += espace + "\t\t\t" + Avoir_texte_date_PP(
                            "BIRT",
                            naissance_date_enfant
                            ) + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"cellule2LTF\">\n";
                        texte += espace + "\t\t\t" + Avoir_texte_date_PP(
                            "DEAT",
                            deces_date_enfant
                            ) + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                }
                if (listIDEnfant.Count == 0)
                {
                    for (int f = 0; f < 2; f++)
                    {
                        texte += espace + "\t<tr>\n";
                        texte += espace + "\t\t<td class=\"listeEnfant\">\n";
                        texte += espace + "\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"listeEnfant\">\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"listeEnfant\">\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + " \t\t<td class=\"listeEnfant\">\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t<tr>\n";
                    }
                }
                texte += espace + "</table>\n";
            }

            // groupe Archive
            {
                string texte_archive;
                (texte_archive, groupe_archive) = Groupe_archive(
                    info_famille.MULTIMEDIA_LINK_liste_ID,
                    sous_dossier,
                    dossier_sortie,
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    tab + 1
                    );
                texte += texte_archive;
            }

            //@Évènement ****************************************************************************************************************************
            string temp;
            (temp, _, _, groupe_evenement) = Groupe_evenement(
                    info_famille.N1_EVEN_Liste,
                    "",
                    "",
                    naissance_date_conjoint,
                    naissance_date_conjointe,
                    sous_dossier,
                    dossier_sortie,
                    menu,
                    numero_carte,
                    numero_source,
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    0);
            texte += temp;
            //@Attribut *************************************************************************************
            (temp, _, _, groupe_attribut) = Groupe_attribut(
                    info_famille.N1_ATTRIBUTE_liste,
                    sous_dossier,
                    dossier_sortie,
                    menu,
                    numero_carte,
                    numero_source,
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    0);
            texte += temp;
            //@chercheur Groupe
            (temp, groupe_chercheur) = Groupe_chercheur(
                    liste_SUBMITTER_ID_numero,
                    sous_dossier,
                    dossier_sortie,
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    3);
            texte += temp;
            // @Citation **************************************************************************************************************
            if (GH.GHClass.Para.voir_reference && liste_citation_ID_numero.Count() > 0)
            {
                (temp, groupe_citation) =
                    Groupe_citation(
                        sous_dossier,
                        dossier_sortie,
                        liste_MULTIMEDIA_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        3);
                texte += temp;
            }
            // @Source **************************************************************************************************************
            if (GH.GHClass.Para.voir_reference && liste_source_ID_numero.Count() > 0)
            {
                (temp, _, _, groupe_source) = Groupe_source
                    (
                    sous_dossier,
                    dossier_sortie,
                    menu,
                    numero_carte,
                    numero_source,
                    nombre_source,
                    liste_SUBMITTER_ID_numero,
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    3);
                texte += temp;
            }
            // @Depot Groupe **********************************************************************************************
            if (GH.GHClass.Para.voir_reference && liste_repo_ID_numero.Count() > 0)
            {

                (temp, groupe_depot) = Groupe_depot(
                    sous_dossier,
                    dossier_sortie,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    3);
                texte += temp;
            }
            // @ Group note **********************************************************************************************
            if (liste_NOTE_RECORD_ID_numero.Count() > 0)
            {
                (temp, groupe_note) = Groupe_NOTE_RECORD(
                    sous_dossier,
                    dossier_sortie,
                    menu,
                    numero_carte,
                    numero_source,
                    nombre_source,
                    liste_SUBMITTER_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    3);
                texte += temp;
            }
            //@menu hamburger
            texte += Avoir_texte_menu_hamburger(
                origine,
                groupe_archive,
                groupe_attribut,
                groupe_chercheur,
                groupe_citation,
                false,
                groupe_depot,
                groupe_enfant,
                groupe_evenement,
                false,
                false,
                false,
                groupe_media,
                groupe_note,
                false,
                false,
                groupe_source
                );
            texte += Bas_Page();
            try
            {
                System.IO.File.WriteAllText(nomFichierFamille, texte);
            }
            catch (Exception msg)
            {
                GC.Collect();
                R.Afficher_message("Ne peut pas écrire le fichier" + nomFichierFamille + ".", msg.Message, GH.GHClass.erreur);
            }
            GC.Collect();
            string nom = "Famille de ";
            if (nom_conjoint != null && nom_conjointe == null)
                nom += nom_conjoint;
            if (nom_conjoint == null && nom_conjointe != null)
                nom += nom_conjointe;
            if (nom_conjoint != null && nom_conjointe != null)
                nom += nom_conjoint + " et de " + nom_conjointe;

            return nom;
        }
        private string Haut_Page(string sous_dossier, bool menu, string titre)
        {
            string origine = @"../";
            if (R.IsNullOrEmpty(sous_dossier))
                origine = "";
            string texte = "<!DOCTYPE html>\n";
            texte += "<html lang=\"fr\">\n";
            texte += "\t<head>\n";
            texte += "\t\t<meta charset = 'UTF-8' />\n";
            texte += "\t\t\t<title>" + titre + "</title>\n";
            texte += "\t\t<link rel=\"stylesheet\" href=\"" + origine + "commun/dapam.css\" type=\"text/css\" />\n";
            texte += "\t\t<link rel=\"stylesheet\" href=\"" + origine + "commun/leaflet.css\" type=\"text/css\" />\n";
            texte += "\t\t<link rel=\"shortcut icon\" href = \"" + origine + "commun/favicon.ico\" />\n";
            texte += "\t\t<script  src=\"" + origine + "commun/modal.js\"></script>\n";
            texte += "\t\t<script  src=\"" + origine + "commun/leaflet.js\"></script>\n";
            texte += "\t</head>\n";
            texte += "\t<body>\n";
            texte += "\t\t<a id=\"haut_page\"></a>\n";
            texte += "\t\t<div id=\"modal-arriere\">\n";
            texte += "\t\t\t<div id=\"modal-image\">\n";
            texte += "\t\t\t</div>\n";
            texte += "\t\t</div>\n";
            if (menu)
            {
                texte += "\t\t<header>\n";
                texte += "\t\t\t<div class=\"navbar\">\n";
                texte += "\t\t\t\t<a class=\"poussoir\" href=\"" + origine + "index.html\">\n";
                texte += "\t\t\t\t\t<img style=\"height:40px\" src=\"" + origine + "commun/accueil.svg\" alt=\"\" />\n";
                texte += "\t\t\t\t\tAccueil\n";
                texte += "\t\t\t\t</a>\n";
                texte += "\t\t\t\t<a class=\"poussoir\" href=\"" + origine + "individus/index.html\">\n";
                texte += "\t\t\t\t\t<img style=\"height:40px\" src=\"" + origine + "commun/neutre.svg\" alt=\"\" />\n";
                texte += "\t\t\t\t\tIndividu\n";
                texte += "\t\t\t\t</a>\n";
                texte += "\t\t\t\t<a class=\"poussoir\" href=\"" + origine + "familles/indexConjoint.html\">\n";
                texte += "\t\t\t\t\t<table>\n";
                texte += "\t\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t\t<td>\n";
                texte += "\t\t\t\t\t\t\t\t<img style=\"height:35px\" src=\"" + origine + "commun/familleConjoint.svg\" alt=\"\" />\n";
                texte += "\t\t\t\t\t\t\t</td>\n";
                texte += "\t\t\t\t\t\t\t<td>\n";
                texte += "\t\t\t\t\t\t\t\tFamille<br/ >par conjoint\n";
                texte += "\t\t\t\t\t\t\t</td>\n";
                texte += "\t\t\t\t\t\t</tr>\n";
                texte += "\t\t\t\t\t</table>\n";
                texte += "\t\t\t\t</a>\n";
                texte += "\t\t\t\t<a class=\"poussoir\" href=\"" + origine + "familles/indexConjointe.html\">\n";
                texte += "\t\t\t\t\t<table>\n";
                texte += "\t\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t\t<td>\n";
                texte += "\t\t\t\t\t\t\t\t<img style=\"height:35px\" src=\"" + origine + "commun/familleConjointe.svg\" alt=\"\" />\n";
                texte += "\t\t\t\t\t\t\t</td>\n";
                texte += "\t\t\t\t\t\t\t<td>\n";
                texte += "\t\t\t\t\t\t\t\tFamille<br />par conjointe\n";
                texte += "\t\t\t\t\t\t\t</td>\n";
                texte += "\t\t\t\t\t\t</tr>\n";
                texte += "\t\t\t\t\t</table>\n";
                texte += "\t\t\t\t</a>\n";
                texte += "\t\t\t</div>\n";
                texte += "\t\t</header>\n";
            }
            texte += "\t\t<div class=\"page\"><!-- haut de page -->\n";
            GC.Collect();
            return texte;
        }

        public void Index(
            string fichierGEDCOM,
            string nombreIndividu,
            string nombreFamille
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + " à <b>" + Avoir_nom_method() + "</b>");
            Regler_code_erreur();
            Application.DoEvents();
            string dossier_sortie = GH.GHClass.dossier_sortie;
            int numero_source = 0;
            int numero_carte = 0;
            string sous_dossier = "";
            string temp_string;
            int tab = 3;
            int nombre_source = 0;
            string espace = Tabulation(tab);
            // déclaration
            // MULTIMEDIA_ID_numero
            List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero = new List<MULTIMEDIA_ID_numero>();
            MULTIMEDIA_ID_numero a = new MULTIMEDIA_ID_numero(); // ajoute entrer [0] null
            liste_MULTIMEDIA_ID_numero.Add(a);

            List<ID_numero> liste_note_STRUCTURE_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_NOTE_RECORD_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_citation_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_source_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_repo_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_SUBMITTER_ID_numero = new List<ID_numero>();

            GEDCOMClass.HEADER info_GEDCOM = GEDCOMClass.Avoir_info_GEDCOM();
            GEDCOMClass.SUBMISSION_RECORD info_SUBMISSION_RECORD = GEDCOMClass.Avoir_info_SUBMISSION_RECORD();

            //Généré référence
            {
                // GEDCOM groupe
                //      HEAD NOTE
                // Groupe Information d'envoie
                if (info_SUBMISSION_RECORD != null)
                {
                    (liste_note_STRUCTURE_ID_numero, _) =
                        Verifier_liste(info_SUBMISSION_RECORD.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                }

                // Chercheur
                if (GH.GHClass.Para.voir_chercheur && info_GEDCOM.N1_SUBM_liste_ID != null)
                {
                    (liste_SUBMITTER_ID_numero, _) = Verifier_liste(info_GEDCOM.N1_SUBM_liste_ID, liste_SUBMITTER_ID_numero);
                }

                // générer tout
                (
                    liste_SUBMITTER_ID_numero,
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    _
                ) =
                    Genere_liste_reference_tous
                    (
                        liste_SUBMITTER_ID_numero,
                        liste_MULTIMEDIA_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero
                    );
            }
            //FIN Généré référence

            string nomFichier = @dossier_sortie + @"\index.html";
            string texte = null;
            bool groupe_archive = false;
            bool groupe_GEDCOM;
            bool groupe_information = false;
            bool groupe_logiciel = false;
            bool groupe_chercheur;
            bool groupe_citation = false;
            bool groupe_source = false;
            bool groupe_depot = false;
            bool groupe_note = false;
            texte += Haut_Page("", true, "Accueil");
            // GEDCOM ******************************************************************************************************
            groupe_GEDCOM = true;
            texte += espace + "<a id=\"groupe_GEDCOM\"></a>\n";
            texte += espace + "<!--**************************\n";
            texte += espace + "*  groupe GEDCOM             *\n";
            texte += espace + "***************************-->\n";
            texte += Titre_groupe(@"commun/GEDCOM.svg", "GEDCOM", null, tab);
            // Tableau
            texte += Separation(5, null, "000", null, tab);
            texte += espace + "<div class=\"tableau\">\n";
            // version
            if (info_GEDCOM.N2_GEDC_VERS != null)
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tVersion" + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info_GEDCOM.N2_GEDC_VERS + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // FORM
            if (info_GEDCOM.N2_GEDC_FORM != null)
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tForme version" + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info_GEDCOM.N2_GEDC_FORM;
                if (R.IsNotNullOrEmpty(info_GEDCOM.N3_GEDC_FORM_VERS))
                    texte += " V" + info_GEDCOM.N3_GEDC_FORM_VERS;
                texte += "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // Fichier
            if (info_GEDCOM.N1_FILE != null)
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tFichier" + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + Convertir_filiation(info_GEDCOM.N1_FILE) + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // Destination
            if (info_GEDCOM.N1_DEST != null)
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tDestination" + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += Convertir_texte_html(info_GEDCOM.N1_DEST, false, tab + 3);
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // date heure
            if (info_GEDCOM.N1_DATE != null)
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tTransmission ou création\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                (string date, _) = Convertir_date(info_GEDCOM.N1_DATE, GH.GHClass.Para.date_longue);
                texte += espace + "\t\t\t" + date + " à " + info_GEDCOM.N2_DATE_TIME + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            //char set
            if (R.IsNotNullOrEmpty(info_GEDCOM.N1_CHAR))
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tCaractère set" + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info_GEDCOM.N1_CHAR + "\n";
                if (R.IsNotNullOrEmpty(info_GEDCOM.N2_CHAR_VERS))
                    texte += espace + "\t\t\tVersion " + info_GEDCOM.N2_CHAR_VERS + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // _GUID Herisis
            if (info_GEDCOM.N1__GUID != null)
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tIdentificateur global unique" + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + info_GEDCOM.N1__GUID + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // language
            if (info_GEDCOM.N1_LANG != null)
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tLangue" + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + Traduire_language(info_GEDCOM.N1_LANG) + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // place
            if (info_GEDCOM.N2_PLAC_FORM != null)
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tPlace" + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + info_GEDCOM.N2_PLAC_FORM;
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // copyright
            if (info_GEDCOM.N1_COPR != null)
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tCopyright\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += Convertir_texte_html(info_GEDCOM.N1_COPR, false, tab + 3) + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // nombre individu
            if (nombreIndividu != "00")
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tNombre d'individu\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + nombreIndividu + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // nombre de famille
            if (nombreFamille != "00")
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tNombre de famille\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\t" + nombreFamille + "\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // destination
            if (info_GEDCOM.N1_DEST != null)
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                texte += espace + "\t\t\tRéception par\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += Convertir_texte_html(info_GEDCOM.N1_DEST, false, tab + 3);
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            //chercheur
            if (info_GEDCOM.N1_SUBM_liste_ID != null)
            {
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += Avoir_texte_lien_chercheur(info_GEDCOM.N1_SUBM_liste_ID, liste_SUBMITTER_ID_numero, tab + 3);
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
            }
            // note GEDCOM ****************************************************************************
            texte += Avoir_texte_NOTE_STRUCTURE(
                info_GEDCOM.N1_NOTE_STRUCTURE_liste_ID,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero,
                .04f, // retrait
                tab + 1);
            // FIN tableau
            texte += espace + "</div>\n";
            // Information d'envoie ****************************************************************************************
            {
                string bloc = null;
                if (info_SUBMISSION_RECORD.N1_FAMF != null)
                {
                    if (GH.GHClass.Para.voir_ID)
                    {
                        bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                        bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                        bloc += espace + "\t\t\t<span class=\"tableau_ID\">[" + info_SUBMISSION_RECORD.N0_ID + "]</span>\n";
                        bloc += espace + "\t\t</div>\n";
                        bloc += espace + "\t</div>\n";
                    }
                    bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                    bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                    bloc += espace + "\t\t\tNom du fichier familial\n";
                    bloc += espace + "\t\t</div>\n";
                    bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                    bloc += espace + "\t\t\t" + info_SUBMISSION_RECORD.N1_FAMF + "\n";
                    bloc += espace + "\t\t</div>\n";
                    bloc += espace + "\t</div>\n";
                }
                if (info_SUBMISSION_RECORD.N1_TEMP != null)
                {
                    bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                    bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                    bloc += espace + "\t\t\tCode du temple\n";
                    bloc += espace + "\t\t</div>\n";
                    bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                    bloc += espace + "\t\t\t" + info_SUBMISSION_RECORD.N1_TEMP + "\n";
                    bloc += espace + "\t\t</div>\n";
                    bloc += espace + "\t</div>\n";
                }
                if (info_SUBMISSION_RECORD.N1_ANCE != null)
                {
                    bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                    bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                    bloc += espace + "\t\t\tGénération d'ancêtre\n";
                    bloc += espace + "\t\t</div>\n";
                    bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                    bloc += espace + "\t\t\t" + info_SUBMISSION_RECORD.N1_ANCE + "\n";
                    bloc += espace + "\t\t</div>\n";
                    bloc += espace + "\t</div>\n";
                }
                if (info_SUBMISSION_RECORD.N1_DESC != null)
                {
                    bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                    bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                    bloc += espace + "\t\t\tGénération de descendant\n";
                    bloc += espace + "\t\t</div>\n";
                    bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                    bloc += espace + "\t\t\t" + info_SUBMISSION_RECORD.N1_DESC + "\n";
                    bloc += espace + "\t\t</div>\n";
                    bloc += espace + "\t</div>\n";
                }
                if (R.IsNotNullOrEmpty(info_SUBMISSION_RECORD.N1_ORDI))
                {
                    bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                    bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                    bloc += espace + "\t\t\tProcessus d'ordonnance\n";
                    bloc += espace + "\t\t</div>\n";
                    bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                    bloc += espace + "\t\t\t" + Traduire_yes_no(info_SUBMISSION_RECORD.N1_ORDI) + "\n";
                    bloc += espace + "\t\t</div>\n";
                    bloc += espace + "\t</div>\n";
                }
                if (info_SUBMISSION_RECORD.N1_RIN != null)
                {
                    bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                    bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                    bloc += espace + "\t\t\tRIN\n";
                    bloc += espace + "\t\t</div>\n";
                    bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                    bloc += espace + "\t\t\t" + info_SUBMISSION_RECORD.N1_RIN + "\n";
                    bloc += espace + "\t\t</div>\n";
                    bloc += espace + "\t</div>\n";
                }
                // sousmit par
                if (info_SUBMISSION_RECORD.N1_SUBM_liste_ID != null)
                {
                    bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                    bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                    string texteTemp = "";
                    bloc += Avoir_texte_lien_chercheur(info_SUBMISSION_RECORD.N1_SUBM_liste_ID, liste_SUBMITTER_ID_numero, tab + 3);
                    bloc += texteTemp.TrimEnd(' ', ',');
                    bloc += espace + "\t\t\t</td>\n";
                    bloc += espace + "\t\t</div>\n";
                    bloc += espace + "\t</div>\n";
                }
                // note  du sumiteur *******************************************************************
                if (info_SUBMISSION_RECORD.N1_NOTE_STRUCTURE_liste_ID != null)
                {
                    bloc +=
                    Avoir_texte_NOTE_STRUCTURE(
                        info_SUBMISSION_RECORD.N1_NOTE_STRUCTURE_liste_ID,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        .04f, // retrait
                        tab + 3);
                }
                // si changement de date
                if (GH.GHClass.Para.voir_date_changement && info_SUBMISSION_RECORD.N1_CHAN != null)
                {
                    bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                    bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                    bloc += Avoir_texte_date_Changement(
                        info_SUBMISSION_RECORD.N1_CHAN,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        false,
                        tab + 3);
                    bloc += espace + "\t\t</div>\n";
                    bloc += espace + "\t</div>\n";
                }
                if (bloc != null)
                {
                    groupe_information = true;
                    texte += espace + "<a id=\"groupe_information\"></a>\n";
                    texte += espace + "<!--**************************\n";
                    texte += espace + "*  groupe Information        *\n";
                    texte += espace + "***************************-->\n";
                    // titre du groupe
                    texte += Titre_groupe(@"commun/envoi.svg", "Information d'envoi", null, tab);
                    texte += Separation(5, null, "000", null, tab);
                    // Tableau
                    texte += espace + "<div class=\"tableau\">\n";
                    texte += bloc;

                    // fin tableau
                    texte += espace + "</div>\n";
                }
            }
            // Logiciel ****************************************************************************************************
            if (info_GEDCOM.N1_SOUR != null)
            {
                groupe_logiciel = true;
                texte += espace + "<a id=\"groupe_logiciel\"></a>\n";
                texte += espace + "<!--**************************\n";
                texte += espace + "*  groupe Logiciel           *\n";
                texte += espace + "***************************-->\n";
                // titre Logiciel
                texte += Titre_groupe(@"commun/logiciel.svg", "Logiciel", null, tab);
                // tableau
                texte += Separation(5, null, "000", null, tab);
                texte += espace + "<div class=\"tableau\">\n";
                // SOUR
                if (info_GEDCOM.N1_SOUR != null)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tID système\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t" + info_GEDCOM.N1_SOUR + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // version
                if (info_GEDCOM.N2_SOUR_VERS != null)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tVersion\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t" + info_GEDCOM.N2_SOUR_VERS + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // NANE
                if (info_GEDCOM.N2_SOUR_NAME != null)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tGénérer par\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += Convertir_texte_html(info_GEDCOM.N2_SOUR_NAME, false, tab + 3);
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // corporation *************************************************************************************************************************
                if (info_GEDCOM.N2_SOUR_CORP != null)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tCompagnie\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += Convertir_texte_html(info_GEDCOM.N2_SOUR_CORP, false, tab + 3);
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                    string texte_adresse = Avoir_texte_adresse(
                        info_GEDCOM.N3_SOUR_CORP_SITE,
                        info_GEDCOM.N3_SOUR_CORP_ADDR_liste,// info_adresse_CORP,
                        info_GEDCOM.N3_SOUR_CORP_PHON_liste,
                        info_GEDCOM.N3_SOUR_CORP_FAX_liste,
                        info_GEDCOM.N3_SOUR_CORP_EMAIL_liste,
                        info_GEDCOM.N3_SOUR_CORP_WWW_liste,
                        sous_dossier,
                        liste_source_ID_numero,
                        liste_citation_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero, // à vérifier
                        tab + 0);

                    if (texte_adresse != null)
                    {
                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                        texte += espace + "\t\t\tAdresse\n";
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                        texte += texte_adresse;
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t</div>\n";
                    }
                }
                // information 
                if (
                    info_GEDCOM.N2_SOUR_DATA != null ||
                    info_GEDCOM.N3_SOUR_DATA_DATE != null ||
                    info_GEDCOM.N3_SOUR_DATA_COPR != null
                    )
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tInformation\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    if (info_GEDCOM.N2_SOUR_DATA != null)
                        texte += Convertir_texte_html(info_GEDCOM.N2_SOUR_DATA, false, tab + 3) + "<br />\n";
                    (string date, _) = Convertir_date(info_GEDCOM.N3_SOUR_DATA_DATE, GH.GHClass.Para.date_longue);
                    if (info_GEDCOM.N3_SOUR_DATA_DATE != null)
                        texte += espace + "\t\t\t" + date + "<br />\n";

                    if (info_GEDCOM.N3_SOUR_DATA_COPR != null)
                        texte += Convertir_texte_html(info_GEDCOM.N3_SOUR_DATA_COPR, false, tab + 3) + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }

                // FIN tableau
                texte += espace + "</div>\n";
            }
            // Groupe chercheur ********************************************************************************************
            string b;
            (b, groupe_chercheur) = Groupe_chercheur(
                liste_SUBMITTER_ID_numero,
                "", // sous_sossier racine
                dossier_sortie,
                liste_MULTIMEDIA_ID_numero,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_repo_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero,
                tab);
            texte += b;
            //Groupe Citation **********************************************************************************************
            if (
                GH.GHClass.Para.voir_reference &&
                liste_citation_ID_numero.Count > 0)
            {
                // Générer le texte HTML
                (temp_string, groupe_citation) =
                        Groupe_citation(
                        "",
                        dossier_sortie,
                        liste_MULTIMEDIA_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        3);
                texte += temp_string;
            }
            // @source ************************************************************************************************************************
            if (GH.GHClass.Para.voir_reference && liste_source_ID_numero.Count() > 0)
            {
                // Générer le texte HTML
                (temp_string, _, _, groupe_source) = Groupe_source(
                        "",
                        dossier_sortie,
                        false, // menu
                        numero_carte,
                        numero_source,
                        nombre_source,
                        liste_SUBMITTER_ID_numero,
                        liste_MULTIMEDIA_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        3);
                texte += temp_string;
            }
            // @Depot Groupe **********************************************************************************************

            if (GH.GHClass.Para.voir_reference && liste_repo_ID_numero.Count() > 0)
            {

                (temp_string, groupe_depot) = Groupe_depot(
                    sous_dossier,
                    dossier_sortie,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    3);
                texte += temp_string;
            }
            //  Groupe note *************************************************************************
            if (liste_NOTE_RECORD_ID_numero.Count() > 0)
            {
                (temp_string, groupe_note) = Groupe_NOTE_RECORD(
                        sous_dossier,
                        dossier_sortie,
                        true,
                        numero_carte,
                         numero_source,
                        nombre_source,
                        liste_SUBMITTER_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        3);
                texte += temp_string;
            }
            // menu hamburger
            texte += Avoir_texte_menu_hamburger(
                "",
                groupe_archive,
                false,

                groupe_chercheur,
                groupe_citation,
                false,
                groupe_depot,
                false,
                false,
                groupe_information,
                groupe_GEDCOM,
                groupe_logiciel,
                false,
                groupe_note,
                false,
                false,
                groupe_source);
            // bas de page
            texte += Bas_Page();
            try
            {
                System.IO.File.WriteAllText(nomFichier, texte);
            }
            catch (Exception msg)
            {
                GC.Collect();
                R.Afficher_message("Ne peut pas écrire le fichier" + nomFichier + ".", msg.Message, GH.GHClass.erreur);
            }
            GC.Collect();
        }
        public void Index_famille_conjoint(string dossier_sortie)
        {
            Regler_code_erreur();
            Application.DoEvents();
            ListView lvChoixFamille = Application.OpenForms["GHClass"].Controls["lvChoixFamille"] as ListView;
            string nomFichier = @dossier_sortie + @"\familles\" + "indexConjoint.html";
            if (File.Exists(nomFichier))
            {
                File.Delete(nomFichier);
            }
            string texte = null;
            string FamilleConjointIndex = Texte_bouton_index_conjoint();
            texte += Haut_Page("familles", true, "Index famille");
            texte += Separation(15, null, "000", null, 3);
            texte += FamilleConjointIndex;
            texte += "\t\t\t<h1> \n";
            texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/familleConjoint.svg\" alt=\"\" />\n";
            texte += "\t\t\t\tIndex des familles par conjoint\n";
            texte += "\t\t\t\t</h1>\n";
            texte += Bas_Page();
            try
            {
                System.IO.File.WriteAllText(nomFichier, texte);
            }
            catch (Exception msg)
            {
                GC.Collect();
                R.Afficher_message("Ne peut pas écrire le fichier" + nomFichier + ".", msg.Message, GH.GHClass.erreur);
            }
            // créé les pages
            bool[] alphabette = new bool[27];
            for (int f = 0; f < 27; f++)
            {
                alphabette[f] = false;
            }
            // détermine si lettre est utiliser
            for (int f = 0; f < lvChoixFamille.Items.Count; f++)
            {
                if (lvChoixFamille.Items[f].SubItems[1].Text != "")
                {
                    char l = lvChoixFamille.Items[f].SubItems[1].Text[0];
                    int v = (int)l - 65;
                    if (v > -1 && v < 26)
                    {
                        alphabette[v] = true;
                    }
                    else
                    {
                        alphabette[26] = true;
                    }
                }
            }
            for (int f = 0; f < 27; f++)
            {
                if (alphabette[f])
                {
                    texte = null;
                    texte += Haut_Page("..//", true, "Index famille");
                    texte += Separation(15, null, "000", null, 3);
                    texte += FamilleConjointIndex;
                    texte += "\t\t\t<h1>\n";
                    if (f == 26)
                    {
                        texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/familleConjoint.svg\" alt=\"\" />\n";
                        texte += "\t\t\t\tFamilles par conjoint diver\n";
                    }
                    else
                    {
                        texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/familleConjoint.svg\" alt=\"\" />\n";
                        texte += "\t\t\t\tFamilles par conjoint " + (char)(f + 65) + "\n";
                    }
                    texte += "\t\t\t</h1>";
                    texte += "\t\t\t<table class=\"atl\" style=\"border:1px;\">";
                    texte += "\t\t\t\t<tr>";
                    texte += "\t\t\t\t\t<td class=\"liste\">Voir</td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Conjoint\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Conjointe\n";

                    texte += "\t\t\t\t\t<td class=\"liste\">Mariage</td>\n";
                    if (GH.GHClass.Para.voir_ID)
                        texte += "\t\t\t\t\t<td class=\"liste\">ID</td>\n";
                    texte += "\t\t\t\t</tr>";
                    for (int ff = 0; ff < lvChoixFamille.Items.Count; ff++)
                    {
                        char l;
                        if (lvChoixFamille.Items[ff].SubItems[1].Text == "")
                        {
                            l = '#';
                        }
                        else
                        {
                            l = lvChoixFamille.Items[ff].SubItems[1].Text[0];
                        }
                        if ((char)(f + 65) == l)
                        {
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t<a class=\"ficheFamille\"  href=\"" + lvChoixFamille.Items[ff].SubItems[0].Text + ".html\">\n";
                            texte += "\t\t\t\t\t\t</a>\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixFamille.Items[ff].SubItems[1].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixFamille.Items[ff].SubItems[2].Text + "</td>\n";

                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t" + Avoir_texte_date_PP(
                                "mariage",
                                lvChoixFamille.Items[ff].SubItems[5].Text) + "\n";
                            texte += "\t\t\t\t\t</td>\n";
                            if (GH.GHClass.Para.voir_ID)
                                texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixFamille.Items[ff].SubItems[0].Text + "</td>\n";
                            texte += "\t\t\t\t</tr>";
                        }
                        else if (f == 26 && !("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(l)))
                        {
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"" + lvChoixFamille.Items[ff].SubItems[0].Text + ".html\">\n";
                            texte += "\t\t\t\t\t\t</a>\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixFamille.Items[ff].SubItems[1].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixFamille.Items[ff].SubItems[2].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t" + Avoir_texte_date_PP(
                                "mariage",
                                lvChoixFamille.Items[ff].SubItems[5].Text
                                ) + "\n";
                            texte += "\t\t\t\t\t</td>\n";
                            if (GH.GHClass.Para.voir_ID)
                                texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixFamille.Items[ff].SubItems[0].Text + "</td>\n";
                            texte += "\t\t\t\t</tr>";
                        }
                    }
                    texte += "\t\t\t</table>";
                    texte += Bas_Page();
                    try
                    {
                        if (f == 26)
                        {
                            System.IO.File.WriteAllText(@dossier_sortie + @"\familles\mDiver.html", texte);
                        }
                        else
                        {
                            System.IO.File.WriteAllText(@dossier_sortie + @"\familles\m" + (char)(f + 65) + ".html", texte);
                        }
                    }
                    catch (Exception msg)
                    {
                        GC.Collect();
                        R.Afficher_message("Ne peut pas écrire le fichier" + dossier_sortie + ".", msg.Message, GH.GHClass.erreur);
                    }
                }
            }
            GC.Collect();
        }
        public void Index_famille_conjointe(string dossier_sortie)
        {
            Regler_code_erreur();
            Application.DoEvents();
            string sous_dossier = "familles";
            //string origine = @"../";

            string nomFichier = @dossier_sortie + @"\familles\" + "indexConjointe.html";
            ListView lvChoixFamille = Application.OpenForms["GHClass"].Controls["lvChoixFamille"] as ListView;
            if (File.Exists(nomFichier))
            {
                File.Delete(nomFichier);
            }
            string texte = null;
            string FamilleConjointeIndex = Texte_bouton_index_conjointe();
            texte += Haut_Page(sous_dossier, true, "Index famille par ordre de conjointe");
            texte += Separation(15, null, "000", null, 3);
            texte += FamilleConjointeIndex;
            texte += "\t\t\t<h1>\n";
            texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/familleConjointe.svg\" alt=\"\" />\n";
            texte += "\t\t\t\tIndex des familles par conjointe\n";
            texte += "\t\t\t</h1>\n";
            texte += Bas_Page();
            try
            {
                System.IO.File.WriteAllText(nomFichier, texte);
            }
            catch (Exception msg)
            {
                GC.Collect();
                R.Afficher_message("Ne peut pas écrire le fichier" + nomFichier + ".", msg.Message, GH.GHClass.erreur);
            }
            // créé les pages
            bool[] alphabette = new bool[27];
            for (int f = 0; f < 27; f++)
            {
                alphabette[f] = false;
            }
            // détermine si lettre est utiliser
            for (int f = 0; f < lvChoixFamille.Items.Count; f++)
            {
                if (lvChoixFamille.Items[f].SubItems[2].Text != "")
                {
                    char l = lvChoixFamille.Items[f].SubItems[2].Text[0];
                    int v = (int)l - 65;
                    if (v > -1 && v < 26)
                    {
                        alphabette[v] = true;
                    }
                    else
                    {
                        alphabette[26] = true;
                    }
                }
            }
            for (int f = 0; f < 27; f++)
            {
                if (alphabette[f])
                {
                    texte = null;
                    texte += Haut_Page("../", true, "Index famille par ordre de conjointe");
                    texte += Separation(15, null, "000", null, 3);
                    texte += FamilleConjointeIndex;
                    texte += "\t\t\t<h1>\n";
                    if (f == 26)
                    {
                        texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/familleConjointe.svg\" alt=\"\" />\n";
                        texte += "\t\t\t\tFamilles diver par conjointe\n";
                    }
                    else
                    {
                        texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/familleConjointe.svg\" alt=\"\" />\n";
                        texte += "\t\t\t\tFamilles par conjointe " + (char)(f + 65) + "\n";
                    }
                    texte += "\t\t\t</h1>\n";
                    texte += "\t\t\t<table class=\"atl\" style=\"border:1px;\">\n";
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Voir</td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Conjointe</td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Conjoint</td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Mariage</td>\n";

                    if (GH.GHClass.Para.voir_ID)
                        texte += "\t\t\t\t\t<td class=\"liste\">ID</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                    for (int ff = 0; ff < lvChoixFamille.Items.Count; ff++)
                    {
                        char l;
                        if (lvChoixFamille.Items[ff].SubItems[2].Text == "")
                        {
                            l = '#';
                        }
                        else
                        {
                            l = lvChoixFamille.Items[ff].SubItems[2].Text[0];
                        }
                        if ((char)(f + 65) == l)
                        {
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t<a class=\"ficheFamille\"  href=\"" + lvChoixFamille.Items[ff].SubItems[0].Text + ".html\"></a>\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixFamille.Items[ff].SubItems[2].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixFamille.Items[ff].SubItems[1].Text + "</td>\n";

                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t" + Avoir_texte_date_PP(
                                "mariage",
                                lvChoixFamille.Items[ff].SubItems[5].Text) + "\n";
                            texte += "\t\t\t\t\t</td>\n";
                            if (GH.GHClass.Para.voir_ID)
                                texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixFamille.Items[ff].SubItems[0].Text + "</td>\n";
                            texte += "\t\t\t\t</tr>\n";
                        }
                        else if (f == 26 && !("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(l)))
                        {
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"" + lvChoixFamille.Items[ff].SubItems[0].Text + ".html\">\n";
                            texte += "\t\t\t\t\t\t</a>\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixFamille.Items[ff].SubItems[2].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixFamille.Items[ff].SubItems[1].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t" + Avoir_texte_date_PP(
                                "mariage",
                                lvChoixFamille.Items[ff].SubItems[5].Text) + "\n";
                            texte += "\t\t\t\t\t</td>\n";
                            if (GH.GHClass.Para.voir_ID)
                                texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixFamille.Items[ff].SubItems[0].Text + "</td>\n";
                            texte += "\t\t\t\t</tr>\n";
                        }
                    }
                    texte += "\t\t\t</table>\n";
                    texte += Bas_Page();
                    try
                    {
                        if (f == 26)
                        {
                            System.IO.File.WriteAllText(@dossier_sortie + @"\familles\fDiver.html", texte);
                            if (GH.GHClass.annuler)
                                return;
                        }
                        else
                        {
                            System.IO.File.WriteAllText(@dossier_sortie + @"\familles\f" + (char)(f + 65) + ".html", texte);
                            if (GH.GHClass.annuler)
                                return;
                        }
                    }
                    catch (Exception msg)
                    {
                        GC.Collect();
                        R.Afficher_message("Ne peut pas écrire le fichier" + dossier_sortie + ".", msg.Message, GH.GHClass.erreur);
                    }
                }
            }
            GC.Collect();
        }

        public void Index_individu(string dossier_sortie)
        {
            Regler_code_erreur();
            Application.DoEvents();
            string nomFichier = @dossier_sortie + @"\individus\" + "index.html";
            ListView lvChoixIndividu = Application.OpenForms["GHClass"].Controls["lvChoixIndividu"] as ListView;
            if (File.Exists(nomFichier))
            {
                File.Delete(nomFichier);
            }
            string texte = null;
            texte += Haut_Page("..//", true, "Index individu");
            texte += Separation(15, null, "000", null, 3);
            string texte_index = Texte_bouton_index_individu();
            texte += texte_index;
            texte += "\t\t\t<h1 style=\"background-color:" + GH.GHClass.Para.arriere_plan + "\">\n";
            texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/neutre.svg\" alt=\"\" />\n";
            texte += "\t\t\t\tIndex des individus\n";
            texte += "\t\t\t</h1>\n";
            texte += Bas_Page();
            try
            {
                System.IO.File.WriteAllText(nomFichier, texte);
            }
            catch (Exception msg)
            {
                GC.Collect();
                R.Afficher_message("Ne peut pas écrire le fichier" + nomFichier + ".", msg.Message, GH.GHClass.erreur);
                if (GH.GHClass.annuler)
                    return;
            }
            // créé les pages
            bool[] alphabette = new bool[27];
            for (int f = 0; f < 27; f++)
            {
                alphabette[f] = false;
            }
            // détermine si lettre est utiliser
            for (int f = 0; f < lvChoixIndividu.Items.Count; f++)
            {
                if (lvChoixIndividu.Items[f].SubItems[1].Text != "")
                {
                    if (lvChoixIndividu.Items[f].SubItems[1].Text[0].ToString() != "")
                    {
                        char l = lvChoixIndividu.Items[f].SubItems[1].Text[0];
                        int v = (int)l - 65;
                        if (v > -1 && v < 26)
                        {
                            alphabette[v] = true;
                        }
                        else
                        {
                            alphabette[26] = true;
                        }
                    }
                    else
                    {
                        alphabette[26] = true;
                    }
                }
                else
                {
                    alphabette[26] = true;
                }
            }
            for (int f = 0; f < 27; f++)
            {
                if (alphabette[f])
                {
                    texte = null;
                    texte += Haut_Page("../", true, "Index individu");
                    texte += Separation(15, null, "000", null, 3);       // retrait
                    texte += texte_index;
                    texte += "\t\t\t<h1>\n";
                    if (f == 26)
                    {
                        texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/neutre.svg\" alt=\"\" />\n";
                        texte += "\t\t\t\tIndividus diver" + "\n";
                    }
                    else
                    {
                        texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/neutre.svg\" alt=\"\" />\n";
                        texte += "\t\t\t\tIndividus  " + (char)(f + 65) + "\n";
                    }
                    texte += "\t\t\t</h1>\n";
                    texte += "\t\t\t<table class=\"atl\" style=\"border:1px;\">\n";
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Voir</td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Nom</td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Naissance</td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Décès</td>\n";
                    if (GH.GHClass.Para.voir_ID)
                        texte += "\t\t\t\t\t<td class=\"liste\">ID</td>\n";

                    texte += "\t\t\t\t</tr>";
                    for (int ff = 0; ff < lvChoixIndividu.Items.Count; ff++)
                    {
                        char l;
                        if (lvChoixIndividu.Items[ff].SubItems[1].Text == "")
                        {
                            l = '#';
                        }
                        else
                        {
                            l = lvChoixIndividu.Items[ff].SubItems[1].Text[0];
                        }
                        if ((char)(f + 65) == l)
                        {

                            texte += "\t\t\t\t<tr>\n";
                            // voir
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"" + lvChoixIndividu.Items[ff].SubItems[0].Text + ".html\">\n";
                            texte += "\t\t\t\t\t\t</a>\n";
                            texte += "\t\t\t\t\t</td>\n";
                            // Nom
                            texte += "\t\t\t\t\t<td>\n";
                            texte += "\t\t\t\t\t\t" + lvChoixIndividu.Items[ff].SubItems[1].Text + "\n";
                            texte += "\t\t\t\t\t</td>\n";
                            // Naissance
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t" + Avoir_texte_date_PP(
                                "BIRT",
                                lvChoixIndividu.Items[ff].SubItems[6].Text
                                ) + "\n";
                            texte += "\t\t\t\t\t" + "</td>\n";
                            // Décès
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t" + Avoir_texte_date_PP(
                                "DEAT",
                                lvChoixIndividu.Items[ff].SubItems[7].Text
                                ) + "\n";
                            texte += "\t\t\t\t\t" + "</td>\n";

                            // ID
                            if (GH.GHClass.Para.voir_ID)
                            {
                                texte += "\t\t\t\t\t<td class=\"liste\">\n";
                                texte += "\t\t\t\t\t\t" + lvChoixIndividu.Items[ff].SubItems[0].Text + "\n";
                                texte += "\t\t\t\t\t</td>\n";
                            }

                            texte += "\t\t\t\t</tr>";
                        }
                        else if (f == 26 && !("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(l)))
                        {
                            texte += "\t\t\t\t<tr>\n";
                            // Voir
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"" + lvChoixIndividu.Items[ff].SubItems[0].Text + ".html\">\n";
                            texte += "\t\t\t\t\t\t</a>\n";
                            texte += "\t\t\t\t\t</td>\n";
                            // Nom
                            texte += "\t\t\t\t\t<td>B\n";
                            texte += "\t\t\t\t\t\t" + lvChoixIndividu.Items[ff].SubItems[1].Text + "\n";
                            texte += "\t\t\t\t\t</td>\n";
                            // Naissance
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t" + Avoir_texte_date_PP(
                                "BIRT",
                                lvChoixIndividu.Items[ff].SubItems[6].Text
                                ) + "\n";
                            texte += "\t\t\t\t\t" + "</td>\n";
                            // Décès
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t" + Avoir_texte_date_PP(
                                "DEAT",
                                lvChoixIndividu.Items[ff].SubItems[7].Text
                                ) + "\n";
                            texte += "\t\t\t\t\t\t" + lvChoixIndividu.Items[ff].SubItems[7].Text + "\n";
                            texte += "\t\t\t\t\t" + "</td>\n";
                            // ID
                            texte += "\t\t\t\t\t<td>\n";
                            texte += "\t\t\t\t\t\t" + lvChoixIndividu.Items[ff].SubItems[0].Text + "\n";
                            texte += "\t\t\t\t\t</td>\n";

                            texte += "\t\t\t\t</tr>";
                        }
                    }
                    texte += "\t\t\t</table>";
                    texte += Bas_Page();
                    try
                    {
                        if (f == 26)
                        {
                            System.IO.File.WriteAllText(@dossier_sortie + @"\individus\Diver.html", texte);
                            if (GH.GHClass.annuler)
                                return;
                        }
                        else
                        {
                            System.IO.File.WriteAllText(@dossier_sortie + @"\individus\" + (char)(f + 65) + ".html", texte);
                            if (GH.GHClass.annuler)
                                return;
                        }
                    }
                    catch (Exception msg)
                    {
                        GC.Collect();
                        R.Afficher_message("Ne peut pas écrire le fichier" + dossier_sortie + ".", msg.Message, GH.GHClass.erreur);
                    }
                }
            }
            GC.Collect();
        }
        public string
            Fiche_individu(string ID_individu, string dossier_sortie, bool menu // p.25 +2
        //, [CallerLineNumber] int callerLineNumber = 0
        )
        {
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            Regler_code_erreur();
            int nombre_source = 0;

            // avoir info individu
            GEDCOMClass.INDIVIDUAL_RECORD info_individu;
            (_, info_individu) = GEDCOMClass.Avoir_info_individu(ID_individu);
            if (info_individu == null)
                return null;
            Application.DoEvents();
            // creation des liste ID_numero pour créer les liens
            List<ID_numero> liste_SUBMITTER_ID_numero = new List<ID_numero>();

            // MULTIMEDIA_ID_numero
            List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero = new List<MULTIMEDIA_ID_numero>();
            MULTIMEDIA_ID_numero temp = new MULTIMEDIA_ID_numero();
            liste_MULTIMEDIA_ID_numero.Add(temp);

            List<ID_numero> liste_note_STRUCTURE_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_NOTE_RECORD_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_citation_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_source_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_repo_ID_numero = new List<ID_numero>();
            List<GEDCOMClass.PERSONAL_NAME_STRUCTURE> info_nom_liste;

            // Généré référence
            {
                // citation et source de l'individu
                if (GH.GHClass.Para.voir_reference)
                {
                    (liste_citation_ID_numero, _) = Verifier_liste(info_individu.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                    (liste_source_ID_numero, _) = Verifier_liste(info_individu.N1_SOUR_source_liste_ID, liste_source_ID_numero);
                }
                // liste de nom
                info_nom_liste = GEDCOMClass.Avoir_liste_nom_individu(ID_individu);
                if (info_nom_liste != null)
                {
                    (
                        liste_MULTIMEDIA_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero
                    ) =
                    Genere_liste_reference_nom(
                        info_nom_liste,
                        liste_MULTIMEDIA_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero
                        );
                    foreach (GEDCOM.GEDCOMClass.PERSONAL_NAME_STRUCTURE info_nom in info_nom_liste)
                    {
                        // citation sur le nom
                        if (GH.GHClass.Para.voir_reference)
                            if (info_nom.N1_PERSONAL_NAME_PIECES != null)
                                (liste_citation_ID_numero, _) = Verifier_liste(
                                    info_nom.N1_PERSONAL_NAME_PIECES.Nn_SOUR_citation_liste_ID,
                                    liste_citation_ID_numero);
                        // note sur le nom
                        if (GH.GHClass.Para.voir_note)
                            if (info_nom.N1_PERSONAL_NAME_PIECES != null)
                                (liste_note_STRUCTURE_ID_numero, _) = Verifier_liste(
                                    info_nom.N1_PERSONAL_NAME_PIECES.Nn_NOTE_STRUCTURE_liste_ID,
                                    liste_note_STRUCTURE_ID_numero);
                        // citatation de FONE
                        if (GH.GHClass.Para.voir_reference)
                            if (info_nom.N1_FONE_name_pieces != null)
                            {
                                (liste_citation_ID_numero, _) = Verifier_liste(info_nom.N1_FONE_name_pieces.Nn_SOUR_citation_liste_ID,
                                liste_citation_ID_numero);
                            }
                        // note sur le FONE
                        if (GH.GHClass.Para.voir_note)
                            if (info_nom.N1_FONE_name_pieces != null)
                                (liste_note_STRUCTURE_ID_numero, _) = Verifier_liste(info_nom.N1_FONE_name_pieces.Nn_NOTE_STRUCTURE_liste_ID,
                                    liste_note_STRUCTURE_ID_numero);
                        // citatation de ROMN
                        if (GH.GHClass.Para.voir_reference)
                            if (info_nom.N1_ROMN_name_pieces != null)
                                (liste_citation_ID_numero, _) = Verifier_liste(info_nom.N1_ROMN_name_pieces.Nn_SOUR_citation_liste_ID,
                                    liste_citation_ID_numero);
                        // note sur le ROMN
                        if (GH.GHClass.Para.voir_note)
                            if (info_nom.N1_ROMN_name_pieces != null)
                                (liste_note_STRUCTURE_ID_numero, _) = Verifier_liste(info_nom.N1_ROMN_name_pieces.Nn_NOTE_STRUCTURE_liste_ID,
                                    liste_note_STRUCTURE_ID_numero);
                    }
                }
                // association
                if (info_individu.N1_ASSO_liste != null)
                {
                    foreach (GEDCOMClass.ASSOCIATION_STRUCTURE info_ASSO in info_individu.N1_ASSO_liste)
                    {
                        // citation source
                        if (GH.GHClass.Para.voir_reference)
                            (liste_citation_ID_numero, _) = Verifier_liste(info_ASSO.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                        (liste_source_ID_numero, _) = Verifier_liste(info_ASSO.N1_SOUR_source_liste_ID, liste_source_ID_numero);
                        // note
                        if (GH.GHClass.Para.voir_note)
                            (liste_note_STRUCTURE_ID_numero, _) = Verifier_liste(info_ASSO.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                    }
                }
                // adresse
                if (info_individu.N1_ADDR_liste != null)
                {
                    foreach (GEDCOMClass.ADDRESS_STRUCTURE info_adresse_A in info_individu.N1_ADDR_liste)
                    {
                        if (info_adresse_A.N1_NOTE_STRUCTURE_liste_ID != null)
                            liste_NOTE_RECORD_ID_numero = Valider_liste_reference_note(info_adresse_A.N1_NOTE_STRUCTURE_liste_ID, liste_NOTE_RECORD_ID_numero);
                    }
                }
                // ANCI
                if (GH.GHClass.Para.voir_chercheur)
                    (liste_SUBMITTER_ID_numero, _) = Verifier_liste(info_individu.N1_ANCI_liste_ID, liste_SUBMITTER_ID_numero);
                // DESI
                if (GH.GHClass.Para.voir_chercheur)
                    (liste_SUBMITTER_ID_numero, _) = Verifier_liste(info_individu.N1_DESI_liste_ID, liste_SUBMITTER_ID_numero);
                // note individu
                if (GH.GHClass.Para.voir_note)
                    (liste_note_STRUCTURE_ID_numero, _) = Verifier_liste(info_individu.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                // Groupe conjoint
                // note conjoint
                Application.DoEvents();
                if (GH.GHClass.Para.voir_note && info_individu.N1_FAMS_liste_Conjoint != null)
                {
                    foreach (GEDCOMClass.SPOUSE_TO_FAMILY_LINK infoLienConjoint in info_individu.N1_FAMS_liste_Conjoint)
                        (liste_note_STRUCTURE_ID_numero, _) = Verifier_liste(infoLienConjoint.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                }

                // Groupe parent
                Application.DoEvents();
                GEDCOMClass.CHILD_TO_FAMILY_LINK info_famille =
                        GEDCOMClass.AvoirInfoFamilleEnfant(ID_individu);
                if (info_famille != null)
                {
                    if (info_famille.N2_ADOP_PLAC != null)
                    {
                        // citation PLAC
                        if (GH.GHClass.Para.voir_reference)
                            (liste_citation_ID_numero, _) = Verifier_liste(
                                info_famille.N2_ADOP_PLAC.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                        // note PLAC
                        if (GH.GHClass.Para.voir_note)
                            (liste_note_STRUCTURE_ID_numero, _) = Verifier_liste(info_famille.N2_ADOP_PLAC.N1_NOTE_STRUCTURE_liste_ID,
                                    liste_note_STRUCTURE_ID_numero);
                    }
                    // note famille
                    if (GH.GHClass.Para.voir_note)
                    {
                        GEDCOMClass.CHILD_TO_FAMILY_LINK infoFamille_A = GEDCOMClass.AvoirInfoFamilleEnfant(ID_individu);
                        (liste_note_STRUCTURE_ID_numero, _) = Verifier_liste(infoFamille_A.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                    }
                }
                // archive individu
                (liste_MULTIMEDIA_ID_numero, _) = Verifier_liste_MULTIMEDIA(info_individu.MULTIMEDIA_LINK_liste_ID, liste_MULTIMEDIA_ID_numero);

                // chercheur
                if (GH.GHClass.Para.voir_chercheur)
                    (liste_SUBMITTER_ID_numero, _) = Verifier_liste(info_individu.N1_SUBM_liste_ID, liste_SUBMITTER_ID_numero);

                // Groupe ordonnance
                if (info_individu.N1_LDS_liste != null)
                {
                    foreach (GEDCOMClass.LDS_INDIVIDUAL_ORDINANCE info_LDS in info_individu.N1_LDS_liste)
                    {
                        // citation d'ordonnance
                        if (GH.GHClass.Para.voir_reference)
                            (liste_citation_ID_numero, _) = Verifier_liste(info_LDS.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                        if (GH.GHClass.Para.voir_note)
                            // note d'ordonnance
                            (liste_note_STRUCTURE_ID_numero, _) = Verifier_liste(info_LDS.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                    }
                }
                // groupe evenement
                if (info_individu.N1_EVEN_Liste != null)
                {
                    foreach (GEDCOMClass.EVEN_ATTRIBUTE_STRUCTURE info_evenement in info_individu.N1_EVEN_Liste)
                    {
                        // citation evenement
                        if (GH.GHClass.Para.voir_reference)
                            (liste_citation_ID_numero, _) = Verifier_liste(info_evenement.N2_SOUR_citation_liste_ID, liste_citation_ID_numero);
                        // note PLAC
                        // if PLAC n'est pas null
                        if (info_evenement.N2_PLAC != null)
                        {
                            // note de PLAC
                            if (GH.GHClass.Para.voir_note)
                                (liste_note_STRUCTURE_ID_numero, _) = Verifier_liste(info_evenement.N2_PLAC.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                            // citation PLAC
                            if (GH.GHClass.Para.voir_reference)
                                (liste_citation_ID_numero, _) = Verifier_liste(info_evenement.N2_PLAC.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                        }
                        // note de l'adresse
                        // if adresse n'est pas null
                        if (info_evenement.N2_ADDR_liste != null)
                            if (GH.GHClass.Para.voir_note)
                                foreach (GEDCOMClass.ADDRESS_STRUCTURE info_adresse_B in info_evenement.N2_ADDR_liste)
                                {
                                    if (info_adresse_B.N1_NOTE_STRUCTURE_liste_ID != null)
                                        (liste_note_STRUCTURE_ID_numero, _) = Verifier_liste(info_adresse_B.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                                }
                        // note évènement
                        if (GH.GHClass.Para.voir_reference)
                            (liste_note_STRUCTURE_ID_numero, _) = Verifier_liste(info_evenement.N2_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                        // note de TEXT
                        if ((info_evenement.N2_TEXT_liste != null) && info_evenement.N2_TEXT_liste.Any())
                        {
                            foreach (GEDCOMClass.TEXT_STRUCTURE info_texte in info_evenement.N2_TEXT_liste)
                            {
                                if ((info_texte.N1_NOTE_STRUCTURE_liste_ID != null) && info_texte.N1_NOTE_STRUCTURE_liste_ID.Any())
                                {
                                    liste_NOTE_RECORD_ID_numero = Valider_liste_reference_note(info_texte.N1_NOTE_STRUCTURE_liste_ID, liste_NOTE_RECORD_ID_numero);
                                }
                            }
                        }
                        (liste_MULTIMEDIA_ID_numero, _) = Verifier_liste_MULTIMEDIA(info_evenement.MULTIMEDIA_LINK_liste_ID, liste_MULTIMEDIA_ID_numero);
                        // note changement
                        if (info_evenement.N2_CHAN != null)
                        {
                            liste_NOTE_RECORD_ID_numero = Valider_liste_reference_note(info_evenement.N2_CHAN.N1_CHAN_NOTE_STRUCTURE_ID_liste, liste_NOTE_RECORD_ID_numero);
                        }
                    }
                }
                // groupe attribut
                if (info_individu.N1_Attribute_liste != null)
                {
                    foreach (GEDCOMClass.EVEN_ATTRIBUTE_STRUCTURE info_attribut in info_individu.N1_Attribute_liste)
                    {
                        // citation attribut
                        if (GH.GHClass.Para.voir_reference)
                            (liste_citation_ID_numero, _) = Verifier_liste(info_attribut.N2_SOUR_citation_liste_ID, liste_citation_ID_numero);
                        // if PLAC n'est pas null
                        if (info_attribut.N2_PLAC != null)
                        {
                            // note PLAC
                            if (GH.GHClass.Para.voir_note)
                                (liste_note_STRUCTURE_ID_numero, _) = Verifier_liste(info_attribut.N2_PLAC.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                            // citation PLAC
                            if (GH.GHClass.Para.voir_reference)
                                (liste_citation_ID_numero, _) = Verifier_liste(info_attribut.N2_PLAC.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                        }
                        // si ADDR n'est pas null
                        if (info_attribut.N2_ADDR_liste != null)
                        {
                            // note de l'adresse
                            if (GH.GHClass.Para.voir_note)
                                foreach (GEDCOMClass.ADDRESS_STRUCTURE info_adresse_C in info_attribut.N2_ADDR_liste)
                                {
                                    if (info_adresse_C.N1_NOTE_STRUCTURE_liste_ID != null)
                                        (liste_note_STRUCTURE_ID_numero, _) = Verifier_liste(info_adresse_C.N1_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                                }
                        }
                        // note attribut
                        if (GH.GHClass.Para.voir_note)
                            (liste_note_STRUCTURE_ID_numero, _) = Verifier_liste(info_attribut.N2_NOTE_STRUCTURE_liste_ID, liste_note_STRUCTURE_ID_numero);
                        // media
                        (liste_MULTIMEDIA_ID_numero, _) = Verifier_liste_MULTIMEDIA(info_attribut.MULTIMEDIA_LINK_liste_ID, liste_MULTIMEDIA_ID_numero);
                    }
                }

                // CHAN
                (
                    liste_MULTIMEDIA_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    _
                ) =
                Genere_liste_reference_date
                    (
                    info_individu.N1_CHAN,
                    liste_MULTIMEDIA_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero
                    );

                // générer tout
                (
                    liste_SUBMITTER_ID_numero,
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    _
                ) =
                Genere_liste_reference_tous
                (
                    liste_SUBMITTER_ID_numero,
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero
                );
            }
            // FIN Généré référence
            int tab = 3;
            string espace = Tabulation(tab);
            string origine = @"../";
            string sous_dossier = "individus";
            if (R.IsNullOrEmpty(ID_individu))
                return null;
            bool groupe_archive;
            bool groupe_conjoint = false;
            bool groupe_parent = false;
            bool groupe_media = false;
            bool groupe_evenement;
            bool groupe_attribut;
            bool groupe_chercheur;
            bool groupe_ordonnance;
            bool groupe_citation = false;
            bool groupe_source = false;
            bool groupe_note = false;
            bool groupe_depot = false;
            int numero_source = 0;
            int numero_carte = 0;

            // Récupérer les informations de l'individu
            string nom = GEDCOMClass.Avoir_premier_nom_individu(ID_individu);
            info_nom_liste = GEDCOMClass.Avoir_liste_nom_individu(ID_individu);
            GEDCOMClass.EVEN_ATTRIBUTE_STRUCTURE NaissanceIndividu;
            (_, NaissanceIndividu) = GEDCOMClass.Avoir_evenement_naissance(info_individu.N1_EVEN_Liste);
            GEDCOMClass.EVEN_ATTRIBUTE_STRUCTURE DecesIndividu;
            (_, DecesIndividu) = GEDCOMClass.Avoir_evenement_deces(info_individu.N1_EVEN_Liste);
            string dateNaissance = NaissanceIndividu.N2_DATE;
            string dateDeces = DecesIndividu.N2_DATE;
            string age = null;
            if (dateNaissance != null && dateDeces != null)
            {
                age = Calculer_age(dateNaissance, dateDeces);
            }
            string nomFichierIndividu = dossier_sortie + @"\individus\" + ID_individu + ".html";
            if (!menu)
                nomFichierIndividu = dossier_sortie + @"\individus\page.html";
            if (File.Exists(nomFichierIndividu))
            {
                File.Delete(nomFichierIndividu);
            }
            string texte = null;
            texte += Haut_Page("../", menu, nom);
            // restriction
            if (info_individu.N1_RESN != null)
            {
                texte += "\t\t\t<div class=\"blink3\">\n";
                texte += "\t\t\t\tRestriction\n";
                texte += "\t\t\t\t" + Traduire_RESN(info_individu.N1_RESN) + "\n";
                texte += "\t\t\t</div>\n";
            }
            // groupe Individu
            {
                texte += espace + "<a id=\"groupe_nom\"></a>\n";
                texte += espace + "<!--**************************\n";
                texte += espace + "*  groupe Individu           *\n";
                texte += espace + "***************************-->\n";
                texte += Titre_groupe(null, nom, ID_individu, tab);
                // Tableau
                texte += Separation(5, null, "000", null, tab);
                texte += espace + "<div class=\"tableau\" >\n";
                // adopter
                if (info_individu.Adopter != null)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\" style=\"font-size:150%;\">\n";
                    texte += "\t\t\t\t\t\t<strong>Adopter</strong>\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // NAME
                texte += Avoir_texte_NAME(
                    info_nom_liste,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    4);
                // si allia
                if (info_individu.N1_ALIA_liste_ID != null)
                {

                    foreach (string ID in info_individu.N1_ALIA_liste_ID)
                    {
                        (bool Ok_nom_Alia, GEDCOMClass.INDIVIDUAL_RECORD alia_individu) = GEDCOMClass.Avoir_info_individu(ID);
                        string nom_ALIA = null;
                        nom_ALIA = GEDCOMClass.Avoir_premier_nom_individu(ID);
                        string txtID = null;
                        if (GH.GHClass.Para.voir_ID == true)
                        {
                            txtID = " [" + ID + "]";
                        }
                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                        texte += espace + "\t\t\tMême personne?\n";
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                        if (menu)
                        {
                            texte += "\t\t\t<a class=\"ficheIndividu\"  href=\"" + ID + ".html\"></a>\n";
                        }
                        else
                        {
                            texte += "\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                        }
                        texte += "\t\t\t" + nom_ALIA + txtID + "\n";
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t</div>\n";
                    }
                }
                // Sex
                if (info_individu.N1_SEX != null)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tSex (biologique)\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t" + Convertir_SEX(info_individu.N1_SEX) + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // SIGH 
                if (R.IsNotNullOrEmpty(info_individu.N1_SIGN))
                {
                    string signature = Traduire_yes_no(info_individu.N1_SIGN);
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tPeut signer\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t" + signature + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // filiation  Heridis
                if (info_individu.N1__CLS != null)
                {
                    if (info_individu.N1__CLS == "YES")
                    {
                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                        texte += "\t\t\tIndividu sans postérité\n";
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                        texte += "\t\t\tOui\n";
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t</div>\n";
                    }
                }
                // filiation  Heridis
                if (info_individu.N1__FIL != null)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += "\t\t\tFiliation\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += "\t\t\t" + info_individu.N1__FIL + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // filiation  Ancestrologie
                if (info_individu.N1_FILA != null)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tFiliation\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t" + info_individu.N1_FILA + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // Âge au décès
                if (age != null)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tÂge au décès\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t" + age + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // association
                texte += Avoir_texte_association(
                    info_individu.N1_ASSO_liste,
                    menu,
                    liste_SUBMITTER_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    tab + 1
                    );

                // si a un adresse V5.3
                string info_adresse = Avoir_texte_adresse(
                    info_individu.N1_SITE,
                    info_individu.N1_ADDR_liste,
                    info_individu.N1_PHON_liste,
                    info_individu.N1_FAX_liste,
                    info_individu.N1_EMAIL_liste,
                    info_individu.N1_WWW_liste,
                    sous_dossier,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    5);
                if (info_adresse != null)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tAdresse \n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += info_adresse;
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // RELI Appartenance religieuse
                if (info_individu.N1_NAMR != null)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tReligion \n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += Convertir_texte_html(info_individu.N1_NAMR, false, tab + 3);
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";

                    // Nom de la religion V5.3
                    if (info_individu.N2_NAMR_RELI != null)
                    {
                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                        texte += espace + "\t\t\t&nbsp;\n";
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                        texte += Convertir_texte_html(info_individu.N2_NAMR_RELI, false, tab + 3);
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t</div>\n";
                    }
                }
                // si N1__ANCES_CLE_FIXE
                if (info_individu.N1__ANCES_CLE_FIXE != null)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\tClé fixe (Ancestrologie) \n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // si REFN ******************************************************************************************************************************
                if (info_individu.N1_REFN_liste != null)
                {
                    foreach (GEDCOMClass.USER_REFERENCE_NUMBER info_REFN in info_individu.N1_REFN_liste)
                    {
                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                        texte += espace + "\t\t\tREFN\n";
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                        texte += espace + "\t\t\t" + info_REFN.N0_REFN;
                        if (info_REFN.N1_TYPE != null)
                        {
                            texte += " Type " + info_REFN.N1_TYPE + "\n";
                        }
                        else
                        {
                            texte += "\n";
                        }
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t</div>\n";
                    }
                }
                // si RIN ******************************************************************************************************************************
                if (info_individu.N1_RIN != null)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tRIN\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t" + info_individu.N1_RIN + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // si AFN *****************************************************************************************************
                if (info_individu.N1_AFN != null)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tAFN\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t" + info_individu.N1_AFN + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // si ANCI *****************************************************************************************************
                if ((info_individu.N1_ANCI_liste_ID != null) && info_individu.N1_ANCI_liste_ID.Any())
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tANCI\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += Avoir_texte_lien_chercheur(info_individu.N1_ANCI_liste_ID, liste_SUBMITTER_ID_numero, tab + 3);
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // si DESI *****************************************************************************************************
                if ((info_individu.N1_DESI_liste_ID != null) && info_individu.N1_DESI_liste_ID.Any())
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tDESI\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += Avoir_texte_lien_chercheur(info_individu.N1_DESI_liste_ID, liste_SUBMITTER_ID_numero, 5);
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // si RFN *****************************************************************************************************
                if (info_individu.N1_RFN != null)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                    texte += espace + "\t\t\tRFN\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += espace + "\t\t\t" + info_individu.N1_RFN + "\n";
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }
                // si WWW
                if (info_individu.N1_WWW_liste != null)
                {
                    foreach (string www in info_individu.N1_WWW_liste)
                    {
                        texte += espace + "\t<div class=\"tableau_ligne\">\n";
                        texte += espace + "\t\t<div class=\"tableau_col1\">\n";
                        texte += espace + "\t\t\tPage Web\n";
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                        texte += espace + "\t\t\t" + www + "\n";
                        texte += espace + "\t\t</div>\n";
                        texte += espace + "\t</div>\n";
                    }
                }
                // chercheur
                if (info_individu.N1_SUBM_liste_ID != null && GH.GHClass.Para.voir_chercheur)
                {
                    texte += espace + "\t<div class=\"tableau_ligne\">\n";
                    texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                    texte += Avoir_texte_lien_chercheur(
                        info_individu.N1_SUBM_liste_ID, liste_SUBMITTER_ID_numero, tab + 3);
                    texte += espace + "\t\t</div>\n";
                    texte += espace + "\t</div>\n";
                }

                // citation lien
                texte += Avoir_texte_lien_citation(
                    info_individu.N1_SOUR_citation_liste_ID,
                    liste_citation_ID_numero,
                    .04f,
                    tab + 1);
                // source lien
                texte += Avoir_texte_lien_source(
                    info_individu.N1_SOUR_source_liste_ID,
                    liste_source_ID_numero,
                    0.04f,
                    tab + 1);
                // note ********************************************************************************************************
                texte += Avoir_texte_NOTE_STRUCTURE(
                    info_individu.N1_NOTE_STRUCTURE_liste_ID,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    .04f, // retrait
                    tab + 1);
                // Date changement
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += Avoir_texte_date_Changement(
                    info_individu.N1_CHAN,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    false,
                    tab + 3
                    );
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
                texte += espace + "</div>\n";
            }
            // FIN groupe Individu

            // Groupe Conjoint
            {
                if ((info_individu.N1_FAMS_liste_Conjoint != null) && info_individu.N1_FAMS_liste_Conjoint.Any())
                {
                    groupe_conjoint = true;
                    texte += espace + "<a id=\"groupe_conjoint\"></a>\n";
                    texte += espace + "<!--**************************\n";
                    texte += espace + "*  groupe Conjoint           *\n";
                    texte += espace + "***************************-->\n";
                    // titre du groupe Conjoint
                    texte += Titre_groupe(origine + @"commun/conjoint.svg", "Conjoint", null, tab);
                    texte += Separation(5, null, "000", null, tab);
                    texte += espace + "<table class=\"tableau\">\n";
                    texte += espace + "\t<thead>\n";
                    texte += espace + "\t\t<tr>\n";
                    texte += espace + "\t\t\t<th class=\"cellule2LMB\" style=\"width:85px\">&nbsp;</th>\n";
                    texte += espace + "\t\t\t<th class=\"cellule2LMB\">Nom</th>\n";
                    texte += espace + "\t\t\t<th class=\"cellule2LMB date\">Naissance</th>\n";
                    texte += espace + "\t\t\t<th class=\"cellule2LMB date\">Décès</th>\n";
                    texte += espace + "\t\t</tr>\n";
                    texte += espace + "\t</thead>\n";
                    foreach (GEDCOMClass.SPOUSE_TO_FAMILY_LINK infoLienConjoint in info_individu.N1_FAMS_liste_Conjoint)
                    {

                        string IDFamilleConjoint = infoLienConjoint.N0_ID;
                        string txtIDFamille;
                        if (GH.GHClass.Para.voir_ID == true && IDFamilleConjoint != null)
                        {
                            txtIDFamille = " [" + IDFamilleConjoint + "]";
                        }
                        else
                        {
                            txtIDFamille = null;
                        }
                        GEDCOMClass.FAM_RECORD info_famille = GEDCOMClass.Avoir_info_famille(IDFamilleConjoint);
                        string IDConjoint = null;
                        if (info_famille != null)
                        {
                            if (info_famille.N1_HUSB == ID_individu)
                            {
                                IDConjoint = info_famille.N1_WIFE;
                            }
                            else
                            {
                                IDConjoint = info_famille.N1_HUSB;
                            }
                        }
                        (
                            string nom_conjoint,
                            string naissance_date_conjoint,
                            string naissance_lieu_conjoint,
                            string deces_date_conjoint,
                            string deces_lieu_conjoint
                        ) = GEDCOMClass.Avoir_nom_naissance_deces(IDConjoint);
                        texte += espace + "\t<tr>\n";
                        texte += espace + "\t\t<td class=\"cellule2LMF\">\n";
                        if (txtIDFamille != null)
                        {
                            if (menu)
                            {
                                texte += espace + "\t\t\t<a class=\"ficheFamille\"  href=../familles/" +
                                    infoLienConjoint.N0_ID + ".html></a>\n";
                            }
                            else
                            {
                                texte += espace + "\t\t\t<a class=\"ficheFamilleGris\"></a>\n";
                            }
                        }
                        else
                        {
                            texte += espace + "\t\t\t<a class=\"ficheFamilleGris\"></a>";
                        }
                        if (GH.GHClass.Para.voir_ID && infoLienConjoint.N0_ID != null)
                            texte += espace + "\t\t\t[" + infoLienConjoint.N0_ID + "]\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"cellule2LMF\">\n";
                        if (IDConjoint != null)
                        {
                            if (menu)
                            {
                                texte += espace + "\t\t\t<a class=\"ficheIndividu\"  href=../individus/" +
                                    IDConjoint + ".html></a>\n";
                            }
                            else
                            {
                                texte += espace + "\t\t\t<a class=\"ficheFamilleGris\"></a>\n";
                            }
                        }
                        else
                        {
                            texte += espace + "\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                        }
                        texte += espace + "\t\t\t" + nom_conjoint + "\n";
                        if (GH.GHClass.Para.voir_ID == true && IDConjoint != null)
                        {
                            texte += espace + "\t\t\t<span style=\"font-size:small\">C[" +
                                IDConjoint + "]</span>" + "\n";
                        }
                        if ((infoLienConjoint.N1_NOTE_STRUCTURE_liste_ID != null) && infoLienConjoint.N1_NOTE_STRUCTURE_liste_ID.Any())
                        {
                            texte += Avoir_texte_NOTE_STRUCTURE(
                                infoLienConjoint.N1_NOTE_STRUCTURE_liste_ID,
                                liste_citation_ID_numero,
                                liste_source_ID_numero,
                                liste_note_STRUCTURE_ID_numero,
                                liste_NOTE_RECORD_ID_numero,
                                0, // retrait
                                tab + 7);
                        }
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"cellule2LMF\">\n";
                        texte += espace + "\t\t\t" + Avoir_texte_date_PP(
                            "BIRT",
                            naissance_date_conjoint
                            ) + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"cellule2LMF\">\n";

                        texte += espace + "\t\t\t" + Avoir_texte_date_PP(
                            "DEAT",
                            deces_date_conjoint
                            ) + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    texte += espace + "</table>\n";
                }
            }

            // FIN Groupe Conjoint

            // Groupe Parent
            {
                GEDCOMClass.CHILD_TO_FAMILY_LINK infoFamille = GEDCOMClass.AvoirInfoFamilleEnfant(ID_individu);
                if (infoFamille != null)
                {
                    if (
                        infoFamille.N0_FAMC != null ||
                        infoFamille.N1_PEDI != null ||
                        infoFamille.N1_STAT != null ||
                        infoFamille.N1_NOTE_STRUCTURE_liste_ID != null
                        )
                    {
                        groupe_parent = true;
                        // père
                        string pere_ID = GEDCOMClass.Avoir_famille_conjoint_ID(infoFamille.N0_FAMC);
                        (
                            string nom_pere,
                            string naissance_date_pere,
                            _, // string naissance_lieu_pere,
                            string deces_date_pere,
                            _ // string deces_lieu_pere
                        ) = GEDCOMClass.Avoir_nom_naissance_deces(pere_ID);
                        // mère
                        string mere_ID = GEDCOMClass.Avoir_famille_conjointe_ID(infoFamille.N0_FAMC);
                        (
                            string nom_mere,
                            string naissance_date_mere,
                            _, //string naissance_lieu_mere,
                            string deces_date_mere,
                            _ //string deces_lieu_mere
                        ) = GEDCOMClass.Avoir_nom_naissance_deces(mere_ID);
                        //GEDCOMClass.INDIVIDUAL_RECORD infoMere;
                        // afficher ou pas le ID
                        string txtIDPere = null;
                        string txtIDMere = null;
                        if (GH.GHClass.Para.voir_ID == true)
                        {
                            if (pere_ID != null && nom_pere != null)
                            {
                                txtIDPere = " [" + pere_ID + "]";
                            }
                            if (mere_ID != null && nom_mere != null)
                            {
                                txtIDMere = " [" + mere_ID + "]";
                            }
                        }
                        texte += espace + "<a id=\"groupe_parent\"></a>\n";
                        texte += espace + "<!--**************************\n";
                        texte += espace + "*  groupe Parent             *\n";
                        texte += espace + "***************************-->\n";
                        // titre du groupe
                        texte += Titre_groupe(origine + @"commun/parent.svg", "Parent", infoFamille.N0_FAMC, tab);
                        texte += Separation(5, null, "000", null, tab);
                        texte += espace + "<table class=\"tableau\">\n";
                        texte += espace + "\t<thead>\n";
                        texte += espace + "\t\t<tr>\n";
                        texte += espace + "\t\t\t<th class=\"cellule2LMB\" style=\"width:25px\">\n";
                        if (infoFamille.N0_FAMC != null)
                        {
                            if (menu)
                            {
                                texte += espace + "\t\t\t\t<a class=\"ficheFamille\"  href=\"../familles/" +
                                    infoFamille.N0_FAMC + ".html\"></a>\n";
                            }
                            else
                            {
                                texte += espace + "\t\t\t\t<a class=\"ficheFamilleGris\"></a>\n";
                            }
                        }
                        else
                        {
                            texte += espace + "\t\t\t\t<a class=\"ficheFamilleGris\"></a>\n";
                        }
                        texte += espace + "\t\t\t</th>\n";
                        texte += espace + "\t\t\t<th class=\"cellule2LMB\">Nom</th>\n";
                        texte += espace + "\t\t\t<th class=\"date cellule2LMB\">Naissance</th>\n";
                        texte += espace + "\t\t\t<th class=\"date cellule2LMB\">Décès</th>\n";
                        texte += espace + "\t\t</tr>\n";
                        texte += espace + "\t</thead>\n";
                        texte += espace + "\t<tr>\n";
                        texte += espace + "\t\t<td class=\"cellule2LMF\">\n";
                        // pere
                        texte += espace + "\t\t\tPère\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"cellule2LMF\">\n";
                        if (pere_ID != null)
                        {
                            if (menu)
                            {
                                texte += espace + "\t\t\t<a class=\"ficheIndividu\"  href=\"" + pere_ID + ".html\"></a>\n";
                            }
                            else
                            {
                                texte += espace + "\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                            }
                        }
                        else
                        {
                            texte += espace + "\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                        }
                        texte += espace + "\t\t\t" + nom_pere + txtIDPere + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"date cellule2LMF\">\n";
                        texte += espace + "\t\t\t" + Avoir_texte_date_PP(
                            "BIRT",
                            naissance_date_pere
                            ) + "\n";

                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"date cellule2LMF\">\n";
                        texte += espace + "\t\t\t" + Avoir_texte_date_PP(
                            "DEAT",
                            deces_date_pere
                            ) + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                        texte += espace + "\t<tr>\n";
                        texte += espace + "\t\t<td class=\"cellule2LMF\">\n";
                        texte += espace + "\t\t\tMère\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"cellule2LMF\">\n";
                        if (mere_ID != null)
                        {
                            if (menu)
                            {
                                texte += espace + "\t\t\t<a class=\"ficheIndividu\"  href=\"" +
                                    mere_ID + ".html\"></a>\n";
                            }
                            else
                            {
                                texte += espace + "\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                            }
                        }
                        else
                        {
                            texte += espace + "\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                        }
                        texte += espace + "\t\t\t" + nom_mere + txtIDMere + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"date cellule2LMF\">\n";
                        texte += espace + "\t\t\t" + Avoir_texte_date_PP(
                            "BIRT",
                            naissance_date_mere
                            ) + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"date cellule2LMF\">\n";
                        texte += espace + "\t\t\t" + Avoir_texte_date_PP(
                            "DEAT",
                            deces_date_mere
                            ) + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                        texte += espace + "</table>\n";
                        string texte_bloc = null;
                        if (infoFamille.N1_PEDI != null ||
                            infoFamille.N1_STAT != null ||
                            infoFamille.N1_NOTE_STRUCTURE_liste_ID != null ||
                            infoFamille.N1_adop ||
                            infoFamille.N1_slgc)
                        {
                            // pédigrée
                            if (infoFamille.N1_PEDI != null)
                            {
                                string pedi = null;
                                switch (infoFamille.N1_PEDI.ToLower())
                                {
                                    case "adopted":
                                        pedi = "Indique les parents adoptifs.";
                                        break;
                                    case "birth":
                                        pedi = "Indique les parents biologiques";
                                        break;
                                    case "foster":
                                        pedi = "Indique que l'enfant faisait partie d'une famille d'accueil ou d'une famille tutrice.";
                                        break;
                                    case "sealing":
                                        pedi = "indique que l'enfant a été scellé à des parents autres que les parents biologiques.";
                                        break;
                                }
                                texte_bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte_bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte_bloc += espace + "\t\t\tPédigrée\n";
                                texte_bloc += espace + "\t\t</div>\n";
                                texte_bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte_bloc += espace + "\t\t\t" + pedi + "\n";
                                texte_bloc += espace + "\t\t</div>\n";
                                texte_bloc += espace + "\t</div>\n";
                            }
                            // status
                            if (infoFamille.N1_STAT != null)
                            {
                                string stat = null;
                                switch (infoFamille.N1_STAT.ToLower())
                                {
                                    case "challenged":
                                        stat = "Lier cet enfant à cette famille est suspect, mais le lien n’a été ni prouvé ni réfuté.";
                                        break;
                                    case "disproven":
                                        stat = "Certains prétendent que cet enfant appartient à cette famille, mais le lien a été réfutée.";
                                        break;
                                    case "proven":
                                        stat = "Certains prétendent que cet enfant n'appartient pas à cette famille, mais le lien a été prouvé.";
                                        break;
                                }
                                texte_bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte_bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte_bloc += espace + "\t\t\tStatus\n";
                                texte_bloc += espace + "\t\t</div>\n";
                                texte_bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte_bloc += espace + "\t\t\t" + stat + "\n";
                                texte_bloc += espace + "\t\t</div>\n";
                                texte_bloc += espace + "\t</div>\n";
                            }
                            // CHILD_FAMILY_EVEN V5.3 ADOP
                            if (infoFamille.N1_adop)
                            {
                                texte_bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                                texte_bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                                texte_bloc += espace + "\t\t\tAdoption\n";
                                texte_bloc += espace + "\t\t</div>\n";
                                texte_bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                                texte_bloc += espace + "\t\t\tType " + infoFamille.N2_ADOP_TYPE + "\n";
                                texte_bloc += espace + "\t\t</div>\n";
                                texte_bloc += espace + "\t</div>\n";
                                if (infoFamille.N2_ADOP_AGE != null)
                                {
                                    texte_bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                                    texte_bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                                    texte_bloc += espace + "\t\t\t&nbsp;\n";
                                    texte_bloc += espace + "\t\t</div>\n";
                                    texte_bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                                    texte_bloc += espace + "\t\t\tÂge " + infoFamille.N2_ADOP_AGE + "\n";
                                    texte_bloc += espace + "\t\t</div>\n";
                                    texte_bloc += espace + "\t</div>\n";
                                }
                                if (infoFamille.N2_ADOP_DATE != null)
                                {
                                    texte_bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                                    texte_bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                                    texte_bloc += espace + "\t\t\t&nbsp; \n";
                                    texte_bloc += espace + "\t\t</div>\n";
                                    texte_bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                                    texte_bloc += espace + "\t\t\tDate" + infoFamille.N2_ADOP_DATE + "\n";
                                    texte_bloc += espace + "\t\t</div>\n";
                                    texte_bloc += espace + "\t</div>\n";
                                }
                                if (infoFamille.N2_ADOP_PLAC != null)
                                {
                                    texte_bloc += espace + "\t<div class=\"tableau_ligne\">\n";
                                    texte_bloc += espace + "\t\t<div class=\"tableau_col1\">\n";
                                    texte_bloc += espace + "\t\t\tLieu\n";
                                    texte_bloc += espace + "\t\t</div>\n";
                                    texte_bloc += espace + "\t\t<div class=\"tableau_colW\">\n";
                                    string texte_place;
                                    (texte_place, numero_carte, numero_source) = Avoir_texte_PLAC(
                                        infoFamille.N2_ADOP_PLAC,
                                        sous_dossier,
                                        numero_carte,
                                        numero_source,
                                        liste_citation_ID_numero,
                                        liste_source_ID_numero,
                                        liste_note_STRUCTURE_ID_numero,
                                        liste_NOTE_RECORD_ID_numero,
                                        4);
                                    texte_bloc += espace + texte_place;
                                    texte_bloc += espace + "\t\t</div>\n";
                                    texte_bloc += espace + "\t</div>\n";
                                }
                            }
                            // note
                            if (infoFamille.N1_NOTE_STRUCTURE_liste_ID != null)
                            {
                                texte_bloc += Avoir_texte_NOTE_STRUCTURE(
                                    infoFamille.N1_NOTE_STRUCTURE_liste_ID,
                                    liste_citation_ID_numero,
                                    liste_source_ID_numero,
                                    liste_note_STRUCTURE_ID_numero,
                                    liste_NOTE_RECORD_ID_numero,
                                    0, // retrait
                                    tab);
                            }
                            if (texte_bloc != null)
                            {
                                if (texte_bloc != "")
                                {
                                    texte += Separation(3, null, "000", null, tab);
                                    // Tableau
                                    texte += espace + "<div class=\"tableau\">\n";
                                    texte += texte_bloc;
                                    // fin tableau
                                    texte += espace + "</div>\n";
                                }
                            }
                        }
                    }
                }
            }
            // FIN Groupe Parent

            // groupe Archive
            {
                string texte_archive;
                (texte_archive, groupe_archive) = Groupe_archive(
                    info_individu.MULTIMEDIA_LINK_liste_ID,
                    sous_dossier,
                    dossier_sortie,
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    tab + 1
                    );
                texte += texte_archive;
            }
            // FIN groupe Archive

            // Groupe ordonnance
            {
                string texte_ordo;
                (texte_ordo, numero_carte, numero_source, groupe_ordonnance) = Groupe_ordonnance(
                    info_individu.N1_LDS_liste,
                    dateNaissance,
                    numero_carte,
                    numero_source,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    3);
                texte += texte_ordo;
            }
            // FIN Groupe ordonnance

            // Groupe évènement
            {
                string texte_even;
                (texte_even, numero_carte, numero_source, groupe_evenement) = Groupe_evenement(
                        info_individu.N1_EVEN_Liste,
                        dateNaissance,
                        dateDeces,
                        "",
                        "",
                        sous_dossier,
                        dossier_sortie,
                        menu,
                        numero_carte,
                        numero_source,
                        liste_MULTIMEDIA_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        3);
                texte += texte_even;
            }

            // Groupe attribut
            {
                string texte_attr;
                (texte_attr, _, _, groupe_attribut) = Groupe_attribut(
                        info_individu.N1_Attribute_liste,
                        sous_dossier,
                        dossier_sortie,
                        menu,
                        numero_carte,
                        numero_source,
                        liste_MULTIMEDIA_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        3);
                texte += texte_attr;
            }
            // FIN Groupe attribut

            // Groupe chercheur
            {
                string texte_cher;
                (texte_cher, groupe_chercheur) =
                    Groupe_chercheur(
                    liste_SUBMITTER_ID_numero,
                    "individus",
                    dossier_sortie,
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    3);
                texte += texte_cher;
            }
            // FIN Groupe chercheur

            // Groupe citation
            {
                if (GH.GHClass.Para.voir_reference && liste_citation_ID_numero.Count > 0)
                {
                    // Générer le texte HTML
                    string texte_cita;
                    (texte_cita, groupe_citation) =
                            Groupe_citation(
                            "individus",
                            dossier_sortie,
                            liste_MULTIMEDIA_ID_numero,
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_repo_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero,
                            3);
                    texte += texte_cita;
                }
            }
            // FIN Groupe citation

            // Groupe source
            {
                if (GH.GHClass.Para.voir_reference && liste_source_ID_numero.Count() > 0)
                {
                    // Générer le texte HTML
                    string texte_sour;
                    (texte_sour, _, _, groupe_source) = Groupe_source(
                            "individus",
                            dossier_sortie,
                            menu,
                            numero_carte,
                            numero_source,
                            nombre_source,
                            liste_SUBMITTER_ID_numero,
                            liste_MULTIMEDIA_ID_numero,
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_repo_ID_numero,
                            liste_note_STRUCTURE_ID_numero,
                            liste_NOTE_RECORD_ID_numero,
                            3);
                    texte += texte_sour;
                }
            }
            // FIN Groupe source

            // Groupe depot
            {
                if (GH.GHClass.Para.voir_reference && liste_repo_ID_numero.Count > 0)
                {

                    string texte_depo;
                    (texte_depo, groupe_depot) = Groupe_depot(
                        sous_dossier,
                        dossier_sortie,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        3);
                    texte += texte_depo;
                }
            }
            // FIN Groupe depot

            // Groupe note
            {
                if (liste_NOTE_RECORD_ID_numero.Count() > 0)
                {
                    string texte_note;
                    (texte_note, groupe_note) = Groupe_NOTE_RECORD(
                        "individu",
                        dossier_sortie,
                        menu,
                        numero_carte,
                        numero_source,
                        nombre_source,
                        liste_SUBMITTER_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        liste_note_STRUCTURE_ID_numero,
                        liste_NOTE_RECORD_ID_numero,
                        3);
                    texte += texte_note;
                }
            }
            // FIN Groupe note

            // menu hamburger
            texte += Avoir_texte_menu_hamburger(
                origine,
                groupe_archive,
                groupe_attribut,
                groupe_chercheur,
                groupe_citation,
                groupe_conjoint,
                groupe_depot,
                false,
                groupe_evenement,
                false,
                false,
                false,
                groupe_media,
                groupe_note,
                groupe_ordonnance,
                groupe_parent,
                groupe_source
                );

            // bas de page
            texte += Bas_Page();
            try
            {
                Regler_code_erreur();
                System.IO.File.WriteAllText(nomFichierIndividu, texte);
            }
            catch (Exception msg)
            {
                R.Afficher_message("Ne peut pas écrire le fichier" + nomFichierIndividu + ".", msg.Message, GH.GHClass.erreur);
            }
            GC.Collect();
            return nom;
        }

        public string Texte_bouton_index_individu()
        {
            Regler_code_erreur();
            string texte = null;
            bool[] alphabette = new bool[27];
            ListView lvChoixIndividu = Application.OpenForms["GHClass"].Controls["lvChoixIndividu"] as ListView;
            for (int f = 0; f < 27; f++)
            {
                alphabette[f] = false;
            }
            // détermine si lettre est utiliser
            for (int f = 0; f < lvChoixIndividu.Items.Count; f++)
            {
                int v;
                if (lvChoixIndividu.Items[f].SubItems[1].Text != "")
                {
                    if (lvChoixIndividu.Items[f].SubItems[1].Text[0].ToString() != "")
                    {
                        char l = lvChoixIndividu.Items[f].SubItems[1].Text[0];
                        v = (int)l - 65;
                        if (v > -1 && v < 26)
                        {
                            alphabette[v] = true;
                        }
                        else
                        {
                            v = 26;
                            alphabette[v] = true;
                        }
                    }
                    else
                    {
                        v = 26;
                        alphabette[v] = true;
                    }
                }
                else
                {
                    v = 26;
                    alphabette[v] = true;
                }
            }
            texte += "\t\t\t<div style=\"width:700px;border-radius:10px;background-color:#6464FF;margin-left:auto;margin-right:auto;padding:5px;\">\n";
            for (int f = 0; f < 26; f++)
            {
                if (alphabette[f])
                {
                    texte += "\t\t\t\t<a class=\"index\" href=\"" + (char)(f + 65) + ".html\" >\n";
                    texte += "\t\t\t\t\t" + (char)(f + 65) + "\n";
                    texte += "\t\t\t\t</a>\n";
                }
                else
                {
                    texte += "\t\t\t\t<div class=\" index\">\n";
                    texte += "\t\t\t\t\t" + (char)(f + 65) + "\n";
                    texte += "\t\t\t\t</div>\n";
                }
            }
            if (alphabette[26])
            {
                texte += "\t\t\t\t<a class=\"index\" href=\"diver.html\" >\n";
                texte += "\t\t\t\t&\n";
                texte += "\t\t\t\t</a>\n";
            }
            else
            {
                texte += "\t\t\t\t<div class=\" index\">\n";
                texte += "\t\t\t\t\t&\n";
                texte += "\t\t\t\t</div>\n";
            }
            texte += "\t\t\t</div>\n";
            GC.Collect();
            return texte;
        }

        private (string, bool) Groupe_citation(
            string sous_dossier,
            string dossier_sortie,
            List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,

            int tab = 0)
        {
            string origine = @"../";
            if (sous_dossier == "")
                origine = "";
            Regler_code_erreur();
            try
            {
                if (R.IsNullOrEmpty(liste_citation_ID_numero))
                    return ("", false);
                string espace = Tabulation(tab);
                string bloc = Avoir_texte_citation(
                sous_dossier,
                dossier_sortie,
                liste_MULTIMEDIA_ID_numero,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_repo_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero,
                tab);
                if (bloc != null)
                {
                    string texte = espace + "<a id=\"groupe_citation\"></a>\n";
                    texte += espace + "<!--**************************\n";
                    texte += espace + "*  groupe Citation           *\n";
                    texte += espace + "***************************-->\n";
                    // titre du groupe
                    string titre = "Citation";
                    if ((liste_citation_ID_numero != null) && liste_citation_ID_numero.Any())
                        titre += "s";
                    //titre
                    texte += Titre_groupe(origine + @"commun/citation.svg", titre, null, tab);
                    texte += bloc;
                    GC.Collect();
                    return (texte, true);
                }
                GC.Collect();
                return (null, false);
            }
            catch (Exception msg)
            {
                GC.Collect();
                R.Afficher_message("La section Citation sera manquante pour cet infdividu.", msg.Message, GH.GHClass.erreur);
                GC.Collect();
                return (null, true);
            }
        }
        private (string, bool)
            Groupe_NOTE_RECORD(
            string sous_dossier,
            string dossier_sortie,
            bool menu,
            int numero_carte,
            int numero_source,
            int nombre_source,
            List<ID_numero> liste_SUBMITTER_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab = 0)
        {
            if (R.IsNullOrEmpty(liste_NOTE_RECORD_ID_numero))
                return (null, false);
            string texte = null;
            string origine = @"../";
            if (sous_dossier == "")
                origine = "";
            string espace = Tabulation(tab);
            string bloc = Avoir_texte_NOTE_RECORD(
                sous_dossier,
                dossier_sortie,
                menu,
                numero_carte,
                numero_source,
                nombre_source,
                liste_SUBMITTER_ID_numero,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_repo_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero,
                tab
                );
            if (bloc != null)
            {
                texte += espace + "<a id=\"groupe_note\"></a>";
                texte += espace + "<!--**************************\n";
                texte += espace + "*  groupe Note RECORD        *\n";
                texte += espace + "***************************-->\n";
                // titre du groupe
                string titre = "Note";
                if ((liste_NOTE_RECORD_ID_numero != null) && liste_NOTE_RECORD_ID_numero.Any())
                    titre += "s";
                texte += Titre_groupe(origine + @"commun/note.svg", titre, null, tab);
                texte += bloc;
                return (texte, true);
            }
            GC.Collect();
            return (texte, true);
        }

        private (string, int, int, bool) Groupe_source(
            string sous_dossier,
            string dossier_sortie,
            bool menu,
            int numero_carte,
            int numero_source,
            int nombre_source,
            List<ID_numero> liste_SUBMITTER_ID_numero,
            List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            string origine = @"../";
            if (sous_dossier == "")
                origine = "";
            if (R.IsNullOrEmpty(liste_source_ID_numero))
                return (
                    "",
                    numero_carte,
                    numero_source,
                    false);
            string espace = Tabulation(tab);
            string texte = null;
            string bloc_1 = null;
            string bloc_2 = null;
            if ((liste_source_ID_numero != null) && liste_source_ID_numero.Any())
            {
                bloc_1 += Avoir_texte_source(
                    //liste_source_ID_numero,
                    sous_dossier,
                    dossier_sortie,
                    menu,
                    numero_carte,
                    numero_source,
                    nombre_source,
                    liste_SUBMITTER_ID_numero,
                    liste_MULTIMEDIA_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    liste_note_STRUCTURE_ID_numero,
                    liste_NOTE_RECORD_ID_numero,
                    tab);
            }
            if (bloc_1 != null || bloc_2 != null)
            {
                texte += espace + "<a id=\"groupe_source\"></a>\n";
                texte += espace + "<!--**************************\n";
                texte += espace + "<!--**************************\n";
                texte += espace + "*  groupe Source          *\n";
                texte += espace + "***************************-->\n";
                // titre du groupe
                string titre = "Source";
                if ((liste_source_ID_numero != null) && liste_source_ID_numero.Any())
                    titre += "s";
                texte += Titre_groupe(origine + @"commun/livre.svg", titre, null, tab);
                texte += bloc_1;
                texte += bloc_2;
                GC.Collect();
                return (
                    texte,
                    numero_carte,
                    numero_source,
                    true);
            }
            GC.Collect();
            return (
                null,
                numero_carte,
                numero_source,
                false);
        }

        private (string, bool) Groupe_chercheur(
            List<ID_numero> liste_ID,
            string sous_dossier,
            string dossier_sortie,
            List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            List<ID_numero> liste_note_STRUCTURE_ID_numero,
            List<ID_numero> liste_NOTE_RECORD_ID_numero,
            int tab
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            /*// debug >
            R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name +
                "</b> à la ligne " + callerLineNumber + " sous_sossier [" + sous_dossier + "] dossier_sortie=[" + dossier_sortie + "]");
            string a = "Liste ID | ";
            foreach (ID_numero b in liste_ID)
                a += " N= " + b.numero + " ID=" + b.ID + " | ";
            R..Z(a);
            // < debug*/
            if (GH.GHClass.Para.voir_chercheur == false)
                return (null, false);
            if (R.IsNullOrEmpty(liste_ID))
                return (null, false);
            string origine = @"../";
            if (sous_dossier == "")
                origine = "";
            string espace = Tabulation(tab);
            string texte = null;
            texte += espace + "<a id=\"groupe_chercheur\"></a>\n";
            texte += espace + "<!--**************************\n";
            texte += espace + "*  groupe Chercheur          *\n";
            texte += espace + "***************************-->\n";
            string titre = "Chercheur";
            if (liste_ID.Count >= 1)
                titre += "s";
            texte += Titre_groupe(origine + @"commun/tete.svg", titre, null, tab);

            // index des événements
            if (liste_ID.Count() > 1)
            {
                texte += Separation(5, null, "000", null, tab);
                texte += espace + "<div class=\"tableau evenement-index \" >\n";
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_tete\">\n";
                texte += espace + "\t\t\t<strong style=\"font-size:150%\">\n";
                texte += espace + "\t\t\t\tIndex\n";
                texte += espace + "\t\t\t</strong>\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
                foreach (ID_numero item in liste_ID)
                {
                    string nom = GEDCOMClass.Avoir_premier_nom_chercheur(item.ID);
                    string lien_chercheur = Avoir_nom_lien_chercheur(nom, item.ID);
                    texte += espace + "\t<table class=\"index_evenement\">\n";
                    texte += espace + "\t\t<tr>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t<button>\n";
                    texte += espace + "\t\t\t\t\t<a href=\"#" + lien_chercheur + "\">\n";
                    texte += espace + "\t\t\t\t\t\t<img style=\"height:25px\" src= \"" + origine + "commun/go_evenement.svg\" alt=\"\" />\n";
                    texte += espace + "\t\t\t\t\t</a>\n";
                    texte += espace + "\t\t\t\t</button>\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t\t" + nom + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t</tr>\n";
                    texte += espace + "\t</table>\n";
                }
                texte += espace + "</div>";
            }
            // FIN index des événements

            string bloc = Avoir_texte_chercheur(
                liste_ID,
                sous_dossier,
                dossier_sortie,
                liste_MULTIMEDIA_ID_numero,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_repo_ID_numero,
                liste_note_STRUCTURE_ID_numero,
                liste_NOTE_RECORD_ID_numero,
                tab);
            if (R.IsNullOrEmpty(bloc))
            {

                // Tableau
                texte += Separation(5, null, "000", null, tab);
                texte += espace + "<div class=\"tableau\">\n";
                texte += espace + "\t<div class=\"tableau_ligne\">\n";
                texte += espace + "\t\t<div class=\"tableau_colW\">\n";
                texte += espace + "\t\t\tInformation manquante dans le fichier GEDCOM\n";
                texte += espace + "\t\t</div>\n";
                texte += espace + "\t</div>\n";
                // fin tableau
                texte += espace + "</div>\n";
            }
            else
            {
                texte += bloc;
            }
            GC.Collect();
            return (texte, true);
        }
        private static string Retiter_espace_extrat(string longString)
        {
            if (R.IsNullOrEmpty(longString))
                return null;
            StringBuilder sb = new StringBuilder();
            bool lastWasSpace = true; // True to eliminate leading spaces

            for (int i = 0; i < longString.Length; i++)
            {
                if (Char.IsWhiteSpace(longString[i]) && lastWasSpace)
                {
                    continue;
                }

                lastWasSpace = Char.IsWhiteSpace(longString[i]);

                sb.Append(longString[i]);
            }

            // The last character might be a space
            if (Char.IsWhiteSpace(sb[sb.Length - 1]))
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }

        private static string Remplacer_espace_html(string s
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            string texte = null;
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            // retire espace avant et après s
            s = s.Trim();
            // remplace les espace par &nbsp; sauf la première espace d'un groupe d'espace
            // &nbsp; est le code HTML pour un espace insécable
            string[] section = s.Split(' ');
            for (int f = 1; f < section.Count(); f++)
            {
                if ((section[f] == "") && (section[f - 1] == "" || section[f - 1] == "&nbsp;"))
                {
                    section[f] = "&nbsp;";
                }
            }
            for (int f = 1; f < section.Count(); f++)
            {
                if (section[f] != "" && section[f] != "&nbsp;" && section[f] != "&nbsp;")
                {
                    if (f != 0)
                    {
                        section[f] = " " + section[f];
                    }
                }
                else
                {
                    if (section[f] == "")
                        section[f] = "&nbsp;";
                }
            }
            foreach (string a in section)
                texte += a;
            return texte;
        }

        private static string Separation
            (
                int hauteur = 15,
                string s = null,
                string couleur = null,
                string arriere_plan = null,
                int tab = 0
            )
        {
            string espace = Tabulation(tab);
            string texte = null;
            texte += espace + "<!-- *** Séparation *** -->\n";
            texte += espace + "<div class=\"separation\" style =\"";
            if (R.IsNotNullOrEmpty(couleur))
                texte += "color:#" + couleur + ";";
            if (R.IsNotNullOrEmpty(arriere_plan))
                texte += "background-color:#" + couleur + ";";
            texte += "height:" + hauteur + "px;\">" + s + "\n";
            texte += espace + "</div>\n";
            return texte;
        }

        private static bool Si_bapteme(string balise)
        {
            // retourne  true si relier à un bapteme
            switch (balise)
            {
                case "BAPM":
                    return true; // The event of baptism (not LDS), performed in infancy or later. (See also BAPL and CHR).
                case "BARM":
                    return true; // The ceremonial event held when a Jewish boy reaches age 13.
                case "BASM":
                    return true; // The ceremonial event held when a Jewish girl reaches age 12, also known as "Bat Mitzvah"
                case "CHR":
                    return true; // The religious event (not LDS) of baptizing and/or naming a child.
                case "CHRA":
                    return true; // The religious event (not LDS) of baptizing and/or naming an adult person.
                case "GODP":
                    return true; // A sponsor at a religious rite(baptism).
            }
            return false;
        }
        private static bool Si_citoyen(string balise)
        {
            switch (balise)
            {
                case "NATI":
                    return true; // The nationality of an individual.
                case "NATU":
                    return true; //The event of obtaining citizenship.
            }
            return false;
        }
        private static bool Si_deceder(string balise)
        {
            if (balise == "DEAT") // The event when mortal life terminates.
                return true;
            return false;
        }
        private static bool Si_inhumer(string balise)
        {
            switch (balise)
            {
                case "BURI":
                    return true;  // The event of the proper disposing of the mortal remains of a deceased person.
                case "CEME":
                    return true; // The name of the cemetery or other resting place where an individual is buried.
                case "CREM":
                    return true; // Disposal of the remains of a person's body by fire.
            }
            return false;
        }

        private static bool Si_ordonnance(string balise)
        {
            // retourne  true si relier à l' ordonnance
            switch (balise)
            {
                case "BAPL":
                    return true; // {BAPTISM-LDS}:= The event of baptism performed at age eight or later by priesthood authority of The Church of Jesus Christ of Latter-day Saints. (See also BAPM.)
                case "CONL":
                    return true; // {CONFIRMATION_L}:= The religious event by which a person receives membership in The Church of Jesus Christ of Latter-day Saints.
                case "ENDL":
                    return true; // A religious event where an endowment ordinance for an individual was performed by priesthood authority in an LDS Temple.
                case "OFFI":
                    return true; // A person acting in an authorized capacity as voice in performing an ordinance or ceremony.
                case "ORDL":
                    return true; //A religious event of receiving authority to act in religious matters pertaining to the LDS Church. (See ORDN, below.)
                case "SLGC":
                    return true; // {SEALING_CHILD}:= A religious event pertaining to the sealing of a child to his or her parents in an LDS temple ceremony.
                case "WAC":
                    return true; // 5.3 {} pas documenter  LDS_INDI_ORD p.31
                                 // 5.6 WAC}A religious event pertaining to the washing and clothing ordinance performed in preparation of an LDS temple endowment ceremony.
            }
            return false;
        }
        private static bool Si_naissance(string balise)
        {
            if (balise == "BIRT")
                return true;
            return false;
        }

        private static bool Si_union(string balise)
        {
            // retourne  true si relier à un mariage
            switch (balise)
            {
                case "ANUL":
                    return true; // Indicates an individual in which the submitter has interest in additional research for ancestors of this individual. (See also DESI)
                case "DIV":
                    return true; //  An event of dissolving a marriage through civil action.
                case "DIVF":
                    return true; // An event of filing for a divorce by a spouse.
                case "ENGA":
                    return true; // An event of recording or announcing an agreement between two people to become married.
                case "MARB":
                    return true; // An event of an official public notice given that two people intend to marry.
                case "MARC":
                    return true; // An event of recording a formal agreement of marriage, including the prenuptial agreement in which marriage partners reach agreement about the property rights of one or both, securing property to their children.
                case "MARL":
                    return true; // An event of obtaining a legal license to marry.

                case "MARR":
                    return true; // An event of creating an agreement between two people contemplating marriage, at which time they agree to release or modify property rights that would otherwise arise from the marriage.
                case "MARS":
                    return true; // An event of creating an agreement between two people contemplating marriage, at which time they agree to release or modify property rights that would otherwise arise from the marriage.
                case "mariage":
                    return true; // à utiliser dans l'index de famille.


            }
            return false;
        }

        /*
                private static bool Si_public(
                    string date_evenement,
                    bool para_evenement_date,
                    int para_evenement_an)
                {
                    if (GH.GHClass.Para.tout_evenement)
                    {
                        para_evenement_date = GH.GHClass.Para.tout_evenement_date;
                        para_evenement_an = GH.GHClass.Para.tout_evenement_ans;
                    }
                    else
                        if (!para_evenement_date)
                        return false;
                    // année courante
                    DateTime maintenant = DateTime.Now;
                    int annee_courante = maintenant.Year;
                    int an;
                    (_, an) = Convertir_date(date_evenement, GH.GHClass.Para.date_longue);
                    if (para_evenement_date && annee_courante >= para_evenement_an + an)
                        return true;
                    return false;
                }
        */

        private static bool Si_testament_ou_autre(
            string balise,
            string date_evenement,
            string date_naissance,
            string date_deces
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + "<br>balise=" + balise + "<br>date_evenement=" + date_evenement + "<br>date_naissance=" + date_naissance + "<br>date_deces=" + date_deces);
            Regler_code_erreur();
            // retourne  true si relier au testament ou autre balise
            switch (balise)
            {
                case "WILL": // {WILL} A legal document treated as an event, by which a person
                    break;              // disposes of his or her estate, to take effect after death. The
                                        // event date is the date the will was signed while the person was
                                        // alive. (See also PROBate.)
                case "PROB": // {PROBATE} An event of judicial determination of the validity
                    break;               // of a will.May indicate several related court activities over
                                         // several dates.
                default:
                    //R..Z("<b>retourne true</b>");
                    return true;
            }
            bool para_evenement_date = GH.GHClass.Para.testament_information;
            int para_evenement_an = GH.GHClass.Para.testament_ans;
            if (date_evenement == null)
            {
                date_evenement = date_deces;
                para_evenement_an = 100;
            }
            if (date_evenement == null)
            {
                date_evenement = date_naissance;
                para_evenement_an = 200;
            }
            if (date_evenement == null)
            {
                //R..Z("<b>Retourne false</b>");
                return false;
            }
            // année courante
            DateTime maintenant = DateTime.Now;
            int annee_courante = maintenant.Year;
            int an;
            (_, an) = Convertir_date(date_evenement, GH.GHClass.Para.date_longue);
            if (para_evenement_date && annee_courante >= para_evenement_an + an)
            {
                // si premier charactère est une lettre, mettre en majuscule.
                //R..Z("<b>Retourne true</b>");
                return true;
            }
            GC.Collect();
            //R..Z("<b>Retourne false</b>");
            return false;

        }
        private static string Tabulation(int tab = 0)
        {
            return string.Concat(Enumerable.Repeat("\t", tab));
        }
        private static string Titre_groupe(string image, string titre, string ID, int tab)
        {
            string espace = Tabulation(tab);
            string texte = null;
            texte += Separation(15, null, "000", null, tab);
            texte += espace + "<div class=\"groupe_titre\">\n";
            if (image != null)
                texte += espace + "\t<span><img style=\"height:64px\" src=\"" + image + "\" /></span>\n";
            texte += espace + "\t<span>" + titre + "</span>\n";
            if (GH.GHClass.Para.voir_ID && ID != null)
                texte += espace + "\t<div class=\"groupe_titre_ID\">[" + ID + "]</div>\n";
            texte += espace + "</div>\n";
            return texte;
        }
        private static (List<ID_numero>, bool) Verifier_liste(string ID, List<ID_numero> liste_numero_ID
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + " à <b>" + Avoir_nom_method() + "</b> ID=" + ID);
            if (R.IsNullOrEmpty(ID))
                return (liste_numero_ID, false);
            bool modifier = false;
            bool trouver = false;
            int numero = 1;
            // vérifie si la liste est  vide
            if (liste_numero_ID != null)
            {
                // verifie si déjà dans la liste et trouve le prochain numero
                for (int f = 0; f < liste_numero_ID.Count; f++)
                {
                    if (liste_numero_ID[f].ID == ID)
                        trouver = true;
                    if (numero <= liste_numero_ID[f].numero)
                        numero = liste_numero_ID[f].numero + 1;
                }
            }
            // si n'est pas dans la liste, l'ajouter
            if (!trouver)
            {
                ID_numero item = new ID_numero
                {
                    ID = ID,
                    numero = numero
                };
                modifier = true;
                liste_numero_ID.Add(item);
            }
            return (liste_numero_ID, modifier);
        }
        private static (List<ID_numero>, bool) Verifier_liste(List<string> liste_ID, List<ID_numero> liste_numero_ID
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber + " à <b>" + Avoir_nom_method() + "</b>");
            if (R.IsNullOrEmpty(liste_ID))
                return (liste_numero_ID, false);
            bool modifier = false;
            foreach (string ID in liste_ID)
            {
                bool trouver = false;
                int numero = 1;
                // vérifie si la liste est  vide
                if (liste_numero_ID != null)
                {
                    // verifie si déjà dans la liste et trouve le prochain numero
                    for (int f = 0; f < liste_numero_ID.Count; f++)
                    {
                        if (liste_numero_ID[f].ID == ID)
                            trouver = true;
                        if (numero <= liste_numero_ID[f].numero)
                            numero = liste_numero_ID[f].numero + 1;
                    }
                }
                // si n'est pas dans la liste, l'ajouter
                if (!trouver)
                {
                    ID_numero item = new ID_numero
                    {
                        ID = ID,
                        numero = numero
                    };
                    modifier = true;
                    liste_numero_ID.Add(item);
                }
            }
            return (liste_numero_ID, modifier);
        }

        private static (List<MULTIMEDIA_ID_numero>, bool) Verifier_liste_MULTIMEDIA(
            List<string> liste_ID,
            List<MULTIMEDIA_ID_numero> liste_MULTIMEDIA_ID_numero
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "</b> à la ligne " + callerLineNumber);
            //string a = "liste_ID = ";foreach (string b in liste_ID){a += b + " ";};R..Z(a);
            if (liste_ID == null)
                return (liste_MULTIMEDIA_ID_numero, false);
            foreach (string ID in liste_ID)
            {
                // avoir l'info MULTIMEDIA_LINK
                GEDCOMClass.MULTIMEDIA_LINK info_LINK = GEDCOMClass.Avoir_info_MULTIMEDIA_LINK(ID);
                if (info_LINK != null)
                {
                    if (info_LINK.ID_seul)
                    {
                        MULTIMEDIA_ID_numero aa = new MULTIMEDIA_ID_numero
                        {
                            ID_RECORD = info_LINK.ID_RECORD,
                        };
                        liste_MULTIMEDIA_ID_numero.Add(aa);
                    }
                    else
                    {

                        MULTIMEDIA_ID_numero temp = new MULTIMEDIA_ID_numero
                        {
                            ID_LINK = info_LINK.ID_LINK,
                        };
                        liste_MULTIMEDIA_ID_numero.Add(temp);
                        if (info_LINK.ID_RECORD != null)
                        {
                            bool trouver_RECORD = false;
                            // verifie si RECORD déjà dans la liste
                            for (int f = 0; f < liste_MULTIMEDIA_ID_numero.Count; f++)
                            {
                                if (liste_MULTIMEDIA_ID_numero[f].ID_RECORD == info_LINK.ID_RECORD)
                                {
                                    trouver_RECORD = true;
                                }
                                if (trouver_RECORD)
                                    break;
                            }
                            if (!trouver_RECORD)
                            {
                                MULTIMEDIA_ID_numero b = new MULTIMEDIA_ID_numero
                                {
                                    ID_RECORD = info_LINK.ID_RECORD,
                                };
                                liste_MULTIMEDIA_ID_numero.Add(b);
                            }
                        }
                    }
                }
            }
            // retour
            /*
            string c = "retour C ";
            foreach (MULTIMEDIA_ID_numero d in liste_MULTIMEDIA_ID_numero)
            {
                c += d.ID_LINK + " | " + d.ID_RECORD + " ## ";
            }
            R..Z(c);
            */
            return (liste_MULTIMEDIA_ID_numero, false);
        }

        public class Liste_par_date : IComparer<GEDCOMClass.EVEN_ATTRIBUTE_STRUCTURE>
        {
            public int Compare(
                GEDCOMClass.EVEN_ATTRIBUTE_STRUCTURE x,
                GEDCOMClass.EVEN_ATTRIBUTE_STRUCTURE y
                )
            {
                return x.DATE_trier.CompareTo(y.DATE_trier);
            }
        }
        public class Ordonnance_liste_par_date : IComparer<GEDCOMClass.LDS_INDIVIDUAL_ORDINANCE>
        {
            public int Compare(GEDCOMClass.LDS_INDIVIDUAL_ORDINANCE x, GEDCOMClass.LDS_INDIVIDUAL_ORDINANCE y)
            {

                return x.DATE_trier.CompareTo(y.DATE_trier);
            }
        }
    }
}
