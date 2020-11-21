
using GEDCOM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace HTML
{
    public class HTMLClass
    {
        private class ID_numero
        {
            public string ID;
            public int numero;
        }
        private static string Avoir_chercheur(
            List<ID_numero> liste,
            string DossierSortie,
            string sousDossier,
            List<ID_numero> liste_note_ID_numero,
            int tab)
        {
            if (liste == null) return "";
            if (liste.Count == 0) return "";
            string espace = Tabulation(tab);
            string texte = "";
            foreach (ID_numero item in liste)
            {
                GEDCOMClass.SUBMITTER_RECORD info = GEDCOMClass.Avoir_info_chercheur(item.ID);
                if (info == null)
                    return "";
                texte += Separation("mince", tab);
                texte += espace + "<table class=\"tableau\">\n";
                texte += espace + "\t<tr>\n";
                texte += espace + "\t\t<td style=\"background-color:#BBB;border:2px solid #000;margin-left:5px;vertical-align:top;\" colspan=2>\n";// ne pas enlever Border.
                texte += espace + "\t\t\t<span style=\"display: table-cell;\"><a id=\"RefSubmitter" + item.numero.ToString() + "\"></a>\n";
                texte += espace + "\t\t\t<span class=\"chercheur\">" + item.numero.ToString() + "</span>\n";
                texte += espace + "\t\t\t\t<span style=\"display:table-cell;text-align:top; padding-left:5px\">\n";
                texte += espace + "\t\t\t\t\t" + info.N1_NAME + "\n";
                if (GH.Properties.Settings.Default.deboguer) texte += espace + "\t\t\t\t\t\t\t\t{" + info.N0_ID + "}\n";
                texte += espace + "\t\t\t\t</span>\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t<tr>\n";
                if ((info.N1_OBJE_ID_liste.Count > 0) && (GH.Properties.Settings.Default.photo_principal))
                {
                    texte += espace + "\t<tr>\n";
                    texte += espace + "\t\t<td  class=\"indexCol1\">\n";
                    texte += espace + "\t\t\t&nbsp;\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td>\n";
                    // avoir le portrait
                    string fichierPortrait;
                    string IDPortrait;
                    if (info.N1_OBJE_ID_liste.Count > 0)
                    {
                        IDPortrait = info.N1_OBJE_ID_liste[0];
                        GEDCOMClass.MULTIMEDIA_RECORD portraitInfo = GEDCOMClass.Avoir_info_media(IDPortrait);
                        fichierPortrait = CopierObjet(portraitInfo.N1_FILE, sousDossier, DossierSortie);
                        if (fichierPortrait == null)
                        {
                            if (sousDossier == "index")
                            {
                                fichierPortrait = "commun\\media_manquant.svg";
                            }
                            else
                            {
                                fichierPortrait = "../commun\\media_manquant.svg";
                            }
                        }
                        else
                        {
                            if (sousDossier == "index")
                            {
                                fichierPortrait = "individus\\medias\\" + fichierPortrait;
                            }
                            else
                            {
                                fichierPortrait = "medias\\" + fichierPortrait;
                            }
                        }
                        texte += espace + "\t\t\t<br /><img class=\"portrait\" src=\"" + fichierPortrait + "\" alt=\"\" />\n";
                    }
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                // adresse
                GEDCOMClass.ADDRESS_STRUCTURE InfoAdresse = info.N1_ADDR;
                if (InfoAdresse != null)
                {
                    texte += espace + "\t<tr>\n";
                    texte += espace + "\t\t<td  class=\"indexCol1\">\n";
                    texte += espace + "\t\t\tAdresse\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td>\n";
                    if (InfoAdresse.N0_ADDR != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N0_ADDR + "\n";
                    if (InfoAdresse.N1_ADR1 != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_ADR1 + "\n";
                    if (InfoAdresse.N1_ADR1 != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_ADR2 + "\n";
                    if (InfoAdresse.N1_ADR3 != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_ADR3 + "\n";
                    if (InfoAdresse.N1_CITY != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_CITY + "\n";
                    if (InfoAdresse.N1_STAE != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_STAE + "\n";
                    if (InfoAdresse.N1_CTRY != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_CTRY + "\n";
                    if (InfoAdresse.N1_POST != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_POST + "\n";
                    // note pour GRAMP
                    if (InfoAdresse.N1_NOTE_liste_ID != null)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td colspan=2>\n";
                        texte += Avoir_lien_note(InfoAdresse.N1_NOTE_liste_ID, liste_note_ID_numero, true,6);
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                // téléphone
                if (info.N1_PHON_liste != null)
                {
                    foreach (string Tel in info.N1_PHON_liste)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\tTéléphone\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\t" + Tel + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                }
                // fax
                if (info.N1_FAX_liste != null)
                {
                    foreach (string fax in info.N1_FAX_liste)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\tFax\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\t" + fax + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                }
                //  courriel
                if (info.N1_EMAIL_liste != null)
                {
                    foreach (string courriel in info.N1_EMAIL_liste)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\tCourriel;\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\t" + courriel + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                }
                // www
                if (info.N1_WWW_liste != null)
                {
                    foreach (string www in info.N1_WWW_liste)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\tPage Web\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\t" + www + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                }
                // langue
                if (info.N1_LANG != "")
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tLangue\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + info.N1_LANG + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // RFN numéro de fichier d'enregistrement permanent
                if (info.N1_RFN != "")
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td colspan=2>\n";
                    texte += "\t\t\t\t\t\tNuméro d'enregistrement permanent(RFN) " + info.N1_RFN + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // RIN id d'enregistrement automatisé
                if (info.N1_RIN != "")
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td colspan=2>\n";
                    texte += "\t\t\t\t\t\tID d'enregistrement automatisé(RIN) " + info.N1_RIN + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // note du sumiteur
                if (info.N1_NOTE_liste_ID != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td colspan=2>\n";
                    texte += Avoir_lien_note(info.N1_NOTE_liste_ID, liste_note_ID_numero, true, tab + 1);
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // si changement de date
                if (info.N1_CHAN != null && GH.Properties.Settings.Default.voir_date_changement)
                {
                    GEDCOMClass.CHANGE_DATE N1_CHAN = info.N1_CHAN;
                    if (N1_CHAN.N1_CHAN_DATE != "")
                    {

                        texte += espace + "\t<tr>\n";
                        texte += espace + "\t\t<td colspan=2>\n";
                        texte += Date_Changement_Bloc(
                                N1_CHAN,
                                liste_note_ID_numero,
                                tab + 3,
                                false);
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                }
                texte += espace + "</table>\n";
            }
            return texte;
        }
        private (string, int, bool) Groupe_attribut(
            List<GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE> liste,
            int numeroCarte,
            string sousDossier,
            string dossierSortie,
            List<ID_numero> liste_note_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            int tab)
        {
            if (liste == null)
                return ("", numeroCarte, false);
            if (liste.Count == 0)
                return ("", numeroCarte, false);
            string texte = "";
            texte += Separation("large", 3);
            texte += "<a id=\"groupe_attribut\"></a>\n";
            texte += Groupe("debut", 3);
            texte += "\t\t\t\t<table class=\"titre\">\n";
            texte += "\t\t\t\t\t<tr>\n";
            texte += "\t\t\t\t\t\t<td>\n";
            if (liste.Count == 1)
                texte += "\t\t\t\t\t\t\tAttribut\n";
            else
                texte += "\t\t\t\t\t\t\tAttributs\n";
            texte += "\t\t\t\t\t\t</td>\n";
            texte += "\t\t\t\t\t</tr>\n";
            texte += "\t\t\t\t</table>\n";
            Liste_par_date par_date = new Liste_par_date();
            liste.Sort(par_date);
            foreach (GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE info in liste)
            {
                string temp = null;
                (temp, numeroCarte) =
                    Avoir_evenement(
                        info,
                        null,
                        null,
                        null,
                        numeroCarte,
                        sousDossier,
                        dossierSortie,
                        liste_note_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        tab);
                texte += temp;
                texte += Groupe("fin", tab);
            }
            return (texte, numeroCarte, true);
        }
        private void Animation(bool actif)
        {
            System.Windows.Forms.Label Lb_HTML_4 = Application.OpenForms["GH"].Controls["Lb_HTML_4"] as System.Windows.Forms.Label;
            if (!actif)
            {
                Lb_HTML_4.Visible = false;
            }
            else
            {
                Lb_HTML_4.Visible = true;
                Random rnd = new Random();
                string ligne = "";
                for (int f = 0; f < 7; f++)
                {
                    if (rnd.Next(0, 2) == 0) ligne += "▀"; else ligne += "▄";
                }
                Lb_HTML_4.Text = ligne.Substring(0, 5);
            }
            Application.DoEvents();
        }
        public string AssemblerPatronymePrenom(string patronyme, string prenom)
        {
            if (prenom == "" && patronyme == "") return "";
            if (prenom == "") prenom = "?";
            if (patronyme == "") patronyme = "?";
            return patronyme + ", " + prenom;
        }
        private static string Avoir_lien_chercheur(List<string> liste_chercheur_ID, List<ID_numero> liste_chercheur_ID_numero, int tab)
        {
            if (!GH.Properties.Settings.Default.voir_reference) return "";
            if (liste_chercheur_ID.Count() == 0) return "";
            string espace = Tabulation(tab);
            string texte = "";
            Separation("mince", tab);
            texte += espace + "<span style=\"\">\n";
            if (liste_chercheur_ID_numero != null)
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
        private static string Avoir_lien_citation_source(List<string> liste_citation_ID, List<ID_numero> liste_citation_ID_numero, int tab)
        {
            if (!GH.Properties.Settings.Default.voir_reference)
                return "";
            if (liste_citation_ID.Count() == 0) return "";
            string espace = Tabulation(tab);
            string texte = "";
            Separation("mince", 6);
            texte += espace + "<div style=\"font-Size:medium\">\n";
            if (liste_citation_ID_numero != null)
            {
                int compteur_citation;
                if (liste_citation_ID.Count == 1)
                {
                    texte += espace + "\tVoir citation ";
                }
                else
                {
                    texte += espace + "\tVoir citations ";
                }
                foreach (string ID in liste_citation_ID)
                {
                    compteur_citation = Avoir_numero_reference(ID, liste_citation_ID_numero);
                    texte += "<a class=\"citation\" href=\"#citation-" + compteur_citation.ToString() + "\">" + compteur_citation.ToString() + "</a>, ";
                }
                texte = texte.TrimEnd(' ', ',') + "\n";
            }
            texte += espace + "</div>\n";
            return texte;
        }
        private static (string, int) Avoir_evenement(
            GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE info,
            string date_individu_naissance,
            string date_conjoint_naissance,
            string date_conjointe_naissance,
            int numeroCarte,
            string sousDossier,
            string dossierSortie,
            List<ID_numero> liste_note_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            int tab)
        {
            string temp;
            string texte = "";
            string espace = Tabulation(tab);
            texte += Separation("mince", tab);
            texte += Groupe("debut", tab);
            // restriction
            if (info.N2_RESN != null)
            {
                texte += espace + "<div class=\"blink2\">\n";
                texte += espace + "\tRestriction\n";
                texte += espace + "\t" + info.N2_RESN + "\n";
                texte += espace + "</div>\n";
            }
            texte += espace + "<table class=\"tableau\">\n";
            if (info.titre != null)
            {
                texte += espace + "\t<tr class=\"caseEvenement\" style=\"background-color:#BBB;border:2px solid #000;\">\n";
                texte += espace + "\t\t<td class=\"caseEvenement evenement\" colspan=2>\n";
                // titre
                if (info.titre != null)
                {
                    if (info.N1_EVEN == "FACT")
                    {
                        texte += espace + "\t\t\t<strong>" + info.N2_TYPE + " " + info.titre + "</strong>\n";
                    }
                    else texte += espace + "\t\t\t<strong>" + info.titre + "</strong>\n";
                }
                else
                {
                    texte += espace + "\t\t\t&nbsp;\n";
                }
                // date
                string date;
                if (GH.Properties.Settings.Default.date_longue)
                {
                    date = GEDCOMClass.ConvertirDateTexte(info.N2_DATE);
                }
                else
                {
                    date = info.N2_DATE;
                }
                // si date
                if (date != "")
                {
                    if (
                        date.Contains("de ") ||
                        date.Contains("autour ") ||
                        date.Contains("avant ") ||
                        date.Contains("estimé ") ||
                        date.Contains("calculé ") ||
                        date.Contains("jusqu") ||
                        date.Contains("après ") ||
                        date.Contains("entre ") ||
                        date.Contains(" à ") ||
                        date.Contains(" et ")
                        )
                    {
                        texte += espace + "\t\t\t " + date;
                    }
                    else
                    {
                        texte += espace + "\t\t\t le " + date;
                    }
                }
                // si age
                if (info.N1_EVEN.ToUpper() != "BIRT")
                {
                    if (info.N2_AGE != null)
                    {
                        texte += espace + " à l'age de " + info.N2_AGE;
                    }
                    else
                    {
                        string age = Avoir_age(date_individu_naissance, info.N2_DATE);
                        if (age != null) texte += espace + " à l'age de " + age;
                    }
                    string age_HUSB;
                    if (info.N3_HUSB_AGE != null)
                    {
                        age_HUSB = info.N3_HUSB_AGE;
                    }
                    else
                    {
                        age_HUSB = Avoir_age(date_conjoint_naissance, info.N2_DATE);
                    }
                    string age_WIFE;
                    if (info.N3_WIFE_AGE != null)
                    {
                        age_WIFE = info.N3_WIFE_AGE;
                    }
                    else
                    {
                        age_WIFE = Avoir_age(date_conjointe_naissance, info.N2_DATE);
                    }
                    if (age_HUSB != null)
                    {
                        texte += ", conjoint à l'age de " + age_HUSB;
                        if (age_WIFE == null) texte += ".";
                    }
                    if (age_WIFE != null)
                    {
                        texte += ", conjointe à l'age de " + age_WIFE + ".";
                    }
                }
                temp = Avoir_lien_citation_source(
                        info.N2_SOUR_citation_liste_ID,
                        liste_citation_ID_numero,
                        tab + 3);
                texte += temp;
                texte += espace + "\n\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // si type 
            if (info.N2_TYPE != null && info.N1_EVEN != "FACT")
            {
                texte += espace + "\t<tr class=\"caseEvenement\">\n";
                texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                texte += espace + "\t\t\tType \n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t\t<td class=\"caseEvenement\">\n";
                texte += espace + "\t\t\t" + info.N2_TYPE + "\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // si Place lieu
            if ((info.N2_PLAC != null) && (info.N1_EVEN != "RESI"))
            {
                if (info.N2_PLAC.N0_PLAC != null)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    texte += espace + "\t\t\tLieu\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td class=\"caseEvenement\">\n";
                    texte += espace + "\t\t\t" + info.N2_PLAC.N0_PLAC + "\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                    // FORM
                    if (info.N2_PLAC.N1_FORM != null)
                    {
                        texte += espace + "\t<tr class=\"caseEvenement\">\n";
                        texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                        texte += espace + "\t\t\t&nbsp;\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\">\n";
                        texte += espace + "\t\t\tEntité juridictionnelle: " + info.N2_PLAC.N1_FORM + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    // si map
                    if ((info.N2_PLAC.N2_MAP_LATI != null) || (info.N2_PLAC.N2_MAP_LONG != null))
                    {
                        numeroCarte++;
                        texte += espace + "\t<tr class=\"caseEvenement\">\n";
                        texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                        texte += espace + "\t\t\t&nbsp;\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\">\n";
                        texte += espace + "\t\t\tLatitude " + info.N2_PLAC.N2_MAP_LATI + " Longitude " + info.N2_PLAC.N2_MAP_LONG + "\n";
                        texte += Carte(numeroCarte, info.N2_PLAC.N2_MAP_LATI, info.N2_PLAC.N2_MAP_LONG, info.N2_PLAC.N0_PLAC, 8);
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    // si FONE
                    if (info.N2_PLAC.N1_FONE != null)
                    {
                        texte += espace + "\t<tr class=\"caseEvenement\">\n";
                        texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                        texte += espace + "\t\t\t&nbsp;\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\">\n";
                        texte += espace + "\t\t\tPhonétisation " + info.N2_PLAC.N1_FONE + "\n";
                        if (info.N2_PLAC.N2_FONE_TYPE != "")
                        {
                            texte += espace + "\t\t\t " + " type " + info.N2_PLAC.N2_FONE_TYPE + "\n";
                        }
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    // si ROMN
                    if (info.N2_PLAC.N1_ROMN != null)
                    {
                        texte += espace + "\t<tr class=\"caseEvenement\">\n";
                        texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\"> \n";
                        texte += espace + "\t\t\t&nbsp;\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\">\n";
                        texte += espace + "\t\t\tRomanisation " + info.N2_PLAC.N1_ROMN + "\n";
                        if (info.N2_PLAC.N2_ROMN_TYPE != "")
                        {
                            texte += espace + "\t\t\t " + " type " + info.N2_PLAC.N2_ROMN_TYPE + "\n";
                        }
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    // Place NOTE
                    if (info.N2_PLAC.N1_NOTE_liste_ID.Count() > 0)
                    {
                        texte += espace + "\t<tr>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                        texte += espace + "\t\t\t&nbsp;\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td style=\"border:0px solid black;\">\n";
                        texte += Avoir_lien_note(info.N2_PLAC.N1_NOTE_liste_ID, liste_note_ID_numero, true, tab + 3);
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    // SOUR pour GEDitCOM
                    if (info.N2_PLAC.N1_SOUR_citation_liste_ID.Count() > 0)
                    {
                        texte += espace + "\t<tr>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                        texte += espace + "\t\t\t&nbsp;\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td style=\"border:0px solid black;\">\n";
                        temp = Avoir_lien_citation_source(
                                info.N2_PLAC.N1_SOUR_citation_liste_ID,
                                liste_citation_ID_numero,
                                tab + 3);
                        texte += temp;
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                }
            }
            // adresse
            if (info.N2_ADDR != null)
            {
                texte += espace + "\t<tr class=\"caseEvenement\">\n";
                texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                texte += espace + "\t\t\tAdresse\n";
                texte += espace + "\t\t\t</td>\n";
                texte += espace + "\t\t\t<td>\n";
                texte += espace + "\t\t\t\t&nbsp;\n";
                texte += espace + "\t\t\t</td>\n";
                texte += espace + "\t</tr>\n";
                if (info.N2_ADDR.N0_ADDR != null)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    texte += espace + "\t\t\t\t&nbsp;\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + info.N2_ADDR.N0_ADDR + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                if (info.N2_ADDR.N1_ADR1 != null)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    texte += espace + "\t\t\t\t&nbsp;\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + info.N2_ADDR.N1_ADR1 + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                if (info.N2_ADDR.N1_ADR2 != null)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    texte += espace + "\t\t\t\t&nbsp;\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + info.N2_ADDR.N1_ADR2 + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                if (info.N2_ADDR.N1_ADR3 != null)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    texte += espace + "\t\t\t\t&nbsp;\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + info.N2_ADDR.N1_ADR3 + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                if (info.N2_ADDR.N1_CITY != null)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t\t<td class=\"caseEvenement evenementCol1 texteR\">\n";
                    texte += espace + "\t\t\t\tVille \n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + info.N2_ADDR.N1_CITY + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                if (info.N2_ADDR.N1_STAE != null)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t\t<td class=\"caseEvenement evenementCol1 texteR\">\n";
                    texte += espace + "\t\t\t\tProvince/État\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + info.N2_ADDR.N1_STAE + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                if (info.N2_ADDR.N1_CTRY != null)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t\t<td class=\"caseEvenement evenementCol1 texteR\">\n";
                    texte += espace + "\t\t\t\tPays\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + info.N2_ADDR.N1_CTRY + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                if (info.N2_ADDR.N1_POST != null)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t\t<td class=\"caseEvenement evenementCol1 texteR\">\n";
                    texte += espace + "\t\t\t\tCode postal\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + info.N2_ADDR.N1_POST + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                // note adresse pour GRAMPS
                if (info.N2_ADDR.N1_NOTE_liste_ID != null)
                {
                    texte += "\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t&nbsp;\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += Avoir_lien_note(info.N2_ADDR.N1_NOTE_liste_ID, liste_note_ID_numero, true, 6);
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t</tr>\n";
                }
            }
            // si téléphone
            if (info.N2_PHON_liste != null)
            {
                bool premier = true;
                foreach (string PHON in info.N2_PHON_liste)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    if (premier)
                    {
                        texte += espace + "\t\t\tTéléphone \n";
                        premier = false;
                    }
                    else
                    {
                        texte += espace + "\t\t\t&nbsp;\n";
                    }
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + PHON + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
            }
            if (info.N2_FAX_liste != null)
            {
                bool premier = true;
                foreach (string FAX in info.N2_FAX_liste)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    if (premier)
                    {
                        texte += espace + "\t\t\tTélécopieur \n";
                        premier = false;
                    }
                    else
                    {
                        texte += espace + "\t\t\t&nbsp;\n";
                    }
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + FAX + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
            }
            if (info.N2_EMAIL_liste != null)
            {
                bool premier = true;
                foreach (string EMAIL in info.N2_EMAIL_liste)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    if (premier)
                    {
                        texte += espace + "\t\t\tCourriel \n";
                        premier = false;
                    }
                    else
                    {
                        texte += espace + "\t\t\t&nbsp;\n";
                    }
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td>\n";
                    texte += espace + "\t\t\t" + EMAIL + "\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
            }
            if (info.N2_WWW_liste != null)
            {
                bool premier = true;
                foreach (string WWW in info.N2_WWW_liste)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    if (premier)
                    {
                        texte += espace + "\t\t\tSite Web \n";
                        premier = false;
                    }
                    else
                    {
                        texte += espace + "\t\t\t&nbsp;\n";
                    }
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td>\n";
                    texte += espace + "\t\t\t" + WWW + "\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
            }
            // si religion
            if (info.N2_RELI != null)
            {
                texte += espace + "\t<tr class=\"caseEvenement\">\n";
                texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\" >\n";
                texte += espace + "\t\t\tReligion \n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t\t<td class=\"caseEvenement\">\n";
                texte += espace + "\t\t\t" + info.N2_RELI + "\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // si cause
            if (info.N2_CAUS != null)
            {
                texte += espace + "\t<tr class=\"caseEvenement\">\n";
                texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\" >\n";
                texte += espace + "\t\t\tCause de l'événement \n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t\t<td class=\"caseEvenement\">\n";
                texte += espace + "\t\t\t" + info.N2_CAUS + "\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // si adoption
            if (info.N2_FAMC != null)
            {
                string pereIDAdoption = GEDCOMClass.Avoir_famille_conjoint_ID(info.N2_FAMC);
                string nomPereAdoption = GEDCOMClass.AvoirPrenomPatronymeIndividu(pereIDAdoption);
                string mereIDAdoption = GEDCOMClass.Avoir_famille_conjointe_ID(info.N2_FAMC);
                string nomMereAdoption = GEDCOMClass.AvoirPrenomPatronymeIndividu(mereIDAdoption);
                texte += espace + "\t<tr class=\"caseEvenement\">\n";
                texte += espace + "\t\t<td class=\"caseEvenement\">\n";
                texte += espace + "\t\t\tAdopter par\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t\t<td class=\"caseEvenement\">\n";
                if (info.N2_FAMC_ADOP != null)
                {
                    if (info.N2_FAMC_ADOP.ToUpper() == "BOTH")
                    {
                        texte += espace + "\t\t\t" + nomPereAdoption + " et " + nomMereAdoption + "\n";
                    }
                    else if (info.N2_FAMC_ADOP.ToUpper() == "HUSB")
                    {
                        texte += espace + "\t\t\t" + nomPereAdoption + "\n";
                    }
                    else if (info.N2_FAMC_ADOP.ToUpper() == "WIFE")
                    {
                        texte += espace + "\t\t\t" + nomMereAdoption + "\n";
                    }
                }
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // si a description
            if (info.description != null)
            {
                texte += espace + "\t<tr class=\"caseEvenement\">\n";
                texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\" style=\"text-align:right;\">\n";
                texte += espace + "\t\t\tDescription \n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t<td class=\"caseEvenement\">\n";
                texte += espace + "\t\t\t" + info.description + "\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // si a agence
            if (info.N2_AGNC != null)
            {
                texte += espace + "\t<tr class=\"caseEvenement\">\n";
                texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                texte += espace + "\t\t\tAgence\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t\t<td class=\"caseEvenement\" >\n";
                texte += espace + "\t\t\t" + info.N2_AGNC + "\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // Si résidence 
            if (info.N1_EVEN_texte == "RESI")
            {
                if (info.N2_ADDR != null)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\" style=\"text-align:right;\">\n";
                    texte += espace + "\t\t\tAdresse: \n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td class=\"caseEvenement\" >\n";
                    texte += espace + "\t\t\t" + info.N2_ADDR.N0_ADDR + "\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                else
                {
                    if (info.N2_ADDR.N1_ADR1 != "")
                    {
                        texte += espace + "\t<tr class=\"caseEvenement\">\n";
                        texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\" style=\"text-align:right;\">\n";
                        texte += espace + "\t\t\tAdresse: \n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t<td class=\"caseEvenement\" >\n";
                        texte += espace + "\t\t\t" + info.N2_ADDR.N1_ADR1 + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    if (info.N2_ADDR.N1_ADR2 != "")
                    {
                        texte += espace + "\t<tr class=\"caseEvenement\">\n";
                        texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\" style=\"text-align:right;\">\n";
                        texte += espace + "\t\t\tAdresse: \n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\" >\n";
                        texte += espace + "\t\t\t" + info.N2_ADDR.N1_ADR2 + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    if (info.N2_ADDR.N1_ADR3 != "")
                    {
                        texte += espace + "\t<tr class=\"caseEvenement\">\n";
                        texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\" style=\"text-align:right;\" colspan=2>\n";
                        texte += espace + "\t\t\tAdresse: \n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\" >\n";
                        texte += espace + "\t\t\t" + info.N2_ADDR.N1_ADR3 + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    if (info.N2_ADDR.N1_CITY != "")
                    {
                        texte += espace + "\t<tr class=\"caseEvenement\">\n";
                        texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\" style=\"text-align:right;\" colspan=2>\n";
                        texte += espace + "\t\t\t\tVille: \n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\" >\n";
                        texte += espace + "\t\t\t" + info.N2_ADDR.N1_CITY + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    if (info.N2_ADDR.N1_STAE != "")
                    {
                        texte += espace + "\t<tr class=\"caseEvenement\">\n";
                        texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\" style=\"text-align:right;\" colspan=2>\n";
                        texte += espace + "\t\t\tProvince: \n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\" >\n";
                        texte += espace + "\t\t\t" + info.N2_ADDR.N1_STAE + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    if (info.N2_ADDR.N1_CTRY != "")
                    {
                        texte += espace + "\t<tr class=\"caseEvenement evenementCol1\">\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\" style=\"text-align:right;\" colspan=2>\n";
                        texte += espace + "\t\t\tPays: \n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\" >\n";
                        texte += espace + "\t\t\t" + info.N2_ADDR.N1_CTRY + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    if (info.N2_ADDR.N1_POST != "")
                    {
                        texte += espace + "\t<tr class=\"caseEvenement evenementCol1\">\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\" style=\"text-align:right;\" colspan=2>\n";
                        texte += espace + "\t\t\tCode postal: \n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\" >\n";
                        texte += espace + "\t\t\t" + info.N2_ADDR.N1_POST + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                }
            }
            // si Ancestrologie
            // _ANCES_ORDRE
            if (info.N2__ANCES_ORDRE != null)
            {
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    texte += espace + "\t\t\tOrdre\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td class=\"caseEvenement\" >\n";
                    texte += espace + "\t\t\t" + info.N2__ANCES_ORDRE + " (" + GEDCOMClass.Info_HEADER.N2_SOUR_NAME + ")\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
            }
            // _ANCES_XINSEE
            if (info.N2__ANCES_XINSEE != null)
            {
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    texte += espace + "\t\t\tCode géographique\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td class=\"caseEvenement\" >\n";
                    texte += espace + "\t\t\t" + info.N2__ANCES_XINSEE + " (" + GEDCOMClass.Info_HEADER.N2_SOUR_NAME + ")\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
            }
            // media
            if (GH.Properties.Settings.Default.voir_media)
            {
                if (info.N2_OBJE_liste_ID.Count > 0)
                {
                    texte += espace + "\t\t<tr>\n";
                    texte += espace + "\t\t\t<td colspan=2>\n";
                    texte += espace + "\t\t\t\t<table class=\"tableauMedia retrait_point5in\" style=\"border:2px;width:480px\">\n";
                    texte += espace + "\t\t\t\t\t<tr>\n";
                    int cRanger = 0;
                    int totalOBJE = info.N2_OBJE_liste_ID.Count;
                    foreach (string media_ID in info.N2_OBJE_liste_ID)
                    {
                        GEDCOMClass.MULTIMEDIA_RECORD OBJE = GEDCOMClass.Avoir_info_media(media_ID);
                        texte += espace + "\t\t\t\t\t\t<td class=\"tableauMedia\" style=\"width:50%\">\n";
                        (temp, liste_note_ID_numero, liste_citation_ID_numero, liste_source_ID_numero, liste_repo_ID_numero) =
                            Objet(
                                OBJE,
                                sousDossier,
                                dossierSortie,
                                liste_note_ID_numero,
                                liste_citation_ID_numero,
                                liste_source_ID_numero,
                                liste_repo_ID_numero,
                                tab + 6);
                        texte += temp;
                        texte += espace + "\t\t\t\t\t\t</td>\n";
                        cRanger++;
                        if (cRanger % 2 == 0 && cRanger != totalOBJE)
                        {
                            texte += espace + "\t\t\t\t\t</tr>\n";
                            texte += espace + "\t\t\t\t\t<tr>\n";
                        }
                    }
                    if (cRanger % 2 == 1)
                    {
                        texte += espace + "\t\t\t\t\t\t<td class=\"tableauMedia\" style=\"border:0px;width:50%\">\n";
                        texte += "\t\t\t\t\t\t</td>\n";
                    }
                    texte += espace + "\t\t\t\t\t</tr>\n";
                    texte += espace + "\t\t\t\t</table>\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t</tr>\n";
                }
            }
            // si note
            if (info.N2_NOTE_liste_ID.Count > 0)
            {
                texte += espace + "\t<tr class=\"caseEvenement\">\n";
                texte += espace + "\t\t<td class=\"caseEvenement\" colspan=2>\n";
                texte += Avoir_lien_note(info.N2_NOTE_liste_ID, liste_note_ID_numero, true, tab + 3);
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            texte += espace + "\t</table>\n";
            return (texte, numeroCarte);
        }
        private static string Avoir_age(string Naissance, string dateEvenement)
        {
            string r = null;
            try
            {
                if (Naissance == null || dateEvenement == null)
                {
                    return null;
                }
                else if (Naissance.Length != 10 || dateEvenement.Length != 10)
                {
                    return null;
                }
                else
                {
                    // vérifie si alphabet dans Naissance et Décès
                    int c = Naissance.Length;
                    int w = 0;
                    while (w < c)
                    {
                        if ((Naissance[w] >= 'a' && Naissance[w] <= 'z') || (Naissance[w] >= 'A' && Naissance[w] <= 'Z'))
                        {
                            return null;
                        }
                        w++;
                    }
                    c = dateEvenement.Length;
                    w = 0;
                    while (w < c)
                    {
                        if ((dateEvenement[w] >= 'a' && dateEvenement[w] <= 'z') || (dateEvenement[w] >= 'A' && dateEvenement[w] <= 'Z'))
                        {
                            return null;
                        }
                        w++;
                    }
                }
                DateTime nDate = Convert.ToDateTime(Naissance);
                DateTime dDate = Convert.ToDateTime(dateEvenement);
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
                else if (annee != null)
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
                return null;
            }
        }
        private static (List<ID_numero>, List<ID_numero>, List<ID_numero>, List<ID_numero>, List<ID_numero>)
            Avoir_liste_reference_famille(
            string ID_famille,
            List<ID_numero> liste_SUBMITTER_ID_numero,
            List<ID_numero> liste_note_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero)
        {
            // info famille
            GEDCOMClass.FAM_RECORD info_famille = GEDCOMClass.Avoir_info_famille(ID_famille);
            // citation famille
            if (GH.Properties.Settings.Default.voir_reference)
                (liste_citation_ID_numero, _) = Verifier_liste(info_famille.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
            // cnote famille
            if (GH.Properties.Settings.Default.voir_note)
                (liste_note_ID_numero, _) = Verifier_liste(info_famille.N1_NOTE_liste_ID, liste_note_ID_numero);
            // note de SLGS
            if (GH.Properties.Settings.Default.voir_note)
                (liste_note_ID_numero, _) = Verifier_liste(info_famille.N1_SLGS.N1_NOTE_liste_ID, liste_note_ID_numero);
            // chercheur
            if (GH.Properties.Settings.Default.voir_chercheur)
                (liste_SUBMITTER_ID_numero, _) = Verifier_liste(info_famille.N1_SUBM_liste_ID, liste_SUBMITTER_ID_numero);
            // citation SLGS
            if (GH.Properties.Settings.Default.voir_reference)
                (liste_citation_ID_numero, _) = Verifier_liste(info_famille.N1_SLGS.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
            // Groupe média
            if (GH.Properties.Settings.Default.voir_media)
            {
                if (info_famille.N1_OBJE_liste != null)
                {
                    foreach (string IDMedia in info_famille.N1_OBJE_liste)
                    {
                        GEDCOMClass.MULTIMEDIA_RECORD info_media = GEDCOMClass.Avoir_info_media(IDMedia);
                        if (info_media != null)
                        {
                            // note du media
                            if (GH.Properties.Settings.Default.voir_note)
                                (liste_note_ID_numero, _) = Verifier_liste(info_media.N1_NOTE_liste_ID, liste_note_ID_numero);
                            // citation du media
                            if (GH.Properties.Settings.Default.voir_reference)
                                (liste_citation_ID_numero, _) = Verifier_liste(info_media.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                        }
                        // avoir note de Date de changement
                        if (GH.Properties.Settings.Default.voir_date_changement)
                            (liste_note_ID_numero, _) = Verifier_liste(info_media.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
                    }
                }
            }
            // groupe evenement
            if (info_famille.N1_EVENT_Liste != null)
            {
                foreach (GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE info_evenement in info_famille.N1_EVENT_Liste)
                {
                    // citation evenement
                    if (GH.Properties.Settings.Default.voir_reference)
                        (liste_citation_ID_numero, _) = Verifier_liste(info_evenement.N2_SOUR_citation_liste_ID, liste_citation_ID_numero);
                    // note PLAC
                    // if PLAC n'est pas null
                    if (info_evenement.N2_PLAC != null)
                    {
                        // note de PLAC
                        if (GH.Properties.Settings.Default.voir_note)
                            (liste_note_ID_numero, _) = Verifier_liste(info_evenement.N2_PLAC.N1_NOTE_liste_ID, liste_note_ID_numero);
                        // citation PLAC
                        if (GH.Properties.Settings.Default.voir_reference)
                            (liste_citation_ID_numero, _) = Verifier_liste(info_evenement.N2_PLAC.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                    }
                    // note de l'adresse
                    // if adresse n'est pas null
                    if (info_evenement.N2_ADDR != null)
                        if (GH.Properties.Settings.Default.voir_note)
                            (liste_note_ID_numero, _) = Verifier_liste(info_evenement.N2_ADDR.N1_NOTE_liste_ID, liste_note_ID_numero);
                    // note événement
                    if (GH.Properties.Settings.Default.voir_reference)
                        (liste_note_ID_numero, _) = Verifier_liste(info_evenement.N2_NOTE_liste_ID, liste_note_ID_numero);
                    // media
                    if (info_evenement.N2_OBJE_liste_ID != null)
                    {
                        foreach (string media_ID in info_evenement.N2_OBJE_liste_ID)
                        {
                            GEDCOMClass.MULTIMEDIA_RECORD media_info = GEDCOMClass.Avoir_info_media(media_ID);
                            // note
                            if (GH.Properties.Settings.Default.voir_note)
                                (liste_note_ID_numero, _) = Verifier_liste(media_info.N1_NOTE_liste_ID, liste_note_ID_numero);
                            // citation
                            if (GH.Properties.Settings.Default.voir_reference)
                                (liste_citation_ID_numero, _) = Verifier_liste(media_info.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                            // note date de chagement
                            if (GH.Properties.Settings.Default.voir_date_changement)
                                (liste_note_ID_numero, _) = Verifier_liste(media_info.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
                        }
                    }
                }
            }
            // groupe chercheur
            if (GH.Properties.Settings.Default.voir_chercheur)
            {

                if (liste_SUBMITTER_ID_numero != null)
                {
                    foreach (ID_numero item in liste_SUBMITTER_ID_numero)
                    {
                        GEDCOMClass.SUBMITTER_RECORD info_chercheur = GEDCOMClass.Avoir_info_chercheur(item.ID);
                        // note de adresse du chercheur
                        GEDCOMClass.ADDRESS_STRUCTURE Info_adresse = info_chercheur.N1_ADDR;
                        if (GH.Properties.Settings.Default.voir_note)
                            (liste_note_ID_numero, _) = Verifier_liste(Info_adresse.N1_NOTE_liste_ID, liste_note_ID_numero);
                        // note du chercheur
                        if (GH.Properties.Settings.Default.voir_note)
                            (liste_note_ID_numero, _) = Verifier_liste(info_chercheur.N1_NOTE_liste_ID, liste_note_ID_numero);
                        // avoir note de Date de changement
                        if (GH.Properties.Settings.Default.voir_date_changement)
                            (liste_note_ID_numero, _) = Verifier_liste(info_chercheur.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
                    }
                }
            }
            // avoir note de Date de changement de individu
            if (GH.Properties.Settings.Default.voir_date_changement)
                (liste_note_ID_numero, _) = Verifier_liste(info_famille.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
            // avoir note de Date de changement dans source depot note
            if (GH.Properties.Settings.Default.voir_date_changement)
            {
                if (GH.Properties.Settings.Default.voir_reference)
                {
                    // source
                    foreach (ID_numero ID_numero in liste_source_ID_numero)
                    {
                        GEDCOMClass.SOURCE_RECORD info_source = GEDCOMClass.Avoir_info_SOURCE(ID_numero.ID);
                        (liste_note_ID_numero, _) = Verifier_liste(info_source.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
                    }
                    // depot
                    foreach (ID_numero ID_numero in liste_repo_ID_numero)
                    {
                        GEDCOMClass.REPOSITORY_RECORD info_repo = GEDCOMClass.Avoir_Info_Repo(ID_numero.ID);
                        if (info_repo != null)
                        {
                            if (info_repo.N1_CHAN != null)
                                (liste_note_ID_numero, _) = Verifier_liste(info_repo.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
                        }
                    }
                }
                // note
                if (GH.Properties.Settings.Default.voir_note)
                {
                    for (int f = 0; f < liste_note_ID_numero.Count; f++)
                    //foreach (ID_numero ID_numero in liste_note_ID_numero)
                    {
                        GEDCOMClass.NOTE_RECORD info_note = GEDCOMClass.Avoir_Info_Note(liste_note_ID_numero[f].ID);
                        (liste_note_ID_numero, _) = Verifier_liste(info_note.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
                    }
                }
            }
            // loop pour note citation source depot media
            if (GH.Properties.Settings.Default.voir_reference || liste_citation_ID_numero != null)
            {
                bool looper;
                bool modifier;
                do
                {
                    looper = false;
                    // note et source dans citation
                    //foreach (ID_numero item_citation in liste_citation_ID_numero)
                    for (int f1 = 0; f1 < liste_citation_ID_numero.Count; f1++)
                    {
                        GEDCOMClass.SOURCE_CITATION info_citation = GEDCOMClass.Avoir_info_citation(liste_citation_ID_numero[f1].ID);
                        // citation
                        // note
                        if (GH.Properties.Settings.Default.voir_note)
                        {
                            (liste_note_ID_numero, modifier) = Verifier_liste(info_citation.N1_NOTE_liste_ID, liste_note_ID_numero);
                            if (modifier) looper = true;
                        }
                        // si citation a media, extraire de media citation note 
                        if (info_citation.N1_OBJE_ID_liste != null)
                        {
                            foreach (string media_ID in info_citation.N1_OBJE_ID_liste)
                            {
                                GEDCOMClass.MULTIMEDIA_RECORD info_media = GEDCOMClass.Avoir_info_media(media_ID);
                                // note
                                if (GH.Properties.Settings.Default.voir_note)
                                {
                                    (liste_note_ID_numero, modifier) = Verifier_liste(info_media.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
                                    if (modifier) looper = true;
                                }
                                // citation
                                if (GH.Properties.Settings.Default.voir_reference)
                                {
                                    (liste_citation_ID_numero, modifier) = Verifier_liste(info_media.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                                    if (modifier) looper = true;
                                }
                            }
                        }

                        // source
                        if (info_citation.N0_ID_source != null)
                        {
                            GEDCOMClass.SOURCE_RECORD info_source = GEDCOMClass.Avoir_info_SOURCE(info_citation.N0_ID_source);
                            if (GH.Properties.Settings.Default.voir_reference)
                            {
                                {
                                    List<string> liste_ID = new List<string>
                                {
                                info_source.N0_ID
                                };
                                    (liste_source_ID_numero, modifier) = Verifier_liste(liste_ID, liste_source_ID_numero);
                                    if (modifier) looper = true;
                                }
                            }
                            // note
                            if (GH.Properties.Settings.Default.voir_note)
                            {
                                // note de la source
                                (liste_note_ID_numero, modifier) = Verifier_liste(info_source.N1_NOTE_liste_ID, liste_note_ID_numero);
                                if (modifier) looper = true;
                                //note de DATA
                                (liste_note_ID_numero, modifier) = Verifier_liste(info_source.N2_DATA_NOTE_liste_ID, liste_note_ID_numero);
                                if (modifier) looper = true;
                            }
                        }
                    }
                    // note dépôt dans source
                    if (liste_source_ID_numero != null)
                    {
                        //foreach (ID_numero liste_ID_numero in liste_source_ID_numero)
                        for (int f1 = 0; f1 < liste_source_ID_numero.Count; f1++)
                        {
                            GEDCOMClass.SOURCE_RECORD info_source = GEDCOMClass.Avoir_info_SOURCE(liste_source_ID_numero[f1].ID);
                            // dépôt
                            if (info_source.N1_REPO_info != null)
                            {
                                List<string> liste_ID = new List<string>
                                    {
                                        info_source.N1_REPO_info.N0_ID
                                    };
                                (liste_repo_ID_numero, modifier) = Verifier_liste(liste_ID, liste_repo_ID_numero);
                                if (modifier) looper = true;
                                // note
                                if (GH.Properties.Settings.Default.voir_note)
                                {
                                    (liste_note_ID_numero, modifier) = Verifier_liste(info_source.N1_REPO_info.N1_NOTE_liste_ID, liste_note_ID_numero);
                                    if (modifier) looper = true;
                                }
                            }
                        }
                    }
                    // citation dans note
                    //foreach (ID_numero liste_ID_numero in liste_note_ID_numero)
                    for (int f1 = 0; f1 < liste_note_ID_numero.Count; f1++)
                    {
                        if (GH.Properties.Settings.Default.voir_reference)
                        {
                            GEDCOMClass.NOTE_RECORD info_note = GEDCOMClass.Avoir_Info_Note(liste_note_ID_numero[f1].ID);
                            (liste_citation_ID_numero, modifier) = Verifier_liste(info_note.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                            if (modifier) looper = true;
                        }
                    }
                    // avoir note de Date de changement dans source depot note
                    if (GH.Properties.Settings.Default.voir_date_changement)
                    {
                        if (GH.Properties.Settings.Default.voir_reference)
                        {
                            // source
                            foreach (ID_numero ID_numero in liste_source_ID_numero)
                            {
                                GEDCOMClass.SOURCE_RECORD info_source = GEDCOMClass.Avoir_info_SOURCE(ID_numero.ID);
                                (liste_note_ID_numero, modifier) = Verifier_liste(info_source.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
                                if (modifier) looper = true;
                            }
                            // depot
                            foreach (ID_numero ID_numero in liste_repo_ID_numero)
                            {
                                GEDCOMClass.REPOSITORY_RECORD info_repo = GEDCOMClass.Avoir_Info_Repo(ID_numero.ID);
                                if (info_repo != null)
                                {
                                    if (info_repo.N1_CHAN != null)
                                    {
                                        (liste_note_ID_numero, modifier) = Verifier_liste(info_repo.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
                                        if (modifier) looper = true;
                                    }
                                }
                            }
                        }
                        // note
                        if (GH.Properties.Settings.Default.voir_note)
                        {
                            for (int f = 0; f < liste_note_ID_numero.Count; f++)
                            //foreach (ID_numero ID_numero in liste_note_ID_numero)
                            {
                                GEDCOMClass.NOTE_RECORD info_note = GEDCOMClass.Avoir_Info_Note(liste_note_ID_numero[f].ID);
                                (liste_note_ID_numero, modifier) = Verifier_liste(info_note.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
                                if (modifier) looper = true;
                            }
                        }
                    }
                } while (looper != false);
            }
            return (liste_SUBMITTER_ID_numero,
                liste_note_ID_numero,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_repo_ID_numero);
        }
        private static (List<ID_numero>, List<ID_numero>, List<ID_numero>, List<ID_numero>, List<ID_numero>)
            Avoir_liste_reference_individu(
            string ID_individu,
            List<ID_numero> liste_SUBMITTER_ID_numero,
            List<ID_numero> liste_note_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero)
        {
            // avoir info individu
            GEDCOMClass.INDIVIDUAL_RECORD info_individu;
            (_, info_individu) = GEDCOMClass.Avoir_info_individu(ID_individu);
            // citation de l'individu
            if (GH.Properties.Settings.Default.voir_reference)
                (liste_citation_ID_numero, _) = Verifier_liste(info_individu.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
            // liste de nom
            List<GEDCOMClass.PERSONAL_NAME_STRUCTURE> listeInfoNom;
            bool OkLIN;
            (OkLIN, listeInfoNom) = GEDCOMClass.AvoirListeNom(ID_individu);
            if (OkLIN)
            {
                foreach (GEDCOM.GEDCOMClass.PERSONAL_NAME_STRUCTURE info_nom in listeInfoNom)
                {
                    // citation sur le nom
                    if (GH.Properties.Settings.Default.voir_reference)
                        (liste_citation_ID_numero, _) = Verifier_liste(info_nom.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                    // note sur le nom
                    if (GH.Properties.Settings.Default.voir_note)
                        (liste_note_ID_numero, _) = Verifier_liste(info_nom.N1_NOTE_liste_ID, liste_note_ID_numero);
                    // citatation de FONE
                    if (GH.Properties.Settings.Default.voir_reference)
                        (liste_citation_ID_numero, _) = Verifier_liste(info_nom.N2_FONE_SOUR_citation_liste_ID, liste_citation_ID_numero);
                    // note sur le FONE
                    if (GH.Properties.Settings.Default.voir_note)
                        (liste_note_ID_numero, _) = Verifier_liste(info_nom.N2_FONE_NOTE_ID_liste, liste_note_ID_numero);
                    // citatation de ROMN
                    if (GH.Properties.Settings.Default.voir_reference)
                        (liste_citation_ID_numero, _) = Verifier_liste(info_nom.N2_ROMN_SOUR_citation_liste_ID, liste_citation_ID_numero);
                    // note sur le ROMN
                    if (GH.Properties.Settings.Default.voir_note)
                        (liste_note_ID_numero, _) = Verifier_liste(info_nom.N2_ROMN_NOTE_ID_liste, liste_note_ID_numero);
                }
            }
            // association
            if (info_individu.N1_ASSO_liste != null)
            {
                foreach (GEDCOMClass.ASSOCIATION_STRUCTURE info_ASSO in info_individu.N1_ASSO_liste)
                {
                    // citation
                    if (GH.Properties.Settings.Default.voir_reference)
                        (liste_citation_ID_numero, _) = Verifier_liste(info_ASSO.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                    // note
                    if (GH.Properties.Settings.Default.voir_note)
                        (liste_note_ID_numero, _) = Verifier_liste(info_ASSO.N1_NOTE_liste_ID, liste_note_ID_numero);
                }
            }
            // ANCI
            if (GH.Properties.Settings.Default.voir_chercheur)
                (liste_SUBMITTER_ID_numero, _) = Verifier_liste(info_individu.N1_ANCI_liste_ID, liste_SUBMITTER_ID_numero);
            // DESI
            if (GH.Properties.Settings.Default.voir_chercheur)
                (liste_SUBMITTER_ID_numero, _) = Verifier_liste(info_individu.N1_DESI_liste_ID, liste_SUBMITTER_ID_numero);
            // note individu
            if (GH.Properties.Settings.Default.voir_note)
                (liste_note_ID_numero, _) = Verifier_liste(info_individu.N1_NOTE_liste_ID, liste_note_ID_numero);
            // chercheur
            if (GH.Properties.Settings.Default.voir_chercheur)
                (liste_SUBMITTER_ID_numero, _) = Verifier_liste(info_individu.N1_SUBM_liste_ID, liste_SUBMITTER_ID_numero);
            // Groupe conjoint
            // note conjoint
            if (GH.Properties.Settings.Default.voir_note)
            {
                foreach (GEDCOMClass.SPOUSE_TO_FAMILY_LINK infoLienConjoint in info_individu.N1_FAMS_liste_Conjoint)
                    (liste_note_ID_numero, _) = Verifier_liste(infoLienConjoint.N1_NOTE_liste_ID, liste_note_ID_numero);
            }
            // Groupe parent
            // note famille
            if (GH.Properties.Settings.Default.voir_note)
            {
                GEDCOMClass.CHILD_TO_FAMILY_LINK infoFamille = GEDCOMClass.AvoirInfoFamilleEnfant(ID_individu);
                (liste_note_ID_numero, _) = Verifier_liste(infoFamille.N1_NOTE_liste_ID, liste_note_ID_numero);
            }
            // Groupe média
            if (GH.Properties.Settings.Default.voir_media)
            {
                if (info_individu.N1_OBJE_liste != null)
                {
                    foreach (string IDMedia in info_individu.N1_OBJE_liste)
                    {
                        GEDCOMClass.MULTIMEDIA_RECORD info_media = GEDCOMClass.Avoir_info_media(IDMedia);
                        if (info_media != null)
                        {
                            // note du media
                            if (GH.Properties.Settings.Default.voir_note)
                                (liste_note_ID_numero, _) = Verifier_liste(info_media.N1_NOTE_liste_ID, liste_note_ID_numero);
                            // citation du media
                            if (GH.Properties.Settings.Default.voir_reference)
                                (liste_citation_ID_numero, _) = Verifier_liste(info_media.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                        }
                        // avoir note de Date de changement
                        if (GH.Properties.Settings.Default.voir_date_changement)
                            (liste_note_ID_numero, _) = Verifier_liste(info_media.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
                    }
                }
            }
            // Groupe ordonnance
            if (info_individu.N1_LDS_liste != null)
            {
                foreach (GEDCOMClass.LDS_INDIVIDUAL_ORDINANCE info_LDS in info_individu.N1_LDS_liste)
                {
                    // citation d'ordonnance
                    if (GH.Properties.Settings.Default.voir_reference)
                        (liste_citation_ID_numero, _) = Verifier_liste(info_LDS.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                    if (GH.Properties.Settings.Default.voir_note)
                        // note d'ordonnance
                        (liste_note_ID_numero, _) = Verifier_liste(info_LDS.N1_NOTE_liste_ID, liste_note_ID_numero);
                }
            }
            // groupe evenement
            if (info_individu.N1_EVENT_Liste != null)
            {
                foreach (GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE info_evenement in info_individu.N1_EVENT_Liste)
                {
                    // citation evenement
                    if (GH.Properties.Settings.Default.voir_reference)
                        (liste_citation_ID_numero, _) = Verifier_liste(info_evenement.N2_SOUR_citation_liste_ID, liste_citation_ID_numero);
                    // note PLAC
                    // if PLAC n'est pas null
                    if (info_evenement.N2_PLAC != null)
                    {
                        // note de PLAC
                        if (GH.Properties.Settings.Default.voir_note)
                            (liste_note_ID_numero, _) = Verifier_liste(info_evenement.N2_PLAC.N1_NOTE_liste_ID, liste_note_ID_numero);
                        // citation PLAC
                        if (GH.Properties.Settings.Default.voir_reference)
                            (liste_citation_ID_numero, _) = Verifier_liste(info_evenement.N2_PLAC.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                    }
                    // note de l'adresse
                    // if adresse n'est pas null
                    if (info_evenement.N2_ADDR != null)
                        if (GH.Properties.Settings.Default.voir_note)
                            (liste_note_ID_numero, _) = Verifier_liste(info_evenement.N2_ADDR.N1_NOTE_liste_ID, liste_note_ID_numero);
                    // note événement
                    if (GH.Properties.Settings.Default.voir_reference)
                        (liste_note_ID_numero, _) = Verifier_liste(info_evenement.N2_NOTE_liste_ID, liste_note_ID_numero);
                    // media
                    if (info_evenement.N2_OBJE_liste_ID != null)
                    {
                        foreach(string media_ID in info_evenement.N2_OBJE_liste_ID)
                        {
                            GEDCOMClass.MULTIMEDIA_RECORD media_info = GEDCOMClass.Avoir_info_media(media_ID);
                            // note
                            if (GH.Properties.Settings.Default.voir_note)
                                (liste_note_ID_numero, _) = Verifier_liste(media_info.N1_NOTE_liste_ID, liste_note_ID_numero);
                            // citation
                            if (GH.Properties.Settings.Default.voir_reference)
                                (liste_citation_ID_numero, _) = Verifier_liste(media_info.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                            // note date de chagement
                            if (GH.Properties.Settings.Default.voir_date_changement)
                                (liste_note_ID_numero, _) = Verifier_liste(media_info.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
                        }
                    }
                }
            }
            // groupe attribut
            if (info_individu.N1_Attribute_liste != null)
            {
                foreach (GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE info_attribut in info_individu.N1_Attribute_liste)
                {
                    // citation attribut
                    if (GH.Properties.Settings.Default.voir_reference)
                        (liste_citation_ID_numero, _) = Verifier_liste(info_attribut.N2_SOUR_citation_liste_ID, liste_citation_ID_numero);
                    // if PLAC n'est pas null
                    if (info_attribut.N2_PLAC != null)
                    {
                        // note PLAC
                        if (GH.Properties.Settings.Default.voir_note)
                            (liste_note_ID_numero, _) = Verifier_liste(info_attribut.N2_PLAC.N1_NOTE_liste_ID, liste_note_ID_numero);
                        // citation PLAC
                        if (GH.Properties.Settings.Default.voir_reference)
                            (liste_citation_ID_numero, _) = Verifier_liste(info_attribut.N2_PLAC.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                    }
                    // si ADDR n'est pas null
                    if (info_attribut.N2_ADDR != null)
                    {
                        // note de l'adresse
                        if (GH.Properties.Settings.Default.voir_note)
                            (liste_note_ID_numero, _) = Verifier_liste(info_attribut.N2_ADDR.N1_NOTE_liste_ID, liste_note_ID_numero);
                    }
                    // note attribut
                    if (GH.Properties.Settings.Default.voir_note)
                        (liste_note_ID_numero, _) = Verifier_liste(info_attribut.N2_NOTE_liste_ID, liste_note_ID_numero);
                    // media
                    if (info_attribut.N2_OBJE_liste_ID != null)
                    {
                        foreach (string media_ID in info_attribut.N2_OBJE_liste_ID)
                        {
                            GEDCOMClass.MULTIMEDIA_RECORD media_info = GEDCOMClass.Avoir_info_media(media_ID);
                            // note
                            if (GH.Properties.Settings.Default.voir_note)
                                (liste_note_ID_numero, _) = Verifier_liste(media_info.N1_NOTE_liste_ID, liste_note_ID_numero);
                            // citation
                            if (GH.Properties.Settings.Default.voir_reference)
                                (liste_citation_ID_numero, _) = Verifier_liste(media_info.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                            // note date de chagement
                            if (GH.Properties.Settings.Default.voir_date_changement)
                                (liste_note_ID_numero, _) = Verifier_liste(media_info.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
                        }
                    }
                }
            }
            // groupe chercheur
            if (GH.Properties.Settings.Default.voir_chercheur)
            {

                if (liste_SUBMITTER_ID_numero != null)
                {
                    foreach (ID_numero item in liste_SUBMITTER_ID_numero)
                    {
                        GEDCOMClass.SUBMITTER_RECORD info_chercheur = GEDCOMClass.Avoir_info_chercheur(item.ID);
                        // note de adresse du chercheur
                        GEDCOMClass.ADDRESS_STRUCTURE Info_adresse = info_chercheur.N1_ADDR;
                        if (GH.Properties.Settings.Default.voir_note)
                            (liste_note_ID_numero, _) = Verifier_liste(Info_adresse.N1_NOTE_liste_ID, liste_note_ID_numero);
                        // note du chercheur
                        if (GH.Properties.Settings.Default.voir_note)
                            (liste_note_ID_numero, _) = Verifier_liste(info_chercheur.N1_NOTE_liste_ID, liste_note_ID_numero);
                        // avoir note de Date de changement
                        if (GH.Properties.Settings.Default.voir_date_changement)
                            (liste_note_ID_numero, _) = Verifier_liste(info_chercheur.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
                    }
                }
            }
            // avoir note de Date de changement de individu
            if (GH.Properties.Settings.Default.voir_date_changement)
                (liste_note_ID_numero, _) = Verifier_liste(info_individu.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
            // avoir note de Date de changement dans source depot note
            if (GH.Properties.Settings.Default.voir_date_changement)
            {
                if (GH.Properties.Settings.Default.voir_reference)
                {
                    // source
                    foreach (ID_numero ID_numero in liste_source_ID_numero)
                    {
                        GEDCOMClass.SOURCE_RECORD info_source = GEDCOMClass.Avoir_info_SOURCE(ID_numero.ID);
                        (liste_note_ID_numero, _) = Verifier_liste(info_source.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
                    }
                    // depot
                    foreach (ID_numero ID_numero in liste_repo_ID_numero)
                    {
                        GEDCOMClass.REPOSITORY_RECORD info_repo = GEDCOMClass.Avoir_Info_Repo(ID_numero.ID);
                        if (info_repo != null)
                        {
                            if (info_repo.N1_CHAN != null)
                                (liste_note_ID_numero, _) = Verifier_liste(info_repo.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
                        }
                    }
                }
                // note
                if (GH.Properties.Settings.Default.voir_note)
                {
                    for (int f = 0; f < liste_note_ID_numero.Count; f++)
                    //foreach (ID_numero ID_numero in liste_note_ID_numero)
                    {
                        GEDCOMClass.NOTE_RECORD info_note = GEDCOMClass.Avoir_Info_Note(liste_note_ID_numero[f].ID);
                        (liste_note_ID_numero, _) = Verifier_liste(info_note.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
                    }
                }
            }
            // loop pour note citation source depot media
            if (GH.Properties.Settings.Default.voir_reference || liste_citation_ID_numero != null)
            {
                bool looper;
                bool modifier;
                do
                {
                    looper = false;
                    // note et source dans citation
                    //foreach (ID_numero item_citation in liste_citation_ID_numero)
                    for(int f1 = 0;f1 < liste_citation_ID_numero.Count;f1++)
                    {
                        GEDCOMClass.SOURCE_CITATION info_citation = GEDCOMClass.Avoir_info_citation(liste_citation_ID_numero[f1].ID);
                        // citation
                        // note
                        if (GH.Properties.Settings.Default.voir_note)
                        {
                            // note de citation
                            (liste_note_ID_numero, modifier) = Verifier_liste(info_citation.N1_NOTE_liste_ID, liste_note_ID_numero);
                            if (modifier) looper = true;
                        }
                        // si citation a media, extraire de media citation note 
                        if (info_citation.N1_OBJE_ID_liste != null)
                        {
                            foreach( string media_ID in info_citation.N1_OBJE_ID_liste)
                            {
                                GEDCOMClass.MULTIMEDIA_RECORD info_media = GEDCOMClass.Avoir_info_media(media_ID);
                                // note
                                if (GH.Properties.Settings.Default.voir_note)
                                {
                                    (liste_note_ID_numero, modifier) = Verifier_liste(info_media.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
                                    if (modifier) looper = true;
                                }
                                // citation
                                if (GH.Properties.Settings.Default.voir_reference)
                                {
                                    (liste_citation_ID_numero, modifier) = Verifier_liste(info_media.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                                    if (modifier) looper = true;
                                }
                            }
                        }

                        // source
                        if (info_citation.N0_ID_source != null)
                        {
                            GEDCOMClass.SOURCE_RECORD info_source = GEDCOMClass.Avoir_info_SOURCE(info_citation.N0_ID_source);
                            if (GH.Properties.Settings.Default.voir_reference)
                            {
                                {
                                    List<string> liste_ID = new List<string>
                                {
                                info_source.N0_ID
                                };
                                    (liste_source_ID_numero, modifier) = Verifier_liste(liste_ID, liste_source_ID_numero);
                                    if (modifier) looper = true;
                                }
                            }
                            // note
                            if (GH.Properties.Settings.Default.voir_note)
                            {
                                // note de citation
                                (liste_note_ID_numero, modifier) = Verifier_liste(info_source.N1_NOTE_liste_ID, liste_note_ID_numero);
                                if (modifier) looper = true;
                                //node de DATA
                                (liste_note_ID_numero, modifier) = Verifier_liste(info_source.N2_DATA_NOTE_liste_ID, liste_note_ID_numero);
                                if (modifier) looper = true;
                            }
                        }
                    }
                    // note dépôt dans source
                    if (liste_source_ID_numero != null)
                    {
                        //foreach (ID_numero liste_ID_numero in liste_source_ID_numero)
                        for (int f1 = 0; f1 < liste_source_ID_numero.Count;f1++)
                        {
                            GEDCOMClass.SOURCE_RECORD info_source = GEDCOMClass.Avoir_info_SOURCE(liste_source_ID_numero[f1].ID);
                            // dépôt
                            if (info_source.N1_REPO_info != null)
                            {
                                List<string> liste_ID = new List<string>
                                    {
                                        info_source.N1_REPO_info.N0_ID
                                    };
                                (liste_repo_ID_numero, modifier) = Verifier_liste(liste_ID, liste_repo_ID_numero);
                                if (modifier) looper = true;
                                // note
                                if (GH.Properties.Settings.Default.voir_note)
                                {
                                    (liste_note_ID_numero, modifier) = Verifier_liste(info_source.N1_REPO_info.N1_NOTE_liste_ID, liste_note_ID_numero);
                                    if (modifier) looper = true;
                                }
                            }
                        }
                    }
                    // citation dans note
                    //foreach (ID_numero liste_ID_numero in liste_note_ID_numero)
                    for (int f1 = 0; f1 < liste_note_ID_numero.Count; f1++)
                    {
                        if (GH.Properties.Settings.Default.voir_reference) 
                        {
                            GEDCOMClass.NOTE_RECORD info_note = GEDCOMClass.Avoir_Info_Note(liste_note_ID_numero[f1].ID);
                            (liste_citation_ID_numero, modifier) = Verifier_liste(info_note.N1_SOUR_citation_liste_ID, liste_citation_ID_numero);
                            if (modifier) looper = true;
                        }
                    }
                        // avoir note de Date de changement dans source depot note
                    if (GH.Properties.Settings.Default.voir_date_changement)
                    {
                        if (GH.Properties.Settings.Default.voir_reference)
                        {
                            // source
                            foreach (ID_numero ID_numero in liste_source_ID_numero)
                            {
                                GEDCOMClass.SOURCE_RECORD info_source = GEDCOMClass.Avoir_info_SOURCE(ID_numero.ID);
                                //node de CHAN
                                (liste_note_ID_numero, modifier) = Verifier_liste(info_source.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
                                if (modifier) looper = true;
                                
                            }
                            // depot
                            foreach (ID_numero ID_numero in liste_repo_ID_numero)
                            {
                                GEDCOMClass.REPOSITORY_RECORD info_repo = GEDCOMClass.Avoir_Info_Repo(ID_numero.ID);
                                if (info_repo != null)
                                {
                                    if (info_repo.N1_CHAN != null)
                                    {
                                        (liste_note_ID_numero, modifier) = Verifier_liste(info_repo.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
                                        if (modifier) looper = true;
                                    }
                                }
                            }
                        }
                        // note
                        if (GH.Properties.Settings.Default.voir_note)
                        {
                            for (int f = 0; f < liste_note_ID_numero.Count; f++)
                            //foreach (ID_numero ID_numero in liste_note_ID_numero)
                            {
                                GEDCOMClass.NOTE_RECORD info_note = GEDCOMClass.Avoir_Info_Note(liste_note_ID_numero[f].ID);
                                (liste_note_ID_numero, modifier) = Verifier_liste(info_note.N1_CHAN.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero);
                                if (modifier) looper = true;
                            }
                        }
                    }
                } while (looper != false);
            }
            return (liste_SUBMITTER_ID_numero,
            liste_note_ID_numero,
            liste_citation_ID_numero,
            liste_source_ID_numero,
            liste_repo_ID_numero);
        }
        private static string Avoir_lien_note( List<string> liste_ID, List<ID_numero> liste_note_ID_numero,bool majuscule, int tab)
        {
            if (!GH.Properties.Settings.Default.voir_note) return null;
            if (liste_ID == null) return null;
            if (liste_ID.Count == 0) return null;
            //liste_note_ID_numero = //Verifier_liste(liste_ID, liste_note_ID_numero);
            string espace = Tabulation(tab);
            string texte = espace;
            //string texte = espace + "<div style=\"font-Size:11px;font-weight:normal;\">\n";
            if (majuscule) texte += "V";
            else texte += "v";
            texte += "oir note";
            if (liste_ID.Count() > 1)
            {
                texte += "s ";
            }
            texte += "\n";
            foreach (string note_ID in liste_ID)
            {
                int numero = 0;
                numero = Avoir_numero_reference(note_ID, liste_note_ID_numero);
                if (numero == 0) return "";
                texte += "<a class=\"note\" href=\"#note-" + numero + "\">" + numero + "</a>" + ", ";
            }
            texte = texte.TrimEnd(' ', ',');
            texte += "\n";// + espace + "</div>\n";
            return texte;
        }
        private static int Avoir_numero_reference(string ID, List<ID_numero> liste)
        {
            int numero = 0;
            foreach (ID_numero a in liste)
            {
                if (a.ID == ID) return a.numero;
            }
            return numero;
        }
        public string Avoir_premier_nom_individu(GEDCOMClass.INDIVIDUAL_RECORD info_individu)
        {
            if (info_individu == null) return "";
            if (info_individu.N1_NAME_liste == null) return "";
            if (info_individu.N1_NAME_liste[0].N1_GIVN != null || info_individu.N1_NAME_liste[0].N1_SURN != null)
            {
                string patronyme = info_individu.N1_NAME_liste[0].N1_SURN;
                string prenom = info_individu.N1_NAME_liste[0].N1_GIVN;
                if (prenom == "" && patronyme == "") return info_individu.N1_NAME_liste[0].N0_NAME;
                if (prenom == null && patronyme == null) return info_individu.N1_NAME_liste[0].N0_NAME;
                if (prenom == "") prenom = "?";
                if (patronyme == "") patronyme = "?";
                return patronyme + ", " + prenom;
            }
            else
            {
                return info_individu.N1_NAME_liste[0].N0_NAME;
            }
        }

        private static (string, List<ID_numero>) Avoir_lien_repo(string ID_repo, List<ID_numero> liste_repo_ID_numero, int tab)
        {
            ID_numero infoPlus = new ID_numero();
            if (ID_repo == null) return ("", liste_repo_ID_numero);
            if (ID_repo == "") return ("", liste_repo_ID_numero);
            string espace = Tabulation(tab);
            string texte = espace + "<span style=\"\">\n";
            texte += espace + "\tVoir le dépôt \n";
            bool trouverRepo = false;
            // si la liste est vide
            if (liste_repo_ID_numero.Count == 0)
            {
                infoPlus.numero = 1;
                infoPlus.ID = ID_repo;
                texte += espace + "\t<a class=\"depot\" href=\"#depot-1\">1</a>, ";
                liste_repo_ID_numero.Add(infoPlus);
            }
            // ajout dans la liste
            else
            {
                // voir si le ID est déjà dans la liste
                texte += espace + "\t";
                foreach (ID_numero f in liste_repo_ID_numero)
                {
                    if (f.ID == ID_repo)  // si la source existe déjà
                    {
                        trouverRepo = true;
                        texte += "<a class=\"depot\" href=\"#depot-" + f.numero.ToString() + "\">" + f.numero.ToString() + "</a>, ";
                    }
                }
                // le ID n'est pas la
                if (!trouverRepo)
                {
                    int compteurRepo = 0;
                    // trouver plus grand numero
                    foreach (ID_numero i in liste_repo_ID_numero)
                    {
                        if (i.numero >= compteurRepo) compteurRepo = i.numero + 1;
                    }
                    infoPlus.numero = compteurRepo;
                    infoPlus.ID = ID_repo;
                    texte += "<a class=\"depot\" href=\"#depot-" + compteurRepo + "\">" + compteurRepo + "</a>, ";
                    liste_repo_ID_numero.Add(infoPlus);
                }
                texte += "\n";
            }
            texte = texte.TrimEnd(' ', ',');
            texte += "\n";
            texte += espace + "</span>\n";
            return (texte, liste_repo_ID_numero);
        }
        private static string Avoir_lien_source(string ID_source, List<ID_numero> liste_source_ID_numero, int tab)
        {
            if (ID_source == null) return "";
            if (ID_source == "") return ("");
            string espace = Tabulation(tab);
            string texte;
            int numero_source = Avoir_numero_reference(ID_source, liste_source_ID_numero);
            texte = espace +
                "\t\t\t\t<span style=\"display:table-cell;text-align:top; padding-left:5px\">Voir la source <a class=\"source\" href=\"#source-" +
                numero_source.ToString() + "\">" + numero_source.ToString() + "</a>";
            return texte; 
        }
        private string AvoirFamilleConjointIndex()
        {
            ListView lvChoixFamille = Application.OpenForms["GH"].Controls["lvChoixFamille"] as ListView;
            bool[] alphabette = new bool[27];
            for (int f = 0; f < 27; f++)
            {
                alphabette[f] = false;
            }
            string texte = "";
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
            texte += "\t\t\t<div style=\"width:860px;border-radius:10px;background-color:#6464FF;margin-left:auto;margin-right:auto;padding:5px;\">\n";
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
        private static string Avoir_code_erreur([CallerLineNumber] int sourceLineNumber = 0)
        {
            return "H" + sourceLineNumber;
        }
        private string AvoirFamilleConjointeIndex()
        {
            bool[] alphabette = new bool[27];
            ListView lvChoixFamille = Application.OpenForms["GH"].Controls["lvChoixFamille"] as ListView;
            for (int f = 0; f < 27; f++)
            {
                alphabette[f] = false;
            }
            string texte = "";
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
            texte += "\t\t\t<div style=\"width:860px;border-radius:10px;background-color:#6464FF;margin-left:auto;margin-right:auto;padding:5px;\">\n";
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
            return texte;
        }
        private string Bas_Page()
        {
            string texte = "";
            texte += "\t\t\t<footer>\n";
            texte += "\t\t\t\t<br />\n";
            texte += "\t\t\t\t<hr style=\"border: 1px solid black;width:300px;margin-left:0;\" />\n";
            DateTime laDate = DateTime.Now;
            texte += "\t\t\t\tGénéré par GH (GEDCOM-Html) V0.1B, le " + laDate.ToString("dd MMMMM yyyy") + "\n";
            texte += "\t\t\t</footer>\n";
            texte += "\t\t</div><!-- fin de page-->\n";
            texte += "\t</body>\n";
            texte += "</html>\n";
            return texte;
        }
        private static string Carte(int NumeroCarte, string LATI, string LONG, string info, int tab)
        {
            if (!GH.Properties.Settings.Default.voir_carte) return "";
            string espace = Tabulation(tab); ;
            string texte = espace + "<div id=\"map" + NumeroCarte.ToString() + "\" class=\"carte\"></div>\n";
            texte += espace + "<script>\n";
            texte += espace + "\t// Initialiser Leaflet\n";
            texte += espace + "\tvar map" + NumeroCarte.ToString() + " = L.map('map" + NumeroCarte.ToString() + "').setView({lon: " + LONG + ", lat: " + LATI + "}, 7);\n";
            texte += espace + "\t// ajouter titre OpenStreetMap\n";
            texte += espace + "\tL.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 25, attribution: '&copy;<a href=\"https://openstreetmap.org/copyright\">Contribution OpenStreetMap</a>'}).addTo(map" + NumeroCarte.ToString() + ");\n";
            texte += espace + "\t// Montrer échelle en bas à gauche\n";
            texte += espace + "\tL.control.scale().addTo(map" + NumeroCarte.ToString() + ");\n";
            texte += espace + "\t// montrer le lieu sur la carte\n";
            texte += espace + "\tL.marker({lon: " + LONG + ", lat: " + LATI + "}).bindPopup('" + info + "').addTo(map" + NumeroCarte.ToString() + ");\n";
            texte += espace + "</script>\n";
            return texte;
        }
        private static string CopierObjet(string fichier, string sousDossier, string DossierSortie)
        {
            if (fichier == null) return null;
            if (fichier == "") return null;
            if (!DossierHTMLValide()) return null;
            string fichierSource = Path.GetFileName(fichier);
            string fichierDestination = fichierSource.Replace(" ", "_");
            string source = null;
            string RepertoireDestination = DossierSortie + "/" + sousDossier + "/medias/";
            if (sousDossier == "commun") RepertoireDestination = DossierSortie + "/commun/";
            if (sousDossier == "index") RepertoireDestination = DossierSortie + "/commun/";
            if (!Directory.Exists(RepertoireDestination))
            {
                MessageErreur("Le dossier de destination\r\n\r\n« " + RepertoireDestination + " »\r\n\r\nn'existe pas, corrigez dans vos paramètres.\r\n\r\n");
                return "";
            }
            if (File.Exists(fichier) && fichier != null)
            {
                source = fichier;
            }
            else
            {
                if (GH.Properties.Settings.Default.DossierMedia != "")
                {
                    source = GH.Properties.Settings.Default.DossierMedia + "/" + fichierSource;
                }
            }
            if (File.Exists(source) && fichierSource != "")
            {
                File.Copy(source, RepertoireDestination + fichierDestination, true);
                return fichierDestination;
            }
            return null;
        }
        private static string Date_Changement_Bloc(
            GEDCOMClass.CHANGE_DATE info,
            List<ID_numero> liste_note_ID_numero,
            int tab, 
            bool multi_ligne = false)
        {
            if (GH.Properties.Settings.Default.voir_date_changement == false) 
                return "";
            if (
                info.N1_CHAN_DATE != null ||
                info.N2_CHAN_DATE_TIME != null ||
                info.N1_CHAN_NOTE_ID_liste != null
                )
            {
                string texte = "";
                string espace = Tabulation(tab);
                // date dernier changement
                texte += Separation("mince", tab);
                texte += espace + "<div class=\"dateChangement\" >\n";
                texte += espace + "\tDernier changement,\n";
                if (multi_ligne) texte += espace + "\t<br/>\n";
                texte += espace + "\t" + GEDCOMClass.ConvertirDateTexte(info.N1_CHAN_DATE) + " " + info.N2_CHAN_DATE_TIME + "\n";
                if (multi_ligne && info.N1_CHAN_NOTE_ID_liste != null) texte += espace + "\t\n";
                texte += Avoir_lien_note(info.N1_CHAN_NOTE_ID_liste, liste_note_ID_numero, false, tab + 1);
                texte += espace + "</div>\n";
                return texte;
            }
            else return "";
        }
        private static bool DossierHTMLValide()
        {
            if (!Directory.Exists(GH.Properties.Settings.Default.DossierHTML))
            {
                MessageErreur("S.V.P. Spécifiez dans les paramêtres, le dossier des fiches HTML.");
                return false;
            }
            return true;
        }
        private (string, bool) Groupe_depot(
            List<ID_numero> liste_note_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            int tab = 0)
        {
            if (liste_repo_ID_numero == null) 
                return ("", false);
            if (liste_repo_ID_numero.Count == 0) 
                return ("", false);
            string espace = Tabulation(tab);
            string texte = Separation("large", tab);
            texte += "<a id=\"groupe_depot\"></a>\n";
            texte += Groupe("debut", tab);
            texte += espace + "\t<table class=\"titre\">\n";
            texte += espace + "\t\t<tr>\n";
            texte += espace + "\t\t\t<td>\n";
            texte += espace + "\t\t\t\t<img  style=\"vertical-align:middle;heigh:64px\" src=\"..\\commun\\depot.svg\" />\n";
            if (liste_repo_ID_numero.Count == 1) texte += espace + "\t\t\t\tDépôt\n";
            else texte += espace + "\t\t\t\tDépôts\n";
            texte += espace + "\t\t\t</td>\n";
            texte += espace + "\t\t</tr>\n";
            texte += espace + "\t</table>\n";
            texte += Separation("mince", tab + 1);
            foreach (ID_numero record in liste_repo_ID_numero)
            {
                GEDCOMClass.REPOSITORY_RECORD info = GEDCOMClass.Avoir_Info_Repo(record.ID);
                {
                    if (info != null)
                    {
                        texte += espace + "\t<a id=\"depot-" + record.numero.ToString() + "\"></a>\n";
                        texte += espace + "\t<table class=\"tableau\">\n";
                        texte += espace + "\t\t<tr>\n";
                        texte += espace + "\t\t\t<td style=\"background-color:#BBB;border:2px solid #000;margin-left:5px;vertical-align:top;\" colspan=2>\n";// ne pas enlever Border.
                        texte += espace + "\t\t\t\t<span class=\"depot\">" + record.numero.ToString() + "</span> \n";
                        texte += espace + "\t\t\t\t<span style=\"display:table-cell;text-align:top; padding-left:5px\">\n";
                        texte += espace + "\t\t\t\t\t" + info.N1_NAME + "\n";


                        if (GH.Properties.Settings.Default.deboguer) texte += espace + "\t\t\t\t\t" + " {" + info.N0_ID + "}\n";
                        texte += espace + "\t\t\t\t</span>\n";
                        texte += espace + "\t\t\t</td>\n";
                        texte += espace + "\t\t</tr>\n";
                        // adresse
                        GEDCOMClass.ADDRESS_STRUCTURE InfoAdresse = info.N1_ADDR;
                        if (InfoAdresse != null)
                        {
                            texte += espace + "\t<tr>\n";
                            texte += espace + "\t\t<td  class=\"indexCol1\">\n";
                            texte += espace + "\t\t\tAdresse\n";
                            texte += espace + "\t\t</td>\n";
                            texte += espace + "\t\t<td>\n";
                            if (InfoAdresse.N0_ADDR != null) texte += espace + "\t\t\t<br/>1" + InfoAdresse.N0_ADDR + "\n";
                            if (InfoAdresse.N1_ADR1 != null) texte += espace + "\t\t\t<br/>2" + InfoAdresse.N1_ADR1 + "\n";
                            if (InfoAdresse.N1_ADR1 != null) texte += espace + "\t\t\t<br/>3" + InfoAdresse.N1_ADR2 + "\n";
                            if (InfoAdresse.N1_ADR3 != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_ADR3 + "\n";
                            if (InfoAdresse.N1_CITY != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_CITY + "\n";
                            if (InfoAdresse.N1_STAE != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_STAE + "\n";
                            if (InfoAdresse.N1_CTRY != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_CTRY + "\n";
                            if (InfoAdresse.N1_POST != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_POST + "\n";
                            texte += espace + "\t\t</td>\n";
                            texte += espace + "\t</tr>\n";
                        }
                        // REPO téléphone
                        if (info.N1_PHON_liste != null)
                        {
                            bool premier = true;
                            foreach (string s in info.N1_PHON_liste)
                            {
                                texte += "\t\t\t\t<tr>\n";
                                texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                                if (premier)
                                {
                                    texte += "\t\t\t\t\t\tTéléphone\n";
                                    premier = false;
                                }
                                else texte += "\t\t\t\t\t\t&nbsp;\n";
                                texte += "\t\t\t\t\t</td>\n";
                                texte += "\t\t\t\t\t<td>\n";
                                texte += "\t\t\t\t\t\t" + s + "\n";
                                texte += "\t\t\t\t\t</td>\n";
                                texte += "\t\t\t\t</tr>\n";
                            }
                        }
                        // REPO fax
                        if (info.N1_FAX_liste != null)
                        {
                            bool premier = true;
                            foreach (string s in info.N1_FAX_liste)
                            {
                                texte += "\t\t\t\t<tr>\n";
                                texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                                if (premier)
                                {
                                    texte += "\t\t\t\t\t\tTélécopieur\n";
                                    premier = false;
                                }
                                else texte += "\t\t\t\t\t\t&nbsp;\n";
                                texte += "\t\t\t\t\t</td>\n";
                                texte += "\t\t\t\t\t<td>\n";
                                texte += "\t\t\t\t\t\t" + s + "\n";
                                texte += "\t\t\t\t\t</td>\n";
                                texte += "\t\t\t\t</tr>\n";
                            }
                        }
                        // REPO courriel
                        if (info.N1_EMAIL_liste != null)
                        {
                            bool premier = true;
                            foreach (string s in info.N1_EMAIL_liste)
                            {
                                texte += "\t\t\t\t<tr>\n";
                                texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                                if (premier)
                                {
                                    texte += "\t\t\t\t\t\tCourriel\n";
                                    premier = false;
                                }
                                else texte += "\t\t\t\t\t\t&nbsp;\n";
                                texte += "\t\t\t\t\t</td>\n";
                                texte += "\t\t\t\t\t<td>\n";
                                texte += "\t\t\t\t\t\t" + s + "\n";
                                texte += "\t\t\t\t\t</td>\n";
                                texte += "\t\t\t\t</tr>\n";
                            }
                        }
                        // REPO WWW
                        if (info.N1_WWW_liste != null)
                        {
                            bool premier = true;
                            foreach (string s in info.N1_WWW_liste)
                            {
                                texte += "\t\t\t\t<tr>\n";
                                texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                                if (premier)
                                {
                                    texte += "\t\t\t\t\t\tSite Web\n";
                                    premier = false;
                                }
                                else texte += "\t\t\t\t\t\t&nbsp;\n";
                                texte += "\t\t\t\t\t</td>\n";
                                texte += "\t\t\t\t\t<td>\n";
                                texte += "\t\t\t\t\t\t" + s + "\n";
                                texte += "\t\t\t\t\t</td>\n";
                                texte += "\t\t\t\t</tr>\n";
                            }
                        }
                        // REFN
                        if (info.N1_REFN_liste != null)
                        {
                            foreach (GEDCOMClass.USER_REFERENCE_NUMBER info_REFN in info.N1_REFN_liste)
                            {
                                texte += espace + "\t<tr>\n";
                                texte += espace + "\t\t<td colspan=2>\n";
                                texte += espace + "\t\t\tNuméro de fichier d'enregistrement permanent(REFN) " + info_REFN.N0_REFN + " Type " + info_REFN.N1_TYPE + "\n";
                                texte += espace + "\t\t</td>\n";
                                texte += espace + "\t</tr>\n";

                            }
                        }
                        // RIN
                        if (info.N1_RIN != null)
                        {
                            texte += espace + "\t<tr>\n";
                            texte += espace + "\t\t<td colspan=2>\n";
                            texte += espace + "\t\t\tID d'enregistrement automatisé(RIN) " + info.N1_RIN + "\n";
                            texte += espace + "\t\t</td>\n";
                            texte += espace + "\t</tr>\n";
                        }
                        // note
                        if (info.N1_NOTE_liste_ID != null)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td style=\"border:0px solid #0e0;text-align:left;\" colspan=2>\n";
                            texte += Avoir_lien_note(info.N1_NOTE_liste_ID, liste_note_ID_numero, true, tab + 4);
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // date changement *********************************************************************************************
                        if (info.N1_CHAN != null && GH.Properties.Settings.Default.voir_date_changement)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td style=\"border:0px solid #0e0;text-align:left;\" colspan=2>\n";
                            texte += Date_Changement_Bloc(
                                info.N1_CHAN,
                                liste_note_ID_numero,
                                tab + 4,
                                false);
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        texte += espace + "\t</table>\n";
                    }
                    else
                    {
                        texte += espace + "\t<a id=\"depot-" + record.numero.ToString() + "\"></a>\n";
                        texte += espace + "\t<table class=\"tableau\">\n";
                        texte += espace + "\t\t<tr>\n";
                        texte += espace + "\t\t\t<td style=\"background-color:#BBB;border:2px solid #000;margin-left:5px;vertical-align:top;\" colspan=2>\n";// ne pas enlever Border.
                        texte += espace + "\t\t\t\t<span class=\"depot\">" + record.numero.ToString() + "</span> \n";
                        texte += espace + "\t\t\t\t<span style=\"display:table-cell;text-align:top; padding-left:5px\">\n";
                        texte += espace + "\t\t\t\t\tInformation manquante dans le fichier GEDCOM\n";
                        if (GH.Properties.Settings.Default.deboguer) texte += espace + "\t\t\t\t" + " {" + record.ID + "}\n";
                        texte += espace + "\t\t\t\t</span>\n";
                        texte += espace + "\t\t\t</td>\n";
                        texte += espace + "\t\t</tr>\n";
                        texte += espace + "\t</table>\n";
                    }
                }
                texte += Separation("mince", 0);
            }
            texte += Groupe("fin", tab);
            return (texte, true);
        }
        private (string, int, bool) Groupe_evenement(
            List<GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE> liste,
            string date_naissance_individu,
            string date_naissace_conjoint,
            string date_naissace_conjointe,
            int numeroCarte,
            string sousDossier,
            string dossierSortie,
            List<ID_numero> liste_note_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            int tab)
        {
            if (liste == null) 
                return ("", numeroCarte, false);
            if (liste.Count == 0) 
                return ("", numeroCarte, false);
            string espace = Tabulation(tab);
            string texte = "";
            texte += Separation("large", tab);
            texte += espace + "<a id=\"groupe_evenement\"></a>\n";
            texte += Groupe("debut", tab);
            texte += espace + "\t<table class=\"titre\">\n";
            texte += espace + "\t\t<tr>\n";
            texte += espace + "\t\t\t<td>\n";
            if(liste.Count == 1)
                texte += espace + "\t\t\t\t\tÉvénement\n";
            else
                texte += espace + "\t\t\t\t\tÉvénements\n";
            texte += espace + "\t\t\t</td>\n";
            texte += espace + "\t\t</tr>\n";
            texte += espace + "\t</table>\n";
            Liste_par_date par_date = new Liste_par_date();
            liste.Sort(par_date);
            foreach (GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE info in liste)
            {
                string temp = "";
                (temp, numeroCarte) = Avoir_evenement(
                        info, 
                        date_naissance_individu,
                        date_naissace_conjoint,
                        date_naissace_conjointe,
                        numeroCarte,
                        sousDossier,
                        dossierSortie,
                        liste_note_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        tab + 1);
                texte += temp;
                texte += Groupe("fin", tab);
            }
            return (texte, numeroCarte, true);
        }
        private (string, int, bool) Groupe_ordonnance(
            List<GEDCOMClass.LDS_INDIVIDUAL_ORDINANCE> liste,
            string dateNaissance,
            int numeroCarte,
            List<ID_numero> liste_note_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            int tab)
        {
            if (liste == null) 
                return ("", numeroCarte, false);
            if (liste.Count == 0) 
                return ("", numeroCarte, false);
            string temp;
            string texte = "";
            string date;

            string espace = Tabulation(tab);
            texte += Separation("large", tab);
            texte += espace + "<a id=\"groupe_ordonnance\"></a>\n";
            texte += Groupe("debut", tab);
            texte += espace + "\t<table class=\"titre\">\n";
            texte += espace + "\t\t<tr>\n";
            texte += espace + "\t\t\t<td>\n";
            if (liste.Count == 1)
                texte += espace + "\t\t\t\t\tOrdonnance\n";
            else
                texte += espace + "\t\t\t\t\tOrdonnances\n";
            texte += espace + "\t\t\t</td>\n";
            texte += espace + "\t\t</tr>\n";
            texte += espace + "\t</table>\n";
            foreach (GEDCOMClass.LDS_INDIVIDUAL_ORDINANCE info_LDS in liste)
            {
                texte += Separation("moyen", tab);
                texte += espace + "<table class=\"tableau\">\n";
                if (info_LDS.N0_Type != null)
                {
                    texte += espace + "\t<tr>\n";
                    texte += espace + "\t\t<td class=\"cellule2LMB\" colspan=2>\n";
                    texte += espace + "\t\t\t" + info_LDS.N0_Type + "\n";
                    string age = Avoir_age(dateNaissance, info_LDS.N1_DATE);
                    if (age != null) texte += espace + "\t\t\t à l'age de " + age + "\n";
                        temp = Avoir_lien_citation_source(
                        info_LDS.N1_SOUR_citation_liste_ID,
                        liste_citation_ID_numero,
                    6);
                    texte += temp;
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t\t\t\t\t\t</tr>\n";
                }
                if (info_LDS.N1_DATE != null)
                {
                    if (GH.Properties.Settings.Default.date_longue)
                    {
                        date = GEDCOMClass.ConvertirDateTexte(info_LDS.N1_DATE);
                    }
                    else
                    {
                        date = info_LDS.N1_DATE;
                    }
                    texte += espace + "\t<tr>\n";
                    texte += espace + "\t\t<td style=\"width:70px\">\n";
                    texte += espace + "\t\t\tDate\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td>\n";
                    texte += espace + "\t\t\t" + date + "\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                if (info_LDS.N1_TEMP != null)
                {
                    texte += espace + "\t<tr>\n";
                    texte += espace + "\t\t<td>\n";
                    texte += espace + "\t\t\tTemple\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td>\n";
                    texte += espace + "\t\t\t" + info_LDS.N1_TEMP + "\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                if (info_LDS.N1_PLAC != null)
                {
                    texte += espace + "\t<tr>\n";
                    texte += espace + "\t\t<td>\n";
                    texte += espace + "\t\t\tLieu\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td>\n";
                    texte += espace + "\t\t\t" + info_LDS.N1_PLAC + "\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                if (info_LDS.N1_STAT != null)
                {
                    texte += espace + "\t<tr>\n";
                    texte += espace + "\t\t<td>\n";
                    texte += espace + "\t\t\tStatus\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td>\n";
                    texte += espace + "\t\t\t" + info_LDS.N1_STAT + "\n";
                    if (info_LDS.N2_STAT_DATE != null)
                    {
                        if (GH.Properties.Settings.Default.date_longue)
                        {
                            date = GEDCOMClass.ConvertirDateTexte(info_LDS.N2_STAT_DATE);
                        }
                        else
                        {
                            date = info_LDS.N2_STAT_DATE;
                        }

                        texte += espace + "\t\t\t En date du " + date + "\n";

                    }
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                // note ordonnance
                if (info_LDS.N1_NOTE_liste_ID.Count > 0)
                {
                    texte += espace + "\t<tr>\n";
                    texte += espace + "\t\t<td colspan=2>\n";
                    texte += Avoir_lien_note(info_LDS.N1_NOTE_liste_ID, liste_note_ID_numero, true, 6);
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                texte += espace + "</table>\n";
            }
            return (texte, numeroCarte, true);
        }
        public void Famille(string IDFamille, bool menu, string dossierSortie)
        {
            List<ID_numero> liste_SUBMITTER_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_note_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_citation_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_source_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_repo_ID_numero = new List<ID_numero>();
            (
                liste_SUBMITTER_ID_numero,
                liste_note_ID_numero,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_repo_ID_numero
            ) = Avoir_liste_reference_famille
            (
                IDFamille,
                liste_SUBMITTER_ID_numero,
                liste_note_ID_numero,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_repo_ID_numero
            );
            string temp;
            TextBox Tb_Status = Application.OpenForms["GH"].Controls["Tb_Status"] as TextBox;
            Tb_Status.Text = "Génération de la fiche famille ID " + IDFamille;
            Application.DoEvents();
            string sousDossier = "familles";
            bool groupe_enfant = false;
            bool groupe_media = false;
            bool groupe_evenement;
            bool groupe_attribut;
            bool groupe_chercheur;
            bool groupe_citation = false;
            bool groupe_source = false;
            bool groupe_note = false;
            bool groupe_depot;
            int numeroCarte = 0;
            if (IDFamille == "") return;
            // dossier
            string nomFichierFamille = @dossierSortie + "/familles/" + IDFamille + ".html";
            if (!menu) nomFichierFamille = @dossierSortie + "/familles/page.html";
            if (File.Exists(nomFichierFamille))
            {
                File.Delete(nomFichierFamille);
            }
            // info famille
            GEDCOMClass.FAM_RECORD infoFamille = GEDCOMClass.Avoir_info_famille(IDFamille);
            // mariage
            // conjoint
            bool Ok;
            GEDCOMClass.INDIVIDUAL_RECORD infoHUSB;
            (Ok, infoHUSB) = GEDCOMClass.Avoir_info_individu(infoFamille.N1_HUSB);
            GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE NaissanceConjoint = new GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE();
            GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE DecesConjoint = new GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE();
            if (Ok)
            {
                (_, NaissanceConjoint) = GEDCOMClass.AvoirEvenementNaissance(infoHUSB.N1_EVENT_Liste);
                (_, DecesConjoint) = GEDCOMClass.AvoirEvenementDeces(infoHUSB.N1_EVENT_Liste);
            }
            string nomConjoint = GEDCOMClass.AvoirPremierNomIndividu(infoFamille.N1_HUSB);
            // conjointe
            GEDCOMClass.INDIVIDUAL_RECORD infoWIFE;
            (_, infoWIFE) = GEDCOMClass.Avoir_info_individu(infoFamille.N1_WIFE);
            GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE NaissanceConjointe = new GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE();
            GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE DecesConjointe = new GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE();
            if (Ok)
            {
                (_, NaissanceConjointe) = GEDCOMClass.AvoirEvenementNaissance(infoWIFE.N1_EVENT_Liste);
                (_, DecesConjointe) = GEDCOMClass.AvoirEvenementDeces(infoWIFE.N1_EVENT_Liste);
            }
            string nomConjointe = GEDCOMClass.AvoirPremierNomIndividu(infoFamille.N1_WIFE);
            // @Parent
            string texte = null;
            string IDConjointTexte = null;
            string IDConjointeTexte = null;
            if (GH.Properties.Settings.Default.VoirID == true)
            {
                if (infoFamille.N1_HUSB != "")
                    IDConjointTexte = " [" + infoFamille.N1_HUSB + "]";
                if (infoFamille.N1_WIFE != "")
                    IDConjointeTexte = " [" + infoFamille.N1_WIFE + "]";
            }
            texte += Haut_Page("../", menu);
            // restriction
            if (infoFamille.N1_RESN != null)
            {
                texte += "\t\t\t\t<div class=\"blink3\">\n";
                texte += "\t\t\t\t\tRestriction\n";
                texte += infoFamille.N1_RESN + "\n";
                texte += "\t\t\t\t</div>\n";
            }
            texte += Separation("large", 3);
            texte += "<a id=\"groupe_famille\"></a>\n";
            texte += "\t\t\t<table class=\"titre\">\n";
            texte += "\t\t\t\t<tr>\n";
            texte += "\t\t\t\t\t<td>\n";
            texte += "\t\t\t\t\t\tFamille\n";
            if (GH.Properties.Settings.Default.VoirID == true)
            {
                texte += "\t\t\t\t\t\t<div style=\"font-size:small\">[" + IDFamille + "]</div>" + "\n";
            }
            texte += "\t\t\t\t\t</td>\n";
            texte += "\t\t\t\t</tr>\n";
            texte += "\t\t\t</table>\n";
            texte += Separation("large", 3);
            // nom parents
            texte += "\t\t\t<table class=\"tableau\">\n";
            texte += "\t\t\t\t<thead>\n";
            texte += "\t\t\t\t\t<tr>\n";
            texte += "\t\t\t\t\t\t<th class=\"cellule2LMB\" style=\"width:85px\">&nbsp;</th>\n";
            texte += "\t\t\t\t\t\t<th class=\"cellule2LMB\">Nom</th>\n";
            texte += "\t\t\t\t\t\t<th class=\"cellule2LMB date\">Naissance</th>\n";
            texte += "\t\t\t\t\t\t<th class=\"cellule2LMB date\">Décès</th>\n";
            texte += "\t\t\t\t\t</tr>\n";
            texte += "\t\t\t\t</thead>\n";
            // conjoint
            texte += "\t\t\t\t<tr>\n";
            texte += "\t\t\t\t\t<td class=\"cellule2LTF\">\n";
            if (infoFamille.N1_HUSB != "")
            {
                if (menu)
                {
                    texte += "\t\t\t\t\t<a class=\"ficheIndividu\" href=\"../individus/" + infoFamille.N1_HUSB + ".html\"></a>\n";
                }
                else
                {
                    texte += "\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                }
                texte += "\t\t\t\t\t\tPère\n";
            }
            else
            {
                texte += "\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                texte += "\t\t\t\t\t\tPère\n";
            }
            texte += "\t\t\t\t\t</td>\n";
            texte += "\t\t\t\t\t<td class=\"cellule2LTF\">\n";
            texte += "\t\t\t\t\t\t" + nomConjoint + IDConjointTexte + "\n";
            texte += "\t\t\t\t\t</td>\n";
            string date_naissance_conjoint;
            if (GH.Properties.Settings.Default.date_longue)
            {
                date_naissance_conjoint = GEDCOMClass.ConvertirDateTexte(NaissanceConjoint.N2_DATE);
            }
            else
            {
                date_naissance_conjoint = NaissanceConjoint.N2_DATE;
            }
            texte += "\t\t\t\t\t<td class=\"cellule2LTF date\">\n";
            texte += "\t\t\t\t\t\t" + date_naissance_conjoint + "\n";
            texte += "\t\t\t\t\t</td>\n";
            string date_deces_conjoint;
            if (GH.Properties.Settings.Default.date_longue)
            {
                date_deces_conjoint = GEDCOMClass.ConvertirDateTexte(DecesConjoint.N2_DATE);
            }
            else
            {
                date_deces_conjoint = DecesConjoint.N2_DATE;
            }
            texte += "\t\t\t\t\t<td class=\"cellule2LTF\" date>\n";
            texte += "\t\t\t\t\t\t" + date_deces_conjoint + "\n";
            texte += "\t\t\t\t\t</td>\n";
            texte += "\t\t\t\t</tr>\n";
            // conjointe
            texte += "\t\t\t\t<tr>\n";
            texte += "\t\t\t\t\t<td class=\"cellule2LTF\">\n";
            if (infoFamille.N1_WIFE != null)
            {
                if (menu)
                {
                    texte += "\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"" + infoFamille.N1_WIFE + ".html\"></a>\n";
                }
                else
                {
                    texte += "\t\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                }
                texte += "\t\t\t\t\t\t\tMère\n";
            }
            else
            {
                texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                texte += "\t\t\t\t\t\t\tMère\n";
            }
            texte += "\t\t\t\t\t</td>\n";
            texte += "\t\t\t\t\t<td class=\"cellule2LTF\">\n";
            texte += "\t\t\t\t\t\t" + nomConjointe + IDConjointeTexte + "\n";
            // ordonnance
            if (infoFamille.N1_SLGS.N1_DATE != null ||
                infoFamille.N1_SLGS.N1_TEMP != null ||
                infoFamille.N1_SLGS.N1_PLAC != null ||
                infoFamille.N1_SLGS.N1_STAT != null ||
                infoFamille.N1_SLGS.N2_STAT_DATE != null
                )
            {
                texte += Separation("mince", 6);
                texte += "\t\t\t\t\t\t<table>\n";
                texte += "\t\t\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t\t\t<td class=\"cellule2LMB\">Ordonnance</td>";
                texte += "\t\t\t\t\t\t\t\t</td>";
                texte += "\t\t\t\t\t\t\t</tr>\n";
                texte += "\t\t\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t\t\t<td class=\"cellule2LMF\">\n";
                string date_SLGS;
                if (infoFamille.N1_SLGS.N1_DATE != null)
                {
                    if (GH.Properties.Settings.Default.date_longue)
                    {
                        date_SLGS = GEDCOMClass.ConvertirDateTexte(infoFamille.N1_SLGS.N1_DATE);
                    }
                    else
                    {
                        date_SLGS = infoFamille.N1_SLGS.N1_DATE;
                    }
                    texte += Separation("mince", 6);
                    texte += "\t\t\t\t\t\tDate:  " + date_SLGS;
                }
                if (infoFamille.N1_SLGS.N1_TEMP != null)
                {
                    texte += Separation("mince", 6);
                    texte += "\t\t\t\t\t\tTemple: " + infoFamille.N1_SLGS.N1_TEMP;
                }
                if (infoFamille.N1_SLGS.N1_PLAC != null)
                {
                    texte += Separation("mince", 6);
                    texte += "\t\t\t\t\t\tLieu: " + infoFamille.N1_SLGS.N1_PLAC;
                }
                if (infoFamille.N1_SLGS.N1_STAT != null)
                {
                    texte += Separation("mince", 6);
                    texte += "\t\t\t\t\t\tStatus: " + infoFamille.N1_SLGS.N1_STAT;
                }
                if (infoFamille.N1_SLGS.N2_STAT_DATE != null)
                {
                    if (GH.Properties.Settings.Default.date_longue)
                    {
                        date_SLGS = GEDCOMClass.ConvertirDateTexte(infoFamille.N1_SLGS.N2_STAT_DATE);
                    }
                    else
                    {
                        date_SLGS = infoFamille.N1_SLGS.N2_STAT_DATE;
                    }

                    texte += "&nbsp;En date du " + date_SLGS;

                }
                texte += Avoir_lien_note(infoFamille.N1_SLGS.N1_NOTE_liste_ID, liste_note_ID_numero, true, 6);
                temp = Avoir_lien_citation_source(infoFamille.N1_SLGS.N1_SOUR_citation_liste_ID, liste_citation_ID_numero, 6);
                texte += temp;
                texte += "\t\t\t\t\t\t\t\t</td>";
                texte += "\t\t\t\t\t\t\t</tr>\n";
                texte += "\t\t\t\t\t\t</table>\n";
            }
            // date naissance conjointe
            texte += "\t\t\t\t\t</td class=\"cellule2LTF date\">\n";
            string date_naissance_conjointe;
            if (GH.Properties.Settings.Default.date_longue)
            {
                date_naissance_conjointe = GEDCOMClass.ConvertirDateTexte(NaissanceConjointe.N2_DATE);
            }
            else
            {
                date_naissance_conjointe = NaissanceConjointe.N2_DATE;
            }
            texte += "\t\t\t\t\t<td class=\"cellule2LTF date\">\n";
            texte += "\t\t\t\t\t\t" + date_naissance_conjointe + "\n";
            texte += "\t\t\t\t\t</td>\n";
            string date_deces_conjointe;
            if (GH.Properties.Settings.Default.date_longue)
            {
                date_deces_conjointe = GEDCOMClass.ConvertirDateTexte(DecesConjointe.N2_DATE);
            }
            else
            {
                date_deces_conjointe = DecesConjointe.N2_DATE;
            }
            texte += "\t\t\t\t\t<td class=\"cellule2LTF date\">\n";
            texte += "\t\t\t\t\t\t" + date_deces_conjointe + "\n";
            texte += "\t\t\t\t\t</td>\n";
            texte += "\t\t\t\t</tr>\n";
            texte += "\t\t\t</table>\n";

            bool OkDate = false;
            if ((infoFamille.N1_CHAN.N1_CHAN_DATE != null ||
                infoFamille.N1_CHAN.N2_CHAN_DATE_TIME !=null ||
                infoFamille.N1_CHAN.N1_CHAN_NOTE_ID_liste != null)
                 && GH.Properties.Settings.Default.voir_date_changement
                )
            {
                OkDate = true;
            }
            if (infoFamille.N1_NCHI != null ||
                infoFamille.N1_REFN_liste.Count > 0 ||
                infoFamille.N1_RIN != null ||
                infoFamille.N1_NOTE_liste_ID.Count > 0 ||
                infoFamille.N1_SUBM_liste_ID.Count > 0 ||
                OkDate
                )
            {
                texte += "\t\t\t<table class=\"tableau tableauFin\">\n";
                texte += " \t\t\t\t<tr>\n";
                texte += " \t\t\t\t\t<td>\n";


                // TYPU pour Ancestrologie
                infoFamille.N1_TYPU = "1";
                if (infoFamille.N1_TYPU != null)
                {
                    texte += "\t\t\t\t\t\t<div>\n";
                    texte += "\t\t\t\t\t\t\tType d'union (Ancestrologie) " + infoFamille.N1_TYPU + "\n";
                    texte += "\t\t\t\t\t\t</div>\n";
                }

                //nombre d'enfant
                if (infoFamille.N1_NCHI != null)
                {
                    texte += "\t\t\t\t\t\t<div>\n";
                    texte += "\t\t\t\t\t\t\tNombre d'enfant " + infoFamille.N1_NCHI + "\n";
                    texte += "\t\t\t\t\t\t</div>\n";
                }
                // REFN
                if (infoFamille.N1_REFN_liste.Count > 0)
                {
                    foreach (GEDCOMClass.USER_REFERENCE_NUMBER info in infoFamille.N1_REFN_liste)
                    {
                        texte += "\t\t\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t\tNuméro de fichier d'enregistrement permanent(REFN) " + info.N0_REFN + " Type " + info.N1_TYPE + "\n";
                        texte += "\t\t\t\t\t\t</div>\n";
                    }
                }
                // RIN
                if (infoFamille.N1_RIN != null)
                {
                    texte += "\t\t\t\t\t\t<div>\n";
                    texte += "\t\t\t\t\t\tID d'enregistrement automatisé(RIN) " + infoFamille.N1_RIN + "\n";
                    texte += "\t\t\t\t\t\t</div>\n";
                }
                // chercheur
                if (infoFamille.N1_SUBM_liste_ID.Count > 0 && GH.Properties.Settings.Default.voir_chercheur)
                {
                    texte += Separation("mince", 5);
                    texte += "\t\t\t\t\t<div>\n";
                    texte += Avoir_lien_chercheur(infoFamille.N1_SUBM_liste_ID, liste_SUBMITTER_ID_numero, 6);
                    texte += "\t\t\t\t\t</div>\n";
                }
                // Source ********************************************************************************************************
                if (infoFamille.N1_SOUR_citation_liste_ID.Count > 0)
                {
                    texte += Separation("mince", 4);
                    texte += "\t\t\t\t<div>\n";
                    texte += Avoir_lien_citation_source(infoFamille.N1_SOUR_citation_liste_ID, liste_citation_ID_numero, 6);
                    texte += "\t\t\t\t</div>\n";
                }
                // note ********************************************************************************************************
                if (infoFamille.N1_NOTE_liste_ID.Count > 0)
                {
                    texte += Separation("mince", 4);
                    texte += "\t\t\t\t<div>\n";
                    texte += Avoir_lien_note(infoFamille.N1_NOTE_liste_ID, liste_note_ID_numero, true, 5);
                    texte += "\t\t\t\t</div>\n";
                }
                // date changement *********************************************************************************************
                texte += Date_Changement_Bloc(
                        infoFamille.N1_CHAN, 
                        liste_note_ID_numero, 
                        6, 
                        false);
                //texte += temp;
                texte += "\t\t\t\t\t</td>\n";
                texte += " \t\t\t\t</tr>\n";
                texte += "\t\t\t</table>\n";
            }
            // @enfant *****************************************************************************************************
            List<string> listIDEnfant = GEDCOMClass.AvoirListIDEnfant(IDFamille);
            if (listIDEnfant.Count() > 0)
            {
                texte += Separation("large", 3);
                texte += "<a id=\"groupe_enfant\"></a>\n";
                groupe_enfant = true;
                texte += Groupe("debut", 3);
                texte += "\t\t\t\t<table class=\"titre\">\n";
                texte += "\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t<td>\n";
                texte += "\t\t\t\t\t\t\tEnfant\n";
                texte += "\t\t\t\t\t\t</td>\n";
                texte += "\t\t\t\t\t</tr>\n";
                texte += "\t\t\t\t</table>\n";
                texte += Separation("mince", 4);
                texte += "\t\t\t\t<!-- Table enfant -->\n";
                texte += "\t\t\t\t<table class=\"tableau\">\n";
                texte += "\t\t\t\t\t<thead>\n";
                texte += "\t\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"listeEnfant\" style='width:85px'>Voir</th>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"listeEnfant\">Non</th>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"listeEnfant date\">Naisannce</th>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"listeEnfant date\">Décès</th>\n";
                texte += "\t\t\t\t\t\t</tr>\n";
                texte += "\t\t\t\t\t</thead>\n";
                string txtIDEfant;
                foreach (string IDEnfant in listIDEnfant)
                {
                    if (IDEnfant != null)
                    {
                        if (GH.Properties.Settings.Default.VoirID)
                        {
                            txtIDEfant = " [" + IDEnfant + "]";
                        }
                        else
                        {
                            txtIDEfant = null;
                        }
                        GEDCOMClass.INDIVIDUAL_RECORD infoEnfant;
                        (Ok, infoEnfant) = GEDCOMClass.Avoir_info_individu(IDEnfant);
                        bool OkNEnfant;
                        GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE NaissanceEnfant;
                        (OkNEnfant, NaissanceEnfant) = GEDCOMClass.AvoirEvenementNaissance(infoEnfant.N1_EVENT_Liste);
                        bool OkDEnfant;
                        GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE DecesEnfant;
                        (OkDEnfant, DecesEnfant) = GEDCOMClass.AvoirEvenementDeces(infoEnfant.N1_EVENT_Liste);
                        string nomEnfant = GEDCOMClass.AvoirPremierNomIndividu(IDEnfant);
                        List<GEDCOMClass.PERSONAL_NAME_STRUCTURE> InfoNomEnfant;
                        (_, InfoNomEnfant) = GEDCOMClass.AvoirListeNom(IDEnfant);
                        texte += "\t\t\t\t\t<tr class=\"listeEnfant\">\n";
                        texte += "\t\t\t\t\t\t<td class=\"listeEnfant\">\n";
                        if (IDEnfant != null)
                        {
                            if (menu)
                            {
                                texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"../individus/" + 
                                    IDEnfant + ".html\"></a>\n";
                            }
                            else
                            {
                                texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                            }
                        }
                        texte += "\t\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t\t<td class=\"listeEnfant\">\n";
                        texte += "\t\t\t\t\t\t\t" + nomEnfant + txtIDEfant + "\n";
                        string adoptionID = GEDCOMClass.AvoirIDAdoption(IDEnfant);
                        if (adoptionID != null)
                        {
                            GEDCOM.GEDCOMClass.INDIVIDUAL_RECORD infoIndividuEnfant;
                            (Ok, infoIndividuEnfant) = GEDCOMClass.Avoir_info_individu(IDEnfant);
                            string pereIDAdoption = GEDCOMClass.Avoir_famille_conjoint_ID(adoptionID);
                            string nomPereAdoption = GEDCOMClass.AvoirPrenomPatronymeIndividu(pereIDAdoption);
                            string mereIDAdoption = GEDCOMClass.Avoir_famille_conjointe_ID(adoptionID);
                            string nomMereAdoption = GEDCOMClass.AvoirPrenomPatronymeIndividu(mereIDAdoption);
                            texte += "\t\t\t\t\t\t\t< br />Famille de " + nomPereAdoption + " et " + nomMereAdoption + "\n";
                        }
                        texte += "\t\t\t\t\t\t</td>\n";
                        string date_naissance_enfant;
                        if (GH.Properties.Settings.Default.date_longue)
                        {
                            date_naissance_enfant = GEDCOMClass.ConvertirDateTexte(NaissanceEnfant.N2_DATE);
                        }
                        else
                        {
                            date_naissance_enfant = NaissanceEnfant.N2_DATE;
                        }
                        texte += "\t\t\t\t\t\t<td class=\"listeEnfant\">\n";
                        texte += "\t\t\t\t\t\t\t" + date_naissance_enfant + "\n";
                        texte += "\t\t\t\t\t\t</td>\n";
                        string date_deces_enfant;
                        if (GH.Properties.Settings.Default.date_longue)
                        {
                            date_deces_enfant = GEDCOMClass.ConvertirDateTexte(DecesEnfant.N2_DATE);
                        }
                        else
                        {
                            date_deces_enfant = DecesEnfant.N2_DATE;
                        }
                        texte += "\t\t\t\t\t\t<td class=\"listeEnfant\">\n";
                        texte += "\t\t\t\t\t\t\t" + date_deces_enfant + "\n";
                        texte += "\t\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t</tr>\n";
                    }
                }
                if (listIDEnfant.Count == 0)
                {
                    for (int f = 0; f < 2; f++)
                    {
                        texte += "    \t\t\t\t<tr>\n";
                        texte += "                        <td class=\"listeEnfant\">\n";
                        texte += "                            <a class=\"ficheIndividuGris\"></a>\n";
                        texte += "    \t\t\t\t\t</td>\n";
                        texte += "                        <td class=\"listeEnfant\">\n";
                        texte += "    \t\t\t\t\t</td>\n";
                        texte += "                        <td class=\"listeEnfant\">\n";
                        texte += "    \t\t\t\t\t</td>\n";
                        texte += "                        <td class=\"listeEnfant\">\n";
                        texte += "    \t\t\t\t\t</td>\n";
                        texte += "    \t\t\t\t<tr>\n";
                    }
                }
                texte += "                </table>\n";
                texte += Groupe("fin", 3);
            }
            // média *******************************************************************************************************
            if (GH.Properties.Settings.Default.voir_media == true && infoFamille.N1_OBJE_liste.Count > 0)
            {
                if (infoFamille.N1_OBJE_liste != null)
                {

                    texte += Separation("large", 3);
                    texte += "<a id=\"groupe_media\"></a>\n";
                    groupe_media = true;
                    texte += Groupe("debut", 3);
                    texte += "\t\t\t\t<table class=\"titre\">\n";
                    texte += "\t\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t\t<td>\n";
                    if (infoFamille.N1_OBJE_liste.Count == 1) texte += "\t\t\t\t\t\t\tMédia\n";
                    else texte += "\t\t\t\t\t\t\tMédias\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t</tr>\n";
                    texte += "\t\t\t\t</table>\n";
                    texte += Separation("mince", 4);
                    texte += "\t\t\t\t<table class=\"tableauMedia\">\n";
                    texte += "\t\t\t\t\t<tr>\n";
                    int cRanger = 0;
                    foreach (string IDMedia in infoFamille.N1_OBJE_liste)
                    {
                        GEDCOMClass.MULTIMEDIA_RECORD mediaInfo = GEDCOMClass.Avoir_info_media(IDMedia);
                        texte += "\t\t\t\t\t\t<td class=\"tableauMedia\">\n";
                        (temp, liste_note_ID_numero, liste_citation_ID_numero, liste_source_ID_numero, liste_repo_ID_numero) = Objet(
                            mediaInfo, "familles", dossierSortie,
                            liste_note_ID_numero,
                            liste_citation_ID_numero,
                            liste_source_ID_numero,
                            liste_repo_ID_numero,
                            6);
                        texte += temp;
                        texte += "\t\t\t\t\t\t</td>\n";
                        cRanger++;
                        if (cRanger % 3 == 0)
                        {
                            texte += "\t\t\t\t\t</tr>\n";
                            texte += "\t\t\t\t\t<tr>\n";
                        }
                    }
                    if (cRanger % 3 == 1)
                    {
                        texte += "\t\t\t\t\t\t<td class=\"tableauMedia\">\n";
                        texte += "\t\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t\t<td class=\"tableauMedia\">\n";
                        texte += "\t\t\t\t\t\t</td>\n";
                    }
                    if (cRanger % 3 == 2)
                    {
                        texte += "\t\t\t\t\t\t<td class=\"tableauMedia\">\n";
                        texte += "\t\t\t\t\t\t</td>\n";
                    }
                    texte += "\t\t\t\t\t</tr>\n";
                    texte += "\t\t\t\t</table>\n";
                    texte += Groupe("fin", 3);
                }
            }
            //@Événement ****************************************************************************************************************************
            (temp, _, groupe_evenement) = Groupe_evenement(
                    infoFamille.N1_EVENT_Liste,
                    "", 
                    NaissanceConjoint.N2_DATE, 
                    NaissanceConjointe.N2_DATE, 
                    numeroCarte, 
                    sousDossier, 
                    dossierSortie,
                    liste_note_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    0);
            texte += temp;
            //@Attribut *************************************************************************************
            (temp, _, groupe_attribut) = Groupe_attribut(
                    infoFamille.N1_ATTRIBUTE_liste, 
                    numeroCarte, 
                    sousDossier, 
                    dossierSortie,
                    liste_note_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    0);
            texte += temp;
            //@chercheur Groupe
            (temp, groupe_chercheur) = Groupe_chercheur(
                    liste_SUBMITTER_ID_numero, 
                    dossierSortie, 
                    "familles",
                    liste_note_ID_numero,
                    3);
            texte += temp;
            // @Citation **************************************************************************************************************
            if (liste_citation_ID_numero.Count() > 0)
            {
                (temp, groupe_citation) =
                    Groupe_citation(
                        dossierSortie,
                        "familles",
                        liste_note_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        3);
                texte += temp;
            }
            // @Source **************************************************************************************************************
            if (liste_source_ID_numero.Count() > 0)
            {
                (temp, groupe_source) = Groupe_source
                        (
                        dossierSortie,
                        "familles",
                        liste_note_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        3);
                texte += temp;
            }
            // @Depot Groupe **********************************************************************************************
            (temp, groupe_depot) = Groupe_depot( liste_note_ID_numero, liste_repo_ID_numero,3);
            texte += temp;
            // @ Group note **********************************************************************************************
            if (liste_note_ID_numero.Count() > 0)
            {
                (temp, groupe_note) = Groupe_note("famille", liste_note_ID_numero, liste_citation_ID_numero, 3);
                texte += temp;
            }
            //@menu hamburger
            texte += "\t\t\t\t<div class=\"hamburger\">\n";
            texte += "\t\t\t\t\t<button class=\"boutonHamburger\"></button>\n";
            texte += "\t\t\t\t\t<div class=\"hamburger-contenu\">\n";
            texte += "\t\t\t\t\t\t<a href=\"#groupe_famille\">Famille</a>\n";
            if (groupe_attribut) texte += "\t\t\t\t\t\t<a href=\"#groupe_attribut\">Attribut</a>\n";
            if (groupe_chercheur) texte += "\t\t\t\t\t\t<a href=\"#groupe_chercheur\">Chercheur</a>\n";
            if (groupe_citation) texte += "\t\t\t\t\t\t<a href=\"#groupe_citation\">Citation</a>\n";
            if (groupe_depot) texte += "\t\t\t\t\t\t<a href=\"#groupe_depot\">Dépôt</a>\n";
            if (groupe_enfant) texte += "\t\t\t\t\t\t<a href=\"#groupe_enfant\">Enfant</a>\n";
            if (groupe_evenement) texte += "\t\t\t\t\t\t<a href=\"#groupe_evenement\">Événement</a>\n";
            if (groupe_media) texte += "\t\t\t\t\t\t<a href=\"#groupe_media\">Média</a>\n";
            if (groupe_note) texte += "\t\t\t\t\t\t<a href=\"#groupe_note\">Note</a>\n";
            if (groupe_source) texte += "\t\t\t\t\t\t<a href=\"#groupe_source\">Source</a>\n";
            texte += "\t\t\t\t\t</div>\n";
            texte += "\t\t\t\t</div>\n";
            texte += Bas_Page();
            try
            {
                System.IO.File.WriteAllText(nomFichierFamille, texte);
            }
            catch (Exception msg)
            {
                string message = "Ne peut pas écrire le fichier" + nomFichierFamille + ".";
                GEDCOMClass.Voir_message(message, msg.Message, Avoir_code_erreur());
            }
        }
        private static string Groupe(string g, int tab)
        {
            string espace = Tabulation(tab);
            if (g == "debut") return espace + "<div class=\"grouper\"><!-- Début du groupe -->\n";
            if (g == "fin") return espace + "</div><!-- Fin du groupe -->\n";
            return "";
        }
        private string Haut_Page(string origine, bool menu)
        {
            string texte = "<!DOCTYPE html>\n";
            texte += "<html lang=\"fr\" style=\"background-color:#FFF;\">\n";
            texte += "\t<head>\n";
            texte += "\t\t<meta charset = 'UTF-8' />\n";
            texte += "\t\t\t<title>GH</title>\n";
            texte += "\t\t<link rel=\"stylesheet\" href=\"" + origine + "commun/dapam.css\" type=\"text/css\" />\n";
            texte += "\t\t<link rel=\"stylesheet\" href=\"" + origine + "commun/leaflet.css\" type=\"text/css\" />\n";
            texte += "\t\t<script  src=\"../commun/modal.js\"></script>\n";
            texte += "\t\t<script  src=\"../commun/leaflet.js\"></script>\n";
            texte += "\t</head>\n";
            texte += "\t<body>\n";
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
                texte += "\t\t\t\t\t<img style=\"height:40px\" src=\"" + origine + "commun/familleConjoint.svg\" alt=\"\" />\n";
                texte += "\t\t\t\t\tFamille\n";
                texte += "\t\t\t\t\t<img style =\"height:40px\" src=\"" + origine + "commun/male.svg\" alt=\"\" />\n";
                texte += "\t\t\t\t</a>\n";
                texte += "\t\t\t\t<a class=\"poussoir\" href=\"" + origine + "familles/indexConjointe.html\">\n";
                texte += "\t\t\t\t\t<img style=\"height:40px\" src=\"" + origine + "commun/familleConjointe.svg\" alt=\"\" />\n";
                texte += "\t\t\t\t\tFamille\n";
                texte += "\t\t\t\t\t<img style =\"height:40px\" src=\"" + origine + "commun/femelle.svg\" alt=\"\" />\n";
                texte += "\t\t\t\t</a>\n";
                texte += "\t\t\t</div>\n";
                texte += "\t\t</header>\n";
            }
            texte += "\t\t<div class=\"page\"><!-- haut de page -->\n";
            return texte;
        }
        public void Index(string fichierGEDCOM, string nombreIndividu, string nombreFamille, string dossierSortie)
        {
            List<ID_numero> liste_note_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_citation_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_source_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_repo_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_SUBMITTER_ID_numero = new List<ID_numero>();
            GEDCOMClass.HEADER InfoGEDCOM = GEDCOMClass.AvoirInfoGEDCOM();
            Verifier_liste(InfoGEDCOM.N1_SUBM_liste_ID, liste_SUBMITTER_ID_numero);
            GEDCOMClass.SUBMISSION_RECORD Info_SUBMISSION_RECORD = GEDCOMClass.AvoirInfoSUBMISSION_RECORD();
            Verifier_liste(Info_SUBMISSION_RECORD.N1_SUBM_liste_ID, liste_SUBMITTER_ID_numero);
            string nomFichier = @dossierSortie + "/index.html";
            string texte = "";
            string temp;
            bool groupe_GEDCOM = true;
            bool groupe_information = true;
            bool groupe_logiciel = true;
            bool groupe_chercheur;
            bool groupe_citation = false;
            bool groupe_source = false;
            bool groupe_depot;
            bool groupe_note = false;
            texte += Haut_Page("", true);
            //int numero_chercheur = 0;
            TextBox Tb_Status = Application.OpenForms["GH"].Controls["Tb_Status"] as TextBox;
            Tb_Status.Text = "Génération de la page couverture";
            Application.DoEvents();
            // GEDCOM ******************************************************************************************************************************
            {
                texte += "<a id=\"groupe_GEDCOM\"></a>\n";
                texte += Separation("large", 3);
                texte += "\t\t\t<table class=\"titre\">\n";
                texte += "\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t<td>\n";
                texte += "\t\t\t\t\t\tGEDCOM\n";
                texte += "\t\t\t\t\t</td>\n";
                texte += "\t\t\t\t</tr>\n";
                texte += "\t\t\t</table>\n";
                texte += Separation("mince", 3);
                texte += "\t\t\t<table class=\"tableau\">\n";
                // version
                if (InfoGEDCOM.N2_GEDC_VERS != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                    texte += "\t\t\t\t\t\tVersion" + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N2_GEDC_VERS + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // Fichier original
                if (InfoGEDCOM.N1_FILE != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tFichier original" + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N1_FILE + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // Fichier
                if (fichierGEDCOM != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tFichier" + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + fichierGEDCOM + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // date heure
                if (InfoGEDCOM.N1_DATE != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tCréer le\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N1_DATE + " " + InfoGEDCOM.N2_DATE_TIME + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                //char set
                if (InfoGEDCOM.N1_CHAR != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tCaractère set" + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N1_CHAR + "\n";
                    if (InfoGEDCOM.N2_CHAR_VERS != "") texte += "\t\t\t\t\t\tVersion " + InfoGEDCOM.N2_CHAR_VERS + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // language
                if (InfoGEDCOM.N1_LANG != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tLangue" + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N1_LANG + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // place
                if (InfoGEDCOM.N2_PLAC_FORM != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tPlace" + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N2_PLAC_FORM + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // copyright
                if (InfoGEDCOM.N1_COPR != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tCopyright\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N1_COPR + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                if (nombreIndividu != "00")
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tNombre d'individu\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + nombreIndividu + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                if (nombreFamille != "00")
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tNombre de famille\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + nombreFamille + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // destination
                if (InfoGEDCOM.N1_DEST != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tRéception par\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N1_DEST + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // date transmission
                if (InfoGEDCOM.N1_DATE != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tDate transmission\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    string date;
                    if (GH.Properties.Settings.Default.date_longue)
                    {
                        date = GEDCOMClass.ConvertirDateTexte(InfoGEDCOM.N1_DATE);
                    }
                    else
                    {
                        date = InfoGEDCOM.N1_DATE;
                    }
                    texte += "\t\t\t\t\t\t" + date + " à " + InfoGEDCOM.N2_DATE_TIME + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                //chercheur
                if (InfoGEDCOM.N1_SUBM_liste_ID != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td colspan=2>\n";
                    texte += Avoir_lien_chercheur(InfoGEDCOM.N1_SUBM_liste_ID, liste_SUBMITTER_ID_numero, 5);
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                    texte += "\t\t\t</tr>\n";
                }
                // note GEDCOM ****************************************************************************
                if (InfoGEDCOM.N1_NOTE != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td colspan=2>\n";
                    texte += InfoGEDCOM.N1_NOTE;
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                texte += "\t\t\t</table>\n";
            }
            // Information d'envoie ************************************************************************
            {
                texte += "<a id=\"groupe_information\"></a>\n";
                if (InfoGEDCOM.N1_SUBN != null)
                {
                    texte += Separation("large", 3);
                    texte += Groupe("debut", 3);
                    texte += "\t\t\t<table class=\"titre\">\n";
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tInformation d'envoie\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                    texte += "\t\t\t</table>\n";

                    texte += Separation("mince", 3);
                    texte += "\t\t\t<table class=\"tableau\">\n";
                    if (Info_SUBMISSION_RECORD.N1_FAMF != null)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td class=\"indexCol1\" style=\"width:170px\">\n";
                        texte += "\t\t\t\t\t\tNom du fichier familial\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\t" + Info_SUBMISSION_RECORD.N1_FAMF + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                    if (Info_SUBMISSION_RECORD.N1_TEMP != null)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                        texte += "\t\t\t\t\t\tCode du temple\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\t" + Info_SUBMISSION_RECORD.N1_TEMP + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                    if (Info_SUBMISSION_RECORD.N1_ANCE != null)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                        texte += "\t\t\t\t\t\tGénération d'ancêtre\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\t" + Info_SUBMISSION_RECORD.N1_ANCE + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                    if (Info_SUBMISSION_RECORD.N1_DESC != null)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                        texte += "\t\t\t\t\t\tGénération de descendant\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\t" + Info_SUBMISSION_RECORD.N1_DESC + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                    if (Info_SUBMISSION_RECORD.N1_ORDI != null)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                        texte += "\t\t\t\t\t\tProcessus d'ordonnance\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\t" + Info_SUBMISSION_RECORD.N1_ORDI + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                    if (Info_SUBMISSION_RECORD.N1_RIN != null)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                        texte += "\t\t\t\t\t\tID d'enregistrement automatisé(RIN) \n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\t" + Info_SUBMISSION_RECORD.N1_RIN + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                    // sousmit par
                    if (Info_SUBMISSION_RECORD.N1_SUBM_liste_ID != null)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td colspan=2>\n";
                        string texteTemp = "";
                        texte += Avoir_lien_chercheur(Info_SUBMISSION_RECORD.N1_SUBM_liste_ID, liste_SUBMITTER_ID_numero, 5);
                        texte += texteTemp.TrimEnd(' ', ',');
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                        texte += "\t\t\t</tr>\n";
                    }
                    // note  du sumiteur *******************************************************************
                    if (Info_SUBMISSION_RECORD.N1_NOTE_liste_ID != null)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td colspan=2>\n";
                        texte += Avoir_lien_note(Info_SUBMISSION_RECORD.N1_NOTE_liste_ID, liste_note_ID_numero, true, 5);
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                    // si changement de date
                    GEDCOMClass.CHANGE_DATE N1_CHAN = Info_SUBMISSION_RECORD.N1_CHAN;
                    if (N1_CHAN != null && GH.Properties.Settings.Default.voir_date_changement)
                    {
                        if (N1_CHAN.N1_CHAN_DATE != null)
                        {
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td colspan=2>\n";
                            texte += Date_Changement_Bloc(
                                    N1_CHAN,
                                    liste_note_ID_numero,
                                    6,
                                    false);
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t</tr>\n";
                        }
                    }
                    texte += "\t\t\t</table>\n";
                    texte += Groupe("fin", 3);
                }
            }
            // Logiciel ************************************************************************************
            if (InfoGEDCOM.N1_SOUR != null)
            {
                texte += "<a id=\"groupe_logiciel\"></a>\n";
                texte += Separation("large", 3);
                texte += Groupe("debut", 3);
                // titre Logiciel
                texte += "\t\t\t<table class=\"titre\">\n";
                texte += "\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t<td>\n";
                texte += "\t\t\t\t\t\tLogiciel\n";
                texte += "\t\t\t\t\t</td>\n";
                texte += "\t\t\t\t</tr>\n";
                texte += "\t\t\t</table>\n";
                texte += Separation("mince", 3);
                texte += "\t\t\t<table class=\"tableau\">\n";
                // SOUR
                if (InfoGEDCOM.N1_SOUR != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                    texte += "\t\t\t\t\t\tID système\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N1_SOUR + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // version
                if (InfoGEDCOM.N2_SOUR_VERS != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tVersion\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N2_SOUR_VERS + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // NANE
                if (InfoGEDCOM.N2_SOUR_NAME != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                    texte += "\t\t\t\t\t\tGénérer par\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N2_SOUR_NAME + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }

                // corporation *************************************************************************************************************************
                if (InfoGEDCOM.N2_SOUR_CORP != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                    texte += "\t\t\t\t\t\t\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N2_SOUR_CORP + "\n";
                    GEDCOMClass.ADDRESS_STRUCTURE adresseCORP = InfoGEDCOM.N3_SOUR_CORP_ADDR;
                    if (adresseCORP != null)
                    {
                        if (adresseCORP.N0_ADDR != null) texte += "\t\t\t\t\t\t<br/>" + adresseCORP.N0_ADDR + "\n";
                        if (adresseCORP.N1_ADR1 != null) texte += "\t\t\t\t\t\t<br/>" + adresseCORP.N1_ADR1 + "\n";
                        if (adresseCORP.N1_ADR1 != null) texte += "\t\t\t\t\t\t<br/>" + adresseCORP.N1_ADR2 + "\n";
                        if (adresseCORP.N1_ADR3 != null) texte += "\t\t\t\t\t\t<br/>" + adresseCORP.N1_ADR3 + "\n";
                        if (adresseCORP.N1_CITY != null) texte += "\t\t\t\t\t\t<br/>" + adresseCORP.N1_CITY + "\n";
                        if (adresseCORP.N1_STAE != null) texte += "\t\t\t\t\t\t<br/>" + adresseCORP.N1_STAE + "\n";
                        if (adresseCORP.N1_CTRY != null) texte += "\t\t\t\t\t\t<br/>" + adresseCORP.N1_CTRY + "\n";
                        if (adresseCORP.N1_POST != "") texte += "\t\t\t\t\t\t<br/>" + adresseCORP.N1_POST + "\n";
                    }
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                    // corporation téléphone
                    if (InfoGEDCOM.N3_SOUR_CORP_PHON_liste != null)
                    {
                        bool premier = true;
                        foreach (string s in InfoGEDCOM.N3_SOUR_CORP_PHON_liste)
                        {
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                            if (premier)
                            {
                                texte += "\t\t\t\t\t\tTéléphone\n";
                                premier = false;
                            }
                            else texte += "\t\t\t\t\t\t&nbsp;\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td>\n";
                            texte += "\t\t\t\t\t\t" + s + "\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t</tr>\n";
                        }
                    }
                    // corporation fax
                    if (InfoGEDCOM.N3_SOUR_CORP_FAX_liste != null)
                    {
                        bool premier = true;
                        foreach (string s in InfoGEDCOM.N3_SOUR_CORP_FAX_liste)
                        {
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                            if (premier)
                            {
                                texte += "\t\t\t\t\t\tTélécopieur\n";
                                premier = false;
                            }
                            else texte += "\t\t\t\t\t\t&nbsp;\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td>\n";
                            texte += "\t\t\t\t\t\t" + s + "\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t</tr>\n";
                        }
                    }
                    // corporation courriel
                    if (InfoGEDCOM.N3_SOUR_CORP_EMAIL_liste != null)
                    {
                        bool premier = true;
                        foreach (string s in InfoGEDCOM.N3_SOUR_CORP_EMAIL_liste)
                        {
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                            if (premier)
                            {
                                texte += "\t\t\t\t\t\tCourriel\n";
                                premier = false;
                            }
                            else texte += "\t\t\t\t\t\t&nbsp;\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td>\n";
                            texte += "\t\t\t\t\t\t" + s + "\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t</tr>\n";
                        }
                    }
                    // corporation WWW
                    if (InfoGEDCOM.N3_SOUR_CORP_WWW_liste != null)
                    {
                        bool premier = true;
                        foreach (string s in InfoGEDCOM.N3_SOUR_CORP_WWW_liste)
                        {
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                            if (premier)
                            {
                                texte += "\t\t\t\t\t\tSite Web\n";
                                premier = false;
                            }
                            else texte += "\t\t\t\t\t\t&nbsp;\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td>\n";
                            texte += "\t\t\t\t\t\t" + s + "\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t</tr>\n";
                        }
                    }
                    // information 
                    if (InfoGEDCOM.N2_SOUR_DATA != null || InfoGEDCOM.N3_SOUR_DATA_DATE != null || InfoGEDCOM.N3_SOUR_DATA_CORP != null)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\tInformation\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        if (InfoGEDCOM.N2_SOUR_DATA != null) texte += "\t\t\t\t\t\t" + InfoGEDCOM.N2_SOUR_DATA + "\n";
                        string date;
                        if (GH.Properties.Settings.Default.date_longue)
                        {
                            date = GEDCOMClass.ConvertirDateTexte(InfoGEDCOM.N3_SOUR_DATA_DATE);
                        }
                        else
                        {
                            date = InfoGEDCOM.N3_SOUR_DATA_DATE;
                        }
                        if (InfoGEDCOM.N3_SOUR_DATA_DATE != null) texte += "\t\t\t\t\t\t<br/>" + date + "\n";
                        if (InfoGEDCOM.N3_SOUR_DATA_CORP != null) texte += "\t\t\t\t\t\t<br/>" + InfoGEDCOM.N3_SOUR_DATA_CORP + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                }
                texte += "\t\t\t</table>\n";
                texte += Groupe("fin", 3);
            }
            // Groupe chercheur ****************************************************************************
            (temp, groupe_chercheur) = Groupe_chercheur(
                liste_SUBMITTER_ID_numero,
                dossierSortie,
                "index",
                liste_note_ID_numero,
                0);
            texte += temp;
            // @citation ************************************************************************************************************************
            if (liste_citation_ID_numero.Count > 0)
            {
                // Générer le texte HTML
                (temp, groupe_citation) =
                        Groupe_citation(
                        dossierSortie,
                        "individus",
                        liste_note_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        3);
                texte += temp;
            }
            // @source ************************************************************************************************************************
            if (liste_source_ID_numero.Count() > 0)
            {
                // Générer le texte HTML
                (temp, groupe_source) = Groupe_source(
                        dossierSortie,
                        "individus",
                        liste_note_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        3);
                texte += temp;
            }
            // @Depot Groupe **********************************************************************************************
            (temp, groupe_depot) = Groupe_depot(liste_note_ID_numero, liste_repo_ID_numero, 3);
            texte += temp;
            // ajouter Groupe note *************************************************************************
            if (liste_note_ID_numero.Count() > 0)
            {
                (temp, groupe_note) = Groupe_note(
                        "index",
                        liste_note_ID_numero,
                        liste_citation_ID_numero,
                        3);
                texte += temp;
            }
            // menu hamburger
            texte += "\t\t\t\t<div class=\"hamburger\">\n";
            texte += "\t\t\t\t\t<button class=\"boutonHamburger\"></button>\n";
            texte += "\t\t\t\t\t<div class=\"hamburger-contenu\">\n";
            if (groupe_GEDCOM) texte += "\t\t\t\t\t\t<a href=\"#groupe_GEDCOM\">GEDCOM</a>\n";
            if (groupe_information) texte += "\t\t\t\t\t\t<a href=\"#groupe_information\">Information</a>\n";
            if (groupe_logiciel) texte += "\t\t\t\t\t\t<a href=\"#groupe_information\">Logiciel</a>\n";
            if (groupe_chercheur) texte += "\t\t\t\t\t\t<a href=\"#groupe_chercheur\">Chercheur</a>\n";
            if (groupe_citation) texte += "\t\t\t\t\t\t<a href=\"#groupe_citation\">Citation</a>\n";
            if (groupe_source) texte += "\t\t\t\t\t\t<a href=\"#groupe_source\">Source</a>\n";
            if (groupe_depot) texte += "\t\t\t\t\t\t<a href=\"#groupe_chercheur\">Dépôt</a>\n";
            if (groupe_note) texte += "\t\t\t\t\t\t<a href=\"#groupe_note\">Note</a>\n";
            texte += "\t\t\t\t\t</div>\n";
            texte += "\t\t\t\t</div>\n";
            texte += Bas_Page();
            try
            {
                System.IO.File.WriteAllText(nomFichier, texte);
            }
            catch (Exception msg)
            {
                string message = "Ne peut pas écrire le fichier" + nomFichier + ".";
                GEDCOMClass.Voir_message(message, msg.Message, Avoir_code_erreur());
            }
        }
        public void Index_famille_conjoint(string dossierSortie)
        {
            ListView lvChoixFamille = Application.OpenForms["GH"].Controls["lvChoixFamille"] as ListView;
            string origine = "../";
            string nomFichier = @dossierSortie + "/familles/" + "indexConjoint.html";
            if (File.Exists(nomFichier))
            {
                File.Delete(nomFichier);
            }
            string texte = "";
            string FamilleConjointIndex = AvoirFamilleConjointIndex();
            texte += Haut_Page(origine, true);
            texte += Separation("large", 3);
            texte += FamilleConjointIndex;
            texte += "\t\t\t<h1> \n";
            texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun//familleConjoint.svg\" alt=\"\" />\n";
            texte += "\t\t\t\tIndex des famille\n";
            texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun//male.svg\" alt=\"\" />\n";
            texte += "\t\t\t\t</h1>\n";
            texte += Bas_Page();
            try
            {
                System.IO.File.WriteAllText(nomFichier, texte);
            }
            catch (Exception msg)
            {
                string message = "Ne peut pas écrire le fichier" + nomFichier + ".";
                GEDCOMClass.Voir_message(message, msg.Message, Avoir_code_erreur());
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
                    texte = "";
                    texte += Haut_Page("..//", true);
                    texte += Separation("large", 3);
                    texte += FamilleConjointIndex;
                    texte += "\t\t\t<h1>\n";
                    if (f == 26)
                    {
                        texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/familleConjoint.svg\" alt=\"\" />\n";
                        texte += "\t\t\t\tFamilles diver\n";
                        Message_HTML("Génération index conjoint  ", "Diver");
                    }
                    else
                    {
                        texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/familleConjoint.svg\" alt=\"\" />\n";
                        texte += "\t\t\t\tFamilles " + (char)(f + 65) + "\n";
                        Message_HTML("Génération index conjoint  ", "lettre " + (char)(f + 65));
                    }
                    texte += "\t\t\t</h1>";
                    texte += "\t\t\t<table class=\"atl\" style=\"border:1px;\">";
                    texte += "\t\t\t\t<tr>";
                    texte += "\t\t\t\t\t<td class=\"liste\">Voir&nbsp;&nbsp;&nbsp;&nbsp;</td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Conjoint<img style =\"height:32px;vertical-align:middle\" src=\"" + "../commun/male.svg\" alt=\"\" /></td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Conjointe<img style =\"height:32px;vertical-align:middle\" src=\"" + "..//commun/femelle.svg\" alt=\"\" /></td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Mariage</td>\n";
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
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[1].Text + "</td >\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[2].Text + "</td >\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[3].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[0].Text + "</td>\n";
                            texte += "\t\t\t\t</tr>";
                        }
                        else if (f == 26 && !("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(l)))
                        {
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"" + lvChoixFamille.Items[ff].SubItems[0].Text + ".html\">\n";
                            texte += "\t\t\t\t\t\t</a>\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[0].Text + "</td >\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[1].Text + "</td >\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixFamille.Items[ff].SubItems[1].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixFamille.Items[ff].SubItems[2].Text + "</td>\n";
                            texte += "\t\t\t\t</tr>";
                        }
                    }
                    texte += "\t\t\t</table>";
                    texte += Bas_Page();
                    string erreur = Avoir_code_erreur();
                    try
                    {
                        erreur = Avoir_code_erreur();
                        if (f == 26)
                        {
                            erreur = Avoir_code_erreur();
                            Animation(true);
                            System.IO.File.WriteAllText(@dossierSortie + "/familles/mDiver.html", texte);
                            //Message_HTML("Génération index conjoint  ", "Diver");
                            Application.DoEvents();
                        }
                        else
                        {
                            erreur = Avoir_code_erreur();
                            Animation(true);
                            //Message_HTML("Génération index conjoint  ", "lettre " + (char)(f + 65));
                            Application.DoEvents();
                            System.IO.File.WriteAllText(@dossierSortie + "/familles/m" + (char)(f + 65) + ".html", texte);
                        }
                    }
                    catch (Exception msg)
                    {
                        string message = "Ne peut pas écrire le fichier" + dossierSortie + ".";
                        GEDCOMClass.Voir_message(message, msg.Message, erreur);
                        Message_HTML();
                    }
                }
            }
            Message_HTML();
        }
        public void Index_famille_conjointe(string dossierSortie)
        {
            string origine = "../";
            string nomFichier = @dossierSortie + "//familles//" + "indexConjointe.html";
            ListView lvChoixFamille = Application.OpenForms["GH"].Controls["lvChoixFamille"] as ListView;
            if (File.Exists(nomFichier))
            {
                File.Delete(nomFichier);
            }
            string texte = "";
            string FamilleConjointeIndex = AvoirFamilleConjointeIndex();
            texte += Haut_Page(origine, true);
            texte += Separation("large", 3);
            texte += FamilleConjointeIndex;
            texte += "\t\t\t<h1>\n";
            texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/familleConjointe.svg\" alt=\"\" />\n";
            texte += "\t\t\t\tIndex des famille\n";
            texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/femelle.svg\" alt=\"\" />\n";
            texte += "\t\t\t</h1>\n";
            texte += Bas_Page();
            try
            {
                System.IO.File.WriteAllText(nomFichier, texte);
            }
            catch (Exception msg)
            {
                string message = "Ne peut pas écrire le fichier" + nomFichier + ".";
                GEDCOMClass.Voir_message(message, msg.Message, Avoir_code_erreur());
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
                    
                    
                    texte = "";
                    texte += Haut_Page("../", true);
                    texte += Separation("large", 3);
                    texte += FamilleConjointeIndex;
                    texte += "\t\t\t<h1>\n";
                    if (f == 26)
                    {
                        texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/familleConjointe.svg\" alt=\"\" />\n";
                        texte += "\t\t\t\tFamilles diver\n";
                        Message_HTML("Génération index conjointe  ", "Divert");
                    }
                    else
                    {
                        texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/familleConjointe.svg\" alt=\"\" />\n";
                        texte += "\t\t\t\tFamilles " + (char)(f + 65) + "\n";
                        Message_HTML("Génération index conjointe  ", "lettre " + (char)(f + 65));
                    }
                    texte += "\t\t\t</h1>\n";
                    texte += "\t\t\t<table class=\"atl\" style=\"border:1px;\">\n";
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Voir&nbsp;&nbsp;&nbsp;&nbsp;</td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Conjointe<img style =\"height:32px;vertical-align:middle\" src=\"" + "../commun/femelle.svg\" alt=\"\" /></td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Conjoint<img style =\"height:32px;vertical-align:middle\" src=\"" + "../commun/male.svg\" alt=\"\" /></td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Mariage</td>\n";
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
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[1].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[2].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[3].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[0].Text + "</td>\n";
                            texte += "\t\t\t\t</tr>\n";
                        }
                        else if (f == 26 && !("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(l)))
                        {
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"" + lvChoixFamille.Items[ff].SubItems[0].Text + ".html\">\n";
                            texte += "\t\t\t\t\t\t</a>\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[2].Text + "</td >\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[3].Text + "</td >\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixFamille.Items[ff].SubItems[2].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixFamille.Items[ff].SubItems[3].Text + "</td>\n";
                            texte += "\t\t\t\t</tr>\n";
                        }
                    }
                    texte += "\t\t\t</table>\n";
                    texte += Bas_Page();
                    string erreur = Avoir_code_erreur();
                    try
                    {
                        erreur = Avoir_code_erreur();
                        if (f == 26)
                        {
                            erreur = Avoir_code_erreur();
                            Animation(true);
                            //Message_HTML("Génération index conjointe  ", "Divert");
                            Application.DoEvents();
                            System.IO.File.WriteAllText(@dossierSortie + "/familles/fDiver.html", texte);
                            if (GH.GH.annuler) return;
                        }
                        else
                        {
                            erreur = Avoir_code_erreur();
                            Animation(true);
                            //Message_HTML("Génération index conjointe  ", "lettre " + (char)(f + 65));
                            Application.DoEvents();
                            System.IO.File.WriteAllText(@dossierSortie + "/familles/f" + (char)(f + 65) + ".html", texte);
                            if (GH.GH.annuler) return;
                        }
                    }
                    catch (Exception msg)
                    {
                        string message = "Ne peut pas écrire le fichier" + dossierSortie + ".";
                        GEDCOMClass.Voir_message(message, msg.Message, erreur);
                        Message_HTML();
                    }
                }
            }
            Message_HTML();
        }
        public void Index_individu(string dossierSortie)
        {
            string nomFichier = @dossierSortie + "/individus/" + "index.html";
            ListView lvChoixIndividu = Application.OpenForms["GH"].Controls["lvChoixIndividu"] as ListView;
            if (File.Exists(nomFichier))
            {
                File.Delete(nomFichier);
            }
            string texte = "";
            texte += Haut_Page("..//", true);
            texte += Separation("large", 3);
            string texte_index = Individu_Index_Bouton();
            texte += texte_index;
            texte += "\t\t\t<h1>\n";
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
                string message = "Ne peut pas écrire le fichier" + nomFichier + ".";
                GEDCOMClass.Voir_message(message, msg.Message, Avoir_code_erreur());
                if (GH.GH.annuler)return;
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
                    texte = "";
                    texte += Haut_Page("../", true);
                    texte += Separation("large", 3);
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
                    texte += "\t\t\t\t\t<td class=\"liste\">Voir&nbsp;&nbsp;&nbsp;&nbsp;</td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Nom</td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">ID</td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Naissance</td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Décès</td>\n";
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
                            Message_HTML("Génération index individu  ", "lettre " + (char)(f + 65));
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"" + lvChoixIndividu.Items[ff].SubItems[0].Text + ".html\">\n";
                            texte += "\t\t\t\t\t\t</a>\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td>\n";
                            texte += "\t\t\t\t\t\t" + lvChoixIndividu.Items[ff].SubItems[1].Text + "\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixIndividu.Items[ff].SubItems[0].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixIndividu.Items[ff].SubItems[2].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixIndividu.Items[ff].SubItems[4].Text + "</td>\n";
                            texte += "\t\t\t\t</tr>";
                        }
                        else if (f == 26 && !("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(l)))
                        {
                            Message_HTML("Génération index individu  ", "Diver");
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"" + lvChoixIndividu.Items[ff].SubItems[2].Text + ".html\">\n";
                            texte += "\t\t\t\t\t\t</a>\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixIndividu.Items[ff].SubItems[1].Text + "</td >\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixIndividu.Items[ff].SubItems[0].Text + "</td >\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixIndividu.Items[ff].SubItems[2].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixIndividu.Items[ff].SubItems[3].Text + "</td>\n";
                            texte += "\t\t\t\t</tr>";
                        }
                    }
                    texte += "\t\t\t</table>";
                    texte += Bas_Page();
                    string erreur = Avoir_code_erreur();
                    try
                    {
                        erreur = Avoir_code_erreur();
                        if (f == 26)
                        {
                            erreur = Avoir_code_erreur();
                            Animation(true);
                            //Message_HTML("Génération index individu  ", "Diver");
                            Application.DoEvents();
                            System.IO.File.WriteAllText(@dossierSortie + "/individus/Diver.html", texte);
                            if (GH.GH.annuler) return;
                        }
                        else
                        {
                            erreur = Avoir_code_erreur();
                            Animation(true);
                            // Message_HTML("Génération index individu  ", "lettre " + (char)(f + 65));
                            Application.DoEvents();
                            System.IO.File.WriteAllText(@dossierSortie + "/individus/" + (char)(f + 65) + ".html", texte);
                            if (GH.GH.annuler) return;
                        }
                    }
                    catch (Exception msg)
                    {
                        string message = "Ne peut pas écrire le fichier" + dossierSortie + ".";
                        GEDCOMClass.Voir_message(message, msg.Message, erreur);
                        Message_HTML();
                    }
                }
            }
            Message_HTML();
        }
        public string Individu(string IDIndividu, bool menu, string dossierSortie)
        {
            List<ID_numero> liste_SUBMITTER_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_note_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_citation_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_source_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_repo_ID_numero = new List<ID_numero>();
            (
                liste_SUBMITTER_ID_numero,
                liste_note_ID_numero,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_repo_ID_numero
            ) = Avoir_liste_reference_individu
            (
                IDIndividu,
                liste_SUBMITTER_ID_numero,
                liste_note_ID_numero,
                liste_citation_ID_numero,
                liste_source_ID_numero,
                liste_repo_ID_numero
            );
            TextBox Tb_Status = Application.OpenForms["GH"].Controls["Tb_Status"] as TextBox;
            Tb_Status.Text = "Génération de la fiche individu ID " + IDIndividu;
            Application.DoEvents();
            string sousDossier = "individus";
            if (IDIndividu == null) return null;
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
            bool groupe_depot;
            int numeroCarte = 0;
            string IDCourantIndividu = IDIndividu;
            // Récupérer les informations de l'individu
            GEDCOMClass.INDIVIDUAL_RECORD infoIndividu;
            (_, infoIndividu) = GEDCOMClass.Avoir_info_individu(IDIndividu);
            string nom = GEDCOMClass.AvoirPremierNomIndividu(IDIndividu);
            List<GEDCOMClass.PERSONAL_NAME_STRUCTURE> listeInfoNom;
            bool OkLIN;
            (OkLIN, listeInfoNom) = GEDCOMClass.AvoirListeNom(IDIndividu);
            string fichierSex;
            string genre = infoIndividu.N1_SEX;
            string genreTxt = null;
            if (genre == "M")
            {
                genreTxt = "masculin";
            };
            if (genreTxt == "F")
            {
                genre = "féminin";
            };
            // avoir le portrait
            string fichierPortrait;
            string IDPortrait = null;
            GEDCOMClass.MULTIMEDIA_RECORD portraitInfo;
            if (infoIndividu.N1_OBJE_liste.Count > 0)
            {
                IDPortrait = infoIndividu.N1_OBJE_liste[0];
                portraitInfo = GEDCOMClass.Avoir_info_media(IDPortrait);
                fichierPortrait = CopierObjet(portraitInfo.N1_FILE, "individus", dossierSortie);
                if (fichierPortrait == null)
                {
                    fichierPortrait = "../commun/media_manquant.svg";
                }
                else
                {
                    fichierPortrait = "medias/" + fichierPortrait;
                }
            }
            else
            {
                if (genre == "M")
                {
                    fichierPortrait = "../commun/masculin.png";
                }
                else if (genre == "F")
                {
                    fichierPortrait = "../commun/feminin.png";
                }
                else
                {
                    fichierPortrait = "../commun/neutre.png";
                }
            }
            if (genre == "M")
            {
                fichierSex = "../commun/male.svg";

            }
            else if (genre == "F")
            {
                fichierSex = "../commun/femelle.svg";
            }
            else
            {
                fichierSex = "../commun/neutre.svg";
            }
            GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE NaissanceIndividu;
            (_, NaissanceIndividu) = GEDCOMClass.AvoirEvenementNaissance(infoIndividu.N1_EVENT_Liste);
            GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE DecesIndividu;
            (_, DecesIndividu) = GEDCOMClass.AvoirEvenementDeces(infoIndividu.N1_EVENT_Liste);
            string dateNaissance = NaissanceIndividu.N2_DATE;
            string dateDeces = DecesIndividu.N2_DATE;
            string age = null;
            if (dateNaissance != null && dateDeces != null)
            {
                age = Avoir_age(dateNaissance, dateDeces);
            }
            string nomFichierIndividu = dossierSortie + "\\individus\\" + IDIndividu + ".html";
            if (!menu) nomFichierIndividu = dossierSortie + "\\individus\\page.html";
            if (File.Exists(nomFichierIndividu))
            {
                File.Delete(nomFichierIndividu);
            }
            string texte = null;
            texte += Haut_Page("../", menu);
            texte += Separation("large", 3);
            // restriction
            if (infoIndividu.N1_RESN != null)
            {
                texte += "\t\t\t<div class=\"blink3\">\n";
                texte += "\t\t\t\tRestriction\n";
                texte += "\t\t\t\t" + infoIndividu.N1_RESN + "\n";
                texte += "\t\t\t</div>\n";
            }
            texte += "<a id=\"groupe_nom\"></a>\n";
            texte += "\t\t\t<table class=\"titre\">\n";
            texte += "\t\t\t\t<tr>\n";
            texte += "\t\t\t\t\t<td>\n";
            texte += "\t\t\t\t\t\t" + nom + "\n";
            string temp;
            temp = Avoir_lien_citation_source(infoIndividu.N1_SOUR_citation_liste_ID, liste_citation_ID_numero, 6);
            texte += temp;
            // affiche si adopter
            if (infoIndividu.Adopter != null)
            {
                texte += "\t\t\t\t\t\t\t<div style=\"font-size:small\"><strong>Adopter</strong></div>" + "\n";
            }
            if (GH.Properties.Settings.Default.VoirID == true)
            {
                texte += "\t\t\t\t\t\t<div style=\"font-size:small\">[" + IDIndividu + "]</div>" + "\n";
            }
            texte += "\t\t\t\t\t</td>\n";
            texte += "\t\t\t\t</tr>\n";
            texte += "\t\t\t</table>\n";
            texte += "\t\t\t<div style=\"border: 2px solid #000;border-top:0px solid #000;padding:10px;min-height:300px;\">\n";
            if (GH.Properties.Settings.Default.photo_principal)
            {
                texte += "\t\t\t\t<img class=\"portrait\" src=\"" + fichierPortrait + "\" alt=\"\" />\n";
            }
            // adopter
            if (infoIndividu.Adopter != null)
            {
                texte += "\t\t\t\t<div>\n";
                texte += "\t\t\t\t\t<span class=\"detail_col1\">&nbsp;</span>\n";
                texte += "\t\t\t\t\t<span class=\"detail_col2\" style=\"font-size:150%;\"><strong>Adopter" +
                    "</strong></span>\n";
                texte += "\t\t\t\t</div>\n";
            }
            // liste des noms **********************************************************************************************************************
            if (OkLIN)
            {
                foreach (GEDCOM.GEDCOMClass.PERSONAL_NAME_STRUCTURE infoNom in listeInfoNom)
                {
                    string nom1 = null;
                    string nom2 = null;
                    if (infoNom.N0_NAME != null)
                        nom1 = infoNom.N0_NAME;
                    if (infoNom.N1_SURN != null || infoNom.N1_GIVN != null)
                    {
                        string titre = infoNom.N1_NPFX;
                        if (titre != null) titre += " ";
                        string prefix = infoNom.N1_SPFX;
                        if (prefix != null) prefix += " ";
                        string suffix = infoNom.N1_NSFX;
                        if (suffix != null) suffix = ", " + suffix;
                        nom2 = titre + prefix + infoNom.N1_SURN + ", " + infoNom.N1_GIVN + suffix;
                    }
                    texte += "\t\t\t\t<div>\n";
                    texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                    texte += "\t\t\t\t\t\t" + infoNom.N1_TYPE + "\n";
                    texte += "\t\t\t\t\t</span>\n";
                    if (nom1 != null && nom2 == null)
                    {
                        texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                        texte += "\t\t\t\t\t\t<strong>" + nom1 + "</strong>\n";
                        texte += "\t\t\t\t\t</span>\n";
                    }
                    if (nom1 != null && nom2 != null)
                    {
                        texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                        texte += "\t\t\t\t\t<strong>" + nom1 + "</strong><br />\n";
                        texte += "\t\t\t\t\t" + nom2 + "\n";
                        texte += "\t\t\t\t\t</span>\n";
                    }
                    if (nom1 == null && nom2 != null)
                    {
                        texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                        texte += "\t\t\t\t\t\t<strong>" + nom2 + "</strong>\n";
                        texte += "\t\t\t\t\t</span>\n";
                    }
                    texte += "\t\t\t\t</div>\n";
                    // si allia BROSKEEP
                    if (infoNom.N1_ALIA_liste != null)
                    {
                        foreach (string alia in infoNom.N1_ALIA_liste)
                        {
                            texte += "\t\t\t\t<div>\n";
                            texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                            texte += "\t\t\t\t\t\tAlias\n";
                            texte += "\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                            texte += "\t\t\t\t\t\t" + alia + "\n";
                            texte += "\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t</div>\n";
                        }
                    }
                    // surnom
                    if (infoNom.N1_NICK != null)
                    {
                        texte += "\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                        texte += "\t\t\t\t\t\t&nbsp;\n";
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                        texte += "\t\t\t\t\t\tSurnom " + infoNom.N1_NICK + "\n";
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t</div>\n";
                    }
                    
                    if (infoNom.N1_SOUR_citation_liste_ID != null)
                    {
                        texte += "\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                        texte += "\t\t\t\t\t\t&nbsp;\n";
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                        temp = Avoir_lien_citation_source(infoNom.N1_SOUR_citation_liste_ID, liste_citation_ID_numero, 6);
                        texte += temp;
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t</div>\n";
                    }
                    if (infoNom.N1_NOTE_liste_ID.Count() > 0)
                    {
                        texte += "\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                        texte += "\t\t\t\t\t\t&nbsp;\n";
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                        texte += Avoir_lien_note(infoNom.N1_NOTE_liste_ID, liste_note_ID_numero, true, 6);
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t</div>\n";
                    }
                    // FONE
                    string nom3 = null;
                    string nom4 = null;
                    string prefixFONE = infoNom.N2_FONE_SPFX;
                    if (prefixFONE != null) prefixFONE += " ";
                    string suffixFONE = infoNom.N2_FONE_NSFX;
                    if (suffixFONE != null) suffixFONE = ", " + suffixFONE;
                    string surnomFONE = infoNom.N2_FONE_NICK;
                    string titreFONE = infoNom.N2_FONE_NPFX;
                    if (titreFONE != null) titreFONE += " ";
                    if (infoNom.N2_FONE_SURN != null || infoNom.N2_FONE_GIVN != null)
                    {
                        nom4 = titreFONE + prefixFONE + infoNom.N2_FONE_SURN + ", " + infoNom.N2_FONE_GIVN + suffixFONE;
                    }
                    nom3 = infoNom.N1_FONE;
                    if (nom3 != null || nom4 != null)
                    {
                        texte += "\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col1 texteR\" >\n";
                        texte += "\t\t\t\t\t\tNom phonétiser&nbsp;&nbsp;\n";
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                        texte += "\t\t\t\t\t\tType " + infoNom.N2_FONE_TYPE + "\n" ;
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t</div>\n";

                        texte += "\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col1 texteR\" >\n";
                        texte += "\t\t\t\t\t\t&nbsp;\n";
                        texte += "\t\t\t\t\t</span>\n";
                        if (nom3 != null && nom4 == null)
                        {
                            texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                            texte += "\t\t\t\t\t\t<strong>" + nom3 + "</strong>\n";
                            texte += "\t\t\t\t\t</span>\n";
                        }
                        if (nom3 != null && nom4 != null)
                        {
                            texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                            texte += "\t\t\t\t\t\t<strong>" + nom3 + "</strong><br />\n";
                            texte += "\t\t\t\t\t\t" + nom4 + "\n";
                            texte += "\t\t\t\t\t</span>\n";
                        }
                        if (nom3 == null && nom4 != null)
                        {
                            texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                            texte += "\t\t\t\t\t\t<strong>" + nom4 + "</strong>\n";
                            texte += "\t\t\t\t\t</span>\n";
                        }
                        texte += "\t\t\t\t</div>\n";
                        // surnom FONE
                        if (infoNom.N2_FONE_NICK != null)
                        {
                            texte += "\t\t\t\t<div>\n";
                            texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                            texte += "\t\t\t\t\t\t&nbsp;\n";
                            texte += "\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                            texte += "\t\t\t\t\t\tSurnom " + infoNom.N2_FONE_NICK + "\n";
                            texte += "\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t</div>\n";
                        }
                        // SOUR FONE
                        if (infoNom.N2_FONE_SOUR_citation_liste_ID != null)
                        {
                            texte += "\t\t\t\t<div>\n";
                            texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                            texte += "\t\t\t\t\t\t&nbsp;\n";
                            texte += "\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                            texte += Avoir_lien_citation_source(infoNom.N2_FONE_SOUR_citation_liste_ID, liste_citation_ID_numero, 6);
                            texte += "\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t</div>\n";
                        }
                        // note FONE
                        if (infoNom.N2_FONE_NOTE_ID_liste != null)
                        {
                            texte += "\t\t\t\t<div>\n";
                            texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                            texte += "\t\t\t\t\t\t&nbsp;\n";
                            texte += "\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                            texte += Avoir_lien_note(infoNom.N2_FONE_NOTE_ID_liste, liste_note_ID_numero, true, 6);
                            texte += "\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t</div>\n";
                        }
                    }
                    // ROMN
                    string nom5 = null;
                    string nom6 = null;
                    string prefixROMN = infoNom.N2_ROMN_SPFX;
                    if (prefixROMN != null) prefixROMN += " ";
                    string suffixROMN = infoNom.N2_ROMN_NSFX;
                    if (suffixROMN != null) suffixROMN = ", " + suffixROMN;
                    string surnomROMN = infoNom.N2_ROMN_NICK;
                    string titreROMN = infoNom.N2_ROMN_NPFX;
                    if (titreROMN != null) titreROMN += " ";
                    if (infoNom.N2_ROMN_SURN != null || infoNom.N2_ROMN_GIVN != null)
                    {
                        nom6 = titreROMN + prefixROMN + infoNom.N2_ROMN_SURN + ", " + infoNom.N2_ROMN_GIVN + suffixROMN;
                    }
                    nom5 = infoNom.N1_ROMN;
                    if (nom5 != null || nom6 != null)
                    {
                        texte += "\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col1 texteR\" >\n";
                        texte += "\t\t\t\t\t\tNom romanisation&nbsp;&nbsp;\n";
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                        texte += "\t\t\t\t\t\tType " + infoNom.N2_ROMN_TYPE +"\n";
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t</div>\n";

                        texte += "\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col1 texteR\" >\n";
                        texte += "\t\t\t\t\t\t&nbsp;\n";
                        texte += "\t\t\t\t\t</span>\n";
                        if (nom5 != null && nom6 == null)
                        {
                            texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                            texte += "\t\t\t\t\t\t<strong>" + nom5 + "</strong>\n";
                            texte += "\t\t\t\t\t</span>\n";
                        }
                        if (nom5 != null && nom6 != null)
                        {
                            texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                            texte += "\t\t\t\t\t<strong>" + nom5 + "</strong><br />\n";
                            texte += "\t\t\t\t\t" + nom6 + "\n";
                            texte += "\t\t\t\t\t</span>\n";
                        }
                        if (nom5 == null && nom6 != null)
                        {
                            texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                            texte += "\t\t\t\t\t\t<strong>" + nom6 + "</strong>\n";
                            texte += "\t\t\t\t\t</span>\n";
                        }
                        texte += "\t\t\t\t</div>\n";
                        // surnom ROMN
                        if (infoNom.N2_FONE_NICK != null)
                        {
                            texte += "\t\t\t\t<div>\n";
                            texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                            texte += "\t\t\t\t\t\t&nbsp;\n";
                            texte += "\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                            texte += "\t\t\t\t\t\tSurnom " + infoNom.N2_ROMN_NICK + "\n";
                            texte += "\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t</div>\n";
                        }
                        // SOUR ROMN
                        if (infoNom.N2_ROMN_SOUR_citation_liste_ID != null)
                        {
                            texte += "\t\t\t\t<div>\n";
                            texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                            texte += "\t\t\t\t\t\t&nbsp;\n";
                            texte += "\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                            texte += Avoir_lien_citation_source(infoNom.N2_ROMN_SOUR_citation_liste_ID, liste_citation_ID_numero, 6);
                            texte += "\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t</div>\n";
                        }
                        // NOTE ROMN
                        if (infoNom.N2_ROMN_NOTE_ID_liste != null)
                        {
                            texte += "\t\t\t\t<div>\n";
                            texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                            texte += "\t\t\t\t\t\t&nbsp;\n";
                            texte += "\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                            texte += Avoir_lien_note(infoNom.N2_ROMN_NOTE_ID_liste, liste_note_ID_numero, true, 6);
                            texte += "\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t</div>\n";
                        }
                    }
                }
                texte += Separation("moyen", 4);
            }
            // si allia
            if (infoIndividu.N1_ALIA_liste_ID != null)
            {
                foreach (string ID in infoIndividu.N1_ALIA_liste_ID)
                {
                    (bool Ok_nom_Alia, GEDCOMClass.INDIVIDUAL_RECORD alia_individu) = GEDCOMClass.Avoir_info_individu(ID);
                    string nom_ALIA = null;
                    if (Ok_nom_Alia)
                    {
                        if (alia_individu.N1_NAME_liste[0].N1_SURN != null ||
                            alia_individu.N1_NAME_liste[0].N1_GIVN != null
                            )
                        {
                            nom_ALIA = AssemblerPatronymePrenom(
                                    alia_individu.N1_NAME_liste[0].N1_SURN,
                                    alia_individu.N1_NAME_liste[0].N1_GIVN);
                        }
                        else
                        {
                            nom_ALIA = alia_individu.N1_NAME_liste[0].N0_NAME;
                        }
                    }
                    string txtID = null;
                    if (GH.Properties.Settings.Default.VoirID == true)
                    {
                        txtID = " [" + ID + "]";
                    }
                    texte += "\t\t\t\t<div>\n";
                    texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                    texte += "\t\t\t\t\t\tMême personne?\n";
                    texte += "\t\t\t\t\t</span>\n";
                    texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                    if (menu)
                    {
                        texte += "\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"" + ID + ".html\"></a>\n";
                    }
                    else
                    {
                        texte += "\t\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                    }
                    texte += "\t\t\t\t\t\t" + nom_ALIA + txtID + "\n";
                    texte += "\t\t\t\t\t</span>\n";
                    texte += "\t\t\t\t</div>\n";
                }
            }
            // Genre
            if (infoIndividu.N1_SEX != null)
            {
                texte += Separation("moyen", 4);
                texte += "\t\t\t\t<div>\n";
                texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                texte += "\t\t\t\t\t\tGenre\n";
                texte += "\t\t\t\t\t</span>\n";
                texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                texte += "\t\t\t\t\t\t" + genre + " " +
                    "<img style =\"height:16px;vertical-align:middle\" src=\"" + fichierSex +
                    "\" alt=\"\" />\n";
                texte += "\t\t\t\t\t</span>\n";
                texte += "\t\t\t\t</div>\n";
            }
            // filiation  Ancestrologie
            if (infoIndividu.N1_FILA != null)
            {
                texte += Separation("moyen", 4);
                texte += "\t\t\t\t<div>\n";
                texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                texte += "\t\t\t\t\t\tFiliation\n";
                texte += "\t\t\t\t\t</span>\n";
                texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                texte += "\t\t\t\t\t\t" + infoIndividu.N1_FILA +"\n";
                texte += "\t\t\t\t\t</span>\n";
                texte += "\t\t\t\t</div>\n";
            }
            // Âge
            if (age != null)
            {
                texte += Separation("moyen", 4);
                texte += "\t\t\t\t<div>\n";
                texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                texte += "\t\t\t\t\t\tÂge au décès\n";
                texte += "\t\t\t\t\t</span>\n";
                texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                texte += "\t\t\t\t\t\t" + age + "\n";
                texte += "\t\t\t\t\t</span>\n";
                texte += "\t\t\t\t</div>\n";
            }
            // Nombre d'enfant
            GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE info = GEDCOMClass.Avoir_attribute_nombre_enfant(infoIndividu.N1_Attribute_liste);
            if (info.N1_EVEN_texte != null) // NCHI {CHILDREN_COUNT}
            {
                texte += Separation("moyen", 4);
                texte += "\t\t\t\t\n";
                texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                texte += "\t\t\t\t\t\tNombre d'enfant\n";
                texte += "\t\t\t\t\t</span>\n";
                texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                texte += "\t\t\t\t\t\t" + info.N1_EVEN_texte + "\n";
                texte += "\t\t\t\t\t</span>\n";
                texte += "\t\t\t\t</div>\n";
            }
            // Nombre de mariage
            GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE evenement = GEDCOMClass.Avoir_attribute_nombre_mariage(infoIndividu.N1_Attribute_liste);
            if (evenement.N1_EVEN_texte != null) // NMR {MARRIAGE_COUNT}
            {
                texte += Separation("moyen", 4);
                texte += "\t\t\t\t<div>\n";
                texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                texte += "\t\t\t\t\t\tNombre de mariage\n";
                texte += "\t\t\t\t\t</span>\n";
                texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                texte += "\t\t\t\t\t\t" + evenement.N1_EVEN_texte + "\n";
                texte += "\t\t\t\t\t</span>\n";
                texte += "\t\t\t\t</div>\n";
            }
            // si association
            if (infoIndividu.N1_ASSO_liste != null)
            {
                foreach (GEDCOMClass.ASSOCIATION_STRUCTURE info_ASSO in infoIndividu.N1_ASSO_liste)
                {
                    texte += Separation("moyen", 4);
                    texte += "\t\t\t\t<div>\n";
                    texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                    texte += "\t\t\t\t\t\tAssociation\n";
                    texte += "\t\t\t\t\t</span>\n";
                    texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                    if (menu)
                    {
                        texte += "\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"" + info_ASSO.N0_ASSO + ".html\"></a>\n";
                    }
                    else
                    {
                        texte += "\t\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                    }
                    texte += "\t\t\t\t\t\t" + GEDCOMClass.AvoirPremierNomIndividu(info_ASSO.N0_ASSO);
                    if (GH.Properties.Settings.Default.VoirID == true)
                    {
                        texte += "\t\t\t\t\t\t[" + info_ASSO.N0_ASSO + "]\n";
                    }
                    texte += "\t\t\t\t\t\tRelation " + info_ASSO.N1_RELA + "\n";
                    // source association
                    List<string> listeLien = new List<string>();
                    texte += Avoir_lien_citation_source(info_ASSO.N1_SOUR_citation_liste_ID, liste_citation_ID_numero, 5);
                    texte += "\t\t\t\t\t</span>\n";
                    texte += "\t\t\t\t</div>\n";
                    // association note
                    if (info_ASSO.N1_NOTE_liste_ID != null)
                    {
                        texte += "\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                        texte += "\t\t\t\t\t\t&nbsp;\n";
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                        texte += Avoir_lien_note(info_ASSO.N1_NOTE_liste_ID, liste_note_ID_numero, true, 5);
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t</div>\n";
                    }
                }
            }
            // si N1__ANCES_CLE_FIXE
            if (infoIndividu.N1__ANCES_CLE_FIXE != null)
            {
                texte += Separation("moyen", 4);
                texte += "\t\t\t\t<div>\n";
                texte += "\t\t\t\t\t\tClé fixe (Ancestrologie) \n";
                texte += "\t\t\t\t\t\t" + infoIndividu.N1__ANCES_CLE_FIXE + "\n";
                texte += "\t\t\t\t</div>\n";
            }
            // si RIN ******************************************************************************************************************************
            if (infoIndividu.N1_RIN != null)
            {
                texte += Separation("moyen", 4);
                texte += "\t\t\t\t<div>\n";
                texte += "\t\t\t\t\tID d'enregistrement automatisé(RIN) \n";
                texte += "\t\t\t\t\t" + infoIndividu.N1_RIN + "\n";
                texte += "\t\t\t\t</div>\n";
            }
            // si REFN ******************************************************************************************************************************
            if (infoIndividu.N1_REFN_liste != null)
            {
                foreach (GEDCOMClass.USER_REFERENCE_NUMBER info_REFN in infoIndividu.N1_REFN_liste)
                {
                    texte += Separation("moyen", 4);
                    texte += "\t\t\t\t<div>\n";
                    texte += "\t\t\t\t\tNuméro de fichier d'enregistrement permanent(REFN) \n";
                    texte += "\t\t\t\t\t" + info_REFN.N0_REFN;
                    if (info_REFN.N1_TYPE != null)
                    {
                        texte += " Type " + info_REFN.N1_TYPE + "\n";
                    }
                    else
                    {
                        texte += "\n";
                    }
                    texte += "\t\t\t\t</div>\n";
                }
            }
            //int numeroSUBMITTER = 0;
            // si ANCI *****************************************************************************************************
            if (infoIndividu.N1_ANCI_liste_ID.Count > 0)
            {
                texte += Separation("moyen", 4);
                texte += "\t\t\t\t<div>\n";
                texte += "\t\t\t\t\tIl y a une recherche en cours pour identifier des ascendants supplémentaires. ";
                //string texteTemp = null;
                texte += Avoir_lien_chercheur(infoIndividu.N1_ANCI_liste_ID, liste_SUBMITTER_ID_numero, 5);
                texte += "\t\t\t\t</div>\n";
            }
            // si DESI *****************************************************************************************************
            if (infoIndividu.N1_DESI_liste_ID.Count > 0)
            {
                texte += Separation("moyen", 5);
                texte += "\t\t\t\t<div>\n";
                texte += "\t\t\t\t\tIl y a une recherche en cours pour identifier des descendants supplémentaires. ";
                //string texteTemp = null;
                texte += Avoir_lien_chercheur(infoIndividu.N1_DESI_liste_ID, liste_SUBMITTER_ID_numero, 5);
                texte += "\t\t\t\t</div>\n";
            }
            // si RFN *****************************************************************************************************
            if (infoIndividu.N1_RFN != null)
            {
                texte += Separation("moyen", 4);
                texte += "\t\t\t\t<div>\n";
                texte += "\t\t\t\t\tNuméro d'enregistrement permanent(RFN) \n";
                texte += "\t\t\t\t\t" + infoIndividu.N1_RFN + "\n";
                texte += "\t\t\t\t</div>\n";
            }
            // si AFN *****************************************************************************************************
            if (infoIndividu.N1_AFN != null)
            {
                texte += Separation("moyen", 5);
                texte += "\t\t\t\t<div>\n";
                texte += "\t\t\t\t\tNuméro de fichier ancestral(AFN) \n";
                texte += "\t\t\t\t\t" + infoIndividu.N1_AFN + "\n";
                texte += "\t\t\t\t</div>\n";
            }
            // si WWW
            if (infoIndividu.N1_WWW_liste != null)
            {
                foreach (string www in infoIndividu.N1_WWW_liste)
                {
                    texte += "\t\t\t\t<div>\n";
                    texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                    texte += "\t\t\t\t\t\tPage Web\n";
                    texte += "\t\t\t\t\t</span>\n";
                    texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                    texte += "\t\t\t\t\t\t" + www + "\n";
                    texte += "\t\t\t\t\t</span>\n";
                    texte += "\t\t\t\t</div>\n";
                }
            }
            // note ********************************************************************************************************
            if (infoIndividu.N1_NOTE_liste_ID.Count() > 0)
            {
                texte += Separation("moyen", 4);
                texte += "\t\t\t\t<div>\n";
                texte += Avoir_lien_note(infoIndividu.N1_NOTE_liste_ID, liste_note_ID_numero, true, 5);
                texte += "\t\t\t\t</div>\n";
            }
            //texte += "\t\t\t\t</div>\n";
            // chercheur
            if (infoIndividu.N1_SUBM_liste_ID.Count > 0 && GH.Properties.Settings.Default.voir_chercheur)
            {
                texte += Separation("mince", 5);
                texte += "\t\t\t\t\t<div>\n";
                //string texteTemp = null;
                texte += Avoir_lien_chercheur(infoIndividu.N1_SUBM_liste_ID, liste_SUBMITTER_ID_numero, 6);
                texte += "\n\t\t\t\t\t</div>\n";
            }
            // Date changement
            texte += Date_Changement_Bloc(
                        infoIndividu.N1_CHAN,
                        liste_note_ID_numero,
                        4,
                        false);
            texte += "\t\t\t</div>\n";
            // @conjoint Groupe ************************************************************************
            if (infoIndividu.N1_FAMS_liste_Conjoint.Count > 0)
            {
                groupe_conjoint = true;
                texte += Separation("large", 4);
                texte += "<a id=\"groupe_conjoint\"></a>\n";
                texte += Groupe("debut", 5);
                texte += "\t\t\t\t\t<table class=\"titre\">\n";
                texte += "\t\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t\t<td>\n";
                texte += "\t\t\t\t\t\t\t\tConjoint\n";
                texte += "\t\t\t\t\t\t\t</td>\n";
                texte += "\t\t\t\t\t\t</tr>\n";
                texte += "\t\t\t\t\t</table>\n";
                texte += Separation("mince", 5);
                texte += "\t\t\t\t<table class=\"tableau\">\n";
                texte += "\t\t\t\t\t<thead>\n";
                texte += "\t\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"cellule2LMB\" style=\"width:85px\">&nbsp;</th>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"cellule2LMB\">Nom</th>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"cellule2LMB date\">Naissance</th>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"cellule2LMB date\">Décès</th>\n";
                texte += "\t\t\t\t\t\t</tr>\n";
                texte += "\t\t\t\t\t</thead>\n";
                foreach (GEDCOMClass.SPOUSE_TO_FAMILY_LINK infoLienConjoint in infoIndividu.N1_FAMS_liste_Conjoint)
                {
                    string IDFamilleConjoint = infoLienConjoint.N0_ID;
                    string txtIDFamille;
                    if (GH.Properties.Settings.Default.VoirID == true)
                    {
                        txtIDFamille = " [" + IDFamilleConjoint + "]";
                    }
                    else
                    {
                        txtIDFamille = null;
                    }
                    GEDCOMClass.FAM_RECORD infoFamilleConjoint = GEDCOMClass.Avoir_info_famille(IDFamilleConjoint);

                    string IDConjoint;
                    if (infoFamilleConjoint.N1_HUSB == IDIndividu)
                    {
                        IDConjoint = infoFamilleConjoint.N1_WIFE;

                    }
                    else
                    {
                        IDConjoint = infoFamilleConjoint.N1_HUSB;
                    }
                    GEDCOMClass.INDIVIDUAL_RECORD infoConjoint;

                    (_, infoConjoint) = GEDCOMClass.Avoir_info_individu(IDConjoint);
                    string nomConjoint = GEDCOMClass.AvoirPremierNomIndividu(IDConjoint);
                    GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE NaissanceConjoint;
                    (_, NaissanceConjoint) = GEDCOMClass.AvoirEvenementNaissance(infoConjoint.N1_EVENT_Liste);
                    GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE DecesConjoint;
                    (_, DecesConjoint) = GEDCOMClass.AvoirEvenementDeces(infoConjoint.N1_EVENT_Liste);
                    texte += "\t\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t\t<td class=\"cellule2LMF\">\n";
                    if (txtIDFamille != null)
                    {
                        if (menu)
                        {
                            texte += "\t\t\t\t\t\t\t<a class=\"ficheFamille\"  href=../familles/" +
                                infoLienConjoint.N0_ID + ".html></a>\n";
                        }
                        else
                        {
                            texte += "\t\t\t\t\t\t\t<a class=\"ficheFamilleGris\"></a>\n";
                        }
                    }
                    else
                    {
                        texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                    }
                    if (GH.Properties.Settings.Default.VoirID)
                        texte += "[" + infoLienConjoint.N0_ID + "]\n";
                    texte += "\t\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t\t<td class=\"cellule2LMF\">\n";
                    texte += "\t\t\t\t\t\t\t" + nomConjoint + "\n";

                    if (GH.Properties.Settings.Default.VoirID == true)
                    {
                        texte += "\t\t\t\t\t\t<span style=\"font-size:small\">[" +
                            IDConjoint + "]</span>" + "\n";
                    }
                    if (infoLienConjoint.N1_NOTE_liste_ID.Count > 0)
                    {
                        texte += Avoir_lien_note(infoLienConjoint.N1_NOTE_liste_ID, liste_note_ID_numero, false, 7);
                    }
                    texte += "\t\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t\t<td class=\"cellule2LMF\">\n";
                    string date = null;
                    if (GH.Properties.Settings.Default.date_longue)
                    {
                        date = GEDCOMClass.ConvertirDateTexte(NaissanceConjoint.N2_DATE);
                    }
                    else
                    {
                        date = NaissanceConjoint.N2_DATE;
                    }
                    texte += "\t\t\t\t\t\t\t" + date + "\n";
                    texte += "\t\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t\t<td class=\"cellule2LMF\">\n";
                    if (GH.Properties.Settings.Default.date_longue)
                    {
                        date = GEDCOMClass.ConvertirDateTexte(DecesConjoint.N2_DATE);
                    }
                    else
                    {
                        date = DecesConjoint.N2_DATE;
                    }
                    texte += "\t\t\t\t\t\t\t" + date + "\n";
                    texte += "\t\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t</tr>\n";
                }
                texte += "\t\t\t\t</table>\n";
                texte += Groupe("fin", 3);
            }
            // @Parent Groupe **************************************************************************
            GEDCOMClass.CHILD_TO_FAMILY_LINK infoFamille = GEDCOMClass.AvoirInfoFamilleEnfant(IDIndividu);
            if (
                infoFamille.N0_FAMC != null ||
                infoFamille.N1_PEDI != null ||
                infoFamille.N1_STAT != null ||
                infoFamille.N1_NOTE_liste_ID != null
                )
            {
                groupe_parent = true;
                // père
                string pereID = GEDCOMClass.Avoir_famille_conjoint_ID(infoFamille.N0_FAMC);
                GEDCOMClass.INDIVIDUAL_RECORD infoPere;
                (_, infoPere) = GEDCOMClass.Avoir_info_individu(pereID);
                string nomPere = Avoir_premier_nom_individu(infoPere);
                GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE NaissancePere;
                (_, NaissancePere) = GEDCOMClass.AvoirEvenementNaissance(infoPere.N1_EVENT_Liste);
                GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE DecesPere;
                (_, DecesPere) = GEDCOMClass.AvoirEvenementDeces(infoPere.N1_EVENT_Liste);
                // mère                
                string mereID = GEDCOMClass.Avoir_famille_conjointe_ID(infoFamille.N0_FAMC);
                GEDCOMClass.INDIVIDUAL_RECORD infoMere;
                (_, infoMere) = GEDCOMClass.Avoir_info_individu(mereID);
                string nomMere = Avoir_premier_nom_individu(infoMere);
                GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE NaissanceMere;
                (_, NaissanceMere) = GEDCOMClass.AvoirEvenementNaissance(infoMere.N1_EVENT_Liste);
                GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE DecesMere;
                (_, DecesMere) = GEDCOMClass.AvoirEvenementDeces(infoMere.N1_EVENT_Liste);
                // afficher ou pas le ID
                string txtIDPere = null;
                string txtIDMere = null;
                if (GH.Properties.Settings.Default.VoirID == true)
                {
                    if (pereID != null && nomPere != null)
                    {
                        txtIDPere = " [" + pereID + "]";
                    }
                    if (mereID != null && nomMere != null)
                    {
                        txtIDMere = " [" + mereID + "]";
                    }
                }
                texte += Separation("mince", 5);
                texte += "<a id=\"groupe_parent\"></a>\n";
                texte += Groupe("debut", 3);
                texte += "\t\t\t\t<table class=\"titre\">\n";
                texte += "\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t<td>\n";
                texte += "\t\t\t\t\t\tParent\n";
                if (GH.Properties.Settings.Default.VoirID == true)
                {
                    texte += "\t\t\t\t\t\t<div style=\"font-size:small\">[" + infoFamille.N0_FAMC + "]</div>" + "\n";
                }
                texte += "\t\t\t\t\t</td>\n";
                texte += "\t\t\t\t</tr>\n";
                texte += "\t\t\t\t</table>\n";
                texte += Separation("mince", 5);
                texte += "\t\t\t\t<table class=\"tableau\">\n";
                texte += "\t\t\t\t\t<thead>\n";
                texte += "\t\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"cellule2LMB\" style=\"width:85px\">\n";
                if (infoFamille.N0_FAMC != null)
                {
                    if (menu)
                    {
                        texte += "\t\t\t\t\t\t\t\t<a class=\"ficheFamille\"  href=\"../familles/" + infoFamille.N0_FAMC + ".html\"></a>\n";
                    }
                    else
                    {
                        texte += "\t\t\t\t\t\t\t\t<a class=\"ficheFamilleGris\"></a>\n";
                    }
                }
                else
                {
                    texte += "\t\t\t\t\t\t\t\t<a class=\"ficheFamilleGris\"></a>\n";
                }
                texte += "\t\t\t\t\t\t\t</th>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"cellule2LMB\">Nom</th>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"date cellule2LMB\">Naissance</th>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"date cellule2LMB\">Décès</th>\n";
                texte += "\t\t\t\t\t\t</tr>\n";
                texte += "\t\t\t\t\t</thead>\n";
                texte += "\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t<td class=\"cellule2LMF\" style=\"width:85px\">\n";
                if (pereID != null)
                {
                    if (menu)
                    {
                        texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"" + pereID + ".html\"></a>\n";
                    }
                    else
                    {
                        texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                    }
                }
                else
                {
                    texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                }
                texte += "\t\t\t\t\t\t\tPère\n";
                texte += "\t\t\t\t\t\t</td>\n";
                texte += "\t\t\t\t\t\t<td class=\"cellule2LMF\">" + nomPere + txtIDPere + "</td>\n";
                string date;
                if (GH.Properties.Settings.Default.date_longue)
                {
                    date = GEDCOMClass.ConvertirDateTexte(NaissancePere.N2_DATE);
                }
                else
                {
                    date = NaissancePere.N2_DATE;
                }
                texte += "\t\t\t\t\t\t<td class=\"date cellule2LMF\">" + date + "</td>\n";
                if (GH.Properties.Settings.Default.date_longue)
                {
                    date = GEDCOMClass.ConvertirDateTexte(DecesPere.N2_DATE);
                }
                else
                {
                    date = DecesPere.N2_DATE;
                }
                texte += "\t\t\t\t\t\t<td class=\"date cellule2LMF\">" + date + "</td>\n";
                texte += "\t\t\t\t\t</tr>\n";
                texte += "\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t<td class=\"cellule2LMf\" style=\"width:85px\">\n";
                if (mereID != null)
                {
                    if (menu)
                    {
                        texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"" +
                            mereID + ".html\"></a>\n";
                    }
                    else
                    {
                        texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                    }
                }
                else
                {
                    texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                }
                texte += "\t\t\t\t\t\t\tMère\n";
                texte += "\t\t\t\t\t\t</td>\n";
                texte += "\t\t\t\t\t\t<td class=\"cellule2LMF\">" + nomMere + txtIDMere + "</td>\n";
                if (GH.Properties.Settings.Default.date_longue)
                {
                    date = GEDCOMClass.ConvertirDateTexte(NaissanceMere.N2_DATE);
                }
                else
                {
                    date = NaissanceMere.N2_DATE;
                }
                texte += "\t\t\t\t\t\t<td class=\"date cellule2LMF\">" + date + "</td>\n";
                if (GH.Properties.Settings.Default.date_longue)
                {
                    date = GEDCOMClass.ConvertirDateTexte(DecesMere.N2_DATE);
                }
                else
                {
                    date = DecesMere.N2_DATE;
                }
                texte += "\t\t\t\t\t\t<td class=\"date cellule2LMF\">" + date + "</td>\n";
                texte += "\t\t\t\t\t</tr>\n";
                texte += "\t\t\t\t</table>\n";
                if (infoFamille.N1_PEDI != null || infoFamille.N1_STAT != null || infoFamille.N1_NOTE_liste_ID != null)
                {
                    texte += "\t\t\t<div style=\"border:2px solid #000;border-top:0px solid #000;padding:10px;\">\n";
                    if (infoFamille.N1_PEDI != null)
                    {
                        texte += Separation("mince", 5);
                        texte += "\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                        texte += "\t\t\t\t\t\tPédigrée\n";
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                        texte += "\t\t\t\t\t\t" + infoFamille.N1_PEDI + "\n";
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t</div>\n";
                    }
                    if (infoFamille.N1_STAT != null)
                    {
                        texte += Separation("mince", 5);
                        texte += "\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                        texte += "\t\t\t\t\t\tStatus\n";
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                        texte += "\t\t\t\t\t\t" + infoFamille.N1_STAT + "\n";
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t</div>\n";
                    }
                    if (infoFamille.N1_NOTE_liste_ID != null)
                    {
                        texte += Separation("mince", 5);
                        texte += "\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                        texte += Avoir_lien_note(infoFamille.N1_NOTE_liste_ID, liste_note_ID_numero, true, 6);
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t</div>\n";
                    }
                    texte += "\t\t\t</div>\n";
                }
                texte += Groupe("fin", 3);
            }
            //@média Groupe ****************************************************************************
            {
                if (GH.Properties.Settings.Default.voir_media)
                {
                    if (infoIndividu.N1_OBJE_liste != null)
                    {
                        groupe_media = true;
                        int totalMedia = infoIndividu.N1_OBJE_liste.Count;
                        int nombreMedia = 0;
                        foreach (string IDMedia in infoIndividu.N1_OBJE_liste)
                        {
                            if (IDMedia != IDPortrait)
                            {
                                nombreMedia++;
                            }
                        }
                        if (nombreMedia > 0)
                        {
                            texte += Separation("large", 4);
                            texte += "<a id=\"groupe_media\"></a>\n";
                            texte += Groupe("debut", 3);
                            texte += "\t\t\t\t<table class=\"titre\">\n";
                            texte += "\t\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t\t<td>\n";
                            if (nombreMedia == 1)
                                texte += "\t\t\t\t\t\t\tMédia\n";
                            else
                                texte += "\t\t\t\t\t\t\tMédias\n";
                            texte += "\t\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t</tr>\n";
                            texte += "\t\t\t\t</table>\n";
                            texte += Separation("mince", 4);
                            texte += "\t\t\t\t<table class=\"tableauMedia\">\n";
                            texte += "\t\t\t\t\t<tr>\n";
                            int cRanger = 0;
                            foreach (string IDMedia in infoIndividu.N1_OBJE_liste)
                            {
                                GEDCOMClass.MULTIMEDIA_RECORD mediaInfo = GEDCOMClass.Avoir_info_media(IDMedia);
                                if (mediaInfo != null)
                                {
                                    texte += "\t\t\t\t\t\t<td class=\"tableauMedia\">\n";
                                    (   temp,
                                        liste_note_ID_numero,
                                        liste_citation_ID_numero,
                                        liste_source_ID_numero,
                                        liste_repo_ID_numero
                                    ) = Objet(
                                            mediaInfo,
                                            sousDossier,
                                            dossierSortie,
                                            liste_note_ID_numero,
                                            liste_citation_ID_numero,
                                            liste_source_ID_numero,
                                            liste_repo_ID_numero,
                                            6);
                                    texte += temp;
                                    texte += "\t\t\t\t\t\t</td>\n";
                                }
                                cRanger++;

                                if (cRanger % 3 == 0 && cRanger != totalMedia)
                                {

                                    texte += "\t\t\t\t\t</tr>\n";
                                    texte += "\t\t\t\t\t<tr>\n";
                                }
                            }
                            if (cRanger % 3 == 1)
                            {
                                texte += "\t\t\t\t\t\t<td class=\"tableauMedia\">\n";
                                texte += "\t\t\t\t\t\t</td>\n";
                                texte += "\t\t\t\t\t\t<td class=\"tableauMedia\">\n";
                                texte += "\t\t\t\t\t\t</td>\n";
                            }
                            if (cRanger % 3 == 2)
                            {
                                texte += "\t\t\t\t\t\t<td class=\"tableauMedia\">\n";
                                texte += "\t\t\t\t\t\t</td>\n";
                            }
                            texte += "\t\t\t\t\t</tr>\n";
                            texte += "\t\t\t\t</table>\n";
                            texte += Groupe("fin", 3);
                        }
                    }
                }
            }
            //@ordonnance Groupe ***********************************************************************
            (temp, numeroCarte, groupe_ordonnance) = Groupe_ordonnance(
                    infoIndividu.N1_LDS_liste, 
                    dateNaissance, 
                    numeroCarte,
                    liste_note_ID_numero,
                    liste_citation_ID_numero,
                    3);
            texte += temp;
            //@Événement Groupe ************************************************************************
            (temp, numeroCarte, groupe_evenement) = Groupe_evenement(
                    infoIndividu.N1_EVENT_Liste, 
                    dateNaissance,
                    "",
                    "",
                    numeroCarte,
                    sousDossier,
                    dossierSortie,
                    liste_note_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    3);
            texte += temp;
            //@Attribut Groupe *************************************************************************
            (temp, _, groupe_attribut) = Groupe_attribut(
                    infoIndividu.N1_Attribute_liste,
                    numeroCarte,
                    sousDossier,
                    dossierSortie,
                    liste_note_ID_numero,
                    liste_citation_ID_numero,
                    liste_source_ID_numero,
                    liste_repo_ID_numero,
                    0);
            texte += temp;
            //@chercheur Groupe ************************************************************************
            (temp, groupe_chercheur) =
                Groupe_chercheur(
                liste_SUBMITTER_ID_numero, 
                dossierSortie, 
                "individus", 
                liste_note_ID_numero,
                3);
            texte += temp;
            // @citation Groupe ************************************************************************
            if (liste_citation_ID_numero.Count > 0)
            {
                // Générer le texte HTML
                (temp, groupe_citation) =
                        Groupe_citation(
                        dossierSortie,
                        "individus",
                        liste_note_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        3);
                texte += temp;
            }
            // @source Groupe **************************************************************************
            if (liste_source_ID_numero.Count() > 0)
            {
                // Générer le texte HTML
                (temp, groupe_source) = Groupe_source(
                        dossierSortie,
                        "individus",
                        liste_note_ID_numero,
                        liste_citation_ID_numero,
                        liste_source_ID_numero,
                        liste_repo_ID_numero,
                        3);
                texte += temp;
            }
            // @Depot Groupe ***************************************************************************
            (temp, groupe_depot) =  Groupe_depot( liste_note_ID_numero, liste_repo_ID_numero, 3);
            texte += temp;
            // @note Groupe ****************************************************************************
            if (liste_note_ID_numero.Count() > 0)
            {
                (temp,groupe_note) = Groupe_note(
                        "individu",
                        liste_note_ID_numero,
                        liste_citation_ID_numero,
                        3);
                texte += temp;
            }
            // menu hamburger
            texte += "\t\t\t\t<div class=\"hamburger\">\n";
            texte += "\t\t\t\t\t<button class=\"boutonHamburger\"></button>\n";
            texte += "\t\t\t\t\t<div class=\"hamburger-contenu\">\n";
            texte += "\t\t\t\t\t\t<a href=\"#groupe_nom\">Nom</a>\n";
            if (groupe_attribut) texte += "\t\t\t\t\t\t<a href=\"#groupe_attribut\">Attribut</a>\n";
            if (groupe_chercheur) texte += "\t\t\t\t\t\t<a href=\"#groupe_chercheur\">Chercheur</a>\n";
            if (groupe_citation) texte += "\t\t\t\t\t\t<a href=\"#groupe_citation\">Citation</a>\n";
            if (groupe_conjoint) texte += "\t\t\t\t\t\t<a href=\"#groupe_conjoint\">Conjoint</a>\n";
            if (groupe_depot) texte += "\t\t\t\t\t\t<a href=\"#groupe_depot\">Dépôt</a>\n";
            if (groupe_evenement) texte += "\t\t\t\t\t\t<a href=\"#groupe_evenement\">Événement</a>\n";
            if (groupe_media) texte += "\t\t\t\t\t\t<a href=\"#groupe_media\">Média</a>\n";
            if (groupe_ordonnance) texte += "\t\t\t\t\t\t<a href=\"#groupe_ordonnance\">Ordonnance</a>\n";
            if (groupe_note) texte += "\t\t\t\t\t\t<a href=\"#groupe_note\">Note</a>\n";
            if (groupe_parent) texte += "\t\t\t\t\t\t<a href=\"#groupe_parent\">Parent</a>\n";
            if (groupe_source) texte += "\t\t\t\t\t\t<a href=\"#groupe_source\">Source</a>\n";
            texte += "\t\t\t\t\t</div>\n";
            texte += "\t\t\t\t</div>\n";
            // bas de page
            texte += Bas_Page();
            try
            {
                System.IO.File.WriteAllText(nomFichierIndividu, texte);
            }
            catch (Exception msg)
            {
                string message = "Ne peut pas écrire le fichier" + nomFichierIndividu + ".";
                GEDCOMClass.Voir_message(message, msg.Message, Avoir_code_erreur());
            }
            return IDCourantIndividu;
        }
        public string Individu_Index_Bouton()
        {
            string texte = null;
            bool[] alphabette = new bool[27];
            ListView lvChoixIndividu = Application.OpenForms["GH"].Controls["lvChoixIndividu"] as ListView;
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
            texte += "\t\t\t<div style=\"width:860px;border-radius:10px;background-color:#6464FF;margin-left:auto;margin-right:auto;padding:5px;\">\n";
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
            return texte;
        }
        private static void MessageErreur(string message, [CallerLineNumber] int lineNumber = 0)
        {
            MessageBox.Show(message, "Erreur H" + lineNumber);
        }
        private (string, bool) Groupe_citation(
            string dossierSortie,
            string sousDossier,
            List<ID_numero> liste_note_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            int tab = 0)
        {
            string erreur = Avoir_code_erreur();
            try
            {
                erreur = Avoir_code_erreur();
                if (liste_citation_ID_numero == null)
                    return ("", false);
                string espace = Tabulation(tab);
                string texte = Separation("large", tab);
                string temp;
                texte += "<a id=\"groupe_citation\"></a>\n";
                texte += Groupe("debut", tab);
                texte += espace + "\t<table class=\"titre\">\n";
                texte += espace + "\t\t<tr>\n";
                texte += espace + "\t\t\t<td>\n";
                texte += espace + "\t\t\t\t<img  style=\"vertical-align:middle;heigh:64px\" src=\"..\\commun\\citation.svg\" />\n";
                if (liste_citation_ID_numero.Count == 1)
                    texte += espace + "\t\t\t\t Citation\n";
                else
                    texte += espace + "\t\t\t\t Citations\n";
                texte += espace + "\t\t\t</td>\n";
                texte += espace + "\t\t</tr>\n";
                texte += espace + "\t</table>\n";
                foreach (ID_numero item_citation in liste_citation_ID_numero)
                {
                    erreur = Avoir_code_erreur();
                    // avoir l'info de la citation
                    GEDCOMClass.SOURCE_CITATION info_citation = GEDCOMClass.Avoir_info_citation(item_citation.ID);
                    texte += espace + "\t<span style=\"display: table-cell;\"><a id=\"citation-" +
                        item_citation.numero.ToString() + "\"></a></span>\n";
                    texte += Separation("mince", tab + 1);

                    texte += espace + "\t<table class=\"tableau\">\n";
                    //if (info_citation != null)
                    {
                        erreur = Avoir_code_erreur();
                        texte += espace + "\t\t<tr>\n";
                        texte += espace + "\t\t\t<td style=\"background-color:#BBB;border:2px solid #000;margin-left:5px;vertical-align:top;\">\n";// ne pas enlever Border.
                        texte += espace + "\t\t\t\t<span class=\"citation\">" + item_citation.numero.ToString() + " </span>\n";
                        if (info_citation.N0_Titre != null)
                        {
                            texte += espace + 
                                "\t\t\t\t<span style=\"display:table-cell;text-align:top; padding-left:5px\">" + info_citation.N0_Titre + "</span>\n";
                        } 
                        if (info_citation.N0_ID_source != null)
                        {
                            texte += Avoir_lien_source(info_citation.N0_ID_source, liste_source_ID_numero, 4);
                        }
                        texte += espace + "\t\t\t\t</span>\n";
                        texte += espace + "\t\t\t</td>\n";
                        texte += espace + "\t\t</tr>\n";
                        erreur = Avoir_code_erreur();
                        // page
                        if (info_citation.N1_PAGE != null)
                        {
                            erreur = Avoir_code_erreur();
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td>\n";
                            texte += espace + "\t\t\t\tpage " + info_citation.N1_PAGE;
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // media
                        if (GH.Properties.Settings.Default.voir_media)
                        {
                            erreur = Avoir_code_erreur();
                            if (info_citation.N1_OBJE_ID_liste.Count > 0)
                            {
                                erreur = Avoir_code_erreur();
                                texte += espace + "\t\t<tr>\n";
                                texte += espace + "\t\t\t<td>\n";
                                texte += espace + "\t\t\t\t<table class=\"tableauMedia retrait_point5in\" style=\"border:2px;width:480px\">\n";
                                texte += espace + "\t\t\t\t\t<tr>\n";
                                int cRanger = 0;
                                int totalOBJE = info_citation.N1_OBJE_ID_liste.Count;
                                foreach (string media in info_citation.N1_OBJE_ID_liste)
                                {
                                    erreur = Avoir_code_erreur();
                                    GEDCOMClass.MULTIMEDIA_RECORD info_OBJE = GEDCOMClass.Avoir_info_media(media);
                                    texte += espace + "\t\t\t\t\t\t<td class=\"tableauMedia\" style=\"width:50%\">\n";
                                    (temp, liste_note_ID_numero, liste_citation_ID_numero, liste_source_ID_numero, liste_repo_ID_numero) =
                                    Objet(
                                        info_OBJE,
                                        sousDossier,
                                        dossierSortie,
                                        liste_note_ID_numero,
                                        liste_citation_ID_numero,
                                        liste_source_ID_numero,
                                        liste_repo_ID_numero,
                                        tab + 6);
                                    texte += temp;
                                    texte += espace + "\t\t\t\t\t\t</td>\n";
                                    cRanger++;
                                    if (cRanger % 2 == 0 && cRanger != totalOBJE)
                                    {
                                        texte += espace + "\t\t\t\t\t</tr>\n";
                                        texte += espace + "\t\t\t\t\t<tr>\n";
                                    }
                                }
                                if (cRanger % 2 == 1)
                                {
                                    erreur = Avoir_code_erreur();
                                    texte += espace + "\t\t\t\t\t\t<td class=\"tableauMedia\" style=\"border:0px;width:50%\">\n";
                                    texte += "\t\t\t\t\t\t</td>\n";
                                }
                                texte += espace + "\t\t\t\t\t</tr>\n";
                                texte += espace + "\t\t\t\t</table>\n";
                                texte += espace + "\t\t\t</td>\n";
                                texte += espace + "\t\t</tr>\n";
                            }
                        }
                        // citation
                        if (info_citation.N1_EVEN != null)
                        {
                            erreur = Avoir_code_erreur();
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td>\n";
                            texte += espace + "\t\t\t\t<div style=\"border:0px solid #00F;padding-left:.5in;\">Événement: " +
                                GEDCOMClass.Convertir_EVENT_titre(info_citation.N1_EVEN) + "\n";
                            texte += espace + "\t\t\t\t</div>\n";
                            if (info_citation.N2_EVEN_ROLE != null)
                            {
                                texte += espace + "\t\t\t\t<div style=\"border:0px solid #00F;padding-left:.5in;\">Role dans l'événement: " +
                                    GEDCOMClass.Convertir_Sujet(info_citation.N2_EVEN_ROLE) + "\n";
                                texte += espace + "\t\t\t\t</div>\n";
                            }
                            texte += Separation("mince", tab + 4);
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // DATA
                        if (info_citation.N2_DATA_DATE != "" && info_citation.N2_DATA_DATE != null)
                        {
                            erreur = Avoir_code_erreur();
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td>\n";
                            texte += espace + "\t\t\t\t<div style=\"border:0px solid #00F;padding-left:.5in;\">Data: ";
                            if (GH.Properties.Settings.Default.date_longue)
                            {
                                texte += GEDCOMClass.ConvertirDateTexte(info_citation.N2_DATA_DATE);
                            }
                            else
                            {
                                texte += info_citation.N2_DATA_DATE;
                            }
                            texte += espace + "\t\t\t\t</div>\n";
                            if (info_citation.N2_DATA_TEXT != "" && info_citation.N2_DATA_TEXT != null)
                            {
                                texte += espace + "\t\t\t\t<div style=\"border:0px solid #00F;padding-left:1in;\">Texte: " +
                                    info_citation.N2_DATA_TEXT + "</div>\n";
                            }
                            texte += Separation("mince", tab + 4);
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // QUAY
                        if (info_citation.N1_QUAY != null)
                        {
                            erreur = Avoir_code_erreur();
                            string temp2 = "";
                            if (info_citation.N1_QUAY == "0")
                            {
                                temp2 = "Preuve non fiable ou données estimées.";
                            }
                            else if (info_citation.N1_QUAY == "1")
                            {
                                temp2 = "Fiabilité douteuse des preuves (entretiens, recensement, généalogies orales ou potentiel de biais par exemple, une autobiographie).";
                            }
                            else if (info_citation.N1_QUAY == "2")
                            {
                                temp2 = "Preuve secondaire, données officiellement enregistrées quelque temps après l'évènement.";
                            }
                            else if (info_citation.N1_QUAY == "3")
                            {
                                temp2 = "Preuve directe et principale utilisée, ou par dominance de la preuve.";
                            }
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td>\n";
                            texte += espace + "\t\t\t\t<div style=\"border:0px solid #00F;padding-left:.5in;\">Crédibilité: " + temp2 + "</div>\n";
                            texte += Separation("mince", tab + 4);
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // note
                        if (info_citation.N1_NOTE_liste_ID != null)
                        {
                            erreur = Avoir_code_erreur();
                            if (info_citation.N1_NOTE_liste_ID.Count > 0)
                            {
                                erreur = Avoir_code_erreur();
                                texte += espace + "\t\t<tr>\n";
                                texte += espace + "\t\t\t<td style=\"border:0px solid #0e0;text-align:left;padding-left:.5in\">\n";
                                texte += Avoir_lien_note(info_citation.N1_NOTE_liste_ID, liste_note_ID_numero, true, tab + 4);
                                texte += espace + "\t\t\t</td>\n";
                                texte += espace + "\t\t</tr>\n";
                            }
                        }
                 
                        texte += espace + "\t</table>\n";
                    }
                }
                texte += Groupe("fin", tab);
                return (texte, true);
            }
            catch (Exception msg)
            {
                GEDCOMClass.Voir_message("La section Citation sera manquante pour cet infdividu.", msg.Message, erreur);
                return (null, true);
            }
        }
        private (string, bool) Groupe_note(
            string sousDossier,
            List<ID_numero> liste_note_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            int tab = 0)
        {
            if (liste_note_ID_numero == null) 
                return ("", false);
            string temp;
            string espace = Tabulation(tab);
            string texte = Separation("large", tab);
            texte += "<a id=\"groupe_note\"></a>";
            texte += Groupe("debut", tab);
            texte += espace + "\t<table class=\"titre\">\n";
            texte += espace + "\t\t<tr>\n";
            texte += espace + "\t\t\t<td>\n";
            if (sousDossier == "index")
                texte += espace + "\t\t\t\t<img  style=\"vertical-align:middle;heigh:64px\" src=\"commun\\note.svg\" />\n";
            else
                texte += espace + "\t\t\t\t<img  style=\"vertical-align:middle;heigh:64px\" src=\"..\\commun\\note.svg\" />\n";
            if (liste_note_ID_numero.Count == 1) 
                texte += espace + "\t\t\t\tNote\n"; 
            else 
                texte += espace + "\t\t\t\tNotes\n";
            texte += espace + "\t\t\t</td>\n";
            texte += espace + "\t\t</tr>\n";
            texte += espace + "\t</table>\n";
            texte += Separation("mince", tab + 1);
            //foreach (ID_numero note_ID_numero in liste_note_ID_numero)
            for(int f = 0; f < liste_note_ID_numero.Count;f++)
            {
                GEDCOMClass.NOTE_RECORD info_note = GEDCOMClass.Avoir_Info_Note(liste_note_ID_numero[f].ID);
                texte += espace + "\t<span style=\"display: table-cell;\"><a id=\"note-" + liste_note_ID_numero[f].numero.ToString() + "\"></a></span>\n";
                texte += espace + "\t<table class=\"tableau\">\n";
                //GEDCOM.GEDCOMClass.SOURCE_RECORD infoSourceDetail = GEDCOMClass.AvoirInfoSource(info_note.N0_ID);
                {
                    texte += espace + "\t\t<tr>\n";
                    texte += espace + "\t\t\t<td style=\"background-color:#BBB;border:2px solid #000;margin-left:5px;vertical-align:top;\">\n";// ne pas enlever Border.
                    //texte += espace + "\t\t\t\t<span style=\"display: table-cell;\"><a id=\"note-" + note_ID_numero.numero.ToString() + "\"></a></span>\n";
                    texte += espace + "\t\t\t\t<span class=\"note\">" + liste_note_ID_numero[f].numero.ToString() + "</span>\n";
                    texte += espace + "\t\t\t\t<span style=\"display:table-cell;text-align:top; padding-left:5px\">\n";
                    texte += espace + "\t\t\t\t\t" + info_note.N0_NOTE_Texte + "\n";
                    if (GH.Properties.Settings.Default.deboguer) texte += "\t\t\t\t {" + info_note.N0_ID + "}\n";
                    texte += espace + "\t\t\t\t</span>\n";
                    temp = Avoir_lien_citation_source(info_note.N1_SOUR_citation_liste_ID, liste_citation_ID_numero, tab + 4);
                    texte += temp;
                    texte += Separation("mince", tab + 4);
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t</tr>\n";
                }
                if (info_note.N1_REFN_liste != null)
                {
                    foreach (GEDCOMClass.USER_REFERENCE_NUMBER info in info_note.N1_REFN_liste)
                    {
                        texte += espace + "\t\t<tr>\n";
                        texte += espace + "\t\t\t<td>\n";
                        texte += espace + "\t\t\t\tRéférence " + info.N0_REFN + " type " + info.N1_TYPE + "\n";
                        texte += espace + "\t\t\t</td>\n";
                        texte += espace + "\t\t</tr>\n";
                    }
                }
                if (info_note.N1_RIN != null)
                {
                    texte += espace + "\t\t<tr>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\tID d'enregistrement automatisé(RIN) " + info_note.N1_RIN + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t</tr>\n";
                }
                if (info_note.N1_CHAN.N1_CHAN_DATE != null && GH.Properties.Settings.Default.voir_date_changement)
                {
                    texte += espace + "\t\t<tr>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += Date_Changement_Bloc(
                        info_note.N1_CHAN,
                        liste_note_ID_numero,
                        tab + 3,
                        false);
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t</tr>\n";
                }
                texte += espace + "\t</table>\n";
                texte += Separation("mince", tab + 1);
            }
            texte += Groupe("fin", tab);
            return (texte, true);
        }
        private (string, bool) Groupe_source(
            string dossierSortie,
            string sousDossier,
            List<ID_numero> liste_note_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            int tab = 0)
        {
            if (liste_source_ID_numero == null) return ("", false);
            if (liste_source_ID_numero.Count == 0)return ("", false);
            string espace = Tabulation(tab);
            string texte = Separation("large", tab);
            string temp;
            texte += "<a id=\"groupe_source\"></a>\n";
            texte += Groupe("debut", tab);
            texte += espace + "\t<table class=\"titre\">\n";
            texte += espace + "\t\t<tr>\n";
            texte += espace + "\t\t\t<td>\n";
            texte += espace + "\t\t\t\t<img  style=\"vertical-align:middle;heigh:64px\" src=\"..\\commun\\livre.svg\" />\n";
            if (liste_source_ID_numero.Count == 1)
                texte += espace + "\t\t\t\t Source\n";
            else
                texte += espace + "\t\t\t\t Sources\n";
            texte += espace + "\t\t\t</td>\n";
            texte += espace + "\t\t</tr>\n";
            texte += espace + "\t</table>\n";
            int compteurMedia = 0;
            //bool Ok_Titre;
            foreach (ID_numero item_source in liste_source_ID_numero)
            {
                compteurMedia++;
                GEDCOM.GEDCOMClass.SOURCE_RECORD info_source = GEDCOMClass.Avoir_info_SOURCE(item_source.ID);
                {
                    texte += espace + "\t\t\t\t<span style=\"display: table-cell;\"><a id=\"source-" + item_source.numero.ToString() + "\"></a></span>\n";
                    texte += Separation("mince", tab + 1);
                    texte += espace + "\t<table class=\"tableau\">\n";
                    texte += espace + "\t\t<tr>\n";
                    texte += espace + "\t\t\t<td colspan=2 style=\"background-color:#BBB;border:2px solid #000;margin-left:5px;vertical-align:top;\">\n";// ne pas enlever Border.
                    texte += espace + "\t\t\t\t<span class=\"source\">" + item_source.numero.ToString() + "</span> \n";
                    texte += espace + "\t\t\t\t<span style=\"display:table-cell;text-align:top; padding-left:5px\">\n";
                    texte += espace + "\t\t\t\t\t" + info_source.N1_TITL + "\n";
                    if (GH.Properties.Settings.Default.deboguer)
                        texte += espace + "\t\t\t\t\t" + " {" + info_source.N0_ID + "}\n";
                    texte += espace + "\t\t\t\t</span>\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t</tr>\n";
                    {
                        // média source
                        if (info_source.N1_OBJE_liste_ID.Count > 0 && GH.Properties.Settings.Default.voir_media)
                        {
                            int totalOBJE = info_source.N1_OBJE_liste_ID.Count;
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td colspan=2>\n";
                            texte += "\t\t\t\t<table class=\"tableauMedia retrait_point25in\" style=\"border:0px;width:480px\">\n";
                            texte += "\t\t\t\t\t<tr>\n";
                            int cRanger = 0;
                            foreach (string media in info_source.N1_OBJE_liste_ID)
                            {
                                GEDCOMClass.MULTIMEDIA_RECORD OBJE = GEDCOMClass.Avoir_info_media(media);
                                texte += "\t\t\t\t\t\t<td class=\"tableauMedia\" style=\"width:50%\">\n";
                                string textMedia = "";
                                (temp, liste_note_ID_numero, liste_citation_ID_numero, liste_source_ID_numero, liste_repo_ID_numero) =
                                    Objet(
                                        OBJE,
                                        sousDossier,
                                        dossierSortie,
                                        liste_note_ID_numero,
                                        liste_citation_ID_numero,
                                        liste_source_ID_numero,
                                        liste_repo_ID_numero,
                                        tab + 6);
                                texte += temp;
                                if (textMedia != "") texte += textMedia;
                                texte += "\t\t\t\t\t\t</td>\n";
                                cRanger++;
                                if (cRanger % 2 == 0 && cRanger != totalOBJE)
                                {
                                    texte += "\t\t\t\t\t</tr>\n";
                                    texte += "\t\t\t\t\t<tr>\n";
                                }
                            }
                            if (cRanger % 2 == 1)
                            {
                                texte += "\t\t\t\t\t\t<td class=\"tableauMedia\" style=\"border:0px;width:50%\">\n";
                                texte += "\t\t\t\t\t\t</td>\n";
                            }
                            texte += "\t\t\t\t\t</tr>\n";
                            texte += "\t\t\t\t</table>\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        if (info_source.N1_EVEN !=null)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td>\n";
                            texte += espace + "\t\t\t\tType d'événement (Ancestrologie)";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t\t<td>\n";
                            texte += espace + "\t\t\t\t" + info_source.N1_EVEN;
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // data
                        if (info_source.N2_DATA_EVEN != null || info_source.N3_DATA_DATE != null || info_source.N3_DATA_PLAC != null ||
                            info_source.N2_DATA_AGNC != null || info_source.N2_DATA_NOTE_liste_ID != null)
                        {

                            // data événemnt
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td colspan=2>\n";
                            texte += espace + "\t\t\t\t<table>\n";
                            
                            if (info_source.N2_DATA_EVEN != null)
                            {
                                texte += espace + "\t\t\t\t\t<tr>\n";
                                texte += espace + "\t\t\t\t\t\t<td style=\"width:150px\">\n";
                                texte += espace + "\t\t\t\tData\n";
                                texte += espace + "\t\t\t\t\t\t</td>\n";
                                texte += espace + "\t\t\t\t\t\t<td style=\"width:150px\">\n";
                                texte += espace + "\t\t\t\t\t\t\tÉvénement:\n";
                                texte += espace + "\t\t\t\t\t\t</td>\n";
                                texte += espace + "\t\t\t\t\t\t<td>\n";
                                texte += espace + "\t\t\t\t\t\t\t" + info_source.N2_DATA_EVEN + ".\n";
                                texte += espace + "\t\t\t\t\t\t</td>\n";
                                texte += espace + "\t\t\t\t\t</tr>\n";
                            }
                            // Date période
                            if (info_source.N3_DATA_DATE != null)
                            {
                                texte += espace + "\t\t\t\t\t<tr>\n";
                                texte += espace + "\t\t\t\t\t\t<td style=\"width:150px\">\n";
                                texte += espace + "\t\t\t\t&nbsp;\n";
                                texte += espace + "\t\t\t\t\t\t</td>\n";
                                texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                                texte += espace + "\t\t\t\tDate période: \n";
                                texte += espace + "\t\t\t</td>\n";
                                texte += espace + "\t\t\t<td>\n";
                                texte += espace + "\t\t\t\t" + info_source.N3_DATA_DATE + "\n";
                                texte += espace + "\t\t\t</td>\n";
                                texte += espace + "\t\t</tr>\n";
                            }
                            // Lieu de juridiction
                            if (info_source.N3_DATA_PLAC != null)
                            {
                                texte += espace + "\t\t\t\t\t<tr>\n";
                                texte += espace + "\t\t\t\t\t\t<td style=\"width:150px\">\n";
                                texte += espace + "\t\t\t\t&nbsp;\n";
                                texte += espace + "\t\t\t\t\t\t</td>\n";
                                texte += espace + "\t\t\t\t\t\t<td style=\"width:150px\">\n";
                                texte += espace + "\t\t\t\t\t\t\tLieu de juridiction: \n";
                                texte += espace + "\t\t\t\t\t\t</td>\n";
                                texte += espace + "\t\t\t\t\t\t<td>\n";
                                texte += espace + "\t\t\t\t\t\t\t" + info_source.N3_DATA_PLAC + ".\n";
                                texte += espace + "\t\t\t\t\t\t</td>\n";
                                texte += espace + "\t\t\t\t\t</tr>\n";
                            }
                            // Agence
                            if (info_source.N2_DATA_AGNC != null)
                            {
                                texte += espace + "\t\t\t\t\t<tr>\n";
                                texte += espace + "\t\t\t\t\t\t<td style=\"width:150px\">\n";
                                texte += espace + "\t\t\t\t&nbsp;\n";
                                texte += espace + "\t\t\t\t\t\t</td>\n";
                                texte += espace + "\t\t\t\t\t\t<td style=\"width:150px\">\n";
                                texte += espace + "\t\t\t\t\t\t\tAgence:\n";
                                texte += espace + "\t\t\t\t\t\t</td>\n";
                                texte += espace + "\t\t\t\t\t\t<td>\n";
                                texte += espace + "\t\t\t\t\t\t\t" + info_source.N2_DATA_AGNC + "\n";
                                texte += espace + "\t\t\t\t\t\t</td>\n";
                                texte += espace + "\t\t\t\t\t</tr>\n";
                            }
                            // Note
                            if (info_source.N2_DATA_NOTE_liste_ID != null)
                            {
                                texte += espace + "\t\t\t\t\t<tr>\n";
                                texte += espace + "\t\t\t\t\t\t<td style=\"width:150px\">\n";
                                texte += espace + "\t\t\t\t&nbsp;\n";
                                texte += espace + "\t\t\t\t\t\t</td>\n";
                                texte += espace + "\t\t\t\t\t\t<td colspan=2>\n";
                                texte += espace + Avoir_lien_note(info_source.N2_DATA_NOTE_liste_ID, liste_note_ID_numero, true, 7) + "\n";
                                texte += espace + "\t\t\t\t\t\t</td>\n";
                                texte += espace + "\t\t\t\t\t</tr>\n";
                            }
                            texte += espace + "\t\t\t\t</table>\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // AUTH
                        if (info_source.N1_AUTH != null)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                            texte += espace + "\t\t\t\tOriginateur:\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t\t<td>\n";
                            texte += espace + "\t\t\t\t" + info_source.N1_AUTH + "\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // ABBR
                        if (info_source.N1_ABBR != null)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                            texte += espace + "\t\t\t\tSaissi des données:\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t\t<td>\n";
                            texte += espace + "\t\t\t\t" + info_source.N1_ABBR + "\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // PUBL
                        if (info_source.N1_PUBL != null)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                            texte += espace + "\t\t\t\tFait sur la publication:\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t\t<td>\n";
                            texte += espace + "\t\t\t\t" + info_source.N1_PUBL + "\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // TEXT
                        if (info_source.N1_PUBL != null)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                            texte += espace + "\t\t\t\tTexte de la source:\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t\t<td>\n";
                            texte += espace + "\t\t\t\t" + info_source.N1_TEXT + "\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // REFN
                        if (info_source.N1_REFN_liste != null)
                        {
                            foreach (GEDCOMClass.USER_REFERENCE_NUMBER info in info_source.N1_REFN_liste)
                            {
                                texte += espace + "\t\t<tr>\n";
                                texte += espace + "\t\t\t<td colspan=2 style=\"border:0px solid #0e0;text-align:left\">\n";
                                texte += "\t\t\t\t\t\tNuméro de fichier d'enregistrement permanent(REFN): " + info.N0_REFN + " Type " + info.N1_TYPE + "\n";
                                texte += espace + "\t\t\t</td>\n";
                                texte += espace + "\t\t</tr>\n";
                            }
                        }
                        // RIN
                        if (info_source.N1_RIN != null)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                            texte += espace + "\t\t\t\t\tRIN:\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t\t<td>\n";
                            texte += espace + "\t\t\t\t" + info_source.N1_RIN + "\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // REPO
                        if (info_source.N1_REPO_info != null)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                            texte += espace + "\t\t\t\t\tDépôt:\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t\t<td>\n";
                            // CALN
                            if (info_source.N1_REPO_info.N1_CALN != null)
                                texte += espace + "Référence du fond (CALN): " + info_source.N1_REPO_info.N1_CALN;
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                            if (info_source.N1_REPO_info.N2_CALN_MEDI != null)
                            {
                                texte += espace + "\t\t<tr>\n";
                                texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                                texte += espace + "\t\t\t\t\t&nbsp;\n";
                                texte += espace + "\t\t\t</td>\n";
                                texte += espace + "\t\t\t<td>\n";
                                texte += espace + "Support du média: " + info_source.N1_REPO_info.N2_CALN_MEDI;
                                texte += espace + "\t\t\t</td>\n";
                                texte += espace + "\t\t</tr>\n";
                            }
                            // note du dépôt
                            if (info_source.N1_REPO_info.N1_NOTE_liste_ID.Count > 0)
                            {
                                texte += espace + "\t\t<tr>\n";
                                texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                                texte += espace + "\t\t\t\t\t&nbsp;\n";
                                texte += espace + "\t\t\t</td>\n";
                                texte += espace + "\t\t\t<td>\n";
                                texte += Avoir_lien_note(info_source.N1_REPO_info.N1_NOTE_liste_ID, liste_note_ID_numero, true, 4);
                                texte += espace + "\t\t\t</td>\n";
                                texte += espace + "\t\t</tr>\n";
                            }
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                            texte += espace + "\t\t\t\t\t&nbsp;\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t\t<td>\n";
                            (temp, liste_repo_ID_numero) = Avoir_lien_repo(info_source.N1_REPO_info.N0_ID, liste_repo_ID_numero, tab + 4);
                            texte += espace + "\t\t\t\t" + temp + "\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // NOTE
                        if (info_source.N1_NOTE_liste_ID.Count > 0)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td colspan=2 style=\"border:0px solid #0e0;text-align:left;\">\n";
                            texte += Avoir_lien_note(info_source.N1_NOTE_liste_ID, liste_note_ID_numero, true, 4);
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // date de changement
                        if (GH.Properties.Settings.Default.voir_date_changement)
                        {
                            if (info_source.N1_CHAN != null)
                            {
                                if (info_source.N1_CHAN.N1_CHAN_DATE != null)
                                {
                                    texte += espace + "\t<tr>\n";
                                    texte += espace + "\t\t<td colspan=2 style=\"vertical-align:bottom\">\n";
                                    texte += Date_Changement_Bloc(
                                            info_source.N1_CHAN,
                                            liste_note_ID_numero,
                                            tab + 3,
                                            false);
                                    texte += espace + "\t\t</td>\n";
                                    texte += espace + "\t</tr>\n";
                                }
                            }
                        }
                    }
                    texte += espace + "\t</table>\n";
                }
            }
            texte += Groupe("fin", tab);
            return (texte, true);
        }
        public void Message_HTML(string ligne1 = "", string ligne2 = "", string ligne3 = "")
        {
            Label Lb_HTML_1 = Application.OpenForms["GH"].Controls["Lb_HTML_1"] as Label;
            Label Lb_HTML_2 = Application.OpenForms["GH"].Controls["Lb_HTML_2"] as Label;
            Label Lb_HTML_3 = Application.OpenForms["GH"].Controls["Lb_HTML_3"] as Label;
            Label Lb_HTML_4 = Application.OpenForms["GH"].Controls["Lb_HTML_4"] as Label;
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
        private static (string, List<ID_numero>, List<ID_numero>, List<ID_numero>, List<ID_numero>) Objet
            (
            GEDCOMClass.MULTIMEDIA_RECORD infoMedia, 
            string sousDossier,
            string dossierSortie,
            List<ID_numero> liste_note_ID_numero,
            List<ID_numero> liste_citation_ID_numero,
            List<ID_numero> liste_source_ID_numero,
            List<ID_numero> liste_repo_ID_numero,
            int tab)
        {
            string texte = "";
            string temp;
            string espace = Tabulation(tab);
            if (infoMedia == null)
            {
                texte += espace + "<table style=\"height:fit-content;width:100%;border:0px solid #00e;\">\n";
                texte += espace + "\t<tr>\n";
                texte += espace + "\t\t<td style=\"border:0px solid #0e0;height:150px\">\n";
                texte += espace + "\t\t\t<div class=\"media_manquant\">\n";
                texte += espace + "\t\t\t\t<div style=\"font-size:50px\">?</div>\n";
                texte += espace + "\t\t\t\tN'a pas trouver le fichier\n";
                texte += espace + "\t\t\t</div>\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
                texte += espace + "</table>\n";
                return (texte, liste_note_ID_numero, liste_citation_ID_numero, liste_source_ID_numero, liste_repo_ID_numero);
            }
            texte += espace + "<table style=\"height:fit-content;width:100%;border:0px solid #00e;\">\n";
            texte += espace + "\t<tr>\n";
            texte += espace + "\t\t<td style=\"border:0px solid #0e0;height:150px\">\n";
            string fichierMedia = CopierObjet(infoMedia.N1_FILE, sousDossier, dossierSortie);
            string extention = null;
            if (fichierMedia != null) extention = Path.GetExtension(fichierMedia.ToLower());
            if (extention != null) // trouver fichier
            {
                // image
                if (extention == ".jpg" ||
                    extention == ".jpeg" ||
                    extention == ".gif" ||
                    extention == ".bmp" ||
                    extention == ".svg" ||
                    extention == ".png"
                )
                {
                    if (fichierMedia != "" && sousDossier != "commun" && sousDossier != "index")
                    {
                        texte += espace + "\t\t\t\t<img class=\"mini\" src=\"" + "medias/" + fichierMedia + "\" alt=\"\" />\n";
                    }
                    else if ((sousDossier == "commun" || sousDossier == "index") && fichierMedia != "")
                    {
                        texte += espace + "\t\t\t\t<img class=\"mini\" src=\"" + "commun/" + fichierMedia + "\" alt=\"\" />\n";
                    }
                }
                // Image tif, pic et pcx doivent être télécharger.
                else if (
                        extention == ".tif" ||
                        extention == ".tiff" ||
                        extention == ".pic" ||
                        extention == ".pcx" ||
                        extention == ".pic" || // Macintosh
                        extention == ".pict" || // Macintosh
                        extention == ".pct" || // Macintosh
                        extention == ".mac" || // Macintosh
                        extention == ".aif" || // Macintosh
                        extention == ".psd" || // photoshop
                        extention == ".tga"
                        )
                {
                    //if (extention == ".pic" || extention == ".pict" || extention == ".pct" || extention == ".aif") extention = ".mac";
                    if (fichierMedia != "" && sousDossier != "commun" && sousDossier != "index")
                    {
                        texte += espace + "\t\t\t<a href=\"medias/" + fichierMedia + "\" target=\"_BLANK\">\n";
                        texte += espace + "\t\t\t\t<img style=\"height:150px\" src=\"../commun/" + infoMedia.N2_FILE_FORM.ToLower() + ".svg\" alt=\"\" />\n";
                        texte += espace + "\t\t\t</a>\n";
                    }
                    else if ((sousDossier == "commun" || sousDossier == "index") && fichierMedia != "")
                    {
                        texte += espace + "\t\t\t<a href=\"commun/" + fichierMedia + "\" >\n";
                        texte += espace + "\t\t\t\t<img style=\"height:150px\" src=\"../commun/" + infoMedia.N2_FILE_FORM.ToLower() + ".svg\" alt=\"\" />\n";
                        texte += espace + "\t\t\t</a>\n";
                    }
                }
                // video audio
                else if (
                    extention == ".mov" ||
                    extention == ".aac" ||
                    extention == ".avi" ||
                    extention == ".flac" ||
                    extention == ".mkv" ||
                    extention == ".wav" ||
                    extention == ".mpg" ||
                    extention == ".mp3"
                    )
                {
                    texte += espace + "\t\t\t<a href=\"medias/" + fichierMedia + "\" target=\"_BLANK\">\n";
                    texte += espace + "\t\t\t\t<img style=\"height:150px\" src=\"../commun/media.svg\" alt=\"\" />\n";
                    texte += espace + "\t\t\t</a>\n";
                }
                // pdf
                else if (extention == ".pdf")
                {
                    if (fichierMedia != null && sousDossier != "commun" && sousDossier != "index")
                    {
                        texte += espace + "\t\t\t<a class=\"media\" href=\"" + "medias/" + fichierMedia + "\" target=\"_BLANK\">\n";
                        texte += espace + "\t\t\t\t<img style=\"height:150px\" src=\"" + "../commun/pdf.svg\" alt=\"\" />\n";
                        texte += espace + "\t\t\t</a>\n";
                    }
                    else if ((sousDossier == "commun" || sousDossier == "index") && fichierMedia != "")
                    {
                        texte += espace + "\t\t\t<a href=\"commun/" + fichierMedia + "\" >\n";
                        texte += espace + "\t\t\t\t<img style=\"height:150px\" src=\"../commun/pdf.svg\" alt=\"\" />\n";
                        texte += espace + "\t\t\t</a>\n";
                    }
                }
                // link 
                else if (infoMedia.N2_FILE_FORM.ToLower() == "url")
                {
                    texte += espace + "\t\t\t<a class=\"media\" href=\"" + infoMedia.N1_FILE + "\" target=\"_BLANK\">\n";
                    texte += espace + "\t\t\t\t<img style=\"height:150px\" src=\"" + "../commun/www.svg\" alt=\"\" />\n";
                    texte += espace + "\t\t\t</a>\n";
                }
                // tout les autres avec extention
                else
                {
                    texte += espace + "\t\t\t<a class=\"media\" href=\"" + "medias/" + fichierMedia + "\" target=\"_BLANK\">\n";
                    texte += espace + "\t\t\t\t<img style=\"height:150px\" src=\"" + "../commun/doc.svg\" alt=\"\" />\n";
                    texte += espace + "\t\t\t</a>\n";
                }
            }
            else // pas trouver de fichier
            {
                texte += espace + "\t\t\t\t<div class=\"media_manquant\">\n";
                texte += espace + "\t\t\t\t\t<div style=\"font-size:50px\">?</div>\n";
                texte += espace + "\t\t\t\t\tN'a pas trouver le fichier\n";
                texte += espace + "\t\t\t\t</div>\n";
            }
            texte += espace + "\t\t</td>\n";
            texte += espace + "\t</tr>\n";
            // ID
            if (GH.Properties.Settings.Default.deboguer)
            {
                texte += espace + "\t<tr>\n";
                texte += espace + "\t\t<td style=\"border:0px solid #0e0;text-align:center\">\n";
                texte += espace + "\t\t\t\t{" + infoMedia.N0_ID + "}\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            if (infoMedia.N3_FILE_FORM_TYPE != null )
            {
                texte += espace + "\t<tr>\n";
                texte += espace + "\t\t<td style=\"border:0px solid #0e0;text-align:center\">\n";
                texte += espace + "\t\t\t\t" + infoMedia.N3_FILE_FORM_TYPE + "\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // titre
            if (infoMedia.N2_FILE_TITL != null)
            {
                texte += espace + "\t<tr>\n";
                texte += espace + "\t\t<td style=\"border:0px solid #0e0;text-align:left\">\n";
                texte += espace + "\t\t\t\t" + infoMedia.N2_FILE_TITL + "\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // REFM
            if (infoMedia.N1_REFN_liste != null)
            {
                foreach (GEDCOMClass.USER_REFERENCE_NUMBER info_REFN in infoMedia.N1_REFN_liste)
                {
                    texte += espace + "\t<tr>\n";
                    texte += espace + "\t\t<td colspan=2 style=\"border:0px solid #0e0;text-align:left\">\n";
                    texte += espace + "\t\t\tREFN: " + info_REFN.N0_REFN + " Type: " + info_REFN.N1_TYPE + "\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
            }
            // RIN
            if (infoMedia.N1_RIN != null)
            {
                texte += espace + "\t<tr>\n";
                texte += espace + "\t\t<td style=\"border:0px solid #0e0;text-align:left\">\n";
                texte += espace + "\t\t\t\tRIN: " + infoMedia.N1_RIN + "\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // note
            texte += espace + "\t<tr>\n";
            texte += espace + "\t\t<td style=\"border:0px solid #0e0;text-align:left\">\n";
            texte += Avoir_lien_note(infoMedia.N1_NOTE_liste_ID, liste_note_ID_numero, true, tab + 3);
            texte += espace + "\t\t</td>\n";
            texte += espace + "\t</tr>\n";
            // référence
            if (infoMedia.N1_SOUR_citation_liste_ID != null && GH.Properties.Settings.Default.voir_reference)
            {
                texte += espace + "\t<tr>\n";
                texte += espace + "\t\t<td style=\"border:0px solid #0e0;text-align:left\">\n";
                temp = Avoir_lien_citation_source(infoMedia.N1_SOUR_citation_liste_ID, liste_citation_ID_numero, 6);
                texte += temp;
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // si a une date de changement
            GEDCOMClass.CHANGE_DATE N1_CHAN = infoMedia.N1_CHAN;
            if (GH.Properties.Settings.Default.voir_date_changement)
            {
                if (N1_CHAN != null)
                {
                    if (N1_CHAN.N1_CHAN_DATE != null)
                    {
                        texte += espace + "\t<tr>\n";
                        texte += espace + "\t\t<td style=\"vertical-align:bottom\">\n";
                        texte += Date_Changement_Bloc(
                                N1_CHAN,
                                liste_note_ID_numero,
                                tab + 3,
                                true);
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                }
            }
            texte += espace + "</table>\n";
            return (texte, liste_note_ID_numero, liste_citation_ID_numero, liste_source_ID_numero, liste_repo_ID_numero);
        }
        private (string, bool) Groupe_chercheur(
            List<ID_numero> liste, 
            string DossierSortie,
            string sousDossier,
            List<ID_numero> liste_note_ID_numero,
            int tab)
        {
            if(GH.Properties.Settings.Default.voir_chercheur == false || liste.Count == 0)
                return ("", false);
            string espace = Tabulation(tab);
            string texte = "";
            texte += Separation("large", tab);
            texte += "<a id=\"groupe_chercheur\"></a>";
            texte += Groupe("debut", tab);
            texte += espace + "<table class=\"titre\">\n";
            texte += espace + "\t<tr>\n";
            texte += espace + "\t\t<td>\n";
            if (sousDossier == "index")
                texte += espace + "\t\t\t\t<img  style=\"vertical-align:middle;heigh:64px\" src=\"commun\\tete.svg\" />\n";
            else
                texte += espace + "\t\t\t\t<img  style=\"vertical-align:middle;heigh:64px\" src=\"..\\commun\\tete.svg\" />\n";
            if (liste.Count == 1)
                texte += espace + "\t\t\t\tChercheur\n";
            else
                texte += espace + "\t\t\t\tChercheurs\n";
            texte += espace + "\t\t</td>\n";
            texte += espace + "\t</tr>\n";
            texte += espace + "</table>\n";
            texte += Avoir_chercheur( liste, DossierSortie, sousDossier, liste_note_ID_numero, tab);
            return (texte, true);
        }
        private static string Separation(string px = "large", int tab = 0, string s = "")
        {
            string espace = Tabulation(tab);
            if (px == "large")
            {
                string texte = espace + "<div style =\"height:15px;\">"+ s + "<!--sépatration large -->\n";
                texte += espace + "</div>\n";
                return texte;
            }
            if (px == "moyen")
            {
                string texte = espace + "<div style =\"height:10px;\">" + s + "<!--sépatration moyen -->\n";
                texte += espace + "</div>\n";
                return texte;
            }
            if (px == "mince")
            {
                string texte = espace + "<div style =\"height:05px;\">" + s + "<!--sépatration mince -->\n";
                texte += espace + "</div>\n";
                return texte;
            }
            return "<!--sépatration ? -->\n";
        }
        private static string Tabulation(int tab = 0)
        {
            return string.Concat(Enumerable.Repeat("    ", tab));

        }
        private static (List<ID_numero>, bool) Verifier_liste(List<string> liste_ID, List<ID_numero> liste_numero_ID)
        {
            if (liste_ID == null) return (liste_numero_ID, false); 
            if (liste_ID.Count == 0) return (liste_numero_ID, false);
            bool modifier = false;
            foreach (string ID in liste_ID) {
                bool trouver = false;
                int numero = 1;
                // vérifie si la liste est  vide
                if (liste_numero_ID != null)
                {
                    // verifie si déjà dans la liste et trouve le prochain numero
                    for (int f = 0; f < liste_numero_ID.Count; f++)
                    {
                        if (liste_numero_ID[f].ID == ID) trouver = true;
                        if (numero <= liste_numero_ID[f].numero) numero = liste_numero_ID[f].numero + 1;
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
        public class Liste_par_date : IComparer<GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE>
        {
            public int Compare(GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE x, GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE y)
            {
                return x.N2_DATE.CompareTo(y.N2_DATE);
            }
        }
    }
}
